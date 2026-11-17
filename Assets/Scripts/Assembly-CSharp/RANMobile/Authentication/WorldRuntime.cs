using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// World-session state beyond remote players: mob/drop views, self vitals,
	/// inventory, server skill list, skill results and respawns. Static Queue*
	/// methods are wired to WorldTcpClient events by CharacterLoginManager
	/// (invoked on the main thread via Pump); the instance drains queues each
	/// frame and raises C# events consumed by WorldHUD.
	/// </summary>
	public class WorldRuntime : MonoBehaviour
	{
		public static WorldRuntime Instance;

		public static Transform LocalPlayer;

		public static Camera WorldCamera;

		/// <summary>The local character's name and class (set by WorldEntry on world load).</summary>
		public static string LocalName = "Player";

		public static int ClassId;

		public static string ClassName = "Brawler";

		public static readonly Dictionary<string, WorldTcpClient.MobInfo> MobInfos = new Dictionary<string, WorldTcpClient.MobInfo>();

		public static readonly Dictionary<string, WorldTcpClient.DropInfo> DropInfos = new Dictionary<string, WorldTcpClient.DropInfo>();

		public static readonly Dictionary<string, WorldMobView> MobViews = new Dictionary<string, WorldMobView>();

		public static readonly Dictionary<string, WorldDropView> DropViews = new Dictionary<string, WorldDropView>();

		public static int Gold;

		public static int Level = 1;

		/// <summary>Progress toward the next level. MaxExp comes from the original exptable_max data.</summary>
		public static int Exp;

		public static int MaxExp = 100;

		// ---- character stats (allocated from StatPoints) ----
		// Source model (RAN GLogixEx): POW→attack, STR→HP, SPI→MP, DEX→def/attack, STA→SP.
		// The Character window's labels map onto these as: 'STR'→Pow, 'DEX'→Dex,
		// 'INT'→Spi (magic), 'VIT'→Str (HP), 'STM'→Sta (SP) — the original UI renamed
		// stats by effect; INT itself is inert (0 growth on all four base classes).

		public static int Pow = 1;

		public static int Str = 1;

		public static int Spi = 1;

		public static int Dex = 1;

		public static int Sta = 1;

		/// <summary>INT stat — inert on the four base classes (0 growth), tracked for skill requirements.</summary>
		public static int Int;

		/// <summary>Unspent stat points. Local-authority: granted on level-up, spendable in the Character window.</summary>
		public static int StatPoints;

		/// <summary>Raised when stats/points change so the Character window can refresh.</summary>
		public static event Action StatsChanged;

		public static readonly List<WorldTcpClient.InventoryItemInfo> Items = new List<WorldTcpClient.InventoryItemInfo>();

		public static readonly List<WorldTcpClient.InventoryItemInfo> Equipped = new List<WorldTcpClient.InventoryItemInfo>();

		public static readonly List<WorldTcpClient.SkillInfo> Skills = new List<WorldTcpClient.SkillInfo>();

		/// <summary>Unspent skill points (server-authoritative; arrives with the skill list).</summary>
		public static int SkillPoints;

		// ---- Skill Slots: 3 saved sets x 10 slots (pairs mainId/subId; -1 = empty) ----
		// Persisted to persistentDataPath/skillsets.json like the original's local hotkey saves.
		public const int SkillSetCount = 3;
		public const int SkillSetSize = 10;
		private static readonly int[,] _skillSets = new int[SkillSetCount, SkillSetSize * 2];
		private static bool _skillSetsLoaded;
		private static bool _skillSetsDirty;

		/// <summary>Currently displayed set (Skill Set 1..3) — drives the slots grid + wheel.</summary>
		public static int ActiveSkillSet;

		private static void EnsureSkillSets()
		{
			if (_skillSetsLoaded)
			{
				return;
			}
			_skillSetsLoaded = true;
			for (int s = 0; s < SkillSetCount; s++)
			{
				for (int i = 0; i < SkillSetSize; i++)
				{
					_skillSets[s, i * 2] = -1;
					_skillSets[s, i * 2 + 1] = -1;
				}
			}
			try
			{
				string path = System.IO.Path.Combine(Application.persistentDataPath, "skillsets.json");
				if (System.IO.File.Exists(path))
				{
					// JsonUtility only — the project's Newtonsoft.Json is a gutted decompile.
					SkillSetSave save = JsonUtility.FromJson<SkillSetSave>(System.IO.File.ReadAllText(path));
					if (save != null)
					{
						int[][] arr = { save.set0, save.set1, save.set2 };
						for (int s = 0; s < SkillSetCount && s < arr.Length; s++)
						{
							if (arr[s] == null)
							{
								continue;
							}
							for (int i = 0; i < SkillSetSize && i * 2 < arr[s].Length; i++)
							{
								int m = arr[s][i * 2];
								int sub = arr[s][i * 2 + 1];
								// Passive skills can't be assigned to a slot (the original refuses to
								// start the drag) — drop any stale passive bind saved before that rule.
								SkillObject so = (m >= 0 && sub >= 0) ? SkillMenuData.Get(m, sub) : null;
								if (so != null && so.SkillRole == Core.Enumerators.SkillRoleEnum.PASSIVE)
								{
									m = -1;
									sub = -1;
								}
								_skillSets[s, i * 2] = m;
								_skillSets[s, i * 2 + 1] = sub;
							}
						}
					}
				}
			}
			catch (System.Exception e)
			{
				UnityEngine.Debug.LogWarning("[WorldRuntime] skillsets load failed: " + e.Message);
			}
		}

		/// <summary>Pick a (mainId,subId) into a slot of a set. Returns false when the skill is already bound.</summary>
		public static bool SkillSlotSet(int set, int slot, int mainId, int subId)
		{
			EnsureSkillSets();
			if (set < 0 || set >= SkillSetCount || slot < 0 || slot >= SkillSetSize)
			{
				return false;
			}
			for (int i = 0; i < SkillSetSize; i++)
			{
				if (_skillSets[set, i * 2] == mainId && _skillSets[set, i * 2 + 1] == subId)
				{
					return false;
				}
			}
			_skillSets[set, slot * 2] = mainId;
			_skillSets[set, slot * 2 + 1] = subId;
			_skillSetsDirty = true;
			SaveSkillSets();
			return true;
		}

		public static void SkillSlotClear(int set, int slot)
		{
			EnsureSkillSets();
			if (set < 0 || set >= SkillSetCount || slot < 0 || slot >= SkillSetSize)
			{
				return;
			}
			_skillSets[set, slot * 2] = -1;
			_skillSets[set, slot * 2 + 1] = -1;
			SaveSkillSets();
		}

		/// <summary>mainId at a slot, or -1 when empty.</summary>
		public static int SkillSlotMain(int set, int slot)
		{
			EnsureSkillSets();
			if (set < 0 || set >= SkillSetCount || slot < 0 || slot >= SkillSetSize)
			{
				return -1;
			}
			return _skillSets[set, slot * 2];
		}

		public static int SkillSlotSub(int set, int slot)
		{
			EnsureSkillSets();
			if (set < 0 || set >= SkillSetCount || slot < 0 || slot >= SkillSetSize)
			{
				return -1;
			}
			return _skillSets[set, slot * 2 + 1];
		}

		private static void SaveSkillSets()
		{
			try
			{
				var save = new SkillSetSave();
				save.set0 = new int[SkillSetSize * 2];
				save.set1 = new int[SkillSetSize * 2];
				save.set2 = new int[SkillSetSize * 2];
				int[][] arr = { save.set0, save.set1, save.set2 };
				for (int s = 0; s < SkillSetCount; s++)
				{
					for (int i = 0; i < SkillSetSize * 2; i++)
					{
						arr[s][i] = _skillSets[s, i];
					}
				}
				string path = System.IO.Path.Combine(Application.persistentDataPath, "skillsets.json");
				System.IO.File.WriteAllText(path, JsonUtility.ToJson(save));
			}
			catch (System.Exception e)
			{
				UnityEngine.Debug.LogWarning("[WorldRuntime] skillsets save failed: " + e.Message);
			}
		}

		[System.Serializable]
		private class SkillSetSave
		{
			public int[] set0;
			public int[] set1;
			public int[] set2;
		}

		/// <summary>
		/// The wheel's 10 skills: active set bindings first (skipping empties), falling back to
		/// the learned order for sets the player hasn't customized (initial state = learned order).
		/// </summary>
		public static List<WorldTcpClient.SkillInfo> GetWheelSkills()
		{
			EnsureSkillSets();
			var byId = new Dictionary<long, WorldTcpClient.SkillInfo>();
			for (int i = 0; i < Skills.Count; i++)
			{
				byId[Key(Skills[i].mainId, Skills[i].subId)] = Skills[i];
			}
			bool anyBound = false;
			for (int i = 0; i < SkillSetSize; i++)
			{
				if (_skillSets[ActiveSkillSet, i * 2] >= 0)
				{
					anyBound = true;
					break;
				}
			}
			var outList = new List<WorldTcpClient.SkillInfo>(SkillSetSize);
			if (!anyBound)
			{
				for (int i = 0; i < SkillSetSize && i < Skills.Count; i++)
				{
					outList.Add(Skills[i]);
				}
				return outList;
			}
			for (int i = 0; i < SkillSetSize; i++)
			{
				int m = _skillSets[ActiveSkillSet, i * 2];
				int s = _skillSets[ActiveSkillSet, i * 2 + 1];
				WorldTcpClient.SkillInfo info;
				if (m >= 0 && byId.TryGetValue(Key(m, s), out info))
				{
					outList.Add(info);
				}
				else
				{
					outList.Add(null);
				}
			}
			return outList;
		}

		private static long Key(int mainId, int subId)
		{
			return ((long)mainId << 16) | (uint)subId;
		}

		public static int Hp = 100;

		public static int MaxHp = 100;

		public static int Mp = 100;

		public static int MaxMp = 100;

		public static int Sp = 100;

		public static int MaxSp = 100;

		public static event Action VitalsChanged;

		public static event Action InventoryChanged;

		public static event Action SkillListChanged;

		public static event Action Respawned;

		public static event Action<WorldTcpClient.SkillResult> SkillResultOccurred;

		/// <summary>Raised (main thread) after a mob view is created/refreshed — lets a MobSpawner attach AI.</summary>
		public static event Action<WorldMobView> MobSpawned;

		/// <summary>Raised after exp/level change so the HUD can refresh the EXP bar.</summary>
		public static event Action ExpChanged;

		/// <summary>Raised (main thread) when any mob dies, with its id — lets spawners schedule respawns.</summary>
		public static event Action<string> MobDied;

		/// <summary>Raised whenever the local buff/ailment set changes (apply/expiry).</summary>
		public static event Action BuffsChanged;

		// ---- P2: local character buffs (14 slots) + ailments (≤4) ----
		// Keyed by slot for buffs, by ailment type for ailments — mirrors the server's BuffState.
		public static readonly Dictionary<int, WorldTcpClient.BuffInfo> Buffs = new Dictionary<int, WorldTcpClient.BuffInfo>();

		public static readonly Dictionary<int, WorldTcpClient.AilmentInfo> Ailments = new Dictionary<int, WorldTcpClient.AilmentInfo>();

		/// <summary>Ailments currently afflicting mobs (type → list), for nameplate icons.</summary>
		public static readonly Dictionary<string, List<WorldTcpClient.AilmentInfo>> MobAilments = new Dictionary<string, List<WorldTcpClient.AilmentInfo>>();

		private static readonly List<WorldTcpClient.MobInfo> _qMobSpawn = new List<WorldTcpClient.MobInfo>();

		private static readonly List<string> _qMobDeath = new List<string>();

		private static readonly List<WorldTcpClient.DropInfo> _qDropSpawn = new List<WorldTcpClient.DropInfo>();

		private static readonly List<string> _qDropRemove = new List<string>();

		private static readonly List<WorldTcpClient.SkillResult> _qSkillResult = new List<WorldTcpClient.SkillResult>();

		// ---- P5 quests + NPCs
		/// <summary>Active quests: questId → step/kill (server-authoritative).</summary>
		public static readonly Dictionary<int, int[]> QuestActive = new Dictionary<int, int[]>(); // [step, kill]

		/// <summary>Completed quest ids.</summary>
		public static readonly HashSet<int> QuestEnded = new HashSet<int>();

		// ---- P5 social (friends + clubs)
		public static readonly List<WorldTcpClient.FriendInfo> Friends = new List<WorldTcpClient.FriendInfo>();

		public static WorldTcpClient.ClubInfoData Club;

		public static string PendingClubInviteClub;

		public static string PendingClubInviteBy;

		public static string LastSocialFeedback = string.Empty;

		/// <summary>Raised when friend/club data changes (social windows refresh).</summary>
		public static event Action SocialChanged;

		/// <summary>Static quest NPCs on the current map.</summary>
		public static readonly Dictionary<string, WorldNpcView> NpcViews = new Dictionary<string, WorldNpcView>();

		/// <summary>Raised whenever quest state changes (log/tracker refresh).</summary>
		public static event System.Action QuestsChanged;

		private static WorldTcpClient.TalkDialogInfo _qTalkDialog;

		private static bool _hasTalkDialog;

		public static WorldTcpClient.NpcInfo[] PendingNpcs;

		private struct VitalsEvt
		{
			public int hp;

			public int maxHp;

			public int mp;

			public int maxMp;

			public int sp;

			public int maxSp;
		}

		private struct RespawnEvt
		{
			public float x;

			public float y;

			public float z;

			public int hp;

			public int maxHp;

			public int mp;

			public int maxMp;
		}

		private static VitalsEvt _qVitals;

		private static bool _hasVitals;

		/// <summary>
		/// Set once the server has pushed authoritative vitals (PLAYER_HP for self).
		/// After that, MaxHp/MaxMp/MaxSp stay server-owned — local re-derivation only
		/// refreshes combat numbers (AP/PA/DEF/…) and never clobbers the bars.
		/// </summary>
		private static bool _serverVitalsSeen;

		private static bool _hasInventory;

		private static bool _hasSkillList;

		private static bool _hasEquipResult;

		private static bool LastEquipOk;

		private static string LastEquipReason;

		private static string LastEquipAction;

		private static int LastEquipPosition;

		private static int LastEquipMainId;

		private static int LastEquipSubId;

		private static readonly List<RespawnEvt> _qRespawn = new List<RespawnEvt>();

		private static ItemDatabaseObject _itemDb;

		private static Dictionary<long, string> _itemNameCache;

		private static FieldInfo _skillDataField;

		private static FieldInfo _skillNameField;

		private static readonly Dictionary<string, FieldInfo> _nativeFieldCache = new Dictionary<string, FieldInfo>();

		public static void Clear()
		{
			MobInfos.Clear();
			DropInfos.Clear();
			foreach (WorldMobView v in MobViews.Values)
			{
				if (v != null)
				{
					UnityEngine.Object.Destroy(v.gameObject);
				}
			}
			MobViews.Clear();
			foreach (WorldDropView v in DropViews.Values)
			{
				if (v != null)
				{
					UnityEngine.Object.Destroy(v.gameObject);
				}
			}
			DropViews.Clear();
			Gold = 0;
			Items.Clear();
			Skills.Clear();
			Hp = 100;
			MaxHp = 100;
			Mp = 100;
			MaxMp = 100;
			Sp = 100;
			MaxSp = 100;
			_qMobSpawn.Clear();
			_qMobDeath.Clear();
			_qDropSpawn.Clear();
			_qDropRemove.Clear();
			_qSkillResult.Clear();
			_qRespawn.Clear();
			// P5: drop quest NPCs + quest state on world reset
			foreach (WorldNpcView npcView in NpcViews.Values)
			{
				if (npcView != null)
				{
					UnityEngine.Object.Destroy(npcView.gameObject);
				}
			}
			NpcViews.Clear();
			QuestActive.Clear();
			QuestEnded.Clear();
			PendingNpcs = null;
			_hasTalkDialog = false;
			_qTalkDialog = null;
			Friends.Clear();
			Club = null;
			PendingClubInviteClub = null;
			PendingClubInviteBy = null;
			LastSocialFeedback = string.Empty;
			_hasVitals = false;
			_serverVitalsSeen = false;
			_hasInventory = false;
			_hasSkillList = false;
			SkillPoints = 0;
			_hasEquipResult = false;
			VitalsChanged = null;
			InventoryChanged = null;
			SkillListChanged = null;
			Respawned = null;
			SkillResultOccurred = null;
			MobSpawned = null;
			ExpChanged = null;
			MobDied = null;
			StatsChanged = null;
			Exp = 0;
			MaxExp = ExpForLevel(1);
			Pow = 1;
			Str = 1;
			Spi = 1;
			Dex = 1;
			Sta = 1;
			StatPoints = 0;
			_derived = null;
			LocalLoot.Clear();
			LocalPlayer = null;
			WorldCamera = null;
			LocalName = "Player";
			ClassId = 0;
			ClassName = "Brawler";
			Instance = null;
		}

		// ---- queue methods (called from TCP events on the main thread) ----

		public static void QueueMobSpawn(WorldTcpClient.MobInfo info)
		{
			if (info == null || string.IsNullOrEmpty(info.id))
			{
				return;
			}
			MobInfos[info.id] = info;
			_qMobSpawn.Add(info);
		}

		// ---- P5 quest + NPC queues

		public static void QueueQuestState(WorldTcpClient.QuestSyncInfo sync)
		{
			if (sync == null)
			{
				return;
			}
			QuestActive.Clear();
			for (int i = 0; i < sync.activeIds.Length; i++)
			{
				int step = (sync.activeSteps != null && i < sync.activeSteps.Length) ? sync.activeSteps[i] : 0;
				int kill = (sync.activeKills != null && i < sync.activeKills.Length) ? sync.activeKills[i] : 0;
				QuestActive[sync.activeIds[i]] = new int[2] { step, kill };
			}
			QuestEnded.Clear();
			if (sync.endedIds != null)
			{
				for (int i = 0; i < sync.endedIds.Length; i++)
				{
					QuestEnded.Add(sync.endedIds[i]);
				}
			}
			UnityEngine.Debug.Log("[Quest] state sync active=" + QuestActive.Count + " ended=" + QuestEnded.Count);
			System.Action changed = QuestsChanged;
			if (changed != null)
			{
				changed();
			}
		}

		public static void QueueQuestProgress(WorldTcpClient.QuestProgressInfo prog)
		{
			if (prog == null)
			{
				return;
			}
			QuestActive[prog.questId] = new int[2] { prog.step, prog.kill };
			UnityEngine.Debug.Log("[Quest] progress q=" + prog.questId + " step=" + prog.step + " kill=" + prog.kill);
			System.Action changed = QuestsChanged;
			if (changed != null)
			{
				changed();
			}
		}

		public static void QueueQuestResult(WorldTcpClient.QuestResultInfo res)
		{
			if (res == null)
			{
				return;
			}
			UnityEngine.Debug.Log("[Quest] result " + res.action + " q=" + res.questId + " ok=" + res.ok + " " + res.reason);
			if (res.ok)
			{
				if (res.action == "accept")
				{
					if (!QuestActive.ContainsKey(res.questId))
					{
						QuestActive[res.questId] = new int[2] { 0, 0 };
					}
				}
				else if (res.action == "giveUp")
				{
					QuestActive.Remove(res.questId);
				}
				else if (res.action == "complete")
				{
					QuestActive.Remove(res.questId);
					QuestEnded.Add(res.questId);
				}
			}
			System.Action changed = QuestsChanged;
			if (changed != null)
			{
				changed();
			}
		}

		public static void QueueNpcList(WorldTcpClient.NpcInfo[] npcs)
		{
			PendingNpcs = npcs;
			UnityEngine.Debug.Log("[Quest] npc list " + (npcs != null ? npcs.Length : 0));
		}

		public static void QueueTalkDialog(WorldTcpClient.TalkDialogInfo dlg)
		{
			_qTalkDialog = dlg;
			_hasTalkDialog = true;
		}

		// ---- P5 social queues (friends + clubs)

		public static void QueueFriendList(WorldTcpClient.FriendInfo[] friends)
		{
			Friends.Clear();
			if (friends != null)
			{
				Friends.AddRange(friends);
			}
			UnityEngine.Debug.Log("[Social] friend list " + Friends.Count);
			RaiseSocial();
		}

		public static void QueueFriendAddLure(string requester)
		{
			UnityEngine.Debug.Log("[Social] friend request from " + requester);
			LastSocialFeedback = "Friend request from '" + requester + "'!";
			WorldNet.FireChatLine("", "Friend request from '" + requester + "'.");
			RaiseSocial();
		}

		public static void QueueFriendFeedback(string action, bool ok, string target, string reason)
		{
			UnityEngine.Debug.Log("[Social] friend " + action + " ok=" + ok + " target=" + target + " " + reason);
			if (!ok)
			{
				LastSocialFeedback = action + " failed: " + reason;
			}
			else if (action == "accepted" && !string.IsNullOrEmpty(target))
			{
				LastSocialFeedback = target + " accepted your friend request!";
				WorldNet.FireChatLine("", "'" + target + "' accepted your friend request.");
			}
			else
			{
				LastSocialFeedback = action + " '" + target + "' ok";
			}
			RaiseSocial();
		}

		public static void QueueFriendState(string name, bool online)
		{
			for (int i = 0; i < Friends.Count; i++)
			{
				if (string.Equals(Friends[i].name, name, System.StringComparison.OrdinalIgnoreCase))
				{
					Friends[i].online = online;
					break;
				}
			}
			UnityEngine.Debug.Log("[Social] " + name + (online ? " online" : " offline"));
			RaiseSocial();
		}

		public static void QueueClubInfo(WorldTcpClient.ClubInfoData info)
		{
			Club = info;
			UnityEngine.Debug.Log("[Social] club info hasClub=" + (info != null && info.hasClub) +
				(info != null && info.hasClub ? (" name=" + info.name + " members=" + info.members.Length +
				" invites=" + info.inviteClubIds.Length) : ""));
			RaiseSocial();
		}

		public static void QueueClubInviteAsk(string clubName, string by)
		{
			PendingClubInviteClub = clubName;
			PendingClubInviteBy = by;
			UnityEngine.Debug.Log("[Social] club invite: " + by + " -> " + clubName);
			WorldNet.FireChatLine("", by + " invited you to club '" + clubName + "'. Open the Club window to accept.");
			RaiseSocial();
		}

		public static void QueueClubResult(string action, bool ok, string reason)
		{
			UnityEngine.Debug.Log("[Social] club " + action + " ok=" + ok + " " + reason);
			if (!ok)
			{
				LastSocialFeedback = "club " + action + " failed: " + reason;
			}
			else if (action == "create")
			{
				LastSocialFeedback = "club created";
			}
			RaiseSocial();
			if (action == "create" || action == "accept" || action == "leave" || action == "kick")
			{
				// pull a fresh CLUB_INFO after any membership change
				WorldTcpClient client = WorldNet.Client;
				if (client != null)
				{
					client.SendClubInfoRequest();
				}
			}
		}

		public static void QueueClubMemberEvent(string kind, string name)
		{
			UnityEngine.Debug.Log("[Social] club member event " + kind + " " + name);
			if (!string.IsNullOrEmpty(name))
			{
				WorldNet.FireChatLine("", "Club: " + name + " " +
					(kind == "joined" ? "joined the club." : kind == "left" ? "left the club." : "was removed from the club."));
			}
			WorldTcpClient client = WorldNet.Client;
			if (client != null)
			{
				client.SendClubInfoRequest();
			}
			RaiseSocial();
		}

		private static void RaiseSocial()
		{
			Action handler = SocialChanged;
			if (handler != null)
			{
				handler();
			}
		}

		/// <summary>Spawn pending NPC views (drained by the world pump).</summary>
		public static void SpawnPendingNpcs()
		{
			if (PendingNpcs == null)
			{
				return;
			}
			WorldTcpClient.NpcInfo[] npcs = PendingNpcs;
			PendingNpcs = null;
			for (int i = 0; i < npcs.Length; i++)
			{
				WorldTcpClient.NpcInfo info = npcs[i];
				if (info == null || NpcViews.ContainsKey(info.id))
				{
					continue;
				}
				WorldNpcView view = WorldNpcView.Create(info);
				NpcViews[info.id] = view;
			}
		}

		/// <summary>Talk dialog pushed by the server; the HUD consumes it.</summary>
		public static bool TryTakeTalkDialog(out WorldTcpClient.TalkDialogInfo dlg)
		{
			dlg = _qTalkDialog;
			bool had = _hasTalkDialog;
			_qTalkDialog = null;
			_hasTalkDialog = false;
			return had;
		}

		/// <summary>Clicked an NPC in the world → ask the server for its dialog.</summary>
		public static void OpenNpcTalk(string npcId)
		{
			WorldTcpClient client = WorldNet.Client;
			if (client == null || string.IsNullOrEmpty(npcId))
			{
				return;
			}
			UnityEngine.Debug.Log("[Quest] talk open " + npcId);
			client.SendTalkOpen(npcId);
		}

		public static void QueueMobMove(string id, float x, float y, float z, float dir)
		{
			WorldTcpClient.MobInfo info;
			if (MobInfos.TryGetValue(id, out info))
			{
				info.x = x;
				info.y = y;
				info.z = z;
				info.dir = dir;
			}
		}

		public static void QueueMobHp(string id, int hp, int maxHp)
		{
			WorldTcpClient.MobInfo info;
			if (MobInfos.TryGetValue(id, out info))
			{
				info.hp = hp;
				info.maxHp = maxHp;
			}
		}

		public static void QueueMobDeath(string id, float x, float y, float z)
		{
			WorldTcpClient.MobInfo info;
			if (MobInfos.TryGetValue(id, out info))
			{
				info.x = x;
				info.y = y;
				info.z = z;
			}
			_qMobDeath.Add(id);
		}

		public static void QueueDropSpawn(WorldTcpClient.DropInfo info)
		{
			if (info == null || string.IsNullOrEmpty(info.id))
			{
				return;
			}
			DropInfos[info.id] = info;
			_qDropSpawn.Add(info);
		}

		public static void QueueDropRemove(string id)
		{
			_qDropRemove.Add(id);
		}

		public static void QueueSkillResult(WorldTcpClient.SkillResult result)
		{
			if (result == null)
			{
				return;
			}
			_qSkillResult.Add(result);
		}

		public static void QueueEquipResult(bool ok, string reason, string action, int position, int mainId, int subId)
		{
			LastEquipOk = ok;
			LastEquipReason = reason;
			LastEquipAction = action;
			LastEquipPosition = position;
			LastEquipMainId = mainId;
			LastEquipSubId = subId;
			_hasEquipResult = true;
		}

		public static void QueueInventory(int gold, WorldTcpClient.InventoryItemInfo[] items, WorldTcpClient.InventoryItemInfo[] equipped)
		{
			Gold = gold;
			Items.Clear();
			if (items != null)
			{
				Items.AddRange(items);
			}
			Equipped.Clear();
			if (equipped != null)
			{
				Equipped.AddRange(equipped);
			}
			_hasInventory = true;
			RefreshLocalPlayerWeapon();
		}

		/// <summary>Show the equipped held-weapon in the local player's hand. Re-runs on every
		/// inventory push so equip/unequip updates the visual (the held skin is bound to the
		/// Bip01 hand socket by PreviewGearBinder).</summary>
		public static void RefreshLocalPlayerWeapon()
		{
			if (LocalPlayer == null)
			{
				UnityEngine.Debug.Log("[Weapon] skip: LocalPlayer not set");
				return;
			}
			for (int i = 0; i < Equipped.Count; i++)
			{
				WorldTcpClient.InventoryItemInfo it = Equipped[i];
				ItemObject io;
				if (!CharacterStatBlock.TryGetItem(it.mainId, it.subId, out io) || io == null)
				{
					continue;
				}
				// Held weapon — skip ammo (arrows are type12, not a held model).
				if (io.wearPosition == Core.Enumerators.WearPositionEnum.Handheld && (int)io.type != 12)
				{
					PreviewGearBinder.ApplyEquippedWeapon(LocalPlayer.gameObject, it.mainId, it.subId);
					UnityEngine.Debug.Log("[Weapon] applied " + it.mainId + "/" + it.subId + " on " + LocalPlayer.name);
					return;
				}
			}
			PreviewGearBinder.ClearWeapon(LocalPlayer.gameObject);
			UnityEngine.Debug.Log("[Weapon] no held weapon in " + Equipped.Count + " equipped items");
		}

		/// <summary>
		/// Animator weapon-family for the equipped held weapon (GAUNT / ONESWORD / BOW / SPEAR /
		/// STICK ...), or null when unarmed. Each class controller carries per-family locomotion +
		/// attack states, so the idle/attacks reflect what's actually in hand. Sword/blade split by
		/// the item's twoHanded/bigHanded flags (one/two/big-handed variants).
		/// </summary>
		public static string EquippedWeaponFamily()
		{
			for (int i = 0; i < Equipped.Count; i++)
			{
				WorldTcpClient.InventoryItemInfo it = Equipped[i];
				ItemObject io;
				if (!CharacterStatBlock.TryGetItem(it.mainId, it.subId, out io) || io == null)
				{
					continue;
				}
				if (io.wearPosition != Core.Enumerators.WearPositionEnum.Handheld || (int)io.type == 12)
				{
					continue; // only the held weapon (skip arrows/ammo)
				}
				switch (io.attackType)
				{
					case Core.Enumerators.ItemAttackType.Gaunt: return "GAUNT";
					case Core.Enumerators.ItemAttackType.Stick: return "STICK";
					case Core.Enumerators.ItemAttackType.Dagger: return "DAGGER";
					case Core.Enumerators.ItemAttackType.Spear: return "SPEAR";
					case Core.Enumerators.ItemAttackType.Bow: return "BOW";
					case Core.Enumerators.ItemAttackType.Sword:
						return io.bigHanded ? "BIGSWORD" : (io.twoHanded ? "TWOSWORD" : "ONESWORD");
					case Core.Enumerators.ItemAttackType.Blade:
						return io.bigHanded ? "BIGBLADE" : (io.twoHanded ? "TWOBLADE" : "ONEBLADE");
					default:
						return null;
				}
			}
			return null;
		}

		public static void QueueSkillList(WorldTcpClient.SkillInfo[] skills, int skillPoints)
		{
			int c = skills == null ? -1 : skills.Length;
			UnityEngine.Debug.Log("[WorldRuntime] QueueSkillList count=" + c + " skillPoints=" + skillPoints);
			Skills.Clear();
			if (skills != null)
			{
				Skills.AddRange(skills);
			}
			// -1 = server didn't include a pool (older payload) → keep the old value.
			if (skillPoints >= 0)
			{
				SkillPoints = skillPoints;
			}
			_hasSkillList = true;
		}

		public static void QueueSelfVitals(int hp, int maxHp, int mp, int maxMp, int sp, int maxSp)
		{
			_serverVitalsSeen = true;
			_qVitals.hp = hp;
			_qVitals.maxHp = maxHp;
			_qVitals.mp = mp;
			_qVitals.maxMp = maxMp;
			_qVitals.sp = sp;
			_qVitals.maxSp = maxSp;
			_hasVitals = true;
		}

		public static void QueueRespawn(float x, float y, float z, int hp, int maxHp, int mp, int maxMp)
		{
			RespawnEvt ev;
			ev.x = x;
			ev.y = y;
			ev.z = z;
			ev.hp = hp;
			ev.maxHp = maxHp;
			ev.mp = mp;
			ev.maxMp = maxMp;
			_qRespawn.Add(ev);
		}

		// ---- P2: buff / ailment queues (applied on the main thread during Drain) ----

		public static void QueueBuffApply(WorldTcpClient.BuffInfo buff)
		{
			if (buff == null)
			{
				return;
			}
			Buffs[buff.slot] = buff;
			Action handler = BuffsChanged;
			if (handler != null)
			{
				handler();
			}
		}

		public static void QueueBuffUpdate(WorldTcpClient.BuffSnapshot snap)
		{
			if (snap == null)
			{
				return;
			}
			ApplyBuffSnapshot(snap);
		}

		public static void QueueAilmentApply(WorldTcpClient.AilmentInfo ailment)
		{
			if (ailment == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(ailment.targetId) || ailment.targetId == SelfIdOrDefault())
			{
				Ailments[ailment.type] = ailment;
			}
			else
			{
				// mob ailment — track per mob for nameplate display
				List<WorldTcpClient.AilmentInfo> list;
				if (!MobAilments.TryGetValue(ailment.targetId, out list))
				{
					list = new List<WorldTcpClient.AilmentInfo>();
					MobAilments[ailment.targetId] = list;
				}
				int idx = list.FindIndex((a) => a.type == ailment.type);
				if (idx >= 0)
				{
					list[idx] = ailment;
				}
				else
				{
					list.Add(ailment);
				}
			}
			Action handler = BuffsChanged;
			if (handler != null)
			{
				handler();
			}
		}

		public static void QueueAilmentUpdate(WorldTcpClient.BuffSnapshot snap)
		{
			if (snap == null)
			{
				return;
			}
			ApplyAilmentSnapshot(snap);
		}

		/// <summary>Replace the local buff set from a server snapshot (expiry/dispel).</summary>
		private static void ApplyBuffSnapshot(WorldTcpClient.BuffSnapshot snap)
		{
			Buffs.Clear();
			if (snap.buffs != null)
			{
				for (int i = 0; i < snap.buffs.Length; i++)
				{
					WorldTcpClient.BuffInfo b = snap.buffs[i];
					if (b != null)
					{
						Buffs[b.slot] = b;
					}
				}
			}
			if (snap.ailments != null)
			{
				for (int i = 0; i < snap.ailments.Length; i++)
				{
					WorldTcpClient.AilmentInfo a = snap.ailments[i];
					if (a != null)
					{
						Ailments[a.type] = a;
					}
				}
			}
			Action handler = BuffsChanged;
			if (handler != null)
			{
				handler();
			}
		}

		/// <summary>Replace the local ailment set from a server snapshot (cure/expiry).</summary>
		private static void ApplyAilmentSnapshot(WorldTcpClient.BuffSnapshot snap)
		{
			Ailments.Clear();
			if (snap.ailments != null)
			{
				for (int i = 0; i < snap.ailments.Length; i++)
				{
					WorldTcpClient.AilmentInfo a = snap.ailments[i];
					if (a != null)
					{
						Ailments[a.type] = a;
					}
				}
			}
			if (snap.buffs != null && snap.buffs.Length > 0)
			{
				// some updates carry both — refresh buffs too
				Buffs.Clear();
				for (int i = 0; i < snap.buffs.Length; i++)
				{
					WorldTcpClient.BuffInfo b = snap.buffs[i];
					if (b != null)
					{
						Buffs[b.slot] = b;
					}
				}
			}
			Action handler = BuffsChanged;
			if (handler != null)
			{
				handler();
			}
		}

		private static string SelfIdOrDefault()
		{
			return (WorldNet.Client != null) ? WorldNet.Client.SelfId : null;
		}

		// ---- instance ----

		public void Init(Transform localPlayer, Camera worldCamera)
		{
			Instance = this;
			LocalPlayer = localPlayer;
			WorldCamera = worldCamera;
			// The ripped scenes ship without an AudioListener (the ripper strips it), and Unity
			// is silent without one — attach it to the world camera so SFX/BGM actually output.
			if (worldCamera != null && worldCamera.GetComponent<AudioListener>() == null)
			{
				worldCamera.gameObject.AddComponent<AudioListener>();
			}
			UnityEngine.Debug.Log("[WorldRuntime] Init client=" + (WorldNet.Client == null ? "NULL" : "OK"));
			Drain();
		}

		private void Awake()
		{
			Instance = this;
		}

		private void Update()
		{
			Drain();
		}

		private void Drain()
		{
			SpawnPendingNpcs(); // P5: static quest NPCs
			for (int i = 0; i < _qMobSpawn.Count; i++)
			{
				WorldTcpClient.MobInfo info = _qMobSpawn[i];
				WorldMobView view;
				bool created = false;
				if (MobViews.TryGetValue(info.id, out view) && view != null)
				{
					view.Refresh(info);
				}
				else
				{
					view = WorldMobView.Create(info);
					if (view != null)
					{
						MobViews[info.id] = view;
						created = true;
					}
				}
				if (view != null)
				{
					// Let a spawner/AI hook attach itself to local mobs (and re-attach on respawn).
					Action<WorldMobView> spawned = MobSpawned;
					if (spawned != null)
					{
						spawned(view);
					}
					if (created)
					{
						WorldFx.PlayAt(view.transform.position, "particles/Mob_Respawn");
					}
				}
			}
			_qMobSpawn.Clear();
			for (int i = 0; i < _qMobDeath.Count; i++)
			{
				string id = _qMobDeath[i];
				WorldMobView view;
				if (MobViews.TryGetValue(id, out view) && view != null)
				{
					WorldFx.PlayAt(view.transform.position, "particles/Mob_Death");
					view.OnDeath();
				}
				WorldTcpClient.MobInfo deadInfo;
				if (MobInfos.TryGetValue(id, out deadInfo) && deadInfo.local && deadInfo.exp > 0)
				{
					GainExp(deadInfo.exp);
				}
				Action<string> died = MobDied;
				if (died != null)
				{
					died(id);
				}
			}
			_qMobDeath.Clear();
			for (int i = 0; i < _qDropSpawn.Count; i++)
			{
				WorldTcpClient.DropInfo info = _qDropSpawn[i];
				WorldDropView view;
				if (!DropViews.TryGetValue(info.id, out view) || view == null)
				{
					view = WorldDropView.Create(info);
					if (view != null)
					{
						DropViews[info.id] = view;
					}
				}
			}
			_qDropSpawn.Clear();
			for (int i = 0; i < _qDropRemove.Count; i++)
			{
				string id = _qDropRemove[i];
				WorldTcpClient.DropInfo info;
				DropInfos.TryGetValue(id, out info);
				DropInfos.Remove(id);
				WorldDropView view;
				if (DropViews.TryGetValue(id, out view))
				{
					DropViews.Remove(id);
					if (view != null)
					{
						view.OnRemoved();
					}
				}
			}
			_qDropRemove.Clear();
			if (_hasVitals)
			{
				_hasVitals = false;
				Hp = _qVitals.hp;
				MaxHp = _qVitals.maxHp;
				Mp = _qVitals.mp;
				MaxMp = _qVitals.maxMp;
				Sp = _qVitals.sp;
				MaxSp = _qVitals.maxSp;
				Raise(VitalsChanged);
			}
			if (_hasInventory)
			{
				_hasInventory = false;
				// Equipment contributes damage/defense/hit/avoid and stats — re-derive.
				RecomputeDerived(true);
				Raise(InventoryChanged);
			}
			if (_hasEquipResult)
			{
				_hasEquipResult = false;
				Debug.Log("[WorldRuntime] equip result: ok=" + LastEquipOk + " action=" + LastEquipAction + " pos=" + LastEquipPosition + " mainId=" + LastEquipMainId + " subId=" + LastEquipSubId + " reason=" + LastEquipReason);
			}
			if (_hasSkillList)
			{
				_hasSkillList = false;
				Raise(SkillListChanged);
			}
			for (int i = 0; i < _qRespawn.Count; i++)
			{
				RespawnEvt ev = _qRespawn[i];
				if (LocalPlayer != null)
				{
					CharacterController cc = LocalPlayer.GetComponent<CharacterController>();
					if (cc != null)
					{
						cc.enabled = false;
					}
					LocalPlayer.position = new Vector3(ev.x, ev.y, ev.z);
					if (cc != null)
					{
						cc.enabled = true;
					}
				}
				Hp = ev.hp;
				MaxHp = ev.maxHp;
				Mp = ev.mp;
				MaxMp = ev.maxMp;
				Raise(VitalsChanged);
				Raise(Respawned);
			}
			_qRespawn.Clear();
			for (int i = 0; i < _qSkillResult.Count; i++)
			{
				WorldTcpClient.SkillResult r = _qSkillResult[i];
				if (r.ok && r.caster != null && WorldNet.Client != null && r.caster == WorldNet.Client.SelfId)
				{
					if (r.casterMaxMp > 0)
					{
						Mp = r.casterMp;
						MaxMp = r.casterMaxMp;
					}
					if (r.casterMaxSp > 0)
					{
						Sp = r.casterSp;
						MaxSp = r.casterMaxSp;
					}
					if (r.casterMaxMp > 0 || r.casterMaxSp > 0)
					{
						Raise(VitalsChanged);
					}
				}
				Action<WorldTcpClient.SkillResult> handler = SkillResultOccurred;
				if (handler != null)
				{
					handler(r);
				}
			}
			_qSkillResult.Clear();
		}

		private static void Raise(Action action)
		{
			if (action != null)
			{
				action();
			}
		}

		// ---- progression (local-authority exp/level, also fed by server SKILL_RESULT) ----

		/// <summary>Exp required to advance from <paramref name="level"/> — the original exptable_max curve.</summary>
		public static int ExpForLevel(int level)
		{
			return (int)CharacterStatBlock.ExpForLevel(level);
		}

		/// <summary>Award exp to the local player, handling multi-level-ups. Returns true if a level was gained.</summary>
		public static bool GainExp(int amount)
		{
			if (amount <= 0)
			{
				return false;
			}
			Exp += amount;
			bool leveled = false;
			int maxLevel = CharacterStatBlock.MaxLevel;
			// Loop in case a single kill grants enough for several levels.
			while (Exp >= MaxExp && Level < maxLevel)
			{
				Exp -= MaxExp;
				Level++;
				MaxExp = ExpForLevel(Level);
				// Original LEVLEUP: +levelStatsPoint stat points, +levelSkillPoint skill
				// points, and a FULL HP/MP/SP restore.
				StatPoints += CharacterStatBlock.StatPointsPerLevel;
				RecomputeDerived(true);
				Hp = MaxHp;
				Mp = MaxMp;
				Sp = MaxSp;
				leveled = true;
			}
			if (Level >= maxLevel && Exp > MaxExp)
			{
				Exp = MaxExp;
			}
			Action<WorldTcpClient.SkillResult> handler = SkillResultOccurred;
			if (handler != null && leveled)
			{
				// Reuse the skill-result channel so the HUD toasts the level-up.
				WorldTcpClient.SkillResult lvl = new WorldTcpClient.SkillResult();
				lvl.ok = true;
				lvl.reason = "levelup";
				lvl.exp = amount;
				lvl.skillLevel = Level;
				handler(lvl);
			}
			Action expHandler = ExpChanged;
			if (expHandler != null)
			{
				expHandler();
			}
			if (leveled)
			{
				Action vitals = VitalsChanged;
				if (vitals != null)
				{
					vitals();
				}
				Action statsHandler = StatsChanged;
				if (statsHandler != null)
				{
					statsHandler();
				}
			}
			return leveled;
		}

		/// <summary>
		/// Which attribute a point button maps to. The Character window's five rows
		/// ('STR'/'DEX'/'INT'/'VIT'/'STM') bind to these — see OriginalHudWindows for
		/// the label mapping notes.
		/// </summary>
		public enum StatKind
		{
			Pow,
			Str,
			Spi,
			Dex,
			Sta
		}

		/// <summary>
		/// Spend one stat point (local-authority; mirrors the reference's + buttons),
		/// then re-derives Max HP/MP/SP and attack stats. Returns false when no points remain.
		/// </summary>
		public static bool SpendStatPoint(StatKind kind)
		{
			if (StatPoints <= 0)
			{
				return false;
			}
			switch (kind)
			{
				case StatKind.Pow: Pow++; break;
				case StatKind.Str: Str++; break;
				case StatKind.Spi: Spi++; break;
				case StatKind.Dex: Dex++; break;
				case StatKind.Sta: Sta++; break;
			}
			StatPoints--;
			// Derivation changes (STR→HP, SPI→MP, STA→SP, POW→PA…) — keep ratios where possible.
			RecomputeDerived(true);
			Action vitals = VitalsChanged;
			if (vitals != null)
			{
				vitals();
			}
			Action statsHandler = StatsChanged;
			if (statsHandler != null)
			{
				statsHandler();
			}
			return true;
		}

		#region derived stats (CharacterStatBlock / SUM_ADDITION)

		private static DerivedCharacterStats _derived;

		/// <summary>Cached derived stats; recomputed by <see cref="RecomputeDerived"/>.</summary>
		public static DerivedCharacterStats Derived
		{
			get
			{
				if (_derived == null)
				{
					RecomputeDerived(false);
				}
				return _derived;
			}
		}

		/// <summary>
		/// Re-run the original SUM_ADDITION pipeline against current level/allocated stats/
		/// equipment and publish the result (MaxHp/MaxMp/MaxSp). With <paramref name="clamp"/>
		/// current vitals are clamped to the new maxima (stats never lower them).
		/// </summary>
		public static void RecomputeDerived(bool clamp)
		{
			GearTerms gear = CharacterStatBlock.GearFromEquipped(Equipped);
			RAN.Mobile.GameModels.CharStats alloc = new RAN.Mobile.GameModels.CharStats();
			alloc.Pow = (ushort)Mathf.Max(0, Pow);
			alloc.Str = (ushort)Mathf.Max(0, Str);
			alloc.Spi = (ushort)Mathf.Max(0, Spi);
			alloc.Dex = (ushort)Mathf.Max(0, Dex);
			alloc.Sta = (ushort)Mathf.Max(0, Sta);
			_derived = CharacterStatBlock.Compute(ClassId, Level, alloc, gear);
			if (!_serverVitalsSeen)
			{
				// Offline/local mode: derive vitals ourselves. Once the server has
				// spoken (PLAYER_HP), it owns MaxHp/MaxMp/MaxSp — we only refresh
				// combat numbers below.
				MaxHp = _derived.HpMax;
				MaxMp = _derived.MpMax;
				MaxSp = _derived.SpMax;
				if (clamp)
				{
					if (Hp > MaxHp) { Hp = MaxHp; }
					if (Mp > MaxMp) { Mp = MaxMp; }
					if (Sp > MaxSp) { Sp = MaxSp; }
				}
			}
			// One-line digest for verifying the ported formulas against class data.
			UnityEngine.Debug.Log(string.Format(
				"[Derived] cls={0} lv={1} stats P{2}/S{3}/PI{4}/D{5}/T{6} → HP={7} MP={8} SP={9} AP={10} DP={11} PA={12} SA={13} MA={14} DEF={15} HIT={16} AVD={17} walk={18} run={19} expNext={20}",
				ClassId, Level, _derived.Pow, _derived.Str, _derived.Spi, _derived.Dex, _derived.Sta,
				_derived.HpMax, _derived.MpMax, _derived.SpMax, _derived.SumAp, _derived.SumDp,
				_derived.Pa, _derived.Sa, _derived.Ma, _derived.Defense, _derived.Hit, _derived.Avoid,
				_derived.WalkSpeed, _derived.RunSpeed, ExpForLevel(Level)));
		}

		/// <summary>Hit rating (DEX-scaled + gear) for the Character window.</summary>
		public static int HitValue()
		{
			return Derived.Hit;
		}

		/// <summary>Avoid rating (DEX-scaled + gear) for the Character window.</summary>
		public static int AvoidValue()
		{
			return Derived.Avoid;
		}

		#endregion

		/// <summary>Attack range shown in the Character window's Condition block (AP + PA + weapon damage).</summary>
		public static string AttackRangeText()
		{
			DerivedCharacterStats d = Derived;
			GearTerms gear = CharacterStatBlock.GearFromEquipped(Equipped);
			int low = d.DamageLow + d.Pa + gear.DamageLow;
			int high = d.DamageHigh + d.Pa + gear.DamageHigh;
			if (high <= low)
			{
				high = low;
			}
			return low + "~" + high;
		}

		/// <summary>Derived defense shown in the Condition block (SUM_ADDITION DEFENSE).</summary>
		public static int DefenseValue()
		{
			return Derived.Defense;
		}

		/// <summary>
		/// Mirrors GLCHARLOGIC::ISLONGRANGE_ARMS — true when an equipped weapon's attack
		/// type is above ITEMATT_NEAR(7), i.e. bow (8) / thrown-gun (9). Determines PA vs SA
		/// scaling, projectile behaviour and the ranged damage branch.
		/// </summary>
		public static bool WeaponIsRanged()
		{
			for (int i = 0; i < Equipped.Count; i++)
			{
				WorldTcpClient.InventoryItemInfo it = Equipped[i];
				if (it == null)
				{
					continue;
				}
				ItemObject io;
				if (CharacterStatBlock.TryGetItem(it.mainId, it.subId, out io))
				{
					if ((int)io.attackType > 7)
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Client-side damage to a locally-authoritative mob (one spawned by a MobSpawner,
		/// not the server). Returns the damage dealt, or -1 if the id is not a local mob
		/// (caller should fall back to sending the attack to the server).
		/// </summary>
		public static int ApplyLocalMobDamage(string mobId, int damage, Vector3 hitPos)
		{
			WorldTcpClient.MobInfo info;
			if (mobId == null || !MobInfos.TryGetValue(mobId, out info) || !info.local)
			{
				return -1;
			}
			WorldMobView view;
			MobViews.TryGetValue(mobId, out view);
			if (view == null || !view.Alive)
			{
				return -1;
			}
			if (damage < 0)
			{
				damage = 0;
			}
			info.hp -= damage;
			MobInfos[mobId] = info;
			if (info.hp <= 0)
			{
				info.hp = 0;
				// Route through the normal death queue so the view hides + VFX fires.
				QueueMobDeath(mobId, info.x, info.y, info.z);
				LocalLoot.OnLocalMobKilled(info, hitPos);
			}
			else if (WorldHUD.Instance != null)
			{
				WorldHUD.Instance.SpawnPopup(hitPos, damage.ToString(), Color.white);
			}
			return damage;
		}

		/// <summary>
		/// Apply damage to the local player from a local mob's melee. Clamps at 0 and,
		/// on knockout, requests a respawn from the start (local-authority fallback that
		/// also works while offline). Returns true if the player was knocked out.
		/// </summary>
		public static bool ApplyLocalPlayerDamage(int damage, Vector3 fromPos)
		{
			if (damage <= 0 || Hp <= 0)
			{
				return false;
			}
			Hp = Mathf.Max(0, Hp - damage);
			if (WorldHUD.Instance != null)
			{
				WorldHUD.Instance.SpawnPopup(LocalPlayerPosition() + Vector3.up * 10f, "-" + damage, new Color(1f, 0.35f, 0.3f));
			}
			Action vitals = VitalsChanged;
			if (vitals != null)
			{
				vitals();
			}
			if (Hp <= 0)
			{
				OnLocalKnockout();
				return true;
			}
			return false;
		}

		private static Vector3 LocalPlayerPosition()
		{
			return LocalPlayer != null ? LocalPlayer.position : Vector3.zero;
		}

		/// <summary>Local knockout: play the respawn VFX, restore vitals and teleport to spawn.</summary>
		private static void OnLocalKnockout()
		{
			WorldFx.PlayAt(LocalPlayerPosition(), "particles/Player_Respawn");
			Hp = MaxHp;
			Mp = MaxMp;
			Sp = MaxSp;
			if (LocalPlayer != null)
			{
				CharacterController cc = LocalPlayer.GetComponent<CharacterController>();
				if (cc != null)
				{
					cc.enabled = false;
				}
				// Return to the map's origin-ish safe spot (fall back to current XZ on the ground).
				Vector3 home = LocalPlayer.position;
				RaycastHit hit;
				if (Physics.Raycast(new Vector3(0f, 300f, 0f), Vector3.down, out hit, 600f))
				{
					home = hit.point;
				}
				LocalPlayer.position = home + Vector3.up * 0.5f;
				if (cc != null)
				{
					cc.enabled = true;
				}
			}
			Action vitals = VitalsChanged;
			if (vitals != null)
			{
				vitals();
			}
			Action respawn = Respawned;
			if (respawn != null)
			{
				respawn();
			}
		}

		/// <summary>Manually raise InventoryChanged (used by local loot grants that bypass the TCP queue).</summary>
		public static void RaiseInventoryChanged()
		{
			Action handler = InventoryChanged;
			if (handler != null)
			{
				handler();
			}
		}

		// ---- queries ----

		public static WorldMobView NearestMob(Vector3 from, float maxDistance)
		{
			WorldMobView best = null;
			float bestSqr = maxDistance * maxDistance;
			foreach (KeyValuePair<string, WorldMobView> kv in MobViews)
			{
				WorldMobView v = kv.Value;
				if (v == null || !v.Alive)
				{
					continue;
				}
				float sqr = (v.transform.position - from).sqrMagnitude;
				if (sqr <= bestSqr)
				{
					bestSqr = sqr;
					best = v;
				}
			}
			return best;
		}

		/// <summary>Attack/cast one-shot for a class — matches the equipped class weapon's swing.</summary>
		public static string ClassAttackSfx(int classId)
		{
			switch (classId)
			{
				case 1: return "b-attack1";   // brawler / gaunt
				case 2: return "1h_slash";    // swordsman / sword
				case 4: return "a-attack";    // archer / bow
				case 8: return "sha-attack1"; // shaman / spear
				default: return "punch1";
			}
		}

		public static string ItemName(int mainId, int subId)
		{
			BuildItemNameCache();
			string name;
			if (_itemNameCache.TryGetValue(((long)mainId << 32) | (uint)subId, out name))
			{
				return name;
			}
			return "Item";
		}

		private static void BuildItemNameCache()
		{
			if (_itemNameCache != null)
			{
				return;
			}
			_itemNameCache = new Dictionary<long, string>();
			if (_itemDb == null)
			{
				_itemDb = Resources.Load<ItemDatabaseObject>("database/ItemDatabase");
			}
			if (_itemDb == null || _itemDb.ItemObjects == null)
			{
				return;
			}
			foreach (ItemObject io in _itemDb.ItemObjects)
			{
				if (io == null || io.data == null)
				{
					continue;
				}
				FieldInfo fMain = io.data.GetType().GetField("mainId");
				FieldInfo fSub = io.data.GetType().GetField("subId");
				if (fMain == null || fSub == null)
				{
					continue;
				}
				int mainId = Convert.ToInt32(fMain.GetValue(io.data));
				int subId = Convert.ToInt32(fSub.GetValue(io.data));
				long key = ((long)mainId << 32) | (uint)subId;
				string name = string.IsNullOrEmpty(io.Name) ? ("Item " + mainId) : io.Name;
				if (!_itemNameCache.ContainsKey(key))
				{
					_itemNameCache[key] = name;
				}
			}
		}

		/// <summary>Client-side skill metadata (name + icon) for the server skill list.</summary>
		public static void FillSkillDisplay(WorldTcpClient.SkillInfo info, out string displayName, out Sprite icon)
		{
			displayName = info.name;
			icon = null;
			SkillDatabaseObject db = Resources.Load<SkillDatabaseObject>("database/SkillDatabase");
			if (db == null || db.skillObjectList == null)
			{
				return;
			}
			if (_skillDataField == null)
			{
				_skillDataField = typeof(RAN.Mobile.GameModels.SkillBase).GetField("data", BindingFlags.Instance | BindingFlags.NonPublic);
				_skillNameField = typeof(RAN.Mobile.GameModels.SkillBase).GetField("Name", BindingFlags.Instance | BindingFlags.NonPublic);
			}
			foreach (SkillObject so in db.skillObjectList)
			{
				if (so == null || _skillDataField == null)
				{
					continue;
				}
				object data = _skillDataField.GetValue(so);
				if (data == null)
				{
					continue;
				}
				FieldInfo mainField = NativeField(data.GetType(), "mainId");
				if (mainField == null)
				{
					continue;
				}
				int mainId = Convert.ToInt32(mainField.GetValue(data));
				if (mainId != info.mainId)
				{
					continue;
				}
				FieldInfo subField = NativeField(data.GetType(), "subId");
				if (subField != null)
				{
					int subId = Convert.ToInt32(subField.GetValue(data));
					if (subId != info.subId)
					{
						continue;
					}
				}
				if (_skillNameField != null)
				{
					string n = _skillNameField.GetValue(so) as string;
					if (!string.IsNullOrEmpty(n))
					{
						displayName = n;
					}
				}
				icon = so.IconSource;
				return;
			}
		}

		private static FieldInfo NativeField(Type type, string name)
		{
			// Key on BOTH type and field name — caching by type alone would return the
			// first field looked up (e.g. mainId) for every later name (e.g. subId),
			// which broke the skill-icon lookup in FillSkillDisplay.
			string key = type.FullName + "." + name;
			FieldInfo f;
			if (!_nativeFieldCache.TryGetValue(key, out f))
			{
				f = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				_nativeFieldCache[key] = f;
			}
			return f;
		}
	}
}
