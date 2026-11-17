using System.Collections;
using Models.Library.Shared.Game;
using TrinityRift.Network;
using UnityEngine;
using UnityEngine.Networking;

namespace RANMobile.Authentication
{
	public class CharacterLoginManager : MonoBehaviour
	{
		public string selectedCharacterID;

		public string selectedCharacterName;

		public int selectedClass;

		public int selectedSchool;

		public int selectedGender;

		public int selectedHair;

		public int selectedFace;

		public static CharacterLoginManager instance;

		private WorldTcpClient _world;

		private bool _connecting;

		public WorldTcpClient World
		{
			get
			{
				return _world;
			}
		}

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if (_world != null)
			{
				_world.Pump();
			}
		}

		public void StartGame()
		{
			if (_connecting)
			{
				return;
			}
			if (string.IsNullOrEmpty(selectedCharacterID))
			{
				Debug.LogWarning("[CharacterLoginManager] StartGame with no character selected.");
				ShowDisconnectedDialog();
				return;
			}
			string token = Authentication.SessionToken;
			if (string.IsNullOrEmpty(token))
			{
				Debug.LogWarning("[CharacterLoginManager] StartGame with no session token.");
				ShowDisconnectedDialog();
				return;
			}
			_connecting = true;
			StartCoroutine(HandoffThenConnect(token));
		}

		private IEnumerator HandoffThenConnect(string accessToken)
		{
			string baseurl = Authentication.BaseUrl;
			if (string.IsNullOrEmpty(baseurl))
			{
				baseurl = "http://127.0.0.1:8080";
			}
			using (var req = new UnityWebRequest(baseurl.TrimEnd('/') + "/api/auth/token", "POST"))
			{
				req.uploadHandler = new UploadHandlerRaw(new byte[0]);
				req.downloadHandler = new DownloadHandlerBuffer();
				req.SetRequestHeader("Authorization", "Bearer " + accessToken);
				req.timeout = 10;
				yield return req.SendWebRequest();
				if (req.result != UnityWebRequest.Result.Success)
				{
					_connecting = false;
					Debug.LogError("[CharacterLoginManager] token handoff failed: " + req.error + " " + req.downloadHandler.text);
					ShowDisconnectedDialog();
					yield break;
				}
				string handoff = ExtractString(req.downloadHandler.text, "newToken");
				if (string.IsNullOrEmpty(handoff))
				{
					_connecting = false;
					Debug.LogError("[CharacterLoginManager] token handoff returned no newToken: " + req.downloadHandler.text);
					ShowDisconnectedDialog();
					yield break;
				}
				Debug.Log("[CharacterLoginManager] handoff token acquired, connecting...");
				ConnectTcp(handoff);
			}
		}

		private static string ExtractString(string json, string key)
		{
			string token = "\"" + key + "\"";
			int i = json.IndexOf(token, System.StringComparison.Ordinal);
			if (i < 0)
			{
				return null;
			}
			i = json.IndexOf(':', i + token.Length);
			if (i < 0)
			{
				return null;
			}
			i = json.IndexOf('"', i);
			if (i < 0)
			{
				return null;
			}
			int start = i + 1;
			int end = json.IndexOf('"', start);
			if (end < 0)
			{
				return null;
			}
			return json.Substring(start, end - start);
		}

		private void ConnectTcp(string handoff)
		{
			string host = "127.0.0.1";
			int port = 7777;
			Debug.Log("[CharacterLoginManager] connecting to " + host + ":" + port + " as " + selectedCharacterName + " (" + selectedCharacterID + ")");
			WorldNet.Clear();
			WorldRuntime.Clear();
			if (_world == null)
			{
				_world = new WorldTcpClient();
			}
			WorldNet.Client = _world;
			string charId = selectedCharacterID;
			string charName = selectedCharacterName;
			int cls = selectedClass;
			int sch = selectedSchool;
			int hair = selectedHair;
			int face = selectedFace;
			bool female = selectedGender == 1;
			_world.OnPlayerEnter = WorldNet.QueueEnter;
			_world.OnPlayerMove = WorldNet.QueueMove;
			_world.OnPlayerLeave = WorldNet.QueueLeave;
			_world.OnPlayerAttack = WorldNet.QueueAttack;
			_world.OnPlayerHp = WorldNet.QueueHp;
			_world.OnPlayerChat = WorldNet.QueueChat;
			_world.OnMobSpawn = WorldRuntime.QueueMobSpawn;
			_world.OnMobMove = WorldRuntime.QueueMobMove;
			_world.OnMobHp = WorldRuntime.QueueMobHp;
			_world.OnMobDeath = WorldRuntime.QueueMobDeath;
			_world.OnDropSpawn = WorldRuntime.QueueDropSpawn;
			_world.OnDropRemove = WorldRuntime.QueueDropRemove;
			_world.OnSkillResult = WorldRuntime.QueueSkillResult;
			_world.OnInventory = WorldRuntime.QueueInventory;
			_world.OnEquipResult = WorldRuntime.QueueEquipResult;
			_world.OnSkillList = WorldRuntime.QueueSkillList;
			_world.OnSelfVitals = WorldRuntime.QueueSelfVitals;
			_world.OnRespawn = WorldRuntime.QueueRespawn;
			_world.OnBuffApply = WorldRuntime.QueueBuffApply;
			_world.OnBuffUpdate = WorldRuntime.QueueBuffUpdate;
			_world.OnAilmentApply = WorldRuntime.QueueAilmentApply;
			_world.OnAilmentUpdate = WorldRuntime.QueueAilmentUpdate;
			_world.OnQuestState = WorldRuntime.QueueQuestState;
			_world.OnQuestProgress = WorldRuntime.QueueQuestProgress;
			_world.OnQuestResult = WorldRuntime.QueueQuestResult;
			_world.OnNpcList = WorldRuntime.QueueNpcList;
			_world.OnTalkDialog = WorldRuntime.QueueTalkDialog;
			_world.OnFriendList = WorldRuntime.QueueFriendList;
			_world.OnFriendAddLure = WorldRuntime.QueueFriendAddLure;
			_world.OnFriendFeedback = WorldRuntime.QueueFriendFeedback;
			_world.OnFriendState = WorldRuntime.QueueFriendState;
			_world.OnClubInfo = WorldRuntime.QueueClubInfo;
			_world.OnClubInviteAsk = WorldRuntime.QueueClubInviteAsk;
			_world.OnClubResult = WorldRuntime.QueueClubResult;
			_world.OnClubMemberEvent = WorldRuntime.QueueClubMemberEvent;
			_world.ConnectAndEnter(host, port, handoff, charId, selectedGender, delegate(WorldTcpClient.ZoneInfo zone)
			{
				_connecting = false;
				Debug.Log("[CharacterLoginManager] enter world ok: map=" + zone.map + " gate=" + zone.gate + " pos=(" + zone.x + "," + zone.y + "," + zone.z + ")");
				GameObject go = new GameObject("WorldEntry");
				Object.DontDestroyOnLoad(go);
				WorldEntry entry = go.AddComponent<WorldEntry>();
				entry.zone = zone;
				entry.characterName = charName;
				entry.classId = cls;
				entry.school = sch;
				entry.hair = hair;
				entry.face = face;
				entry.female = female;
			}, delegate(string error)
			{
				_connecting = false;
				Debug.LogError("[CharacterLoginManager] world connect failed: " + error);
				ShowDisconnectedDialog();
			});
		}

		private void GoToServerEntry()
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("ServerEntry");
		}

		private void WireZoneHandlers(ServerConnectionManager scm)
		{
		}

		private static void ClearZoneHandlers(ServerConnectionManager scm)
		{
		}

		private static SItemCustom[] CloneItems(SItemCustom[] src)
		{
			return src;
		}

		private void ShowDisconnectedDialog()
		{
			Debug.Log("[Offline] Not connected to a server.");
		}

		private static void OnDisconnectedConfirm()
		{
		}

		public void CreateParam()
		{
		}
	}
}
