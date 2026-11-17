using Core.Enumerators;
using UnityEngine;

namespace Assets.Scripts
{
	public struct TargetInfoEx
	{
		public CrowType CrowType;

		public ushort Id;

		public Vector3 MyPosition;

		public Vector3 Position;

		public float Range;

		public TargetInfoEx(CrowType crowType, ushort id)
		{
			CrowType = CrowType.CROWPC;
			Id = 0;
			MyPosition = default;
			Position = default;
			Range = 0f;
		}

		public TargetInfoEx(CrowType crowType, ushort id, Vector3 position)
		{
			CrowType = CrowType.CROWPC;
			Id = 0;
			MyPosition = default;
			Position = default;
			Range = 0f;
		}

		public void Reset()
		{
		}

		public bool IsEqual(TargetInfoEx targetInfo)
		{
			return false;
		}

		public bool IsNotEqual(TargetInfoEx targetInfo)
		{
			return false;
		}
	}
}
