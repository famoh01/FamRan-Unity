using UnityEngine;
using UnityEngine.SceneManagement;

public static class AssetMemorySweeper
{
	private static AsyncOperation s_sweep;

	private const int LowMemoryKeepShellsPerGender = 20;

	private const int LowMemoryKeepMobsPerType = 4;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void Install()
	{
	}

	private static void OnSceneUnloaded(Scene _)
	{
	}

	private static void OnLowMemory()
	{
	}

	private static void Sweep()
	{
	}
}
