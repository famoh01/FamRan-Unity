using System.Runtime.CompilerServices;

namespace Assets.Scripts
{
	public class ClientCellGrid
	{
		public readonly int Width;

		public readonly int Height;

		public readonly float CellSize;

		public readonly float InvCellSize;

		public readonly float OriginX;

		public readonly float OriginZ;

		public bool IsValid => false;

		public ClientCellGrid(int width, int height, float cellSize, float originX, float originZ)
		{
		}

		public static ClientCellGrid FromMapData(float cellSize, float minX, float maxX, float minZ, float maxZ)
		{
			return null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WorldToCell(float x, float z, out int cx, out int cz)
		{
			cx = default;
			cz = default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int CellId(int cx, int cz)
		{
			return 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int CellIdFromWorld(float x, float z)
		{
			return 0;
		}
	}
}
