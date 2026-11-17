using System.Collections.Generic;
using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>One skill row of the real game's Skills menu (order = observed display order).</summary>
	[System.Serializable]
	public class SkillMenuRow
	{
		public string name;
		public int grade;
		public int mainId;
		public int subId;
		public int role; // 1 = Passive
	}

	/// <summary>One tab: display label + the single mainId grid it shows + ordered rows.</summary>
	[System.Serializable]
	public class SkillMenuTab
	{
		public string label;
		public List<int> mainIds = new List<int>();
		public List<SkillMenuRow> skills = new List<SkillMenuRow>();
	}

	/// <summary>Per-class menu spec (tab strip + tabs) captured from the real KlassX Online.</summary>
	[System.Serializable]
	public class SkillMenuClass
	{
		public string className;
		public int emClass;
		public int classMask;
		public List<string> tabStrip = new List<string>();
		public List<SkillMenuTab> tabs = new List<SkillMenuTab>();
		public int skillCount;
	}

	// ---- flat wire format (Resources/data/skillmenu.json) ----
	// Single-level List<POCO> only: JsonUtility reliably handles this shape (it parsed
	// the classes list on day one), whereas the earlier nested tabs[].skills[] form
	// came back empty for tabs 2-4 at runtime. Nested structures are rebuilt in code.

	[System.Serializable]
	internal class SkillMenuFlatClass
	{
		public string @class;
		public int emClass;
		public int classMask;
		public List<string> tabStrip = new List<string>();
	}

	[System.Serializable]
	internal class SkillMenuFlatRow
	{
		public int c; // class index
		public int t; // tab index within that class's strip
		public string name;
		public int grade;
		public int mainId;
		public int subId;
		public int role;
	}

	[System.Serializable]
	internal class SkillMenuFlatFile
	{
		public List<SkillMenuFlatClass> classes;
		public List<SkillMenuFlatRow> rows;
	}

	/// <summary>
	/// Loads Resources/data/skillmenu.json once (the faithful tab structure captured from
	/// the live game: per-class tab labels + exact row orders) and indexes the client's
	/// SkillDatabase for lookups by (mainId, subId).
	///
	/// NOTE: parsed with Unity's JsonUtility — the project's Newtonsoft.Json is a gutted
	/// decompile (JObject.Parse returns null), so it must never be used at runtime here.
	/// </summary>
	public static class SkillMenuData
	{
		private static SkillMenuClass[] _classes;
		private static Dictionary<long, SkillObject> _objects;

		public static SkillMenuClass[] Classes
		{
			get
			{
				if (_classes == null)
				{
					Load();
				}
				return _classes;
			}
		}

		/// <summary>Default tab strips per class (insurance if tabStrip fails to parse).</summary>
		private static List<string> DefaultStrip(int emClass)
		{
			var strip = new List<string>();
			if (emClass == 8)
			{
				strip.AddRange(new[] { "STR", "SUPPORT", "ATTACK", "ETC" });
			}
			else
			{
				strip.AddRange(new[] { "DEX", "STR", "INT", "ETC" });
			}
			return strip;
		}

		/// <summary>Parse + rebuild the nested menu structures. Returns the per-class
		/// per-tab row counts string (also logged) — used by runtime and the editor self-test.</summary>
		internal static SkillMenuClass[] ParseAndBuild(string text, out string summary)
		{
			var classes = new List<SkillMenuClass>();
			var sb = new System.Text.StringBuilder();
			summary = string.Empty;
			if (string.IsNullOrEmpty(text))
			{
				summary = "EMPTY TEXT";
				return classes.ToArray();
			}
			SkillMenuFlatFile file = JsonUtility.FromJson<SkillMenuFlatFile>(text);
			if (file == null || file.classes == null || file.classes.Count == 0)
			{
				summary = "PARSE RETURNED NOTHING (file=" + (file == null ? "null" : "ok") + ")";
				return classes.ToArray();
			}
			for (int ci = 0; ci < file.classes.Count; ci++)
			{
				SkillMenuFlatClass fc = file.classes[ci];
				var sc = new SkillMenuClass
				{
					className = fc.@class,
					emClass = fc.emClass,
					classMask = fc.classMask,
					tabStrip = (fc.tabStrip != null && fc.tabStrip.Count >= 4) ? fc.tabStrip : DefaultStrip(fc.emClass),
				};
				for (int t = 0; t < sc.tabStrip.Count; t++)
				{
					sc.tabs.Add(new SkillMenuTab { label = sc.tabStrip[t] });
				}
				int used = 0;
				if (file.rows != null)
				{
					for (int r = 0; r < file.rows.Count; r++)
					{
						SkillMenuFlatRow row = file.rows[r];
						if (row.c != ci || row.t < 0 || row.t >= sc.tabs.Count)
						{
							continue;
						}
						SkillMenuTab tab = sc.tabs[row.t];
						tab.skills.Add(new SkillMenuRow
						{
							name = row.name,
							grade = row.grade,
							mainId = row.mainId,
							subId = row.subId,
							role = row.role,
						});
						if (!tab.mainIds.Contains(row.mainId))
						{
							tab.mainIds.Add(row.mainId);
						}
						used++;
					}
				}
				for (int t = 0; t < sc.tabs.Count; t++)
				{
					sc.tabs[t].mainIds.Sort();
				}
				sc.skillCount = used;
				classes.Add(sc);
				sb.Append(sc.className.Length > 0 ? sc.className : ("class" + sc.emClass)).Append(":");
				for (int t = 0; t < sc.tabs.Count; t++)
				{
					sb.Append(" ").Append(sc.tabs[t].label).Append("=").Append(sc.tabs[t].skills.Count);
				}
				sb.Append(" |");
			}
			summary = sb.ToString();
			return classes.ToArray();
		}

		private static void Load()
		{
			TextAsset ta = Resources.Load<TextAsset>("data/skillmenu");
			if (ta == null)
			{
				UnityEngine.Debug.LogError("[SkillMenuData] Resources/data/skillmenu missing");
				_classes = new SkillMenuClass[0];
				return;
			}
			try
			{
				string summary;
				_classes = ParseAndBuild(ta.text, out summary);
				UnityEngine.Debug.Log("[SkillMenuData] loaded " + _classes.Length + " classes (" + ta.text.Length + " chars): " + summary);
				if (_classes.Length == 0)
				{
					UnityEngine.Debug.LogError("[SkillMenuData] NO CLASSES: " + summary);
				}
			}
			catch (System.Exception e)
			{
				UnityEngine.Debug.LogError("[SkillMenuData] parse failed: " + e);
				_classes = new SkillMenuClass[0];
			}
		}

#if UNITY_EDITOR
		/// <summary>
		/// Editor-only self test: parses the JSON straight from disk on every compile so
		/// Editor.log always shows the per-tab counts — no play session needed to verify.
		/// </summary>
		[UnityEditor.InitializeOnLoadMethod]
		private static void EditorSelfTest()
		{
			try
			{
				string path = System.IO.Path.Combine(Application.dataPath, "Resources/data/skillmenu.json");
				if (!System.IO.File.Exists(path))
				{
					return;
				}
				string summary;
				SkillMenuClass[] probe = ParseAndBuild(System.IO.File.ReadAllText(path), out summary);
				UnityEngine.Debug.Log("[SkillMenuData#selftest] " + probe.Length + " classes: " + summary);
			}
			catch (System.Exception e)
			{
				UnityEngine.Debug.LogError("[SkillMenuData#selftest] " + e.Message);
			}
		}
#endif

		/// <summary>Menu spec for the local character's emClass (1/2/4/8), or null.</summary>
		public static SkillMenuClass ForClass(int emClass)
		{
			SkillMenuClass[] all = Classes;
			for (int i = 0; i < all.Length; i++)
			{
				if (all[i] != null && all[i].emClass == emClass)
				{
					return all[i];
				}
			}
			return null;
		}

		/// <summary>All SkillObjects indexed by (mainId &lt;&lt; 16) | subId, loaded from the database asset.</summary>
		public static Dictionary<long, SkillObject> Objects
		{
			get
			{
				if (_objects == null)
				{
					_objects = new Dictionary<long, SkillObject>();
					SkillDatabaseObject db = Resources.Load<SkillDatabaseObject>("database/SkillDatabase");
					if (db != null && db.skillObjectList != null)
					{
						for (int i = 0; i < db.skillObjectList.Length; i++)
						{
							SkillObject so = db.skillObjectList[i];
							if (so == null)
							{
								continue;
							}
							_objects[Key(so.Data.mainId, so.Data.subId)] = so;
						}
					}
					UnityEngine.Debug.Log("[SkillMenuData] indexed " + _objects.Count + " skill objects");
				}
				return _objects;
			}
		}

		public static long Key(int mainId, int subId)
		{
			return ((long)mainId << 16) | (uint)subId;
		}

		public static SkillObject Get(int mainId, int subId)
		{
			SkillObject so;
			return Objects.TryGetValue(Key(mainId, subId), out so) ? so : null;
		}
	}
}
