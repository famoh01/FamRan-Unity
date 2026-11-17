using System;
using UnityEngine;

namespace TrinityRift.Helpers
{
	public class GameHelper
	{
		private static System.Random random;

		public static bool IsSpherePointCollision(Vector3 position, Vector3 center, float radius)
		{
			return false;
		}

		public static float DXGetThetaYFromDirection(Vector3 vDir, Vector3 vDirOrig)
		{
			return 0f;
		}

		public static Matrix4x4 CreateRotationMatrixY(float angle)
		{
			return default;
		}

		public static Vector3 TransformCoord(Vector3 vector, Matrix4x4 matrix)
		{
			return default;
		}

		public static float CalculateThetaY(Vector3 vDirOrig)
		{
			return 0f;
		}

		public static bool IsCollisionLineToAABB(Vector3 vStart, Vector3 vEnd, Vector3 vMax, Vector3 vMin)
		{
			return false;
		}
	}
}
