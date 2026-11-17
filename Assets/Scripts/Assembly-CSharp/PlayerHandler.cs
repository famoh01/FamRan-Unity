using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Server;
using Core.Enumerators;
using Models.Library.Shared.Game;
using Models.Network.Shared.Enums;
using Models.Network.Shared.Response;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
	public enum ReplayEntityKind
	{
		Mob = 0,
		Player = 1,
		Pet = 2
	}

	private readonly struct ConeQuery
	{
		public readonly bool Enabled;

		public readonly float Ox;

		public readonly float Oz;

		public readonly float Dx;

		public readonly float Dz;

		public readonly float RangeSqr;

		public readonly float InvRange;

		public readonly float MinCos;

		public readonly float InvAngSpan;

		public readonly float AngleWeight;

		public ConeQuery(Vector3 origin, Vector3 dirXZ, float range, float halfAngleDeg, float angleWeight)
		{
			Enabled = false;
			Ox = 0f;
			Oz = 0f;
			Dx = 0f;
			Dz = 0f;
			RangeSqr = 0f;
			InvRange = 0f;
			MinCos = 0f;
			InvAngSpan = 0f;
			AngleWeight = 0f;
		}
	}

	public static PlayerHandler Instance;

	public LinkedList<Unit> playerClientList;

	public Unit[] playerClientArray;

	private readonly Dictionary<string, Unit> _playersByName;

	public LinkedList<UnitCrow> crowsList;

	public UnitCrow[] crowsArray;

	public LinkedList<GroundItem> itemsDropList;

	public GroundItem[] itemsDropArray;

	public LinkedList<GroundGold> moneyDropList;

	public GroundGold[] moneyDropArray;

	[CompilerGenerated]
	private Action<ItemObject, int> m_FieldItemPickupSucceeded;

	[CompilerGenerated]
	private Action<long> m_FieldMoneyPickupSucceeded;

	private readonly Dictionary<int, Stack<GameObject>> _mobPool;

	private readonly Queue<CrowSpawnData> _spawnQueue;

	private readonly HashSet<uint> _pendingDespawns;

	private readonly HashSet<uint> _queuedCrowIds;

	private readonly Dictionary<int, Stack<GameObject>> _playerPool;

	private readonly Queue<SDROP_CHAR> _playerSpawnQueue;

	private readonly HashSet<ushort> _pendingPlayerDespawns;

	private readonly HashSet<uint> _queuedPlayerIds;

	private readonly Queue<ushort> _playerDespawnQueue;

	private readonly HashSet<ushort> _pendingDespawnSet;

	private readonly Queue<ushort> _crowDespawnQueue;

	private readonly HashSet<ushort> _pendingCrowDespawnSet;

	private readonly Queue<uint> _petDespawnQueue;

	private readonly HashSet<uint> _pendingPetDespawnSet;

	private readonly Stack<SDROP_CHAR> _sdropCharPool;

	private readonly Queue<SDROP_PET> _petSpawnQueue;

	private readonly HashSet<uint> _pendingPetDespawns;

	private readonly HashSet<uint> _queuedPetIds;

	private readonly Stack<SDROP_PET> _sdropPetPool;

	private readonly Dictionary<uint, ElementEnum> _attackElementByTarget;

	private const float SpawnBudgetMs = 3f;

	private const float SpawnBudgetLoaderMs = 14f;

	private const int MaxPlayersPerFrame = 6;

	private const int MaxPlayersPerFrameLoader = 16;

	private const int MaxCrowsPerFrame = 8;

	private const int MaxCrowsPerFrameLoader = 16;

	private const int MaxPetsPerFrame = 4;

	private const int MaxPetsPerFrameLoader = 8;

	private const int MaxPlayerDespawnsPerFrame = 3;

	private const int MaxCrowDespawnsPerFrame = 3;

	private const int MaxPetDespawnsPerFrame = 3;

	private const int DespawnCatchUpFrames = 6;

	private const int DespawnHardCap = 12;

	private const float MeshShowDist = 190f;

	private const float MeshHideDist = 200f;

	private static readonly float MeshShowDistSqr;

	public static readonly float MeshHideDistSqr;

	private const float NameplateShowDist = 240f;

	private const float NameplateHideDist = 260f;

	private static readonly float NameplateShowDistSqr;

	private static readonly float NameplateHideDistSqr;

	public static readonly float VfxShowDistSqr;

	public static readonly float VfxHideDistSqr;

	public static readonly float FarRevealDistSqr;

	[SerializeField]
	[Tooltip("ON (default): mob/PC/pet spawns are queued and drained over frames so a crowded AOI entry doesn't hitch (and action packets for not-yet-spawned entities are buffered + replayed on spawn). OFF: entities spawn immediately on packet receipt — original behavior, no frame-budgeting and no replay buffering. Runtime-safe to flip.")]
	private bool useSpawnQueue;

	public int maxPlayerClient;

	public int gaeaId;

	public Maps currentMap;

	private Transform _dropContainer;

	private Transform _characterContainer;

	private List<TargetInfoEx> targetInfos;

	private LinkedListNode<UnitCrow> currentCrowNode;

	private LinkedListNode<Unit> currentPlayerNode;

	private string[] onlinePlayerList;

	private bool setItemQFact;

	private GameObject _myPetObject;

	private uint _myPetGlobalId;

	public const ushort NoneSlot = 65535;

	public const int InventoryDefaultRows = 4;

	public const int InventoryMaxRows = 10;

	private readonly Dictionary<uint, GameObject> _remotePets;

	private readonly Dictionary<int, Stack<GameObject>> _petPool;

	[Header("Off-screen animation LOD")]
	[Tooltip("Remote players outside the camera frustum (character-sized box with a margin) stop writing bone transforms (Animator CullUpdateTransforms); the state machine, movement and everything else keep running. Off = every remote poses every frame.")]
	public bool EntityAnimationLOD;

	private Camera _lodCamera;

	private readonly Plane[] _lodFrustum;

	private static readonly Vector3 LodBoundsCenter;

	private static readonly Vector3 LodBoundsSize;

	private static readonly Vector3 LodBoundsMobCenter;

	private static readonly Vector3 LodBoundsMobSize;

	private const float LodFrustumMarginDeg = 6f;

	private int _lodFrame;

	private int _lastVisualFrame;

	private const float ManualStackTolerance = 3f;

	private static readonly Comparison<TargetInfoEx> _crowTypeComparer;

	public bool UseSpawnQueue
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public ClientCellGrid CellGrid { get; private set; }

	public uint MyPetGlobalId => 0u;

	public GameObject MyPetObject => null;

	public ushort MyActivePetCardSlot { get; set; }

	public byte MyInvenLine { get; set; }

	public int MyEnabledInventoryRows => 0;

	public int MyEnabledInventorySlots => 0;

	private static double NowMs => 0.0;

	public bool HasQueuedSpawns => false;

	private static int IdlePoolFloor => 0;

	public static bool LowMemoryDevice => false;

	public static int MaxIdlePlayerShellsPerGender => 0;

	public static int MaxIdleMobShellsPerType => 0;

	public event Action<ItemObject, int> FieldItemPickupSucceeded
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

	public event Action<long> FieldMoneyPickupSucceeded
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

	public static bool IsWithinMeshRange(Vector3 pos)
	{
		return false;
	}

	public void InitCellGrid()
	{
	}

	private Transform GetDropContainer()
	{
		return null;
	}

	private Transform GetCharacterContainer()
	{
		return null;
	}

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private static int AdaptiveDespawnCap(int queued, int baseCap)
	{
		return 0;
	}

	private void DrainSpawnQueues()
	{
	}

	private void TopUpPlayerPool()
	{
	}

	public void MobAvoidResponseMessage(F2C_MobAvoidBroadcast message)
	{
	}

	public void MobDamageResponseMessage(F2C_MobDamageBroadcast message)
	{
	}

	public void MobAttackResponseMessage(F2C_MobAttackBroadcast message)
	{
	}

	public void MobSkillActionResponseMessage(F2C_MobSkillActionBroadcast message)
	{
	}

	public void PlayerSkillHoldBroadcastResponseMessage(F2C_SkillHoldBroadcast message)
	{
	}

	public void PlayerTyrannyCrowOwnerResponseMessage(F2C_TyrannyCrowOwner message)
	{
	}

	public void PlayerSystemBuffResponseMessage(F2C_SystemBuff message)
	{
	}

	public void PlayerLandEffectResponseMessage(F2C_LandEffect message)
	{
	}

	public void PlayerSkillFactBroadcastResponseMessage(F2C_SkillFactBroadcast message)
	{
	}

	public void PlayerSkillCancelBroadcastResponseMessage(F2C_SkillCancelBroadcast message)
	{
	}

	public void PlayerPushPullResponseMessage(F2C_PushPull message)
	{
	}

	public void PlayerActionSkillBroadcastResponseMessage(F2C_SkillActionBroadcast message)
	{
	}

	public void PlayerRequireSkillResponseMessage(F2C_PlayerSkillResponse message)
	{
	}

	public void PlayerAttackAvoidBroadcastResponseMessage(F2C_PlayerAvoidBroadcast message)
	{
	}

	public void PlayerAttackAvoidResponseMessage(F2C_PlayerAvoidResponse message)
	{
	}

	public void PlayerAttackDamageBroadcastResponseMessage(F2C_PlayerDamageBroadcast message)
	{
	}

	public void PlayerAttackDamageResponseMessage(F2C_PlayerDamageResponse message)
	{
	}

	public void PlayerAttackCancelBroadcastResponseMessage(F2C_PlayerAttackCancelBroadcast message)
	{
	}

	public void PlayerAttackBroadcastResponseMessage(F2C_PlayerAttackBroadcast message)
	{
	}

	public void QueueDropCrow(SNETDROP_CROW message)
	{
	}

	public void QueueDropPlayer(SNETDROP_PC packet)
	{
	}

	private SDROP_CHAR RentSDropChar()
	{
		return null;
	}

	private void ReturnSDropChar(SDROP_CHAR data)
	{
	}

	public void QueueDropPet(SDROP_PET source)
	{
	}

	private SDROP_PET RentSDropPet()
	{
		return null;
	}

	private void ReturnSDropPet(SDROP_PET data)
	{
	}

	public int ProcessPetSpawnQueue(int maxThisCall = 2147483647)
	{
		return 0;
	}

	public int ProcessSpawnQueue(int maxThisCall = 2147483647)
	{
		return 0;
	}

	public int ProcessPlayerSpawnQueue(int maxThisCall = 2147483647)
	{
		return 0;
	}

	private void DropPlayer(ref SDROP_CHAR data)
	{
	}

	public void SpawnPet(SDROP_PET data)
	{
	}

	public void DespawnMyPet()
	{
	}

	public void ClearActivePetCardSlot()
	{
	}

	public void DespawnPetByGlobalId(uint globalId)
	{
	}

	public void ProcessPetDespawnQueue(int maxThisCall = 2147483647)
	{
	}

	private void DespawnPetImmediate(uint globalId)
	{
	}

	private void ReturnPetToPool(GameObject petGO)
	{
	}

	public void MovePet(uint petGUID, Vector3 curPos, Vector3 target, bool isRunning)
	{
	}

	public void StopPet(uint petGUID, Vector3 pos)
	{
	}

	public void PlayPetAction(uint petGUID, PetActionType actionType, byte subType)
	{
	}

	public void PlayPetSkillLearnEffect(uint petGUID)
	{
	}

	public void UpdatePetCustomization(uint petGUID, byte changeType, byte style, byte color, string name)
	{
	}

	private GameObject FindPetObject(uint petGUID)
	{
		return null;
	}

	private GameObject RentPlayer(CharacterClassEnum charClass, Vector3 pos, Quaternion rot, out bool isPooled)
	{
		isPooled = default;
		return null;
	}

	private static bool IsClassFemale(CharacterClassEnum charClass)
	{
		return false;
	}

	public void DespawnAllFieldEntities()
	{
	}

	public void TrimPlayerPool(int keepPerGender)
	{
	}

	public void TrimMobPool(int keepPerType)
	{
	}

	public void ClearPlayerPool()
	{
	}

	public void PrewarmPlayerPool(int maleTarget, int femaleTarget)
	{
	}

	public void PrewarmSDropCharPool(int target)
	{
	}

	public void PrewarmSDropPetPool(int target)
	{
	}

	private void PrewarmPlayerShells(int genderKey, int target, CharacterClassEnum prefabClass)
	{
	}

	private void DropCrow(ref CrowSpawnData data)
	{
	}

	public bool IsEntitySpawned(ReplayEntityKind kind, uint id)
	{
		return false;
	}

	public bool IsEntityQueued(ReplayEntityKind kind, uint id)
	{
		return false;
	}

	public void PlayerUpdateExperienceResponseMessage(F2C_UpdateExperience message)
	{
	}

	public void PlayerRequireLevelUpBroadastMessage(F2C_LevelUpBroadcast message)
	{
	}

	public void PlayerRequireLevelUpResponseMessage(F2C_LevelUp message)
	{
	}

	public void HandleItemDropToFieldResponse(F2C_ItemDropToFieldResponse message)
	{
	}

	public void HandleFieldPickupItemResponse(F2C_FieldPickupItemResponse message)
	{
	}

	public void HandleFieldPickupMoneyResponse(F2C_FieldPickupMoneyResponse message)
	{
	}

	public void HandleInventoryUpdateResponse(F2C_InventoryUpdateResponse message)
	{
	}

	public void HandleDropItemOnField(SNETDROP_ITEM message)
	{
	}

	private static Vector3 GroundDropPosition(Vector3 pos)
	{
		return default;
	}

	public void HandleDropMoneyOnField(SNETDROP_MONEY message)
	{
	}

	public void HandleStatsUpResponse(F2C_StatsUpResponse message)
	{
	}

	public void HandleMoneyUpdateResponse(F2C_MoneyUpdateResponse message)
	{
	}

	public void HandleGestureBroadcast(F2C_GestureBroadcast message)
	{
	}

	public void HandleStorageGetResponse(F2C_StorageGetResponse message)
	{
	}

	public void HandleStorageItemResponse(F2C_StorageItemResponse message)
	{
	}

	public void HandleStorageMoneyUpdateResponse(F2C_StorageMoneyUpdateResponse message)
	{
	}

	public void HandleItemBankGetResponse(F2C_ItemBankGetResponse message)
	{
	}

	public void HandleItemBankClaimResponse(F2C_ItemBankClaimResponse message)
	{
	}

	public void HandleLockerSlotUpdateResponse(F2C_LockerSlotUpdateResponse message)
	{
	}

	private void UpdateLockerSlot(PlayerEquipment equipment, ItemSlotTypeEnum slotType, int index, SItemCustom itemCustom, byte channel)
	{
	}

	public void UpdateStateResponseMessage(F2C_UpdateState message)
	{
	}

	public void ChatBroadcastResponseMessage(F2C_ChatBroadcast message)
	{
	}

	public void CustomBroadcastResponseMessage(A2C_CustomBroadcast message)
	{
	}

	private static Color CustomBroadcastColor(CustomBroadcastTypeEnum type)
	{
		return default;
	}

	public void SpawnCharacter()
	{
	}

	private void ReloadActionSlots()
	{
	}

	private void ReloadConsumableQuickSlots()
	{
	}

	private void PlayerLoadInventoryData(CharacterSelectDataSO gameData)
	{
	}

	public void FrameMoveEntities(float dt)
	{
	}

	private bool IsVisibleForLod(in Vector3 pos)
	{
		return false;
	}

	private bool IsVisibleForLodTall(in Vector3 pos)
	{
		return false;
	}

	private bool EnsureLodFrustum(bool force = false)
	{
		return false;
	}

	private void UpdateRemoteAnimatorCulling()
	{
	}

	public static bool IsProbablyOnScreen(in Vector3 pos, bool tall = false)
	{
		return false;
	}

	private void FrameMovePet(GameObject petGO, float dt, bool cullPlate, in Vector3 viewerPos, bool lodOn)
	{
	}

	public Unit GetPlayer(ushort id)
	{
		return null;
	}

	public Unit GetPlayer(string name)
	{
		return null;
	}

	public void RegisterPlayer(ushort gaeaId, Unit unit)
	{
	}

	public void UnregisterPlayer(ushort gaeaId)
	{
	}

	public Unit GetPlayerByCharId(uint charId)
	{
		return null;
	}

	public UnitCrow GetCrow(ushort id)
	{
		return null;
	}

	public GameObject FindClosestTarget(Vector3 origin, float maxRange, bool includeMobs, bool includePlayers, out CrowType crowType, bool excludeFriendlyFire = false)
	{
		crowType = default;
		return null;
	}

	private static bool IsInsideCircle(Vector3 p, Vector3 circleCenter, float circleRadiusSqr)
	{
		return false;
	}

	private static void ConeConsider(GameObject go, Vector3 p, in ConeQuery q, CrowType type, ref GameObject best, ref float bestScore, ref CrowType bestType)
	{
	}

	public GameObject FindManualTarget(Vector3 circleCenter, float circleRadius, bool includeMobs, bool includePlayers, out CrowType crowType, bool excludeFriendlyFire, Vector3 coneOrigin, Vector3 coneDirXZ, float coneRange, float coneHalfAngleDeg, float coneAngleWeight, GameObject preferredTarget, out bool usedConeAssist)
	{
		crowType = default;
		usedConeAssist = default;
		return null;
	}

	public GameObject FindClosestTower(Vector3 origin, float maxRange = 3.4028235E+38f)
	{
		return null;
	}

	public void DespawnCrow(ushort gaeaId)
	{
	}

	public void ProcessCrowDespawnQueue(int maxThisCall = 2147483647)
	{
	}

	public void DespawnCrowImmediate(ushort gaeaId)
	{
	}

	public void DespawnItem(uint globalId)
	{
	}

	public void DespawnMoney(uint globalId)
	{
	}

	public void DespawnPlayer(ushort gaeaId)
	{
	}

	public void ProcessPlayerDespawnQueue(int maxThisCall = 2147483647)
	{
	}

	public void DespawnPlayerImmediate(ushort gaeaId, bool clearVendMirror = true)
	{
	}

	private GameObject RentMob(SNativeID mobId, GameObject prefab, Vector3 pos, Quaternion rot, out bool isPooled)
	{
		isPooled = default;
		return null;
	}

	public void ClearMobPool()
	{
	}

	public void ClearPetPool()
	{
	}

	public GroundItem GetItem(ushort id)
	{
		return null;
	}

	public GroundGold GetMoney(ushort id)
	{
		return null;
	}

	public List<TargetInfoEx> DetectCrow(ushort crowType, Vector3 fromPt, Vector3 targetPt)
	{
		return null;
	}

	public List<TargetInfoEx> DetectCrow(ushort crowType, Vector3 targetPosition, float totalRange)
	{
		return null;
	}

	public Vector3 GetTargetPosition(TargetInfoEx targetInfo)
	{
		return default;
	}

	public bool ValidCheckTarget(TargetInfoEx targetInfo)
	{
		return false;
	}

	public GameObject GetActor(TargetInfoID targetInfo)
	{
		return null;
	}

	public GameObject GetActor(ushort playerId)
	{
		return null;
	}

	public IActor GetActor(TargetInfoEx targetInfo)
	{
		return null;
	}

	public IActor GetActor(CrowType crow, GameObject gameObject)
	{
		return null;
	}

	private void SpawnDamageHit(ushort targetId, int damage, DamageTypeEnum flag, bool useCache)
	{
	}

	private void SpawnSkillHitVfx(uint targetId)
	{
	}

	private ElementEnum ResolveHitElement(ushort targetId, bool useCache)
	{
		return ElementEnum.EMELEMENT_SPIRIT;
	}

	private ElementEnum GetWeaponElement(uint attackerId, bool isLocal)
	{
		return ElementEnum.EMELEMENT_SPIRIT;
	}
}
