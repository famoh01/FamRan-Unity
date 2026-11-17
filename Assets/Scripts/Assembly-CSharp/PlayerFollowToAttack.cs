using System;
using Assets.Scripts.Enums;
using Core.Enumerators;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerFollowToAttack : MonoBehaviour
{
	public static PlayerFollowToAttack Instance;

	public bool toMove;

	public GameObject enemy;

	public CrowType crowType;

	public GameObject enemyEffectsMb;

	public GameObject enemyEffectsFL;

	private Button basicAttackButton;

	public RectTransform cancelArea;

	[Header("Attack Targeting")]
	public bool isTargetingEnabled;

	public bool isManualTargeting;

	[Range(4f, 20f)]
	public float manualSelectorRadius;

	[NonSerialized]
	public GameObject manualLatchTarget;

	private bool isIndicatorInEnemyAtk;

	public bool isOverCancelArea;

	public float indicatorSensitivity;

	public float manualDragSensitivity;

	private Vector3 initialClickPosition;

	private float maxDragDistance;

	public GameObject actionIndicatorPrefab;

	private GameObject attackIndicator;

	private int attackButtonTouchId;

	[Header("Enemy Detection")]
	public float detectionRange;

	public GameObject closestEnemy;

	public float manualEngageRange;

	[Header("Auto-Snap Targeting")]
	public float autoSnapSearchRadius;

	[Header("Manual Targeting — Input Mapping")]
	[Range(0f, 0.15f)]
	public float manualEdgeMarginFraction;

	[Range(0.04f, 0.3f)]
	public float manualThrowFloorFraction;

	[Range(1f, 5f)]
	public float manualMaxThrowMultiplier;

	[Range(1f, 3f)]
	public float manualThrowRoomAllowance;

	[Range(3f, 40f)]
	public float manualAimSmoothing;

	[Header("Manual Targeting — Edge Hold")]
	public bool manualEdgeHoldEnabled;

	[Range(0f, 0.12f)]
	public float manualEdgeHoldMarginFraction;

	[Range(0.05f, 1.5f)]
	public float manualEdgeHoldRampTime;

	[Header("Manual Targeting — Magnet")]
	public bool manualMagnetToTarget;

	[Range(5f, 60f)]
	public float manualMagnetSnapSpeed;

	[Header("Manual Targeting — Cone Assist")]
	public bool manualConeAssistOnEmptyCircle;

	[Range(5f, 60f)]
	public float manualConeHalfAngle;

	[Range(0f, 6f)]
	public float manualConeAngleWeight;

	[NonSerialized]
	private bool _manualDragActive;

	[NonSerialized]
	private Transform _manualSelectorT;

	[NonSerialized]
	private Vector2 _manualDeflection;

	[NonSerialized]
	private float _manualMaxRange;

	[NonSerialized]
	private bool _manualExcludeFriendlyFire;

	[NonSerialized]
	private int _manualFollowedFrame;

	[NonSerialized]
	private float _manualThrowPx;

	[NonSerialized]
	private Vector2 _manualFingerScreen;

	[NonSerialized]
	private float _edgeHold01;

	[NonSerialized]
	private Vector2 _manualSmoothedDefl;

	[NonSerialized]
	private bool _manualConeAllowed;

	[NonSerialized]
	private GroundCircleRenderer _manualCircle;

	[NonSerialized]
	private bool _manualTargetsUnit;

	[NonSerialized]
	private GameObject _manualStickyPick;

	[NonSerialized]
	private bool _manualLastPickAssisted;

	[NonSerialized]
	private bool _manualMagnetActive;

	[NonSerialized]
	private Vector3 _manualMagnetPos;

	private PlayerEnemyClicker _enemyClicker;

	private PlayerStatistics _statistics;

	private static readonly Vector3 NoPathTarget;

	private GameObject _lastUnitPointerTarget;

	private Vector3 _lastPathedTarget;

	private const float MinRepathDistanceSqr = 0.25f;

	private const float AttackVirtualStickFraction = 0.18f;

	private static readonly Color ManualSelectorColor;

	private static readonly Color SkillRangeRingColor;

	public float ManualThrowPx => 0f;

	private void Awake()
	{
	}

	public bool IsOverCancelArea(Vector2 eventData)
	{
		return false;
	}

	private void Update()
	{
	}

	private void GetTargetingPrefs()
	{
	}

	public float GetCharacterTargetingRange()
	{
		return 0f;
	}

	public Vector3 ComputeManualGroundFromDeflection(Vector2 deflection, Vector3 playerPos, float maxRange)
	{
		return default;
	}

	public float ManualThrowScale()
	{
		return 0f;
	}

	public void ResolveManualThrowBudget(Vector2 originScreen, float referenceRadiusPx)
	{
	}

	private Vector2 ApplyEdgeHold(Vector2 deflection)
	{
		return default;
	}

	private Vector2 SmoothAim(Vector2 target)
	{
		return default;
	}

	private bool IsFingerPressingEdge(Vector2 deflection)
	{
		return false;
	}

	public void SetupManualIndicatorVisual(GameObject indicator)
	{
	}

	public void SetupSkillRangeRing(GameObject indicator, float radius, Transform follow)
	{
	}

	private static GroundCircleRenderer GetOrAddGroundCircle(GameObject host)
	{
		return null;
	}

	public GameObject ManualSelect(Vector3 position, bool excludeFriendlyFire)
	{
		return null;
	}

	public void BeginManualDrag(Transform selectorT, float maxRange, bool excludeFriendlyFire, bool targetsUnit)
	{
	}

	public void SetManualDragDeflection(Vector2 deflection, float maxRange, Vector2 fingerScreenPos)
	{
	}

	public void EndManualDrag()
	{
	}

	public void ApplyManualDragFollow()
	{
	}

	private Vector3 ApplyTargetMagnet(Vector3 aimPos, GameObject picked)
	{
		return default;
	}

	private void UpdateManualDragFollow()
	{
	}

	public void ResetManualLatch()
	{
	}

	public GameObject ResolveManualCommitTarget()
	{
		return null;
	}

	public void DisableFollow()
	{
	}

	private bool MaybeRepath(Vector3 target)
	{
		return false;
	}

	public void SetEnemy(CrowType crowType, GameObject gameObject)
	{
	}

	public ushort GetNpcId()
	{
		return 0;
	}

	public PlayerAttackSkillActionEnum OnButtonPress()
	{
		return PlayerAttackSkillActionEnum.OK;
	}

	public PlayerAttackSkillActionEnum DoAttackAction()
	{
		return PlayerAttackSkillActionEnum.OK;
	}

	public void OnAtkDragTargetStart(PointerEventData eventData)
	{
	}

	public void OnAtkDragTargetEnd(PointerEventData eventData)
	{
	}

	public void OnAtkTargetDrag(PointerEventData eventData)
	{
	}

	public void FocusAndEngageNearestTower()
	{
	}

	public void CancelAttack()
	{
	}

	private Touch? GetTouchById(int touchId)
	{
		return null;
	}

	private void FindClosestEnemyToPosition(Vector3 position)
	{
	}

	private void OnDrawGizmosSelected()
	{
	}
}
