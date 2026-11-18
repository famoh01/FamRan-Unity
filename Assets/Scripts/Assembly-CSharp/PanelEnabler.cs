using System;
using System.Collections;
using System.Text;
using RAN.Mobile.Enumerators;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PanelEnabler : MonoBehaviour
{
	public static PanelEnabler instance;

	public GameObject loginPanel;

	public GameObject mainScreen;

	public GameObject serverSelect;

	public GameObject webLoader;

	public Transform serverPanelParent;

	public GameObject serverPanel;

	public Transform serverParent;

	public GameObject serverButton;

	public GameObject serverChannels;

	public Button exitButton;

	public TextMeshProUGUI ChannelName;

	public TextMeshProUGUI responseMsgTxt;

	private Button selectedChannelButton;

	private GameObject selectedChannelGameObject;

	public GameObject globalConfirmButton;

	[Header("Connecting Modal")]
	[Tooltip("Modal shown while a connect attempt is in flight. Falls back to webLoader if left empty.")]
	public GameObject connectingModal;

	[Tooltip("Seconds to wait for the server before the connect attempt is treated as failed.")]
	public float connectTimeoutSeconds = 10f;

	[Tooltip("Minimum time the connecting modal stays up, even if the server replies instantly. Keeps the guard raised so a fast response can't be spammed.")]
	public float minConnectingModalSeconds = 1f;

	[Header("Connecting Label (animated dots)")]
	[Tooltip("Optional label whose trailing dots animate ('Connecting', 'Connecting.', '..', '...').")]
	public TextMeshProUGUI connectingText;

	[Tooltip("Base text shown before the animated dots.")]
	public string connectingBaseText = "Connecting";

	[Tooltip("Seconds between each dot-animation step.")]
	public float dotAnimInterval = 0.4f;

	private bool _isConnecting;

	private Coroutine _connectTimeoutCo;

	private Coroutine _minModalCo;

	private Coroutine _dotAnimCo;

	private float _connectStartTime;

	[Header("Channel Text Colors")]
	public Color selectedChannelTextColor = Color.white;

	public Color unselectedChannelTextColor = new Color(0.5f, 0.5f, 0.5f);

	[Header("Server Panel Positioning")]
	[Tooltip("Drag the parent container of the server list here")]
	public RectTransform serversPanelContainer;

	[Tooltip("The X position when the panel is in the middle")]
	public float centeredXPosition;

	[Tooltip("The X position when the panel moves to the left")]
	public float shiftedXPosition;

	[Header("HTTP")]
	[Tooltip("Auth API root. Leave empty to reuse Authentication.apiBaseUrl.")]
	public string apiBaseUrl = "";

	private ClientServerAddress[] _servers;

	private ClientServerChannel[] _channels;

	private int _selectedServerIndex = -1;

	private int _selectedChannelIndex = -1;

	private string SelectedChannelLabel;

	private static readonly Color ChannelSmoothColor = new Color(0.35f, 0.85f, 0.4f);

	private static readonly Color ChannelBusyColor = new Color(0.95f, 0.75f, 0.2f);

	private static readonly Color ChannelFullColor = new Color(0.95f, 0.3f, 0.3f);

	private static readonly Color ChannelUnknownColor = new Color(0.7f, 0.7f, 0.7f);

	private bool _exitDialogOpen;

	private Action _exitConfirm;

	private Action _exitCancel;

	private void ResolveApiBaseUrl()
	{
		if (string.IsNullOrEmpty(apiBaseUrl))
		{
			var auth = FindObjectOfType<RANMobile.Authentication.Authentication>();
			apiBaseUrl = auth != null && !string.IsNullOrEmpty(auth.apiBaseUrl)
				? auth.apiBaseUrl.TrimEnd('/')
				: "http://127.0.0.1:8085";
		}
		apiBaseUrl = FamHawaiiEndpoint.ApiBaseUrl(apiBaseUrl);
	}

	public void Awake()
	{
		instance = this;
		ResolveApiBaseUrl();
		ValidateWiring();
		ApplyExitButtonPlatformPolicy();
		if (exitButton != null)
		{
			exitButton.onClick.RemoveListener(OnExitButton);
			exitButton.onClick.AddListener(OnExitButton);
		}
		ShowConnectingModal(false);
	}

	private void ValidateWiring()
	{
		if (serverSelect == null) Debug.LogWarning("[PanelEnabler] serverSelect is not wired.", this);
		if (serverParent == null) Debug.LogWarning("[PanelEnabler] serverParent is not wired.", this);
		if (serverButton == null) Debug.LogWarning("[PanelEnabler] serverButton prefab is not wired.", this);
		if (serverChannels == null) Debug.LogWarning("[PanelEnabler] serverChannels is not wired.", this);
	}

	private void ApplyExitButtonPlatformPolicy()
	{
		// Desktop/editor build: exit button stays visible.
		if (exitButton != null)
		{
			exitButton.gameObject.SetActive(true);
		}
	}

	private static Color ChannelStatusColor(ChannelStatusEnum status)
	{
		switch (status)
		{
		case ChannelStatusEnum.Smooth:
			return ChannelSmoothColor;
		case ChannelStatusEnum.Busy:
			return ChannelBusyColor;
		case ChannelStatusEnum.Full:
			return ChannelFullColor;
		default:
			return ChannelUnknownColor;
		}
	}

	public void ServerChannel()
	{
		if (_servers == null)
		{
			return;
		}
		_channels = _servers[_selectedServerIndex].channels;
		BuildChannelRows(serverChannels.transform, _channels);
	}

	private void BuildChannelRows(Transform channelRoot, ClientServerChannel[] channels)
	{
		if (channelRoot == null)
		{
			return;
		}
		// Reuse the first child as a template when one exists; otherwise fall back
		// to nothing (no prefab was wired for channels on this object).
		Transform template = channelRoot.childCount > 0 ? channelRoot.GetChild(0) : null;
		// Clear previous rows but keep the first child as the template.
		for (int i = channelRoot.childCount - 1; i >= 1; i--)
		{
			Destroy(channelRoot.GetChild(i).gameObject);
		}
		if (template == null || channels == null)
		{
			return;
		}
		template.gameObject.SetActive(false);
		for (int i = 0; i < channels.Length; i++)
		{
			GameObject row = Instantiate(template.gameObject, channelRoot);
			row.SetActive(true);
			ApplyChannelRow(row.transform, channels[i], i);
		}
	}

	private void ApplyChannelRow(Transform channelUI, ClientServerChannel existing, int displayIndex)
	{
		var label = channelUI.GetComponentInChildren<TextMeshProUGUI>(true);
		if (label != null)
		{
			label.text = TrinityRift.UI.ServerSelect.ServerSelectLabels.Channel(existing.channelIndex, existing.pk);
			label.color = unselectedChannelTextColor;
		}
		var status = channelUI.GetComponentsInChildren<TextMeshProUGUI>(true);
		if (status.Length > 1)
		{
			status[1].text = existing.channelStatus.ToString();
			status[1].color = ChannelStatusColor(existing.channelStatus);
		}
		var button = channelUI.GetComponent<Button>();
		if (button == null)
		{
			button = channelUI.GetComponentInChildren<Button>(true);
		}
		if (button != null)
		{
			ChannelEnum channelEnum = (ChannelEnum)existing.channelIndex;
			int captured = existing.channelIndex;
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(delegate
			{
				SelectedServerChannel(_servers[_selectedServerIndex].serverName);
				_selectedChannelIndex = captured;
				if (selectedChannelButton != null && selectedChannelButton != button)
				{
					var prev = selectedChannelButton.GetComponentInChildren<TextMeshProUGUI>(true);
					if (prev != null)
					{
						prev.color = unselectedChannelTextColor;
					}
				}
				selectedChannelButton = button;
				selectedChannelGameObject = channelUI.gameObject;
				if (label != null)
				{
					label.color = selectedChannelTextColor;
				}
			});
		}
	}

	private static void ClearChildren(Transform parent)
	{
		if (parent == null)
		{
			return;
		}
		for (int i = parent.childCount - 1; i >= 0; i--)
		{
			Destroy(parent.GetChild(i).gameObject);
		}
	}

	public void UpdateServerChannel(ServerEnum server, ChannelEnum channel)
	{
		ClientServerAddress address = FindServer(server);
		if (address == null)
		{
			return;
		}
		ClientServerChannel ch = FindChannel(address, channel);
		if (ch == null)
		{
			return;
		}
		_channels = address.channels;
		_selectedChannelIndex = ch.channelIndex;
		SelectedChannelLabel = TrinityRift.UI.ServerSelect.ServerSelectLabels.Channel(ch.channelIndex, ch.pk);
	}

	private static ClientServerAddress FindServer(ServerEnum server)
	{
		if (instance == null || instance._servers == null)
		{
			return null;
		}
		foreach (ClientServerAddress s in instance._servers)
		{
			if (s.serverName == server)
			{
				return s;
			}
		}
		return null;
	}

	private static ClientServerChannel FindChannel(ClientServerAddress address, ChannelEnum? channel)
	{
		if (address == null || address.channels == null)
		{
			return null;
		}
		foreach (ClientServerChannel c in address.channels)
		{
			if (channel == null || (ChannelEnum)c.channelIndex == channel.Value)
			{
				return c;
			}
		}
		return null;
	}

	public void ServerConnect(ChannelEnum channel, GameObject connectChannelGameObject = null, Button selectedButton = null)
	{
		if (_isConnecting)
		{
			return;
		}
		_selectedChannelIndex = (int)channel;
		SelectedChannelLabel = TrinityRift.UI.ServerSelect.ServerSelectLabels.Channel((int)channel, false);
		if (selectedButton != null)
		{
			selectedChannelButton = selectedButton;
			var t = selectedButton.GetComponentInChildren<TextMeshProUGUI>(true);
			if (t != null)
			{
				t.color = selectedChannelTextColor;
			}
		}
		if (connectChannelGameObject != null)
		{
			selectedChannelGameObject = connectChannelGameObject;
		}
		BeginConnecting();
	}

	public void ServerConnectButton()
	{
		if (_isConnecting || _selectedChannelIndex < 0)
		{
			return;
		}
		BeginConnecting();
	}

	private void DeselectPreviousButton()
	{
		if (selectedChannelButton != null)
		{
			var t = selectedChannelButton.GetComponentInChildren<TextMeshProUGUI>(true);
			if (t != null)
			{
				t.color = unselectedChannelTextColor;
			}
		}
		selectedChannelButton = null;
		selectedChannelGameObject = null;
	}

	public void SelectedServerChannel(ServerEnum server)
	{
		if (_servers == null)
		{
			return;
		}
		for (int i = 0; i < _servers.Length; i++)
		{
			if (_servers[i].serverName == server)
			{
				_selectedServerIndex = i;
				break;
			}
		}
		if (_selectedServerIndex < 0)
		{
			return;
		}
		DeselectPreviousButton();
		_selectedChannelIndex = -1;
		if (serverChannels != null)
		{
			serverChannels.SetActive(true);
		}
		if (serverPanel != null)
		{
			serverPanel.SetActive(false);
		}
		MoveServersPanel(shiftedXPosition);
		if (_servers[_selectedServerIndex].channels != null)
		{
			ServerChannel();
		}
		else
		{
			StartCoroutine(FetchChannelsForSelected());
		}
	}

	public void OnBackButtonClicked()
	{
		if (serverChannels != null && serverChannels.activeSelf)
		{
			serverChannels.SetActive(false);
			if (serverPanel != null)
			{
				serverPanel.SetActive(true);
			}
			MoveServersPanel(centeredXPosition);
			DeselectPreviousButton();
			_selectedChannelIndex = -1;
		}
		else
		{
			HideServerSelect();
		}
	}

	private void HideServerSelect()
	{
		if (serverSelect != null)
		{
			var canvas = serverSelect.GetComponent<Canvas>();
			if (canvas != null)
			{
				canvas.enabled = false;
				var ray = serverSelect.GetComponent<GraphicRaycaster>();
				if (ray != null)
				{
					ray.enabled = false;
				}
			}
			else
			{
				serverSelect.SetActive(false);
			}
		}
		if (mainScreen != null)
		{
			mainScreen.SetActive(true);
		}
	}

	public void TapPanel()
	{
		// The scene's visible server-select screen is ServerSelectNew, driven by
		// ServerSelectUI. When that component is present, let it own the flow.
		if (TrinityRift.UI.ServerSelect.ServerSelectUI.Instance != null)
		{
			TrinityRift.UI.ServerSelect.ServerSelectUI.Instance.TapPanel();
			return;
		}
		if (serverSelect != null)
		{
			var canvas = serverSelect.GetComponent<Canvas>();
			if (canvas != null)
			{
				canvas.enabled = true;
				var ray = serverSelect.GetComponent<GraphicRaycaster>();
				if (ray != null)
				{
					ray.enabled = true;
				}
				serverSelect.SetActive(true);
			}
			else
			{
				serverSelect.SetActive(true);
			}
		}
		if (mainScreen != null)
		{
			mainScreen.SetActive(false);
		}
		GetRegisteredChannels();
	}

	public void OnExitButton()
	{
		if (_exitDialogOpen)
		{
			return;
		}
		_exitDialogOpen = true;
		_exitConfirm = Application.Quit;
		_exitCancel = delegate { _exitDialogOpen = false; };
		if (globalConfirmButton != null)
		{
			globalConfirmButton.SetActive(true);
			var buttons = globalConfirmButton.GetComponentsInChildren<Button>(true);
			foreach (Button b in buttons)
			{
				b.onClick.RemoveAllListeners();
			}
			if (buttons.Length > 0)
			{
				buttons[0].onClick.AddListener(delegate
				{
					globalConfirmButton.SetActive(false);
					_exitDialogOpen = false;
					_exitConfirm?.Invoke();
				});
			}
			if (buttons.Length > 1)
			{
				buttons[1].onClick.AddListener(delegate
				{
					globalConfirmButton.SetActive(false);
					_exitDialogOpen = false;
					_exitCancel?.Invoke();
				});
			}
		}
		else
		{
			_exitDialogOpen = false;
			Application.Quit();
		}
	}

	private void ConfirmExit()
	{
		_exitConfirm?.Invoke();
	}

	private void CancelExit()
	{
		_exitCancel?.Invoke();
	}

	public void LoadLoginScreen()
	{
		if (loginPanel != null)
		{
			var canvas = loginPanel.GetComponent<Canvas>();
			if (canvas != null)
			{
				canvas.enabled = true;
			}
			loginPanel.SetActive(true);
		}
		HideServerSelect();
	}

	private void BeginConnecting()
	{
		_isConnecting = true;
		_connectStartTime = Time.realtimeSinceStartup;
		ShowConnectingModal(true);
		_connectTimeoutCo = StartCoroutine(ConnectTimeout());
		var auth = FindObjectOfType<RANMobile.Authentication.Authentication>();
		if (auth != null && _servers != null && _selectedServerIndex >= 0)
		{
			auth.serverSelected = _servers[_selectedServerIndex].serverName.ToString();
		}
		if (ChannelName != null && !string.IsNullOrEmpty(SelectedChannelLabel))
		{
			ChannelName.text = SelectedChannelLabel;
		}
		BeginFinalize();
	}

	private IEnumerator ConnectTimeout()
	{
		yield return new WaitForSecondsRealtime(connectTimeoutSeconds);
		if (_isConnecting)
		{
			FailConnect("Connection timed out.");
		}
	}

	private void BeginFinalize()
	{
		RunAfterMinModalTime(delegate
		{
			EndConnecting();
			LoadLoginScreen();
		});
	}

	private void RunAfterMinModalTime(Action onComplete)
	{
		_minModalCo = StartCoroutine(MinModalDelay(onComplete));
	}

	private IEnumerator MinModalDelay(Action onComplete)
	{
		float elapsed = Time.realtimeSinceStartup - _connectStartTime;
		if (elapsed < minConnectingModalSeconds)
		{
			yield return new WaitForSecondsRealtime(minConnectingModalSeconds - elapsed);
		}
		onComplete?.Invoke();
	}

	private void EndConnecting()
	{
		_isConnecting = false;
		if (_connectTimeoutCo != null)
		{
			StopCoroutine(_connectTimeoutCo);
			_connectTimeoutCo = null;
		}
		if (_minModalCo != null)
		{
			StopCoroutine(_minModalCo);
			_minModalCo = null;
		}
		ShowConnectingModal(false);
	}

	private void FailConnect(string reason)
	{
		EndConnecting();
		ReturnToServerSelect();
		ShowResponseMessage(reason);
	}

	private void ReturnToServerSelect()
	{
		if (serverSelect != null)
		{
			var canvas = serverSelect.GetComponent<Canvas>();
			if (canvas != null)
			{
				canvas.enabled = true;
				var ray = serverSelect.GetComponent<GraphicRaycaster>();
				if (ray != null)
				{
					ray.enabled = true;
				}
			}
			serverSelect.SetActive(true);
		}
	}

	private void ShowConnectingModal(bool show)
	{
		GameObject modal = connectingModal != null ? connectingModal : webLoader;
		if (modal != null)
		{
			modal.SetActive(show);
		}
		if (show && connectingText != null)
		{
			connectingText.text = connectingBaseText;
			if (_dotAnimCo != null)
			{
				StopCoroutine(_dotAnimCo);
			}
			_dotAnimCo = StartCoroutine(AnimateConnectingDots());
		}
		else if (_dotAnimCo != null)
		{
			StopCoroutine(_dotAnimCo);
			_dotAnimCo = null;
		}
	}

	private IEnumerator AnimateConnectingDots()
	{
		int dots = 0;
		var wait = new WaitForSecondsRealtime(dotAnimInterval);
		while (connectingText != null)
		{
			connectingText.text = connectingBaseText + new string('.', dots);
			dots = (dots + 1) % 4;
			yield return wait;
		}
	}

	public void GetRegisteredChannels()
	{
		ResolveApiBaseUrl();
		if (IsAllGameDataReady())
		{
			StartCoroutine(FetchServerList());
		}
		else
		{
			StartCoroutine(WaitForGameDataThenFetchServerList());
		}
	}

	private static bool IsAllGameDataReady()
	{
		// The standalone server does not gate on the original client's game-data
		// download, so the list can be fetched immediately.
		return true;
	}

	private IEnumerator WaitForGameDataThenFetchServerList()
	{
		float t0 = Time.realtimeSinceStartup;
		while (!IsAllGameDataReady() && Time.realtimeSinceStartup - t0 < 15f)
		{
			yield return null;
		}
		yield return FetchServerList();
	}

	public void ShowResponseMessage(string msg)
	{
		if (responseMsgTxt == null)
		{
			return;
		}
		responseMsgTxt.text = msg;
		responseMsgTxt.transform.parent.gameObject.SetActive(true);
		StartCoroutine(RemoveAfterSeconds(3, responseMsgTxt.transform.parent.gameObject));
	}

	private IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
	{
		yield return new WaitForSecondsRealtime(seconds);
		if (obj != null)
		{
			obj.SetActive(false);
		}
	}

	private void MoveServersPanel(float targetXPosition)
	{
		if (serversPanelContainer == null)
		{
			return;
		}
		Vector2 pos = serversPanelContainer.anchoredPosition;
		pos.x = targetXPosition;
		serversPanelContainer.anchoredPosition = pos;
	}

	[Serializable]
	private class ServerListDto
	{
		public ServerDto[] servers;
	}

	[Serializable]
	private class ServerDto
	{
		public int id;
		public string name;
		public string host;
		public int port;
		public string status;
		public int population;
		public int maxPopulation;
	}

	[Serializable]
	private class ChannelListDto
	{
		public ChannelDto[] channels;
	}

	[Serializable]
	private class ChannelDto
	{
		public byte channelIndex;
		public int playersInChannel;
		public int maxChannelUser;
		public int channel;
		public int channelStatus;
		public bool pk;
	}

	private IEnumerator FetchServerList()
	{
		using (UnityWebRequest req = UnityWebRequest.Get(apiBaseUrl + "/api/servers"))
		{
			yield return req.SendWebRequest();
			if (req.result != UnityWebRequest.Result.Success)
			{
				ShowResponseMessage("Could not reach the server list.");
				yield break;
			}
			ServerListDto dto = null;
			try
			{
				dto = JsonUtility.FromJson<ServerListDto>(req.downloadHandler.text);
			}
			catch (Exception e)
			{
				Debug.LogError("[PanelEnabler] Bad /api/servers payload: " + e.Message, this);
			}
			if (dto == null || dto.servers == null)
			{
				ShowResponseMessage("Empty server list.");
				yield break;
			}
			_servers = new ClientServerAddress[dto.servers.Length];
			for (int i = 0; i < dto.servers.Length; i++)
			{
				ServerDto s = dto.servers[i];
				ServerEnum name;
				if (!Enum.TryParse(s.name, true, out name))
				{
					name = ServerEnum.Strife;
				}
				_servers[i] = new ClientServerAddress
				{
					serverName = name,
					groupId = (byte)s.id,
					agentState = 0,
					agentIpAddress = s.host,
					agentPort = (ushort)s.port,
					totalPlayers = (ushort)s.population,
					channels = null
				};
			}
			_selectedServerIndex = -1;
			BuildServerRows();
		}
	}

	private void BuildServerRows()
	{
		if (serverParent == null || serverButton == null || _servers == null)
		{
			return;
		}
		ClearChildren(serverParent);
		for (int i = 0; i < _servers.Length; i++)
		{
			GameObject row = Instantiate(serverButton, serverParent);
			row.SetActive(true);
			var label = row.GetComponentInChildren<TextMeshProUGUI>(true);
			if (label != null)
			{
				label.text = _servers[i].serverName.ToString();
			}
			var popLabel = row.GetComponentsInChildren<TextMeshProUGUI>(true);
			if (popLabel.Length > 1)
			{
				popLabel[1].text = _servers[i].totalPlayers.ToString();
			}
			var button = row.GetComponent<Button>();
			if (button == null)
			{
				button = row.GetComponentInChildren<Button>(true);
			}
			if (button != null)
			{
				ServerEnum captured = _servers[i].serverName;
				button.onClick.RemoveAllListeners();
				button.onClick.AddListener(delegate
				{
					SelectedServerChannel(captured);
				});
			}
		}
		if (serverPanel != null)
		{
			serverPanel.SetActive(true);
		}
		if (serverChannels != null)
		{
			serverChannels.SetActive(false);
		}
		MoveServersPanel(centeredXPosition);
	}

	private IEnumerator FetchChannelsForSelected()
	{
		if (_selectedServerIndex < 0 || _servers == null)
		{
			yield break;
		}
		ClientServerAddress srv = _servers[_selectedServerIndex];
		using (UnityWebRequest req = UnityWebRequest.Get(apiBaseUrl + "/api/servers/" + srv.groupId + "/channels"))
		{
			yield return req.SendWebRequest();
			if (req.result != UnityWebRequest.Result.Success)
			{
				ShowResponseMessage("Could not load channels.");
				yield break;
			}
			ChannelListDto dto = null;
			try
			{
				dto = JsonUtility.FromJson<ChannelListDto>(req.downloadHandler.text);
			}
			catch (Exception e)
			{
				Debug.LogError("[PanelEnabler] Bad channels payload: " + e.Message, this);
			}
			if (dto == null || dto.channels == null)
			{
				ShowResponseMessage("No channels available.");
				yield break;
			}
			ClientServerChannel[] mapped = new ClientServerChannel[dto.channels.Length];
			for (int i = 0; i < dto.channels.Length; i++)
			{
				ChannelDto c = dto.channels[i];
				mapped[i] = new ClientServerChannel
				{
					channelIndex = c.channelIndex,
					playersInChannel = (ushort)c.playersInChannel,
					maxChannelUser = (ushort)c.maxChannelUser,
					channel = (ChannelEnum)c.channel,
					channelStatus = (ChannelStatusEnum)c.channelStatus,
					pk = c.pk
				};
			}
			srv.channels = mapped;
			_channels = mapped;
			ServerChannel();
		}
	}
}
