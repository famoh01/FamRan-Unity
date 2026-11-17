using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RAN.Mobile.GameModels;

namespace RANMobile.Authentication
{
	/// <summary>
	/// The Skills menu: faithful rebuild of the real KlassX Online window over the ripped
	/// UICanvasGameplay pieces (OverlayPanels/Skills Canvas → SkillsListCanvas 4-tab list +
	/// SkillSlotsCanvas 10-slot grid).
	///
	/// Tab labels/orders come from Resources/data/skillmenu.json (captured from the live
	/// game — per-class strips like STR/SUPPORT/ATTACK/ETC for shamans); rows show the full
	/// class skill set with rank/points exactly like the reference:
	///   [Name] Rank. N | (Passive)         point line = Level L | Point Needed / Point Needed / Mastered
	/// Tapping the row learns / ranks up (server validates); tapping the icon opens the
	/// description popup; dragging an icon onto a Skill Slot binds it (only learned skills
	/// can be dropped — same rule as the original).
	/// </summary>
	public partial class OriginalHudDriver
	{
		private const string SkillsRootPath = "In-Game UI/OverlayPanels/Skills Canvas";

		private bool _skillMenuReady;
		private SkillMenuClass _menuClass;
		private int _activeMenuTab;
		private readonly Transform[] _tabScrolls = new Transform[4];
		private readonly Transform[] _tabContents = new Transform[4];
		private readonly TextMeshProUGUI[] _tabLabels = new TextMeshProUGUI[4];
		// Each tab page (DEX/STR/INT/ETC) carries its OWN nested Canvas whose `enabled`
		// flag is what actually shows/hides it — the rip keeps every page active but only
		// enables the selected tab's Canvas. Switching must toggle these, not just SetActive.
		private readonly Canvas[] _tabCanvases = new Canvas[4];
		// Page roots (DEX/STR/INT/ETC). Toggling each page's GO active-state is the
		// bulletproof tab switch: a disabled nested Canvas alone proved unreliable at
		// runtime (non-selected tabs stayed visible). Names are the rip's page nodes.
		private static readonly string[] TabPageNames = { "DEX", "STR", "INT", "ETC" };
		private readonly Transform[] _tabPages = new Transform[4];
		/// <summary>The REAL visible tab buttons (Dex/Pow/Int/Etc) the click listeners attach to.</summary>
		private GameObject[] _tabButtonGos;
		private readonly List<Transform> _menuRows = new List<Transform>();
		private Transform _rowTemplate;
		private readonly Dictionary<long, WorldTcpClient.SkillInfo> _learnedIndex = new Dictionary<long, WorldTcpClient.SkillInfo>();
		private GameObject _popupGo;
		private bool _skillsOpen;

		// slot cells in the SkillSlotsCanvas grid (index 0..9) — drop targets + icons.
		private readonly List<RectTransform> _slotCells = new List<RectTransform>();

		// ---------------------------------------------------------------- wiring

		/// <summary>Discover the ripped window's tabs/labels/rows/slots and hook the open/close buttons.</summary>
		private bool EnsureSkillMenuWiring()
		{
			if (_skillMenuReady)
			{
				return true;
			}
			Transform root = transform.Find(SkillsRootPath);
			if (root == null)
			{
				UnityEngine.Debug.LogError("[SkillMenu] " + SkillsRootPath + " not found");
				return false;
			}

			// 4 tab scroll views (the ripped STR/Pow list has had several names — try candidates).
			string[][] scrollNames =
			{
				new[] { "DEX_ScrollSkills" },
				new[] { "STR_ScrollSkills", "Pow_ScrollSkills", "POW_ScrollSkills", "S_ScrollSkills" },
				new[] { "INT_ScrollSkills" },
				new[] { "ETC_ScrollSkills" },
			};
			string[] contentNames = { "D_Skills", "S_Skills", "I_Skills", "E_Skills" };
			string[] labelNames = { "DexTab_Label", "PowTab_Label", "IntTab_Label", "EtcTab_Label" };

			for (int i = 0; i < 4; i++)
			{
				for (int c = 0; c < scrollNames[i].Length; c++)
				{
					_tabScrolls[i] = Deep(root, scrollNames[i][c]);
					if (_tabScrolls[i] != null)
					{
						break;
					}
				}
				// The tab's own Canvas (on the DEX/STR/INT/ETC page, parent of the scroll) —
				// its `enabled` flag is the real visibility switch for that tab.
				_tabCanvases[i] = (_tabScrolls[i] != null)
					? _tabScrolls[i].GetComponentInParent<Canvas>(true)
					: null;
				// Page root: SetActive on it is the hard on/off (canvas toggle alone left
				// other tabs stuck on screen in practice).
				_tabPages[i] = Deep(root, TabPageNames[i]);
				_tabContents[i] = Deep(root, contentNames[i]);
				// Gutted rip leftovers on the list contents: EventTrigger implements
				// IDragHandler/IScrollHandler with no delegates, so it swallows every
				// drag/wheel before the ScrollRect above it ever sees it — that's why
				// the list could not scroll. Remove them.
				if (_tabContents[i] != null)
				{
					EventTrigger[] blockers = _tabContents[i].GetComponents<EventTrigger>();
					for (int e = 0; e < blockers.Length; e++)
					{
						DestroyImmediate(blockers[e]);
					}
				}
				Transform labelT = Deep(root, labelNames[i]);
				_tabLabels[i] = (labelT != null) ? DeepText(labelT) : null;
			}
			// Row template lives in D_Skills; every list is populated from clones of it.
			_rowTemplate = (_tabContents[0] != null) ? Deep(_tabContents[0], "Skill (1)") : null;

			// Tab buttons: the REAL visible strip is Dex/Pow/Int/Etc under
			// SkillsListCanvas/PanelBase/Buttons (the rip's own buttons — their baked
			// onClick only flips page canvases, never the scroll SetActive/heal/labels,
			// so without our listener on THEM every switched tab came up empty). The
			// DexBtn/label set from the rip is inactive in this prefab — dead wiring.
			Transform skillsList = Deep(root, "SkillsListCanvas");
			Transform buttonsNode = (skillsList != null) ? Deep(skillsList, "Buttons") : null;
			string[] tabBtnNames = new string[4] { "Dex", "Pow", "Int", "Etc" };
			_tabButtonGos = new GameObject[4];
			if (buttonsNode != null)
			{
				for (int i = 0; i < 4; i++)
				{
					Transform btn = null;
					for (int c = 0; c < buttonsNode.childCount; c++)
					{
						Transform ch = buttonsNode.GetChild(c);
						if (ch != null && ch.name == tabBtnNames[i])
						{
							btn = ch;
							break;
						}
					}
					if (btn == null)
					{
						UnityEngine.Debug.LogWarning("[SkillMenu] real tab button missing: " + tabBtnNames[i]);
						continue;
					}
					int tab = i;
					_tabButtonGos[i] = btn.gameObject;
					ClickListener(btn.gameObject).onClick.AddListener(delegate { SwitchSkillTab(tab); });
					// selection colour feedback goes on the visible TMP text
					Transform txt = Deep(btn, "Text (TMP)");
					if (txt != null)
					{
						TextMeshProUGUI tmp = txt.GetComponent<TextMeshProUGUI>();
						if (tmp != null)
						{
							_tabLabels[i] = tmp;
						}
					}
				}
			}
			else
			{
				UnityEngine.Debug.LogWarning("[SkillMenu] Buttons node not found — tab clicks unwired");
			}

			// Prev/Next paging buttons (ripped alternates between tabs with them).
			Transform prev = Deep(root, "PrevSkillTab");
			Transform next = Deep(root, "NextSkillTab");
			if (prev != null)
			{
				ClickListener(prev.gameObject).onClick.AddListener(delegate { SwitchSkillTab((_activeMenuTab + 3) % 4); });
			}
			if (next != null)
			{
				ClickListener(next.gameObject).onClick.AddListener(delegate { SwitchSkillTab((_activeMenuTab + 1) % 4); });
			}

			// Skill-slot cells: drop targets wired to WorldRuntime.SkillSets.
			Transform slotsRoot = Deep(root, "SkillSlotsCanvas");
			for (int i = 1; i <= WorldRuntime.SkillSetSize; i++)
			{
				Transform cell = (slotsRoot != null) ? Deep(slotsRoot, "Skill Slot (" + i + ")") : null;
				if (cell == null)
				{
					continue;
				}
				int slot = i - 1;
				SkillSlotDrop drop = cell.GetComponent<SkillSlotDrop>();
				if (drop == null)
				{
					drop = cell.gameObject.AddComponent<SkillSlotDrop>();
				}
				drop.slot = slot;
				// Drop targets need a raycastable graphic — guarantee an invisible
				// full-cell Image underneath if the ripped cell has none enabled.
				bool hasRaycast = false;
				foreach (Image im in cell.GetComponentsInChildren<Image>(true))
				{
					if (im.raycastTarget)
					{
						hasRaycast = true;
						break;
					}
				}
				if (!hasRaycast)
				{
					GameObject hit = new GameObject("DropHit", typeof(RectTransform));
					hit.transform.SetParent(cell, false);
					RectTransform hrt = (RectTransform)hit.transform;
					hrt.anchorMin = Vector2.zero;
					hrt.anchorMax = Vector2.one;
					hrt.offsetMin = Vector2.zero;
					hrt.offsetMax = Vector2.zero;
					hit.transform.SetSiblingIndex(0);
					Image himg = hit.AddComponent<Image>();
					himg.color = new Color(0f, 0f, 0f, 0f);
					himg.raycastTarget = true;
				}
				_slotCells.Add((RectTransform)cell);
			}

			// Skill Set (1..3) switchers: the ripped SkillTabManager carries 3 serialized buttons.
			SkillTabManager setMgr = root.GetComponentInChildren<SkillTabManager>(true);
			if (setMgr != null && setMgr.directTabButtons != null)
			{
				for (int i = 0; i < setMgr.directTabButtons.Length && i < WorldRuntime.SkillSetCount; i++)
				{
					Button b = setMgr.directTabButtons[i];
					if (b == null)
					{
						continue;
					}
					int set = i;
					b.onClick.AddListener(delegate { SwitchSkillSet(set); });
				}
			}

			// Close button inside the window.
			Transform closeBtn = Deep(root, "Close");
			if (closeBtn == null)
			{
				closeBtn = Deep(root, "CloseMenuBtn");
			}
			if (closeBtn != null)
			{
				ClickListener(closeBtn.gameObject).onClick.AddListener(CloseSkillMenu);
			}

			// The radial menu's "Skills" item opens the window.
			Transform openBtn = transform.Find("In-Game UI/_SkillButtons/_ETCButtons/Controller/Skills");
			if (openBtn != null)
			{
				ClickListener(openBtn.gameObject).onClick.AddListener(OpenSkillMenu);
			}

			_menuClass = SkillMenuData.ForClass(WorldRuntime.ClassId);

			_skillMenuReady = true;
			UnityEngine.Debug.Log("[SkillMenu] wired tabs=" + Count(_tabScrolls) + "/4 pages=" + Count(_tabPages)
				+ "/4 contents=" + Count(_tabContents) + "/4 labels=" + Count(_tabLabels) + "/4 slots=" + _slotCells.Count
				+ " template=" + (_rowTemplate != null)
				+ " menu=" + (_menuClass != null ? _menuClass.className : "NULL") + " class=" + WorldRuntime.ClassId
				+ " pages=[" + DebugNames(_tabPages) + "]");
			return true;
		}

		private static int Count(Transform[] arr)
		{
			int n = 0;
			for (int i = 0; i < arr.Length; i++)
			{
				if (arr[i] != null)
				{
					n++;
				}
			}
			return n;
		}

		private static string DebugNames(Transform[] arr)
		{
			var sb = new System.Text.StringBuilder();
			for (int i = 0; i < arr.Length; i++)
			{
				if (i > 0)
				{
					sb.Append(',');
				}
				sb.Append(arr[i] != null ? arr[i].name : "null");
			}
			return sb.ToString();
		}

		private string PagesActiveText()
		{
			var sb = new System.Text.StringBuilder();
			for (int i = 0; i < _tabPages.Length; i++)
			{
				if (i > 0)
				{
					sb.Append(',');
				}
				bool active = _tabPages[i] != null && _tabPages[i].gameObject.activeSelf;
				bool canvasOn = _tabCanvases[i] != null && _tabCanvases[i].enabled;
				sb.Append(active ? "on" : "off").Append(canvasOn ? "+C" : "-C");
			}
			return sb.ToString();
		}

		private static int Count(TextMeshProUGUI[] arr)
		{
			int n = 0;
			for (int i = 0; i < arr.Length; i++)
			{
				if (arr[i] != null)
				{
					n++;
				}
			}
			return n;
		}

		// ---------------------------------------------------------------- open/close

		private void OpenSkillMenu()
		{
			ToggleSkillMenu(true);
		}

		public void CloseSkillMenu()
		{
			ToggleSkillMenu(false);
		}

		private void ToggleSkillMenu(bool on)
		{
			_skillsOpen = on;
			SetCanvasEnabled(SkillsRootPath, on);
			// Mirror the inventory open/close pattern: HUD hides while a window is open.
			SetCanvasEnabled("In-Game UI/_SkillButtons", !on);
			SetCanvasEnabled("In-Game UI/MainView/OuterView", !on);
			if (_popupGo != null && !on)
			{
				_popupGo.SetActive(false);
			}
			if (on)
			{
				RefreshSkillMenu();
			}
		}

		private void SwitchSkillSet(int set)
		{
			if (set < 0 || set >= WorldRuntime.SkillSetCount)
			{
				return;
			}
			WorldRuntime.ActiveSkillSet = set;
			RefreshSkillMenu();
			OnSkills(); // repaint the wheel from the new set
		}

		/// <summary>Public entry for the slot drop component (same partial class, but called externally).</summary>
		public void RefreshSkillMenuPublic()
		{
			RefreshSkillMenu();
			OnSkills();
		}

		// ---------------------------------------------------------------- refresh

		/// <summary>Replaces the old learned-only DEX dump: fills all 4 tabs from skillmenu.json.</summary>
		private void RefreshSkillMenu()
		{
			if (!EnsureSkillMenuWiring())
			{
				return;
			}
			if (_menuClass == null)
			{
				_menuClass = SkillMenuData.ForClass(WorldRuntime.ClassId);
				if (_menuClass == null)
				{
					UnityEngine.Debug.LogWarning("[SkillMenu] no menu spec for class " + WorldRuntime.ClassId);
					return;
				}
			}

			// learned index (mainId<<16|subId → SkillInfo)
			_learnedIndex.Clear();
			for (int i = 0; i < WorldRuntime.Skills.Count; i++)
			{
				WorldTcpClient.SkillInfo s = WorldRuntime.Skills[i];
				_learnedIndex[SkillMenuData.Key(s.mainId, s.subId)] = s;
			}

			// Tab strip labels (per-class, e.g. shaman STR/SUPPORT/ATTACK/ETC).
			for (int i = 0; i < 4 && i < _menuClass.tabStrip.Count; i++)
			{
				if (_tabLabels[i] != null)
				{
					_tabLabels[i].text = _menuClass.tabStrip[i];
				}
			}

			// Rebuild every tab's rows. Generated rows are removed with DestroyImmediate
			// (Destroy is deferred — repeated refresh passes would accumulate clones).
			for (int t = 0; t < 4; t++)
			{
				Transform content = _tabContents[t];
				if (content == null)
				{
					continue;
				}
				for (int c = content.childCount - 1; c >= 0; c--)
				{
					Transform child = content.GetChild(c);
					if (child != null && child.name.StartsWith("SkillRow_"))
					{
						DestroyImmediate(child.gameObject);
					}
				}
			}
			_menuRows.Clear();

			int tabs = Mathf.Min(4, _menuClass.tabs.Count);
			var counts = new System.Text.StringBuilder();
			for (int t = 0; t < tabs; t++)
			{
				int before = _menuRows.Count;
				PopulateTab(t, _menuClass.tabs[t]);
				counts.Append(_menuClass.tabs[t].label).Append("=").Append(_menuRows.Count - before).Append(" ");
			}
			UnityEngine.Debug.Log("[SkillMenu] populated tabs=" + tabs + " of " + _menuClass.tabs.Count + ": " + counts.ToString());

			// Reset each content to viewport-top + rebuild layout NOW, then scroll to top.
			for (int t = 0; t < 4; t++)
			{
				Transform content = _tabContents[t];
				if (content == null)
				{
					continue;
				}
				RectTransform crt = content as RectTransform;
				if (crt != null)
				{
					crt.anchorMin = new Vector2(0f, 1f);
					crt.anchorMax = new Vector2(1f, 1f);
					crt.pivot = new Vector2(0.5f, 1f);
					crt.anchoredPosition = Vector2.zero;
					crt.localScale = Vector3.one;
					LayoutRebuilder.ForceRebuildLayoutImmediate(crt);
				}
				if (_tabScrolls[t] != null)
				{
					ScrollRect sr = _tabScrolls[t].GetComponent<ScrollRect>();
					if (sr != null)
					{
						sr.verticalNormalizedPosition = 1f;
					}
				}
			}

			// Skill Points footer (server-authoritative).
			if (_skillPoints != null)
			{
				_skillPoints.text = WorldRuntime.SkillPoints.ToString();
			}

			// Skill-slot icons reflect the ACTIVE set.
			for (int i = 0; i < _setIcons.Count; i++)
			{
				Sprite sprite = null;
				int m = WorldRuntime.SkillSlotMain(WorldRuntime.ActiveSkillSet, i);
				int s = WorldRuntime.SkillSlotSub(WorldRuntime.ActiveSkillSet, i);
				if (m >= 0)
				{
					WorldTcpClient.SkillInfo info;
					if (_learnedIndex.TryGetValue(SkillMenuData.Key(m, s), out info) && info != null)
					{
						string unused;
						WorldRuntime.FillSkillDisplay(info, out unused, out sprite);
					}
				}
				if (_setIcons[i] != null)
				{
					_setIcons[i].sprite = sprite;
					_setIcons[i].enabled = sprite != null;
				}
			}

			SwitchSkillTab(_activeMenuTab);
		}

		private void PopulateTab(int t, SkillMenuTab tab)
		{
			Transform content = _tabContents[t];
			if (content == null || _rowTemplate == null || tab == null || tab.skills == null)
			{
				return;
			}
			// Deactivate the baked placeholder rows; keep the donor for cloning.
			for (int c = 0; c < content.childCount; c++)
			{
				content.GetChild(c).gameObject.SetActive(false);
			}

			for (int i = 0; i < tab.skills.Count; i++)
			{
				SkillMenuRow row = tab.skills[i];
				GameObject go = Instantiate(_rowTemplate.gameObject, content, false);
				go.name = "SkillRow_" + row.mainId + "_" + row.subId;
				go.SetActive(true);
				// Force a predictable full-width row: the rip's template carries odd
				// anchors/positions that leave un-laid-out clones floating off-viewport.
				RectTransform rt = go.GetComponent<RectTransform>();
				if (rt != null)
				{
					rt.anchorMin = new Vector2(0f, 1f);
					rt.anchorMax = new Vector2(1f, 1f);
					rt.pivot = new Vector2(0.5f, 1f);
					rt.sizeDelta = new Vector2(0f, (_rowTemplate is RectTransform && ((RectTransform)_rowTemplate).sizeDelta.y > 10f) ? ((RectTransform)_rowTemplate).sizeDelta.y : 115f);
					rt.localScale = Vector3.one;
					// Stack explicitly: the VerticalLayoutGroup re-derives these on the
					// active tab, but contents whose scroll starts inactive never get a
					// rebuild pass — without this their rows sit unpositioned/invisible.
					rt.anchoredPosition = new Vector2(0f, -i * rt.sizeDelta.y);
				}
				LayoutElement le = go.GetComponent<LayoutElement>();
				if (le == null)
				{
					le = go.AddComponent<LayoutElement>();
				}
				le.preferredHeight = rt != null ? rt.sizeDelta.y : 115f;
				le.minHeight = le.preferredHeight;
				_menuRows.Add(go.transform);

				bool learned = _learnedIndex.ContainsKey(SkillMenuData.Key(row.mainId, row.subId));
				WorldTcpClient.SkillInfo info;
				_learnedIndex.TryGetValue(SkillMenuData.Key(row.mainId, row.subId), out info);
				SkillObject so = SkillMenuData.Get(row.mainId, row.subId);
				int level = learned ? info.skillLevel : 0;
				int maxLevel = (so != null && so.MaxLevel > 0) ? (int)so.MaxLevel : 9;

				// [Name]Rank.N (+ Passive) — exact original row format (Emulator capture:
				// "[Focus]Rank.1 Passive" / line 2 "Master" when mastered)
				bool passive = row.role == 1 || (so != null && so.SkillRole == Core.Enumerators.SkillRoleEnum.PASSIVE);
				string line1 = "[" + row.name + "]Rank." + row.grade;
				if (passive)
				{
					line1 += " Passive";
				}
				SetChildText(go.transform, "Skill_Name", line1);

				// Point line: Mastered / Level L | Point Needed / Point Needed
				SetChildText(go.transform, "Skill_PointNeeded", BuildPointLine(so, row, learned, level, maxLevel));

				// Icon
				Transform iconT = go.transform.Find("Skill_Img/Image");
				Image iconImg = (iconT != null) ? iconT.GetComponent<Image>() : null;
				if (iconImg != null && so != null)
				{
					iconImg.sprite = so.IconSource;
					iconImg.enabled = so.IconSource != null;
					iconImg.raycastTarget = true; // icon taps open the popup
				}

				// Fixed two-line row text: name on top, points below, one line each —
				// the rip's baked rects wrap long titles ("| (Passive)") into the point line.
				LayoutRowText(go.transform);

				// Row tap → learn (unlearned) / rank-up (learned). Server validates.
				int m = row.mainId;
				int s = row.subId;
				bool isLearned = learned;
				Button rowBtn = ClickListener(go);
				rowBtn.onClick.AddListener(delegate { OnMenuRowPressed(m, s, isLearned); });

				// Icon tap → description popup (child button wins inside the icon rect).
				if (iconT != null)
				{
					Image raycastImg = iconT.GetComponent<Image>();
					if (raycastImg == null)
					{
						// container without its own Image: put the click on the visible child
						iconT = go.transform.Find("Skill_Img/Image");
					}
					Button iconBtn = ClickListener(iconT.gameObject);
					iconBtn.onClick.AddListener(delegate { ShowSkillPopup(m, s); });
					// Drag-to-slot only for learned ACTIVE skills (same rule as the original).
					// An always-on IDragHandler on every icon eats list scrolling, since a drag
					// that starts on the icon never reaches the ScrollRect above it.
					bool draggable = isLearned
						&& row.role != 1
						&& (so == null || so.SkillRole != Core.Enumerators.SkillRoleEnum.PASSIVE);
					SkillRowDrag drag = iconT.GetComponent<SkillRowDrag>();
					if (draggable)
					{
						if (drag == null)
						{
							drag = iconT.gameObject.AddComponent<SkillRowDrag>();
						}
						drag.mainId = m;
						drag.subId = s;
						drag.role = row.role;
					}
					else if (drag != null)
					{
						DestroyImmediate(drag);
					}
				}
			}
		}

		/// <summary>Two-line row layout: name (top) + point line (bottom), each single-line
		/// full-width right of the icon gutter — kills the rip's wrap-into-overlap behavior.</summary>
		private static void LayoutRowText(Transform row)
		{
			ConfigureRowText(Deep(row, "Skill_Name"), 0.52f, 1f);
			ConfigureRowText(Deep(row, "Skill_PointNeeded"), 0f, 0.5f);
		}

		private static void ConfigureRowText(Transform t, float yMin, float yMax)
		{
			if (t == null)
			{
				return;
			}
			RectTransform rt = t as RectTransform;
			if (rt != null)
			{
				rt.anchorMin = new Vector2(0.17f, yMin);
				rt.anchorMax = new Vector2(0.99f, yMax);
				rt.offsetMin = Vector2.zero;
				rt.offsetMax = new Vector2(-6f, 0f);
				rt.pivot = new Vector2(0f, 0.5f);
				rt.anchoredPosition = Vector2.zero;
				rt.localScale = Vector3.one;
			}
			TextMeshProUGUI tmp = t.GetComponent<TextMeshProUGUI>();
			if (tmp != null)
			{
				tmp.enableWordWrapping = false;
				tmp.overflowMode = TextOverflowModes.Overflow;
				tmp.alignment = TextAlignmentOptions.MidlineLeft;
			}
		}

		private static string BuildPointLine(SkillObject so, SkillMenuRow row, bool learned, int level, int maxLevel)
		{
			if (learned && level >= maxLevel)
			{
				return "Master"; // original line 2 (Emulator capture)
			}
			SkillLearnLevel[] steps = (so != null && so.skillLearnSettings.levelStep != null) ? so.skillLearnSettings.levelStep : null;
			int idx = learned ? Mathf.Min(level, (steps != null ? steps.Length : 1) - 1) : 0;
			int skp = row.grade;
			if (steps != null && idx >= 0 && idx < steps.Length)
			{
				skp = (int)steps[idx].skp;
			}
			if (learned)
			{
				return "Level " + level + " | Point Needed: " + skp;
			}
			return "Point Needed: " + skp;
		}

		/// <summary>Row tap: rank-up an learned skill, learn a new one — the server gates both.</summary>
		private void OnMenuRowPressed(int mainId, int subId, bool learned)
		{
			WorldTcpClient client = WorldNet.Client;
			if (client == null)
			{
				UnityEngine.Debug.Log("[SkillMenu] offline — cannot " + (learned ? "rank up" : "learn"));
				return;
			}
			UnityEngine.Debug.Log("[SkillMenu] row tap → " + (learned ? "levelUp" : "learn") + " " + mainId + "_" + subId);
			client.SendSkillAction(learned ? "levelUp" : "learn", mainId, subId);
		}

		private void SwitchSkillTab(int t)
		{
			if (t < 0 || t > 3)
			{
				return;
			}
			_activeMenuTab = t;
			for (int i = 0; i < 4; i++)
			{
				// Hard switch on the page root: exactly one DEX/STR/INT/ETC page can be
				// active. Canvas/scroll toggles below stay in sync for the ripped layout.
				if (_tabPages[i] != null)
				{
					_tabPages[i].gameObject.SetActive(i == t);
				}
				// Enable only the selected tab page's Canvas — this is the real show/hide.
				if (_tabCanvases[i] != null)
				{
					_tabCanvases[i].enabled = (i == t);
				}
				if (_tabScrolls[i] != null)
				{
					_tabScrolls[i].gameObject.SetActive(i == t);
				}
				if (_tabLabels[i] != null)
				{
					bool sel = i == t;
					_tabLabels[i].color = sel ? new Color(1f, 0.92f, 0.45f, 1f) : new Color(1f, 1f, 1f, 0.85f);
				}
			}
			// Heal at switch-time: contents that started inside an inactive scroll never
			// got a canvas/layout pass — force them top-anchored + rebuilt, then scroll to top.
			Transform content = _tabContents[t];
			if (content != null)
			{
				content.gameObject.SetActive(true);
				RectTransform crt = content as RectTransform;
				if (crt != null)
				{
					crt.anchorMin = new Vector2(0f, 1f);
					crt.anchorMax = new Vector2(1f, 1f);
					crt.pivot = new Vector2(0.5f, 1f);
					crt.anchoredPosition = Vector2.zero;
					LayoutRebuilder.ForceRebuildLayoutImmediate(crt);
				}
				int rows = 0;
				for (int c = 0; c < content.childCount; c++)
				{
					Transform child = content.GetChild(c);
					if (child.name.StartsWith("SkillRow_") && child.gameObject.activeSelf)
					{
						rows++;
					}
				}
				string label = (_menuClass != null && t < _menuClass.tabStrip.Count) ? _menuClass.tabStrip[t] : ("tab" + t);
				UnityEngine.Debug.Log("[SkillMenu] SwitchSkillTab " + t + " (" + label + ") rows=" + rows
					+ " pagesActive=[" + PagesActiveText() + "]");
			}
			if (_tabScrolls[t] != null)
			{
				ScrollRect sr = _tabScrolls[t].GetComponent<ScrollRect>();
				if (sr != null)
				{
					RectTransform scrollRt = _tabScrolls[t] as RectTransform;
					RectTransform contentRt = (sr.content != null) ? sr.content : null;
					UnityEngine.Debug.Log("[SkillMenu] scrollRange tab=" + t
						+ " content=" + (contentRt != null ? contentRt.rect.height.ToString("F0") : "?")
						+ " viewport=" + (scrollRt != null ? scrollRt.rect.height.ToString("F0") : "?"));
					sr.verticalNormalizedPosition = 1f;
				}
			}
		}

		// ---------------------------------------------------------------- popup

		/// <summary>The description popup (icon tap): information + weapon + learn requirements.</summary>
		private void ShowSkillPopup(int mainId, int subId)
		{
			SkillObject so = SkillMenuData.Get(mainId, subId);
			if (so == null)
			{
				return;
			}
			if (_popupGo == null)
			{
				BuildSkillPopup();
			}
			if (_popupGo == null)
			{
				return;
			}
			_popupGo.SetActive(true);
			_popupGo.transform.SetAsLastSibling();
			FillSkillPopup(so, mainId, subId);
		}

		private void BuildSkillPopup()
		{
			Transform host = transform.Find(SkillsRootPath);
			if (host == null)
			{
				return;
			}
			GameObject go = new GameObject("SkillPopup", typeof(RectTransform));
			go.transform.SetParent(host, false);
			RectTransform rt = (RectTransform)go.transform;
			rt.anchorMin = new Vector2(0.5f, 0.5f);
			rt.anchorMax = new Vector2(0.5f, 0.5f);
			rt.pivot = new Vector2(0.5f, 0.5f);
			rt.sizeDelta = new Vector2(780f, 880f);
			rt.anchoredPosition = Vector2.zero;
			Image bg = go.AddComponent<Image>();
			bg.color = new Color(0.03f, 0.05f, 0.09f, 0.96f);
			bg.raycastTarget = true;
			BoxOutline(rt, new Color(0.55f, 0.62f, 0.75f, 0.9f));
			_popupGo = go;
			go.SetActive(false);
		}

		private void FillSkillPopup(SkillObject so, int mainId, int subId)
		{
			// clear old body (keep the panel + its Close button)
			for (int i = _popupGo.transform.childCount - 1; i >= 0; i--)
			{
				Transform c = _popupGo.transform.GetChild(i);
				if (c.name != "PopupClose")
				{
					Destroy(c.gameObject);
				}
			}

			Transform p = _popupGo.transform;
			float y = -26f;
			// Title (green, bracketed — matches the reference popup)
			TextMeshProUGUI title = MakeText(p, "PopupTitle", 34f, TextAlignmentOptions.Center, new Color(0.45f, 1f, 0.55f));
			PlaceTitle(title.rectTransform, 760f, ref y, 44f);
			title.text = so.SkillName;

			// ---- Skill Information ----
			TextMeshProUGUI hdr1 = MakeText(p, "PopupH1", 26f, TextAlignmentOptions.Left, new Color(0.45f, 0.7f, 1f));
			PlaceBlock(hdr1.rectTransform, 720f, ref y, 34f);
			hdr1.text = "Skill Information";

			WorldTcpClient.SkillInfo learned;
			_learnedIndex.TryGetValue(SkillMenuData.Key(mainId, subId), out learned);

			// original card fields (Emulator capture of the real client)
			bool passiveInfo = so.SkillRole == Core.Enumerators.SkillRoleEnum.PASSIVE;
			PlaceInfoLine(p, "Name:" + so.SkillName + (passiveInfo ? "(Passive)" : ""), ref y, MetColor);
			PlaceInfoLine(p, "Characteristic (" + ElementName(so) + ")", ref y, MetColor);
			PlaceInfoLine(p, "Class: " + ClassText(so), ref y, MetColor); // keeps its space (reference popup)
			string act = SkillActionText(so);
			PlaceInfoLine(p, act.Length > 0 ? act : ("Action Type:" + (passiveInfo ? "Passive" : "Normal")), ref y, MetColor);
			PlaceInfoLine(p, "Target is Beyond Range Limit:" + so.TargetRange, ref y, Color.white);

			// ---- Required Weapon ----
			TextMeshProUGUI hdr2 = MakeText(p, "PopupH2", 26f, TextAlignmentOptions.Left, new Color(0.45f, 0.7f, 1f));
			PlaceBlock(hdr2.rectTransform, 720f, ref y, 34f);
			hdr2.text = "Required Weapon";
			int right = (int)so.UseRightItem;
			int left = (int)so.UseLeftItem;
			if (IsMeaningfulHand(right))
			{
				PlaceInfoLine(p, "Use Principal's tool:" + WeaponName(right), ref y, Color.white);
			}
			if (IsMeaningfulHand(left))
			{
				PlaceInfoLine(p, "Left Hand:" + WeaponName(left), ref y, Color.white);
			}
			PlaceInfoLine(p, "Attribute:" + AttributeText(so), ref y, MetColor);
			// per-level apply data — the original card's quantity/pierce/target lines
			var al = so.skillApplySettings.dataLevel;
			if (al != null && al.Length > 0)
			{
				int dlv = Mathf.Clamp((learned != null ? learned.skillLevel : 0), 0, al.Length - 1);
				var dl = al[dlv];
				PlaceInfoLine(p, "Applicable Quantity:" + dl.applyNum, ref y, MetColor);
				PlaceInfoLine(p, "Penetrate Level:" + dl.pierceNum, ref y, MetColor);
				PlaceInfoLine(p, "Target Quantities:" + dl.tarNum, ref y, MetColor);
			}

			// ---- Learn Requirements (red = unmet, green = met) ----
			TextMeshProUGUI hdr3 = MakeText(p, "PopupH3", 26f, TextAlignmentOptions.Left, new Color(0.45f, 0.7f, 1f));
			PlaceBlock(hdr3.rectTransform, 720f, ref y, 34f);
			hdr3.text = "Learn Requirements";

			SkillLearnLevel[] steps = so.skillLearnSettings.levelStep;
			bool isLearned = learned != null;
			bool mastered = isLearned && learned.skillLevel >= ((so.MaxLevel > 0) ? (int)so.MaxLevel : 9);
			if (mastered)
			{
				PlaceInfoLine(p, "Mastered", ref y, new Color(0.45f, 1f, 0.55f));
			}
			else
			{
				int idx = isLearned ? Mathf.Min(learned.skillLevel, (steps != null ? steps.Length : 1) - 1) : 0;
				SkillLearnLevel step = (steps != null && idx < steps.Length) ? steps[idx] : default(SkillLearnLevel);

				// prerequisite skill + its level
				Models.Library.Shared.Game.SNativeID pre = so.skillLearnSettings.skill;
				if (pre.mainId != 65535 || pre.subId != 65535)
				{
					WorldTcpClient.SkillInfo preInfo;
					bool have = _learnedIndex.TryGetValue(SkillMenuData.Key(pre.mainId, pre.subId), out preInfo);
					string preName = PreName(pre.mainId, pre.subId);
					bool lvlOk = have && preInfo.skillLevel >= (int)step.skillLevel;
					PlaceInfoLine(p, "Required Skill:" + preName, ref y, lvlOk ? MetColor : UnmetColor);
					PlaceInfoLine(p, "Required Skill Level:" + step.skillLevel, ref y, lvlOk ? MetColor : UnmetColor);
				}
				PlaceInfoLine(p, "Required Skill Points:" + step.skp, ref y,
					WorldRuntime.SkillPoints >= (int)step.skp ? MetColor : UnmetColor);
				PlaceInfoLine(p, "Required Level:" + step.level, ref y,
					WorldRuntime.Level >= (int)step.level ? MetColor : UnmetColor);
				// stat requirements (only the non-zero ones, like the reference)
				PlaceStat(p, "Pow", (int)step.stats.Pow, WorldRuntime.Pow, ref y);
				PlaceStat(p, "Str", (int)step.stats.Str, WorldRuntime.Str, ref y);
				PlaceStat(p, "Spi", (int)step.stats.Spi, WorldRuntime.Spi, ref y);
				PlaceStat(p, "Dex", (int)step.stats.Dex, WorldRuntime.Dex, ref y);
				PlaceStat(p, "Int", (int)step.stats.Int, WorldRuntime.Int, ref y);
				PlaceStat(p, "Sta", (int)step.stats.Sta, WorldRuntime.Sta, ref y);
			}

			// ---- Description (original card puts it last, below everything) ----
			TextMeshProUGUI hdrDesc = MakeText(p, "PopupHDesc", 26f, TextAlignmentOptions.Left, new Color(0.45f, 0.7f, 1f));
			PlaceBlock(hdrDesc.rectTransform, 720f, ref y, 34f);
			hdrDesc.text = "Description";
			TextMeshProUGUI desc = MakeText(p, "PopupDesc", 22f, TextAlignmentOptions.TopLeft, new Color(1f, 1f, 1f, 0.92f));
			PlaceBlock(desc.rectTransform, 720f, ref y, 84f);
			desc.text = so.Description ?? string.Empty;
			desc.enableWordWrapping = true;

			// ---- bottom action row: Learn (unlearned) / Upgrade (learned, not mastered),
			// like the original window's per-row up-button — plus Close on the right.
			bool canAct = !mastered;
			if (canAct)
			{
				GameObject actionGo = new GameObject(isLearned ? "PopupUpgrade" : "PopupLearn", typeof(RectTransform));
				actionGo.transform.SetParent(p, false);
				PlacePopupButton((RectTransform)actionGo.transform, new Vector2(-160f, 18f), new Color(0.13f, 0.32f, 0.2f, 1f),
					isLearned ? "Upgrade" : "Learn");
				int aMain = mainId;
				int aSub = subId;
				bool aLearned = isLearned;
				ClickListener(actionGo).onClick.AddListener(delegate
				{
					WorldTcpClient client = WorldNet.Client;
					if (client != null)
					{
						client.SendSkillAction(aLearned ? "levelUp" : "learn", aMain, aSub);
					}
					_popupGo.SetActive(false);
				});
			}

			// Close button (bottom, like the reference)
			GameObject closeGo = new GameObject("PopupClose", typeof(RectTransform));
			closeGo.transform.SetParent(p, false);
			PlacePopupButton((RectTransform)closeGo.transform, new Vector2(canAct ? 160f : 0f, 18f),
				new Color(0.16f, 0.2f, 0.28f, 1f), "Close");
			ClickListener(closeGo).onClick.AddListener(delegate { _popupGo.SetActive(false); });
		}

		/// <summary>Shared popup button placement: 260x58 at the given bottom offset.</summary>
		private static void PlacePopupButton(RectTransform crt, Vector2 pos, Color color, string label)
		{
			crt.anchorMin = new Vector2(0.5f, 0f);
			crt.anchorMax = new Vector2(0.5f, 0f);
			crt.pivot = new Vector2(0.5f, 0f);
			crt.sizeDelta = new Vector2(260f, 58f);
			crt.anchoredPosition = pos;
			Image img = crt.gameObject.AddComponent<Image>();
			img.color = color;
			TextMeshProUGUI lt = MakeText(crt, "Label", 30f, TextAlignmentOptions.Center, Color.white);
			((RectTransform)lt.transform).anchorMin = Vector2.zero;
			((RectTransform)lt.transform).anchorMax = Vector2.one;
			((RectTransform)lt.transform).offsetMin = Vector2.zero;
			((RectTransform)lt.transform).offsetMax = Vector2.zero;
			lt.text = label;
		}

		private static readonly Color MetColor = new Color(0.45f, 1f, 0.55f, 1f);
		private static readonly Color UnmetColor = new Color(1f, 0.42f, 0.42f, 1f);

		private void PlaceStat(Transform p, string key, int need, int have, ref float y)
		{
			if (need <= 0)
			{
				return;
			}
			PlaceInfoLine(p, "Required " + key + ":" + need, ref y, have >= need ? MetColor : UnmetColor);
		}

		private static string PreName(int mainId, int subId)
		{
			SkillObject so = SkillMenuData.Get(mainId, subId);
			return so != null ? so.SkillName : ("skill " + mainId + "-" + subId);
		}

		private void PlaceInfoLine(Transform p, string text, ref float y, Color color)
		{
			TextMeshProUGUI tmp = MakeText(p, "PopupLine", 24f, TextAlignmentOptions.Left, color);
			PlaceBlock(tmp.rectTransform, 720f, ref y, 32f);
			tmp.text = text;
		}

		private void PlaceTitle(RectTransform rt, float width, ref float y, float height)
		{
			rt.anchorMin = new Vector2(0.5f, 1f);
			rt.anchorMax = new Vector2(0.5f, 1f);
			rt.pivot = new Vector2(0.5f, 1f);
			rt.sizeDelta = new Vector2(width, height);
			rt.anchoredPosition = new Vector2(0f, y);
			y -= height + 6f;
		}

		private void PlaceBlock(RectTransform rt, float width, ref float y, float height)
		{
			rt.anchorMin = new Vector2(0.5f, 1f);
			rt.anchorMax = new Vector2(0.5f, 1f);
			rt.pivot = new Vector2(0.5f, 1f);
			rt.sizeDelta = new Vector2(width, height);
			rt.anchoredPosition = new Vector2(-10f, y);
			y -= height + 4f;
		}

		private static string AttributeText(SkillObject so)
		{
			// COMMENT::BRIGHT display: the reference popup shows "Attribute:Both(Neutral)" for
			// BRIGHT_BOTH, so map the BrightEnum *name* to the comment-table string (its name
			// LightGood/DarkEvil/BothNeutral is not the display text).
			string enumName = System.Enum.GetName(typeof(Core.Enumerators.BrightEnum), so.skillLearnSettings.bright) ?? "BothNeutral";
			string bright;
			switch (enumName)
			{
				case "LightGood": bright = "Light"; break;
				case "DarkEvil": bright = "Dark"; break;
				case "BothNeutral": bright = "Both"; break;
				default: bright = enumName; break;
			}
			int elem = (int)so.skillApplySettings.element;
			string elemName;
			if (elem == 9)
			{
				elemName = "Neutral"; // EMELEMENT_ARM defers to the weapon element
			}
			else
			{
				elemName = System.Enum.GetName(typeof(Core.Enumerators.ElementEnum), so.skillApplySettings.element) ?? elem.ToString();
				elemName = elemName.Replace("EMELEMENT_", "");
				if (elemName.Length > 0)
				{
					elemName = char.ToUpperInvariant(elemName[0]) + elemName.Substring(1).ToLowerInvariant();
				}
			}
			return bright + "(" + elemName + ")";
		}

		private static string ClassText(SkillObject so)
		{
			int mask = (int)so.skillLearnSettings.@class;
			string name;
			switch (mask)
			{
				case 65: name = "Swordsman"; break;
				case 130: name = "Brawler"; break;
				case 260: name = "Archer"; break;
				case 520: name = "Shaman"; break;
				default: return "mask " + mask;
			}
			return name + " " + name; // original: "Class: Archer Archer"
		}

		/// <summary>Element display for the original's "Characteristic (Neutral)" line.</summary>
		private static string ElementName(SkillObject so)
		{
			int elem = (int)so.skillApplySettings.element;
			if (elem == 9)
			{
				return "Neutral"; // EMELEMENT_ARM defers to the weapon element
			}
			string elemName = System.Enum.GetName(typeof(Core.Enumerators.ElementEnum), so.skillApplySettings.element) ?? elem.ToString();
			elemName = elemName.Replace("EMELEMENT_", "");
			if (elemName.Length > 0)
			{
				elemName = char.ToUpperInvariant(elemName[0]) + elemName.Substring(1).ToLowerInvariant();
			}
			return elemName;
		}

		/// <summary>The original card's bare action line, e.g. "Proficient Skill"
		/// (from the SkillAction enum); empty for NONE/NOTHING → caller falls back.</summary>
		private static string SkillActionText(SkillObject so)
		{
			string act = so.SkillAction.ToString();
			if (act == "0" || act.Equals("NONE", System.StringComparison.OrdinalIgnoreCase) ||
				act.Equals("NOTHING", System.StringComparison.OrdinalIgnoreCase))
			{
				return "";
			}
			string[] parts = act.Split('_');
			for (int i = 0; i < parts.Length; i++)
			{
				if (parts[i].Length > 0)
				{
					parts[i] = char.ToUpperInvariant(parts[i][0]) + parts[i].Substring(1).ToLowerInvariant();
				}
			}
			string joined = string.Join(" ", parts);
			return joined.EndsWith("Skill") ? joined : joined + " Skill";
		}

		private static bool IsMeaningfulHand(int v)
		{
			return v != 0 && v != 18; // 0/18 = nothing required
		}

		private static string WeaponName(int v)
		{
			if (v == 3)
			{
				return "Sword/Blade"; // verified in the reference popup (Shogun Swing)
			}
			string n = System.Enum.GetName(typeof(Core.Enumerators.SkillAttackTypeEnum), (Core.Enumerators.SkillAttackTypeEnum)v);
			if (string.IsNullOrEmpty(n))
			{
				return "#" + v;
			}
			n = n.Replace("SKILLATT_", "").Replace("ITEMATT_", "").Replace("_", " ");
			return char.ToUpperInvariant(n[0]) + n.Substring(1).ToLowerInvariant();
		}

		// ---------------------------------------------------------------- layout util

		private static void BoxOutline(RectTransform rt, Color color)
		{
			GameObject o = new GameObject("Outline", typeof(RectTransform));
			o.transform.SetParent(rt, false);
			RectTransform ort = (RectTransform)o.transform;
			ort.anchorMin = Vector2.zero;
			ort.anchorMax = Vector2.one;
			ort.offsetMin = new Vector2(2f, 2f);
			ort.offsetMax = new Vector2(-2f, -2f);
			Image img = o.AddComponent<Image>();
			img.color = color;
			img.raycastTarget = false;
			img.transform.SetSiblingIndex(0);
		}
	}

	// ---------------------------------------------------------------- components

	/// <summary>Drag source on a row's icon: only learned skills may land in a slot.</summary>
	public class SkillRowDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public int mainId;
		public int subId;
		public int role; // 1 = passive (skillmenu row role)

		private GameObject _ghost;

		public void OnBeginDrag(PointerEventData eventData)
		{
			SkillObject so = SkillMenuData.Get(mainId, subId);
			if (so == null)
			{
				UnityEngine.Debug.Log("[SkillMenu] drag refused: no SkillObject " + mainId + "_" + subId);
				return;
			}
			if (!IsLearned())
			{
				UnityEngine.Debug.Log("[SkillMenu] drag refused (not learned): " + so.SkillName);
				return; // unlearned skills can't be dragged (original behavior)
			}
			if (role == 1 || so.SkillRole == Core.Enumerators.SkillRoleEnum.PASSIVE)
			{
				// Passive skills are always-on, never assigned to a skill slot — the original
				// bails out the same way (CSkillSlot::TranslateUIMessage returns on EMROLE_PASSIVE).
				UnityEngine.Debug.Log("[SkillMenu] drag refused (passive): " + so.SkillName);
				return;
			}
			UnityEngine.Debug.Log("[SkillMenu] drag begin: " + so.SkillName);
			_ghost = new GameObject("SkillDragGhost", typeof(RectTransform));
			RectTransform grt = (RectTransform)_ghost.transform;
			Canvas canvas = GetComponentInParent<Canvas>();
			Transform parent = (canvas != null) ? canvas.transform : null;
			_ghost.transform.SetParent(parent, false);
			grt.sizeDelta = new Vector2(72f, 72f);
			Image img = _ghost.AddComponent<Image>();
			img.sprite = so.IconSource;
			img.raycastTarget = false;
			img.color = new Color(1f, 1f, 1f, 0.85f);
			_ghost.transform.SetAsLastSibling();
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (_ghost == null)
			{
				return;
			}
			Canvas canvas = GetComponentInParent<Canvas>();
			if (canvas == null)
			{
				return;
			}
			Vector2 pos;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
				(RectTransform)canvas.transform, eventData.position, canvas.worldCamera, out pos))
			{
				((RectTransform)_ghost.transform).anchoredPosition = pos;
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (_ghost != null)
			{
				Destroy(_ghost);
				_ghost = null;
			}
		}

		private bool IsLearned()
		{
			for (int i = 0; i < WorldRuntime.Skills.Count; i++)
			{
				WorldTcpClient.SkillInfo s = WorldRuntime.Skills[i];
				if (s.mainId == mainId && s.subId == subId)
				{
					return true;
				}
			}
			return false;
		}
	}

	/// <summary>Drop target on a "Skill Slot (N)" cell.</summary>
	public class SkillSlotDrop : MonoBehaviour, IDropHandler
	{
		public int slot;

		public void OnDrop(PointerEventData eventData)
		{
			GameObject dragGo = (eventData != null) ? eventData.pointerDrag : null;
			SkillRowDrag src = (dragGo != null) ? dragGo.GetComponent<SkillRowDrag>() : null;
			if (src == null)
			{
				UnityEngine.Debug.Log("[SkillMenu] OnDrop slot " + (slot + 1) + ": no SkillRowDrag on pointerDrag"
					+ (dragGo != null ? " (it's " + dragGo.name + ")" : " (null)"));
				return;
			}
			bool ok = WorldRuntime.SkillSlotSet(WorldRuntime.ActiveSkillSet, slot, src.mainId, src.subId);
			UnityEngine.Debug.Log("[SkillMenu] OnDrop slot " + (slot + 1) + ": " + src.mainId + "_" + src.subId
				+ (ok ? " BOUND" : " rejected (already bound)"));
			if (ok && OriginalHudDriver.Instance != null)
			{
				OriginalHudDriver.Instance.RefreshSkillMenuPublic();
			}
		}
	}
}
