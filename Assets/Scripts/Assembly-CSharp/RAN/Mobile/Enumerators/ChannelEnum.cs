using System.Runtime.Serialization;

namespace RAN.Mobile.Enumerators
{
	public enum ChannelEnum
	{
		[EnumMember(Value = "0 Channel")]
		Channel0 = 0,
		[EnumMember(Value = "1 Channel")]
		Channel1 = 1,
		[EnumMember(Value = "2 Channel")]
		Channel2 = 2,
		[EnumMember(Value = "No Channel")]
		ChannelNone = 3
	}
}
