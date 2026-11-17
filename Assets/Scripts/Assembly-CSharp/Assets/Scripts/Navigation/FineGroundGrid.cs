using System.Collections;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Navigation
{
	public sealed class FineGroundGrid
	{
		public const byte Version = 2;

		private const uint Magic = 1313296964u;

		private const uint ObfKey = 2654435761u;

		public readonly int Width;

		public readonly int Height;

		public readonly float CellSize;

		public readonly float InvCellSize;

		public readonly float OriginX;

		public readonly float OriginZ;

		private readonly float[] _groundY;

		private readonly BitArray _walkable;

		private const float MaxWalkSlope = 1f;

		private static int _lastDeltaLogFrame;

		public bool IsValid => false;

		public FineGroundGrid(int width, int height, float cellSize, float originX, float originZ, float[] groundY, BitArray walkable)
		{
		}

		private bool InBounds(int cx, int cz)
		{
			return false;
		}

		private int CellId(int cx, int cz)
		{
			return 0;
		}

		public bool IsWalkableWorld(float x, float z)
		{
			return false;
		}

		public bool SampleGroundY(float x, float z, out float y)
		{
			y = default;
			return false;
		}

		public Vector3 ClampToWalkable(Vector3 from, Vector3 to)
		{
			return default;
		}

		private static void LogStepDelta(float maxDelta, float stepLen, float maxAllowed, bool blocked, float x, float z)
		{
		}

		public Vector3 ClampToWalkableSlide(Vector3 from, Vector3 to)
		{
			return default;
		}

		private static float Moved(Vector3 a, Vector3 b)
		{
			return 0f;
		}

		private static float Dist2XZ(Vector3 a, Vector3 b)
		{
			return 0f;
		}

		private bool TryY(int cx, int cz, out float y)
		{
			y = default;
			return false;
		}

		public void Save(BinaryWriter bw)
		{
		}

		public static FineGroundGrid Load(BinaryReader br)
		{
			return null;
		}

		private static void Scramble(byte[] data, int w, int h)
		{
		}

		private static uint Fnv1a(byte[] data)
		{
			return 0u;
		}
	}
}
