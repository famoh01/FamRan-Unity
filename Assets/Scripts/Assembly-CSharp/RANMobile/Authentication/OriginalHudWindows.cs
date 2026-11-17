using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Second half of OriginalHudDriver. Trims the ripped HUD to the reference
	/// layout, fans the radial menu out, and fills the Inventory and Skills
	/// windows (OverlayPanels: Invent Equip Canvas, Skills Canvas) from
	/// WorldRuntime. Those windows are closed in the prefab; the prefab's own
	/// buttons open them, so nothing here opens or closes them.
	/// </summary>
	public partial class OriginalHudDriver
	{
		private const int InventorySlotCount = 60;

		private const int InventoryColumns = 6;

		private const int SkillSetSlotCount = 10;

		private const int SkillSetBindCount = 6;

		private readonly List<Image> _invIcons = new List<Image>();
		// Per-slot item (mainId/subId; -1 = empty) so tapping an inventory slot can equip it.
		private readonly List<int> _invMain = new List<int>();
		private readonly List<int> _invSub = new List<int>();

		private readonly List<TextMeshProUGUI> _invNames = new List<TextMeshProUGUI>();

		private readonly List<TextMeshProUGUI> _invCounts = new List<TextMeshProUGUI>();

		private readonly List<Image> _setIcons = new List<Image>();

		private readonly List<Transform> _skillRows = new List<Transform>();

		private static readonly Dictionary<long, Sprite> _itemIconCache = new Dictionary<long, Sprite>();

		private static bool _itemIconsLoaded;

		private static readonly Dictionary<long, string> _itemWearCache = new Dictionary<long, string>();

		private TextMeshProUGUI _invGold;

		private TextMeshProUGUI _skillPoints;

		private Transform _lootBtn;

		private float _nextLootCheck;

		private float _nextDateAt;

		// ---- Character (Player Stats Canvas) window ----
		private const string StatsCanvasPath = "In-Game UI/OverlayPanels/Player Stats Canvas";

		private const string StatsRootPath = StatsCanvasPath + "/Canvas Skills & Stats/PanelBase/StatsMask/PLAYER STATS";

		private bool _charWired;

		private TextMeshProUGUI _charName;

		private UnityEngine.UI.Image _charExpFill;

		private TextMeshProUGUI _charExpLabel;

		private readonly Dictionary<WorldRuntime.StatKind, TextMeshProUGUI> _statValues = new Dictionary<WorldRuntime.StatKind, TextMeshProUGUI>();

		private TextMeshProUGUI _charPoints;

		private readonly Dictionary<string, TextMeshProUGUI> _condValues = new Dictionary<string, TextMeshProUGUI>();

		// ---- Equipment (Equipment Canvas) paper-doll ----
		private const string EquipCanvasPath = "In-Game UI/OverlayPanels/Invent Equip Canvas/Equipment Canvas";

		private const string EquipGridPath = EquipCanvasPath + "/EquipmentScreen/Background/GridContainer";

		private readonly Dictionary<string, List<UnityEngine.UI.Image>> _equipSlots = new Dictionary<string, List<UnityEngine.UI.Image>>();

		// Exact WearPositionEnum member -> equip_* slot key. Multiple enum members can map to the
		// same slot (both fingers -> ring); the grid may hold several buttons per key (two rings).
		private static readonly Dictionary<string, string> _wearToSlot = new Dictionary<string, string>
		{
			{ "HeadGear",  "equip_head" },
			{ "Necklace",  "equip_necklace" },
			{ "UpperBody", "equip_upper" },
			{ "LowerBody", "equip_lower" },
			{ "Hand",      "equip_gloves" },
			{ "Foot",      "equip_feet" },
			{ "Handheld",  "equip_weapon" },
			{ "Ring",      "equip_ring" },
			{ "Bracelet",  "equip_bracelet" },
			{ "Vehicle",   "equip_mount" },
			{ "Belt",      "equip_belt" },
			{ "Earring",   "equip_ring" },
		};

		private bool _equipWired;
		// First equipped item shown per equip_* slot, so tapping the slot can unequip it.
		private readonly Dictionary<string, WorldTcpClient.InventoryItemInfo> _equipSlotItem = new Dictionary<string, WorldTcpClient.InventoryItemInfo>();

		// Called from Spawn() with the OverlayPanels transform. The panel stays active:
		// its windows are closed by default (Canvas disabled in the prefab) and the
		// prefab's own buttons open them. Only popups that would be drawn at all times are hidden.
		private static void PrepareOverlay(Transform overlayPanels)
		{
			string[] alwaysHidden = { "CustomUIIndicator", "ModeSelectionSystems", "TyrannyWarCanvas", "UIItemInfo (1)", "UIModalBox", "UINPCItemInfo", "TargetMode" };
			for (int i = 0; i < overlayPanels.childCount; i++)
			{
				Transform c = overlayPanels.GetChild(i);
				if (System.Array.IndexOf(alwaysHidden, c.name) >= 0)
				{
					c.gameObject.SetActive(false);
				}
				else if (c.name == "KillStreakCanvas")
				{
					Canvas canvas = c.GetComponent<Canvas>();
					if (canvas != null)
					{
						canvas.enabled = false;
					}
				}
			}
		}

		// Ripped HUD pieces that have no data behind them in this client.
		private void HideHudExtras()
		{
			string[] paths =
			{
				"In-Game UI/_SkillButtons/DuelTextUI",
				"In-Game UI/_SkillButtons/_ETCButtons/Text (TMP)",
				"In-Game UI/_SkillButtons/NewMenuButtons/Text (TMP)",
				"In-Game UI/_SkillButtons/SkillBackground/ForcePK",
				"In-Game UI/_SkillButtons/SkillBackground/FocusAtkTowerBtn",
				"In-Game UI/_SkillButtons/Talk2NPC",
				"In-Game UI/_SkillButtons/_ETCButtons/Controller/BtnMainMenuTrigger"
			};
			for (int i = 0; i < paths.Length; i++)
			{
				Transform t = transform.Find(paths[i]);
				if (t != null)
				{
					t.gameObject.SetActive(false);
				}
			}
		}

		// A disabled Canvas does not hide its Graphics in this prefab (they keep rendering through the
		// enabled parent canvas), so each window root GameObject is toggled to follow the Canvas.enabled
		// flag that the prefab's own buttons already set. Closed windows start with their panels turned off.
		private readonly List<GameObject> _gateGos = new List<GameObject>();

		private readonly List<Canvas> _gateCanvases = new List<Canvas>();

		private void GateOverlayWindows()
		{
			Transform overlay = transform.Find("In-Game UI/OverlayPanels");
			if (overlay == null)
			{
				return;
			}
			for (int i = 0; i < overlay.childCount; i++)
			{
				GateWindow(overlay.GetChild(i));
			}
			Transform invent = overlay.Find("Invent Equip Canvas");
			if (invent != null)
			{
				for (int i = 0; i < invent.childCount; i++)
				{
					GateWindow(invent.GetChild(i));
				}
			}
		}

		private void GateWindow(Transform t)
		{
			Canvas cv = t.GetComponent<Canvas>();
			if (cv == null)
			{
				return;
			}
			// Opaque white backdrops with no sprite are broken rips; show them as a translucent veil instead.
			for (int i = 0; i < t.childCount; i++)
			{
				Transform c = t.GetChild(i);
				Image img = (c.name == "Panel") ? c.GetComponent<Image>() : null;
				if (img != null && img.sprite == null && img.color.a > 0.9f && img.color.r > 0.9f)
				{
					img.color = new Color(1f, 1f, 1f, 0.55f);
				}
			}
			_gateGos.Add(t.gameObject);
			_gateCanvases.Add(cv);
			cv.enabled = false;
			t.gameObject.SetActive(false);
		}

		// The reference shows a plain "M" macro button in this spot.
		private void RelabelMacroButton()
		{
			Transform m5 = transform.Find("In-Game UI/_SkillButtons/_MiniChatCanvas/Macro/M5");
			TextMeshProUGUI label = (m5 != null) ? m5.GetComponentInChildren<TextMeshProUGUI>(true) : null;
			if (label != null)
			{
				label.text = "M";
			}
		}

		// The bottom quick-slot bar sits on its own canvas and drifts to the top of the screen while
		// a window is open, so it is hidden whenever Inventory, Equipment or Skills is open.
		private Canvas _miniCanvas;

		private void UpdateMiniQuickBar()
		{
			if (_miniCanvas == null)
			{
				Transform mini = transform.Find("In-Game UI/_SkillButtons/_MiniChatCanvas");
				_miniCanvas = (mini != null) ? mini.GetComponent<Canvas>() : null;
				if (_miniCanvas == null)
				{
					return;
				}
			}
			bool windowOpen = false;
			for (int i = 0; i < _gateCanvases.Count; i++)
			{
				Canvas cv = _gateCanvases[i];
				if (cv != null && cv.enabled && (cv.name == "Inventory Canvas" || cv.name == "Equipment Canvas" || cv.name == "Skills Canvas"))
				{
					windowOpen = true;
					break;
				}
			}
			if (_miniCanvas.enabled == windowOpen)
			{
				_miniCanvas.enabled = !windowOpen;
			}
		}

		private void UpdateWindowGates()
		{
			for (int i = 0; i < _gateGos.Count; i++)
			{
				GameObject go = _gateGos[i];
				Canvas cv = _gateCanvases[i];
				if (go == null || cv == null)
				{
					continue;
				}
				bool open = cv.enabled;
				if (go.activeSelf != open)
				{
					go.SetActive(open);
				}
			}
		}

		private void WireWindowActions()
		{
			Button invClose = ButtonAt("In-Game UI/OverlayPanels/Invent Equip Canvas/Inventory Canvas/InventPanel/BackButton (1)");
			if (invClose != null)
			{
				invClose.onClick.AddListener(CloseInventoryWindow);
			}
			Button invOpen = ButtonAt("In-Game UI/_SkillButtons/_ETCButtons/Controller/btnInventory");
			if (invOpen != null)
			{
				invOpen.onClick.AddListener(RequestInventory);
			}
		}

		private Button ButtonAt(string path)
		{
			Transform t = transform.Find(path);
			return (t != null) ? t.GetComponent<Button>() : null;
		}

		private void SetCanvasEnabled(string path, bool on)
		{
			Transform t = transform.Find(path);
			Canvas c = (t != null) ? t.GetComponent<Canvas>() : null;
			if (c != null)
			{
				c.enabled = on;
			}
		}

		// The prefab's close button only calls a stub, so restore the HUD that the window hid.
		private void CloseInventoryWindow()
		{
			SetCanvasEnabled("In-Game UI/OverlayPanels/Invent Equip Canvas/Inventory Canvas", false);
			SetCanvasEnabled("In-Game UI/OverlayPanels/Invent Equip Canvas/Equipment Canvas", false);
			SetCanvasEnabled("In-Game UI/_SkillButtons", true);
			SetCanvasEnabled("In-Game UI/MainView/OuterView", true);
		}

		private void RequestInventory()
		{
			WorldTcpClient client = WorldNet.Client;
			if (client != null)
			{
				client.SendInventoryRequest();
			}
		}

		/// <summary>
		/// Tap a filled inventory slot: send the item to its wear slot on the server. The equip
		/// position mirrors the starter-gear convention (equip_slot = wearPosition - 1), so a
		/// weapon replaces the weapon already sitting in that slot.
		/// </summary>
		private void EquipInventoryItem(int slot)
		{
			if (slot < 0 || slot >= _invMain.Count)
			{
				return;
			}
			int mid = _invMain[slot];
			int sid = _invSub[slot];
			if (mid < 0)
			{
				return;
			}
			ItemObject io;
			if (!CharacterStatBlock.TryGetItem(mid, sid, out io) || io == null)
			{
				return;
			}
			// openable items: prompt to open (original flow) — the server rolls the box
			if (io.randomBox != null && io.randomBox.Length > 0)
			{
				ShowUsePrompt(io, mid, sid);
				return;
			}
			// not equippable at all (Nothing wear position) — don't junk-equip it
			if ((int)io.wearPosition == 0)
			{
				return;
			}
			int pos;
			if ((int)io.type == 12)
			{
				// arrows: wearPosition is Handheld (6) like the bow — using wear-1 would
				// displace the weapon; the seed keeps arrows in slot 6 beside the weapon
				pos = 6;
			}
			else
			{
				pos = Mathf.Max(0, (int)io.wearPosition - 1);
			}
			WorldTcpClient client = WorldNet.Client;
			if (client != null)
			{
				client.SendEquip(mid, sid, pos, false);
			}
		}

		private GameObject _usePromptGo;

		/// <summary>Original-style confirmation before opening an item ("prompt to open"):
		/// Yes -> USE_ITEM (server consumes + rolls the box), No -> dismiss.</summary>
		private void ShowUsePrompt(ItemObject io, int mid, int sid)
		{
			Transform host = transform.Find("In-Game UI/OverlayPanels/Invent Equip Canvas/Inventory Canvas");
			if (host == null)
			{
				host = transform;
			}
			if (_usePromptGo != null)
			{
				Destroy(_usePromptGo);
			}
			_usePromptGo = new GameObject("UsePrompt", typeof(RectTransform));
			_usePromptGo.transform.SetParent(host, false);
			RectTransform rt = (RectTransform)_usePromptGo.transform;
			rt.anchorMin = new Vector2(0.5f, 0.5f);
			rt.anchorMax = new Vector2(0.5f, 0.5f);
			rt.pivot = new Vector2(0.5f, 0.5f);
			rt.sizeDelta = new Vector2(700f, 300f);
			rt.anchoredPosition = Vector2.zero;
			Image bg = _usePromptGo.AddComponent<Image>();
			bg.color = new Color(0.03f, 0.05f, 0.09f, 0.97f);
			bg.raycastTarget = true;
			BoxOutline(rt, new Color(0.55f, 0.62f, 0.75f, 0.9f));

			TextMeshProUGUI title = MakeText(_usePromptGo.transform, "PromptTitle", 34f,
				TextAlignmentOptions.Center, new Color(0.45f, 1f, 0.55f));
			((RectTransform)title.transform).anchorMin = new Vector2(0f, 1f);
			((RectTransform)title.transform).anchorMax = new Vector2(1f, 1f);
			((RectTransform)title.transform).pivot = new Vector2(0.5f, 1f);
			((RectTransform)title.transform).sizeDelta = new Vector2(0f, 50f);
			((RectTransform)title.transform).anchoredPosition = new Vector2(0f, -16f);
			title.text = "Open Item";

			TextMeshProUGUI q = MakeText(_usePromptGo.transform, "PromptQ", 26f,
				TextAlignmentOptions.Center, Color.white);
			((RectTransform)q.transform).anchorMin = new Vector2(0f, 1f);
			((RectTransform)q.transform).anchorMax = new Vector2(1f, 1f);
			((RectTransform)q.transform).pivot = new Vector2(0.5f, 1f);
			((RectTransform)q.transform).sizeDelta = new Vector2(-40f, 110f);
			((RectTransform)q.transform).anchoredPosition = new Vector2(0f, -84f);
			q.text = "Do you want to open '" + io.Name + "'?";
			q.enableWordWrapping = true;

			int pMid = mid;
			int pSid = sid;
			GameObject yesGo = new GameObject("Yes", typeof(RectTransform));
			yesGo.transform.SetParent(_usePromptGo.transform, false);
			BuildQuestButton((RectTransform)yesGo.transform, new Vector2(0.5f, 0f),
				new Vector2(220f, 56f), new Vector2(-130f, 22f), "Yes", delegate
				{
					WorldTcpClient client = WorldNet.Client;
					if (client != null)
					{
						client.SendUseItem(pMid, pSid);
					}
					if (_usePromptGo != null)
					{
						_usePromptGo.SetActive(false);
					}
				});
			GameObject noGo = new GameObject("No", typeof(RectTransform));
			noGo.transform.SetParent(_usePromptGo.transform, false);
			BuildQuestButton((RectTransform)noGo.transform, new Vector2(0.5f, 0f),
				new Vector2(220f, 56f), new Vector2(130f, 22f), "No", delegate
				{
					if (_usePromptGo != null)
					{
						_usePromptGo.SetActive(false);
					}
				});

			_usePromptGo.transform.SetAsLastSibling();
		}

		/// <summary>Tap a filled paper-doll slot: send that item back to the bag (server round-trip).</summary>
		private void UnequipSlot(string slotKey)
		{
			WorldTcpClient.InventoryItemInfo it;
			if (!_equipSlotItem.TryGetValue(slotKey, out it) || it == null)
			{
				return;
			}
			WorldTcpClient client = WorldNet.Client;
			if (client != null)
			{
				client.SendEquip(it.mainId, it.subId, it.position, true);
			}
		}

		private void WireWindows()
		{
			// Top-right readouts: use the visible set (the MapDetails group is inactive in the prefab).
			SetLabel(ref _coordLabel, "In-Game UI/_SkillButtons/_LABELS (Bottom Left)/Coordinates");
			SetLabel(ref _channelLabel, "In-Game UI/_SkillButtons/_LABELS (Bottom Left)/Channel");
			SetLabel(ref _serverLabel, "In-Game UI/_SkillButtons/_LABELS (Bottom Left)/Server");
			SetLabel(ref _mapInfoLabel, "In-Game UI/_SkillButtons/_LABELS (Bottom Left)/MapInfo");
			SetLabel(ref _dateLabel, "In-Game UI/_SkillButtons/_LABELS (Bottom Left)/DateAndTime");

			TextMeshProUGUI version = PathText("In-Game UI/OverlayPanels/Text (TMP)");
			if (version != null)
			{
				version.text = "Version: " + Application.version;
			}

			// The Bag button is WorldHUD's own; the inventory now lives in the prefab window.
			if (_hud != null)
			{
				Transform bag = _hud.transform.Find("HUDCanvas/BagBtn");
				if (bag != null)
				{
					bag.gameObject.SetActive(false);
				}
			}

			HideHudExtras();
			LayoutRadialMenu();

			// Bottom-left stance icon: the prefab wires it to empty UIHUDFunctions stubs.
			Transform stance = Deep(transform, "AttackIdle");
			if (stance != null)
			{
				ClickListener(stance.gameObject).onClick.AddListener(OnAttackPressed);
			}

			// The loot hand only shows while a drop is within reach (see TickHudExtras).
			_lootBtn = Deep(transform, "Loot");
			if (_lootBtn != null)
			{
				_lootBtn.gameObject.SetActive(false);
			}

			GateOverlayWindows();
			RelabelMacroButton();
			BuildInventoryWindow();
			BuildSkillWindow();
			BuildCharacterWindow();
			BuildEquipmentWindow();
			WireWindowActions();
		}

		private void SetLabel(ref TextMeshProUGUI field, string path)
		{
			TextMeshProUGUI found = PathText(path);
			if (found != null)
			{
				field = found;
			}
		}

		private TextMeshProUGUI PathText(string path)
		{
			Transform t = transform.Find(path);
			return (t != null) ? t.GetComponent<TextMeshProUGUI>() : null;
		}

		// The prefab leaves all eight radial items stacked at the hub (anchoredPosition 0,0)
		// because RadialHoldMenu is an empty stub. Fan them out in an EVEN ring (45° apart,
		// same radius) around the grid "Tab" button — matching the original — but keep them
		// HIDDEN until that button is held. The grid button itself carries the 4-square
		// "menu-btns" icon and sits at the centre of the ring.
		private void LayoutRadialMenu()
		{
			Transform ctrl = transform.Find("In-Game UI/_SkillButtons/_ETCButtons/Controller");
			if (ctrl == null)
			{
				return;
			}
			string[] order = { "btnInventory", "Skills", "Char", "itemBank", "Party", "QuestBtn", "FriendsBtn", "ClubBtn" };
			const float radius = 140f;
			var items = new List<GameObject>();
			for (int i = 0; i < order.Length; i++)
			{
				RectTransform rt = ctrl.Find(order[i]) as RectTransform;
				if (rt == null)
				{
					continue;
				}
				rt.anchorMin = new Vector2(0.5f, 0.5f);
				rt.anchorMax = new Vector2(0.5f, 0.5f);
				rt.pivot = new Vector2(0.5f, 0.5f);
				// Even 45° spacing; start at top and go clockwise like the reference ring.
				float angle = (90f - i * 45f) * Mathf.Deg2Rad;
				rt.anchoredPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
				// Hidden by default — revealed only while the grid Tab button is held.
				rt.gameObject.SetActive(false);
				items.Add(rt.gameObject);
			}

			// The grid "Tab" button: give it the 4-square icon, park it at the ring centre
			// (the Controller hub) so the fan opens around it, and wire press-and-hold.
			Transform tab = transform.Find("In-Game UI/_SkillButtons/SkillBackground/SkillTabBTN");
			if (tab == null)
			{
				tab = Deep(transform, "SkillTabBTN");
			}
			if (tab != null)
			{
				ApplyGridIcon(tab);
				// Co-locate the trigger with the fan hub so the ring is centred on it.
				(tab as RectTransform).position = ctrl.position;

				RadialHoldToggle hold = tab.GetComponent<RadialHoldToggle>();
				if (hold == null)
				{
					hold = tab.gameObject.AddComponent<RadialHoldToggle>();
				}
				hold.Items.Clear();
				hold.Items.AddRange(items);
				hold.SetOpen(false);
				// Make sure the button actually receives pointer events.
				ClickListener(tab.gameObject);
			}
		}

		/// <summary>
		/// Put the 4-square "menu-btns" grid sprite on the radial trigger (loaded from
		/// Resources/ui/menu-btns), replacing the plain circle base, and drop the stray
		/// page-number label that used to sit over it.
		/// </summary>
		private static void ApplyGridIcon(Transform tab)
		{
			// Hide any leftover text label (e.g. "1"/"Tab") so only the grid icon shows.
			Transform txt = tab.Find("SkillTabTXT");
			if (txt != null)
			{
				txt.gameObject.SetActive(false);
			}
			Sprite grid = Resources.Load<Sprite>("ui/menu-btns");
			if (grid == null)
			{
				return;
			}
			Image img = tab.GetComponent<Image>();
			if (img != null)
			{
				img.sprite = grid;
				img.enabled = true;
			}
		}

		private void BuildInventoryWindow()
		{
			Transform panel = transform.Find("In-Game UI/OverlayPanels/Invent Equip Canvas/Inventory Canvas/InventPanel");
			if (panel == null)
			{
				return;
			}
			// Original layout: the slots live inside the baked SCROLL VIEW
			// (InventContent/Invent_Scroll View/Invent_Viewport/Invent_Content) with the
			// rip's own GridLayout — cell 156x156, spacing 5, 6 columns, ContentSizeFitter,
			// vertical scrollbar + UP/DOWN — that grid IS the original spaces/size. The old
			// code laid a private 30-cell grid on InventContent with computed cells and
			// margins, which never matched the original window.
			Transform content = panel.Find("InventContent/InventoryCanvas/Invent_Scroll View/Invent_Viewport/Invent_Content");
			if (content == null)
			{
				content = panel.Find("InventContent");
			}
			if (content != null)
			{
				// The rip bakes a garbage anchoredPosition (y≈932047) on the scroll
				// content — every slot renders off-screen until it's re-anchored to the
				// viewport's top (CSF+grid then grow it downward for scrolling).
				RectTransform crt = content as RectTransform;
				if (crt != null)
				{
					crt.anchorMin = new Vector2(0f, 1f);
					crt.anchorMax = new Vector2(1f, 1f);
					crt.pivot = new Vector2(0.5f, 1f);
					crt.anchoredPosition = Vector2.zero;
					crt.offsetMin = Vector2.zero;
					crt.offsetMax = Vector2.zero;
					crt.localScale = Vector3.one;
				}
				ScrollRect sr = content.GetComponentInParent<ScrollRect>();
				if (sr != null)
				{
					sr.verticalNormalizedPosition = 1f;
				}
			}
			if (content != null && _invIcons.Count == 0)
			{
				// keep the baked grid untouched — 60 slots (the server bag cap) laid out
				// by the original grid + fitter, scrollable like the original window
				for (int i = 0; i < InventorySlotCount; i++)
				{
					GameObject slot = MakeSlot(content, "Slot " + (i + 1));
					Image icon = MakeImage(slot.transform, "Icon", new Vector2(0.12f, 0.12f), new Vector2(0.88f, 0.88f));
					icon.enabled = false;
					_invIcons.Add(icon);
					_invNames.Add(MakeText(slot.transform, "Name", 13f, TextAlignmentOptions.Center, new Color(1f, 1f, 1f, 0.9f)));
					_invCounts.Add(MakeText(slot.transform, "Count", 18f, TextAlignmentOptions.BottomRight, new Color(1f, 0.85f, 0.3f)));
					_invMain.Add(-1);
					_invSub.Add(-1);
					// Tapping a filled slot equips it (the bag click -> equip round-trip).
					Image frame = slot.GetComponent<Image>();
					if (frame != null)
					{
						frame.raycastTarget = true;
					}
					int slotIdx = i;
					Button btn = slot.GetComponent<Button>();
					if (btn == null)
					{
						btn = slot.AddComponent<Button>();
						btn.transition = Selectable.Transition.None;
						btn.targetGraphic = frame;
					}
					btn.onClick.AddListener(delegate { EquipInventoryItem(slotIdx); });
				}
			}

			if (_invGold == null)
			{
				_invGold = MakeText(panel, "GoldReadout", 34f, TextAlignmentOptions.MidlineLeft, new Color(1f, 0.9f, 0.45f));
				RectTransform gold = (RectTransform)_invGold.transform;
				gold.anchorMin = Vector2.zero;
				gold.anchorMax = Vector2.zero;
				gold.pivot = Vector2.zero;
				gold.sizeDelta = new Vector2(320f, 56f);
				gold.anchoredPosition = new Vector2(24f, 20f);
			}

			RefreshInventoryWindow();
		}

		private void BuildSkillWindow()
		{
			Transform upper = transform.Find("In-Game UI/OverlayPanels/Skills Canvas/SkillSlotsCanvas/PanelBase (1)/Upper Blocks");
			Transform cells = (upper != null) ? upper.Find("Panel") : null;
			if (cells != null && _setIcons.Count == 0)
			{
				// The prefab bakes 10 "Skill Slot (N)" buttons inside Panel's grid; put one icon in each.
				for (int i = 1; i <= SkillSetSlotCount; i++)
				{
					Transform cell = cells.Find("Skill Slot (" + i + ")");
					if (cell == null)
					{
						continue;
					}
					Transform existing = cell.Find("Icon");
					Image icon = (existing != null) ? existing.GetComponent<Image>() : MakeImage(cell, "Icon", new Vector2(0.1f, 0.1f), new Vector2(0.9f, 0.9f));
					if (icon != null)
					{
						icon.enabled = false;
						_setIcons.Add(icon);
					}
				}
			}

			Transform points = transform.Find("In-Game UI/OverlayPanels/Skills Canvas/SkillsListCanvas/PanelBase/SkillPoint");
			if (points != null)
			{
				foreach (TextMeshProUGUI tmp in points.GetComponentsInChildren<TextMeshProUGUI>(true))
				{
					if (tmp.name != "PointLabel")
					{
						_skillPoints = tmp;
						break;
					}
				}
				if (_skillPoints == null)
				{
					_skillPoints = MakeText(points, "PointValue", 30f, TextAlignmentOptions.MidlineRight, new Color(1f, 0.9f, 0.45f));
				}
			}
		}

		private void RefreshInventoryWindow()
		{
			UnityEngine.Debug.Log("[Inv] refresh icons=" + _invIcons.Count + " items=" + WorldRuntime.Items.Count +
				(WorldRuntime.Items.Count > 0 ? (" first=" + WorldRuntime.Items[0].mainId + "/" + WorldRuntime.Items[0].subId + "@slot" + WorldRuntime.Items[0].slot) : ""));
			if (_invIcons.Count == 0)
			{
				return;
			}			for (int i = 0; i < _invIcons.Count; i++)
			{
				_invIcons[i].sprite = null;
				_invIcons[i].enabled = false;
				_invNames[i].text = string.Empty;
				_invCounts[i].text = string.Empty;
				_invMain[i] = -1;
				_invSub[i] = -1;
			}

			bool[] used = new bool[_invIcons.Count];
			List<WorldTcpClient.InventoryItemInfo> items = WorldRuntime.Items;
			for (int n = 0; n < items.Count; n++)
			{
				WorldTcpClient.InventoryItemInfo item = items[n];
				int idx = -1;
				if (item.slot >= 0 && item.slot < used.Length && !used[item.slot])
				{
					idx = item.slot;
				}
				else
				{
					for (int j = 0; j < used.Length; j++)
					{
						if (!used[j])
						{
							idx = j;
							break;
						}
					}
				}
				if (idx < 0)
				{
					break;
				}
				used[idx] = true;

				Sprite icon = ItemIconFor(item.mainId, item.subId);
				if (icon != null)
				{
					_invIcons[idx].sprite = icon;
					_invIcons[idx].enabled = true;
				}
				else
				{
					_invNames[idx].text = WorldRuntime.ItemName(item.mainId, item.subId);
				}
				_invCounts[idx].text = (item.count > 1) ? item.count.ToString() : string.Empty;
				_invMain[idx] = item.mainId;
				_invSub[idx] = item.subId;
			}

			if (_invGold != null)
			{
				_invGold.text = WorldRuntime.Gold.ToString("N0");
			}
			// diagnostic: did the fill's visual actually land on the visible slot?
			if (_invIcons.Count > 0)
			{
				UnityEngine.Debug.Log("[Inv] slot0 icon.enabled=" + _invIcons[0].enabled +
					" sprite=" + (_invIcons[0].sprite != null ? _invIcons[0].sprite.name : "NULL") +
					" active=" + _invIcons[0].gameObject.activeInHierarchy +
					" parent=" + _invIcons[0].transform.parent.name +
					" name0='" + _invNames[0].text + "' count0='" + _invCounts[0].text + "'" +
					" main0=" + _invMain[0]);
			}
		}

		private void RefreshSkillWindow()
		{
			// Full Skills-menu rebuild (4 tabs from skillmenu.json, SP footer, slot icons)
			// lives in OriginalHudSkillMenu.cs — this keeps the OnSkills call chain intact.
			RefreshSkillMenu();
		}

		// ---- Character (Player Stats Canvas) ----

		/// <summary>
		/// Wire the ripped Character window: name, EXP bar, the five main stats with their
		/// + buttons (local stat allocation), and the Condition block readouts. The window's
		/// own Exit button already closes it via the prefab; we only fill in live values.
		/// </summary>
		private void BuildCharacterWindow()
		{
			Transform root = transform.Find(StatsRootPath);
			if (root == null)
			{
				return;
			}

			_charName = DeepText(transform.Find(StatsCanvasPath + "/Canvas Skills & Stats/PanelBase/IgnSection/_Player Name"));
			Transform expSection = root.Find("EXP section");
			if (expSection != null)
			{
				_charExpFill = DeepImage(expSection.Find("ExpFIll"));
				_charExpLabel = DeepText(expSection.Find("_ExpNoLabel"));
			}

			// Main stats + their + buttons.
			_statValues.Clear();
			// Row → source stat. The window's label text renames stats by effect:
			// 'STR' row = POW (attack), 'DEX' = DEX, 'INT' = SPI (magic/MP),
			// 'VIT' = STR (HP), 'STM' = STA (SP). Buttons keep their prefab names.
			WireStat(root, WorldRuntime.StatKind.Pow, "PowLabel", "_PowValue", "Pow++");
			WireStat(root, WorldRuntime.StatKind.Dex, "DexLabel", "_DexValue", "Dex++");
			WireStat(root, WorldRuntime.StatKind.Spi, "IntLabel", "_IntValue", "Int++");
			WireStat(root, WorldRuntime.StatKind.Str, "VitLabel", "_VitValue", "Vit++");
			WireStat(root, WorldRuntime.StatKind.Sta, "StmLabel", "_StmValue", "Stm++");
			_charPoints = DeepText(root.Find("MainStats Section/Point"));

			// Condition block values.
			_condValues.Clear();
			Transform cond = root.Find("CharCon Section");
			if (cond != null)
			{
				_condValues["level"] = DeepText(cond.Find("__PlayerLevel__"));
				_condValues["atk"] = DeepText(cond.Find("__PlayerAttack__"));
				_condValues["def"] = DeepText(cond.Find("__PlayerDefense__"));
				_condValues["hp"] = DeepText(cond.Find("__PlayerHP__"));
				_condValues["mp"] = DeepText(cond.Find("__PlayerMP__"));
				_condValues["sp"] = DeepText(cond.Find("__PlayerSP__"));
				_condValues["hit"] = DeepText(cond.Find("__PlayerHitRate__"));
				_condValues["avoid"] = DeepText(cond.Find("__PlayerAvoid__"));
			}
			// Personality block: school/class.
			Transform pers = root.Find("Char Personality Section");
			if (pers != null)
			{
				_condValues["class"] = DeepText(pers.Find("__PlayerClass__"));
			}

			if (!_charWired)
			{
				_charWired = true;
				WorldRuntime.StatsChanged += RefreshCharacterWindow;
				WorldRuntime.ExpChanged += RefreshCharacterWindow;
				WorldRuntime.VitalsChanged += RefreshCharacterWindow;
				WorldRuntime.InventoryChanged += RefreshCharacterWindow;
			}
			RefreshCharacterWindow();
		}

		private void WireStat(Transform root, WorldRuntime.StatKind kind, string labelName, string valueName, string buttonName)
		{
			Transform section = root.Find("MainStats Section");
			if (section == null)
			{
				return;
			}
			_statValues[kind] = DeepText(section.Find(valueName));
			Button btn = ButtonAt(StatsRootPath + "/MainStats Section/" + buttonName);
			if (btn != null)
			{
				// Capture the kind per listener.
				WorldRuntime.StatKind k = kind;
				btn.onClick.AddListener(delegate
				{
					OnStatPlus(k);
			});
			}
		}

		private void OnStatPlus(WorldRuntime.StatKind kind)
		{
			if (WorldRuntime.StatPoints <= 0)
			{
				ShowHudToast("No stat points available");
				return;
			}
			WorldRuntime.SpendStatPoint(kind);
		}

		private void RefreshCharacterWindow()
		{
			if (!_charWired)
			{
				return;
			}
			if (_charName != null)
			{
				_charName.text = string.IsNullOrEmpty(WorldRuntime.LocalName) ? "Player" : WorldRuntime.LocalName;
			}
			if (_charExpFill != null)
			{
				float ratio = (WorldRuntime.MaxExp > 0) ? Mathf.Clamp01((float)WorldRuntime.Exp / WorldRuntime.MaxExp) : 0f;
				_charExpFill.fillAmount = ratio;
			}
			if (_charExpLabel != null)
			{
				_charExpLabel.text = WorldRuntime.Exp + "/ " + WorldRuntime.MaxExp;
			}
			SetStat(WorldRuntime.StatKind.Pow);
			SetStat(WorldRuntime.StatKind.Dex);
			SetStat(WorldRuntime.StatKind.Spi);
			SetStat(WorldRuntime.StatKind.Str);
			SetStat(WorldRuntime.StatKind.Sta);
			if (_charPoints != null)
			{
				_charPoints.text = WorldRuntime.StatPoints.ToString();
			}
			SetCond("level", WorldRuntime.Level.ToString());
			SetCond("atk", WorldRuntime.AttackRangeText());
			SetCond("def", WorldRuntime.DefenseValue().ToString());
			SetCond("hp", WorldRuntime.Hp + "/" + WorldRuntime.MaxHp);
			SetCond("mp", WorldRuntime.Mp + "/" + WorldRuntime.MaxMp);
			SetCond("sp", WorldRuntime.Sp + "/" + WorldRuntime.MaxSp);
			SetCond("hit", WorldRuntime.HitValue().ToString());
			SetCond("avoid", WorldRuntime.AvoidValue().ToString());
			Transform pers = transform.Find(StatsRootPath + "/Char Personality Section");
			Transform classT = (pers != null) ? pers.Find("__PlayerClass__") : null;
			TextMeshProUGUI classTxt = DeepText(classT);
			if (classTxt != null)
			{
				classTxt.text = WorldRuntime.ClassName;
			}
		}

		private void SetStat(WorldRuntime.StatKind kind)
		{
			TextMeshProUGUI t;
			if (_statValues.TryGetValue(kind, out t) && t != null)
			{
				int v = StatOf(kind);
				t.text = v.ToString();
			}
		}

		private static int StatOf(WorldRuntime.StatKind kind)
		{
			switch (kind)
			{
				case WorldRuntime.StatKind.Pow: return WorldRuntime.Pow;
				case WorldRuntime.StatKind.Dex: return WorldRuntime.Dex;
				case WorldRuntime.StatKind.Spi: return WorldRuntime.Spi;
				case WorldRuntime.StatKind.Str: return WorldRuntime.Str;
				default: return WorldRuntime.Sta;
			}
		}

		private void SetCond(string key, string value)
		{
			TextMeshProUGUI t;
			if (_condValues.TryGetValue(key, out t) && t != null)
			{
				t.text = value;
			}
		}

		private TextMeshProUGUI DeepText(Transform t)
		{
			return (t != null) ? t.GetComponent<TextMeshProUGUI>() : null;
		}

		private UnityEngine.UI.Image DeepImage(Transform t)
		{
			return (t != null) ? t.GetComponent<UnityEngine.UI.Image>() : null;
		}

		private void ShowHudToast(string msg)
		{
			if (_hud != null)
			{
				_hud.ShowToast(msg);
			}
		}

		// ---- Equipment (paper-doll) ----

		/// <summary>
		/// Wire the ripped Equipment window: cache each equip_* slot Image and refresh the
		/// paper-doll from WorldRuntime.Equipped. Tapping a filled slot unequips it (round-trips
		/// to the server; the result refreshes the list). Slots are matched to equipped items by
		/// the item's own wearPosition (read from the ItemDatabase), so the server's numeric
		/// position codes don't matter.
		/// </summary>
		private void BuildEquipmentWindow()
		{
			Transform grid = transform.Find(EquipGridPath);
			if (grid == null)
			{
				return;
			}
			_equipSlots.Clear();
			for (int i = 0; i < grid.childCount; i++)
			{
				Transform child = grid.GetChild(i);
				if (child.name.StartsWith("equip_"))
				{
					UnityEngine.UI.Image img = child.GetComponent<UnityEngine.UI.Image>();
					if (img != null)
					{
						List<UnityEngine.UI.Image> list;
						if (!_equipSlots.TryGetValue(child.name, out list))
						{
							list = new List<UnityEngine.UI.Image>();
							_equipSlots[child.name] = list;
						}
						list.Add(img);
						img.enabled = false;
					}
					// Tapping a filled paper-doll slot unequips it (round-trips to the server);
					// the icon Image only raycasts when it's shown, so empty slots stay inert.
					if (img != null)
					{
						img.raycastTarget = true;
					}
					string slotKey = child.name;
					Button slotBtn = child.GetComponent<Button>();
					if (slotBtn == null)
					{
						slotBtn = child.gameObject.AddComponent<Button>();
						slotBtn.transition = Selectable.Transition.None;
						slotBtn.targetGraphic = img;
					}
					slotBtn.onClick.AddListener(delegate { UnequipSlot(slotKey); });
				}
			}
			if (!_equipWired)
			{
				_equipWired = true;
				WorldRuntime.InventoryChanged += RefreshEquipmentWindow;
			}
			RefreshEquipmentWindow();
		}

		private void RefreshEquipmentWindow()
		{
			if (_equipSlots.Count == 0)
			{
				return;
			}
			// Blank every slot first.
			_equipSlotItem.Clear();
			foreach (KeyValuePair<string, List<UnityEngine.UI.Image>> kv in _equipSlots)
			{
				for (int i = 0; i < kv.Value.Count; i++)
				{
					if (kv.Value[i] != null)
					{
						kv.Value[i].sprite = null;
						kv.Value[i].enabled = false;
					}
				}
			}
			// Track how many of each slot key we've filled so two rings can both show.
			Dictionary<string, int> filled = new Dictionary<string, int>();
			List<WorldTcpClient.InventoryItemInfo> equipped = WorldRuntime.Equipped;
			for (int i = 0; i < equipped.Count; i++)
			{
				WorldTcpClient.InventoryItemInfo it = equipped[i];
				string slotKey = ResolveEquipSlot(it.mainId, it.subId);
				if (slotKey == null)
				{
					continue;
				}
				List<UnityEngine.UI.Image> slots;
				if (!_equipSlots.TryGetValue(slotKey, out slots) || slots == null || slots.Count == 0)
				{
					continue;
				}
				int idx;
				filled.TryGetValue(slotKey, out idx);
				if (idx >= slots.Count)
				{
					idx = 0; // wrap into the first slot if more items than buttons
				}
				filled[slotKey] = idx + 1;
				if (idx == 0 && !_equipSlotItem.ContainsKey(slotKey))
				{
					_equipSlotItem[slotKey] = it;
				}
				Sprite icon = ItemIconFor(it.mainId, it.subId);
				if (icon != null && slots[idx] != null)
				{
					slots[idx].sprite = icon;
					slots[idx].enabled = true;
				}
			}
		}

		/// <summary>Map an equipped item to an equip_* slot via its exact wearPosition enum name.
		/// Arrows are the exception: they're wearPosition Handheld too (like the bow), so
		/// without the split they'd fight the weapon for equip_weapon — the original paper
		/// doll shows bow in equip_weapon and arrows beside it in equip_2ndweapon.</summary>
		private string ResolveEquipSlot(int mainId, int subId)
		{
			string wear = WearPositionOf(mainId, subId);
			if (string.IsNullOrEmpty(wear))
			{
				return null;
			}
			ItemObject io;
			if (CharacterStatBlock.TryGetItem(mainId, subId, out io) && io != null && (int)io.type == 12)
			{
				return "equip_2ndweapon";
			}
			string slotKey;
			return _wearToSlot.TryGetValue(wear, out slotKey) ? slotKey : null;
		}

		// Cheap periodic work: the clock (every 10 s) and the loot hand (checked every 0.25 s).
		private void TickHudExtras(float now)
		{
			UpdateWindowGates();
			UpdateMiniQuickBar();
			UpdateSkillCooldowns();
			UpdateBuffTimers();
			if (now >= _nextDateAt)
			{
				_nextDateAt = now + 10f;
				RefreshDate();
			}
			if (_lootBtn == null || now < _nextLootCheck)
			{
				return;
			}
			_nextLootCheck = now + 0.25f;
			bool near = false;
			if (WorldRuntime.LocalPlayer != null)
			{
				foreach (KeyValuePair<string, WorldDropView> pair in WorldRuntime.DropViews)
				{
					WorldDropView view = pair.Value;
					if (view != null && (view.transform.position - WorldRuntime.LocalPlayer.position).sqrMagnitude < 16f)
					{
						near = true;
						break;
					}
				}
			}
			if (_lootBtn.gameObject.activeSelf != near)
			{
				_lootBtn.gameObject.SetActive(near);
			}
		}

		/// <summary>
		/// Drive the cooldown overlay + seconds label on each ripped wheel button from the
		/// skill-use cooldowns held by WorldHUD. Fills the overlay radially as it sweeps.
		/// The wheel is two pages (tab1 = skills 0-4, tab2 = skills 5-9).
		/// </summary>
		private void UpdateSkillCooldowns()
		{
			if (_hud == null)
			{
				return;
			}
			List<WorldTcpClient.SkillInfo> skills = WorldRuntime.Skills;
			UpdateCooldownPage(_tab1Buttons, skills, 0);
			UpdateCooldownPage(_tab2Buttons, skills, 5);
		}

		private void UpdateCooldownPage(List<Transform> buttons, List<WorldTcpClient.SkillInfo> skills, int skillOffset)
		{
			for (int i = 0; i < buttons.Count; i++)
			{
				Transform btn = buttons[i];
				if (btn == null)
				{
					continue;
				}
				Transform cdT = btn.Find("CdOverlay");
				if (cdT == null)
				{
					continue;
				}
				Image cdImg = cdT.GetComponent<Image>();
				Transform txtT = cdT.Find("CdText");
				TextMeshProUGUI cdTxt = (txtT != null) ? txtT.GetComponent<TextMeshProUGUI>() : null;
				int skillIndex = skillOffset + i;
				if (skillIndex >= skills.Count)
				{
					// No skill bound — keep the overlay hidden.
					if (cdImg != null) { cdImg.color = new Color(0f, 0f, 0f, 0f); }
					if (cdTxt != null) { cdTxt.text = string.Empty; }
					continue;
				}
				WorldTcpClient.SkillInfo info = skills[skillIndex];
				float remain = _hud.CooldownRemaining(info.mainId, info.subId);
				float total = Mathf.Max(0.001f, info.cooldownMs / 1000f);
				if (remain > 0f)
				{
					if (cdImg != null) { cdImg.color = new Color(0f, 0f, 0f, 0.6f); cdImg.fillAmount = Mathf.Clamp01(remain / total); }
					if (cdTxt != null) { cdTxt.text = Mathf.CeilToInt(remain).ToString(); }
				}
				else
				{
					if (cdImg != null) { cdImg.color = new Color(0f, 0f, 0f, 0f); }
					if (cdTxt != null) { cdTxt.text = string.Empty; }
				}
			}
		}

		// Item icons come from the ripped ItemDatabase, keyed like WorldRuntime.ItemName (mainId/subId).
		private static Sprite ItemIconFor(int mainId, int subId)
		{
			EnsureItemCache();
			Sprite icon;
			return _itemIconCache.TryGetValue(((long)mainId << 32) | (uint)subId, out icon) ? icon : null;
		}

		/// <summary>The item's wearPosition enum name (e.g. "SLOT_UPPER") used to place it on the paper-doll.</summary>
		private static string WearPositionOf(int mainId, int subId)
		{
			EnsureItemCache();
			string wear;
			return _itemWearCache.TryGetValue(((long)mainId << 32) | (uint)subId, out wear) ? wear : null;
		}

		private static void EnsureItemCache()
		{
			if (_itemIconsLoaded)
			{
				return;
			}
			_itemIconsLoaded = true;
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
				long key = ((long)System.Convert.ToInt32(fMain.GetValue(io.data)) << 32) | (uint)System.Convert.ToInt32(fSub.GetValue(io.data));
				if (io.IconSource != null && !_itemIconCache.ContainsKey(key))
				{
					_itemIconCache[key] = io.IconSource;
				}
				if (!_itemWearCache.ContainsKey(key))
				{
					_itemWearCache[key] = io.wearPosition.ToString();
				}
			}
		}

		private static void SetChildText(Transform root, string childName, string value)
		{
			Transform t = root.Find(childName);
			TextMeshProUGUI text = (t != null) ? t.GetComponent<TextMeshProUGUI>() : null;
			if (text != null)
			{
				text.text = value;
			}
		}

		private static GameObject MakeSlot(Transform parent, string name)
		{
			GameObject go = new GameObject(name, typeof(RectTransform));
			go.transform.SetParent(parent, false);
			Image frame = go.AddComponent<Image>();
			frame.color = new Color(0.05f, 0.06f, 0.08f, 0.85f);
			frame.raycastTarget = false;
			Outline line = go.AddComponent<Outline>();
			line.effectColor = new Color(0.8f, 0.8f, 0.85f, 0.9f);
			line.effectDistance = new Vector2(1.5f, -1.5f);
			return go;
		}

		private static Image MakeImage(Transform parent, string name, Vector2 anchorMin, Vector2 anchorMax)
		{
			GameObject go = new GameObject(name, typeof(RectTransform));
			go.transform.SetParent(parent, false);
			RectTransform rt = (RectTransform)go.transform;
			rt.anchorMin = anchorMin;
			rt.anchorMax = anchorMax;
			rt.offsetMin = Vector2.zero;
			rt.offsetMax = Vector2.zero;
			Image img = go.AddComponent<Image>();
			img.preserveAspect = true;
			img.raycastTarget = false;
			return img;
		}

		private static TextMeshProUGUI MakeText(Transform parent, string name, float size, TextAlignmentOptions align, Color color)
		{
			GameObject go = new GameObject(name, typeof(RectTransform));
			go.transform.SetParent(parent, false);
			RectTransform rt = (RectTransform)go.transform;
			rt.anchorMin = Vector2.zero;
			rt.anchorMax = Vector2.one;
			rt.offsetMin = new Vector2(4f, 4f);
			rt.offsetMax = new Vector2(-4f, -4f);
			TextMeshProUGUI text = go.AddComponent<TextMeshProUGUI>();
			text.fontSize = size;
			text.alignment = align;
			text.color = color;
			text.raycastTarget = false;
			text.overflowMode = TextOverflowModes.Ellipsis;
			text.text = string.Empty;
			return text;
		}
	}
}
