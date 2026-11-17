using System.Collections.Generic;
using Core.Enumerators;
using Models.Library.Shared.Game;
using UnityEngine;
using UnityEngine.UI;

public class SkillSpecial : MonoBehaviour
{
	private struct BuffIconData
	{
		public Image fillImage;

		public float totalDuration;
	}

	private enum TimedReturnKind : byte
	{
		Previous = 0,
		Destroyable = 1
	}

	private struct PendingReturn
	{
		public GameObject effect;

		public float fireTime;

		public int generation;

		public TimedReturnKind kind;

		public int playerId;

		public Vector2 skillVec;

		public (int, ushort, ushort, int) destKey;
	}

	private struct PendingSpawn
	{
		public float fireTime;

		public int playerId;

		public Vector2 skillVec;

		public GameObject prefab;

		public float lifetime;

		public float castSpeed;

		public bool isNonParent;

		public Transform container;

		public Vector3 pos;

		public Quaternion rot;

		public Transform resolveBone;
	}

	private struct PendingVolley
	{
		public ArrowShooter shooter;

		public GameObject projectile;

		public TargetInfoID[] targets;

		public float nextFireTime;

		public float loopDelayScaled;

		public int remainingShots;

		public float arrowSpeed;

		public bool ignoreFlightCap;

		public bool overrideSpawner;

		public Vector3 spawnerLocalOffset;
	}

	public static SkillSpecial instance;

	private const string EnemyMainBodyBone = "EnemyMainBody_Gum(Clone)";

	private const string EnemyFloorBone = "EnemyFloor_Gum(Clone)";

	private const string PlayerMainBodyBone = "MainBodyGum";

	private const string PlayerFloorBone = "FloorGum";

	public const int MobCasterIdOffset = 65536;

	[Header("Buff Indicator")]
	[SerializeField]
	private GameObject buffIndicatorPrefab;

	[SerializeField]
	private Transform buffIndicatorParent;

	private Dictionary<int, HashSet<Vector2>> playerCastingSkills;

	private Dictionary<(int, Vector2), List<GameObject>> previousEffects;

	private Dictionary<(int, ushort, ushort, int), List<GameObject>> destroyableEffects;

	private readonly List<(int, Vector2)> _scratchKeyPrev;

	private readonly List<(int, ushort, ushort, int)> _scratchKeyDest;

	private static readonly List<ParticleSystem> s_psBuffer;

	private Dictionary<Vector2, GameObject> buffIcon;

	private Dictionary<Vector2, BuffIconData> _buffIconData;

	private static readonly Dictionary<ElementEnum, AbnormalConditionEnum> s_ElementToCondition;

	private readonly List<PendingReturn> _pendingReturns;

	private readonly List<PendingSpawn> _pendingSpawns;

	private readonly List<PendingVolley> _pendingVolleys;

	private readonly List<Vector2> _buffIconRemoveBuffer;

	public static float RemoteVfxMaxDist;

	public static float RemoteVfxNearAlwaysDist;

	private const float MaxFastForwardSeconds = 2f;

	public static int MobCasterId(uint gaeaId)
	{
		return 0;
	}

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void UpdateBuffIcons()
	{
	}

	private void ReturnLocalDestroyableBuffVfx(ushort mainId, ushort subId)
	{
	}

	public void InitiateBuffIcon(SNativeID skillID, float buffAge)
	{
	}

	public void RemoveBuffIcon(int playerId, SNativeID skillID)
	{
	}

	public AbnormalConditionEnum GetWeaponElement(GameObject playerObj)
	{
		return AbnormalConditionEnum.None;
	}

	public bool SkillEffectNotInUsed(ElementEnum effectType, GameObject playerObj)
	{
		return false;
	}

	private EffectClientSpawner GetOrCreateSpawner(Transform playerSelf)
	{
		return null;
	}

	private void SpawnPlayerEffect(int playerId, Vector2 skillVec, EffectsData skillEffect, EffectClientSpawner spawner, Transform selfTransform, short charClass, float lifetime, float castSpeed = 1f)
	{
	}

	public static bool VfxWorthSpawning(Transform at)
	{
		return false;
	}

	public void SpawnCastEffects(int casterId, SNativeID skillID, GameObject caster, float castSpeed = 1f)
	{
	}

	public void SpawnMobCastEffects(int casterId, SNativeID skillID, GameObject mobCaster, float castSpeed = 1f)
	{
	}

	private void FireArrowProjectiles(ArrowShooter shooter, GameObject projectile, TargetInfoID[] targetIds, ushort count, float arrowSpeed = 300f, bool ignoreFlightCap = false, bool overrideSpawner = false, Vector3 spawnerLocalOffset = default(Vector3))
	{
	}

	public void SpawnTargetHitEffects(int casterId, int targetId, SNativeID skillID, GameObject caster, GameObject target, float castSpeed = 1f)
	{
	}

	public void HandleBuffEffects(in SkillFact skillFact, int playerId, GameObject playerGameObject = null)
	{
	}

	private static void FastForwardParticles(GameObject effect, float seconds)
	{
	}

	private void SpawnDestroyableForSkillEffect(EffectsData skillEffect, EffectClientSpawner spawner, Transform selfTransform, short charClass, List<GameObject> outList)
	{
	}

	private static void AddSpawn(SkillEffectPool pool, GameObject prefab, Transform parent, List<GameObject> outList)
	{
	}

	public void HandleEnemyBuffEffects(in SkillFact skillFact, int enemyId, GameObject enemyGameObject)
	{
	}

	public void SpawnEffect(int playerId, Vector2 skillID, GameObject effectPrefab, Transform effectContainer, float lifetime, float castSpeed = 1f)
	{
	}

	public void SpawnNPEffect(int playerId, Vector2 skillID, GameObject effectPrefab, Vector3 position, Quaternion rotation, float lifetime, float castSpeed = 1f)
	{
	}

	public void SelfSpawnNPEffect(int playerId, Vector2 skillID, EffectsData effectPrefab, Transform playerSelf, Vector3 position, Quaternion rotation, float lifetime, float castSpeed = 1f)
	{
	}

	public void SelfSpawnNPEffect(int playerId, Vector2 skillID, GameObject effectPrefab, Vector3 position, Quaternion rotation, float lifetime, float castSpeed = 1f)
	{
	}

	private static void ApplyCastSpeed(GameObject effect, float castSpeed)
	{
	}

	private static void SwapRemove<T>(List<T> list, int index)
	{
	}

	private void TickPendingReturns(float now)
	{
	}

	private void TickPendingSpawns(float now)
	{
	}

	private void TickPendingVolleys(float now)
	{
	}

	private void ScheduleReturnPrevious(int playerId, Vector2 skillVec, GameObject effect, float lifetime)
	{
	}

	private void ScheduleReturnDestroyable((int, ushort, ushort, int) key, GameObject effect, float lifetime)
	{
	}

	private void ScheduleSpawnParented(int playerId, Vector2 skillVec, GameObject prefab, Transform container, float lifetime, float delay, float castSpeed)
	{
	}

	private void ScheduleSpawnNonParent(int playerId, Vector2 skillVec, GameObject prefab, Vector3 pos, Quaternion rot, float lifetime, float delay, float castSpeed)
	{
	}

	private void ScheduleSpawnNonParentFromBone(int playerId, Vector2 skillVec, GameObject prefab, Transform bone, float lifetime, float delay, float castSpeed)
	{
	}

	private void ScheduleVolley(ArrowShooter shooter, GameObject projectile, TargetInfoID[] targets, float delayStart, int amount, float loopDelay, float castSpeed, float arrowSpeed, bool ignoreFlightCap, bool overrideSpawner = false, Vector3 spawnerLocalOffset = default(Vector3))
	{
	}

	public void ClearBuffEffects(int playerId, SNativeID skillId)
	{
	}

	public void ClearSkillCastAndTargetVfx(int playerId, in Vector2 skillVec)
	{
	}

	public void ClearAllSkillEffectsForPlayer(int playerId)
	{
	}

	public void ClearForMapTransition()
	{
	}

	public void ClearEffectsOnTarget(GameObject target)
	{
	}

	public void CancelSkillCasting(int playerId)
	{
	}

	private void CancelSkillEffect(int playerId, Vector2 skillID)
	{
	}

	private void AddPlayerCastingSkill(int playerId, Vector2 skillId)
	{
	}

	private void RemovePlayerCastingSkill(int playerId, Vector2 skillId)
	{
	}

	public bool IsPlayerCastingSkill(int playerId, Vector2 skillId)
	{
		return false;
	}
}
