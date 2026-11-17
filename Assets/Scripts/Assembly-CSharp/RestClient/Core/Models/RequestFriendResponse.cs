using System.Collections.Generic;

namespace RestClient.Core.Models
{
	public class RequestFriendResponse : BaseRequestResponse
	{
		public List<RequestFriendDataResponse> data { get; set; }
	}
}
