using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Drives the ripped UICanvasGameplay HUD. The decompiled scripts on that
	/// prefab are empty stubs, so this component re-wires its bars, labels,
	/// joystick and buttons to WorldRuntime / WorldHUD, and hides the duplicate
	/// widgets WorldHUD builds for itself.
	/// </summary>
	public partial class OriginalHudDriver : MonoBehaviour
	{
		public static OriginalHudDriver Instance;

		private WorldHUD _hud;

		private WorldPlayerMover _mover;

		private UIHUDFunctions _fn;

		private Transform _hpBar;

		private Transform _mpBar;

		private Transform _spBar;

		private Transform _expBar;

		private TextMeshProUGUI _hpTxt;

		private TextMeshProUGUI _mpTxt;

		private TextMeshProUGUI _spTxt;

		private TextMeshProUGUI _expTxt;

		private TextMeshProUGUI _levelLabel;

		private TextMeshProUGUI _goldLabel;

		private TextMeshProUGUI _coordLabel;

		private TextMeshProUGUI _mapInfoLabel;

		private TextMeshProUGUI _channelLabel;

		private TextMeshProUGUI _serverLabel;

		private TextMeshProUGUI _dateLabel;

		private TextMeshProUGUI _fpsLabel;

		private readonly List<Transform> _originalSkillButtons = new List<Transform>();

		// P2: buff icon row (top-left under the bars) — one Image+timer Text per active buff.
		private RectTransform _buffRow;

		private readonly Dictionary<int, Transform> _buffIcons = new Dictionary<int, Transform>();

		// The ripped wheel is two stacked pages: Tab 1 (SkillButton 1-5) and Tab 2 (SkillButton
		// 6-10), flipped by SwitchTabBTN. We keep the per-page button lists and the visible page
		// so a click maps to the right skill and only one page renders at a time.
		private readonly List<Transform> _tab1Buttons = new List<Transform>();

		private readonly List<Transform> _tab2Buttons = new List<Transform>();

		private int _activeTab;

		private Transform _tab1Root;

		private Transform _tab2Root;

		// The skill-page indicator button ("1"/"2") that flips the wheel between Tab 1 and Tab 2.
		private Transform _pageLabel;

		private int _frames;

		private float _fpsWindowStart;

		private float _nextCoordAt;

		public static OriginalHudDriver Spawn(WorldHUD hud, WorldPlayerMover mover)
		{
			GameObject prefab = Resources.Load<GameObject>("hud/UICanvasGameplay");
			if (prefab == null)
			{
				UnityEngine.Debug.LogWarning("[OriginalHud] prefab missing from Resources/hud.");
				return null;
			}
			GameObject go = Instantiate(prefab);
			go.name = "UICanvasGameplay";
			Transform cams = go.transform.Find("Cameras");
			if (cams != null)
			{
				Destroy(cams.gameObject);
			}
			Transform overlay = go.transform.Find("In-Game UI/OverlayPanels");
			if (overlay != null)
			{
				PrepareOverlay(overlay);
			}
			Transform nameplate = Deep(go.transform, "_EnemyNameDisplay");
			if (nameplate != null)
			{
				nameplate.gameObject.SetActive(false);
			}
			OriginalHudDriver driver = go.AddComponent<OriginalHudDriver>();
			driver._hud = hud;
			driver._mover = mover;
			Instance = driver;
			UnityEngine.Debug.Log("[OriginalHud] spawned");
			return driver;
		}

		private void Start()
		{
			_fn = GetComponentInChildren<UIHUDFunctions>(true);
			WireBars();
			WireLabels();
			WireButtons();
			WireJoystick();
			WireWindows();
			HideOwnDuplicates();
			RaiseOwnCanvas();
			if (_mapInfoLabel != null)
			{
				_mapInfoLabel.text = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
			}
			if (_serverLabel != null)
			{
				_serverLabel.text = "KlassX";
			}
			if (_channelLabel != null)
			{
				_channelLabel.text = "[1]";
			}
			WorldRuntime.VitalsChanged += RefreshVitals;
			WorldRuntime.InventoryChanged += RefreshInventory;
			WorldRuntime.SkillListChanged += OnSkills;
			WorldRuntime.ExpChanged += RefreshVitals;
			WorldRuntime.BuffsChanged += RefreshBuffs;
			RefreshVitals();
			RefreshInventory();
			RefreshDate();
			OnSkills();
			RefreshBuffs();
			// World BGM — one default track for now (per-map selection can layer on top later).
			AudioManager.PlayBgm("ran12");
			// Show the equipped weapon in-hand (covers the player spawning after the inventory push).
			WorldRuntime.RefreshLocalPlayerWeapon();
			UnityEngine.Debug.Log("[OriginalHud] wired fn=" + (_fn != null));
		}

		private void OnDestroy()
		{
			WorldRuntime.VitalsChanged -= RefreshVitals;
			WorldRuntime.InventoryChanged -= RefreshInventory;
			WorldRuntime.SkillListChanged -= OnSkills;
			WorldRuntime.ExpChanged -= RefreshVitals;
			WorldRuntime.BuffsChanged -= RefreshBuffs;
			if (Instance == this)
			{
				Instance = null;
			}
		}

		private void Update()
		{
			_frames++;
			float now = Time.unscaledTime;
			TickHudExtras(now);
			TickQuestUi(now);
			if (now - _fpsWindowStart >= 0.5f)
			{
				if (_fpsLabel != null && _fpsWindowStart > 0f)
				{
					_fpsLabel.text = Mathf.RoundToInt(_frames / (now - _fpsWindowStart)) + "FPS";
				}
				_frames = 0;
				_fpsWindowStart = now;
			}
			if (now >= _nextCoordAt)
			{
				_nextCoordAt = now + 0.5f;
				RefreshCoords();
			}
		}

		private void WireBars()
		{
			Transform barPanel = Deep(transform, "BarPanel");
			if (barPanel == null)
			{
				return;
			}
			_hpBar = Deep(barPanel, "HPBar");
			_mpBar = Deep(barPanel, "MPBar");
			_spBar = Deep(barPanel, "SPBar");
			_expBar = Deep(barPanel, "EXPBar");
			if (_hpBar != null)
			{
				_hpTxt = ChildText(_hpBar, "HP TXT");
				ColorFill(_hpBar, new Color(0.87f, 0.24f, 0.20f));
			}
			if (_mpBar != null)
			{
				_mpTxt = ChildText(_mpBar, "MP TXT");
				ColorFill(_mpBar, new Color(0.23f, 0.48f, 0.92f));
			}
			if (_spBar != null)
			{
				_spTxt = ChildText(_spBar, "SP TXT");
				ColorFill(_spBar, new Color(0.25f, 0.80f, 0.38f));
				SetFill(_spBar, 1f);
			}
			if (_expBar != null)
			{
				_expTxt = ChildText(_expBar, "EXP TXT");
				ColorFill(_expBar, new Color(1f, 0.65f, 0.18f));
				SetFill(_expBar, 0f);
			}
		}

		private void WireLabels()
		{
			_coordLabel = Prefer((_fn != null) ? _fn.mapCoordinates : null, "Coordinates");
			_mapInfoLabel = Prefer((_fn != null) ? _fn.mapInfoText : null, "MapInfo");
			_channelLabel = Prefer((_fn != null) ? _fn.mapChannel : null, "Channel");
			_serverLabel = Prefer((_fn != null) ? _fn.serverNameText : null, "Server");
			_dateLabel = Prefer((_fn != null) ? _fn.dateAndTimeText : null, "DateAndTime");
			_goldLabel = (_fn != null) ? _fn.goldText : null;
			_fpsLabel = (_fn != null) ? _fn.fps : null;
			_levelLabel = (_fn != null) ? _fn.playerLevel : null;
			if (_fpsLabel == null)
			{
				Transform fpsRoot = Deep(transform, "FPS and MS");
				if (fpsRoot != null)
				{
					TextMeshProUGUI[] texts = fpsRoot.GetComponentsInChildren<TextMeshProUGUI>(true);
					for (int i = 0; i < texts.Length; i++)
					{
						string s = (texts[i].text ?? string.Empty).ToUpperInvariant();
						if (s.Contains("FPS"))
						{
							_fpsLabel = texts[i];
							break;
						}
					}
				}
			}
		}

		private void WireButtons()
		{
			Transform skillBg = Deep(transform, "SkillBackground");
			Transform basic = (skillBg != null) ? Deep(skillBg, "BasicAttack") : null;
			if (basic != null)
			{
				ClickListener(basic.gameObject).onClick.AddListener(OnAttackPressed);
			}
			if (skillBg != null)
			{
				// The wheel is two stacked pages: Tab 1 = SkillButton (1..5), Tab 2 = SkillButton (6..10).
				_tab1Root = Deep(skillBg, "Tab 1");
				_tab2Root = Deep(skillBg, "Tab 2");
				CollectTabButtons(_tab1Root, _tab1Buttons);
				CollectTabButtons(_tab2Root, _tab2Buttons);
				// Keep the flat list (all 10) for anything that iterates the whole wheel.
				_originalSkillButtons.Clear();
				_originalSkillButtons.AddRange(_tab1Buttons);
				_originalSkillButtons.AddRange(_tab2Buttons);
				// Only page 1 shows at a time — page 2's empty circles would otherwise cover it.
				_activeTab = 0;
				SetTabVisible(0);
				Transform switchTab = Deep(skillBg, "SwitchTabBTN");
				if (switchTab != null)
				{
					// The page button sits directly BELOW the central BasicAttack button and
					// shows the current page number (1/2). Reposition it there.
					PositionPageButtonBelowAttack(switchTab as RectTransform, basic as RectTransform);
					_pageLabel = Deep(switchTab, "SwitchTabText");
					UpdatePageLabel();
					ClickListener(switchTab.gameObject).onClick.AddListener(OnSwitchTabPressed);
				}
			}
			Transform walkRun = Deep(transform, "WalkRun");
			if (walkRun != null)
			{
				ClickListener(walkRun.gameObject).onClick.AddListener(OnWalkRunPressed);
			}
			Transform chatBtn = Deep(transform, "ChatBtn");
			if (chatBtn != null)
			{
				ClickListener(chatBtn.gameObject).onClick.AddListener(OnChatPressed);
			}
			Transform loot = Deep(transform, "Loot");
			if (loot != null)
			{
				ClickListener(loot.gameObject).onClick.AddListener(OnLootPressed);
			}
		}

		/// <summary>Collect a tab's SkillButton (N) children in ascending slot order and wire clicks.</summary>
		private void CollectTabButtons(Transform tabRoot, List<Transform> into)
		{
			into.Clear();
			if (tabRoot == null)
			{
				return;
			}
			for (int i = 0; i < tabRoot.childCount; i++)
			{
				Transform child = tabRoot.GetChild(i);
				if (!child.name.StartsWith("SkillButton"))
				{
					continue;
				}
				into.Add(child);
				// Capture the slot index within this tab; the global skill index is derived
				// from the visible tab at click time (see OnSkillPressed).
				int localSlot = into.Count - 1;
				Transform captured = child;
				ClickListener(captured.gameObject).onClick.AddListener(delegate ()
				{
					OnSkillSlotClicked(localSlot);
				});
			}
		}

		/// <summary>Show one wheel page and hide the other (they share the same screen slots).</summary>
		private void SetTabVisible(int tab)
		{
			if (_tab1Root != null)
			{
				_tab1Root.gameObject.SetActive(tab == 0);
			}
			if (_tab2Root != null)
			{
				_tab2Root.gameObject.SetActive(tab == 1);
			}
		}

		private void OnSwitchTabPressed()
		{
			_activeTab = (_activeTab == 0) ? 1 : 0;
			SetTabVisible(_activeTab);
			UpdatePageLabel();
			// Repaint icons/cooldowns for the newly shown page.
			OnSkillsBody();
		}

		/// <summary>
		/// Park the page-indicator button ("1"/"2") directly below the central BasicAttack
		/// button, horizontally centred on it, matching the reference wheel layout. Uses world
		/// corners (not rect units) so it stays correct under any CanvasScaler and pivot, and
		/// keeps the button on-screen.
		/// </summary>
		private static void PositionPageButtonBelowAttack(RectTransform pageBtn, RectTransform attack)
		{
			if (pageBtn == null || attack == null)
			{
				return;
			}
			// Centre the pivot so .position == the button's visual centre (fixes the skew).
			pageBtn.pivot = new Vector2(0.5f, 0.5f);

			Vector3[] ac = new Vector3[4];
			attack.GetWorldCorners(ac);
			Vector3[] pc = new Vector3[4];
			pageBtn.GetWorldCorners(pc);

			float attackCenterX = (ac[0].x + ac[2].x) * 0.5f;
			float attackCenterY = (ac[0].y + ac[2].y) * 0.5f;
			float attackHalfH = (ac[2].y - ac[0].y) * 0.5f;
			float pageHalfH = (pc[2].y - pc[0].y) * 0.5f;
			const float gap = 16f;

			Vector3 pos = pageBtn.position;
			pos.x = attackCenterX;
			pos.y = attackCenterY - attackHalfH - pageHalfH - gap;
			pageBtn.position = pos;
		}

		/// <summary>Show the current wheel page number (1-based) on the page button.</summary>
		private void UpdatePageLabel()
		{
			if (_pageLabel == null)
			{
				return;
			}
			TextMeshProUGUI t = _pageLabel.GetComponent<TextMeshProUGUI>();
			if (t != null)
			{
				t.text = (_activeTab + 1).ToString();
			}
		}

		private void WireJoystick()
		{
			Transform joy = Deep(transform, "Fixed Joystick");
			if (joy == null)
			{
				return;
			}
			Transform handle = Deep(joy, "Handle");
			RectTransform baseRt = joy as RectTransform;
			RectTransform knobRt = handle as RectTransform;
			if (knobRt != null)
			{
				Vector3 wp = knobRt.position;
				knobRt.anchorMin = new Vector2(0.5f, 0.5f);
				knobRt.anchorMax = new Vector2(0.5f, 0.5f);
				knobRt.pivot = new Vector2(0.5f, 0.5f);
				knobRt.position = wp;
			}
			WorldJoystick wj = joy.GetComponent<WorldJoystick>();
			if (wj == null)
			{
				wj = joy.gameObject.AddComponent<WorldJoystick>();
			}
			wj.BaseRect = baseRt;
			wj.KnobRect = knobRt;
			if (baseRt != null)
			{
				wj.Radius = baseRt.rect.width * 0.35f;
			}
			Image bg = joy.GetComponent<Image>();
			if (bg != null)
			{
				bg.raycastTarget = true;
			}
		}

		private void HideOwnDuplicates()
		{
			if (_hud == null)
			{
				return;
			}
			string[] names = new string[5] { "HpBar", "MpBar", "Joystick", "AttackBtn", "ChatBtn" };
			for (int i = 0; i < names.Length; i++)
			{
				Transform t = _hud.transform.Find("HUDCanvas/" + names[i]);
				if (t != null)
				{
					t.gameObject.SetActive(false);
				}
			}
		}

		private void RaiseOwnCanvas()
		{
			int max = 0;
			Canvas[] canvases = GetComponentsInChildren<Canvas>(true);
			for (int i = 0; i < canvases.Length; i++)
			{
				Canvas c = canvases[i];
				if (c.renderMode != RenderMode.ScreenSpaceOverlay)
				{
					c.renderMode = RenderMode.ScreenSpaceOverlay;
				}
				if (c.enabled && c.gameObject.activeInHierarchy && c.sortingOrder > max)
				{
					max = c.sortingOrder;
				}
			}
			if (_hud != null)
			{
				Transform own = _hud.transform.Find("HUDCanvas");
				if (own != null)
				{
					Canvas c = own.GetComponent<Canvas>();
					if (c != null)
					{
						c.sortingOrder = max + 10;
					}
				}
			}
			UnityEngine.Debug.Log("[OriginalHud] maxCanvasSort=" + max);
		}

		private void RefreshVitals()
		{
			if (_hpBar != null)
			{
				SetFill(_hpBar, (WorldRuntime.MaxHp > 0) ? ((float)WorldRuntime.Hp / WorldRuntime.MaxHp) : 0f);
			}
			if (_mpBar != null)
			{
				SetFill(_mpBar, (WorldRuntime.MaxMp > 0) ? ((float)WorldRuntime.Mp / WorldRuntime.MaxMp) : 0f);
			}
			if (_hpTxt != null)
			{
				_hpTxt.text = WorldRuntime.Hp.ToString();
			}
			if (_mpTxt != null)
			{
				_mpTxt.text = WorldRuntime.Mp.ToString();
			}
			if (_spTxt != null)
			{
				_spTxt.text = WorldRuntime.Sp.ToString();
			}
			if (_expBar != null)
			{
				float expRatio = (WorldRuntime.MaxExp > 0) ? Mathf.Clamp01((float)WorldRuntime.Exp / WorldRuntime.MaxExp) : 0f;
				SetFill(_expBar, expRatio);
			}
			if (_expTxt != null)
			{
				float expRatio = (WorldRuntime.MaxExp > 0) ? Mathf.Clamp01((float)WorldRuntime.Exp / WorldRuntime.MaxExp) : 0f;
				_expTxt.text = (expRatio * 100f).ToString("0.00") + "%";
			}
			if (_levelLabel != null)
			{
				_levelLabel.text = WorldRuntime.Level.ToString();
			}
			RefreshCoords();
		}

		private void RefreshInventory()
		{
			RefreshInventoryWindow();
			RefreshInventoryBody();
		}

		private void RefreshInventoryBody()
		{
			if (_goldLabel != null)
			{
				_goldLabel.text = WorldRuntime.Gold.ToString("N0");
			}
		}

		private void RefreshCoords()
		{
			if (_coordLabel == null || WorldRuntime.LocalPlayer == null)
			{
				return;
			}
			Vector3 p = WorldRuntime.LocalPlayer.position;
			_coordLabel.text = Mathf.RoundToInt(p.x) + " / " + Mathf.RoundToInt(p.z);
		}

		private void RefreshDate()
		{
			if (_dateLabel != null)
			{
				_dateLabel.text = System.DateTime.Now.ToString("M/d HH:mm");
			}
		}

		private void OnSkills()
		{
			RefreshSkillWindow();
			OnSkillsBody();
		}

		// Paint the server skill list straight onto the ripped wheel buttons: each button gets the
		// skill's icon plus a cooldown overlay + seconds text. The wheel is two stacked pages
		// (Tab 1 = skills 0-4, Tab 2 = skills 5-9), flipped by SwitchTabBTN. This replaces the old
		// approach of reparenting WorldHUD's own slots, which left floating names and black circles.
		private void OnSkillsBody()
		{
			// The wheel mirrors the ACTIVE Skill Set (falls back to learned order when unbound).
			List<WorldTcpClient.SkillInfo> skills = WorldRuntime.GetWheelSkills();
			PaintTabPage(_tab1Buttons, skills, 0);
			PaintTabPage(_tab2Buttons, skills, 5);
		}

		/// <summary>Paint one wheel page: slot i shows global skill (skillOffset + i), or blanks if absent.</summary>
		private void PaintTabPage(List<Transform> buttons, List<WorldTcpClient.SkillInfo> skills, int skillOffset)
		{
			for (int i = 0; i < buttons.Count; i++)
			{
				Transform btn = buttons[i];
				if (btn == null)
				{
					continue;
				}
				int skillIndex = skillOffset + i;
				WorldTcpClient.SkillInfo info = (skillIndex < skills.Count) ? skills[skillIndex] : null;
				Sprite icon = null;
				if (info != null)
				{
					string display;
					WorldRuntime.FillSkillDisplay(info, out display, out icon);
				}
				PaintSkillButton(btn, icon);
			}
		}

		/// <summary>Create/reuse a button's SkillIcon + CdOverlay children and set the icon sprite.</summary>
		private static void PaintSkillButton(Transform btn, Sprite icon)
		{
			// Icon image: reuse or create, then set the sprite (empty when no skill bound).
			Transform iconT = btn.Find("SkillIcon");
			Image iconImg;
			if (iconT == null)
			{
				GameObject go = new GameObject("SkillIcon", typeof(RectTransform));
				go.transform.SetParent(btn, false);
				RectTransform rt = (RectTransform)go.transform;
				rt.anchorMin = Vector2.zero;
				rt.anchorMax = Vector2.one;
				rt.offsetMin = Vector2.zero;
				rt.offsetMax = Vector2.zero;
				iconImg = go.AddComponent<Image>();
				iconImg.preserveAspect = true;
				iconImg.raycastTarget = false;
			}
			else
			{
				iconImg = iconT.GetComponent<Image>();
			}
			if (iconImg != null)
			{
				iconImg.sprite = icon;
				iconImg.enabled = icon != null;
			}
			// Cooldown overlay (dark fill) + seconds label, created once per button.
			Transform cdT = btn.Find("CdOverlay");
			if (cdT == null)
			{
				GameObject go = new GameObject("CdOverlay", typeof(RectTransform));
				go.transform.SetParent(btn, false);
				RectTransform rt = (RectTransform)go.transform;
				rt.anchorMin = Vector2.zero;
				rt.anchorMax = Vector2.one;
				rt.offsetMin = Vector2.zero;
				rt.offsetMax = Vector2.zero;
				Image cdImg = go.AddComponent<Image>();
				cdImg.type = Image.Type.Filled;
				cdImg.fillMethod = Image.FillMethod.Radial360;
				cdImg.fillOrigin = (int)Image.Origin360.Top;
				cdImg.color = new Color(0f, 0f, 0f, 0f);
				cdImg.raycastTarget = false;
				GameObject txtGo = new GameObject("CdText", typeof(RectTransform));
				txtGo.transform.SetParent(go.transform, false);
				RectTransform trt = (RectTransform)txtGo.transform;
				trt.anchorMin = Vector2.zero;
				trt.anchorMax = Vector2.one;
				trt.offsetMin = Vector2.zero;
				trt.offsetMax = Vector2.zero;
				TextMeshProUGUI t = txtGo.AddComponent<TextMeshProUGUI>();
				t.fontSize = 34f;
				t.color = Color.white;
				t.alignment = TextAlignmentOptions.Center;
				t.raycastTarget = false;
			}
		}

		// ---- P2: buff icon row (rebuilt on BuffsChanged, timers ticked per frame) ----

		/// <summary>Ensure the horizontal buff row exists, anchored under the vitals bars.</summary>
		private RectTransform EnsureBuffRow()
		{
			if (_buffRow != null)
			{
				return _buffRow;
			}
			GameObject go = new GameObject("BuffRow", typeof(RectTransform));
			// Parent under the bar panel so it sits with the rest of the vitals UI.
			Transform barPanel = Deep(transform, "BarPanel");
			go.transform.SetParent(barPanel != null ? barPanel : transform, false);
			_buffRow = (RectTransform)go.transform;
			_buffRow.anchorMin = new Vector2(0f, 1f);
			_buffRow.anchorMax = new Vector2(0f, 1f);
			_buffRow.pivot = new Vector2(0f, 1f);
			_buffRow.anchoredPosition = new Vector2(6f, -4f);
			HorizontalLayoutGroup lay = go.AddComponent<HorizontalLayoutGroup>();
			lay.childAlignment = TextAnchor.UpperLeft;
			lay.spacing = 4f;
			lay.padding = new RectOffset(2, 2, 2, 2);
			lay.childForceExpandWidth = false;
			lay.childForceExpandHeight = false;
			return _buffRow;
		}

		/// <summary>Rebuild the buff icons from WorldRuntime.Buffs (called on BuffsChanged).</summary>
		private void RefreshBuffs()
		{
			if (_buffRow == null && WorldRuntime.Buffs.Count == 0 && WorldRuntime.Ailments.Count == 0)
			{
				return; // nothing to show yet — don't build the row eagerly
			}
			RectTransform row = EnsureBuffRow();
			if (row == null)
			{
				return;
			}

			// Mark all existing icons for removal, then upsert the live ones.
			List<int> toRemove = new List<int>(_buffIcons.Keys);
			foreach (WorldTcpClient.BuffInfo b in WorldRuntime.Buffs.Values)
			{
				if (b == null)
				{
					continue;
				}
				Transform icon;
				TextMeshProUGUI timer;
				if (!_buffIcons.TryGetValue(b.slot, out icon))
				{
					icon = CreateBuffIcon(row, b.slot);
					_buffIcons[b.slot] = icon;
					toRemove.Remove(b.slot);
				}
				else
				{
					toRemove.Remove(b.slot);
				}
				timer = icon.GetComponentInChildren<TextMeshProUGUI>();
				if (timer != null)
				{
					timer.text = Mathf.CeilToInt(b.life) + "s";
				}
			}
			// Remove icons whose buffs expired.
			for (int i = 0; i < toRemove.Count; i++)
			{
				Transform icon;
				if (_buffIcons.TryGetValue(toRemove[i], out icon))
				{
					if (icon != null)
					{
						UnityEngine.Object.Destroy(icon.gameObject);
					}
					_buffIcons.Remove(toRemove[i]);
				}
			}
		}

		/// <summary>Create one buff icon cell (colored square + name + seconds).</summary>
		private Transform CreateBuffIcon(RectTransform row, int slot)
		{
			GameObject go = new GameObject("Buff_" + slot, typeof(RectTransform));
			go.transform.SetParent(row, false);
			RectTransform rt = (RectTransform)go.transform;
			rt.sizeDelta = new Vector2(40f, 40f);
			Image img = go.AddComponent<Image>();
			img.color = new Color(0.35f, 0.6f, 1f, 0.85f);
			img.raycastTarget = false;
			LayoutElement le = go.AddComponent<LayoutElement>();
			le.preferredWidth = 40f;
			le.preferredHeight = 40f;

			GameObject txtGo = new GameObject("Timer", typeof(RectTransform));
			txtGo.transform.SetParent(go.transform, false);
			RectTransform trt = (RectTransform)txtGo.transform;
			trt.anchorMin = Vector2.zero;
			trt.anchorMax = Vector2.one;
			trt.offsetMin = Vector2.zero;
			trt.offsetMax = Vector2.zero;
			TextMeshProUGUI t = txtGo.AddComponent<TextMeshProUGUI>();
			t.fontSize = 16f;
			t.color = Color.white;
			t.alignment = TextAlignmentOptions.Center;
			t.raycastTarget = false;
			return go.transform;
		}

		/// <summary>Per-frame countdown of buff remaining-time labels.</summary>
		private void UpdateBuffTimers()
		{
			if (_buffIcons.Count == 0)
			{
				return;
			}
			float dt = Time.deltaTime;
			bool expired = false;
			List<int> gone = null;
			foreach (WorldTcpClient.BuffInfo b in WorldRuntime.Buffs.Values)
			{
				if (b == null)
				{
					continue;
				}
				// Client-side countdown prediction; the server's BUFF_UPDATE on expiry
				// is the authoritative removal.
				b.life -= dt;
				if (b.life <= 0f)
				{
					expired = true;
					if (gone == null)
					{
						gone = new List<int>();
					}
					gone.Add(b.slot);
					continue;
				}
				Transform icon;
				if (!_buffIcons.TryGetValue(b.slot, out icon) || icon == null)
				{
					continue;
				}
				TextMeshProUGUI timer = icon.GetComponentInChildren<TextMeshProUGUI>();
				if (timer != null)
				{
					int secs = Mathf.CeilToInt(b.life);
					string txt = secs + "s";
					if (timer.text != txt)
					{
						timer.text = txt;
					}
				}
			}
			// Drop locally-expired buffs (server will confirm via BUFF_UPDATE).
			if (expired && gone != null)
			{
				for (int i = 0; i < gone.Count; i++)
				{
					WorldRuntime.Buffs.Remove(gone[i]);
					Transform icon;
					if (_buffIcons.TryGetValue(gone[i], out icon))
					{
						if (icon != null)
						{
							UnityEngine.Object.Destroy(icon.gameObject);
						}
						_buffIcons.Remove(gone[i]);
					}
				}
			}
		}

		private void OnAttackPressed()
		{
			if (_mover != null)
			{
				_mover.AttackOnce();
			}
		}

		/// <summary>Legacy direct-index skill press (kept for keyboard shortcuts).</summary>
		private void OnSkillPressed(int index)
		{
			if (_hud != null)
			{
				_hud.TryUseSkillIndex(index);
			}
		}

		/// <summary>
		/// A wheel slot was clicked. Each tab page holds 5 slots, so the global skill index is
		/// (visible tab * 5) + slot — matching how OnSkillsBody paints Skills[] across the pages.
		/// </summary>
		private void OnSkillSlotClicked(int localSlot)
		{
			int globalIndex = (_activeTab * 5) + localSlot;
			OnSkillPressed(globalIndex);
		}

		private void OnWalkRunPressed()
		{
			if (_mover == null)
			{
				return;
			}
			_mover.RunOverride = !_mover.RunOverride;
			UnityEngine.Debug.Log("[OriginalHud] run=" + _mover.RunOverride);
		}

		private void OnChatPressed()
		{
			if (_hud != null)
			{
				_hud.OpenChat();
			}
		}

		private void OnLootPressed()
		{
			if (WorldRuntime.LocalPlayer == null)
			{
				return;
			}
			WorldDropView best = null;
			float bestSqr = 64f;
			foreach (KeyValuePair<string, WorldDropView> kv in WorldRuntime.DropViews)
			{
				WorldDropView view = kv.Value;
				if (view == null)
				{
					continue;
				}
				float sqr = (view.transform.position - WorldRuntime.LocalPlayer.position).sqrMagnitude;
				if (sqr < bestSqr)
				{
					bestSqr = sqr;
					best = view;
				}
			}
			if (best != null)
			{
				best.TryPickup();
			}
		}

		private static void ColorFill(Transform bar, Color color)
		{
			Transform fillArea = Deep(bar, "Fill Area");
			Transform fill = (fillArea != null) ? Deep(fillArea, "Fill") : null;
			if (fill == null)
			{
				return;
			}
			Image img = fill.GetComponent<Image>();
			if (img != null)
			{
				img.color = color;
			}
		}

		private static void SetFill(Transform bar, float ratio)
		{
			ratio = Mathf.Clamp01(ratio);
			Slider slider = bar.GetComponent<Slider>();
			if (slider != null)
			{
				slider.value = ratio;
				return;
			}
			Transform fillArea = Deep(bar, "Fill Area");
			Transform fill = (fillArea != null) ? Deep(fillArea, "Fill") : null;
			RectTransform fillRt = fill as RectTransform;
			if (fillRt != null)
			{
				Image img = fillRt.GetComponent<Image>();
				if (img != null && img.type == Image.Type.Filled)
				{
					img.fillAmount = ratio;
					return;
				}
				RectTransform areaRt = fillArea as RectTransform;
				if (areaRt != null)
				{
					Vector2 om = fillRt.offsetMax;
					om.x = -(1f - ratio) * areaRt.rect.width;
					fillRt.offsetMax = om;
				}
			}
		}

		private static Button ClickListener(GameObject go)
		{
			Button b = go.GetComponent<Button>();
			if (b == null)
			{
				b = go.AddComponent<Button>();
				Image img = go.GetComponent<Image>();
				if (img != null)
				{
					b.targetGraphic = img;
				}
				b.transition = Selectable.Transition.None;
			}
			return b;
		}

		private static Transform Deep(Transform root, string name)
		{
			if (root == null)
			{
				return null;
			}
			Transform[] all = root.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < all.Length; i++)
			{
				if (all[i].name == name)
				{
					return all[i];
				}
			}
			return null;
		}

		private static TextMeshProUGUI ChildText(Transform root, string name)
		{
			Transform t = Deep(root, name);
			return (t != null) ? t.GetComponent<TextMeshProUGUI>() : null;
		}

		private TextMeshProUGUI Prefer(TextMeshProUGUI wired, string name)
		{
			if (wired != null)
			{
				return wired;
			}
			return ChildText(transform, name);
		}
	}
}
