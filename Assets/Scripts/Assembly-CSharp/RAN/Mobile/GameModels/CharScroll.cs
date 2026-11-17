using System;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class CharScroll
	{
		public string CharId { get; set; }

		public string Name { get; set; }

		public SNativeID SNativeID { get; set; }
	}
}
