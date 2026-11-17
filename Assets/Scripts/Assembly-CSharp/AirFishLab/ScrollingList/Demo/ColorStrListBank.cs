using UnityEngine;

namespace AirFishLab.ScrollingList.Demo
{
	public class ColorStrListBank : BaseListBank
	{
		[SerializeField]
		private ColorString[] _contents;

		public override object GetListContent(int index)
		{
			return null;
		}

		public override int GetListLength()
		{
			return 0;
		}
	}
}
