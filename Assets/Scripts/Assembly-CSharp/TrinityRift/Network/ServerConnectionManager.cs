using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Core.Enumerators;
using Enumerators;
using Models.Library.Shared.Enums;
using Models.Library.Shared.Game;
using Models.Library.Shared.Game.Club;
using Models.Library.Shared.Game.ClubWar;
using Models.Library.Shared.Game.Tyranny;
using Models.Network.Shared.Enums;
using Models.Network.Shared.Interface;
using Models.Network.Shared.Response;
using RAN.Mobile.GameModels;
using UnityEngine;

namespace TrinityRift.Network
{
	public class ServerConnectionManager : MonoBehaviour
	{
		public struct PKKillFeedEntry
		{
			public string Killer;

			public string Victim;

			public ushort KillerGaeaId;

			public ushort VictimGaeaId;

			public byte KillerSchool;

			public byte KillerClass;

			public byte VictimSchool;

			public byte VictimClass;
		}

		private struct BufferedZonePacket
		{
			public ushort Opcode;

			public IMessage Packet;
		}

		private struct PendingSkillFact
		{
			public Models.Library.Shared.Game.SNativeID SkillId;

			public ushort Level;

			public int Slot;

			public ushort CasterCrow;

			public uint CasterId;

			public float Age;
		}

		public sealed class ClubMarkCacheEntry
		{
			public uint Version;

			public byte[] Image;

			public Sprite CachedSprite;
		}

		public enum ClubOp : byte
		{
			Notice = 0,
			Alias = 1,
			Mark = 2,
			Invite = 3,
			Kick = 4,
			RankUp = 5
		}

		private readonly Stopwatch _pingClock;

		private int _lastNetworkPingMs;

		private long _lastPongClockMs;

		private long _lastWatchdogTickMs;

		private ServerListResponse _cachedServerList;

		private LoginResponse _cachedLoginResponse;

		public Action<ServerListResponse> OnServerListReceived;

		public Action<LoginResponse> OnLoginResponseReceived;

		public Action<JoinServerResponse> OnJoinServerResponseReceived;

		public Action<ChannelStatusResponse> OnChannelStatusReceived;

		public Action<CharacterListResponse> OnCharacterListReceived;

		public Action<CharacterCreateResponse> OnCharacterCreateReceived;

		public Action<Models.Network.Shared.Response.CharacterDeleteResponse> OnCharacterDeleteReceived;

		public Action<CharacterSelectResponse> OnCharacterSelectReceived;

		public Action<S_CharacterInventoryData> OnCharacterInventoryReceived;

		public Action<S_ZoneIdentifyResponse> OnZoneIdentifyReceived;

		public Action<F2C_ActionResponse> OnActionResponseReceived;

		public Action<F2C_ActStateBroadcast> OnActStateBroadcastReceived;

		public Action<F2C_GoToBroadcast> OnGoToBroadcastReceived;

		public Action<F2C_UpdateState> OnUpdateStateReceived;

		public Action<F2C_UpdateStateBroadcast> OnUpdateStateBroadcastReceived;

		public Action<F2C_LevelUp> OnLevelUpReceived;

		public Action<F2C_LevelUpBroadcast> OnLevelUpBroadcastReceived;

		public Action<F2C_UpdateExperience> OnUpdateExperienceReceived;

		public Action<SNETDROP_PC> OnDropPCReceived;

		public Action<SNETDROP_CROW> OnDropCrowReceived;

		public Action<SNETDROP_OUT> OnDropOutReceived;

		public Action<SNETDROP_ITEM> OnDropItemReceived;

		public Action<SNETDROP_MONEY> OnDropMoneyReceived;

		public Action<F2C_ItemSlotUpdateResponse> OnItemSlotUpdateReceived;

		public Action<F2C_ItemSlotUpdateBroadcast> OnItemSlotUpdateBroadcastReceived;

		public Action<F2C_StatsUpResponse> OnStatsUpReceived;

		public Action<F2C_QuestActionResponse> OnQuestActionResponse;

		public Action<F2C_QuestProgressUpdate> OnQuestProgressUpdate;

		public Action<F2C_QuestStateSync> OnQuestStateSync;

		public Action<F2C_GestureBroadcast> OnGestureBroadcastReceived;

		public Action<F2C_ChatBroadcast> OnChatBroadcastReceived;

		public const bool EnablePKKillFeed = true;

		private const int PKKillFeedMax = 20;

		public readonly Queue<PKKillFeedEntry> RecentPKKills;

		public Action<PKKillFeedEntry> OnPKKillNoticeReceived;

		public Action<F2C_PlayerAvoidResponse> OnPlayerAvoidResponseReceived;

		public Action<F2C_PlayerAvoidBroadcast> OnPlayerAvoidBroadcastReceived;

		public Action<F2C_PlayerAttackBroadcast> OnPlayerAttackBroadcastReceived;

		public Action<F2C_PlayerAttackCancelBroadcast> OnPlayerAttackCancelBroadcastReceived;

		public Action<F2C_PlayerSkillResponse> OnPlayerSkillResponseReceived;

		public Action<F2C_SkillActionBroadcast> OnSkillActionBroadcastReceived;

		public Action<F2C_SkillCancelBroadcast> OnSkillCancelBroadcastReceived;

		public Action<F2C_PushPull> OnPushPullReceived;

		public Action<F2C_PlayerDamageResponse> OnPlayerDamageResponseReceived;

		public Action<F2C_PlayerDamageBroadcast> OnPlayerDamageBroadcastReceived;

		public Action<F2C_SkillFactBroadcast> OnSkillFactBroadcastReceived;

		public Action<F2C_SkillHoldBroadcast> OnSkillHoldBroadcastReceived;

		public Action<F2C_SystemBuff> OnSystemBuffReceived;

		public Action<F2C_SkillConsumeResponse> OnSkillConsumeResponseReceived;

		public Action<F2C_StateBlowBroadcast> OnStateBlowBroadcastReceived;

		public Action<F2C_MobDamageBroadcast> OnMobDamageBroadcastReceived;

		public Action<F2C_MobAttackBroadcast> OnMobAttackBroadcastReceived;

		public Action<F2C_MobSkillActionBroadcast> OnMobSkillActionBroadcastReceived;

		public Action<F2C_MobAvoidBroadcast> OnMobAvoidBroadcastReceived;

		public Action<F2C_DefenseSkillActiveResponse> OnDefenseSkillActiveResponseReceived;

		public Action<F2C_RebirthResponse> OnRebirthResponseReceived;

		public Action<A2C_MapTransferResponse> OnMapTransferResponseReceived;

		public static bool GameTickActive;

		private bool _handlingDisconnect;

		public bool WasMountedOnTransfer;

		private bool _autoRemountPending;

		public const float VehicleToggleCooldownSeconds = 5f;

		private float _vehicleToggleReadyTime;

		private bool _livenessKillPending;

		private ushort _petFullness;

		private readonly List<Models.Library.Shared.Game.SNativeID> _petLearnedSkills;

		private Models.Library.Shared.Game.SNativeID _petActiveA;

		private Models.Library.Shared.Game.SNativeID _petActiveB;

		private bool _petDualUnlocked;

		[CompilerGenerated]
		private Action m_PetSkillStateChanged;

		public Action SkillListSyncReceived;

		public Action<F2C_SkillLearnResponse> SkillLearnResponseReceived;

		private readonly HashSet<ushort> _confrontOpponents;

		private readonly HashSet<ushort> _duelEliminated;

		private uint _lastDuelEndKey;

		private float _lastDuelEndTime;

		private const float DuelEndDedupWindow = 2f;

		private float _localConfrontEndTime;

		private const float PostConfrontGraceSeconds = 10f;

		private float _confrontHpRate;

		public Action<F2C_ConfrontForward> ConfrontForwardReceived;

		public Action<F2C_ConfrontFeedback> ConfrontFeedbackReceived;

		public Action<F2C_ConfrontStart> ConfrontStartReceived;

		public Action ConfrontFightReceived;

		public Action<F2C_ConfrontEnd, bool> ConfrontEndReceived;

		public Action<ushort, ushort, bool> ConfrontRecoverReceived;

		public Action<ChangeSchoolResultEnum, byte> ChangeSchoolResultReceived;

		public Action<ChangeNameResultEnum> ChangeNameResultReceived;

		public Action<F2C_PMarketTitleFB> OnPMarketTitleFB;

		public Action<F2C_PMarketRegItemFB> OnPMarketRegItemFB;

		public Action<F2C_PMarketDisItemFB> OnPMarketDisItemFB;

		public Action<F2C_PMarketOpenFB> OnPMarketOpenFB;

		public Action<F2C_PMarketOpenBrd> OnPMarketOpenBrd;

		public Action<F2C_PMarketCloseBrd> OnPMarketCloseBrd;

		public Action<F2C_PMarketBuyFB> OnPMarketBuyFB;

		public Action<F2C_PMarketItemInfoBrd> OnPMarketItemInfoBrd;

		public Action<F2C_PMarketItemUpdateBrd> OnPMarketItemUpdateBrd;

		private const int MaxReplayPerEntity = 32;

		private readonly Dictionary<long, List<BufferedZonePacket>> _replayBuffer;

		private readonly Stack<List<BufferedZonePacket>> _replayListPool;

		private readonly F2C_GoToBroadcast _goToScratch;

		private readonly List<PendingSkillFact> _pendingSkillFactRestore;

		private readonly Dictionary<uint, ClubMarkCacheEntry> _clubMarkCache;

		private readonly Dictionary<uint, string> _clubAliasCache;

		private readonly HashSet<uint> _clubMarkRequested;

		public Action<F2C_ClubCreateResponse> ClubCreateResultReceived;

		public Action<F2C_ClubInfoResponse> ClubInfoReceived;

		public Action<F2C_ClubInviteAsk> ClubInviteAskReceived;

		public Action<F2C_ClubInviteResult> ClubInviteResultReceived;

		public Action<F2C_ClubMemberAdd> ClubMemberAdded;

		public Action<F2C_ClubMemberRemove> ClubMemberRemoved;

		public Action<F2C_ClubMasterChange> ClubMasterChanged;

		public Action<F2C_ClubMemberFlagsUpdate> ClubMemberFlagsUpdated;

		public Action<F2C_ClubNoticeUpdate> ClubNoticeChanged;

		public Action<F2C_ClubAliasUpdate> ClubAliasChanged;

		public Action<F2C_ClubDissolveNotify> ClubDissolved;

		public Action<F2C_ClubMembershipResponse> ClubMembershipResultReceived;

		public Action<F2C_ClubMarkResult> ClubMarkResultReceived;

		public Action<uint> ClubMarkUpdated;

		public Action<F2C_ClubMemberStateUpdate> ClubMemberStateChanged;

		public Action<F2C_ClubLivingPointsUpdate> ClubLivingPointsChanged;

		public Action<F2C_ClubRankUpdate> ClubRankChanged;

		public Action<F2C_ClubRelationChanged> ClubRelationChanged;

		public readonly HashSet<uint> MyClubHostiles;

		public readonly HashSet<uint> MyClubFriendlies;

		public readonly HashSet<uint> MyAlliance;

		public uint MyAllianceChief;

		public Action<F2C_ClubAllianceAsk> ClubAllianceAsk;

		public Action ClubAllianceChanged;

		public Action<F2C_ClubAllianceResult> ClubAllianceResult;

		public Action<F2C_ClubRenamed> ClubRenamed;

		private const float ClubCooldownNotice = 5f;

		private const float ClubCooldownAlias = 30f;

		private const float ClubCooldownMark = 60f;

		private const float ClubCooldownInvite = 1f;

		private const float ClubCooldownKick = 1f;

		private const float ClubCooldownRankUp = 3f;

		private readonly float[] _clubCooldownEndsAt;

		public Action<int, string> OnClubWarStartNotice;

		public Action<string, string> OnClubWarEndNotice;

		public Action<uint> OnClubWarRemainUpdated;

		public Action<ClubWarCertifyResultEnum> OnClubWarCertifyResult;

		public Action OnClubWarRankingUpdated;

		public readonly SCWRank[] ClubWarRanks;

		public Action<TyrannyState, float> OnTyrannyStateChanged;

		public Action<TyrannyRegisterFb, ushort> OnTyrannyRegisterFb;

		public Action<TyrannyRejoinFb> OnTyrannyRejoinFb;

		public Action<TyrannyReviveFb> OnTyrannyReviveFb;

		public Action OnTyrannyQueueMoved;

		public Action<ushort> OnTyrannyQueueUpdate;

		public Action OnTyrannyBattleInfo;

		public Action<ushort> OnTyrannyToBattleTime;

		public Action OnTyrannyTowerInfo;

		public Action<ushort, ushort, ushort> OnTyrannyTowerCaptured;

		public Action<ushort, bool> OnTyrannyWinner;

		public Action OnTyrannyRankingUpdated;

		public Action OnTyrannyRankingFinalized;

		public Action OnTyrannyRewardInfo;

		public Action<ushort, ushort> OnTyrannyCrowOwner;

		public Action<ushort, float, float, float> OnTyrannyCrowDamage;

		public Action OnTyrannyNextSched;

		public readonly ushort[] TyrannyTowerOwner;

		public readonly ushort[] TyrannyPlayerNum;

		public readonly TyrannyScheduleNext TyrannyNextSchedule;

		private readonly List<TyrannyPlayerData> _tyrannyRankBuf;

		private int _tyrannyRankCount;

		private bool _tyrannyRankStreamDone;

		private readonly TyrannyPlayerData[] _tyrannyRewardTop;

		private int _tyrannyRewardTopCount;

		public readonly TyrannyNativeId[] TyrannyRewardBuff;

		public static ServerConnectionManager Instance { get; private set; }

		public GameTcpConnection LoginServerConnection { get; private set; }

		public GameTcpConnection AgentConnection { get; private set; }

		public GameTcpConnection ZoneConnection { get; private set; }

		public int LastPingMs { get; private set; }

		public static bool PingProbeOn => false;

		public ServerListResponse CachedServerList => null;

		public LoginResponse CachedLoginResponse => null;

		public bool VehicleLoaded { get; private set; }

		public int VehicleBattery { get; private set; }

		public float VehicleToggleCooldownRemaining => 0f;

		public bool IsZoneConnected => false;

		public long PongCount { get; private set; }

		public ushort PetFullness => 0;

		public IReadOnlyList<Models.Library.Shared.Game.SNativeID> PetLearnedSkills => null;

		public Models.Library.Shared.Game.SNativeID PetActiveSkillA => default;

		public Models.Library.Shared.Game.SNativeID PetActiveSkillB => default;

		public bool PetDualSkillUnlocked => false;

		public ConfrontTypeEnum ConfrontType { get; private set; }

		public ushort ConfrontTargetGaeaId { get; private set; }

		public string ConfrontTargetName { get; private set; }

		public bool bConfrontFight { get; private set; }

		public bool IsConfronting => false;

		public ushort wConfrontRecover { get; private set; }

		public ushort wConfrontRecoverUsed { get; private set; }

		public bool bSchoolFreePK { get; private set; }

		public bool bBrightEvent { get; private set; }

		public bool LandIsPK { get; private set; }

		public bool LandIsClubBattle { get; private set; }

		public bool LandIsClubBattleHall { get; private set; }

		public bool LandIsClubDeathMatch { get; private set; }

		public bool LandIsClubDeathMatchHall { get; private set; }

		public bool LandIsTyranny { get; private set; }

		public bool IsInPostConfrontGrace => false;

		public IReadOnlyCollection<ushort> ConfrontOpponents => null;

		public IReadOnlyCollection<ushort> DuelEliminated => null;

		public SClub MyClub { get; private set; }

		public uint ClubWarRemainSec { get; private set; }

		public int ClubWarRankCount { get; private set; }

		public ushort ClubWarRankGuidId { get; private set; }

		public TyrannyState TyrannyStateNow { get; private set; }

		public float TyrannyRemainSec { get; private set; }

		public ushort TyrannyLevelReq { get; private set; }

		public ushort TyrannyPlayerLimit { get; private set; }

		public uint TyrannyCaptureMax { get; private set; }

		public bool TyrannyRegistered { get; private set; }

		public bool TyrannyQueued { get; private set; }

		public ushort TyrannyQueueNum { get; private set; }

		public bool TyrannyIsPkCounted { get; private set; }

		public ushort TyrannyLastWinner { get; private set; }

		public int TyrannyRankCount => 0;

		public ushort TyrannyRewardWinner { get; private set; }

		public int TyrannyRewardTopCount => 0;

		private static uint LocalCharId => 0u;

		public event Action PetSkillStateChanged
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static void SetPingProbe(bool on)
		{
		}

		public static bool TogglePingProbe()
		{
			return false;
		}

		private void Awake()
		{
		}

		private void Update()
		{
		}

		public void ConnectToLoginServer(string host, int port, Action<bool> callback)
		{
		}

		public void ConnectToAgent(string host, int port, Action<bool> callback)
		{
		}

		public void DisconnectLoginServer()
		{
		}

		public void DisconnectAgent()
		{
		}

		public void DisconnectZone()
		{
		}

		public void DisconnectAll()
		{
		}

		private void HandleUnexpectedDisconnect(string message)
		{
		}

		public void RequestServerList()
		{
		}

		public void SendCharacterListRequest()
		{
		}

		public void SendCharacterCreateRequest(string name, ushort charClass, ushort school, ushort hairType, ushort hairColorType, ushort faceType)
		{
		}

		public void SendCharacterDeleteRequest(uint charId)
		{
		}

		public void SendCharacterSelectRequest(uint charId)
		{
		}

		public void SendChannelStatusRequest(byte channelIndex)
		{
		}

		public void SendLoginRequest(string username, string password)
		{
		}

		public void SendJoinServerRequest(byte channelIndex, string username, string token)
		{
		}

		public void SendActionRequest(ActionEnums action)
		{
		}

		public void SendActStateRequest(uint actState)
		{
		}

		public void SendMoveStateRequest(uint actState)
		{
		}

		public void SendVehicleGetRequest(ushort itemMain, ushort itemSub, ushort invenSlot, bool release)
		{
		}

		public void SendVehicleActiveRequest(bool active)
		{
		}

		public void TryAutoRemountAfterTransfer()
		{
		}

		public void RequestToggleMount()
		{
		}

		public static bool IsVehicleAllowedOnCurrentMap()
		{
			return false;
		}

		private void ArmVehicleToggleCooldown()
		{
		}

		private static bool IsVehicleEquipped()
		{
			return false;
		}

		private void HandleVehicleGetResponse(F2C_VehicleGetResponse msg)
		{
		}

		private void HandleVehicleActiveResponse(F2C_VehicleActiveResponse msg)
		{
		}

		private void HandleVehicleActiveBroadcast(F2C_VehicleActiveBroadcast msg)
		{
		}

		private void HandleVehicleBatteryUpdate(F2C_VehicleBatteryUpdate msg)
		{
		}

		private static void ShowVehicleFailedToast(VehicleResultEnum result)
		{
		}

		public void ResetVehicleClientState()
		{
		}

		public void SendLandInRequest()
		{
		}

		public void SendReadyRequest()
		{
		}

		public void SendPing()
		{
		}

		private void OnZonePacketRecvThread(ushort opcode, IMessage packet)
		{
		}

		public long MsSinceLastPong()
		{
			return 0L;
		}

		public void ForceZoneDisconnect()
		{
		}

		public void TickZoneLiveness(float cadenceSeconds, float pongTimeoutSeconds)
		{
		}

		public void SendAgentHeartbeat()
		{
		}

		public void SendGoToRequest(float targetX, float targetZ, uint actState, float startY)
		{
		}

		public void SendChatGeneral(string message)
		{
		}

		public void SendChatLoudSpeaker(string message)
		{
		}

		public void SendChatWhisper(string targetName, string message)
		{
		}

		public void SendChatParty(string message)
		{
		}

		public void SendChatGuild(string message)
		{
		}

		public void SendChatAlliance(string message)
		{
		}

		public void SendChatAnnouncement(string message)
		{
		}

		public void SendChatSchool(string message)
		{
		}

		public void SendItemDropToFieldRequest(ItemSlotTypeEnum slotType, ushort index, ushort expMainId, ushort expSubId, long expGenNum)
		{
		}

		public void SendPartyLure(ushort targetGaeaId, PartyOption options)
		{
		}

		public void SendPartyLureAnswer(ushort inviterGaeaId, PartyLureAnswer answer)
		{
		}

		public void SendPartySecede(ushort targetGaeaId)
		{
		}

		public void SendPartyDissolve()
		{
		}

		public void SendPartyAuthority(ushort newMasterGaeaId)
		{
		}

		public void SendFriendListRequest()
		{
		}

		public void SendFriendAdd(string targetName)
		{
		}

		public void SendFriendAddAnswer(string requesterName, bool accept)
		{
		}

		public void SendFriendDel(string targetName)
		{
		}

		public void SendFriendBlock(string targetName, bool block)
		{
		}

		public void SendFriendMoveRequest(string friendName, ushort cardSlot)
		{
		}

		internal void ClearGateTransferHandlers()
		{
		}

		public void SendFieldPickupRequest(CrowType crowType, ushort globalId)
		{
		}

		public void SendItemSplitRequest(ushort index, ushort splitCount, ItemSlotTypeEnum slotType = ItemSlotTypeEnum.Inventory, byte channel = 0)
		{
		}

		public void SendItemCombineRequest(ushort index1, ItemSlotTypeEnum type1, ushort index2, ItemSlotTypeEnum type2, byte channel = 0)
		{
		}

		public void SendStatsUpRequest(SCharStats stats)
		{
		}

		public void SendQuestAction(QuestActionEnum action, int questId, uint npcGaeaId = 0u, ushort talkId = 0)
		{
		}

		private void ApplyQuestStateSyncToHolder(F2C_QuestStateSync sync)
		{
		}

		private void ApplyQuestProgressToHolder(F2C_QuestProgressUpdate prog)
		{
		}

		public void SendNpcBuyRequest(ushort npcGaeaId, byte npcTabIndex, byte slotIndex, ushort buyNum)
		{
		}

		public void SendNpcShopBuyRequest(ushort npcGaeaId, RAN.Mobile.GameModels.SNativeID itemId, ushort shopType, ushort buyNum)
		{
		}

		public void SendNpcItemExchangeRequest(ushort npcGaeaId, ushort tradeId)
		{
		}

		public void SendNpcTradeItemRequest(ushort npcGaeaId, ushort statementId, ushort convIndex, ushort answerIndex)
		{
		}

		public void SendNpcBrightCleanseRequest(ushort npcGaeaId)
		{
		}

		private void HandleSkillPointUpdate(F2C_SkillPointUpdate msg)
		{
		}

		private void HandleNpcTradeItemResult(F2C_NpcTradeItemResult msg)
		{
		}

		private void HandleNpcBrightCleanseResult(F2C_NpcBrightCleanseResult msg)
		{
		}

		private void HandleNpcItemExchangeResult(F2C_NpcItemExchangeResult msg)
		{
		}

		public void SendNpcSaleRequest(ushort npcGaeaId, ushort slotIndex)
		{
		}

		public void SendRegenGateRequest(ushort npcGaeaId)
		{
		}

		public void SendCureRequest(ushort npcGaeaId)
		{
		}

		public void SendGestureRequest(ushort gestureId)
		{
		}

		public void SendRandomBoxOpenRequest(ushort index)
		{
		}

		public void SendInvenDrugRequest(ushort index)
		{
		}

		public void SendStorageGetRequest(ushort npcGaeaId, byte channel)
		{
		}

		public void SendStorageCloseRequest()
		{
		}

		public void SendStorageSaveMoneyRequest(ushort npcGaeaId, long money)
		{
		}

		public void SendStorageDrawMoneyRequest(ushort npcGaeaId, long money)
		{
		}

		public void SendItemBankGetRequest()
		{
		}

		public void SendItemBankClaimRequest(long id)
		{
		}

		public void SendLockerSlotUpdateRequest(ushort index1, ItemSlotTypeEnum type1, ushort index2, ItemSlotTypeEnum type2, byte channel)
		{
		}

		public void SendItemSlotUpdateRequest(ItemSlotTypeEnum type1, ushort index1, ItemSlotTypeEnum type2, ushort index2)
		{
		}

		public void SendTradeRequest(uint targetGaeaId)
		{
		}

		public void SendTradeAnswer(uint masterGaeaId, byte answer)
		{
		}

		public void SendTradeCancel()
		{
		}

		public void SendTradeMoney(long money)
		{
		}

		public void SendTradeItemRegister(byte tradeSlot, ushort inventoryIndex)
		{
		}

		public void SendTradeItemRemove(byte tradeSlot)
		{
		}

		public void SendTradeLock()
		{
		}

		public void SendTradeAgree()
		{
		}

		public void SendGrindingRegisterItem(ushort inventoryIndex, byte grindingType)
		{
		}

		public void SendGrindingRemoveItem(byte grindingSlot)
		{
		}

		public void SendGrindingRequest()
		{
		}

		public void SendGrindingCancel()
		{
		}

		public void SendGMGenerateItem(ushort mainId, ushort subId, ushort quantity)
		{
		}

		public void SendGMGenerateItem(ushort mainId, ushort subId, ushort quantity, byte[] optTypes, float[] optValues, int optCount)
		{
		}

		public void SendGMMobGen(ushort mobMain, ushort mobSub, ushort mapMain, ushort mapSub, float posX, float posZ, ushort range, ushort num, byte thisChannel)
		{
		}

		public void SendGMMobDel(ushort mobMain, ushort mobSub, ushort mapMain, ushort mapSub, byte thisChannel)
		{
		}

		public void SendGMMoveToMapPos(ushort mapMain, ushort mapSub, float posX, float posZ)
		{
		}

		public void SendGMMoveToChar(uint targetCharId, string targetName)
		{
		}

		public void SendGMGenChar(uint targetCharId, string targetName)
		{
		}

		public void SendGMKickByCharId(uint targetCharId)
		{
		}

		public void SendGMKickAll()
		{
		}

		public void SendGMEventFact(GMEventTypeEnum eventType, ushort value)
		{
		}

		public void SendGMEventRateCmd(byte rateType, byte action, float rate)
		{
		}

		public void SendGMAutoSkill()
		{
		}

		public void SendGMBypassRefine(ushort code)
		{
		}

		public void SendPetSummon(ushort invenSlot)
		{
		}

		public void SendPetDismiss()
		{
		}

		public void SendPetGoto(Vector3 curPos, Vector3 tarPos, bool isRunning)
		{
		}

		public void SendPetStop(Vector3 pos)
		{
		}

		public void SendPetRename(ushort itemSlot, string newName)
		{
		}

		public void SendPetChangeStyle(ushort itemSlot, ushort value, byte changeType)
		{
		}

		private void HandlePetCustomizeResponse(F2C_PetCustomizeResponse msg)
		{
		}

		private void HandlePetCustomizeBroadcast(F2C_PetCustomizeBroadcast msg)
		{
		}

		public void SendPetAccessoryEquip(ushort invenSlot, byte accSlot)
		{
		}

		public void SendPetAccessoryUnequip(byte accSlot)
		{
		}

		private void HandlePetAccessoryResponse(F2C_PetAccessoryResponse msg)
		{
		}

		private void HandlePetAccessoryBroadcast(F2C_PetAccessoryBroadcast msg)
		{
		}

		public void SendPetGiveFood(ushort foodSlot, ushort petCardSlot)
		{
		}

		public void SendSetNonRebirth(bool nonRebirth)
		{
		}

		private void HandlePetGiveFoodResponse(F2C_PetGiveFoodResponse msg)
		{
		}

		private void HandlePetFullnessUpdate(F2C_PetFullnessUpdate msg)
		{
		}

		public void SendPetAction(PetActionType actionType, byte subType = 0)
		{
		}

		private void HandlePetAction(F2C_PetActionBroadcast msg)
		{
		}

		private void HandlePetSummonResponse(F2C_PetSummonResponse msg)
		{
		}

		private void HandlePetDrop(SNETDROP_PET msg)
		{
		}

		private void HandlePetGoto(F2C_PetGotoBroadcast msg)
		{
		}

		private void HandlePetGotoBatch(F2C_PetGotoBatch msg)
		{
		}

		private void HandlePetStop(F2C_PetStopBroadcast msg)
		{
		}

		public void SendSkillLearn(ushort invenSlot)
		{
		}

		public void SendSkillUpgrade(ushort mainId, ushort subId)
		{
		}

		public void SendPetSkillLearn(ushort invenSlot)
		{
		}

		public void SendPetSkillChange(byte slot, ushort mainId, ushort subId)
		{
		}

		public void SendPetDualSkillUnlock(ushort invenSlot)
		{
		}

		private void HandlePetSkillLearnResponse(F2C_PetSkillLearnResponse msg)
		{
		}

		private void HandlePetSkillChangeResponse(F2C_PetSkillChangeResponse msg)
		{
		}

		private void HandlePetDualSkillUnlockResponse(F2C_PetDualSkillUnlockResponse msg)
		{
		}

		private void HandlePetSkillLearnBroadcast(F2C_PetSkillLearnBroadcast msg)
		{
		}

		public void ClearPetSkillState()
		{
		}

		private void HandlePetSkillSync(F2C_PetSkillSync msg)
		{
		}

		private static void ApplyPetSkillFactClient(ref SkillFact fact, in Models.Library.Shared.Game.SNativeID skillId)
		{
		}

		private void HandleSkillLearnResponse(F2C_SkillLearnResponse msg)
		{
		}

		private void HandleSkillListSync(F2C_SkillListSync msg)
		{
		}

		public bool IsSchoolFreePKOn()
		{
			return false;
		}

		public void SendConfrontRequest(ushort targetGaeaId, ushort hpRate, ushort tarHpRate, ushort recover, byte bound)
		{
		}

		public void SendConfrontAnswer(ushort requesterGaeaId, ConfrontFeedbackEnum feedback)
		{
		}

		private void HandleConfrontForward(F2C_ConfrontForward msg)
		{
		}

		private void HandleConfrontFeedback(F2C_ConfrontFeedback msg)
		{
		}

		private void HandleConfrontStart(F2C_ConfrontStart msg)
		{
		}

		public float GetConfrontHpRate()
		{
			return 0f;
		}

		private void HandleConfrontFight(F2C_ConfrontFight msg)
		{
		}

		private void HandleUpdateStateBroadcast(F2C_UpdateStateBroadcast msg)
		{
		}

		private void HandleConfrontEnd(F2C_ConfrontEnd msg)
		{
		}

		public bool IsConfrontTarget(ushort gaeaId)
		{
			return false;
		}

		public bool IsDuelEliminated(ushort gaeaId)
		{
			return false;
		}

		public bool IsPK_TAR(ushort targetGaeaId, bool bForced = false)
		{
			return false;
		}

		public bool IsForcePkOn(ushort targetGaeaId)
		{
			return false;
		}

		public bool IsFriendlyFireBlocked(Unit targetUnit)
		{
			return false;
		}

		public bool IsFriendlyFireBlocked(ushort targetGaeaId)
		{
			return false;
		}

		public bool IsReActionable(CrowType targetCrow, ushort targetGaeaId, bool bEnemy)
		{
			return false;
		}

		private void HandleConfrontRecover(F2C_ConfrontRecover msg)
		{
		}

		private void HandleHostileAdd(F2C_HostileAdd msg)
		{
		}

		private void HandleHostileDel(F2C_HostileDel msg)
		{
		}

		private void HandleBrightUpdate(F2C_BrightUpdate msg)
		{
		}

		private void HandleBrightUpdateBroadcast(F2C_BrightUpdateBroadcast msg)
		{
		}

		private void HandlePKScoreUpdate(F2C_PKScoreUpdate msg)
		{
		}

		private void HandlePKDeathUpdate(F2C_PKDeathUpdate msg)
		{
		}

		private void HandlePKResuUpdate(F2C_PKResuUpdate msg)
		{
		}

		private void HandleItemTimeLmtExpired(F2C_ItemTimeLmtExpired msg)
		{
		}

		public void SendBoxOpenRequest(ushort slotIndex)
		{
		}

		private void HandleBoxOpenResponse(F2C_BoxOpenResponse msg)
		{
		}

		public void SendCostumeApply(ushort costumeSlot, ushort targetSlot)
		{
		}

		public void SendCostumeRemove(ushort cleanserSlot, ushort targetSlot)
		{
		}

		private void HandleCostumeResponse(F2C_CostumeResponse msg)
		{
		}

		private bool IsItemOnCooldownPreflight(ushort cardSlot)
		{
			return false;
		}

		public void SendGarbageCardRequest(ushort cardSlot, ushort targetSlot)
		{
		}

		private void HandleGarbageCardResponse(F2C_GarbageCardResponse msg)
		{
		}

		public void SendNonDropCardRequest(ushort cardSlot, ushort targetSlot)
		{
		}

		private void HandleNonDropCardResponse(F2C_NonDropCardResponse msg)
		{
		}

		public void SendWrapperApplyRequest(ushort cardSlot, ushort targetSlot)
		{
		}

		public void SendWrapperUnwrapRequest(ushort targetSlot)
		{
		}

		private void HandleWrapperApplyResponse(F2C_WrapperApplyResponse msg)
		{
		}

		private void HandleWrapperUnwrapResponse(F2C_WrapperUnwrapResponse msg)
		{
		}

		public void SendMoveMapCardRequest(ushort cardSlot)
		{
		}

		private void HandleMoveMapCardResponse(F2C_MoveMapCardResponse msg)
		{
		}

		public void SendRecallCardRequest(ushort cardSlot)
		{
		}

		private void HandleRecallCardResponse(F2C_RecallCardResponse msg)
		{
		}

		public void SendSkillPointResetRequest(ushort cardSlot)
		{
		}

		public void SendStatPointResetRequest(ushort cardSlot)
		{
		}

		public void SendSkillStatPointResetRequest(ushort cardSlot)
		{
		}

		public void RequestChangeSchool(ushort cardSlot, byte targetSchool)
		{
		}

		private void HandleChangeSchoolResponse(F2C_ChangeSchoolResponse msg)
		{
		}

		private void HandleSchoolRefresh(F2C_SchoolRefresh msg)
		{
		}

		public void RequestChangeName(ushort cardSlot, string newName)
		{
		}

		private void HandleChangeNameResponse(F2C_ChangeNameResponse msg)
		{
		}

		private void HandleSkillPointResetResponse(F2C_SkillPointResetResponse msg)
		{
		}

		private void ResetSkillHotkeySlots()
		{
		}

		private void HandleStatPointResetResponse(F2C_StatPointResetResponse msg)
		{
		}

		private void HandleSkillStatPointResetResponse(F2C_SkillStatPointResetResponse msg)
		{
		}

		private static void ShowResetFailedToast(ResetSkillStatResultEnum result)
		{
		}

		private void HandleBackPointUpdate(F2C_BackPointUpdate msg)
		{
		}

		private void HandleItemCoolTimeUpdate(F2C_ItemCoolTimeUpdate msg)
		{
		}

		private void HandleItemCoolTimeListSync(F2C_ItemCoolTimeListSync msg)
		{
		}

		private void HandleItemCoolTimeError(F2C_ItemCoolTimeError msg)
		{
		}

		private void HandlePositionSnap(F2C_PositionSnap msg)
		{
		}

		private void HandleStateBlowBroadcast(F2C_StateBlowBroadcast m)
		{
		}

		private StateBlow[] ResolveStateBlowSlots(CrowType targetCrow, ushort gaeaId)
		{
			return null;
		}

		private void HandleCureStateBlowBroadcast(F2C_CureStateBlowBroadcast m)
		{
		}

		public void SendBusTravelRequest(ushort npcGaeaId, Models.Library.Shared.Game.SNativeID destMapId, byte gateId)
		{
		}

		private void HandleBusTravelResponse(F2C_BusTravelResponse msg)
		{
		}

		public void SendPMarketTitle(string title)
		{
		}

		public void SendPMarketRegItem(ushort invenSlot, long price, uint quantity)
		{
		}

		public void SendPMarketDisItem(ushort saleSlot)
		{
		}

		public void SendPMarketOpen(ushort permitSlot)
		{
		}

		public void SendPMarketClose()
		{
		}

		public void SendPMarketItemInfo(ushort vendorGaeaId)
		{
		}

		public void SendPMarketBuy(ushort vendorGaeaId, ushort saleSlot, uint quantity)
		{
		}

		private void HandlePMarketTitleFB(F2C_PMarketTitleFB msg)
		{
		}

		private void HandlePMarketRegItemFB(F2C_PMarketRegItemFB msg)
		{
		}

		private void HandlePMarketDisItemFB(F2C_PMarketDisItemFB msg)
		{
		}

		private void HandlePMarketOpenFB(F2C_PMarketOpenFB msg)
		{
		}

		private void HandlePMarketOpenBrd(F2C_PMarketOpenBrd msg)
		{
		}

		private void HandlePMarketCloseBrd(F2C_PMarketCloseBrd msg)
		{
		}

		private void HandlePMarketBuyFB(F2C_PMarketBuyFB msg)
		{
		}

		private void HandlePMarketItemInfoBrd(F2C_PMarketItemInfoBrd msg)
		{
		}

		private void HandlePMarketItemUpdateBrd(F2C_PMarketItemUpdateBrd msg)
		{
		}

		public void SendPlayerAttackRequest(ushort animSelect, CrowType crowType, uint targetId)
		{
		}

		public void SendPlayerAttackCancelRequest()
		{
		}

		public void SendPlayerSkillRequest(ushort skillMainId, ushort skillSubId, bool isDefenseSkill, float targetPosX, float targetPosY, float targetPosZ, ushort targetNum, CrowType[] targetCrows, uint[] targetIds, GameVec3[] targetPositions, float moveVelo = 0f, float usedTotalRange = 0f, float usedTotalAngle = 0f)
		{
		}

		public void SendPlayerSkillCancelRequest(ushort skillMainId, ushort skillSubId)
		{
		}

		public void JoinServerAndConnectToZone(Action<bool, string> callback)
		{
		}

		private void ConnectToZone(string host, ushort port, uint sessionNonce, Action<bool, string> callback)
		{
		}

		private static long ReplayKey(PlayerHandler.ReplayEntityKind kind, uint id)
		{
			return 0L;
		}

		private void RecycleReplayList(List<BufferedZonePacket> list)
		{
		}

		private bool TryBufferZonePacket(ushort opcode, IMessage packet)
		{
			return false;
		}

		public void ReplayEntity(PlayerHandler.ReplayEntityKind kind, uint id)
		{
		}

		public void DropReplay(PlayerHandler.ReplayEntityKind kind, uint id)
		{
		}

		public void ClearReplayBuffer()
		{
		}

		private static bool TryGetReplaySubject(ushort opcode, IMessage packet, out PlayerHandler.ReplayEntityKind kind, out uint id)
		{
			kind = default;
			id = default;
			return false;
		}

		private void OnZonePacket(ushort opcode, IMessage packet)
		{
		}

		private void OnLoginServerPacket(ushort opcode, IMessage packet)
		{
		}

		private void OnAgentPacket(ushort opcode, IMessage packet)
		{
		}

		private void HandleActionResponse(F2C_ActionResponse resp)
		{
		}

		private void HandleActStateBroadcast(F2C_ActStateBroadcast resp)
		{
		}

		private void HandleGoToBatch(F2C_GoToBatch batch)
		{
		}

		private void HandleBundle(F2C_Bundle bundle)
		{
		}

		private void HandleGoToBroadcast(F2C_GoToBroadcast resp)
		{
		}

		private void HandleMobGoToBroadcast(F2C_MobGoToBroadcast resp)
		{
		}

		private void HandleDropOut(SNETDROP_OUT dropOut)
		{
		}

		private void HandleChatBroadcast(F2C_ChatBroadcast chat)
		{
		}

		private void HandleCustomBroadcast(A2C_CustomBroadcast msg)
		{
		}

		private void HandleBossKnockNotice(A2C_BossKnockNotice msg)
		{
		}

		private void HandleBotChallenge(F2C_BotChallenge msg)
		{
		}

		private void HandleBotChallengeResult(F2C_BotChallengeResult msg)
		{
		}

		public void SendBotChallengeAnswer(uint challengeId, string answer)
		{
		}

		public void SendGMVerify(string charName)
		{
		}

		public void SendGMBotCheck(bool enabled)
		{
		}

		public void SendGMSwitch(GMGlobalSwitch sw, bool enabled)
		{
		}

		public void SendGMSwitchStatus(GMGlobalSwitch sw)
		{
		}

		public void SendGMVerify(string charName, GMVerifyMode mode)
		{
		}

		private void HandleItemUpgradeNotice(A2C_ItemUpgradeNotice msg)
		{
		}

		private void HandleBossRepulseNotice(A2C_BossRepulseNotice msg)
		{
		}

		private void HandlePKKillNotice(A2C_PKKillNotice msg)
		{
		}

		private void HandleItemSlotUpdateBroadcast(F2C_ItemSlotUpdateBroadcast broad)
		{
		}

		private void HandleInventoryMoveBlocked(F2C_InventoryMoveBlockedFB msg)
		{
		}

		public void SendLockerRentalUse(ushort cardSlot)
		{
		}

		private void HandleLockerRentalUseResponse(F2C_LockerRentalUseResponse msg)
		{
		}

		public void SendInventoryRowExpand(ushort cardSlot)
		{
		}

		private void HandleInventoryRowExpandResponse(F2C_InventoryRowExpandResponse msg)
		{
		}

		private void OnDestroy()
		{
		}

		public void SendRebirthRequest(bool useRosary)
		{
		}

		public void SendGateTransferRequest(ushort gateId)
		{
		}

		internal void WireGateTransferHandlers()
		{
		}

		internal void ProcessZoneIdentifyArrival(S_ZoneIdentifyResponse zoneResp)
		{
		}

		internal void ProcessCharacterInventoryArrival(S_CharacterInventoryData invData)
		{
		}

		private static SItemCustom[] CloneItems(SItemCustom[] src)
		{
			return null;
		}

		private void HandleRandomBoxOpenResponse(F2C_RandomBoxOpenResponse msg)
		{
		}

		private void HandleGateTransferFailed(F2C_GateTransferFailed msg)
		{
		}

		private void HandleRegenGateResponse(F2C_RegenGateResponse msg)
		{
		}

		private void HandleQItemFactBroadcast(F2C_QItemFactBroadcast msg)
		{
		}

		private void HandleQItemFactEndBroadcast(F2C_QItemFactEndBroadcast msg)
		{
		}

		private void ApplyQItemLabel(ItemQuestionTypeEnum type, float fTime)
		{
		}

		private static void ShowMaxSplash(GameObject maxSplash, GameObject fallbackSplash)
		{
		}

		private void HideItemQuestion()
		{
		}

		private void HandleMapEntryReset(F2C_MapEntryResetBroadcast msg)
		{
		}

		private void SnapshotLocalSkillFacts(PlayerController player)
		{
		}

		public void RestorePendingSkillFacts(PlayerController pc)
		{
		}

		private void HandleEventFactBroadcast(F2C_EventFactBroadcast msg)
		{
		}

		private void HandleEventFactEndBroadcast(F2C_EventFactEndBroadcast msg)
		{
		}

		private void HandleEventFactInfo(F2C_EventFactInfo msg)
		{
		}

		private void HandleEventStateInfo(F2C_EventStateInfo msg)
		{
		}

		public void SendGMEventStateCmd(byte action, int startTime, int busterTime, float expRate, float itemRate, int minLv, int maxLv, int endMin)
		{
		}

		private void HandleLandInfo(F2C_LandInfo msg)
		{
		}

		private void HandleServerInfo(F2C_ServerInfo msg)
		{
		}

		public void SendSchoolFreePKCmd(byte action)
		{
		}

		public void SendBrightEventCmd(byte action, int durationMin)
		{
		}

		private void HandleQBoxOptionMember(F2C_QBoxOptionMember msg)
		{
		}

		private void HandleChatLoudSpeakerFB(F2C_ChatLoudSpeakerFB msg)
		{
		}

		public void SendQBoxOptionRequest(bool bEnable)
		{
		}

		private void HandleRebirthResponse(F2C_RebirthResponse resp)
		{
		}

		private void OnApplicationQuit()
		{
		}

		private void OnDisable()
		{
		}

		private void HandleMapTransferResponse(A2C_MapTransferResponse resp)
		{
		}

		public float GetClubCooldownRemaining(ClubOp op)
		{
			return 0f;
		}

		public float GetClubCooldownProgress(ClubOp op)
		{
			return 0f;
		}

		public bool IsClubOpOnCooldown(ClubOp op)
		{
			return false;
		}

		private static float ClubCooldownDurationFor(ClubOp op)
		{
			return 0f;
		}

		private void StampClubCooldown(ClubOp op)
		{
		}

		private void HandlePartyFieldsRefresh(F2C_PartyFieldsRefresh msg)
		{
		}

		private void RefreshLocalUnitClubState()
		{
		}

		public ClubMarkCacheEntry GetOrFetchClubMark(uint clubId, uint serverKnownVer)
		{
			return null;
		}

		[Obsolete("Per-character alias as of 2026-05-07. Read Unit.GuildAliasName directly.")]
		public string GetClubAlias(uint clubId)
		{
			return null;
		}

		public void SendClubCreateRequest(string clubName, ushort npcGaeaId)
		{
		}

		public void SendClubDissolveRequest()
		{
		}

		public void SendClubInviteRequest(ushort targetGaeaId)
		{
		}

		public void SendClubInviteAnswer(uint inviterCharId, ClubInviteAnswerEnum answer)
		{
		}

		public void SendClubKickRequest(uint targetCharId)
		{
		}

		public void SendClubLeaveRequest()
		{
		}

		public void SendClubMasterTransferRequest(uint targetCharId)
		{
		}

		public void SendClubSetOfficerFlags(uint targetCharId, byte flags)
		{
		}

		public void SendClubUpdateNotice(string notice)
		{
		}

		public void SendClubUpdateAlias(string alias)
		{
		}

		public void SendClubInfoRequest()
		{
		}

		public void SendClubMarkUpload(byte[] image)
		{
		}

		public void SendClubMarkInfoRequest(uint clubId, uint cachedVer)
		{
		}

		public void SendClubDonateGold(long goldAmount)
		{
		}

		public void SendClubRankUpRequest(ushort npcGaeaId)
		{
		}

		public void SendClubRelationToggle(uint targetClubId, ClubRelationKindEnum kind, bool isAdd)
		{
		}

		public void SendClubAllianceCmd(ClubAllianceOpEnum op, uint targetCharId, uint targetClubId, bool answer)
		{
		}

		public void SendClubRenameRequest(string newName)
		{
		}

		private void HandleClubCreateResponse(F2C_ClubCreateResponse m)
		{
		}

		private void HandleClubDissolveNotify(F2C_ClubDissolveNotify m)
		{
		}

		private void HandleClubInviteAsk(F2C_ClubInviteAsk m)
		{
		}

		private void HandleClubInviteResult(F2C_ClubInviteResult m)
		{
		}

		private void HandleClubMemberAdd(F2C_ClubMemberAdd m)
		{
		}

		private void HandleClubMemberRemove(F2C_ClubMemberRemove m)
		{
		}

		private void HandleClubMasterChange(F2C_ClubMasterChange m)
		{
		}

		private void HandleClubMemberFlagsUpdate(F2C_ClubMemberFlagsUpdate m)
		{
		}

		private void HandleClubNoticeUpdate(F2C_ClubNoticeUpdate m)
		{
		}

		private void HandleClubAliasUpdate(F2C_ClubAliasUpdate m)
		{
		}

		private void HandleClubMemberStateUpdate(F2C_ClubMemberStateUpdate m)
		{
		}

		private void HandleClubLivingPointsUpdate(F2C_ClubLivingPointsUpdate m)
		{
		}

		private void HandleClubRankUpdate(F2C_ClubRankUpdate m)
		{
		}

		private void HandleClubFieldsRefresh(F2C_ClubFieldsRefresh m)
		{
		}

		private void HandleClubRenamed(F2C_ClubRenamed m)
		{
		}

		private void HandleClubRelationChanged(F2C_ClubRelationChanged m)
		{
		}

		private void HandleClubAllianceAsk(F2C_ClubAllianceAsk m)
		{
		}

		private void HandleClubAllianceResult(F2C_ClubAllianceResult m)
		{
		}

		private void HandleClubAllianceChanged(F2C_ClubAllianceChanged m)
		{
		}

		private void HandleClubInfoResponse(F2C_ClubInfoResponse m)
		{
		}

		private void HandleClubMembershipResponse(F2C_ClubMembershipResponse m)
		{
		}

		private void HandleClubMarkResult(F2C_ClubMarkResult m)
		{
		}

		private void HandleClubMarkInfo(F2C_ClubMarkInfo m)
		{
		}

		private static void CopyClubInto(SClub src, SClub dst)
		{
		}

		private static void CopyClubMemberInto(SClubMember src, SClubMember dst)
		{
		}

		private static SCWRank[] NewRankCache()
		{
			return null;
		}

		private void HandleClubWarStartNotice(A2C_ClubWarStartNotice m)
		{
		}

		private void HandleClubWarEndNotice(A2C_ClubWarEndNotice m)
		{
		}

		private void HandleClubWarRemain(A2C_ClubWarRemain m)
		{
		}

		private void HandleClubWarCertifyAnnounce(A2C_ClubWarCertifyAnnounce m)
		{
		}

		private void HandleClubWarCertifyResult(F2C_ClubWarCertifyResult m)
		{
		}

		private void HandleClubWarRanking(F2C_ClubWarRanking m)
		{
		}

		private static void ShowClubWarToast(string text)
		{
		}

		public void SendClubWarCertifyRequest(ushort npcGaeaId)
		{
		}

		public void SendClubWarRankingRequest()
		{
		}

		public void SendGMClubWarCmd(byte action, ushort guidId)
		{
		}

		public TyrannyPlayerData GetTyrannyRank(int i)
		{
			return null;
		}

		public TyrannyPlayerData GetTyrannyRewardTop(int i)
		{
			return null;
		}

		private static TyrannyPlayerData[] NewPlayerCache(int n)
		{
			return null;
		}

		private void HandleTyrannyRegisterFb(A2C_TyrannyRegisterFb m)
		{
		}

		private void HandleTyrannyRejoinFb(A2C_TyrannyRejoinFb m)
		{
		}

		private void HandleTyrannyQueueMoved(A2C_TyrannyQueueMoved m)
		{
		}

		private void HandleTyrannyQueueUpdate(A2C_TyrannyQueueUpdate m)
		{
		}

		private void HandleTyrannyTowerInfo(A2C_TyrannyTowerInfo m)
		{
		}

		private void HandleTyrannyBattleInfo(A2C_TyrannyBattleInfo m)
		{
		}

		private void HandleTyrannyToBattleTime(A2C_TyrannyToBattleTime m)
		{
		}

		private void HandleTyrannyRankInfo(A2C_TyrannyRankInfo m)
		{
		}

		private void HandleTyrannyStateRegister(A2FC_TyrannyStateRegister m)
		{
		}

		private void HandleTyrannyStateBattle(A2FC_TyrannyStateBattle m)
		{
		}

		private void HandleTyrannyStateReward(A2FC_TyrannyStateReward m)
		{
		}

		private void HandleTyrannyStateEnded(A2FC_TyrannyStateEnded m)
		{
		}

		private void HandleTyrannyNextSched(A2FC_TyrannyNextSched m)
		{
		}

		private void HandleTyrannyTowerCapture(F2C_TyrannyTowerCapture m)
		{
		}

		private void HandleTyrannyTowerWinner(F2C_TyrannyTowerWinner m)
		{
		}

		private void HandleTyrannyRankingUpdate(F2C_TyrannyRankingUpdate m)
		{
		}

		private void HandleTyrannyRankingEnd(F2C_TyrannyRankingEnd m)
		{
		}

		private void HandleTyrannyCrowOwner(F2C_TyrannyCrowOwner m)
		{
		}

		private void HandleTyrannyCrowDamage(F2C_TyrannyCrowDamage m)
		{
		}

		public void InvokeTyrannyCrowDamage(ushort crowGlobId, float pctSG, float pctMP, float pctPHX)
		{
		}

		public void InvokeTyrannyCrowOwner(ushort crowGlobId, ushort owner)
		{
		}

		private void HandleTyrannyReviveFb(F2C_TyrannyReviveFb m)
		{
		}

		public void SendTyrannyRegisterReq(bool register, ushort level, ushort itemMain = 0, ushort itemSub = 0)
		{
		}

		public void SendTyrannyRejoinReq()
		{
		}

		public void SendTyrannyMapExitReq()
		{
		}

		public void SendTyrannyTowerInfoReq()
		{
		}

		public void SendTyrannyReviveReq()
		{
		}

		public void SendGMTyrannyCmd(byte action)
		{
		}

		private static void TyrannyMsg(string text, Color color)
		{
		}

		private static void TyrannyInfo(string text)
		{
		}

		private static void TyrannyError(string text)
		{
		}

		private static string TySchool(ushort s)
		{
			return null;
		}

		private static string RegisterFbText(TyrannyRegisterFb fb, ushort queueNum)
		{
			return null;
		}

		private static bool RegisterFbOk(TyrannyRegisterFb fb)
		{
			return false;
		}

		private static string RejoinFbText(TyrannyRejoinFb fb)
		{
			return null;
		}

		private static void ShowTyrannyWarToast(string text)
		{
		}

		private static void CopyPlayer(TyrannyPlayerData src, TyrannyPlayerData dst)
		{
		}

		private static void CopyScheduleNext(TyrannyScheduleNext src, TyrannyScheduleNext dst)
		{
		}
	}
}
