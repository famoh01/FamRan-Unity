using System;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class VendItem
	{
		public int[] inventoryIndex;

		public int vendIndex;

		public SNativeID itemId;

		public int unitCount;

		public long unitPrice;

		public bool sold;
	}
}
