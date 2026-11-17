using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Core.Enumerators;
using Models.Library.Shared.DTO;
using Models.Library.Shared.Game;
using Models.Network.Shared.Response;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerClient : MonoBehaviour, IActor
{
	[CompilerGenerated]
	private sealed class _003CCheckAnimationCompleted_003Ed__235 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PlayerControllerClient _003C_003E4__this;

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
		public _003CCheckAnimationCompleted_003Ed__235(int _003C_003E1__state)
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

	public Animator Animator;

	public AnimationType ANISUBTYPECLIENT;

	public uint dwANISUBGESTURE;

	public TargetInfoEx TargetID;

	public GameActor Actor;

	public int NavMeshSyncHandle;

	private GameObject _hpBarContainer;

	private Image _hpNameBackgroundImage;

	private Slider _hpBar;

	private Image _hpFillImage;

	private bool _hpRefsResolved;

	[Header("Character Data")]
	public ActionEnums Action;

	public int ActState;

	public AnimationMainTypeEnum AniMainTypeSkill;

	public AnimationSubTypeEnum AniSubTypeSkill;

	public AnimationSubTypeEnum AnimSubType;

	public uint RevData;

	public SDROP_CHAR CharData;

	public CharacterIndexEnum CHARINDEX;

	public Vector3 vMaxOrg;

	public Vector3 vMinOrg;

	public Vector3 vMax;

	public Vector3 vMin;

	public PlayerConstantDTO[] playerConst;

	public SkillFact[] SkillFacts;

	public SkillFact[] SystemBuffs;

	public StateBlow[] StateBlows;

	public uint TransfomrSkill;

	public float StateMove;

	public float StateDelay;

	public bool IsStatePant;

	public bool IsStateStun;

	public bool IsInvisible;

	public float fSKILL_MOVE;

	public float fATTVELO;

	public float ItemMoveR;

	public float ItemAttackVelocityR;

	public float ItemMove;

	public float ItemAttackVelocity;

	public string AnimTriggerName;

	private Coroutine _animCompleteRoutine;

	public ushort TargetNum;

	public Vector3 vTargetPosition;

	public TargetInfoID[] TargetInfoIDs;

	public ushort ActiveSkillLevel;

	public SNativeID ActiveSkillID;

	public int[] ActivePlayerIds;

	public ushort ActiveClientId;

	public float _attackTimer;

	public float _lastUpdateTime;

	private Vector3 _lastPosition;

	private float _lastGoToTime;

	private Vector3 _snapPrevPos;

	private Vector3 _snapCurPos;

	private Vector3 _snapTarget;

	private float _snapTime;

	private float _snapInterval;

	private Vector3 _snapVel;

	private Vector3 _smoothVel;

	private float _swingStartTime;

	private const float SwingMoveGuardSeconds = 0.12f;

	private bool _hasSnapshot;

	private Canvas[] _nameplateCanvases;

	private Billboard[] _nameplateBillboards;

	private IGNScript _nameplateIgn;

	private WorldSpaceUiConstantSize[] _nameplateConstSize;

	private NameDisplayPosController[] _nameplatePosCtrl;

	private IGNWithVendScript _vendNameplate;

	private bool _nameplateResolved;

	private bool _nameplateShown;

	private bool _vendNameplateInRange;

	private const float VendNameplateShowDistance = 160f;

	private const float VendNameplateHideDistance = 180f;

	private const float VendNameplateShowDistanceSqr = 25600f;

	private const float VendNameplateHideDistanceSqr = 32400f;

	private readonly List<Renderer> _meshCullRenderers;

	private bool _meshShown;

	public static bool SmoothReveal;

	private const float FadeInDuration = 0.35f;

	private const float RevealHoldCap = 0.75f;

	private const float RevealHoldCapFar = 2.5f;

	private static readonly int s_idGhostAlpha;

	private static readonly int s_idSrcBlend;

	private static readonly int s_idDstBlend;

	private static MaterialPropertyBlock s_fadeBlock;

	private static readonly List<Material> s_fadeMatScratch;

	private readonly List<Renderer> _fadeRenderers;

	private static readonly List<Renderer> s_fadeRendererScratch;

	private readonly List<bool> _fadeHadBlock;

	private readonly List<Material> _fadeMaterials;

	private readonly List<ParticleSystem> _culledParticles;

	private static readonly List<ParticleSystem> s_psScratch;

	private float _fadeStart;

	private bool _revealHold;

	private float _revealHoldStart;

	private bool _revealFar;

	private int _stagger;

	private bool _vfxNear;

	private bool _peaceDirty;

	private bool _lastIdleHasWeapon;

	private bool _lastIdlePeace;

	private static readonly int s_movementSpeedHash;

	private static readonly int s_attackSpeedHash;

	private bool _aniStringsBuilt;

	private AnimationType _aniCacheKey;

	private string _aniIdleWithWeapon;

	private string _aniRunWithWeapon;

	private string _aniWalkWithWeapon;

	private string _aniAttackWithWeapon;

	private string _aniAnimIdleWithWeapon;

	private const float DefaultGoToInterval = 0.1f;

	private const float MinGoToInterval = 0.05f;

	private const float MaxGoToInterval = 0.3f;

	public const float PositionSmoothTime = 0.08f;

	public const float ExtrapolationCap = 1f;

	public const float MoveRotateRate = 15f;

	private const float SnapVelMaxFactor = 1.25f;

	private const float SnapVelSmoothing = 0.35f;

	private const float SnapDampMaxSpeedFactor = 2.5f;

	private Vector3 _netPos;

	private Vector3 _chaseVel;

	private const float HardSnapDistance = 60f;

	private const float ChaseSmoothTime = 0.16f;

	public static bool UseTargetWalk;

	private const float RemoteTeleportDistSq = 62500f;

	private const float RemoteMoveIdleTimeout = 0.8f;

	private const float RemoteAtRestEpsilonSq = 0.25f;

	private bool _remoteAtRestHint;

	public bool IsVehicle;

	public bool IsPassenger;

	private ushort _mountSkinMain;

	private ushort _mountSkinSub;

	public bool IsSafeZone;

	public ushort? clientSkillHolderId;

	private Vector3 _actionEasePos;

	private bool _actionEasing;

	private const float ActionEaseDeadbandSq = 1f;

	private PlayerEquipmentClient _cachedEquipment;

	private bool _nameplateOnScreen;

	private const int AnimGraceFrames = 2;

	private int _animForceFrame;

	private static readonly Dictionary<string, RuntimeAnimatorController> s_animatorCache;

	private static readonly string[] s_animatorPaths;

	private static readonly Dictionary<RuntimeAnimatorController, HashSet<int>> s_animParamCache;

	private static readonly HashSet<long> s_missingIdleLogged;

	public bool IsMeshShown => false;

	private int Stagger => 0;

	private bool ParticlesWanted => false;

	private float FadeElapsed => 0f;

	public float SnapStartY => 0f;

	public uint GetCharId => 0u;

	public ref readonly PlayerConstantDTO PlayerConst
	{
		get
		{
			throw null;
		}
	}

	public PlayerEquipmentClient GetEquipment => null;

	private bool RenderersHidden => false;

	private void EnsureAniStrings()
	{
	}

	public void SetMountVisual(bool on, ushort skinMain = 0, ushort skinSub = 0)
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

	public void SetActionPosition(Vector3 pos)
	{
	}

	private void TickActionEase(float dt)
	{
	}

	public void MoveToPosition(Vector3 targetPos)
	{
	}

	public CrowType GetCrow()
	{
		return CrowType.CROWPC;
	}

	public uint GetId()
	{
		return 0u;
	}

	public string GetName()
	{
		return null;
	}

	public PAData GetHP()
	{
		return default;
	}

	private void UpdateHealth()
	{
	}

	public ushort GetLevel()
	{
		return 0;
	}

	public ushort GetSchool()
	{
		return 0;
	}

	public CharacterClassEnum GetClass()
	{
		return CharacterClassEnum.GLCC_NONE;
	}

	public bool IsInSafeZone()
	{
		return false;
	}

	public float GETATTVELO()
	{
		return 0f;
	}

	public float GETMOVEVELO()
	{
		return 0f;
	}

	public float GETMOVE_ITEM()
	{
		return 0f;
	}

	public float GETATT_ITEM()
	{
		return 0f;
	}

	public float GetMoveVelocity()
	{
		return 0f;
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void FrameMove(float ellapsedTime = 0f, bool fullDetail = true, bool refreshVisual = true)
	{
	}

	private void ResolveNameplateRefs()
	{
	}

	internal void PrepareVendNameplateForSpawn(IGNWithVendScript vendNameplate)
	{
	}

	private void SetNameplateCanvasesEnabled(bool enabled)
	{
	}

	private void ApplyPlateDrawState(bool drawn)
	{
	}

	public void SetNameplateOnScreen(bool onScreen)
	{
	}

	public void SetBakedNameplateShown(bool show)
	{
	}

	public void UpdateNameplateCulling(in Vector3 viewerPos, float showDistSqr, float hideDistSqr)
	{
	}

	public void UpdateNameplateCulling(in Vector3 viewerPos, float showDistSqr, float hideDistSqr, bool onScreen)
	{
	}

	private void GatherMeshCullRenderers()
	{
	}

	private void ApplyMeshCull(bool forceOff)
	{
	}

	private void StopParticlesForCull()
	{
	}

	private void ResumeParticlesAfterCull()
	{
	}

	private void ApplyHiddenState()
	{
	}

	private void HideRenderersNow()
	{
	}

	public void NotifyEffectAttached()
	{
	}

	public void UpdateAnimatorCulling(bool onScreen)
	{
	}

	private void ForceAnimateBriefly()
	{
	}

	public void SetMeshShown(bool show)
	{
	}

	public void NotifyRenderersChanged()
	{
	}

	public void BeginSpawnReveal()
	{
	}

	private void BeginReveal()
	{
	}

	private void TickReveal()
	{
	}

	private static float FadeCurve(float x)
	{
		return 0f;
	}

	private void BeginFadeIn()
	{
	}

	private void GatherFadeTargets()
	{
	}

	private void ApplyFadeAlpha(float alpha)
	{
	}

	private void RestoreFadeMaterials()
	{
	}

	private void EndFade()
	{
	}

	private void ReleaseFadeRenderers()
	{
	}

	private void TickFade()
	{
	}

	public void UpdateMeshCulling(in Vector3 viewerPos, float showDistSqr, float hideDistSqr)
	{
	}

	public Vector3 UpdateSkillDirection(Vector3 myPosition, Vector3 myDirection, in SNativeID skillID, Vector3 targetPosition, in TargetInfoID[] targetIDs)
	{
		return default;
	}

	public void ReceiveDamage(int damage)
	{
	}

	public void ReceiveDamage(int damage, DamageTypeEnum flag)
	{
	}

	public void UpdateItem()
	{
	}

	public void Initialize(SDROP_CHAR charData)
	{
	}

	public void DISABLESKEFF(int index)
	{
	}

	public void UpdateData(float ellapsedTime, bool client)
	{
	}

	private void ClearNonEnemyBuffsForMount()
	{
	}

	public void UpdateSkillEffect(ref SkillFact[] skillFacts, ref StateBlow[] stateBlows)
	{
	}

	public bool IsAction(ActionEnums action)
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

	public bool IsValidBody()
	{
		return false;
	}

	private void UpdateAnimation()
	{
	}

	public void SetTransRotation(Vector3 targetPos)
	{
	}

	public void TurnAction(ActionEnums toAction)
	{
	}

	private void ToDie()
	{
	}

	private void StartAttackProcess()
	{
	}

	private void InterruptActionAnimation(bool forceIdle)
	{
	}

	private void StartSkillProcess()
	{
	}

	[IteratorStateMachine(typeof(_003CCheckAnimationCompleted_003Ed__235))]
	public IEnumerator CheckAnimationCompleted(string toPlayAnim, Action Oncomplete, bool skill = false, params string[] endAnim)
	{
		return null;
	}

	public void AnimatorChecker()
	{
	}

	private void SetPeaceModeAnimation()
	{
	}

	public void ResourcesLoader(string path)
	{
	}

	public static void PreloadAnimators()
	{
	}

	public void TurnSkill(string param)
	{
	}

	public static bool HasAnimParam(Animator animator, int paramHash)
	{
		return false;
	}

	public static string ResolveIdleState(Animator animator, string wanted, string fallback)
	{
		return null;
	}

	private void AnimationSetBool(string name, bool value)
	{
	}

	private void SetAnimBoolSafe(string name, bool value)
	{
	}

	private void MountAnimationSetBool(string action, bool value)
	{
	}

	public void MsgGoToPositionResponse(F2C_GoToBroadcast message)
	{
	}

	private static Vector3 ClampExtrapToTarget(Vector3 snapCurPos, Vector3 extrapXZ, Vector3 dest)
	{
		return default;
	}

	private void RecordMoveSnapshot(in Vector3 startPos)
	{
	}

	public void MsgPlayerMoveStateResponse(F2C_ActStateBroadcast message)
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

	public void ResetForPool(bool rebindAnimator = true)
	{
	}

	public void ResetPlayer()
	{
	}

	public void ExecuteResetPlayer()
	{
	}

	public void CancelSkill(SNativeID skillId)
	{
	}
}
