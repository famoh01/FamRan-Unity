using UnityEngine;

namespace XftWeapon
{
	internal sealed class TrailMeshJanitor : MonoBehaviour
	{
		private const int SweepIntervalFrames = 120;

		private static TrailMeshJanitor s_instance;

		private int _frameCounter;

		internal static void EnsureRunning()
		{
		}

		private void Update()
		{
		}
	}
}
