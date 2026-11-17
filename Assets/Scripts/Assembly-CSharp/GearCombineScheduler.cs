using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(200)]
public sealed class GearCombineScheduler : MonoBehaviour
{
	public static float FrameBudgetMs;

	public static float BudgetPerPendingMs;

	public static float MaxExtraBudgetMs;

	public static float LoaderFrameBudgetMs;

	public static float SlowFrameSeconds;

	public static int MemoryTickFrames;

	public static float MinUnmergeDist;

	public static float UnmergeMargin;

	public static int ReadmitPerTick;

	public static int MaxTicksPerFrame;

	public static int MaxStartsPerFrame;

	public static int MaxInFlight;

	public static float MaxMergeDistanceSqr;

	public static bool Enabled;

	private static GearCombineScheduler s_instance;

	private static readonly List<PlayerEquipmentClient> s_inFlight;

	private static readonly HashSet<PlayerEquipmentClient> s_inFlightSet;

	private static readonly List<PlayerEquipmentClient> s_pendingSetup;

	private static readonly HashSet<PlayerEquipmentClient> s_pendingSetupSet;

	private static readonly List<PlayerEquipmentClient> s_retry;

	private static readonly List<PlayerEquipmentClient> s_waiting;

	private static Transform s_camera;

	private static readonly List<PlayerEquipmentClient> s_merged;

	private static readonly HashSet<PlayerEquipmentClient> s_mergedSet;

	private static readonly List<PlayerEquipmentClient> s_memoryDeferred;

	private static readonly HashSet<PlayerEquipmentClient> s_memoryDeferredSet;

	public static void Request(PlayerEquipmentClient owner)
	{
	}

	public static void Cancel(PlayerEquipmentClient owner)
	{
	}

	public static void OnMerged(PlayerEquipmentClient owner)
	{
	}

	public static void OnCombinedReleased(PlayerEquipmentClient owner)
	{
	}

	public static void MarkMemoryDeferred(PlayerEquipmentClient owner)
	{
	}

	private static void EnsureInstance()
	{
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void ResetStatics()
	{
	}

	private void LateUpdate()
	{
	}

	private static void MemoryTick(in Vector3 eye)
	{
	}

	private static void NearestOf(List<PlayerEquipmentClient> list, HashSet<PlayerEquipmentClient> set, in Vector3 eye, ref float bestSqr, ref PlayerEquipmentClient best)
	{
	}

	private static int PickNearest(List<PlayerEquipmentClient> list, HashSet<PlayerEquipmentClient> set, Vector3 eye)
	{
		return 0;
	}
}
