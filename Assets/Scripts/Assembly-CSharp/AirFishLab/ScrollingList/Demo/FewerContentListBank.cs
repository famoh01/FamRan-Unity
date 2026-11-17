namespace AirFishLab.ScrollingList.Demo
{
	public class FewerContentListBank : BaseListBank
	{
		private int[] contents;

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
