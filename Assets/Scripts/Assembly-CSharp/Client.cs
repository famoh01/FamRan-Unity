using System;
using System.Runtime.CompilerServices;
using Models.Network.Shared.Enums;
using Models.Network.Shared.Response;
using RAN.Mobile.Enumerators;
using UnityEngine;

public class Client : MonoBehaviour
{
	[Serializable]
	public class LoginData
	{
		public string loginHost;

		public int loginPort;
	}

	public ClientVersionEnum currentClientVersion;

	public ServerEnum selectedServer;

	public ChannelEnum? selectedChannel;

	public ClientServerAddress[] mainServerAddress;

	[CompilerGenerated]
	private static Action<ClientServerAddress[]> m_OnServerListUpdated;

	public string _userId;

	[Header("Environment")]
	public EnvironmentEnum environment;

	[Header("TCP Server Addresses")]
	public LoginData[] loginData;

	[HideInInspector]
	public ServerListResponse cachedServerList;

	[HideInInspector]
	public string sessionToken;

	public static Client Instance { get; private set; }

	public bool HasServerList { get; private set; }

	public string LoginServerHost => null;

	public int LoginServerPort => 0;

	public static event Action<ClientServerAddress[]> OnServerListUpdated
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void FetchServerList()
	{
	}

	private void OnServerListReceived(ServerListResponse response)
	{
	}

	public int GetSelectedChannelIndex()
	{
		return 0;
	}

	public int GetSelectedChannelMaxUsers()
	{
		return 0;
	}
}
