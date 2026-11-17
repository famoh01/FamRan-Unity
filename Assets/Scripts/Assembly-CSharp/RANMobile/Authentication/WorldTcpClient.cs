// UNIQUE_TEST_MARKER_ABC123
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace RANMobile.Authentication
{
	/// <summary>
	/// Minimal game-server TCP client: handshake (KX01/KXOK+nonce), LOGIN with the
	/// HTTP session token, CHARACTER_SELECT, ENTER_WORLD. Server→client payloads are
	/// XOR-obfuscated with a per-connection keystream (KEY_MIX ^ nonce, xorshift32,
	/// re-seeded per message to match the Node server). Client→server stays plaintext.
	/// Runs the blocking protocol on a worker thread; results are Pump()'d on main.
	/// </summary>
	public class WorldTcpClient
	{
		public const ushort OpHandshake = 0x0001;

		public const ushort OpLogin = 0x0002;

		public const ushort OpHeartbeat = 0x0003;

		public const ushort OpCharSelect = 0x0013;

		public const ushort OpEnterWorld = 0x0030;

		public const ushort OpMove = 0x0040;

		public const ushort OpAttack = 0x0041;

		public const ushort OpChat = 0x0042;

		public const ushort OpSkillUse = 0x0043;

		public const ushort OpPickup = 0x0044;

		public const ushort OpInventoryRequest = 0x0045;
		public const ushort OpSkillListRequest = 0x0046;
		public const ushort OpEquipRequest = 0x0047;

		public const ushort OpUseItem = 0x0048;

		public const ushort OpHandshakeAck = 0x8001;

		public const ushort OpLoginResult = 0x8002;

		public const ushort OpHeartbeatAck = 0x8003;

		public const ushort OpCharSelectResult = 0x8013;

		public const ushort OpZoneIdentify = 0x8020;

		public const ushort OpPlayerEnter = 0x8040;

		public const ushort OpPlayerMove = 0x8041;

		public const ushort OpPlayerLeave = 0x8042;

		public const ushort OpPlayerAttack = 0x8043;

		public const ushort OpPlayerHp = 0x8044;

		public const ushort OpPlayerChat = 0x8045;

		public const ushort OpMobSpawn = 0x8046;

		public const ushort OpMobMove = 0x8047;

		public const ushort OpMobHp = 0x8048;

		public const ushort OpMobDeath = 0x8049;

		public const ushort OpDropSpawn = 0x804A;

		public const ushort OpDropRemove = 0x804B;

		public const ushort OpSkillResult = 0x804C;

		public const ushort OpInventory = 0x804D;

		public const ushort OpSkillList = 0x804E;

		public const ushort OpPlayerRespawn = 0x804F;

		public const ushort OpEquipResult = 0x8050;

		public const ushort OpBuffApply = 0x8051;

		public const ushort OpBuffUpdate = 0x8052;

		public const ushort OpAilmentApply = 0x8053;

		public const ushort OpAilmentUpdate = 0x8054;

		// P5 quests + NPC talk (mirror the server protocol)
		public const ushort OpQuestAction = 0x0200;

		public const ushort OpTalkOpen = 0x0201;

		public const ushort OpTalkAnswer = 0x0202;

		// friends + clubs (P5) — mirror the server protocol
		public const ushort OpFriendListReq = 0x0300;

		public const ushort OpFriendAdd = 0x0301;

		public const ushort OpFriendAccept = 0x0302;

		public const ushort OpFriendRemove = 0x0303;

		public const ushort OpClubInfoReq = 0x0310;

		public const ushort OpClubCreate = 0x0311;

		public const ushort OpClubInvite = 0x0312;

		public const ushort OpClubAccept = 0x0313;

		public const ushort OpClubLeave = 0x0314;

		public const ushort OpClubKick = 0x0315;

		public const ushort OpQuestState = 0x8200;

		public const ushort OpQuestProgress = 0x8201;

		public const ushort OpQuestResult = 0x8202;

		public const ushort OpNpcList = 0x8203;

		public const ushort OpTalkDialog = 0x8204;

		// friends + clubs (P5) S2C
		public const ushort OpFriendList = 0x8300;

		public const ushort OpFriendAddLure = 0x8301;

		public const ushort OpFriendFeedback = 0x8302;

		public const ushort OpFriendState = 0x8303;

		public const ushort OpClubInfo = 0x8310;

		public const ushort OpClubInviteAsk = 0x8311;

		public const ushort OpClubResult = 0x8312;

		public const ushort OpClubMemberEvent = 0x8313;

		public const ushort OpError = 0x80ff;

		private const uint KeyMix = 1511506921u;

		private const int HeartbeatIntervalMs = 30000;

		private readonly ConcurrentQueue<Action> _mainQueue = new ConcurrentQueue<Action>();

		private TcpClient _tcp;

		private Thread _thread;

		private volatile bool _running;

		public class ZoneInfo
		{
			public int map;

			public int gate;

			public float x;

			public float y;

			public float z;

			public string zoneHost;

			public int zonePort;
		}

		public class RemotePlayerInfo
		{
			public string id;

			public string name;

			public int cls;

			public int school;

			public int gender;

			public int hair;

			public int face;

			public int level;

			public float x;

			public float y;

			public float z;

			public float dir;
		}

		public Action<RemotePlayerInfo> OnPlayerEnter;

		public Action<string, float, float, float, float, bool> OnPlayerMove;

		public Action<string> OnPlayerLeave;

		public Action<string, int, float> OnPlayerAttack;

		public Action<string, int, int> OnPlayerHp;

		public Action<string, string, string> OnPlayerChat;

		public class MobInfo
		{
			public string id;

			public string template;

			public string name;

			public string prefab;

			public float x;

			public float y;

			public float z;

			public float dir;

			public int hp;

			public int maxHp;

			public int level;

			/// <summary>Client-authoritative mob (spawned by a local MobSpawner, not the server). Drives AI + local death/loot.</summary>
			public bool local;

			/// <summary>Exp awarded to the killer. Client-side spawner fills this; server mobs leave it 0.</summary>
			public int exp;
		}

		public class DropInfo
		{
			public string id;

			public int mainId;

			public int subId;

			public int count;

			public int gold;

			public float x;

			public float y;

			public float z;

			/// <summary>Client-authoritative drop (spawned by the local loot table). Picked up without a server round-trip.</summary>
			public bool local;

			/// <summary>Rarity tier used for the label colour (0 common … 4 legendary).</summary>
			public int rarity;
		}

		// ---- P5 quest/talk payloads (server QUEST_STATE/PROGRESS/RESULT/NPC_LIST/TALK_DIALOG)

		public class QuestSyncInfo
		{
			public int[] activeIds;

			public int[] activeSteps;

			public int[] activeKills;

			public int[] endedIds;
		}

		public class QuestProgressInfo
		{
			public int questId;

			public int step;

			public int kill;

			public int flags;
		}

		public class QuestResultInfo
		{
			public string action;

			public int questId;

			public bool ok;

			public string reason;
		}

		public class NpcInfo
		{
			public string id;

			public string name;

			public int mainId;

			public int subId;

			public float x;

			public float z;
		}

		public class TalkAnswerInfo
		{
			public int id;

			public string text;
		}

		public class TalkDialogInfo
		{
			public string npc;

			public string name;

			public int statementId;

			public string text;

			public TalkAnswerInfo[] answers;
		}

		public class FriendInfo
		{
			public string name;

			public int level;

			public int cls;

			public int school;

			public bool online;

			public string state;
		}

		public class ClubMemberInfo
		{
			public string name;

			public int level;

			public int cls;

			public int rank;

			public bool online;
		}

		public class ClubInfoData
		{
			public bool hasClub;

			public int id;

			public string name;

			public string notice;

			public string masterId;

			public ClubMemberInfo[] members;

			public int[] inviteClubIds;

			public string[] inviteClubNames;

			public string[] inviteFrom;
		}

		public class InventoryItemInfo
		{
			public int rowId;

			public int mainId;

			public int subId;

			public int count;

			public int slot;

			public int position;
		}

		public class SkillInfo
		{
			public int mainId;

			public int subId;

			public string name;

			public string type;

			public int power;

			public int cooldownMs;

			public int mpCost;

			public int spCost;

			public int range;

			public int grade;

			public int maxLevel;

			public int skillLevel;
		}

		public class SkillResult
		{
			public bool ok;

			public string reason;

			public string caster;

			public int mainId;

			public int subId;

			public int skillLevel;

			public string target;

			public int damage;

			public int heal;

			public int targetHp;

			public int targetMaxHp;

			public bool killed;

			public int exp;

			public int casterMp;

			public int casterMaxMp;

			public int casterSp;

			public int casterMaxSp;

			public int needed;

			public int have;

			public int grade;
		}

		/// <summary>A timed buff (SKILLFACT) held in one of 14 slots.</summary>
		public class BuffInfo
		{
			public int slot;

			public int mainId;

			public int subId;

			public string name;

			public int level;

			public float life;

			public float maxLife;

			public bool refreshed;
		}

		/// <summary>A status ailment (STATEBLOW): NUMB/STUN/STONE/FROZEN/POISON/…</summary>
		public class AilmentInfo
		{
			public int type;

			public string name;

			public float life;

			public float maxLife;

			public float var1;

			public float var2;

			/// <summary>Who it's on: "self" or a mob id.</summary>
			public string targetId;
		}

		/// <summary>Full buff/ailment snapshot (sent on update/expiry).</summary>
		public class BuffSnapshot
		{
			public BuffInfo[] buffs;

			public AilmentInfo[] ailments;
		}

		public Action<MobInfo> OnMobSpawn;

		public Action<string, float, float, float, float> OnMobMove;

		public Action<string, int, int> OnMobHp;

		public Action<string, float, float, float> OnMobDeath;

		public Action<DropInfo> OnDropSpawn;

		public Action<string> OnDropRemove;

		public Action<SkillResult> OnSkillResult;

		public Action<int, InventoryItemInfo[], InventoryItemInfo[]> OnInventory;

		public Action<bool, string, string, int, int, int> OnEquipResult;

		public Action<SkillInfo[], int> OnSkillList;

		public Action<int, int, int, int, int, int> OnSelfVitals;

		public Action<float, float, float, int, int, int, int> OnRespawn;

		public Action<BuffInfo> OnBuffApply;

		public Action<BuffSnapshot> OnBuffUpdate;

		public Action<AilmentInfo> OnAilmentApply;

		public Action<BuffSnapshot> OnAilmentUpdate;

		// P5 quests + NPC talk
		public Action<QuestSyncInfo> OnQuestState;

		public Action<QuestProgressInfo> OnQuestProgress;

		public Action<QuestResultInfo> OnQuestResult;

		public Action<NpcInfo[]> OnNpcList;

		public Action<TalkDialogInfo> OnTalkDialog;

		// P5 friends + clubs
		public Action<FriendInfo[]> OnFriendList;

		public Action<string> OnFriendAddLure;

		public Action<string, bool, string, string> OnFriendFeedback;

		public Action<string, bool> OnFriendState;

		public Action<ClubInfoData> OnClubInfo;

		public Action<string, string> OnClubInviteAsk;

		public Action<string, bool, string> OnClubResult;

		public Action<string, string> OnClubMemberEvent;

		/// <summary>Character id of this session (set before ConnectAndEnter).</summary>
		public string SelfId { get; private set; }

		private readonly object _writeLock = new object();

		private NetworkStream _stream;

		private uint _nonce;

		public void ConnectAndEnter(string host, int port, string handoffToken, string characterId, int gender, Action<ZoneInfo> onEntered, Action<string> onError)
		{
			SelfId = characterId;
			_running = true;
			_thread = new Thread(delegate()
			{
				try
				{
					NetworkStream stream;
					uint nonce;
					ZoneInfo zone = Run(host, port, handoffToken, characterId, gender, out stream, out nonce);
					_mainQueue.Enqueue(delegate()
					{
						if (onEntered != null)
						{
							onEntered(zone);
						}
					});
					SessionLoop(stream, nonce, onError);
				}
				catch (Exception ex)
				{
					_mainQueue.Enqueue(delegate()
					{
						if (onError != null)
						{
							onError(ex.Message);
						}
					});
				}
			});
			_thread.IsBackground = true;
			_thread.Start();
		}

		/// <summary>
		/// Report the local player's transform to the server (main thread). The
		/// server rebroadcasts it to the other players on the same map.
		/// </summary>
		public void SendMove(float x, float y, float z, float dir, bool run)
		{
			NetworkStream stream = _stream;
			if (!_running || stream == null)
			{
				return;
			}
			string json = "{\"x\":" + F(x) + ",\"y\":" + F(y) + ",\"z\":" + F(z) + ",\"dir\":" + F(dir) + ",\"run\":" + (run ? "1" : "0") + "}";
			try
			{
				lock (_writeLock)
				{
					Send(stream, OpMove, Encoding.UTF8.GetBytes(json), true);
				}
			}
			catch
			{
				// Socket died — the session loop reports the disconnect.
			}
		}

		public void SendAttack(int combo, float dir, string targetId)
		{
			NetworkStream stream = _stream;
			if (!_running || stream == null)
			{
				return;
			}
			if (combo < 1)
			{
				combo = 1;
			}
			if (combo > 3)
			{
				combo = 3;
			}
			string json = "{\"combo\":" + combo + ",\"dir\":" + F(dir) + ",\"target\":" + (string.IsNullOrEmpty(targetId) ? "null" : "\"" + EscapeJson(targetId) + "\"") + "}";
			try
			{
				lock (_writeLock)
				{
					Send(stream, OpAttack, Encoding.UTF8.GetBytes(json), true);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("[WorldTcpClient] SendAttack failed: " + ex.Message);
			}
		}

		public void SendChat(string text)
		{
			NetworkStream stream = _stream;
			if (!_running || stream == null || string.IsNullOrEmpty(text))
			{
				return;
			}
			string json = "{\"text\":\"" + EscapeJson(text) + "\"}";
			try
			{
				lock (_writeLock)
				{
					Send(stream, OpChat, Encoding.UTF8.GetBytes(json), true);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("[WorldTcpClient] SendChat failed: " + ex.Message);
			}
		}

		public void SendEquip(int mainId, int subId, int position, bool unequip = false)
		{
			NetworkStream stream = _stream;
			if (!_running || stream == null)
			{
				return;
			}
			string action = unequip ? "unequip" : "equip";
			string json = "{\"action\":\"" + action + "\",\"mainId\":" + mainId + ",\"subId\":" + subId + ",\"position\":" + position + "}";
			try
			{
				lock (_writeLock)
				{
					Send(stream, OpEquipRequest, Encoding.UTF8.GetBytes(json), true);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("[WorldTcpClient] SendEquip failed: " + ex.Message);
			}
		}

		// ---- P5 quest + NPC talk sends

		public void SendQuestAction(string action, int questId)
		{
			NetworkStream stream = _stream;
			if (!_running || stream == null)
			{
				return;
			}
			string json = "{\"action\":\"" + EscapeJson(action) + "\",\"questId\":" + questId + "}";
			try
			{
				lock (_writeLock)
				{
					Send(stream, OpQuestAction, Encoding.UTF8.GetBytes(json), true);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("[WorldTcpClient] SendQuestAction failed: " + ex.Message);
			}
		}

		public void SendTalkOpen(string npcId)
		{
			NetworkStream stream = _stream;
			if (!_running || stream == null || string.IsNullOrEmpty(npcId))
			{
				return;
			}
			string json = "{\"npc\":\"" + EscapeJson(npcId) + "\"}";
			try
			{
				lock (_writeLock)
				{
					Send(stream, OpTalkOpen, Encoding.UTF8.GetBytes(json), true);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("[WorldTcpClient] SendTalkOpen failed: " + ex.Message);
			}
		}

		public void SendTalkAnswer(string npcId, int statementId, int answerId)
		{
			NetworkStream stream = _stream;
			if (!_running || stream == null || string.IsNullOrEmpty(npcId))
			{
				return;
			}
			string json = "{\"npc\":\"" + EscapeJson(npcId) + "\",\"statementId\":" + statementId + ",\"answerId\":" + answerId + "}";
			try
			{
				lock (_writeLock)
				{
					Send(stream, OpTalkAnswer, Encoding.UTF8.GetBytes(json), true);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("[WorldTcpClient] SendTalkAnswer failed: " + ex.Message);
			}
		}

		// ---- P5 friends + clubs sends

		private void SendSocial(ushort opcode, string json, string label)
		{
			NetworkStream stream = _stream;
			if (!_running || stream == null)
			{
				return;
			}
			try
			{
				lock (_writeLock)
				{
					Send(stream, opcode, Encoding.UTF8.GetBytes(json), true);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("[WorldTcpClient] " + label + " failed: " + ex.Message);
			}
		}

		public void SendFriendListRequest()
		{
			SendSocial(OpFriendListReq, "{}", "SendFriendListRequest");
		}

		public void SendFriendAdd(string name)
		{
			SendSocial(OpFriendAdd, "{\"name\":\"" + EscapeJson(name) + "\"}", "SendFriendAdd");
		}

		public void SendFriendAccept(string name)
		{
			SendSocial(OpFriendAccept, "{\"name\":\"" + EscapeJson(name) + "\"}", "SendFriendAccept");
		}

		public void SendFriendRemove(string name)
		{
			SendSocial(OpFriendRemove, "{\"name\":\"" + EscapeJson(name) + "\"}", "SendFriendRemove");
		}

		public void SendClubInfoRequest()
		{
			SendSocial(OpClubInfoReq, "{}", "SendClubInfoRequest");
		}

		public void SendClubCreate(string name)
		{
			SendSocial(OpClubCreate, "{\"name\":\"" + EscapeJson(name) + "\"}", "SendClubCreate");
		}

		public void SendClubInvite(string name)
		{
			SendSocial(OpClubInvite, "{\"name\":\"" + EscapeJson(name) + "\"}", "SendClubInvite");
		}

		public void SendClubAccept()
		{
			SendSocial(OpClubAccept, "{}", "SendClubAccept");
		}

		public void SendClubLeave()
		{
			SendSocial(OpClubLeave, "{}", "SendClubLeave");
		}

		public void SendClubKick(string name)
		{
			SendSocial(OpClubKick, "{\"name\":\"" + EscapeJson(name) + "\"}", "SendClubKick");
		}

		public void SendSkillLevelUp(int mainId, int subId)
		{
			NetworkStream stream = _stream;
			if (!_running || stream == null)
			{
				return;
			}
			string json = "{\"action\":\"levelUp\",\"mainId\":" + mainId + ",\"subId\":" + subId + "}";
			try
			{
				lock (_writeLock)
				{
					Send(stream, OpSkillUse, Encoding.UTF8.GetBytes(json), true);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("[WorldTcpClient] SendSkillLevelUp failed: " + ex.Message);
			}
		}

		/// <summary>Open/use a bag item (box prompt -> server rolls the randomBox table).</summary>
		public void SendUseItem(int mainId, int subId)
		{
			NetworkStream stream = _stream;
			if (!_running || stream == null)
			{
				return;
			}
			string json = "{\"mainId\":" + mainId + ",\"subId\":" + subId + "}";
			try
			{
				lock (_writeLock)
				{
					Send(stream, OpUseItem, Encoding.UTF8.GetBytes(json), true);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("[WorldTcpClient] SendUseItem failed: " + ex.Message);
			}
		}

		private static string F(float v)
		{
			return v.ToString("0.###", System.Globalization.CultureInfo.InvariantCulture);
		}

		public void SendSkillUse(int mainId, int subId, string targetId)
		{
			NetworkStream stream = _stream;
			if (!_running || stream == null)
			{
				return;
			}
			string json = "{\"mainId\":" + mainId + ",\"subId\":" + subId + ",\"target\":" + (string.IsNullOrEmpty(targetId) ? "null" : "\"" + EscapeJson(targetId) + "\"") + "}";
			try
			{
				lock (_writeLock)
				{
					Send(stream, OpSkillUse, Encoding.UTF8.GetBytes(json), true);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("[WorldTcpClient] SendSkillUse failed: " + ex.Message);
			}
		}

		/// <summary>Skill-menu actions: "learn" (new skill) or "levelUp" (rank-up) — server validates.</summary>
		public void SendSkillAction(string action, int mainId, int subId)
		{
			NetworkStream stream = _stream;
			if (!_running || stream == null)
			{
				return;
			}
			string json = "{\"action\":\"" + EscapeJson(action) + "\",\"mainId\":" + mainId + ",\"subId\":" + subId + "}";
			try
			{
				lock (_writeLock)
				{
					Send(stream, OpSkillUse, Encoding.UTF8.GetBytes(json), true);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("[WorldTcpClient] SendSkillAction failed: " + ex.Message);
			}
		}

		public void SendPickup(string dropId)
		{
			NetworkStream stream = _stream;
			if (!_running || stream == null || string.IsNullOrEmpty(dropId))
			{
				return;
			}
			string json = "{\"id\":\"" + EscapeJson(dropId) + "\"}";
			try
			{
				lock (_writeLock)
				{
					Send(stream, OpPickup, Encoding.UTF8.GetBytes(json), true);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("[WorldTcpClient] SendPickup failed: " + ex.Message);
			}
		}

		public void SendInventoryRequest()
		{
			NetworkStream stream = _stream;
			if (!_running || stream == null)
			{
				return;
			}
			try
			{
				lock (_writeLock)
				{
					Send(stream, OpInventoryRequest, new byte[0], true);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("[WorldTcpClient] SendInventoryRequest failed: " + ex.Message);
			}
		}

		public void SendSkillListRequest()
		{
			// Request the server to re-send the full skill list after zoning in.
			UnityEngine.Debug.Log("[WorldTcpClient] SendSkillListRequest called");
			NetworkStream stream = _stream;
			if (!_running || stream == null)
			{
				return;
			}
			try
			{
				lock (_writeLock)
				{
					Send(stream, OpSkillListRequest, new byte[0], true);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("[WorldTcpClient] SendSkillListRequest failed: " + ex.Message);
			}
		}

		private void SessionLoop(NetworkStream stream, uint nonce, Action<string> onError)
		{
			int lastHeartbeat = Environment.TickCount;
			try
			{
				while (_running)
				{
					if (Environment.TickCount - lastHeartbeat >= HeartbeatIntervalMs)
					{
						lastHeartbeat = Environment.TickCount;
						lock (_writeLock)
						{
							Send(stream, OpHeartbeat, new byte[0], true);
						}
					}
					try
					{
						if (!stream.DataAvailable)
						{
							Thread.Sleep(50);
							continue;
						}
						ushort opcode;
						byte[] payload = ReadFrame(stream, out opcode, nonce);
						DispatchSessionFrame(opcode, payload);
					}
					catch (IOException ioEx)
					{
						// Transient read error (e.g. timeout on partial frame) — retry unless socket is dead.
						if (_tcp == null || !_tcp.Client.Connected)
						{
							throw;
						}
						Thread.Sleep(100);
					}
				}
			}
			catch (Exception ex)
			{
				if (_running && onError != null)
				{
					string message = ex.Message;
					_mainQueue.Enqueue(delegate()
					{
						onError("connection lost: " + message);
					});
				}
			}
		}

		private void DispatchSessionFrame(ushort opcode, byte[] payload)
		{
			if (opcode == OpHeartbeatAck)
			{
				return;
			}
			string json = Encoding.UTF8.GetString(payload);
			if (opcode == OpPlayerEnter)
			{
				// Request the skill list now that we've entered the world. The
				// server sends SKILL_LIST on zone join, but it can arrive while
				// the scene is still loading and get lost, so re-request it once
				// the main-thread pump is running again.
				SendSkillListRequest();
				foreach (RemotePlayerInfo info in ParsePlayerEnter(json))
				{
					RemotePlayerInfo captured = info;
					_mainQueue.Enqueue(delegate()
					{
						Action<RemotePlayerInfo> handler = OnPlayerEnter;
						if (handler != null)
						{
							handler(captured);
						}
					});
				}
				return;
			}
			if (opcode == OpPlayerMove)
			{
				string id = ReadString(json, "id", null);
				if (id == null)
				{
					return;
				}
				float x = ReadFloat(json, "x", 0f);
				float y = ReadFloat(json, "y", 0f);
				float z = ReadFloat(json, "z", 0f);
				float dir = ReadFloat(json, "dir", 0f);
				bool run = ReadInt(json, "run", 0) != 0;
				_mainQueue.Enqueue(delegate()
				{
					Action<string, float, float, float, float, bool> handler = OnPlayerMove;
					if (handler != null)
					{
						handler(id, x, y, z, dir, run);
					}
				});
				return;
			}
			if (opcode == OpPlayerLeave)
			{
				string id = ReadString(json, "id", null);
				if (id == null)
				{
					return;
				}
				_mainQueue.Enqueue(delegate()
				{
					Action<string> handler = OnPlayerLeave;
					if (handler != null)
					{
						handler(id);
					}
				});
				return;
			}
			if (opcode == OpPlayerAttack)
			{
				string id = ReadString(json, "id", null);
				if (id == null)
				{
					return;
				}
				int combo = ReadInt(json, "combo", 1);
				float dir = ReadFloat(json, "dir", 0f);
				_mainQueue.Enqueue(delegate()
				{
					Action<string, int, float> handler = OnPlayerAttack;
					if (handler != null)
					{
						handler(id, combo, dir);
					}
				});
				return;
			}
			if (opcode == OpPlayerHp)
			{
				string id = ReadString(json, "id", null);
				if (id == null)
				{
					return;
				}
				int hp = ReadInt(json, "hp", 0);
				int maxHp = ReadInt(json, "maxHp", 100);
				int mp = ReadInt(json, "mp", 0);
				int maxMp = ReadInt(json, "maxMp", 0);
				int sp = ReadInt(json, "sp", 0);
				int maxSp = ReadInt(json, "maxSp", 0);
				_mainQueue.Enqueue(delegate()
				{
					Action<string, int, int> handler = OnPlayerHp;
					if (handler != null)
					{
						handler(id, hp, maxHp);
					}
					if (id == SelfId)
					{
						Action<int, int, int, int, int, int> vitals = OnSelfVitals;
						if (vitals != null)
						{
							vitals(hp, maxHp, mp, maxMp, sp, maxSp);
						}
					}
				});
				return;
			}
			if (opcode == OpPlayerChat)
			{
				string id = ReadString(json, "id", null);
				string name = ReadString(json, "name", "???");
				string text = ReadString(json, "text", "");
				_mainQueue.Enqueue(delegate()
				{
					Action<string, string, string> handler = OnPlayerChat;
					if (handler != null)
					{
						handler(id ?? "", name, text);
					}
				});
				return;
			}
			if (opcode == OpMobSpawn)
			{
				MobInfo info = new MobInfo();
				info.id = ReadString(json, "id", null);
				if (info.id == null)
				{
					return;
				}
				info.template = ReadString(json, "template", "");
				info.name = ReadString(json, "name", "Mob");
				info.prefab = ReadString(json, "prefab", info.template);
				info.x = ReadFloat(json, "x", 0f);
				info.y = ReadFloat(json, "y", 0f);
				info.z = ReadFloat(json, "z", 0f);
				info.dir = ReadFloat(json, "dir", 0f);
				info.hp = ReadInt(json, "hp", 1);
				info.maxHp = ReadInt(json, "maxHp", 1);
				info.level = ReadInt(json, "level", 1);
				_mainQueue.Enqueue(delegate()
				{
					Action<MobInfo> handler = OnMobSpawn;
					if (handler != null)
					{
						handler(info);
					}
				});
				return;
			}
			if (opcode == OpMobMove)
			{
				string id = ReadString(json, "id", null);
				if (id == null)
				{
					return;
				}
				float x = ReadFloat(json, "x", 0f);
				float y = ReadFloat(json, "y", 0f);
				float z = ReadFloat(json, "z", 0f);
				float dir = ReadFloat(json, "dir", 0f);
				_mainQueue.Enqueue(delegate()
				{
					Action<string, float, float, float, float> handler = OnMobMove;
					if (handler != null)
					{
						handler(id, x, y, z, dir);
					}
				});
				return;
			}
			if (opcode == OpMobHp)
			{
				string id = ReadString(json, "id", null);
				if (id == null)
				{
					return;
				}
				int hp = ReadInt(json, "hp", 0);
				int maxHp = ReadInt(json, "maxHp", 1);
				_mainQueue.Enqueue(delegate()
				{
					Action<string, int, int> handler = OnMobHp;
					if (handler != null)
					{
						handler(id, hp, maxHp);
					}
				});
				return;
			}
			if (opcode == OpMobDeath)
			{
				string id = ReadString(json, "id", null);
				if (id == null)
				{
					return;
				}
				float x = ReadFloat(json, "x", 0f);
				float y = ReadFloat(json, "y", 0f);
				float z = ReadFloat(json, "z", 0f);
				_mainQueue.Enqueue(delegate()
				{
					Action<string, float, float, float> handler = OnMobDeath;
					if (handler != null)
					{
						handler(id, x, y, z);
					}
				});
				return;
			}
			if (opcode == OpDropSpawn)
			{
				DropInfo info = new DropInfo();
				info.id = ReadString(json, "id", null);
				if (info.id == null)
				{
					return;
				}
				info.mainId = ReadInt(json, "mainId", 0);
				info.subId = ReadInt(json, "subId", 0);
				info.count = ReadInt(json, "count", 0);
				info.gold = ReadInt(json, "gold", 0);
				info.x = ReadFloat(json, "x", 0f);
				info.y = ReadFloat(json, "y", 0f);
				info.z = ReadFloat(json, "z", 0f);
				_mainQueue.Enqueue(delegate()
				{
					Action<DropInfo> handler = OnDropSpawn;
					if (handler != null)
					{
						handler(info);
					}
				});
				return;
			}
			if (opcode == OpDropRemove)
			{
				string id = ReadString(json, "id", null);
				if (id == null)
				{
					return;
				}
				_mainQueue.Enqueue(delegate()
				{
					Action<string> handler = OnDropRemove;
					if (handler != null)
					{
						handler(id);
					}
				});
				return;
			}
			if (opcode == OpSkillResult)
			{
				SkillResult result = new SkillResult();
				result.ok = ReadBool(json, "ok", false);
				result.reason = ReadString(json, "reason", null);
				result.caster = ReadString(json, "caster", null);
				result.mainId = ReadInt(json, "mainId", 0);
				result.subId = ReadInt(json, "subId", 0);
				result.skillLevel = ReadInt(json, "skillLevel", 1);
				result.target = ReadString(json, "target", null);
				result.damage = ReadInt(json, "damage", 0);
				result.heal = ReadInt(json, "heal", 0);
				result.targetHp = ReadInt(json, "targetHp", 0);
				result.targetMaxHp = ReadInt(json, "targetMaxHp", 1);
				result.killed = ReadBool(json, "killed", false);
				result.exp = ReadInt(json, "exp", 0);
				result.casterMp = ReadInt(json, "casterMp", 0);
				result.casterMaxMp = ReadInt(json, "casterMaxMp", 0);
				result.casterSp = ReadInt(json, "casterSp", 0);
				result.casterMaxSp = ReadInt(json, "casterMaxSp", 0);
				result.needed = ReadInt(json, "needed", 0);
				result.have = ReadInt(json, "have", 0);
				result.grade = ReadInt(json, "grade", 0);
				_mainQueue.Enqueue(delegate()
				{
					Action<SkillResult> handler = OnSkillResult;
					if (handler != null)
					{
						handler(result);
					}
				});
				return;
			}
			if (opcode == OpEquipResult)
			{
				bool ok = ReadBool(json, "ok", false);
				string reason = ReadString(json, "reason", null);
				string action = ReadString(json, "action", null);
				int position = ReadInt(json, "position", 0);
				int mainId = ReadInt(json, "mainId", 0);
				int subId = ReadInt(json, "subId", 0);
				_mainQueue.Enqueue(delegate()
				{
					Action<bool, string, string, int, int, int> handler = OnEquipResult;
					if (handler != null)
					{
						handler(ok, reason, action, position, mainId, subId);
					}
				});
				return;
			}
			if (opcode == OpInventory)
			{
				int gold = ReadInt(json, "gold", 0);
				InventoryItemInfo[] items = ParseInventoryItems(json);
				InventoryItemInfo[] equipped = ParseEquippedItems(json);
				_mainQueue.Enqueue(delegate()
				{
					Action<int, InventoryItemInfo[], InventoryItemInfo[]> handler = OnInventory;
					if (handler != null)
					{
						handler(gold, items, equipped);
					}
				});
				return;
			}
			if (opcode == OpSkillList)
			{
				SkillInfo[] skills = ParseSkillList(json);
				int skillPoints = ReadInt(json, "skillPoints", -1);
				_mainQueue.Enqueue(delegate()
				{
					Action<SkillInfo[], int> handler = OnSkillList;
					if (handler != null)
					{
						handler(skills, skillPoints);
					}
				});
				return;
			}
			if (opcode == OpBuffApply)
			{
				BuffInfo b = new BuffInfo();
				b.slot = ReadInt(json, "slot", 0);
				b.mainId = ReadInt(json, "mainId", 0);
				b.subId = ReadInt(json, "subId", 0);
				b.name = ReadString(json, "name", null);
				b.level = ReadInt(json, "level", 1);
				b.life = ReadFloat(json, "life", 0f);
				b.maxLife = ReadFloat(json, "maxLife", 0f);
				b.refreshed = ReadBool(json, "refreshed", false);
				_mainQueue.Enqueue(delegate()
				{
					Action<BuffInfo> handler = OnBuffApply;
					if (handler != null)
					{
						handler(b);
					}
				});
				return;
			}
			if (opcode == OpAilmentApply)
			{
				AilmentInfo a = new AilmentInfo();
				a.type = ReadInt(json, "type", 0);
				a.name = ReadString(json, "name", null);
				a.life = ReadFloat(json, "life", 0f);
				a.maxLife = ReadFloat(json, "maxLife", 0f);
				a.var1 = ReadFloat(json, "var1", 0f);
				a.var2 = ReadFloat(json, "var2", 0f);
				a.targetId = ReadString(json, "targetId", null);
				_mainQueue.Enqueue(delegate()
				{
					Action<AilmentInfo> handler = OnAilmentApply;
					if (handler != null)
					{
						handler(a);
					}
				});
				return;
			}
			if (opcode == OpBuffUpdate || opcode == OpAilmentUpdate)
			{
				BuffSnapshot snap = ParseBuffSnapshot(json);
				bool isBuff = opcode == OpBuffUpdate;
				_mainQueue.Enqueue(delegate()
				{
					if (isBuff)
					{
						Action<BuffSnapshot> hb = OnBuffUpdate;
						if (hb != null)
						{
							hb(snap);
						}
					}
					else
					{
						Action<BuffSnapshot> ha = OnAilmentUpdate;
						if (ha != null)
						{
							ha(snap);
						}
					}
				});
				return;
			}
			if (opcode == OpPlayerRespawn)
			{
				float x = ReadFloat(json, "x", 0f);
				float y = ReadFloat(json, "y", 0f);
				float z = ReadFloat(json, "z", 0f);
				int hp = ReadInt(json, "hp", 100);
				int maxHp = ReadInt(json, "maxHp", 100);
				int mp = ReadInt(json, "mp", 100);
				int maxMp = ReadInt(json, "maxMp", 100);
				_mainQueue.Enqueue(delegate()
				{
					Action<float, float, float, int, int, int, int> handler = OnRespawn;
					if (handler != null)
					{
						handler(x, y, z, hp, maxHp, mp, maxMp);
					}
				});
				return;
			}
			if (opcode == OpQuestState)
			{
				QuestSyncInfo sync = ParseQuestSync(json);
				_mainQueue.Enqueue(delegate()
				{
					Action<QuestSyncInfo> handler = OnQuestState;
					if (handler != null)
					{
						handler(sync);
					}
				});
				return;
			}
			if (opcode == OpQuestProgress)
			{
				QuestProgressInfo prog = new QuestProgressInfo();
				prog.questId = ReadInt(json, "questId", 0);
				prog.step = ReadInt(json, "step", 0);
				prog.kill = ReadInt(json, "kill", 0);
				prog.flags = ReadInt(json, "flags", 0);
				_mainQueue.Enqueue(delegate()
				{
					Action<QuestProgressInfo> handler = OnQuestProgress;
					if (handler != null)
					{
						handler(prog);
					}
				});
				return;
			}
			if (opcode == OpQuestResult)
			{
				QuestResultInfo res = new QuestResultInfo();
				res.action = ReadString(json, "action", "");
				res.questId = ReadInt(json, "questId", 0);
				res.ok = ReadInt(json, "ok", 0) != 0 || ReadBool(json, "ok", false);
				res.reason = ReadString(json, "reason", "");
				_mainQueue.Enqueue(delegate()
				{
					Action<QuestResultInfo> handler = OnQuestResult;
					if (handler != null)
					{
						handler(res);
					}
				});
				return;
			}
			if (opcode == OpNpcList)
			{
				NpcInfo[] npcs = ParseNpcList(json);
				_mainQueue.Enqueue(delegate()
				{
					Action<NpcInfo[]> handler = OnNpcList;
					if (handler != null)
					{
						handler(npcs);
					}
				});
				return;
			}
			if (opcode == OpTalkDialog)
			{
				TalkDialogInfo dlg = new TalkDialogInfo();
				dlg.npc = ReadString(json, "npc", "");
				dlg.name = ReadString(json, "name", "");
				dlg.statementId = ReadInt(json, "statementId", 0);
				dlg.text = ReadString(json, "text", "");
				dlg.answers = ParseTalkAnswers(json);
				_mainQueue.Enqueue(delegate()
				{
					Action<TalkDialogInfo> handler = OnTalkDialog;
					if (handler != null)
					{
						handler(dlg);
					}
				});
				return;
			}
			if (opcode == OpFriendList)
			{
				FriendInfo[] friends = ParseFriendList(json);
				_mainQueue.Enqueue(delegate()
				{
					Action<FriendInfo[]> handler = OnFriendList;
					if (handler != null)
					{
						handler(friends);
					}
				});
				return;
			}
			if (opcode == OpFriendAddLure)
			{
				string requester = ReadString(json, "requester", "");
				_mainQueue.Enqueue(delegate()
				{
					Action<string> handler = OnFriendAddLure;
					if (handler != null)
					{
						handler(requester);
					}
				});
				return;
			}
			if (opcode == OpFriendFeedback)
			{
				string action = ReadString(json, "action", "");
				bool ok = ReadInt(json, "ok", 0) != 0 || ReadBool(json, "ok", false);
				string target = ReadString(json, "target", "");
				string reason = ReadString(json, "reason", "");
				_mainQueue.Enqueue(delegate()
				{
					Action<string, bool, string, string> handler = OnFriendFeedback;
					if (handler != null)
					{
						handler(action, ok, target, reason);
					}
				});
				return;
			}
			if (opcode == OpFriendState)
			{
				string fname = ReadString(json, "name", "");
				bool online = ReadInt(json, "online", 0) != 0 || ReadBool(json, "online", false);
				_mainQueue.Enqueue(delegate()
				{
					Action<string, bool> handler = OnFriendState;
					if (handler != null)
					{
						handler(fname, online);
					}
				});
				return;
			}
			if (opcode == OpClubInfo)
			{
				ClubInfoData info = ParseClubInfo(json);
				_mainQueue.Enqueue(delegate()
				{
					Action<ClubInfoData> handler = OnClubInfo;
					if (handler != null)
					{
						handler(info);
					}
				});
				return;
			}
			if (opcode == OpClubInviteAsk)
			{
				string clubName = ReadString(json, "clubName", "");
				string by = ReadString(json, "by", "");
				_mainQueue.Enqueue(delegate()
				{
					Action<string, string> handler = OnClubInviteAsk;
					if (handler != null)
					{
						handler(clubName, by);
					}
				});
				return;
			}
			if (opcode == OpClubResult)
			{
				string action = ReadString(json, "action", "");
				bool ok = ReadInt(json, "ok", 0) != 0 || ReadBool(json, "ok", false);
				string reason = ReadString(json, "reason", "");
				_mainQueue.Enqueue(delegate()
				{
					Action<string, bool, string> handler = OnClubResult;
					if (handler != null)
					{
						handler(action, ok, reason);
					}
				});
				return;
			}
			if (opcode == OpClubMemberEvent)
			{
				string kind = ReadString(json, "kind", "");
				string who = ReadString(json, "name", "");
				_mainQueue.Enqueue(delegate()
				{
					Action<string, string> handler = OnClubMemberEvent;
					if (handler != null)
					{
						handler(kind, who);
					}
				});
				return;
			}
		}

		private static InventoryItemInfo[] ParseInventoryItems(string json)
		{
			List<InventoryItemInfo> list = new List<InventoryItemInfo>();
			int arrKey = json.IndexOf("\"items\"", System.StringComparison.Ordinal);
			if (arrKey < 0)
			{
				return new InventoryItemInfo[0];
			}
			int open = json.IndexOf('[', arrKey);
			int close = (open >= 0) ? json.IndexOf(']', open) : -1;
			if (open < 0 || close < 0)
			{
				return new InventoryItemInfo[0];
			}
			int index = open + 1;
			while (index < close)
			{
				int s = json.IndexOf('{', index);
				if (s < 0 || s > close)
				{
					break;
				}
				int e = json.IndexOf('}', s);
				if (e < 0 || e > close)
				{
					break;
				}
				string obj = json.Substring(s, e - s + 1);
				InventoryItemInfo it = new InventoryItemInfo();
				it.rowId = ReadInt(obj, "rowId", 0);
				it.mainId = ReadInt(obj, "mainId", 0);
				it.subId = ReadInt(obj, "subId", 0);
				it.count = ReadInt(obj, "count", 0);
				it.slot = ReadInt(obj, "slot", 0);
				list.Add(it);
				index = e + 1;
			}
			return list.ToArray();
		}

		private static InventoryItemInfo[] ParseEquippedItems(string json)
		{
			List<InventoryItemInfo> list = new List<InventoryItemInfo>();
			int arrKey = json.IndexOf("\"equipped\"", System.StringComparison.Ordinal);
			if (arrKey < 0)
			{
				return new InventoryItemInfo[0];
			}
			int open = json.IndexOf('[', arrKey);
			int close = (open >= 0) ? json.IndexOf(']', open) : -1;
			if (open < 0 || close < 0)
			{
				return new InventoryItemInfo[0];
			}
			int index = open + 1;
			while (index < close)
			{
				int s = json.IndexOf('{', index);
				if (s < 0 || s > close)
				{
					break;
				}
				int e = json.IndexOf('}', s);
				if (e < 0 || e > close)
				{
					break;
				}
				string obj = json.Substring(s, e - s + 1);
				InventoryItemInfo it = new InventoryItemInfo();
				it.position = ReadInt(obj, "position", 0);
				it.mainId = ReadInt(obj, "mainId", 0);
				it.subId = ReadInt(obj, "subId", 0);
				it.count = ReadInt(obj, "count", 0);
				list.Add(it);
				index = e + 1;
			}
			return list.ToArray();
		}

		private static SkillInfo[] ParseSkillList(string json)
		{
			UnityEngine.Debug.Log("[SkillList raw] len=" + json.Length + " " + ((json.Length > 220) ? json.Substring(0, 220) : json));
			List<SkillInfo> list = new List<SkillInfo>();
			int arrKey = json.IndexOf("\"skills\"", System.StringComparison.Ordinal);
			if (arrKey < 0)
			{
				UnityEngine.Debug.LogWarning("[SkillList] no skills key");
				return new SkillInfo[0];
			}
			int open = json.IndexOf('[', arrKey);
			int close = (open >= 0) ? json.IndexOf(']', open) : -1;
			if (open < 0 || close < 0)
			{
				UnityEngine.Debug.LogWarning("[SkillList] no array bounds");
				return new SkillInfo[0];
			}
			int index = open + 1;
			while (index < close)
			{
				int s = json.IndexOf('{', index);
				if (s < 0 || s > close)
				{
					break;
				}
				int e = json.IndexOf('}', s);
				if (e < 0 || e > close)
				{
					break;
				}
				string obj = json.Substring(s, e - s + 1);
				SkillInfo skill = new SkillInfo();
				skill.mainId = ReadInt(obj, "mainId", 0);
				skill.subId = ReadInt(obj, "subId", 0);
				skill.name = ReadString(obj, "name", "Skill");
				skill.type = ReadString(obj, "type", "damage");
				skill.power = ReadInt(obj, "power", 0);
				skill.cooldownMs = ReadInt(obj, "cooldownMs", 1000);
				skill.mpCost = ReadInt(obj, "mpCost", 0);
				skill.spCost = ReadInt(obj, "spCost", 0);
				skill.range = ReadInt(obj, "range", 30);
				skill.grade = ReadInt(obj, "grade", 0);
				skill.maxLevel = ReadInt(obj, "maxLevel", 9);
				skill.skillLevel = ReadInt(obj, "skillLevel", 1);
				list.Add(skill);
				index = e + 1;
			}
			UnityEngine.Debug.Log("[SkillList parsed] count=" + list.Count);
			return list.ToArray();
		}

		/// <summary>
		/// BUFF_UPDATE / AILMENT_UPDATE payload: {"buffs":[{slot,mainId,subId,name,level,life,maxLife},…],
		/// "ailments":[{type,name,life,maxLife,var1,var2},…]}.
		/// </summary>
		private static BuffSnapshot ParseBuffSnapshot(string json)
		{
			BuffSnapshot snap = new BuffSnapshot();
			List<BuffInfo> buffs = new List<BuffInfo>();
			List<AilmentInfo> ailments = new List<AilmentInfo>();

			int bKey = json.IndexOf("\"buffs\"", System.StringComparison.Ordinal);
			if (bKey >= 0)
			{
				int open = json.IndexOf('[', bKey);
				int close = (open >= 0) ? json.IndexOf(']', open) : -1;
				if (open >= 0 && close >= 0)
				{
					int index = open + 1;
					while (index < close)
					{
						int s = json.IndexOf('{', index);
						if (s < 0 || s > close) break;
						int e = json.IndexOf('}', s);
						if (e < 0 || e > close) break;
						string obj = json.Substring(s, e - s + 1);
						BuffInfo b = new BuffInfo();
						b.slot = ReadInt(obj, "slot", 0);
						b.mainId = ReadInt(obj, "mainId", 0);
						b.subId = ReadInt(obj, "subId", 0);
						b.name = ReadString(obj, "name", null);
						b.level = ReadInt(obj, "level", 1);
						b.life = ReadFloat(obj, "life", 0f);
						b.maxLife = ReadFloat(obj, "maxLife", 0f);
						buffs.Add(b);
						index = e + 1;
					}
				}
			}

			int aKey = json.IndexOf("\"ailments\"", System.StringComparison.Ordinal);
			if (aKey >= 0)
			{
				int open = json.IndexOf('[', aKey);
				int close = (open >= 0) ? json.IndexOf(']', open) : -1;
				if (open >= 0 && close >= 0)
				{
					int index = open + 1;
					while (index < close)
					{
						int s = json.IndexOf('{', index);
						if (s < 0 || s > close) break;
						int e = json.IndexOf('}', s);
						if (e < 0 || e > close) break;
						string obj = json.Substring(s, e - s + 1);
						AilmentInfo a = new AilmentInfo();
						a.type = ReadInt(obj, "type", 0);
						a.name = ReadString(obj, "name", null);
						a.life = ReadFloat(obj, "life", 0f);
						a.maxLife = ReadFloat(obj, "maxLife", 0f);
						a.var1 = ReadFloat(obj, "var1", 0f);
						a.var2 = ReadFloat(obj, "var2", 0f);
						ailments.Add(a);
						index = e + 1;
					}
				}
			}

			snap.buffs = buffs.ToArray();
			snap.ailments = ailments.ToArray();
			return snap;
		}

		/// <summary>
		/// PLAYER_ENTER arrives either as one object (a single joiner) or as
		/// {"players":[...]} (the snapshot sent to the joiner).
		/// </summary>
		private static RemotePlayerInfo[] ParsePlayerEnter(string json)
		{
			int arrayStart = json.IndexOf("\"players\"", StringComparison.Ordinal);
			if (arrayStart < 0)
			{
				RemotePlayerInfo single = ParsePlayer(json);
				return (single != null) ? new RemotePlayerInfo[] { single } : new RemotePlayerInfo[0];
			}
			int open = json.IndexOf('[', arrayStart);
			int close = (open >= 0) ? json.IndexOf(']', open) : -1;
			if (open < 0 || close < 0)
			{
				return new RemotePlayerInfo[0];
			}
			System.Collections.Generic.List<RemotePlayerInfo> list = new System.Collections.Generic.List<RemotePlayerInfo>();
			int index = open + 1;
			while (index < close)
			{
				int s = json.IndexOf('{', index);
				if (s < 0 || s > close)
				{
					break;
				}
				int e = json.IndexOf('}', s);
				if (e < 0 || e > close)
				{
					break;
				}
				RemotePlayerInfo info = ParsePlayer(json.Substring(s, e - s + 1));
				if (info != null)
				{
					list.Add(info);
				}
				index = e + 1;
			}
			return list.ToArray();
		}

		private static RemotePlayerInfo ParsePlayer(string json)
		{
			string id = ReadString(json, "id", null);
			if (id == null)
			{
				return null;
			}
			RemotePlayerInfo info = new RemotePlayerInfo();
			info.id = id;
			info.name = ReadString(json, "name", "???");
			info.cls = ReadInt(json, "cls", 1);
			info.school = ReadInt(json, "school", 0);
			info.gender = ReadInt(json, "gender", 0);
			info.hair = ReadInt(json, "hair", 0);
			info.face = ReadInt(json, "face", 0);
			info.level = ReadInt(json, "level", 1);
			info.x = ReadFloat(json, "x", 0f);
			info.y = ReadFloat(json, "y", 0f);
			info.z = ReadFloat(json, "z", 0f);
			info.dir = ReadFloat(json, "dir", 0f);
			return info;
		}

		private QuestSyncInfo ParseQuestSync(string json)
		{
			QuestSyncInfo sync = new QuestSyncInfo();
			sync.activeIds = ParseIntArray(json, "active", "questId");
			sync.activeSteps = ParseIntArray(json, "active", "step");
			sync.activeKills = ParseIntArray(json, "active", "kill");
			sync.endedIds = ParseIntArray(json, "ended", "questId");
			return sync;
		}

		/// <summary>Parses an array of objects: reads one numeric key per element.</summary>
		private static int[] ParseIntArray(string json, string arrayKey, string elemKey)
		{
			List<int> list = new List<int>();
			int arrKey = json.IndexOf("\"" + arrayKey + "\"", StringComparison.Ordinal);
			if (arrKey < 0)
			{
				return new int[0];
			}
			int open = json.IndexOf('[', arrKey);
			int close = (open >= 0) ? json.IndexOf(']', open) : -1;
			if (open < 0 || close < 0)
			{
				return new int[0];
			}
			int index = open + 1;
			while (index < close)
			{
				int s = json.IndexOf('{', index);
				if (s < 0 || s > close)
				{
					break;
				}
				int e = json.IndexOf('}', s);
				if (e < 0 || e > close)
				{
					break;
				}
				list.Add(ReadInt(json.Substring(s, e - s + 1), elemKey, 0));
				index = e + 1;
			}
			return list.ToArray();
		}

		private static NpcInfo[] ParseNpcList(string json)
		{
			List<NpcInfo> list = new List<NpcInfo>();
			int arrKey = json.IndexOf("\"npcs\"", StringComparison.Ordinal);
			if (arrKey < 0)
			{
				return new NpcInfo[0];
			}
			int open = json.IndexOf('[', arrKey);
			if (open < 0)
			{
				return new NpcInfo[0];
			}
			// depth-aware close: elements contain inner arrays (crow:[m,s]) that would
			// fool a first-']' scan
			int close = -1;
			int depth = 0;
			for (int k = open; k < json.Length; k++)
			{
				if (json[k] == '[')
				{
					depth++;
				}
				else if (json[k] == ']')
				{
					depth--;
					if (depth == 0)
					{
						close = k;
						break;
					}
				}
			}
			if (close < 0)
			{
				return new NpcInfo[0];
			}
			int index = open + 1;
			while (index < close)
			{
				int s = json.IndexOf('{', index);
				if (s < 0 || s > close)
				{
					break;
				}
				int e = json.IndexOf('}', s);
				if (e < 0 || e > close)
				{
					break;
				}
				string body = json.Substring(s, e - s + 1);
				NpcInfo info = new NpcInfo();
				info.id = ReadString(body, "id", null);
				info.name = ReadString(body, "name", "NPC");
				info.x = ReadFloat(body, "x", 0f);
				info.z = ReadFloat(body, "z", 0f);
				int crowKey = body.IndexOf("\"crow\"", StringComparison.Ordinal);
				if (crowKey >= 0)
				{
					int arr = body.IndexOf('[', crowKey);
					int arrEnd = (arr >= 0) ? body.IndexOf(']', arr) : -1;
					if (arr >= 0 && arrEnd > arr)
					{
						string pair = body.Substring(arr + 1, arrEnd - arr - 1);
						string[] parts = pair.Split(',');
						if (parts.Length > 0)
						{
							int.TryParse(parts[0].Trim(), out info.mainId);
						}
						if (parts.Length > 1)
						{
							int.TryParse(parts[1].Trim(), out info.subId);
						}
					}
				}
				if (!string.IsNullOrEmpty(info.id))
				{
					list.Add(info);
				}
				index = e + 1;
			}
			return list.ToArray();
		}

		private static TalkAnswerInfo[] ParseTalkAnswers(string json)
		{
			List<TalkAnswerInfo> list = new List<TalkAnswerInfo>();
			int arrKey = json.IndexOf("\"answers\"", StringComparison.Ordinal);
			if (arrKey < 0)
			{
				return new TalkAnswerInfo[0];
			}
			int open = json.IndexOf('[', arrKey);
			int close = (open >= 0) ? json.IndexOf(']', open) : -1;
			if (open < 0 || close < 0)
			{
				return new TalkAnswerInfo[0];
			}
			int index = open + 1;
			while (index < close)
			{
				int s = json.IndexOf('{', index);
				if (s < 0 || s > close)
				{
					break;
				}
				int e = json.IndexOf('}', s);
				if (e < 0 || e > close)
				{
					break;
				}
				string body = json.Substring(s, e - s + 1);
				TalkAnswerInfo a = new TalkAnswerInfo();
				a.id = ReadInt(body, "id", 0);
				a.text = ReadString(body, "text", "...");
				list.Add(a);
				index = e + 1;
			}
			return list.ToArray();
		}

		private static FriendInfo[] ParseFriendList(string json)
		{
			List<FriendInfo> list = new List<FriendInfo>();
			foreach (string body in SplitObjectArray(json, "friends"))
			{
				FriendInfo f = new FriendInfo();
				f.name = ReadString(body, "name", "");
				f.level = ReadInt(body, "level", 1);
				f.cls = ReadInt(body, "cls", 1);
				f.school = ReadInt(body, "school", 0);
				f.online = ReadInt(body, "online", 0) != 0 || ReadBool(body, "online", false);
				f.state = ReadString(body, "state", "accepted");
				if (!string.IsNullOrEmpty(f.name))
				{
					list.Add(f);
				}
			}
			return list.ToArray();
		}

		private static ClubInfoData ParseClubInfo(string json)
		{
			ClubInfoData info = new ClubInfoData();
			info.hasClub = ReadInt(json, "hasClub", 0) != 0 || ReadBool(json, "hasClub", false);
			info.id = ReadInt(json, "id", 0);
			info.name = ReadString(json, "name", "");
			info.notice = ReadString(json, "notice", "");
			info.masterId = ReadString(json, "masterId", null);
			List<ClubMemberInfo> members = new List<ClubMemberInfo>();
			foreach (string body in SplitObjectArray(json, "members"))
			{
				ClubMemberInfo m = new ClubMemberInfo();
				m.name = ReadString(body, "name", "");
				m.level = ReadInt(body, "level", 1);
				m.cls = ReadInt(body, "cls", 1);
				m.rank = ReadInt(body, "rank", 1);
				m.online = ReadInt(body, "online", 0) != 0 || ReadBool(body, "online", false);
				if (!string.IsNullOrEmpty(m.name))
				{
					members.Add(m);
				}
			}
			info.members = members.ToArray();
			List<int> invIds = new List<int>();
			List<string> invNames = new List<string>();
			List<string> invFrom = new List<string>();
			foreach (string body in SplitObjectArray(json, "invites"))
			{
				invIds.Add(ReadInt(body, "clubId", 0));
				invNames.Add(ReadString(body, "clubName", ""));
				invFrom.Add(ReadString(body, "by", ""));
			}
			info.inviteClubIds = invIds.ToArray();
			info.inviteClubNames = invNames.ToArray();
			info.inviteFrom = invFrom.ToArray();
			return info;
		}

		/// <summary>Splits a JSON array of flat objects ("key": [...]) into per-element bodies.</summary>
		private static List<string> SplitObjectArray(string json, string arrayKey)
		{
			List<string> list = new List<string>();
			int arrKey = json.IndexOf("\"" + arrayKey + "\"", StringComparison.Ordinal);
			if (arrKey < 0)
			{
				return list;
			}
			int open = json.IndexOf('[', arrKey);
			if (open < 0)
			{
				return list;
			}
			// depth-aware close (some elements carry inner arrays)
			int close = -1;
			int depth = 0;
			for (int k = open; k < json.Length; k++)
			{
				if (json[k] == '[')
				{
					depth++;
				}
				else if (json[k] == ']')
				{
					depth--;
					if (depth == 0)
					{
						close = k;
						break;
					}
				}
			}
			if (close < 0)
			{
				return list;
			}
			int index = open + 1;
			while (index < close)
			{
				int s = json.IndexOf('{', index);
				if (s < 0 || s > close)
				{
					break;
				}
				int e = json.IndexOf('}', s);
				if (e < 0 || e > close)
				{
					break;
				}
				list.Add(json.Substring(s, e - s + 1));
				index = e + 1;
			}
			return list;
		}

		public void Pump()
		{
			Action action;
			while (_mainQueue.TryDequeue(out action))
			{
				action();
			}
		}

		public void Close()
		{
			_running = false;
			try
			{
				if (_tcp != null)
				{
					_tcp.Close();
				}
			}
			catch
			{
			}
		}

		private ZoneInfo Run(string host, int port, string handoffToken, string characterId, int gender, out NetworkStream outStream, out uint outNonce)
		{
			_tcp = new TcpClient();
			_tcp.NoDelay = true;
			_tcp.ReceiveTimeout = 10000;
			_tcp.SendTimeout = 10000;
			_tcp.Connect(host, port);
			NetworkStream stream = _tcp.GetStream();
			Send(stream, OpHandshake, Encoding.ASCII.GetBytes("KX01"), false);
			ushort opcode;
			byte[] payload = ReadFrame(stream, out opcode, 0u);
			if (opcode != OpHandshakeAck || payload.Length < 8 || Encoding.ASCII.GetString(payload, 0, 4) != "KXOK")
			{
				throw new IOException("bad handshake ack (opcode 0x" + opcode.ToString("X4") + ", " + payload.Length + " bytes)");
			}
			uint nonce = BitConverter.ToUInt32(payload, 4);
			byte[] loginPayload = Encoding.UTF8.GetBytes("{\"token\":\"" + EscapeJson(handoffToken) + "\"}");
			Send(stream, OpLogin, loginPayload, true);
			payload = ReadFrame(stream, out opcode, nonce);
			if (opcode == OpError)
			{
				throw new IOException("login error: " + Encoding.UTF8.GetString(payload));
			}
			if (opcode != OpLoginResult || !ContainsOk(payload))
			{
				throw new IOException("login failed (opcode 0x" + opcode.ToString("X4") + "): " + Encoding.UTF8.GetString(payload));
			}
			byte[] selectPayload = Encoding.UTF8.GetBytes("{\"id\":\"" + EscapeJson(characterId) + "\"}");
			Send(stream, OpCharSelect, selectPayload, true);
			payload = ReadFrame(stream, out opcode, nonce);
			if (opcode != OpCharSelectResult || !ContainsOk(payload))
			{
				throw new IOException("character select failed: " + Encoding.UTF8.GetString(payload));
			}
			Send(stream, OpEnterWorld, Encoding.UTF8.GetBytes("{\"gender\":" + gender + "}"), true);
			payload = ReadFrame(stream, out opcode, nonce);
			if (opcode != OpZoneIdentify)
			{
				throw new IOException("enter world failed (opcode 0x" + opcode.ToString("X4") + "): " + Encoding.UTF8.GetString(payload));
			}
			_stream = stream;
			_nonce = nonce;
			_tcp.ReceiveTimeout = 0;
			outStream = stream;
			outNonce = nonce;
			return ParseZone(Encoding.UTF8.GetString(payload));
		}

		private static ZoneInfo ParseZone(string json)
		{
			ZoneInfo zone = new ZoneInfo();
			zone.map = ReadInt(json, "map", 2);
			zone.gate = ReadInt(json, "gate", 0);
			zone.x = ReadFloat(json, "x", 0f);
			zone.y = ReadFloat(json, "y", 0f);
			zone.z = ReadFloat(json, "z", 0f);
			zone.zoneHost = ReadString(json, "zoneHost", "127.0.0.1");
			zone.zonePort = ReadInt(json, "zonePort", 7777);
			int pos = json.IndexOf("\"position\"", StringComparison.Ordinal);
			if (pos >= 0)
			{
				zone.x = ReadFloatNear(json, pos, "x", zone.x);
				zone.y = ReadFloatNear(json, pos, "y", zone.y);
				zone.z = ReadFloatNear(json, pos, "z", zone.z);
			}
			return zone;
		}

		private static bool ContainsOk(byte[] payload)
		{
			string s = Encoding.UTF8.GetString(payload);
			return s.Contains("\"ok\":true") || s.Contains("\"ok\": true");
		}

		private static int ReadInt(string json, string key, int fallback)
		{
			string token = "\"" + key + "\"";
			int i = json.IndexOf(token, StringComparison.Ordinal);
			if (i < 0)
			{
				return fallback;
			}
			i = json.IndexOf(':', i + token.Length);
			if (i < 0)
			{
				return fallback;
			}
			i++;
			while (i < json.Length && (json[i] == ' ' || json[i] == '\t'))
			{
				i++;
			}
			int start = i;
			while (i < json.Length && (char.IsDigit(json[i]) || json[i] == '-' || json[i] == '+'))
			{
				i++;
			}
			int value;
			if (int.TryParse(json.Substring(start, i - start), out value))
			{
				return value;
			}
			return fallback;
		}

		private static bool ReadBool(string json, string key, bool fallback)
		{
			string token = "\"" + key + "\"";
			int i = json.IndexOf(token, StringComparison.Ordinal);
			if (i < 0)
			{
				return fallback;
			}
			i = json.IndexOf(':', i + token.Length);
			if (i < 0)
			{
				return fallback;
			}
			i++;
			while (i < json.Length && (json[i] == ' ' || json[i] == '\t'))
			{
				i++;
			}
			if (i < json.Length && json[i] == 't')
			{
				return true;
			}
			if (i < json.Length && json[i] == 'f')
			{
				return false;
			}
			return fallback;
		}

		private static float ReadFloat(string json, string key, float fallback)
		{
			return ReadFloatNear(json, 0, key, fallback);
		}

		private static float ReadFloatNear(string json, int from, string key, float fallback)
		{
			string token = "\"" + key + "\"";
			int i = json.IndexOf(token, from, StringComparison.Ordinal);
			if (i < 0)
			{
				return fallback;
			}
			i = json.IndexOf(':', i + token.Length);
			if (i < 0)
			{
				return fallback;
			}
			i++;
			while (i < json.Length && (json[i] == ' ' || json[i] == '\t'))
			{
				i++;
			}
			int start = i;
			while (i < json.Length && (char.IsDigit(json[i]) || json[i] == '-' || json[i] == '+' || json[i] == '.' || json[i] == 'e' || json[i] == 'E'))
			{
				i++;
			}
			float value;
			if (float.TryParse(json.Substring(start, i - start), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out value))
			{
				return value;
			}
			return fallback;
		}

		private static string ReadString(string json, string key, string fallback)
		{
			string token = "\"" + key + "\"";
			int i = json.IndexOf(token, StringComparison.Ordinal);
			if (i < 0)
			{
				return fallback;
			}
			i = json.IndexOf(':', i + token.Length);
			if (i < 0)
			{
				return fallback;
			}
			i = json.IndexOf('"', i);
			if (i < 0)
			{
				return fallback;
			}
			int start = i + 1;
			int end = json.IndexOf('"', start);
			if (end < 0)
			{
				return fallback;
			}
			return json.Substring(start, end - start);
		}

		private static string EscapeJson(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			return s.Replace("\\", "\\\\").Replace("\"", "\\\"");
		}

		/// <summary>
		/// Server decodeJson XORs inbound payloads with the nonce-0 keystream, so
		/// client→server bodies must be pre-encrypted with the same key. The raw
		/// handshake travels in the clear.
		/// </summary>
		private static void Send(NetworkStream stream, ushort opcode, byte[] payload, bool encrypt)
		{
			if (encrypt && payload.Length > 0)
			{
				byte[] key = DeriveKeystream(0u, payload.Length);
				for (int i = 0; i < payload.Length; i++)
				{
					payload[i] = (byte)(payload[i] ^ key[i]);
				}
			}
			byte[] frame = new byte[4 + payload.Length];
			frame[0] = (byte)(payload.Length & 0xff);
			frame[1] = (byte)((payload.Length >> 8) & 0xff);
			frame[2] = (byte)(opcode & 0xff);
			frame[3] = (byte)((opcode >> 8) & 0xff);
			Buffer.BlockCopy(payload, 0, frame, 4, payload.Length);
			stream.Write(frame, 0, frame.Length);
			stream.Flush();
		}

		private byte[] ReadFrame(NetworkStream stream, out ushort opcode, uint nonce)
		{
			byte[] header = ReadExact(stream, 4);
			int length = header[0] | (header[1] << 8);
			opcode = (ushort)(header[2] | (header[3] << 8));
			byte[] payload = (length > 0) ? ReadExact(stream, length) : new byte[0];
			if (nonce != 0u && opcode != OpHandshakeAck && payload.Length > 0)
			{
				byte[] key = DeriveKeystream(nonce, payload.Length);
				for (int i = 0; i < payload.Length; i++)
				{
					payload[i] = (byte)(payload[i] ^ key[i]);
				}
			}
			return payload;
		}

		private static byte[] ReadExact(NetworkStream stream, int count)
		{
			byte[] buffer = new byte[count];
			int offset = 0;
			while (offset < count)
			{
				int read = stream.Read(buffer, offset, count - offset);
				if (read <= 0)
				{
					throw new IOException("connection closed (" + offset + "/" + count + ")");
				}
				offset += read;
			}
			return buffer;
		}

		private static byte[] DeriveKeystream(uint nonce, int length)
		{
			byte[] output = new byte[length];
			uint state = KeyMix ^ nonce;
			for (int i = 0; i < length; i++)
			{
				state ^= state << 13;
				state ^= state >> 17;
				state ^= state << 5;
				output[i] = (byte)(state & 0xff);
			}
			return output;
		}
	}
}

