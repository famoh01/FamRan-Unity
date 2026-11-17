using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Assets.Scripts;
using Assets.Scripts.Character_Scripts;
using Assets.Scripts.Interfaces;
using Core.Enumerators;
using Models.Library.Shared.DTO;
using Models.Library.Shared.Game;
using Models.Network.Shared.Response;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour, IActor
{
	[CompilerGenerated]
	private sealed class _003CCheckAnimationCompleted_003Ed__118 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PlayerController _003C_003E4__this;

		public string toPlayAnim;

		public string[] endAnim;

		public Action Oncomplete;

		private int _003CstartStateHash_003E5__2;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CCheckAnimationCompleted_003Ed__118(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	public static PlayerController Instance;

	public Animator Animator;

	public string AnimTriggerName;

	public AnimationMainTypeEnum AniMainTypeSkill;

	public AnimationSubTypeEnum AniSubTypeSkill;

	public AnimationSubTypeEnum AniSubType;

	public CGameCharLogic CharLogic;

	[Header("Initialization")]
	private FixedJoystick _joystick;

	private Transform _camPivot;

	private GameActor actorMove;

	private int _walkableMask;

	private NavMeshPath _navPath;

	private Vector3 _manualMoveTarget;

	private bool _manualMoveActive;

	private const float ManualMoveArriveDistSqr = 4f;

	private const float ManualMoveStuckEpsilonSqr = 0.25f;

	[Header("States")]
	public ushort gaeaId;

	public ActionEnums actionEnum;

	public uint dwActState;

	public byte WaitCount;

	public SNativeID ActiveSkillID;

	public Vector3 vMaxOrg;

	public Vector3 vMinOrg;

	public Vector3 vMax;

	public Vector3 vMin;

	[Header("Target")]
	public TargetInfoEx TargetInfo;

	public Vector3 ManualTargetPosition;

	public ushort TargetNum;

	public float UsedTotalRange;

	public float UsedTotalAngle;

	public TargetInfoID[] TargetInfoIDs;

	public Vector3 vTargetPosition;

	public ushort? clientSkillHolderId;

	[Header("Float / Int")]
	public float fAttackSpeed;

	public float _attackTimer;

	public float _idleTime;

	public float _timer;

	public float _timerNav;

	public ushort _animationSubSelect;

	public ushort _animationSubCount;

	public ushort _animationSubGesture;

	public float _lastUpdateTime;

	private const float joystickDeadZone = 0.1f;

	private const float positionUpdateInterval = 0.2f;

	private float timeSinceLastPositionUpdate;

	private float _lastSentTargetX;

	private float _lastSentTargetZ;

	private float _lastSentClientX;

	private float _lastSentClientZ;

	private uint _lastSentActState;

	private const float GoToSendEpsilonSq = 0.01f;

	private const float GoToKeepAliveInterval = 0.25f;

	private readonly RaycastHit[] _groundHits;

	private const float GroundSnapRayUp = 30f;

	private const float GroundSnapRayDist = 90f;

	private const float GroundSnapStepUp = 6f;

	[Header("Booleans")]
	public bool IsNavAgent;

	public bool IsJoyStick;

	private bool hasPlayerAwaken;

	private bool _joystickHeldThroughCastRaw;

	private InventoryData _inventoryData;

	private PlayerEquipment _playerEquipment;

	private PlayerFollowToAttack _followToAttack;

	private SkillData _skillData;

	private string _animIdleWithWeapon;

	private string _animRunWithWeapon;

	private string _animRunNoWeapon;

	private string _animWalkWithWeapon;

	private string _animWalkNoWeapon;

	private const float JoystickLookAhead = 2f;

	private const float JoyOnMeshSampleRadius = 2f;

	private const float JoyOnMeshVerticalTol = 1.5f;

	private const float JoyOffMeshRecoverRadius = 12f;

	private const float JoyOffMeshRecoverVerticalTol = 2f;

	private const float JoyOffMeshMaxLookAhead = 6f;

	public int WalkableMask => 0;

	private bool _joystickHeldThroughCast
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public bool SkillCastPending { get; set; }

	public ref readonly PlayerConstantDTO PlayerConst
	{
		get
		{
			throw null;
		}
	}

	public void ArmHeldJoystickSuppression()
	{
	}

	public void ConfirmHeldJoystickSuppression()
	{
	}

	uint IActor.GetId()
	{
		return 0u;
	}

	public CrowType GetCrow()
	{
		return CrowType.CROWPC;
	}

	public ushort GetBodyRadius()
	{
		return 0;
	}

	public Vector3 GetPosition()
	{
		return default;
	}

	public bool IsAction(ActionEnums actionCurr)
	{
		return false;
	}

	public bool IsState(ActStateEnums dwState)
	{
		return false;
	}

	public void SetState(ActStateEnums state)
	{
	}

	public void ReSetSTATE(ActStateEnums state)
	{
	}

	public bool IsCoolTime(in SNativeID nid)
	{
		return false;
	}

	public long GetMaxCoolTime(in SNativeID nid)
	{
		return 0L;
	}

	public float GetCoolTimeFraction(in SNativeID nid)
	{
		return 0f;
	}

	public ushort GetId()
	{
		return 0;
	}

	public bool IsDie()
	{
		return false;
	}

	public bool IsValidBody()
	{
		return false;
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	private void EnableCapsuleCollider()
	{
	}

	public void OnUpdateCameraMethods(bool useFixedUpdate = true)
	{
	}

	public void DoActWait()
	{
	}

	public void SnapToPosition(Vector3 pos)
	{
	}

	public void InitializeData(GameCharData2 charData)
	{
	}

	private void OnActorArrived()
	{
	}

	public void CheckAnimationSub()
	{
	}

	private void ProcessJoystickInput(float joyH, float joyV)
	{
	}

	public float GetMoveVelo()
	{
		return 0f;
	}

	public void FrameMove(float dt)
	{
	}

	private void ForceIdleAnimation(bool hasWeapon)
	{
	}

	[IteratorStateMachine(typeof(_003CCheckAnimationCompleted_003Ed__118))]
	public IEnumerator CheckAnimationCompleted(string toPlayAnim, Action Oncomplete, bool skill = false, params string[] endAnim)
	{
		return null;
	}

	private void UpdateAnimation(float ellapsedTime = 0f)
	{
	}

	public void SaveHotKey()
	{
	}

	public void ToggleRun()
	{
	}

	public void ToggleMount()
	{
	}

	public void SetMountVisual(bool on)
	{
	}

	private void ClearNonEnemyBuffVfxForMount()
	{
	}

	public void TogglePeaceMode()
	{
	}

	private void SetPeaceModeAnimation()
	{
	}

	private void SendStopPosition()
	{
	}

	public void TurnAction(ActionEnums toAction)
	{
	}

	public void ResetAnimation()
	{
	}

	public void ToDie()
	{
	}

	private void StartSkillProcess()
	{
	}

	private void StartAttackProcess()
	{
	}

	private void MountAnimationSetBool(string action, bool value)
	{
	}

	private void AnimationSetBool(string name, bool value)
	{
	}

	public void AnimatorChecker()
	{
	}

	public void ResourcesLoader(string path)
	{
	}

	public void HaltLocalMovement()
	{
	}

	public void ClearLocalMovementIntentsForModal()
	{
	}

	public bool OnManualMovement(Vector3 targetPosition, bool allowFarContinuation = false)
	{
		return false;
	}

	private bool TryContinueManualMove()
	{
		return false;
	}

	public void SetTargetInfo(CrowType crowType, Vector3 position, int id)
	{
	}

	public void UpdateSkillEffect(ref SkillFact[] skillFacts, ref StateBlow[] stateBlows)
	{
	}

	public void ProcessPlayerSkillHoldBroadcastResponseMessage(F2C_SkillHoldBroadcast message)
	{
	}

	public bool ReceiveSkillFact(SNativeID skillID, ushort level, uint select, ushort casterCrow, uint casterID, float age = -1f)
	{
		return false;
	}

	public void ProcessSystemBuffResponseMessage(F2C_SystemBuff message)
	{
	}

	public void ProcessLandEffectResponseMessage(F2C_LandEffect message)
	{
	}

	public bool ReceiveSystemBuff(SNativeID skillID, ushort level, byte slot, uint casterID)
	{
		return false;
	}

	public void RemoveSystemBuff(byte slot)
	{
	}

	public bool RequireInventoryDrug(InventorySlot slot)
	{
		return false;
	}

	public bool RequireGesture(ushort emoticon, bool isCommand = false)
	{
		return false;
	}

	public void ResetPlayer()
	{
	}

	public void CancelSkill(SNativeID skillId, bool bCancelSkillEff = false)
	{
	}
}
