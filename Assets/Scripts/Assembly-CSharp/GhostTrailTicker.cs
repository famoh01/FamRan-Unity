using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

internal class GhostTrailTicker : MonoBehaviour
{
	private static int s_nextId;

	private int id;

	private SkillGhostTrail host;

	private GameObject targetPlayer;

	private int remaining;

	private int total;

	private float interval;

	private float timer;

	private static readonly Dictionary<GameObject, GhostTrailTicker> s_liveByPlayer;

	[Conditional("GHOST_TRAIL_DEBUG")]
	internal static void GTLog(string msg)
	{
	}

	public void Launch(SkillGhostTrail owner, GameObject player, int count, float spawnInterval)
	{
	}

	private void OnDestroy()
	{
	}

	private void Update()
	{
	}
}
