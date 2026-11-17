using Assets.Scripts;
using Core.Enumerators;
using Models.Library.Shared.Game;
using UnityEngine;
using UnityEngine.AI;

public class PetMover : MonoBehaviour
{
	private const float RunArea = 60f;

	private const float WalkArea = 40f;

	private const float StopArea = 5f;

	private const float PetRunSpeed = 38f;

	private const float PetWalkSpeed = 15f;

	private const float RotationSpeed = 8f;

	private const float IdleFunnyInterval = 4f;

	private const float IdleAnimDelay = 0.25f;

	private const float RetargetThresholdSqr = 9f;

	private const float FirstBroadcastSnapSqr = 100f;

	private const float TeleportJumpSqr = 1600f;

	private int _floatFrameCount;

	private const int FloatGraceFrames = 8;

	private const float GroundSnapEpsilon = 0.0001f;

	private float _idleTimer;

	private bool _isPlayingAction;

	private float _stationaryDuration;

	public SNativeID CrowId;

	private Animator _animator;

	private bool _animatorCached;

	private bool _isMoving;

	private bool _isOwner;

	private bool _wasRunning;

	private bool _hasPrevBroadcast;

	private Vector3 _prevBroadcastPos;

	private GameObject _nameDisplayRoot;

	private PetNameDisplay _nameDisplay;

	private readonly NameplateCuller _plateCuller;

	private ushort _ownerGaeaId;

	private bool _ownerNameResolved;

	private static readonly Color OwnPetNameColor;

	private static readonly Color RemotePetNameColor;

	private Vector3 _lastFollowTarget;

	private float _lastPetGotoSendTime;

	private bool _petGotoSendPending;

	private const float PetGotoSendInterval = 0.2f;

	private const float OwnerDistance = 12f;

	private NavMeshPath _navPath;

	private Vector3[] _cornersBuffer;

	private bool _lastAnimMoving;

	private bool _lastAnimRunning;

	public GameActor Actor { get; private set; }

	public int NavMeshSyncHandle { get; private set; }

	private Animator GetAnimator()
	{
		return null;
	}

	public void Initialize(Vector3 spawnPos, bool isOwner)
	{
	}

	public void UpdateNameplateCulling(in Vector3 viewerPos, float showDistSqr, float hideDistSqr, bool onScreen = true)
	{
	}

	public void FrameMove(float dt)
	{
	}

	private void GroundOwnPet(Vector3 actorPos)
	{
	}

	private void TickFollow(float dt)
	{
	}

	public void SetTarget(Vector3 curPos, Vector3 target, bool isRunning)
	{
	}

	public void Stop(Vector3 finalPos)
	{
	}

	private void ComputeAndSetPath(Vector3 target)
	{
	}

	private void SetAnim(bool moving, bool isRunning = true)
	{
	}

	public void PlayAction(PetActionType actionType)
	{
	}

	private void TickActionAnimation()
	{
	}

	public void CleanUp()
	{
	}

	public void SetupNamePlate(ushort ownerGaeaId, string petName)
	{
	}

	private string ResolveOwnerName()
	{
		return null;
	}

	private void TryResolveOwnerName()
	{
	}

	private void OnDestroy()
	{
	}
}
