using UnityEngine;

public static class NavMeshSnap
{
	private static int s_walkableMask;

	private static bool s_walkableMaskResolved;

	private static int WalkableMask => 0;

	public static bool TrySnapForSpawn(Vector3 pos, out Vector3 snapped, float radius = 10f)
	{
		snapped = default;
		return false;
	}

	private static bool IsFinite(Vector3 v)
	{
		return false;
	}
}
