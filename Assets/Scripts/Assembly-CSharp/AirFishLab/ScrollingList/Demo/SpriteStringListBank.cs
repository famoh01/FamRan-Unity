using UnityEngine;

namespace AirFishLab.ScrollingList.Demo
{
	public class SpriteStringListBank : BaseListBank
	{
		[SerializeField]
		private SpriteStringData[] _datas;

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
