using System;
using RAN.Mobile.Enumerators;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class ServerChannel
	{
		public ServerEnum server { get; set; }

		public ChannelEnum channel { get; set; }

		public ChannelStatusEnum channelStatus { get; set; }

		public string[] charIds { get; set; }

		public int port { get; set; }

		public bool pk { get; set; }
	}
}
