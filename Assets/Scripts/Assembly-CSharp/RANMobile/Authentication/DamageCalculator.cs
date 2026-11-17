using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>Damage result flags — mirrors DAMAGE_TYPE_* (GLDefine.h:729).</summary>
	[System.Flags]
	public enum DamageFlags
	{
		None = 0,
		Shock = 1,
		Critical = 2,
		CrushingBlow = 4,
		PsyReduce = 8,
		MagicReduce = 16,
		PsyReflection = 32,
		MagicReflection = 64
	}

	/// <summary>
	/// Target-side inputs for the damage roll — everything CALCDAMAGE pulls from the
	/// defender (GLChar.cpp GetDefense/GetBodyDefense/GetItemDefense/GetLevel/
	/// GETSTATE_DAMAGE/GETRESIST).
	/// </summary>
	public sealed class DamageTarget
	{
		public int Level = 1;
		/// <summary>Effective defense after bright adjustments (nDEFENSE).</summary>
		public int Defense;
		/// <summary>Body (default) defense component (nDEFAULT_DEFENSE).</summary>
		public int BodyDefense;
		/// <summary>Item defense component (nITEM_DEFENSE).</summary>
		public int ItemDefense;
		/// <summary>Incoming-damage multiplier from ailments (FROZEN etc.) — fSTATE_DAMAGE, default 1.</summary>
		public float StateDamage = 1f;
		/// <summary>Elemental resist of the skill's element, 0..99.</summary>
		public int Resist;
		/// <summary>Current HP percent (0..100) — used only for the target-side display.</summary>
		public int HpPercent = 100;

		public static DamageTarget Simple(int level, int defense)
		{
			DamageTarget t = new DamageTarget();
			t.Level = level;
			t.Defense = defense;
			t.BodyDefense = defense;
			t.ItemDefense = 0;
			return t;
		}
	}

	/// <summary>
	/// Unity port of the original combat math:
	///   GLOGICEX::GLHITRATE        (GLogicEx.cpp:39)
	///   critical / crushing rolls   (GLogixExPC.cpp:1160-1183)
	///   GLCHARLOGIC::CALCDAMAGE_20060328 (GLogixExPC.cpp:977-1265)
	///   GLOGICEX::CHECKSHOCK        (GLogicEx.cpp:55)
	/// Constants (criticalMax, criticalDamage, lowSeedDamage, damageDecRate,
	/// resistG/resistPhysicG, crushingBlow*, lowSPHitDrop) come from defaultcharset.asset.
	/// </summary>
	public static class DamageCalculator
	{
		#region hit

		private static readonly int[] BrightBias = { -10, 0, 10 };

		/// <summary>GLHITRATE: 100 + hit − avoid + alignment(−10/0/+10), clamped to [20,99].</summary>
		public static int HitRate(int hit, int avoid, int brightBiasIndex, bool lowSp)
		{
			int idx = Mathf.Clamp(brightBiasIndex + 1, 0, 2);
			int rate = 100 + hit - avoid + BrightBias[idx];
			if (lowSp)
			{
				DefaultCharsetObject c = CharacterStatBlock.Constants;
				float drop = (c != null) ? c.lowSPHitDrop : 0.25f;
				rate = (int)(rate * (1f - drop));
			}
			return Mathf.Clamp(rate, 20, 99);
		}

		/// <summary>Hit roll — original: nHitRate >= RANDOM*100.</summary>
		public static bool RollHit(int hit, int avoid, int brightBiasIndex, bool lowSp)
		{
			return HitRate(hit, avoid, brightBiasIndex, lowSp) >= (Random.value * 100f);
		}

		#endregion

		#region critical / crushing / shock

		/// <summary>
		/// Critical chance % — 1000/max(10,myHp%) − 10 + clamp(targetLvl−myLvl,−5,5)
		/// + gearCrit×100, clamped to [0, criticalMax].
		/// </summary>
		public static int CritChance(int myHpPercent, int myLevel, int targetLevel, int gearCritPercent)
		{
			DefaultCharsetObject c = CharacterStatBlock.Constants;
			int cap = (c != null) ? c.criticalMax : 40;
			int perHp = myHpPercent;
			if (perHp <= 10)
			{
				perHp = 10;
			}
			int lvlDiff = Mathf.Clamp(targetLevel - myLevel, -5, 5);
			int chance = 1000 / perHp - 10 + lvlDiff;
			chance += gearCritPercent;
			return Mathf.Clamp(chance, 0, cap);
		}

		/// <summary>Crushing-blow chance % (gear + skill spec), clamped to crushingBlowMax.</summary>
		public static int CrushChance(int gearCrushPercent, int skillSpecPercent)
		{
			DefaultCharsetObject c = CharacterStatBlock.Constants;
			int cap = (c != null) ? (int)c.crushingBlowMax : 40;
			return Mathf.Clamp(gearCrushPercent + skillSpecPercent, 0, cap);
		}

		/// <summary>CHECKSHOCK: no shock if target is &gt;5 levels below; needs dmg ≥ myLevel+6; 5% crit / 1% otherwise.</summary>
		public static bool RollShock(int myLevel, int targetLevel, int damage, bool critical)
		{
			const int MinDxLevel = 5;
			const int MinDamage = 6;
			const int CleanRate = 1;
			const int CritRate = 5;
			int dx = targetLevel - myLevel;
			if (-MinDxLevel > dx)
			{
				return false;
			}
			if ((myLevel + MinDamage) > damage)
			{
				return false;
			}
			return critical ? (CritRate > Random.value * 100f) : (CleanRate > Random.value * 100f);
		}

		#endregion

		#region damage — CALCDAMAGE_20060328

		/// <summary>Attacker-side inputs for one damage roll.</summary>
		public sealed class AttackContext
		{
			/// <summary>Attacker level.</summary>
			public int Level = 1;

			/// <summary>Attacker current-HP percent — drives crit chance (yes, lower HP = more crits).</summary>
			public int HpPercent = 100;

			/// <summary>Weapon damage range (m_gdDAMAGE / item gdDamage).</summary>
			public int DamageLow;
			public int DamageHigh;

			/// <summary>PA (melee) or SA (ranged) addition — VAR_PARAM is ADDITIVE.</summary>
			public int AttackAdd;

			/// <summary>Skill path only: |fBASIC_VAR × elementPower|.</summary>
			public int SkillVar;

			/// <summary>Weapon grind grade for EMGRINDING_DAMAGE (0..gradeMax); skill path only.</summary>
			public int Grade;

			/// <summary>Element-resist divisor for this damage type (resistPhysicG for melee/ranged, resistG for magic).</summary>
			public float ResistG = 0.1f;

			/// <summary>m_fDamageRate (buff/quest damage multiplier), default 1.</summary>
			public float DamageRate = 1f;

			/// <summary>Gear critical% (fraction×100 as in fIncR_Critical×100) and crushing%.</summary>
			public int GearCritPercent;
			public int GearCrushPercent;

			/// <summary>Extra crushing chance contributed by the skill spec (sSPEC.fVAR2×100).</summary>
			public int SkillCrushPercent;

			/// <summary>Reduction% from damage-reduce specs (0 = none).</summary>
			public float DamageReduce;

			/// <summary>Strike divisions to split the final hit across (m_wDivCount).</summary>
			public int StrikeCount = 1;

			/// <summary>True when this roll is a skill (applies skill var, grade, element resist); false = normal attack.</summary>
			public bool IsSkill;

			/// <summary>Magic damage type — ignores all defense terms and uses resistG.</summary>
			public bool IsMagic;

			/// <summary>Ranged attack (uses SA addition instead of PA in the normal path).</summary>
			public bool IsRanged;
		}

		/// <summary>
		/// Full damage roll. Returns final damage (≥1) and sets <paramref name="flags"/>
		/// to the DAMAGE_TYPE_* bitmask (Shock/Critical/CrushingBlow/…).
		/// </summary>
		public static int ComputeDamage(AttackContext ctx, DamageTarget target, out DamageFlags flags)
		{
			flags = DamageFlags.None;
			DefaultCharsetObject c = CharacterStatBlock.Constants;
			float damageGradeK = (c != null && c.damageGradeK > 0f) ? c.damageGradeK : 8f;
			float resistPhysicG = (c != null) ? c.resistPhysicG : 0.1f;
			float resistG = (c != null) ? c.resistG : 0.68f;
			float lowSeed = (c != null) ? c.lowSeedDamage : 0.1f;
			float damageDecRate = (c != null && c.damageDecRate > 0f) ? c.damageDecRate : 75000f;
			int critDamage = (c != null && c.criticalDamage > 0) ? c.criticalDamage : 120;
			int crushDamage = (c != null && c.crushingBlowDamage > 0) ? (int)c.crushingBlowDamage : 130;
			int maxLevel = CharacterStatBlock.MaxLevel;

			// --- seed range (gdDamage) ---
			float low = ctx.DamageLow;
			float high = ctx.DamageHigh;

			if (ctx.IsSkill)
			{
				// skill var + grind grade bonus, then element resist cut
				float grade = ctx.Grade / damageGradeK;
				float var = ctx.SkillVar;
				low += var + low * grade;
				high += var + high * grade;

				float resistGUsed = ctx.IsMagic ? resistG : resistPhysicG;
				float resistTotal = (Mathf.Min(target.Resist, 99) * 0.01f) * resistGUsed;
				low -= low * resistTotal;
				high -= high * resistTotal;
				if (low < 0f) { low = 0f; }
				if (high < 0f) { high = 0f; }
			}
			else
			{
				// normal attack: + weapon damage (already in DamageLow/High if caller folded it)
				// and the ADDITIVE PA/SA bonus (VAR_PARAM semantics).
				low += ctx.AttackAdd;
				high += ctx.AttackAdd;
			}

			// damage-rate multiplier (buffs)
			low *= ctx.DamageRate;
			high *= ctx.DamageRate;

			// underdog bonus: if the target is higher level, add up to (diff/10) of the roll
			int levelDiff = target.Level - ctx.Level;
			int extForce = 0;
			if (levelDiff > 0)
			{
				extForce = (int)(Random.value * levelDiff / 10f);
			}

			// critical roll (uses the ATTACKER's own hp%)
			bool critical = (CritChance(ctx.HpPercent, ctx.Level, target.Level, ctx.GearCritPercent) > Random.value * 100f);

			// crushing roll
			int crushChance = CrushChance(ctx.GearCrushPercent, ctx.SkillCrushPercent);
			bool crushing = (crushChance > Random.value * 100f);

			// damage roll
			int rolled = (int)(low + (high - low) * Random.value);
			int oldDamage = rolled + extForce;

			// defense subtraction with the low-seed guard
			int defense = ctx.IsMagic ? 0 : target.Defense;
			int net = (int)(oldDamage * (1f - lowSeed) - defense);
			int result;
			if (net > 0)
			{
				result = (int)(oldDamage - defense);
			}
			else
			{
				result = (int)(oldDamage * lowSeed * Random.value);
			}

			result = (int)(result * target.StateDamage);

			// stacked defense-rate reduction (bodyDef × itemDef / damageDecRate)
			float rate = 1f;
			if (!ctx.IsMagic)
			{
				rate = 1f - (target.BodyDefense * (float)target.ItemDefense) / damageDecRate;
			}
			if (rate > 1f) { rate = 1f; }
			if (rate < 0f) { rate = 0f; }
			result = (int)(result * rate);

			if (critical)
			{
				result = (int)(result * critDamage / 100f);
			}
			if (crushing)
			{
				result = (int)(result * crushDamage / 100f);
			}

			// damage-reduce specs (scaled by attacker level / max level)
			if (ctx.DamageReduce > 0f)
			{
				int reduce = (int)(((result * ctx.DamageReduce) * ctx.Level) / maxLevel);
				result -= reduce;
				flags |= ctx.IsMagic ? DamageFlags.MagicReduce : DamageFlags.PsyReduce;
			}

			// minimum 1, clear the procs that no longer apply
			if (result <= 1)
			{
				critical = false;
				crushing = false;
				result = 1;
			}

			// split across strike divisions
			if (ctx.StrikeCount > 1)
			{
				result /= ctx.StrikeCount;
			}
			if (result < 1)
			{
				result = 1;
			}

			// shock stagger
			bool shock = RollShock(ctx.Level, target.Level, result, critical);

			if (shock) { flags |= DamageFlags.Shock; }
			if (critical) { flags |= DamageFlags.Critical; }
			if (crushing) { flags |= DamageFlags.CrushingBlow; }
			return result;
		}

		#endregion
	}
}
