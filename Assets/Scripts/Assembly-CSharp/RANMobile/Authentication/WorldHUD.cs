using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RANMobile.Authentication
{
	/// <summary>
	/// HEADLESS world service. The ripped UICanvasGameplay HUD (driven by
	/// OriginalHudDriver) owns every persistent widget — bars, joystick, skill
	/// wheel, buttons and windows. WorldHUD only supplies transient feedback
	/// (toasts, floating damage numbers), the chat input, and the skill-use
	/// service that the ripped wheel/buttons call into.
	/// </summary>
	public class WorldHUD : MonoBehaviour
	{
		public static WorldHUD Instance;

		public static Vector2 MoveAxis;

		public static bool ChatOpen;

		private Camera _cam;

		private Transform _player;

		private WorldPlayerMover _mover;

		private Canvas _canvas;

		private RectTransform _canvasRect;

		private TextMeshProUGUI _toast;

		private float _toastUntil;

		private GameObject _chatInputGo;

		private TMP_InputField _chatInput;

		private readonly Dictionary<string, float> _readyAt = new Dictionary<string, float>();

		public void Init(Transform player, WorldPlayerMover mover, Camera cam)
		{
			Instance = this;
			_player = player;
			_mover = mover;
			_cam = cam;
			// WorldHUD is now a HEADLESS service: the ripped UICanvasGameplay HUD (driven by
			// OriginalHudDriver) owns all persistent widgets (bars, joystick, skill wheel, buttons).
			// We only build a canvas for transient feedback — toasts and floating damage numbers.
			BuildCanvas();
			BuildToast();
			Debug.Log("[WorldHUD] built (headless service)");
		}

		private void OnEnable()
		{
			Instance = this;
			WorldRuntime.SkillResultOccurred += OnSkillResult;
			WorldNet.ChatLine += OnChatLine;
		}

		private void OnDisable()
		{
			WorldRuntime.SkillResultOccurred -= OnSkillResult;
			WorldNet.ChatLine -= OnChatLine;
			if (Instance == this)
			{
				Instance = null;
			}
		}

		private void Update()
		{
			float now = Time.unscaledTime;
			if (_toast != null && _toastUntil > 0f && now > _toastUntil)
			{
				_toast.text = string.Empty;
				_toastUntil = 0f;
			}
			if (ChatOpen && Input.GetKeyDown(KeyCode.Return) && _chatInput != null)
			{
				SendChat();
			}
			if (ChatOpen && Input.GetKeyDown(KeyCode.Escape))
			{
				CloseChat();
			}
			if (!ChatOpen && Input.GetMouseButtonDown(0) && EventSystem.current != null && !EventSystem.current.IsPointerOverGameObject() && _cam != null)
			{
				RaycastHit hit;
				if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit, 200f))
				{
					// P5: NPC talk takes priority over drops on the same click
					WorldNpcView npc = hit.collider.GetComponentInParent<WorldNpcView>();
					if (npc != null)
					{
						WorldRuntime.OpenNpcTalk(npc.NpcId);
						return;
					}
					WorldDropView drop = hit.collider.GetComponentInParent<WorldDropView>();
					if (drop != null)
					{
						drop.TryPickup();
					}
				}
			}
		}


		// ---- construction ----

		private void BuildCanvas()
		{
			GameObject go = new GameObject("HUDCanvas");
			go.transform.SetParent(transform, false);
			_canvas = go.AddComponent<Canvas>();
			_canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			_canvas.sortingOrder = 500;
			CanvasScaler scaler = go.AddComponent<CanvasScaler>();
			scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			scaler.referenceResolution = new Vector2(1920f, 1080f);
			scaler.matchWidthOrHeight = 0.5f;
			go.AddComponent<GraphicRaycaster>();
			_canvasRect = go.GetComponent<RectTransform>();
		}

		private static RectTransform Rect(string name, Transform parent, Vector2 anchor, Vector2 pivot, Vector2 pos, Vector2 size)
		{
			GameObject go = new GameObject(name);
			RectTransform rt = go.AddComponent<RectTransform>();
			rt.SetParent(parent, false);
			rt.anchorMin = anchor;
			rt.anchorMax = anchor;
			rt.pivot = pivot;
			rt.anchoredPosition = pos;
			rt.sizeDelta = size;
			return rt;
		}

		private static Image Img(RectTransform rt, Color color)
		{
			Image img = rt.gameObject.AddComponent<Image>();
			img.color = color;
			img.raycastTarget = false;
			return img;
		}

		private static TextMeshProUGUI Txt(RectTransform rt, string text, float size, Color color, TextAlignmentOptions align)
		{
			TextMeshProUGUI t = rt.gameObject.AddComponent<TextMeshProUGUI>();
			t.text = text;
			t.fontSize = size;
			t.color = color;
			t.alignment = align;
			t.raycastTarget = false;
			t.overflowMode = TextOverflowModes.Overflow;
			return t;
		}

		/// <summary>Full-rect child used for labels that must sit on a parent that already has a Graphic.</summary>
		private static RectTransform Child(RectTransform parent)
		{
			RectTransform rt = Rect("Label", parent, Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero);
			rt.anchorMin = Vector2.zero;
			rt.anchorMax = Vector2.one;
			return rt;
		}

		/// <summary>
		/// Force refresh the skill list display. Call this after skills are received from server.
		/// </summary>
		private void BuildToast()
		{
			RectTransform rt = Rect("Toast", _canvasRect, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -150f), new Vector2(1000f, 60f));
			_toast = Txt(rt, "", 32f, new Color(1f, 1f, 0.75f), TextAlignmentOptions.Center);
		}

		// ---- runtime ----

		public void ShowToast(string text)
		{
			if (_toast == null)
			{
				return;
			}
			_toast.text = text;
			_toastUntil = Time.unscaledTime + 2.4f;
			Debug.Log("[WorldHUD] toast: " + text);
		}

		/// <summary>
		/// Incoming chat line. The persistent chat log lives in the ripped HUD's
		/// _ChatSectionCanvas (wired separately); here we only surface it transiently.
		/// </summary>
		private void OnChatLine(string name, string text)
		{
			Debug.Log("[WorldCHAT] " + name + ": " + text);
		}

		/// <summary>
		/// Open the chat input. Built lazily so WorldHUD stays headless until the
		/// player actually asks to chat (the input is transient, not persistent chrome).
		/// </summary>
		public void OpenChat()
		{
			EnsureChatInput();
			if (_chatInputGo == null)
			{
				return;
			}
			ChatOpen = true;
			_chatInputGo.SetActive(true);
			if (_chatInput != null)
			{
				_chatInput.text = string.Empty;
				_chatInput.ActivateInputField();
			}
		}

		/// <summary>Create the one-off chat input bar on first use.</summary>
		private void EnsureChatInput()
		{
			if (_chatInputGo != null || _canvasRect == null)
			{
				return;
			}
			RectTransform inputBg = Rect("ChatInput", _canvasRect, new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(40f, 560f), new Vector2(620f, 54f));
			Img(inputBg, new Color(0f, 0f, 0f, 0.7f));
			_chatInputGo = inputBg.gameObject;

			RectTransform textRt = Rect("Text", inputBg, new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(10f, 0f), new Vector2(470f, 44f));
			textRt.anchorMin = Vector2.zero;
			textRt.anchorMax = new Vector2(0f, 1f);
			TextMeshProUGUI inputText = Txt(textRt, "", 24f, Color.white, TextAlignmentOptions.MidlineLeft);

			RectTransform phRt = Rect("Placeholder", inputBg, new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(10f, 0f), new Vector2(470f, 44f));
			phRt.anchorMin = Vector2.zero;
			phRt.anchorMax = new Vector2(0f, 1f);
			TextMeshProUGUI placeholder = Txt(phRt, "Say something...", 24f, new Color(1f, 1f, 1f, 0.4f), TextAlignmentOptions.MidlineLeft);

			_chatInput = inputBg.gameObject.AddComponent<TMP_InputField>();
			_chatInput.textComponent = inputText;
			_chatInput.placeholder = placeholder;
			_chatInput.targetGraphic = inputBg.GetComponent<Image>();
			_chatInput.characterLimit = 200;

			RectTransform sendRt = Rect("Send", inputBg, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(-8f, 0f), new Vector2(120f, 46f));
			Image sendImg = Img(sendRt, new Color(0.2f, 0.5f, 0.25f, 0.95f));
			sendImg.raycastTarget = true;
			Txt(Child(sendRt), "Send", 24f, Color.white, TextAlignmentOptions.Center);
			Button sendBtn = sendRt.gameObject.AddComponent<Button>();
			sendBtn.targetGraphic = sendImg;
			sendBtn.transition = Selectable.Transition.None;
			sendBtn.onClick.AddListener(SendChat);

			_chatInputGo.SetActive(false);
		}

		private void CloseChat()
		{
			ChatOpen = false;
			if (_chatInputGo != null)
			{
				_chatInputGo.SetActive(false);
			}
		}

		private void SendChat()
		{
			if (_chatInput == null)
			{
				return;
			}
			string text = _chatInput.text;
			_chatInput.text = string.Empty;
			WorldTcpClient client = WorldNet.Client;
			if (client != null && !string.IsNullOrEmpty(text))
			{
				client.SendChat(text);
			}
			CloseChat();
		}

		public void UseSkill(WorldTcpClient.SkillInfo info)
		{
			if (WorldRuntime.LocalPlayer == null || info == null)
			{
				return;
			}
			WorldTcpClient client = WorldNet.Client;
			string skillKey = info.mainId + "_" + info.subId;
			float ready;
			if (_readyAt.TryGetValue(skillKey, out ready) && Time.unscaledTime < ready)
			{
				ShowToast("Cooling down...");
				return;
			}

			// Weapon gate — the original refuses a skill unless the held weapon's attack type
			// matches the skill's requirement (CHECHSKILL_ITEM over the right-hand slot).
			// Passive / NOCARE skills need no weapon.
			SkillObject soGate = SkillMenuData.Get(info.mainId, info.subId);
			if (soGate != null && !WeaponMatchesSkill(soGate.UseRightItem))
			{
				ShowToast("Equip your class weapon first");
				return;
			}

			string target = null;
			WorldMobView skillMob = null;
			if (info.type != "heal")
			{
				float range = (info.range > 0) ? info.range : 30f;
				skillMob = WorldRuntime.NearestMob(WorldRuntime.LocalPlayer.position, range);
				if (skillMob == null)
				{
					ShowToast("No target in range");
					return;
				}
				target = skillMob.Id;
			}
			_readyAt[skillKey] = Time.unscaledTime + info.cooldownMs / 1000f;

			// Cast animation: the class animator carries a per-skill state named by the
			// skill's animationKey (e.g. "dex1"), so play it on the local player the moment
			// the skill fires (before local/server damage resolution).
			PlaySkillAnimation(info);

			// Local-authoritative mob: resolve the skill here (works offline too).
			bool resolvedLocally = false;
			if (skillMob != null)
			{
				WorldTcpClient.MobInfo minfo;
				if (WorldRuntime.MobInfos.TryGetValue(skillMob.Id, out minfo) && minfo.local)
				{
					int dmg = RollSkillDamage(info);
					WorldRuntime.ApplyLocalMobDamage(skillMob.Id, dmg, skillMob.transform.position + Vector3.up * 12f);
					resolvedLocally = true;
				}
			}
			// Still tell the server when we have a connection (keeps remote state consistent).
			if (client != null && !resolvedLocally)
			{
				client.SendSkillUse(info.mainId, info.subId, target);
			}
			else if (client == null && !resolvedLocally && info.type != "heal")
			{
				ShowToast("No connection");
			}
		}

		/// <summary>
		/// Play the cast animation for a skill on the local player. Each class animator has a
		/// state per skill named by that skill's animationKey (e.g. "dex1"); the key comes from
		/// the client-side skill asset, so no server round-trip is needed.
		/// </summary>
		private static void PlaySkillAnimation(WorldTcpClient.SkillInfo info)
		{
			if (WorldRuntime.LocalPlayer == null || info == null)
			{
				return;
			}
			SkillObject so = SkillMenuData.Get(info.mainId, info.subId);
			if (so == null || string.IsNullOrEmpty(so.AnimationKey))
			{
				return;
			}
			// Use the same animator the basic-attack combo drives (WorldPlayerMover is the
			// local, input-driven player), with component-walk fallbacks for other layouts.
			Animator pa = null;
			WorldPlayerMover mover = UnityEngine.Object.FindFirstObjectByType<WorldPlayerMover>();
			if (mover != null)
			{
				pa = mover.animator;
			}
			if (pa == null)
			{
				pa = WorldRuntime.LocalPlayer.GetComponentInChildren<Animator>(true);
			}
			if (pa == null)
			{
				pa = WorldRuntime.LocalPlayer.GetComponentInParent<Animator>();
			}
			if (pa != null)
			{
				pa.Play(so.AnimationKey);
			}
			// Cast one-shot for the class's weapon swing (the original pairs the cast with a sound).
			AudioManager.PlaySfx(WorldRuntime.ClassAttackSfx(WorldRuntime.ClassId));
		}

		/// <summary>
		/// The original's CHECHSKILL_ITEM: does an equipped right-hand weapon satisfy the skill's
		/// required weapon? The two sides use different enums — the skill side carries SWORDBLADE,
		/// which the item side lacks, so the non-sword skill values sit one higher than the item
		/// values — map explicitly. NOCARE/NOTHING skills need no weapon.
		/// </summary>
		private static bool WeaponMatchesSkill(Core.Enumerators.SkillAttackTypeEnum need)
		{
			Core.Enumerators.ItemAttackType w1;
			Core.Enumerators.ItemAttackType w2;
			bool dual = false;
			switch (need)
			{
				case Core.Enumerators.SkillAttackTypeEnum.SKILLATT_SWORD:
					w1 = Core.Enumerators.ItemAttackType.Sword; w2 = w1; break;
				case Core.Enumerators.SkillAttackTypeEnum.SKILLATT_BLADE:
					w1 = Core.Enumerators.ItemAttackType.Blade; w2 = w1; break;
				case Core.Enumerators.SkillAttackTypeEnum.SKILLATT_SWORDBLADE:
					w1 = Core.Enumerators.ItemAttackType.Sword;
					w2 = Core.Enumerators.ItemAttackType.Blade;
					dual = true;
					break;
				case Core.Enumerators.SkillAttackTypeEnum.SKILLATT_DAGGER:
					w1 = Core.Enumerators.ItemAttackType.Dagger; w2 = w1; break;
				case Core.Enumerators.SkillAttackTypeEnum.SKILLATT_SPEAR:
					w1 = Core.Enumerators.ItemAttackType.Spear; w2 = w1; break;
				case Core.Enumerators.SkillAttackTypeEnum.SKILLATT_STICK:
					w1 = Core.Enumerators.ItemAttackType.Stick; w2 = w1; break;
				case Core.Enumerators.SkillAttackTypeEnum.SKILLATT_GAUNT:
					w1 = Core.Enumerators.ItemAttackType.Gaunt; w2 = w1; break;
				case Core.Enumerators.SkillAttackTypeEnum.SKILLATT_BOW:
					w1 = Core.Enumerators.ItemAttackType.Bow; w2 = w1; break;
				default:
					return true; // NOCARE / NOTHING / unknown -> no weapon required
			}
			for (int i = 0; i < WorldRuntime.Equipped.Count; i++)
			{
				WorldTcpClient.InventoryItemInfo it = WorldRuntime.Equipped[i];
				ItemObject io;
				if (!CharacterStatBlock.TryGetItem(it.mainId, it.subId, out io) || io == null)
				{
					continue;
				}
				if (io.wearPosition != Core.Enumerators.WearPositionEnum.Handheld)
				{
					continue; // only the held (main-hand) weapon counts
				}
				if (io.attackType == w1 || (dual && io.attackType == w2))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Skill damage — the original CALCDAMAGE skill branch: base AP range + skill power
		/// var + weapon-grade bonus − element resist, rolled through DamageCalculator's real
		/// crit/defense math. Magic-type skills ignore defense entirely (handled inside).
		/// </summary>
		private static int RollSkillDamage(WorldTcpClient.SkillInfo info)
		{
			DerivedCharacterStats d = WorldRuntime.Derived;
			DefaultCharsetObject consts = CharacterStatBlock.Constants;

			DamageCalculator.AttackContext ctx = new DamageCalculator.AttackContext();
			ctx.Level = WorldRuntime.Level;
			ctx.HpPercent = (WorldRuntime.MaxHp > 0) ? (WorldRuntime.Hp * 100 / WorldRuntime.MaxHp) : 100;
			// Skill path seeds from the AP range; PA/SA/item damage are NOT re-added here
			// (the original adds them only in the non-skill branch).
			ctx.DamageLow = d.DamageLow;
			ctx.DamageHigh = d.DamageHigh;
			ctx.IsSkill = true;
			// fBASIC_VAR analogues: skill power scaled by its level (the original stores
			// the negative damage value directly in sDATA_LVL[].fBASIC_VAR).
			int power = Mathf.Max(1, info.power);
			int lvl = Mathf.Max(1, info.skillLevel);
			ctx.SkillVar = power + lvl * 6;
			// Magic if the skill scales with MA (shaman-type); otherwise physical resistG.
			ctx.IsMagic = (d.Ma > 0 && d.Pa <= d.Ma);
			ctx.ResistG = (ctx.IsMagic && consts != null) ? consts.resistG : ((consts != null) ? consts.resistPhysicG : 0.1f);
			// No weapon grade until grinding is wired to local gear — grade 0 = no bonus.
			ctx.Grade = 0;
			ctx.DamageRate = 1f;
			ctx.StrikeCount = 1;

			DamageTarget target = new DamageTarget();
			target.Level = WorldRuntime.Level;
			// Skills in this client currently target self/mobs; use a soft defense so
			// player-vs-mob skills aren't nulled (mob defense is added properly in P3).
			target.Defense = 0;
			target.BodyDefense = 0;
			target.ItemDefense = 0;
			target.Resist = 0;
			target.HpPercent = 100;

			DamageFlags flags;
			return DamageCalculator.ComputeDamage(ctx, target, out flags);
		}

		/// <summary>
		/// Use the skill in the Nth hotbar slot. The slot order comes straight from the
		/// server skill list (WorldRuntime.Skills), which is also what OriginalHudDriver
		/// paints into the ripped skill wheel — so wheel index N == Skills[N].
		/// </summary>
		/// <summary>Remaining cooldown in seconds for a skill (0 = ready). Used by the ripped wheel overlays.</summary>
		public float CooldownRemaining(int mainId, int subId)
		{
			float ready;
			if (_readyAt.TryGetValue(mainId + "_" + subId, out ready))
			{
				float remain = ready - Time.unscaledTime;
				return remain > 0f ? remain : 0f;
			}
			return 0f;
		}

		public void TryUseSkillIndex(int index)
		{
			if (index >= 0 && index < WorldRuntime.Skills.Count)
			{
				UseSkill(WorldRuntime.Skills[index]);
			}
		}

		private void OnSkillResult(WorldTcpClient.SkillResult r)
		{
			if (r == null)
			{
				return;
			}
			string selfId = (WorldNet.Client != null) ? WorldNet.Client.SelfId : null;
			bool mine = selfId != null && r.caster != null && r.caster == selfId;
			if (!r.ok)
			{
				if (mine)
				{
					if (r.mainId > 0 && (r.reason == "cooldown" || r.reason == "mp" || r.reason == "sp"))
					{
						_readyAt.Remove(r.mainId + "_" + r.subId);
					}
					if (r.reason == "insufficient_skill_points")
					{
						ShowToast("Need " + r.needed + " SP, have " + r.have);
					}
					else if (r.reason == "max_level")
					{
						ShowToast("Skill at max level");
					}
					else if (r.reason == "skill_not_learned")
					{
						ShowToast("Skill not learned");
					}
					else if (r.reason == "class_restricted")
					{
						ShowToast("Class restricted (grade " + r.grade + ")");
					}
					else
					{
						ShowToast(ReasonText(r.reason));
					}
				}
				return;
			}
			if (r.reason == "levelup")
			{
				// Local-authority level-up (GainExp reuses the skill-result channel).
				ShowToast("Level up! Now level " + r.skillLevel);
				SpawnPopup(PopupPos(null), "LEVEL UP!", new Color(1f, 0.85f, 0.3f));
				return;
			}
			if (mine && r.heal > 0 && r.target == selfId)
			{
				SpawnPopup(PopupPos(r.target), "+" + r.heal, new Color(0.4f, 1f, 0.5f));
				return;
			}
			if (mine && r.damage > 0)
			{
				SpawnPopup(PopupPos(r.target), "-" + r.damage, r.killed ? new Color(1f, 0.45f, 0.2f) : Color.white);
				if (r.killed && r.exp > 0)
				{
					ShowToast("+" + r.exp + " exp");
				}
			}
			if (mine && r.skillLevel > 0)
			{
				ShowToast("Skill leveled to " + r.skillLevel + "! SP remaining: " + (r.casterMaxSp >= 0 ? "updated" : "-"));
			}
		}

		private static string ReasonText(string reason)
		{
			switch (reason)
			{
				case "cooldown":
					return "Skill cooling down";
				case "mp":
					return "Not enough MP";
				case "out_of_range":
					return "Out of range";
				case "no_target":
					return "No target";
				case "too_far":
					return "Too far away";
				case "gone":
					return "Already taken";
				case "unknown_skill":
					return "Unknown skill";
				case "insufficient_level":
					return "Your level is too low";
				case "stat_requirement":
					return "Stats too low";
				case "requires_skill":
					return "Prerequisite skill required";
				case "already_learned":
					return "Already learned";
				case "wrong_class":
					return "Wrong class for this skill";
				default:
					return string.IsNullOrEmpty(reason) ? "Action failed" : reason;
			}
		}

		private Vector3 PopupPos(string targetId)
		{
			if (string.IsNullOrEmpty(targetId))
			{
				return _player != null ? _player.position + Vector3.up * 16f : Vector3.zero;
			}
			WorldMobView mob;
			if (WorldRuntime.MobViews.TryGetValue(targetId, out mob) && mob != null)
			{
				return mob.transform.position + Vector3.up * 16f;
			}
			if (WorldRuntime.LocalPlayer != null && WorldNet.Client != null && targetId == WorldNet.Client.SelfId)
			{
				return WorldRuntime.LocalPlayer.position + Vector3.up * 16f;
			}
			WorldNet.RemoteState state;
			if (WorldNet.Remotes.TryGetValue(targetId, out state) && state.avatar != null)
			{
				return state.avatar.transform.position + Vector3.up * 16f;
			}
			return _player != null ? _player.position + Vector3.up * 16f : Vector3.zero;
		}

		public void SpawnPopup(Vector3 worldPos, string text, Color color)
		{
			if (_canvas == null || _cam == null)
			{
				return;
			}
			Vector2 screen = _cam.WorldToScreenPoint(worldPos);
			Vector2 local;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, screen, null, out local))
			{
				return;
			}
			RectTransform rt = Rect("Popup", _canvasRect, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), local, new Vector2(300f, 56f));
			TextMeshProUGUI t = Txt(rt, text, 40f, color, TextAlignmentOptions.Center);
			t.fontStyle = FontStyles.Bold;
			StartCoroutine(PopupAnim(rt, t));
		}

		private IEnumerator PopupAnim(RectTransform rt, TextMeshProUGUI text)
		{
			Vector2 start = rt.anchoredPosition;
			float t = 0f;
			while (t < 0.9f)
			{
				t += Time.unscaledDeltaTime;
				float k = t / 0.9f;
				rt.anchoredPosition = start + new Vector2(0f, k * 70f);
				Color c = text.color;
				c.a = 1f - Mathf.SmoothStep(0f, 1f, Mathf.Max(0f, (k - 0.4f) / 0.6f));
				text.color = c;
				yield return null;
			}
			Destroy(rt.gameObject);
		}
	}
}
