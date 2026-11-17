using System;
using System.Collections.Generic;
using System.Reflection;
using RAN.Mobile.GameModels;
using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Snapshot of a character's fully derived stats — the Unity port of
	/// GLCHARLOGIC::SUM_ADDITION (GLogixExPC.cpp:219). All constants come from the
	/// original charset data (<c>Assets/Resources/_ranclient/data/glogic/class*.asset</c>)
	/// and <c>defaultcharset.asset</c>, loaded once and cached.
	///
	/// Stat semantics (as in the original):
	///   STR → Max HP,  SPI → Max MP,  STA → Max SP,
	///   POW → physical/ranged attack (PA/SA),  DEX → PA/SA/MA + defense (+hit/avoid from gear),
	///   INT → magic attack component (unused by the four base classes).
	/// </summary>
	public sealed class DerivedCharacterStats
	{
		// Totals = base + growth*(level-1) + allocated points (+ gear stats, when supplied).
		public int Pow, Str, Spi, Dex, Int, Sta;

		public int HpMax;
		public int MpMax;
		public int SpMax;

		public int SumAp;          // attack power base (AP)
		public int SumDp;          // defense power base (DP)
		public int Pa, Sa, Ma;     // physical / ranged / magic attack additions
		public int Hit, Avoid;
		public int DefenseBody;
		public int Defense;

		/// <summary>Base damage range before weapon/skill additions (gdDamage = AP..AP).</summary>
		public int DamageLow;
		public int DamageHigh;

		public float WalkSpeed;
		public float RunSpeed;

		/// <summary>Weapon attack range (cells); filled by the caller from equipped gear.</summary>
		public int AttackRange;
	}

	/// <summary>Gathered gear contributions (SUM_ITEM subset we can resolve from ItemDatabase).</summary>
	public struct GearTerms
	{
		public int DamageLow;
		public int DamageHigh;
		public int Defense;
		public int Hit;
		public int Avoid;
		public int Hp;
		public int Mp;
		public int Sp;

		public static GearTerms Zero { get { return default(GearTerms); } }
	}

	/// <summary>
	/// Loads the original RAN Online character data and exposes the exact formulas:
	/// derived stats (SUM_ADDITION), exp requirements (exptable_max), level-up rules.
	/// </summary>
	public static class CharacterStatBlock
	{
		private const string ConstPath = "_ranclient/data/glogic/defaultcharset";
		private const string ExpPath = "_ranclient/data/glogic/exptable_max";

		private static DefaultCharsetObject _consts;
		private static ExperienceTableObject _exp;
		private static bool _constsTried;
		private static bool _expTried;

		// PlayerConstant's public accessors are decompile stubs; the private
		// [SerializeField] fields still hold the real asset data, so read them once.
		private static readonly Dictionary<string, FieldInfo> _pcFields = new Dictionary<string, FieldInfo>();
		private static readonly Dictionary<string, FieldInfo> _paFields = new Dictionary<string, FieldInfo>();
		private static readonly Dictionary<int, CharsetObject> _classCache = new Dictionary<int, CharsetObject>();

		private static readonly List<long> _fallbackExp = new List<long>();

		#region asset access

		public static DefaultCharsetObject Constants
		{
			get
			{
				if (_consts == null && !_constsTried)
				{
					_constsTried = true;
					_consts = Resources.Load<DefaultCharsetObject>(ConstPath);
					if (_consts == null)
					{
						Debug.LogWarning("[CharacterStatBlock] defaultcharset not found at " + ConstPath);
					}
				}
				return _consts;
			}
		}

		public static ExperienceTableObject Experience
		{
			get
			{
				if (_exp == null && !_expTried)
				{
					_expTried = true;
					_exp = Resources.Load<ExperienceTableObject>(ExpPath);
					if (_exp == null)
					{
						Debug.LogWarning("[CharacterStatBlock] exptable_max not found at " + ExpPath);
					}
				}
				return _exp;
			}
		}

		/// <summary>Class slot 0..3 = Brawler / Swordsman / Archer / Shaman (schools share constants).</summary>
		public static int ClassSlot(int classId)
		{
			switch (classId)
			{
				case 2: return 1;      // Swordsman (GLCC_ARMS)
				case 4: return 2;      // Archer   (GLCC_ARCHER)
				case 8: return 3;      // Shaman   (GLCC_SPIRIT)
				default: return 0;     // Brawler / unknown → 0
			}
		}

		/// <summary>The CharsetObject (class00..03) for a GLCC class id, or null.</summary>
		public static CharsetObject ClassInit(int classId)
		{
			int slot = ClassSlot(classId);
			CharsetObject cs;
			if (_classCache.TryGetValue(slot, out cs))
			{
				return cs;
			}
			DefaultCharsetObject def = Constants;
			if (def == null || def.classInit0 == null || slot >= def.classInit0.Length)
			{
				return null;
			}
			cs = def.classInit0[slot];
			_classCache[slot] = cs;
			return cs;
		}

		#endregion

		#region reflection helpers (gutted-decompile safe)

		private static FieldInfo PcField(string name)
		{
			FieldInfo f;
			if (!_pcFields.TryGetValue(name, out f))
			{
				f = typeof(PlayerConstant).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
				_pcFields[name] = f;
			}
			return f;
		}

		/// <summary>Read a private float field of PlayerConstant (e.g. "_HP_Str").</summary>
		private static float Pcf(PlayerConstant pc, string name)
		{
			FieldInfo f = PcField(name);
			if (f == null)
			{
				return 0f;
			}
			object v = f.GetValue(pc);
			return (v is float) ? (float)v : 0f;
		}

		/// <summary>Read a private struct field of PlayerConstant (e.g. "_Begin_Stats" → CharStats).</summary>
		private static T PcStruct<T>(PlayerConstant pc, string name) where T : struct
		{
			FieldInfo f = PcField(name);
			if (f == null)
			{
				return default(T);
			}
			object v = f.GetValue(pc);
			return (v is T) ? (T)v : default(T);
		}

		/// <summary>Read PAData/PAFData private "_min"/"_max" (their properties are stubs too).</summary>
		private static void ReadPaPair(object pa, out long min, out long max)
		{
			min = 0;
			max = 0;
			if (pa == null)
			{
				return;
			}
			Type t = pa.GetType();
			FieldInfo fMin;
			if (!_paFields.TryGetValue(t.FullName + "._min", out fMin))
			{
				fMin = t.GetField("_min", BindingFlags.Instance | BindingFlags.NonPublic);
				_paFields[t.FullName + "._min"] = fMin;
			}
			FieldInfo fMax;
			if (!_paFields.TryGetValue(t.FullName + "._max", out fMax))
			{
				fMax = t.GetField("_max", BindingFlags.Instance | BindingFlags.NonPublic);
				_paFields[t.FullName + "._max"] = fMax;
			}
			if (fMin != null)
			{
				min = Convert.ToInt64(fMin.GetValue(pa));
			}
			if (fMax != null)
			{
				max = Convert.ToInt64(fMax.GetValue(pa));
			}
		}

		#endregion

		#region derived stats — SUM_ADDITION()

		/// <summary>
		/// Exact port of GLCHARLOGIC::SUM_ADDITION (GLogixExPC.cpp:219), minus passives/school
		/// conveniences the Unity client does not carry yet. Every intermediate that the C++
		/// casts to WORD is truncated the same way.
		/// </summary>
		public static DerivedCharacterStats Compute(int classId, int level, CharStats allocated, GearTerms gear)
		{
			DerivedCharacterStats s = new DerivedCharacterStats();
			if (level < 1)
			{
				level = 1;
			}
			CharsetObject cs = ClassInit(classId);
			if (cs == null || cs.CharConstant.Animations == null && cs.CharConstant.WalkVelocity == 0f && PcField("_Begin_Stats") == null)
			{
				// No class data — fall back to sane defaults so gameplay still functions.
				return FallbackCompute(level, allocated);
			}
			PlayerConstant k = cs.CharConstant;
			int zb = level - 1;

			CharStats begin = PcStruct<CharStats>(k, "_Begin_Stats");
			CharFloatStats grow = PcStruct<CharFloatStats>(k, "_LevelUp_Stats");

			// stats = begin + growth*(level-1) + allocated   (truncated to whole points)
			s.Pow = Floor(begin.Pow + grow.Pow * zb) + allocated.Pow;
			s.Str = Floor(begin.Str + grow.Str * zb) + allocated.Str;
			s.Spi = Floor(begin.Spi + grow.Spi * zb) + allocated.Spi;
			s.Dex = Floor(begin.Dex + grow.Dex * zb) + allocated.Dex;
			s.Int = Floor(begin.Int + grow.Int * zb) + allocated.Int;
			s.Sta = Floor(begin.Sta + grow.Sta * zb) + allocated.Sta;

			// AP / DP base
			s.SumAp = Trunc((Pcf(k, "_Begin_AP") + Pcf(k, "_LevelUp_AP") * zb) * Pcf(k, "_conv_AP"));
			s.SumDp = Trunc((Pcf(k, "_Begin_DP") + Pcf(k, "_LevelUp_DP") * zb) * Pcf(k, "_conv_DP"));

			// PA / SA / MA (level part + stat part)
			int pa = Trunc((Pcf(k, "_Begin_PA") + Pcf(k, "_LevelUp_PA") * zb) * Pcf(k, "_conv_PA"));
			pa += Floor(s.Pow * Pcf(k, "_PA_Pow") + s.Dex * Pcf(k, "_PA_Dex"));
			s.Pa = pa;

			int sa = Trunc((Pcf(k, "_Begin_SA") + Pcf(k, "_LevelUp_SA") * zb) * Pcf(k, "_conv_SA"));
			sa += Floor(s.Pow * Pcf(k, "_SA_Pow") + s.Dex * Pcf(k, "_SA_Dex"));
			s.Sa = sa;

			s.Ma = Floor(s.Dex * Pcf(k, "_MA_Dex") + s.Spi * Pcf(k, "_MA_Spi") + s.Int * Pcf(k, "_MA_Int"));

			// Vitals
			s.HpMax = Floor(s.Str * Pcf(k, "_HP_Str") + gear.Hp);
			s.MpMax = Floor(s.Spi * Pcf(k, "_MP_Spi") + gear.Mp);
			s.SpMax = Floor(s.Sta * Pcf(k, "_SP_Sta") + gear.Sp);
			if (s.HpMax < 1) { s.HpMax = 1; }
			if (s.MpMax < 0) { s.MpMax = 0; }
			if (s.SpMax < 0) { s.SpMax = 0; }

			// Hit / avoid / defense (DEX-scaled; per-class hit_Dex/avoid_Dex are 0 → gear decides)
			s.Hit = Floor(s.Dex * Pcf(k, "_hit_Dex") + gear.Hit);
			s.Avoid = Floor(s.Dex * Pcf(k, "_avoid_Dex") + gear.Avoid);
			s.DefenseBody = s.SumDp + Floor(s.Dex * Pcf(k, "_defense_Dex"));
			s.Defense = s.DefenseBody + gear.Defense;

			// Base damage range = AP (gdDamage), weapon damage is added later in CALCDAMAGE.
			s.DamageLow = s.SumAp;
			s.DamageHigh = s.SumAp;

			s.WalkSpeed = Pcf(k, "_walkVelocity");
			s.RunSpeed = Pcf(k, "_runVelocity");
			if (s.WalkSpeed <= 0f) { s.WalkSpeed = 14f; }
			if (s.RunSpeed <= 0f) { s.RunSpeed = 37f; }
			return s;
		}

		private static DerivedCharacterStats FallbackCompute(int level, CharStats allocated)
		{
			DerivedCharacterStats s = new DerivedCharacterStats();
			int zb = level - 1;
			s.Pow = 10 + allocated.Pow;
			s.Str = 34 + 4 * zb + allocated.Str;
			s.Spi = 9 + allocated.Spi;
			s.Dex = 10 + allocated.Dex;
			s.Int = allocated.Int;
			s.Sta = 9 + 3 * zb + allocated.Sta;
			s.SumAp = Trunc((4 + 1.2f * zb) * 0.4f);
			s.SumDp = Trunc((5 + 0.1f * zb) * 0.75f);
			s.Pa = Trunc((5 + 0.4f * zb) * 0.8f) + Floor(s.Pow * 0.14f + s.Dex * 0.1f);
			s.Sa = Trunc((3 + 0.18f * zb) * 0.2f) + Floor(s.Pow * 0.08f + s.Dex * 0.18f);
			s.Ma = Floor(s.Spi * 0.22f + s.Dex * 0.12f);
			s.HpMax = Math.Max(1, Floor(s.Str * 1.8f));
			s.MpMax = Floor(s.Spi * 0.65f);
			s.SpMax = Floor(s.Sta * 1.2f);
			s.DefenseBody = s.SumDp + Floor(s.Dex * 0.1f);
			s.Defense = s.DefenseBody;
			s.DamageLow = s.SumAp;
			s.DamageHigh = s.SumAp;
			s.WalkSpeed = 14f;
			s.RunSpeed = 37f;
			return s;
		}

		private static int Floor(float v)
		{
			return Mathf.FloorToInt(v);
		}

		/// <summary>C-style truncation toward zero (WORD cast in the C++ source).</summary>
		private static int Trunc(float v)
		{
			return (int)v;
		}

		#endregion

		#region experience — exptable_max

		/// <summary>
		/// Exp required to advance FROM <paramref name="level"/> to level+1.
		/// Table verified against the live game: at Lv.22 the HUD shows "…/3173" and
		/// expTable[22] == 3173.
		/// </summary>
		public static long ExpForLevel(int level)
		{
			long[] table = ExpTable;
			if (table != null && table.Length > 0)
			{
				int idx = Mathf.Clamp(level, 0, table.Length - 1);
				long v = table[idx];
				if (v > 0)
				{
					return v;
				}
			}
			// Legacy curve fallback (GLogicEx historical formula).
			double l = Math.Max(1, level);
			return (long)(((l + 1) * l + (l + 2) * (l + 2) * (l + 1 + 25)) * 1.3);
		}

		public static long[] ExpTable
		{
			get
			{
				ExperienceTableObject e = Experience;
				return (e != null) ? e.expTable : null;
			}
		}

		/// <summary>Highest playable level (defaultcharset.maxLevel, 150 in the shipped data).</summary>
		public static int MaxLevel
		{
			get
			{
				DefaultCharsetObject c = Constants;
				if (c != null && c.maxLevel > 0)
				{
					return c.maxLevel;
				}
				return 150;
			}
		}

		/// <summary>Stat points granted per level (defaultcharset.levelStatsPoint = 3).</summary>
		public static int StatPointsPerLevel
		{
			get
			{
				DefaultCharsetObject c = Constants;
				return (c != null && c.levelStatsPoint > 0) ? c.levelStatsPoint : 3;
			}
		}

		/// <summary>Skill points granted per level (defaultcharset.levelSkillPoint = 1).</summary>
		public static int SkillPointsPerLevel
		{
			get
			{
				DefaultCharsetObject c = Constants;
				return (c != null && c.levelSkillPoint > 0) ? c.levelSkillPoint : 1;
			}
		}

		#endregion

		#region gear aggregation

		private static Dictionary<long, ItemObject> _itemLookup;
		private static bool _itemLookupTried;

		/// <summary>Aggregate damage/defense/hit/avoid of equipped items (SUM_ITEM subset).</summary>
		public static GearTerms GearFromEquipped(List<WorldTcpClient.InventoryItemInfo> equipped)
		{
			GearTerms g = GearTerms.Zero;
			if (equipped == null || equipped.Count == 0)
			{
				return g;
			}
			EnsureItemLookup();
			for (int i = 0; i < equipped.Count; i++)
			{
				WorldTcpClient.InventoryItemInfo it = equipped[i];
				if (it == null)
				{
					continue;
				}
				ItemObject io;
				if (!_itemLookup.TryGetValue(Key(it.mainId, it.subId), out io) || io == null)
				{
					continue;
				}
				long min, max;
				ReadPaPair(io.damage, out min, out max);
				g.DamageLow += (int)min;
				g.DamageHigh += (int)max;
				g.Defense += io.defense;
				g.Hit += io.hitRate;
				g.Avoid += io.avoidRate;
			}
			return g;
		}

		private static long Key(int mainId, int subId)
		{
			return ((long)mainId << 32) | (uint)subId;
		}

		private static void EnsureItemLookup()
		{
			if (_itemLookup != null || _itemLookupTried)
			{
				return;
			}
			_itemLookupTried = true;
			_itemLookup = new Dictionary<long, ItemObject>();
			ItemDatabaseObject db = Resources.Load<ItemDatabaseObject>("database/ItemDatabase");
			if (db == null || db.ItemObjects == null)
			{
				return;
			}
			foreach (ItemObject io in db.ItemObjects)
			{
				if (io == null || io.data == null)
				{
					continue;
				}
				System.Reflection.FieldInfo fMain = io.data.GetType().GetField("mainId");
				System.Reflection.FieldInfo fSub = io.data.GetType().GetField("subId");
				if (fMain == null || fSub == null)
				{
					continue;
				}
				long k = Key(System.Convert.ToInt32(fMain.GetValue(io.data)), System.Convert.ToInt32(fSub.GetValue(io.data)));
				if (!_itemLookup.ContainsKey(k))
				{
					_itemLookup[k] = io;
				}
			}
		}

		/// <summary>Item template lookup by (mainId, subId); false when unknown.</summary>
		public static bool TryGetItem(int mainId, int subId, out ItemObject item)
		{
			EnsureItemLookup();
			return _itemLookup.TryGetValue(Key(mainId, subId), out item) && item != null;
		}

		#endregion
	}
}
