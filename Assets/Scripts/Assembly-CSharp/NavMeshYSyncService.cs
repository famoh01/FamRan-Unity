using System;
using Assets.Scripts;
using Unity.Collections;
using UnityEngine;

public class NavMeshYSyncService : MonoBehaviour
{
	private const float SampleRadius = 10f;

	private const float FallbackRadius = 30f;

	private static int _walkableMask;

	private static bool _walkableMaskResolved;

	private const float GroundRayUp = 30f;

	private const float GroundRayDist = 90f;

	private const float GroundSnapBand = 6f;

	private static readonly RaycastHit[] _groundBuf;

	private static readonly int _groundRayMask;

	private Transform[] _transforms;

	private GameActor[] _actors;

	private Action<int, int>[] _handleUpdaters;

	private int[] _noGroundFrames;

	private int _count;

	private const int GroundGraceFrames = 8;

	private const int RayMaxHits = 4;

	private const int RayMinPerJob = 32;

	private NativeArray<RaycastCommand> _rayCmds;

	private NativeArray<RaycastHit> _rayHits;

	private int[] _cmdToEntity;

	private Vector2[] _lastSampledXZ;

	private const float MoveEpsilonSqXZ = 1E-06f;

	private const int StationaryRecheckFrames = 15;

	private uint _frameCounter;

	public static NavMeshYSyncService Instance { get; private set; }

	private static int WalkableMask => 0;

	private void Awake()
	{
	}

	public int Register(Transform t, GameActor actor, Action<int, int> handleUpdater = null)
	{
		return 0;
	}

	public void Unregister(int handle)
	{
	}

	public void RunGroundPass()
	{
	}

	public static bool TrySampleGroundY(Vector3 pos, out float y, bool allowBigDrop = true)
	{
		y = default;
		return false;
	}

	private static bool TryGroundY(Vector3 pos, out float y, bool allowBigDrop = true)
	{
		y = default;
		return false;
	}

	public void Clear()
	{
	}

	private void Grow()
	{
	}

	private void OnDestroy()
	{
	}
}
