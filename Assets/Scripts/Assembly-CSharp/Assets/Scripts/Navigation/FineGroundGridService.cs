using System.Collections.Generic;

namespace Assets.Scripts.Navigation
{
	public static class FineGroundGridService
	{
		public static bool UseGrid;

		public static bool LogStepDeltas;

		private const int CacheCapacity = 10;

		private static readonly Dictionary<string, FineGroundGrid> _cache;

		private static readonly List<string> _lru;

		public static FineGroundGrid Current { get; private set; }

		public static bool HasGrid => false;

		public static bool Active => false;

		public static void Clear()
		{
		}

		public static void ClearCache()
		{
		}

		public static bool Load(string mapName)
		{
			return false;
		}

		private static void CacheGrid(string mapName, FineGroundGrid grid)
		{
		}

		private static void Touch(string mapName)
		{
		}

		private static byte[] ReadBytes(string mapName)
		{
			return null;
		}
	}
}
