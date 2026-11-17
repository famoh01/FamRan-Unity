using System;

namespace RAN.Mobile.GameModels.Item
{
	[Serializable]
	public class ItemFlagsObject
	{
		public bool sell;

		public bool trade;

		public bool drop;

		public bool costume;

		public bool expiry;

		public bool consumable;

		public bool garbage;

		public bool eventItem;

		public bool allChannel;

		public bool itemTransfer;
	}
}
