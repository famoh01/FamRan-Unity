using System;
using RAN.Mobile.Enumerators;

[Serializable]
public class ClientServerAddress
{
	public ServerEnum serverName;

	public byte groupId;

	public byte agentState;

	public string agentIpAddress;

	public ushort agentPort;

	public ushort totalPlayers;

	public ClientServerChannel[] channels;
}
