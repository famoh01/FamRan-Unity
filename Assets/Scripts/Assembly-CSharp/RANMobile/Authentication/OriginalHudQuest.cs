using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RANMobile.Authentication
{
	/// <summary>
	/// P5 quest UI (partial OriginalHudDriver): the server-driven NPC talk dialog, the
	/// quest log window, the T-key shortcut, the original accept-chat lines and the
	/// Arrow counter. Quest DEFINITIONS come from the ripped client's own QuestDatabase
	/// (621 assets, Resources/database/QuestDatabase); STATE is server-authoritative
	/// (WorldRuntime.QuestActive/QuestEnded).
	///
	/// The radial "QuestBtn" opens the log (the ripped empty _Mission Canvas is hidden
	/// so it doesn't flash behind it). Talking to an NPC (WorldNpcView click) asks the
	/// server for a dialog; answers are sent back and the server advances/accepts.
	/// DebugShowQuestLog / DebugTourQuests / DebugRealClickSkillTab are test hooks.
	/// </summary>
	public partial class OriginalHudDriver
	{
		private GameObject _questLogGo;

		private Transform _questPanel;

		private GameObject _talkGo;

		private Transform _talkPanel;

		private string _talkNpcId;

		private int _talkStatementId;

		private readonly Dictionary<int, QuestObject> _questDefs = new Dictionary<int, QuestObject>();

		private bool _questDefsLoaded;

		private bool _questLogOpen;

		private int _questDetailId = -1;

		private bool _questWired;

		private bool _questSyncSeeded;

		private readonly HashSet<int> _seenActiveQuests = new HashSet<int>();

		private float _nextArrowAt;

		private TextMeshProUGUI _arrowLabel;

		private WorldTcpClient.TalkDialogInfo _lastTalk;

		private Coroutine _debugTour;

		// ---------------------------------------------------------------- wiring

		private void EnsureQuestUi()
		{
			if (_questWired)
			{
				return;
			}
			_questWired = true;
			Transform questBtn = Deep(transform, "QuestBtn");
			if (questBtn != null)
			{
				ClickListener(questBtn.gameObject).onClick.AddListener(ToggleQuestLog);
			}
			else
			{
				UnityEngine.Debug.LogWarning("[QuestUI] QuestBtn not found");
			}
			WorldRuntime.QuestsChanged += OnQuestsChangedUi;
		}

		private void LoadQuestDefs()
		{
			if (_questDefsLoaded)
			{
				return;
			}
			QuestDatabaseObject db = Resources.Load<QuestDatabaseObject>("database/QuestDatabase");
			if (db == null || db.questObject == null)
			{
				UnityEngine.Debug.LogWarning("[QuestUI] QuestDatabase missing");
				_questDefsLoaded = true;
				return;
			}
			for (int i = 0; i < db.questObject.Length; i++)
			{
				QuestObject q = db.questObject[i];
				if (q != null && q.data != null && !_questDefs.ContainsKey(q.data.questId))
				{
					_questDefs[q.data.questId] = q;
				}
			}
			UnityEngine.Debug.Log("[QuestUI] defs loaded: " + _questDefs.Count);
			_questDefsLoaded = true;
		}

		private void TickQuestUi(float now)
		{
			WorldTcpClient.TalkDialogInfo dlg;
			if (WorldRuntime.TryTakeTalkDialog(out dlg) && dlg != null)
			{
				_lastTalk = dlg;
				ShowTalkDialog(dlg);
			}
			// original client: T opens/closes the quest window (chat typing swallows it)
			if (!WorldHUD.ChatOpen && Input.GetKeyDown(KeyCode.T))
			{
				ToggleQuestLog();
			}
			// original "Arrow : 135" counter (ripped arrowCount node, was hidden)
			if (now - _nextArrowAt >= 0.5f)
			{
				_nextArrowAt = now + 0.5f;
				RefreshArrowCount();
			}
		}

		private void RefreshArrowCount()
		{
			if (_arrowLabel == null)
			{
				Transform t = transform.Find("In-Game UI/_SkillButtons/_MiniChatCanvas/arrowCount");
				if (t == null)
				{
					return;
				}
				_arrowLabel = t.GetComponent<TextMeshProUGUI>();
				if (_arrowLabel == null)
				{
					_arrowLabel = t.GetComponentInChildren<TextMeshProUGUI>(true);
				}
				if (_arrowLabel == null)
				{
					return;
				}
			}
			int arrows = 0;
			for (int i = 0; i < WorldRuntime.Items.Count; i++)
			{
				WorldTcpClient.InventoryItemInfo it = WorldRuntime.Items[i];
				ItemObject io;
				if (CharacterStatBlock.TryGetItem(it.mainId, it.subId, out io) && io != null && (int)io.type == 12)
				{
					arrows += it.count;
				}
			}
			for (int i = 0; i < WorldRuntime.Equipped.Count; i++)
			{
				WorldTcpClient.InventoryItemInfo it = WorldRuntime.Equipped[i];
				ItemObject io;
				if (CharacterStatBlock.TryGetItem(it.mainId, it.subId, out io) && io != null && (int)io.type == 12)
				{
					arrows += it.count;
				}
			}
			_arrowLabel.text = "Arrow : " + arrows;
			_arrowLabel.gameObject.SetActive(true);
		}

		private void OnQuestsChangedUi()
		{
			// original chat lines on accept: "Quest 'X' is running now. / Please open
			// the quest window (shortcut key T) to confirm." — seed silently on the
			// first (login) sync so persisted quests don't spam.
			if (!_questSyncSeeded)
			{
				_questSyncSeeded = true;
				foreach (int qid in WorldRuntime.QuestActive.Keys)
				{
					_seenActiveQuests.Add(qid);
				}
			}
			else
			{
				foreach (int qid in WorldRuntime.QuestActive.Keys)
				{
					if (_seenActiveQuests.Add(qid))
					{
						QuestObject def;
						_questDefs.TryGetValue(qid, out def);
						string title = (def != null) ? def.questTitle : ("Quest " + qid);
						WorldNet.FireChatLine("", "Quest '" + title + "' is running now.");
						WorldNet.FireChatLine("", "Please open the quest window (shortcut key T) to confirm.");
					}
				}
				foreach (int qid in new List<int>(_seenActiveQuests))
				{
					if (!WorldRuntime.QuestActive.ContainsKey(qid))
					{
						_seenActiveQuests.Remove(qid);
					}
				}
			}
			if (_questLogOpen)
			{
				BuildQuestLogBody();
			}
		}

		// ---------------------------------------------------------------- quest log

		private void ToggleQuestLog()
		{
			SetQuestLogOpen(!_questLogOpen);
		}

		/// <summary>Test hook: idempotent open (immune to double-invokes).</summary>
		private void DebugShowQuestLog()
		{
			SetQuestLogOpen(true);
		}

		private void SetQuestLogOpen(bool open)
		{
			EnsureQuestUi();
			// the ripped QuestBtn also enables the empty "_Mission Canvas" — hide it
			Transform mission = Deep(transform, "_Mission Canvas");
			if (mission != null)
			{
				Canvas c = mission.GetComponent<Canvas>();
				if (c != null)
				{
					c.enabled = false;
				}
				mission.gameObject.SetActive(false);
			}
			if (_questLogGo == null)
			{
				BuildQuestLogWindow();
			}
			_questLogOpen = open;
			_questLogGo.SetActive(open);
			UnityEngine.Debug.Log("[QuestUI] SetQuestLogOpen " + open + " (rows=" +
				(_questPanel != null ? _questPanel.childCount : -1) + ")");
			if (open)
			{
				BuildQuestLogBody();
			}
		}

		private void BuildQuestLogWindow()
		{
			// Host under "OverlayPanels": its canvas is ALWAYS enabled (the "Skills Canvas"
			// canvas is disabled in the prefab and suppresses its whole subtree — nested
			// canvases included — so my window would never render there). The window GO
			// carries its own sorting canvas; content lives in a fixed panel child so a
			// screen-driven rect can't stretch it.
			Transform host = transform.Find("In-Game UI/OverlayPanels");
			if (host == null)
			{
				host = transform;
			}
			GameObject go = new GameObject("QuestLogWindow", typeof(RectTransform));
			go.transform.SetParent(host, false);
			Canvas ownCanvas = go.AddComponent<Canvas>();
			ownCanvas.overrideSorting = true;
			ownCanvas.sortingOrder = 710;
			go.AddComponent<GraphicRaycaster>();
			_questLogGo = go;

			GameObject panel = new GameObject("Panel", typeof(RectTransform));
			panel.transform.SetParent(go.transform, false);
			RectTransform prt = (RectTransform)panel.transform;
			prt.anchorMin = new Vector2(0.5f, 0.5f);
			prt.anchorMax = new Vector2(0.5f, 0.5f);
			prt.pivot = new Vector2(0.5f, 0.5f);
			prt.sizeDelta = new Vector2(900f, 820f);
			prt.anchoredPosition = Vector2.zero;
			Image pbg = panel.AddComponent<Image>();
			pbg.color = new Color(0.03f, 0.05f, 0.09f, 0.96f);
			pbg.raycastTarget = true;
			BoxOutline(prt, new Color(0.55f, 0.62f, 0.75f, 0.9f));
			_questPanel = panel.transform;

			TextMeshProUGUI title = MakeText(panel.transform, "Title", 36f, TextAlignmentOptions.Center, new Color(0.45f, 1f, 0.55f));
			((RectTransform)title.transform).anchorMin = new Vector2(0f, 1f);
			((RectTransform)title.transform).anchorMax = new Vector2(1f, 1f);
			((RectTransform)title.transform).pivot = new Vector2(0.5f, 1f);
			((RectTransform)title.transform).sizeDelta = new Vector2(0f, 54f);
			((RectTransform)title.transform).anchoredPosition = new Vector2(0f, -10f);
			title.text = "Quests";

			GameObject closeGo = new GameObject("Close", typeof(RectTransform));
			closeGo.transform.SetParent(panel.transform, false);
			BuildQuestButton((RectTransform)closeGo.transform, new Vector2(0.5f, 0f), new Vector2(200f, 52f), new Vector2(0f, 12f), "Close", delegate { ToggleQuestLog(); });
		}

		private void BuildQuestLogBody()
		{
			Transform p = _questPanel;
			for (int i = p.childCount - 1; i >= 0; i--)
			{
				Transform c = p.GetChild(i);
				if (c.name == "Title" || c.name == "Close")
				{
					continue;
				}
				if (c.name.StartsWith("QRow_") || c.name.StartsWith("CDetail") || c.name == "NoQuests")
				{
					Destroy(c.gameObject);
				}
			}
			float y = -72f;
			int row = 0;
			foreach (KeyValuePair<int, int[]> kv in WorldRuntime.QuestActive)
			{
				int qid = kv.Key;
				int step = kv.Value[0];
				QuestObject def;
				_questDefs.TryGetValue(qid, out def);
				string titleTxt = (def != null) ? def.questTitle : ("Quest " + qid);
				string objText = QuestObjectiveText(def, step);
				GameObject rowGo = new GameObject("QRow_" + qid, typeof(RectTransform));
				rowGo.transform.SetParent(p, false);
				BuildQuestRow((RectTransform)rowGo.transform, y, titleTxt, objText, qid == _questDetailId);
				int captured = qid;
				ClickListener(rowGo).onClick.AddListener(delegate
				{
					_questDetailId = captured;
					BuildQuestLogBody();
				});
				y -= 74f;
				row++;
			}
			// completed section
			if (WorldRuntime.QuestEnded.Count > 0)
			{
				TextMeshProUGUI hdr = MakeText(p, "CDetailHdr", 26f, TextAlignmentOptions.Left, new Color(0.45f, 0.7f, 1f));
				PlaceBlockT(hdr.rectTransform, y, 32f);
				hdr.text = "Completed";
				y -= 36f;
				int shown = 0;
				foreach (int qid in WorldRuntime.QuestEnded)
				{
					QuestObject def;
					_questDefs.TryGetValue(qid, out def);
					TextMeshProUGUI t = MakeText(p, "CDetail" + qid, 22f, TextAlignmentOptions.Left, new Color(0.5f, 0.85f, 0.55f, 0.85f));
					((RectTransform)t.transform).anchorMin = new Vector2(0f, 1f);
					((RectTransform)t.transform).anchorMax = new Vector2(1f, 1f);
					((RectTransform)t.transform).pivot = new Vector2(0f, 1f);
					((RectTransform)t.transform).sizeDelta = new Vector2(0f, 28f);
					((RectTransform)t.transform).anchoredPosition = new Vector2(36f, y);
					t.text = "[Done] " + ((def != null) ? def.questTitle : ("Quest " + qid));
					y -= 30f;
					shown++;
					if (shown >= 6)
					{
						break;
					}
				}
			}
			if (row == 0)
			{
				TextMeshProUGUI none = MakeText(p, "NoQuests", 26f, TextAlignmentOptions.Center, new Color(1f, 1f, 1f, 0.6f));
				PlaceBlockT(none.rectTransform, -140f, 60f);
				none.text = "No active quests.\nTalk to the campus NPCs to get some!";
				none.enableWordWrapping = true;
			}
			BuildQuestDetail(ref y);
		}

		private string QuestObjectiveText(QuestObject def, int step)
		{
			if (def == null || def.questStep == null || step >= def.questStep.Length)
			{
				return "Return to the NPC to finish.";
			}
			QuestStep s = def.questStep[step];
			if (s == null)
			{
				return "...";
			}
			// type markers first (SNativeID internals are protected — read counts only)
			if (s.killMob != null && s.killMob.numberOfMobs > 0)
			{
				int done = 0;
				int[] state;
				if (def.data != null && WorldRuntime.QuestActive.TryGetValue(def.data.questId, out state))
				{
					done = state[1];
				}
				return "Defeat " + s.killMob.numberOfMobs + " monsters. (" + done + "/" + s.killMob.numberOfMobs + ")";
			}
			if (s.reachLevel != 0 && s.reachLevel != 65535)
			{
				return "Reach level " + s.reachLevel + ".";
			}
			if (!string.IsNullOrEmpty(s.description))
			{
				return s.description;
			}
			if (!string.IsNullOrEmpty(s.title))
			{
				return s.title;
			}
			return "Talk to the quest NPC.";
		}

		private void BuildQuestRow(RectTransform rt, float y, string titleTxt, string objText, bool selected)
		{
			rt.anchorMin = new Vector2(0f, 1f);
			rt.anchorMax = new Vector2(1f, 1f);
			rt.pivot = new Vector2(0.5f, 1f);
			rt.sizeDelta = new Vector2(0f, 66f);
			rt.anchoredPosition = new Vector2(0f, y);
			rt.offsetMin = new Vector2(24f, rt.offsetMin.y);
			rt.offsetMax = new Vector2(-24f, rt.offsetMax.y);
			Image img = rt.gameObject.AddComponent<Image>();
			img.color = selected ? new Color(0.16f, 0.26f, 0.2f, 1f) : new Color(0.1f, 0.13f, 0.18f, 0.9f);
			TextMeshProUGUI t = MakeText(rt, "RowText", 22f, TextAlignmentOptions.Left, Color.white);
			((RectTransform)t.transform).anchorMin = Vector2.zero;
			((RectTransform)t.transform).anchorMax = Vector2.one;
			((RectTransform)t.transform).offsetMin = new Vector2(14f, 4f);
			((RectTransform)t.transform).offsetMax = new Vector2(-10f, -4f);
			t.text = "[" + titleTxt + "]\n<size=18><color=#b9d7ff>" + objText + "</color></size>";
			t.enableWordWrapping = false;
			t.overflowMode = TextOverflowModes.Ellipsis;
		}

		private void BuildQuestDetail(ref float y)
		{
			QuestObject def;
			if (_questDetailId < 0 || !_questDefs.TryGetValue(_questDetailId, out def))
			{
				return;
			}
			y -= 10f;
			Transform p = _questPanel;
			TextMeshProUGUI dt = MakeText(p, "CDetailTitle", 28f, TextAlignmentOptions.Left, new Color(0.45f, 1f, 0.55f));
			PlaceBlockT((RectTransform)dt.transform, y, 34f);
			dt.text = def.questTitle;
			y -= 36f;
			TextMeshProUGUI dd = MakeText(p, "CDetailDesc", 20f, TextAlignmentOptions.TopLeft, new Color(1f, 1f, 1f, 0.9f));
			((RectTransform)dd.transform).anchorMin = new Vector2(0f, 1f);
			((RectTransform)dd.transform).anchorMax = new Vector2(1f, 1f);
			((RectTransform)dd.transform).pivot = new Vector2(0f, 1f);
			((RectTransform)dd.transform).sizeDelta = new Vector2(-48f, 92f);
			((RectTransform)dd.transform).anchoredPosition = new Vector2(24f, y);
			dd.text = def.questDescription ?? string.Empty;
			dd.enableWordWrapping = true;
			y -= 96f;
			if (def.reward != null)
			{
				TextMeshProUGUI rw = MakeText(p, "CDetailReward", 20f, TextAlignmentOptions.Left, new Color(1f, 0.9f, 0.5f));
				PlaceBlockT((RectTransform)rw.transform, y, 28f);
				rw.text = "Reward: " + def.reward.experienceReward + " EXP, " + def.reward.goldReward + " gold";
				y -= 32f;
			}
			// give-up button (only when the original allows it)
			int detailId = _questDetailId;
			if (def.isCanGiveUp && WorldRuntime.QuestActive.ContainsKey(detailId))
			{
				GameObject giveGo = new GameObject("GiveUp", typeof(RectTransform));
				giveGo.transform.SetParent(p, false);
				((RectTransform)giveGo.transform).pivot = new Vector2(0f, 1f);
				BuildQuestButton((RectTransform)giveGo.transform, new Vector2(0f, 1f), new Vector2(180f, 48f), new Vector2(120f, y), "Give Up", delegate
				{
					WorldTcpClient client = WorldNet.Client;
					if (client != null)
					{
						client.SendQuestAction("giveUp", detailId);
					}
					_questDetailId = -1;
				});
			}
		}

		private void BuildQuestButton(RectTransform rt, Vector2 anchor, Vector2 size, Vector2 pos, string label, UnityEngine.Events.UnityAction onClick)
		{
			rt.anchorMin = anchor;
			rt.anchorMax = anchor;
			rt.sizeDelta = size;
			rt.anchoredPosition = pos;
			Image img = rt.gameObject.AddComponent<Image>();
			img.color = new Color(0.16f, 0.2f, 0.28f, 1f);
			TextMeshProUGUI lt = MakeText(rt, "Label", 26f, TextAlignmentOptions.Center, Color.white);
			((RectTransform)lt.transform).anchorMin = Vector2.zero;
			((RectTransform)lt.transform).anchorMax = Vector2.one;
			((RectTransform)lt.transform).offsetMin = Vector2.zero;
			((RectTransform)lt.transform).offsetMax = Vector2.zero;
			lt.text = label;
			ClickListener(rt.gameObject).onClick.AddListener(onClick);
		}

		// ---------------------------------------------------------------- talk dialog

		private void ShowTalkDialog(WorldTcpClient.TalkDialogInfo dlg)
		{
			EnsureQuestUi();
			if (_talkGo == null)
			{
				// Same host as the quest log: "OverlayPanels" canvas is always enabled
				Transform host = transform.Find("In-Game UI/OverlayPanels");
				if (host == null)
				{
					host = transform;
				}
				GameObject go = new GameObject("NpcTalkDialogUI", typeof(RectTransform));
				go.transform.SetParent(host, false);
				// own canvas with override sorting (renderMode stays inherited)
				Canvas ownCanvas = go.AddComponent<Canvas>();
				ownCanvas.overrideSorting = true;
				ownCanvas.sortingOrder = 720;
				go.AddComponent<GraphicRaycaster>();
				_talkGo = go;

				GameObject panel = new GameObject("Panel", typeof(RectTransform));
				panel.transform.SetParent(go.transform, false);
				RectTransform prt = (RectTransform)panel.transform;
				prt.anchorMin = new Vector2(0.5f, 0f);
				prt.anchorMax = new Vector2(0.5f, 0f);
				prt.pivot = new Vector2(0.5f, 0f);
				prt.sizeDelta = new Vector2(980f, 480f);
				prt.anchoredPosition = new Vector2(0f, 20f);
				Image pbg = panel.AddComponent<Image>();
				pbg.color = new Color(0.04f, 0.06f, 0.1f, 0.97f);
				pbg.raycastTarget = true;
				BoxOutline(prt, new Color(0.55f, 0.62f, 0.75f, 0.9f));
				_talkPanel = panel.transform;
			}
			_talkGo.SetActive(true);
			_talkGo.transform.SetAsLastSibling();
			// don't stack the quest log behind an open conversation
			if (_questLogOpen)
			{
				SetQuestLogOpen(false);
			}
			_talkNpcId = dlg.npc;
			_talkStatementId = dlg.statementId;

			Transform p = _talkPanel;
			for (int i = p.childCount - 1; i >= 0; i--)
			{
				Destroy(p.GetChild(i).gameObject);
			}
			// NPC name (green, like the ripped dialog)
			TextMeshProUGUI name = MakeText(p, "TalkName", 30f, TextAlignmentOptions.Left, new Color(0.45f, 1f, 0.55f));
			PlaceBlockT((RectTransform)name.transform, -12f, 40f);
			((RectTransform)name.transform).pivot = new Vector2(0f, 1f);
			((RectTransform)name.transform).sizeDelta = new Vector2(0f, 40f);
			name.text = dlg.name;
			// conversation text
			TextMeshProUGUI msg = MakeText(p, "TalkMessage", 24f, TextAlignmentOptions.TopLeft, new Color(1f, 1f, 1f, 0.92f));
			((RectTransform)msg.transform).anchorMin = new Vector2(0f, 1f);
			((RectTransform)msg.transform).anchorMax = new Vector2(1f, 1f);
			((RectTransform)msg.transform).pivot = new Vector2(0f, 1f);
			((RectTransform)msg.transform).sizeDelta = new Vector2(-48f, 130f);
			((RectTransform)msg.transform).anchoredPosition = new Vector2(24f, -54f);
			msg.text = dlg.text ?? string.Empty;
			msg.enableWordWrapping = true;
			// answer buttons (stacked like the original)
			float ay = -196f;
			if (dlg.answers != null && dlg.answers.Length > 0)
			{
				for (int i = 0; i < dlg.answers.Length && i < 6; i++)
				{
					WorldTcpClient.TalkAnswerInfo a = dlg.answers[i];
					GameObject btnGo = new GameObject("Answer_" + a.id, typeof(RectTransform));
					btnGo.transform.SetParent(p, false);
					int aId = a.id;
					BuildTalkAnswer((RectTransform)btnGo.transform, ay, a.text, aId);
					ay -= 52f;
				}
			}
			else
			{
				GameObject btnGo = new GameObject("Answer_close", typeof(RectTransform));
				btnGo.transform.SetParent(p, false);
				BuildTalkAnswer((RectTransform)btnGo.transform, ay, "Continue", -1);
			}
		}

		private void BuildTalkAnswer(RectTransform rt, float y, string label, int answerId)
		{
			rt.anchorMin = new Vector2(0f, 1f);
			rt.anchorMax = new Vector2(1f, 1f);
			rt.pivot = new Vector2(0.5f, 1f);
			rt.sizeDelta = new Vector2(-48f, 44f);
			rt.anchoredPosition = new Vector2(0f, y);
			Image img = rt.gameObject.AddComponent<Image>();
			img.color = new Color(0.14f, 0.18f, 0.26f, 1f);
			TextMeshProUGUI lt = MakeText(rt, "Label", 22f, TextAlignmentOptions.MidlineLeft, new Color(1f, 0.95f, 0.7f));
			((RectTransform)lt.transform).anchorMin = Vector2.zero;
			((RectTransform)lt.transform).anchorMax = Vector2.one;
			((RectTransform)lt.transform).offsetMin = new Vector2(14f, 0f);
			((RectTransform)lt.transform).offsetMax = new Vector2(-8f, 0f);
			lt.text = "> " + label;
			string npc = _talkNpcId;
			int stmt = _talkStatementId;
			ClickListener(rt.gameObject).onClick.AddListener(delegate
			{
				WorldTcpClient client = WorldNet.Client;
				if (answerId < 0)
				{
					_talkGo.SetActive(false);
					return;
				}
				if (client != null)
				{
					client.SendTalkAnswer(npc, stmt, answerId);
				}
			});
		}

		// ---------------------------------------------------------------- test hooks

		/// <summary>Test hook: walk every NPC, open its dialog and click its first
		/// answers — exercises accept/stepTalk/turn-in through the live server.</summary>
		private void DebugTourQuests()
		{
			if (_debugTour != null)
			{
				StopCoroutine(_debugTour);
			}
			_debugTour = StartCoroutine(DebugTourRoutine());
		}

		private System.Collections.IEnumerator DebugTourRoutine()
		{
			List<string> ids = new List<string>(WorldRuntime.NpcViews.Keys);
			UnityEngine.Debug.Log("[QuestUI] debug tour over " + ids.Count + " NPCs");
			foreach (string id in ids)
			{
				WorldRuntime.OpenNpcTalk(id);
				yield return new WaitForSecondsRealtime(0.45f);
				WorldTcpClient.TalkDialogInfo dlg = _lastTalk;
				if (dlg == null || dlg.npc != id || dlg.answers == null)
				{
					continue;
				}
				for (int i = 0; i < Mathf.Min(4, dlg.answers.Length); i++)
				{
					WorldTcpClient client = WorldNet.Client;
					if (client == null)
					{
						yield break;
					}
					UnityEngine.Debug.Log("[QuestUI] tour " + id + " click ans " + dlg.answers[i].id + " '" + dlg.answers[i].text + "'");
					client.SendTalkAnswer(dlg.npc, dlg.statementId, dlg.answers[i].id);
					yield return new WaitForSecondsRealtime(0.35f);
					dlg = _lastTalk;
					if (dlg == null || dlg.npc != id)
					{
						break;
					}
				}
			}
			UnityEngine.Debug.Log("[QuestUI] debug tour done; active=" + WorldRuntime.QuestActive.Count);
			yield break;
		}

		/// <summary>Test hook: TRUE click fidelity — projects the tab label to screen
		/// space, raycasts through the live GraphicRaycasters and executes the click on
		/// whatever is topmost (exactly like a finger tap, including overlays that
		/// would swallow it). Logs the hit so dead clicks are visible.</summary>
		private void DebugRealClickSkillTab(int t)
		{
			if (!EnsureSkillMenuWiring() || t < 0 || t >= 4)
			{
				UnityEngine.Debug.Log("[SkillMenu] DebugRealClickSkillTab " + t + " not wired");
				return;
			}
			if (!_skillsOpen)
			{
				ToggleSkillMenu(true);
			}
			// target the REAL tab button when wired (that's what a finger taps)
			UnityEngine.GameObject target = (_tabButtonGos != null && _tabButtonGos[t] != null)
				? _tabButtonGos[t]
				: (_tabLabels[t] != null ? _tabLabels[t].gameObject : null);
			if (target == null)
			{
				UnityEngine.Debug.Log("[SkillMenu] DebugClick " + t + ": no target");
				return;
			}
			Canvas camCanvas = target.GetComponentInParent<Canvas>(true);
			if (camCanvas == null)
			{
				UnityEngine.Debug.Log("[SkillMenu] DebugClick " + t + ": no parent canvas");
				return;
			}
			Camera uiCam = camCanvas.worldCamera;
			RectTransform labelRt = (RectTransform)target.transform;
			Vector2 world = labelRt.position;
			Vector2 screen = RectTransformUtility.WorldToScreenPoint(uiCam, world);
			var raycaster = new System.Collections.Generic.List<UnityEngine.UI.GraphicRaycaster>();
			GetComponentsInChildren(true, raycaster);
			var ped = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
			ped.position = screen;
			var results = new System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>();
			foreach (UnityEngine.UI.GraphicRaycaster rc in raycaster)
			{
				if (rc != null && rc.enabled && rc.gameObject.activeInHierarchy)
				{
					rc.Raycast(ped, results);
				}
			}
			results.Sort((a, b) => a.depth.CompareTo(b.depth));
			// frontmost = HIGHEST depth (children render after the panel background)
			UnityEngine.EventSystems.RaycastResult top = results.Count > 0
				? results[results.Count - 1]
				: default(UnityEngine.EventSystems.RaycastResult);
			string hitDesc = results.Count > 0
				? (top.gameObject.name + " (depth " + top.depth + ")")
				: "MISS (nothing raycastable at label position)";
			var topDesc = new System.Text.StringBuilder();
			for (int i = 0; i < results.Count && i < 6; i++)
			{
				if (i > 0)
				{
					topDesc.Append(" | ");
				}
				topDesc.Append(results[i].gameObject.name)
					.Append('#').Append(results[i].depth);
			}
			UnityEngine.Debug.Log("[SkillMenu] DebugClick tab=" + t + " screen=" + screen +
				" hits=" + results.Count + " top=" + hitDesc +
				" all=[" + topDesc + "]" +
				" pagesBefore=[" + PagesActiveText() + "]");
			if (results.Count > 0)
			{
				// real input module walks UP from the frontmost graphic to the nearest
				// IPointerClickHandler (the Button on Pow, not its Text child)
				UnityEngine.GameObject handler =
					UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<UnityEngine.EventSystems.IPointerClickHandler>(top.gameObject);
				if (handler != null)
				{
					UnityEngine.Debug.Log("[SkillMenu] DebugClick executes on handler=" + handler.name);
					UnityEngine.EventSystems.ExecuteEvents.Execute(handler, ped,
						UnityEngine.EventSystems.ExecuteEvents.pointerClickHandler);
				}
				else
				{
					UnityEngine.Debug.Log("[SkillMenu] DebugClick NO IPointerClickHandler above " + top.gameObject.name);
				}
			}
			UnityEngine.Debug.Log("[SkillMenu] DebugClick done pagesAfter=[" + PagesActiveText() + "]");
		}
	}
}
