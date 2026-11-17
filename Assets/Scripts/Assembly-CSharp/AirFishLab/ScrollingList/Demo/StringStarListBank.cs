using UnityEngine;

namespace AirFishLab.ScrollingList.Demo
{
	public class StringStarListBank : BaseListBank
	{
		[SerializeField]
		private StringStarData[] _datas;

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
