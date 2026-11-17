using System.Collections.Generic;
using UnityEngine;

public sealed class ClientNavPath
{
	public int PathNodePoolSize;

	public int MaxIterations;

	public Vector3 MapExtents;

	public float YDeviationMax;

	public int MaxMergeLookahead;

	public bool VerboseLog;

	private readonly List<Vector3> _mids;

	private Vector3[] _out;

	private int _outCount;

	public static ClientNavPath Instance { get; }

	private ClientNavPath()
	{
	}

	public bool TryFindPath(Vector3 start, Vector3 goal, int areaMask, out Vector3[] corners, out int count)
	{
		corners = null;
		count = default;
		return false;
	}

	private void SmoothOnNavMesh(List<Vector3> pts, int areaMask)
	{
	}

	private bool ClearSegment(List<Vector3> pts, int i, int j, int areaMask)
	{
		return false;
	}

	private void Emit(Vector3 p)
	{
	}

	private void EmitUnique(Vector3 p)
	{
	}

	private static bool VEqual(Vector3 a, Vector3 b)
	{
		return false;
	}

	private static bool IsFinite(Vector3 v)
	{
		return false;
	}
}
