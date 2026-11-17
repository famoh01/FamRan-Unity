using System;
using RAN.Mobile.Enumerators;

[Serializable]
public class ClientServerChannel
{
	public byte channelIndex;

	public ushort playersInChannel;

	public ushort maxChannelUser;

	public ChannelEnum channel;

	public ChannelStatusEnum channelStatus;

	public bool pk;
}
