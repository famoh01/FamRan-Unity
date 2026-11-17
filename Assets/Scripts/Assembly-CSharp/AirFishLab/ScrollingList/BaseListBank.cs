using UnityEngine;

namespace AirFishLab.ScrollingList
{
	public abstract class BaseListBank : MonoBehaviour
	{
		public abstract object GetListContent(int index);

		public abstract int GetListLength();
	}
}
