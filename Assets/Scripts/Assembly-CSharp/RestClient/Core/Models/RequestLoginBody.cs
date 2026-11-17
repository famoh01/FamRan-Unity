using RAN.Mobile.Enumerators;

namespace RestClient.Core.Models
{
	public class RequestLoginBody
	{
		public string username;

		public string password;

		public ServerEnum server;

		public ChannelEnum channel;
	}
}
