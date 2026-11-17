using RAN.Mobile.Enumerators;

namespace TrinityRift.UI.ServerSelect
{
	public static class ServerSelectLabels
	{
		public static string Server(ServerEnum server)
		{
			return server.ToString();
		}

		public static string Status(ChannelStatusEnum status)
		{
			return status.ToString();
		}

		public static string Channel(int displayIndex, bool pk)
		{
			return displayIndex + " Channel" + (pk ? " [PK]" : string.Empty);
		}
	}
}
