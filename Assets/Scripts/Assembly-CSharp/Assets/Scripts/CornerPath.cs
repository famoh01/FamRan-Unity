using UnityEngine;

namespace Assets.Scripts
{
	public sealed class CornerPath
	{
		public Vector3[] Corners;

		public int Count;

		public Vector3 this[int i]
		{
			get
			{
				return default;
			}
			set
			{
			}
		}

		public CornerPath(int capacity = 64)
		{
		}

		public void Clear()
		{
		}

		public void EnsureCapacity(int required)
		{
		}
	}
}
