using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RANMobile.Authentication
{
	/// <summary>
	/// P5 social UI (partial OriginalHudDriver): Friends and Club windows hosted under
	/// the always-enabled "OverlayPanels" canvas, opened from the radial FriendsBtn/
	/// ClubBtn. State is server-authoritative (WorldRuntime.Friends/Club); both windows
	/// rebuild on WorldRuntime.SocialChanged.
	///
	/// Original parity: friend requests (accept/deny), online markers, remove, club
	/// create / offline-capable invite / member list / kick / leave + invite banner.
	/// Debug* hooks drive the flows in tests without keyboard input.
	/// </summary>
	public partial class OriginalHudDriver
	{
		private GameObject _friendsGo;

		private GameObject _clubGo;

		private bool _friendsOpen;

		private bool _clubOpen;

		private bool _socialWired;

		private TMP_InputField _friendInput;

		private TMP_InputField _clubInput;

		private TMP_InputField _inviteInput;

		private const string SocialHostPath = "In-Game UI/OverlayPanels";

		private static readonly Dictionary<int, string> ClassNames = new Dictionary<int, string>
		{
			{ 1, "Brawler" }, { 2, "Swordsman" }, { 4, "Archer" }, { 8, "Shaman" },
		};

		// ---------------------------------------------------------------- wiring

		private void EnsureSocialWiring()
		{
			if (_socialWired)
			{
				return;
			}
			_socialWired = true;
			Transform friendsBtn = Deep(transform, "FriendsBtn");
			if (friendsBtn != null)
			{
				ClickListener(friendsBtn.gameObject).onClick.AddListener(ToggleFriends);
			}
			else
			{
				UnityEngine.Debug.LogWarning("[Social] FriendsBtn not found");
			}
			Transform clubBtn = Deep(transform, "ClubBtn");
			if (clubBtn != null)
			{
				ClickListener(clubBtn.gameObject).onClick.AddListener(ToggleClub);
			}
			else
			{
				UnityEngine.Debug.LogWarning("[Social] ClubBtn not found");
			}
			WorldRuntime.SocialChanged += OnSocialChangedUi;
		}

		private void OnSocialChangedUi()
		{
			if (_friendsOpen)
			{
				BuildFriendsBody();
			}
			if (_clubOpen)
			{
				BuildClubBody();
			}
		}

		// ---------------------------------------------------------------- shared builders

		private static void PlaceBlockT(RectTransform rt, float y, float height)
		{
			rt.anchorMin = new Vector2(0f, 1f);
			rt.anchorMax = new Vector2(1f, 1f);
			rt.pivot = new Vector2(0.5f, 1f);
			rt.sizeDelta = new Vector2(0f, height);
			rt.anchoredPosition = new Vector2(0f, y);
		}

		/// <summary>Horizontal margins for a PlaceBlockT line. NEVER touch offset Y on a
		/// point-Y anchor — it collapses the height to zero (that hid the feedback lines).</summary>
		private static void PadBlockX(RectTransform rt)
		{
			Vector2 min = rt.offsetMin;
			Vector2 max = rt.offsetMax;
			rt.offsetMin = new Vector2(24f, min.y);
			rt.offsetMax = new Vector2(-24f, max.y);
		}

		private static RectTransform MakeInfoLine(Transform parent, string text, float y, Color color, float height = 30f)
		{
			TextMeshProUGUI t = MakeText(parent, "Line", 22f, TextAlignmentOptions.Left, color);
			PlaceBlockT((RectTransform)t.transform, y, height);
			t.text = text;
			return (RectTransform)t.transform;
		}

		/// <summary>Window panel host + title. Caller owns open/close state.</summary>
		private GameObject MakeSocialWindow(string name, Vector2 size, string title)
		{
			Transform host = transform.Find(SocialHostPath);
			if (host == null)
			{
				host = transform;
			}
			GameObject go = new GameObject(name, typeof(RectTransform));
			go.transform.SetParent(host, false);
			// own sorting canvas — draws above the HUD regardless of the host's state
			Canvas c = go.AddComponent<Canvas>();
			c.overrideSorting = true;
			c.sortingOrder = 705;
			go.AddComponent<GraphicRaycaster>();
			RectTransform rt = (RectTransform)go.transform;
			rt.anchorMin = new Vector2(0.5f, 0.5f);
			rt.anchorMax = new Vector2(0.5f, 0.5f);
			rt.pivot = new Vector2(0.5f, 0.5f);
			rt.sizeDelta = size;
			rt.anchoredPosition = Vector2.zero;
			// fixed panel child so a screen-driven window rect can't stretch content
			GameObject panel = new GameObject("Panel", typeof(RectTransform));
			panel.transform.SetParent(go.transform, false);
			RectTransform prt = (RectTransform)panel.transform;
			prt.anchorMin = new Vector2(0.5f, 0.5f);
			prt.anchorMax = new Vector2(0.5f, 0.5f);
			prt.pivot = new Vector2(0.5f, 0.5f);
			prt.sizeDelta = size;
			prt.anchoredPosition = Vector2.zero;
			Image bg = panel.AddComponent<Image>();
			bg.color = new Color(0.03f, 0.05f, 0.09f, 0.96f);
			bg.raycastTarget = true;
			BoxOutline(prt, new Color(0.55f, 0.62f, 0.75f, 0.9f));

			TextMeshProUGUI titleT = MakeText(panel.transform, "Title", 34f, TextAlignmentOptions.Center, new Color(0.45f, 1f, 0.55f));
			((RectTransform)titleT.transform).anchorMin = new Vector2(0f, 1f);
			((RectTransform)titleT.transform).anchorMax = new Vector2(1f, 1f);
			((RectTransform)titleT.transform).pivot = new Vector2(0.5f, 1f);
			((RectTransform)titleT.transform).sizeDelta = new Vector2(0f, 50f);
			((RectTransform)titleT.transform).anchoredPosition = new Vector2(0f, -10f);
			titleT.text = title;

			GameObject closeGo = new GameObject("Close", typeof(RectTransform));
			closeGo.transform.SetParent(panel.transform, false);
			BuildQuestButton((RectTransform)closeGo.transform, new Vector2(0.5f, 0f), new Vector2(200f, 50f), new Vector2(0f, 12f), "Close",
				delegate
				{
					if (name == "FriendsWindow") { SetFriendsOpen(false); }
					else { SetClubOpen(false); }
				});
			go.SetActive(false);
			return go;
		}

		/// <summary>Single-line name input (width leaves room for a right-side button).</summary>
		private static TMP_InputField MakeNameInput(Transform parent, float y, string placeholder, out TMP_InputField existing)
		{
			GameObject go = new GameObject("NameInput", typeof(RectTransform));
			go.transform.SetParent(parent, false);
			RectTransform rt = (RectTransform)go.transform;
			rt.anchorMin = new Vector2(0f, 1f);
			rt.anchorMax = new Vector2(1f, 1f);
			rt.pivot = new Vector2(0.5f, 1f);
			rt.sizeDelta = new Vector2(-216f, 54f);
			rt.anchoredPosition = new Vector2(0f, y);
			Image bg = go.AddComponent<Image>();
			bg.color = new Color(0.1f, 0.13f, 0.18f, 1f);
			TMP_InputField field = go.AddComponent<TMP_InputField>();
			TextMeshProUGUI text = MakeText(go.transform, "Text", 24f, TextAlignmentOptions.MidlineLeft, Color.white);
			((RectTransform)text.transform).anchorMin = Vector2.zero;
			((RectTransform)text.transform).anchorMax = Vector2.one;
			((RectTransform)text.transform).offsetMin = new Vector2(14f, 2f);
			((RectTransform)text.transform).offsetMax = new Vector2(-14f, -2f);
			text.text = string.Empty;
			TextMeshProUGUI ph = MakeText(go.transform, "Placeholder", 24f, TextAlignmentOptions.MidlineLeft, new Color(1f, 1f, 1f, 0.35f));
			((RectTransform)ph.transform).anchorMin = Vector2.zero;
			((RectTransform)ph.transform).anchorMax = Vector2.one;
			((RectTransform)ph.transform).offsetMin = new Vector2(14f, 2f);
			((RectTransform)ph.transform).offsetMax = new Vector2(-14f, -2f);
			ph.text = placeholder;
			field.textComponent = text;
			field.placeholder = ph;
			field.targetGraphic = bg;
			field.caretColor = Color.white;
			existing = field;
			return field;
		}

		/// <summary>Right-side action button aligned with an input row.</summary>
		private Button MakeSideButton(Transform parent, string label, float y, UnityEngine.Events.UnityAction onClick)
		{
			GameObject go = new GameObject(label + "Btn", typeof(RectTransform));
			go.transform.SetParent(parent, false);
			BuildQuestButton((RectTransform)go.transform, new Vector2(1f, 1f), new Vector2(150f, 54f), new Vector2(-24f, y), label, onClick);
			((RectTransform)go.transform).pivot = new Vector2(1f, 1f);
			return go.GetComponent<Button>();
		}

		private static void ClearBody(Transform panel, params string[] keep)
		{
			for (int i = panel.childCount - 1; i >= 0; i--)
			{
				Transform c = panel.GetChild(i);
				bool kept = false;
				for (int k = 0; k < keep.Length; k++)
				{
					if (c.name == keep[k])
					{
						kept = true;
						break;
					}
				}
				if (!kept)
				{
					Destroy(c.gameObject);
				}
			}
		}

		// ---------------------------------------------------------------- friends window

		private void ToggleFriends()
		{
			SetFriendsOpen(!_friendsOpen);
		}

		/// <summary>Test hook: idempotent open + fresh list.</summary>
		private void DebugOpenFriends()
		{
			SetFriendsOpen(true);
		}

		private void SetFriendsOpen(bool open)
		{
			EnsureSocialWiring();
			if (_friendsGo == null)
			{
				_friendsGo = MakeSocialWindow("FriendsWindow", new Vector2(820f, 880f), "Friends");
			}
			_friendsOpen = open;
			_friendsGo.SetActive(open);
			if (open)
			{
				BuildFriendsBody();
				WorldTcpClient client = WorldNet.Client;
				if (client != null)
				{
					client.SendFriendListRequest();
				}
			}
		}

		private void BuildFriendsBody()
		{
			if (_friendsGo == null)
			{
				return;
			}
			Transform panel = _friendsGo.transform.Find("Panel");
			if (panel == null)
			{
				return;
			}
			ClearBody(panel, "Title", "Close");
			float y = -58f;

			// last feedback (errors / accept notices)
			UnityEngine.Debug.Log("[SocialUI] build friends feedback='" + WorldRuntime.LastSocialFeedback + "' y=" + y + " open=" + _friendsOpen);
			if (!string.IsNullOrEmpty(WorldRuntime.LastSocialFeedback))
			{
				TextMeshProUGUI fb = MakeText(panel, "Feedback", 20f, TextAlignmentOptions.Left, new Color(1f, 0.75f, 0.4f));
				PlaceBlockT((RectTransform)fb.transform, y, 28f);
				PadBlockX((RectTransform)fb.transform);
				fb.text = WorldRuntime.LastSocialFeedback;
				y -= 32f;
			}

			// add-friend row
			string addText = "Add by character name...";
			TMP_InputField input;
			MakeNameInput(panel, y, addText, out input);
			_friendInput = input;
			MakeSideButton(panel, "Add", y, delegate
			{
				string v = (_friendInput != null) ? _friendInput.text.Trim() : string.Empty;
				if (v.Length > 0)
				{
					WorldTcpClient client = WorldNet.Client;
					if (client != null)
					{
						client.SendFriendAdd(v);
					}
					if (_friendInput != null)
					{
						_friendInput.text = string.Empty;
					}
				}
			});
			y -= 66f;

			// pending incoming requests first (original flow: accept/deny)
			int pending = 0;
			foreach (WorldTcpClient.FriendInfo f in WorldRuntime.Friends)
			{
				if (f.state == "pending_in")
				{
					if (pending == 0)
					{
						MakeInfoLine(panel, "Friend Requests:", y, new Color(0.45f, 0.7f, 1f));
						y -= 32f;
					}
					string who = f.name;
					GameObject row = new GameObject("Req_" + who, typeof(RectTransform));
					row.transform.SetParent(panel, false);
					PlaceBlockT((RectTransform)row.transform, y, 54f);
					Image rbg = row.AddComponent<Image>();
					rbg.color = new Color(0.1f, 0.14f, 0.2f, 0.95f);
					TextMeshProUGUI rt2 = MakeText(row.transform, "Name", 22f, TextAlignmentOptions.Left, Color.white);
					((RectTransform)rt2.transform).anchorMin = Vector2.zero;
					((RectTransform)rt2.transform).anchorMax = Vector2.one;
					((RectTransform)rt2.transform).offsetMin = new Vector2(14f, 0f);
					((RectTransform)rt2.transform).offsetMax = new Vector2(-250f, 0f);
					rt2.text = who;
					string acceptName = who;
					BuildSideRowButton(row.transform, "Accept", new Vector2(-170f, 0f), delegate
					{
						WorldTcpClient client = WorldNet.Client;
						if (client != null)
						{
							client.SendFriendAccept(acceptName);
						}
					});
					BuildSideRowButton(row.transform, "Deny", new Vector2(-24f, 0f), delegate
					{
						WorldTcpClient client = WorldNet.Client;
						if (client != null)
						{
							client.SendFriendRemove(acceptName);
						}
					});
					y -= 58f;
					pending++;
				}
			}
			if (pending > 0)
			{
				y -= 6f;
			}

			// accepted friends
			int shown = 0;
			int total = 0;
			foreach (WorldTcpClient.FriendInfo f in WorldRuntime.Friends)
			{
				if (f.state != "accepted")
				{
					continue;
				}
				total++;
				if (shown >= 10)
				{
					continue;
				}
				shown++;
				GameObject row = new GameObject("Friend_" + f.name, typeof(RectTransform));
				row.transform.SetParent(panel, false);
				PlaceBlockT((RectTransform)row.transform, y, 58f);
				Image rbg = row.AddComponent<Image>();
				rbg.color = new Color(0.09f, 0.11f, 0.16f, 0.95f);
				string clsName;
				if (!ClassNames.TryGetValue(f.cls, out clsName))
				{
					clsName = "?";
				}
				bool isActive = WorldRuntime.Friends.Count >= 0 && f.online;
				TextMeshProUGUI nm = MakeText(row.transform, "Name", 22f, TextAlignmentOptions.Left,
					f.online ? new Color(0.5f, 1f, 0.55f) : new Color(0.75f, 0.75f, 0.8f));
				((RectTransform)nm.transform).anchorMin = Vector2.zero;
				((RectTransform)nm.transform).anchorMax = Vector2.one;
				((RectTransform)nm.transform).offsetMin = new Vector2(14f, 0f);
				((RectTransform)nm.transform).offsetMax = new Vector2(-320f, 0f);
				nm.text = f.name + "  <size=17><color=#9fb6d8>Lv." + f.level + " " + clsName + "</color></size>";
				TextMeshProUGUI st = MakeText(row.transform, "State", 19f, TextAlignmentOptions.MidlineRight,
					f.online ? new Color(0.5f, 1f, 0.55f) : new Color(1f, 1f, 1f, 0.4f));
				((RectTransform)st.transform).anchorMin = Vector2.zero;
				((RectTransform)st.transform).anchorMax = Vector2.one;
				((RectTransform)st.transform).offsetMin = new Vector2(0f, 0f);
				((RectTransform)st.transform).offsetMax = new Vector2(-120f, 0f);
				st.text = f.online ? "ONLINE" : "offline";
				string removeName = f.name;
				BuildSideRowButton(row.transform, "Remove", new Vector2(-24f, 0f), delegate
				{
					WorldTcpClient client = WorldNet.Client;
					if (client != null)
					{
						client.SendFriendRemove(removeName);
					}
				});
				y -= 62f;
			}
			if (total > shown)
			{
				MakeInfoLine(panel, "(" + total + " friends — showing first " + shown + ")", y, new Color(1f, 1f, 1f, 0.5f), 26f);
				y -= 30f;
			}
			MakeInfoLine(panel, "Friends: " + total + " / 50", y, new Color(0.45f, 0.7f, 1f));
		}

		private static void BuildSideRowButton(Transform parent, string label, Vector2 pos, UnityEngine.Events.UnityAction onClick)
		{
			GameObject go = new GameObject(label, typeof(RectTransform));
			go.transform.SetParent(parent, false);
			RectTransform rt = (RectTransform)go.transform;
			rt.anchorMin = new Vector2(1f, 0.5f);
			rt.anchorMax = new Vector2(1f, 0.5f);
			rt.pivot = new Vector2(1f, 0.5f);
			rt.sizeDelta = new Vector2(label == "Accept" ? 110f : 100f, 44f);
			rt.anchoredPosition = pos;
			Image img = go.AddComponent<Image>();
			img.color = new Color(0.16f, 0.2f, 0.28f, 1f);
			TextMeshProUGUI lt = MakeText(go.transform, "Label", 20f, TextAlignmentOptions.Center, Color.white);
			((RectTransform)lt.transform).anchorMin = Vector2.zero;
			((RectTransform)lt.transform).anchorMax = Vector2.one;
			((RectTransform)lt.transform).offsetMin = Vector2.zero;
			((RectTransform)lt.transform).offsetMax = Vector2.zero;
			lt.text = label;
			ClickListener(go).onClick.AddListener(onClick);
		}

		// ---------------------------------------------------------------- club window

		private void ToggleClub()
		{
			SetClubOpen(!_clubOpen);
		}

		/// <summary>Test hook: idempotent open + fresh info.</summary>
		private void DebugOpenClub()
		{
			SetClubOpen(true);
		}

		private void SetClubOpen(bool open)
		{
			EnsureSocialWiring();
			if (_clubGo == null)
			{
				_clubGo = MakeSocialWindow("ClubWindow", new Vector2(820f, 880f), "Club");
			}
			_clubOpen = open;
			_clubGo.SetActive(open);
			if (open)
			{
				BuildClubBody();
				WorldTcpClient client = WorldNet.Client;
				if (client != null)
				{
					client.SendClubInfoRequest();
				}
			}
		}

		private void BuildClubBody()
		{
			if (_clubGo == null)
			{
				return;
			}
			Transform panel = _clubGo.transform.Find("Panel");
			if (panel == null)
			{
				return;
			}
			ClearBody(panel, "Title", "Close");
			float y = -58f;

			WorldTcpClient.ClubInfoData club = WorldRuntime.Club;

			// pending invite banner: pushed when online, or returned by CLUB_INFO for
			// invites sent while we were offline (both paths land in the same UI)
			string invClub = WorldRuntime.PendingClubInviteClub;
			string invBy = WorldRuntime.PendingClubInviteBy;
			if (string.IsNullOrEmpty(invClub) && club != null && club.inviteClubIds != null && club.inviteClubIds.Length > 0)
			{
				invClub = club.inviteClubNames[0];
				invBy = club.inviteFrom[0];
			}
			if (!string.IsNullOrEmpty(invClub))
			{
				GameObject banner = new GameObject("InviteBanner", typeof(RectTransform));
				banner.transform.SetParent(panel, false);
				PlaceBlockT((RectTransform)banner.transform, y, 92f);
				PadBlockX((RectTransform)banner.transform);
				Image bimg = banner.AddComponent<Image>();
				bimg.color = new Color(0.13f, 0.17f, 0.1f, 0.95f);
				TextMeshProUGUI bt = MakeText(banner.transform, "Text", 21f, TextAlignmentOptions.TopLeft, Color.white);
				((RectTransform)bt.transform).anchorMin = Vector2.zero;
				((RectTransform)bt.transform).anchorMax = Vector2.one;
				((RectTransform)bt.transform).offsetMin = new Vector2(14f, 6f);
				((RectTransform)bt.transform).offsetMax = new Vector2(-14f, -6f);
				bt.text = invBy + " invited you to join club '" + invClub + "'.";
				bt.enableWordWrapping = true;
				y -= 98f;
				GameObject acceptGo = new GameObject("AcceptInvite", typeof(RectTransform));
				acceptGo.transform.SetParent(panel, false);
				((RectTransform)acceptGo.transform).pivot = new Vector2(0f, 1f);
				BuildQuestButton((RectTransform)acceptGo.transform, new Vector2(0f, 1f), new Vector2(230f, 46f), new Vector2(40f, y), "Accept Invite", delegate
				{
					WorldTcpClient client = WorldNet.Client;
					if (client != null)
					{
						client.SendClubAccept();
					}
					WorldRuntime.PendingClubInviteClub = null;
					WorldRuntime.PendingClubInviteBy = null;
					BuildClubBody();
				});
				y -= 56f;
			}

			if (!string.IsNullOrEmpty(WorldRuntime.LastSocialFeedback) && WorldRuntime.LastSocialFeedback.StartsWith("club"))
			{
				TextMeshProUGUI fb = MakeText(panel, "Feedback", 20f, TextAlignmentOptions.Left, new Color(1f, 0.75f, 0.4f));
				PlaceBlockT((RectTransform)fb.transform, y, 28f);
				PadBlockX((RectTransform)fb.transform);
				fb.text = WorldRuntime.LastSocialFeedback;
				y -= 32f;
			}

			if (club == null || !club.hasClub)
			{
				// no club: create flow (+ hint)
				MakeInfoLine(panel, "You are not in a club.", y, new Color(1f, 1f, 1f, 0.75f));
				y -= 34f;
				MakeInfoLine(panel, "Create one — name length 3-16:", y, new Color(0.45f, 0.7f, 1f), 26f);
				y -= 32f;
				TMP_InputField input;
				MakeNameInput(panel, y, "Club name", out input);
				_clubInput = input;
				MakeSideButton(panel, "Create", y, delegate
				{
					string v = (_clubInput != null) ? _clubInput.text.Trim() : string.Empty;
					if (v.Length >= 3)
					{
						WorldTcpClient client = WorldNet.Client;
						if (client != null)
						{
							client.SendClubCreate(v);
						}
						if (_clubInput != null)
						{
							_clubInput.text = string.Empty;
						}
					}
				});
				y -= 70f;
				MakeInfoLine(panel, "Tip: invite friends after creating — offline\nplayers get the invite when they log in.", y, new Color(1f, 1f, 1f, 0.45f), 64f);
				return;
			}

			// club view
			TextMeshProUGUI cname = MakeText(panel, "ClubName", 30f, TextAlignmentOptions.Left, new Color(1f, 0.9f, 0.45f));
			PlaceBlockT((RectTransform)cname.transform, y, 36f);
			PadBlockX((RectTransform)cname.transform);
			cname.text = club.name;
			y -= 42f;

			string masterName = "?";
			foreach (WorldTcpClient.ClubMemberInfo m in club.members)
			{
				if (m.rank >= 3)
				{
					masterName = m.name;
					break;
				}
			}
			MakeInfoLine(panel, "Master: " + masterName + "   Members: " + club.members.Length, y, new Color(0.45f, 0.7f, 1f));
			y -= 40f;
			if (!string.IsNullOrEmpty(club.notice))
			{
				TextMeshProUGUI nt = MakeText(panel, "Notice", 19f, TextAlignmentOptions.TopLeft, new Color(1f, 1f, 1f, 0.7f));
				PlaceBlockT((RectTransform)nt.transform, y, 50f);
				PadBlockX((RectTransform)nt.transform);
				nt.text = "Notice: " + club.notice;
				nt.enableWordWrapping = true;
				y -= 54f;
			}

			MakeInfoLine(panel, "Members:", y, new Color(0.45f, 0.7f, 1f));
			y -= 32f;
			int shown = 0;
			foreach (WorldTcpClient.ClubMemberInfo m in club.members)
			{
				if (shown >= 10)
				{
					break;
				}
				shown++;
				GameObject row = new GameObject("Mem_" + m.name, typeof(RectTransform));
				row.transform.SetParent(panel, false);
				PlaceBlockT((RectTransform)row.transform, y, 54f);
				Image rbg = row.AddComponent<Image>();
				rbg.color = new Color(0.09f, 0.11f, 0.16f, 0.95f);
				TextMeshProUGUI nm = MakeText(row.transform, "Name", 21f, TextAlignmentOptions.Left,
					m.online ? new Color(0.5f, 1f, 0.55f) : new Color(0.75f, 0.75f, 0.8f));
				((RectTransform)nm.transform).anchorMin = Vector2.zero;
				((RectTransform)nm.transform).anchorMax = Vector2.one;
				((RectTransform)nm.transform).offsetMin = new Vector2(14f, 0f);
				((RectTransform)nm.transform).offsetMax = new Vector2(-330f, 0f);
				string rankTag = m.rank >= 3 ? "<color=#ffd34d>[Master]</color> " : (m.rank == 2 ? "[Officer] " : "");
				string clsName;
				if (!ClassNames.TryGetValue(m.cls, out clsName))
				{
					clsName = "?";
				}
				nm.text = rankTag + m.name + "  <size=17><color=#9fb6d8>Lv." + m.level + " " + clsName + "</color></size>";
				TextMeshProUGUI st = MakeText(row.transform, "State", 19f, TextAlignmentOptions.MidlineRight,
					m.online ? new Color(0.5f, 1f, 0.55f) : new Color(1f, 1f, 1f, 0.4f));
				((RectTransform)st.transform).anchorMin = Vector2.zero;
				((RectTransform)st.transform).anchorMax = Vector2.one;
				((RectTransform)st.transform).offsetMin = new Vector2(0f, 0f);
				((RectTransform)st.transform).offsetMax = new Vector2(-140f, 0f);
				st.text = m.online ? "ONLINE" : "offline";
				if (m.rank < 3)
				{
					string kickName = m.name;
					BuildSideRowButton(row.transform, "Kick", new Vector2(-24f, 0f), delegate
					{
						WorldTcpClient client = WorldNet.Client;
						if (client != null)
						{
							client.SendClubKick(kickName);
						}
					});
				}
				y -= 58f;
			}
			y -= 8f;

			// invite row
			TMP_InputField invInput;
			MakeNameInput(panel, y, "Invite by character name...", out invInput);
			_inviteInput = invInput;
			MakeSideButton(panel, "Invite", y, delegate
			{
				string v = (_inviteInput != null) ? _inviteInput.text.Trim() : string.Empty;
				if (v.Length > 0)
				{
					WorldTcpClient client = WorldNet.Client;
					if (client != null)
					{
						client.SendClubInvite(v);
					}
					if (_inviteInput != null)
					{
						_inviteInput.text = string.Empty;
					}
				}
			});
			y -= 70f;

			GameObject leaveGo = new GameObject("Leave", typeof(RectTransform));
			leaveGo.transform.SetParent(panel, false);
			BuildQuestButton((RectTransform)leaveGo.transform,
				new Vector2(0f, 1f), new Vector2(170f, 46f), new Vector2(40f, y), "Leave Club", delegate
				{
					WorldTcpClient client = WorldNet.Client;
					if (client != null)
					{
						client.SendClubLeave();
					}
				});
		}

		// ---------------------------------------------------------------- social send helpers (driven by buttons AND tests)

		private void DebugAddFriend(string name)
		{
			WorldTcpClient client = WorldNet.Client;
			if (client != null)
			{
				client.SendFriendListRequest();
				client.SendFriendAdd(name);
			}
		}

		private void DebugAcceptFriend(string name)
		{
			WorldTcpClient client = WorldNet.Client;
			if (client != null)
			{
				client.SendFriendAccept(name);
			}
		}

		private void DebugRemoveFriend(string name)
		{
			WorldTcpClient client = WorldNet.Client;
			if (client != null)
			{
				client.SendFriendRemove(name);
			}
		}

		private void DebugCreateClub(string name)
		{
			WorldTcpClient client = WorldNet.Client;
			if (client != null)
			{
				client.SendClubCreate(name);
			}
		}

		private void DebugInviteToClub(string name)
		{
			WorldTcpClient client = WorldNet.Client;
			if (client != null)
			{
				client.SendClubInvite(name);
			}
		}

		private void DebugClubAccept()
		{
			WorldTcpClient client = WorldNet.Client;
			if (client != null)
			{
				client.SendClubAccept();
			}
		}

		private void DebugClubLeave()
		{
			WorldTcpClient client = WorldNet.Client;
			if (client != null)
			{
				client.SendClubLeave();
			}
		}

		private void DebugClubKick(string name)
		{
			WorldTcpClient client = WorldNet.Client;
			if (client != null)
			{
				client.SendClubKick(name);
			}
		}
	}
}
