using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Character_Scripts;
using Assets.Scripts.Interfaces;
using Core.Enumerators;
using Models.Library.Shared.Game;
using Models.Network.Shared.Response;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAiController : MonoBehaviour, IActor
{
	[Header("Mob Details")]
	public ushort Id;

	public CGameCrowLogic CrowLogic;

	public GameActor Actor;

	public Animator anim;

	public Transform TargetTransform;

	public TargetInfoEx TargetID;

	public string targetTag;

	public LayerMask whatIsGround;

	public LayerMask whatIsPlayer;

	public bool isDead;

	[Header("Patrolling")]
	public float walkPointRange;

	[Header("States")]
	public ActionEnums Action;

	public uint dwActState;

	[Header("ETC")]
	public MobNpcObject mobNpcObject;

	public int AttackIndex;

	public string CurrAttackAnimation;

	public float _movementSpeed;

	public ushort ActiveSkillMainId;

	public ushort ActiveSkillSubId;

	public ushort ActiveSkillLevel;

	public Vector3 vMaxOrg;

	public Vector3 vMinOrg;

	public Vector3 vMax;

	public Vector3 vMin;

	public int NavMeshSyncHandle;

	private GameObject _hpBarContainer;

	private Image _hpNameBackgroundImage;

	private Slider _hpBar;

	private Image _hpFillImage;

	private bool _hpRefsResolved;

	private TextMeshProUGUI _nameText;

	private bool _nameTextResolved;

	private readonly NameplateCuller _plateCuller;

	private static NavMeshPath _sharedNavPath;

	private static Vector3[] _sharedCornersBuffer;

	private const float PathSnapBand = 6f;

	private const float PathSnapRadius = 5f;

	private const float PathSnapTightRadius = 1f;

	private readonly List<ParticleSystem> instantiatedEffects;

	private CrowEffect _crowEffectCache;

	private static readonly List<Animator> s_animScratch;

	private float _popupAnchorHeight;

	private static readonly List<Renderer> _rendererScratch;

	public float PopupAnchorHeight => 0f;

	public void UpdateNameplateCulling(in Vector3 viewerPos, float showDistSqr, float hideDistSqr, bool onScreen = true)
	{
	}

	private static Vector3 SnapToServerLayer(Vector3 p)
	{
		return default;
	}

	public bool IsAction(ActionEnums action)
	{
		return false;
	}

	public bool IsState(ActStateEnums dwState)
	{
		return false;
	}

	public void SetSTATE(ActStateEnums state)
	{
	}

	public void ReSetSTATE(ActStateEnums state)
	{
	}

	public ushort GetBodyRadius()
	{
		return 0;
	}

	public Vector3 GetPosition()
	{
		return default;
	}

	public CrowType GetCrow()
	{
		return CrowType.CROWPC;
	}

	public uint GetId()
	{
		return 0u;
	}

	public bool IsValidBody()
	{
		return false;
	}

	public bool IsFalling()
	{
		return false;
	}

	public bool IsDie()
	{
		return false;
	}

	private void Awake()
	{
	}

	private void ResolveAnimators()
	{
	}

	private void Start()
	{
	}

	public void Initialize(MobNpcObject crowObject, Vector3 spawnPos)
	{
	}

	private bool IsNpc()
	{
		return false;
	}

	private void UpdateCrowNameColor()
	{
	}

	public void FrameMove(float dt, bool fullDetail = true, bool refreshVisual = true)
	{
	}

	public void UpdateSkillEffect(ref SkillFact[] skillFacts, ref StateBlow[] stateBlows)
	{
	}

	private bool HasDirectTarget()
	{
		return false;
	}

	private void UpdateHealth()
	{
	}

	public float GetMoveVelocity()
	{
		return 0f;
	}

	public void MoveToPosition(Vector3 targetPos)
	{
	}

	public void MoveToFromStartPos(Vector3 serverStartPos, Vector3 targetPos, Vector3 actualPos, int cornerIndex)
	{
	}

	public void TurnAction(ActionEnums action)
	{
	}

	private void SpawnSkillCastEffects()
	{
	}

	public void SetTransRotation(Vector3 targetPos)
	{
	}

	private bool TryMeasurePopupAnchorHeight(out float height)
	{
		height = default;
		return false;
	}

	public void ReceiveDamage(int damage)
	{
	}

	public void ReceiveDamage(int damage, DamageTypeEnum flag, bool skipVfx = false)
	{
	}

	private void HandleDeath()
	{
	}

	private void HandleDeathEffect()
	{
	}

	private void DestroyEnemy()
	{
	}

	private void OnDestroy()
	{
	}

	public void ResetForPool()
	{
	}

	private void AttackPlayer()
	{
	}

	public void TargetPlayerTrailEffect()
	{
	}

	private void AttackAnimation()
	{
	}

	private void WalkAnimation(bool activator)
	{
	}

	private void RunAnimation(bool activator)
	{
	}

	private void DeathAnimation()
	{
	}

	private CrowEffect ResolveCrowEffect()
	{
		return null;
	}

	public void CrowEffectClear()
	{
	}

	public void CrowTrailEffect()
	{
	}

	private void SpawnTrailEffect(Transform parent, ParticleSystem prefab)
	{
	}

	public void OnAwakeCrowTrailEffect()
	{
	}

	private static void SpawnAwakeEffect(Transform parent, ParticleSystem prefab)
	{
	}

	public void ProcessPlayerSkillHoldBroadcastResponseMessage(F2C_SkillHoldBroadcast message)
	{
	}

	public bool ReceiveSkillFact(SNativeID skillID, ushort level, uint select, ushort casterCrow, uint casterID, float age = -1f)
	{
		return false;
	}
}
