using System;

namespace AirFishLab.ScrollingList
{
	public class ListContentManager
	{
		private readonly CircularScrollingListSetting _listSetting;

		private readonly BaseListBank _listBank;

		private readonly int _numOfBoxes;

		private readonly int _idFactor;

		private readonly Func<int, int> _idHandler;

		public int ContentCount => 0;

		public ListContentManager(CircularScrollingListSetting setting, BaseListBank listBank, int numOfBoxes)
		{
		}

		public int GetInitialContentID(int listBoxID)
		{
			return 0;
		}

		public int GetContentID(int boxIDOffset, int centerContentID)
		{
			return 0;
		}

		public object GetListContent(int contentID)
		{
			return null;
		}

		public int GetIDFromNextBox(int nextBoxContentID)
		{
			return 0;
		}

		public int GetIDFromLastBox(int lastBoxContentID)
		{
			return 0;
		}

		public bool IsIDValid(int id)
		{
			return false;
		}

		public int GetShortestDiff(int fromID, int toID)
		{
			return 0;
		}
	}
}
