using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace TrinityRift.UI.ServerSelect
{
	[DisallowMultipleComponent]
	public class ServerSelectUI : MonoBehaviour
	{
		private sealed class PanelToggle
		{
			private readonly GameObject _go;

			private readonly Canvas _canvas;

			private readonly GraphicRaycaster _raycaster;

			public bool HasCanvas => _canvas != null;

			public bool Exists => _go != null;

			public bool IsVisible
			{
				get
				{
					if (_go == null || !_go.activeInHierarchy)
					{
						return false;
					}
					return _canvas == null || _canvas.enabled;
				}
			}

			public string Name => _go != null ? _go.name : "<null>";

			public PanelToggle(GameObject go)
			{
				_go = go;
				if (go != null)
				{
					_canvas = go.GetComponent<Canvas>();
					_raycaster = go.GetComponent<GraphicRaycaster>();
				}
			}

			public void Set(bool visible)
			{
				if (_go == null)
				{
					return;
				}
				_go.SetActive(visible);
				if (_canvas != null)
				{
					_canvas.enabled = visible;
				}
				if (_raycaster != null)
				{
					_raycaster.enabled = visible;
				}
			}
		}

		[Header("Server list")]
		[Tooltip("Content transform the server rows are parented to (the one with the Layout Group).")]
		[SerializeField]
		private RectTransform serverContent;

		[SerializeField]
		private ServerRowView serverRowPrefab;

		[Tooltip("AutoScrollView on the server ScrollRect — shows the scrollbar past 3 servers.")]
		[SerializeField]
		private AutoScrollView serverScroll;

		[Header("Channel list")]
		[Tooltip("The channel frame/window. PanelEnabler's 'serverChannels'. Off until a server is picked.")]
		[SerializeField]
		private GameObject channelsPanel;

		[Tooltip("The container the channel rows live under. PanelEnabler's 'serverPanelParent'. Toggled together with channelsPanel. Leave empty if your layout only has one object.")]
		[SerializeField]
		private GameObject channelPanelParent;

		[SerializeField]
		private RectTransform channelContent;

		[SerializeField]
		private ChannelRowView channelRowPrefab;

		[Tooltip("AutoScrollView on the channel ScrollRect — shows the scrollbar past 3 channels.")]
		[SerializeField]
		private AutoScrollView channelScroll;

		[Tooltip("Optional — shows the name of the server whose channels are listed.")]
		[SerializeField]
		private TextMeshProUGUI selectedServerLabel;

		[Header("Panel positioning")]
		[Tooltip("The server list's RectTransform — the one that slides aside.")]
		[SerializeField]
		private RectTransform serverListRect;

		[Tooltip("Server list x while nothing is selected. It is the only panel on screen, so 0.")]
		[SerializeField]
		private float serverListCenteredX;

		[Tooltip("Server list x once a server is picked and the channel list shares the screen.")]
		[SerializeField]
		private float serverListShiftedX;

		[Tooltip("The channel list's RectTransform.")]
		[SerializeField]
		private RectTransform channelListRect;

		[Tooltip("Channel list x while it is open.")]
		[SerializeField]
		private float channelListX;

		[Header("Buttons")]
		[Tooltip("Connect / Start. Non-interactable until a channel is picked.")]
		[SerializeField]
		private Button connectButton;

		[Tooltip("Optional — closes the channel list and recentres the server list.")]
		[SerializeField]
		private Button backButton;

		[Header("Connecting modal")]
		[Tooltip("Shown while a connect attempt is in flight. Parent it OUTSIDE any panel this script switches off, or it will never be visible.")]
		[SerializeField]
		private GameObject connectingModal;

		[Tooltip("Optional label whose trailing dots animate ('Connecting', 'Connecting.', '..', '...').")]
		[SerializeField]
		private TextMeshProUGUI connectingText;

		[SerializeField]
		private string connectingBaseText = "Connecting";

		[SerializeField]
		private float dotAnimInterval = 0.4f;

		[Tooltip("Seconds to wait for the server before the attempt is treated as failed.")]
		[SerializeField]
		private float connectTimeoutSeconds = 10f;

		[Tooltip("Minimum time the modal stays up even if the server replies instantly, so a fast response cannot be spammed.")]
		[SerializeField]
		private float minConnectingModalSeconds = 1f;

		[Header("On successful connect")]
		[Tooltip("Login panel revealed once the channel accepts us.")]
		[SerializeField]
		private GameObject loginPanel;

		[Tooltip("Main screen to hide on success.")]
		[SerializeField]
		private GameObject mainScreen;

		[Tooltip("This whole server-select screen, hidden on success. Hidden by DISABLING ITS CANVAS component, never by deactivating the GameObject — a deactivated root can never be switched back on from here, since this script would have to reach into a dead object.")]
		[SerializeField]
		private GameObject serverSelectRoot;

		[Tooltip("Optional — label showing which channel was joined.")]
		[SerializeField]
		private TextMeshProUGUI channelNameLabel;

		[Header("Messages")]
		[Tooltip("Error/status text. Its PARENT object is the popup that gets shown for 3 seconds.")]
		[SerializeField]
		private TextMeshProUGUI responseMsgTxt;

		[Header("Diagnostics")]
		[SerializeField]
		private bool verboseLogging;

		private readonly List<ServerRowView> _serverRows = new List<ServerRowView>();

		private readonly List<ChannelRowView> _channelRows = new List<ChannelRowView>();

		private ClientServerAddress[] _servers;

		// Display names from the server list are not restricted to ServerEnum.
		// Keep the enum for existing game logic, but show the server's real name.
		private readonly Dictionary<byte, string> _serverDisplayNames = new Dictionary<byte, string>();

		private string ServerDisplayName(ClientServerAddress server)
		{
			string name;
			return _serverDisplayNames.TryGetValue(server.groupId, out name) && !string.IsNullOrEmpty(name)
				? name : server.serverName.ToString();
		}

		private ClientServerChannel[] _channels;

		private int _selectedServer = -1;

		private int _selectedChannel = -1;

		private PanelToggle _screen;

		private PanelToggle _channelsPanelToggle;

		private PanelToggle _channelParentToggle;

		private PanelToggle _connectingToggle;

		private PanelToggle _loginPanelToggle;

		private PanelToggle _mainScreenToggle;

		private PanelToggle _responsePopupToggle;

		private bool _isConnecting;

		private Coroutine _connectTimeoutCo;

		private Coroutine _minModalCo;

		private Coroutine _dotAnimCo;

		private float _connectStartTime;

		private Coroutine _responseHideCo;

		private string _apiBaseUrl = "http://127.0.0.1:8080";

		private string _selectedServerHost;

		private ushort _selectedServerPort = 7777;

		public static ServerSelectUI Instance { get; private set; }

		private void Log(string msg)
		{
			if (verboseLogging)
			{
				Debug.Log("[ServerSelectUI] " + msg, this);
			}
		}

		private void Awake()
		{
			Instance = this;
			EnsureWired();
			var auth = FindObjectOfType<RANMobile.Authentication.Authentication>();
			if (auth != null && !string.IsNullOrEmpty(auth.apiBaseUrl))
			{
				_apiBaseUrl = auth.apiBaseUrl.TrimEnd('/');
			}
			_apiBaseUrl = FamHawaiiEndpoint.ApiBaseUrl(_apiBaseUrl);
			_channelsPanelToggle.Set(false);
			_channelParentToggle.Set(false);
			SetConnectInteractable(false);
			if (channelContent != null && channelContent.childCount > 0)
			{
				// First child is the row template — keep it hidden until rows are built.
				channelContent.GetChild(0).gameObject.SetActive(false);
			}
			if (serverContent != null && serverContent.childCount > 0)
			{
				serverContent.GetChild(0).gameObject.SetActive(false);
			}
			ValidateWiring();
		}

		private void OnEnable()
		{
			// Domain reloads (script recompiles during play) wipe non-serialized
			// fields but re-run OnEnable — rebuild the toggles and re-attach
			// button listeners so the UI keeps working after an eval/recompile.
			Instance = this;
			EnsureWired();
		}

		private void EnsureWired()
		{
			if (_screen != null && _connectingToggle != null && _loginPanelToggle != null)
			{
				return;
			}
			_screen = new PanelToggle(serverSelectRoot != null ? serverSelectRoot : gameObject);
			_channelsPanelToggle = new PanelToggle(channelsPanel);
			_channelParentToggle = new PanelToggle(channelPanelParent);
			_connectingToggle = new PanelToggle(connectingModal);
			_loginPanelToggle = new PanelToggle(loginPanel);
			_mainScreenToggle = new PanelToggle(mainScreen);
			_responsePopupToggle = new PanelToggle(responseMsgTxt != null ? responseMsgTxt.transform.parent != null ? responseMsgTxt.transform.parent.gameObject : null : null);
			if (backButton != null)
			{
				backButton.onClick.RemoveListener(OnBackButtonClicked);
				backButton.onClick.AddListener(OnBackButtonClicked);
			}
			if (connectButton != null)
			{
				connectButton.onClick.RemoveListener(ServerConnectButton);
				connectButton.onClick.AddListener(ServerConnectButton);
			}
		}

		private void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}

		private void ValidateWiring()
		{
			if (serverContent == null) Debug.LogWarning("[ServerSelectUI] serverContent is not wired.", this);
			if (serverRowPrefab == null) Debug.LogWarning("[ServerSelectUI] serverRowPrefab is not wired.", this);
			if (channelContent == null) Debug.LogWarning("[ServerSelectUI] channelContent is not wired.", this);
			if (channelRowPrefab == null) Debug.LogWarning("[ServerSelectUI] channelRowPrefab is not wired.", this);
			if (connectButton == null) Debug.LogWarning("[ServerSelectUI] connectButton is not wired.", this);
		}

		public void TapPanel()
		{
			ShowScreen();
			GetRegisteredChannels();
		}

		public void ShowScreen()
		{
			_screen.Set(true);
			_mainScreenToggle.Set(false);
		}

		public void HideScreen()
		{
			_screen.Set(false);
		}

		private static string Describe(PanelToggle t)
		{
			return t != null && t.Exists ? t.Name + (t.IsVisible ? " (visible)" : " (hidden)") : "<null>";
		}

		public void BackToServerSelect()
		{
			ClearSelection();
			CloseChannelPanels();
		}

		public void GetRegisteredChannels()
		{
			StartCoroutine(FetchServers());
		}

		private void OnServerListUpdated(ClientServerAddress[] servers)
		{
			_servers = servers;
			BuildServerRows(servers);
		}

		public void ServerChannel(ClientServerAddress[] servers)
		{
			OnServerListUpdated(servers);
		}

		public void OnServerRowClicked(int index)
		{
			if (_servers == null || index < 0 || index >= _servers.Length || _isConnecting)
			{
				return;
			}
			_selectedServer = index;
			for (int i = 0; i < _serverRows.Count; i++)
			{
				_serverRows[i].SetSelected(i == index);
			}
			ClientServerAddress srv = _servers[index];
			_selectedServerHost = srv.agentIpAddress;
			_selectedServerPort = srv.agentPort;
			if (selectedServerLabel != null)
			{
				selectedServerLabel.text = ServerDisplayName(srv);
			}
			SetServerListX(serverListShiftedX);
			if (srv.channels != null && srv.channels.Length > 0)
			{
				_channels = srv.channels;
				BuildChannelRows(_channels);
				_channelsPanelToggle.Set(true);
				_channelParentToggle.Set(true);
			}
			else
			{
				StartCoroutine(FetchChannels(srv));
			}
		}

		public void SelectedServerChannel(int index)
		{
			_selectedChannel = index;
			for (int i = 0; i < _channelRows.Count; i++)
			{
				_channelRows[i].SetSelected(i == index);
			}
			SetConnectInteractable(true);
		}

		private void BuildServerRows(ClientServerAddress[] servers)
		{
			foreach (ServerRowView row in _serverRows)
			{
				if (row != null)
				{
					row.SetActive(false);
				}
			}
			int count = servers != null ? servers.Length : 0;
			for (int i = 0; i < count; i++)
			{
				ServerRowView row;
				if (i < _serverRows.Count)
				{
					row = _serverRows[i];
					row.SetActive(true);
				}
				else
				{
					row = Instantiate(serverRowPrefab, serverContent);
					row.Initialize(this);
					_serverRows.Add(row);
				}
				ClientServerAddress s = servers[i];
				row.Bind(i, ServerDisplayName(s), s.totalPlayers);
				WireRowClick(row, i);
			}
			if (serverScroll != null)
			{
				serverScroll.Apply(count);
			}
			Log("Built " + count + " server rows.");
		}

		private void WireRowClick(ServerRowView row, int index)
		{
			// The prefab's serialized Button reference can silently fail to receive
			// runtime listeners in this ripped project, so attach a closure directly
			// to whatever Button actually renders the row.
			Button b = row.GetComponentInChildren<Button>(true);
			if (b == null)
			{
				return;
			}
			b.onClick.RemoveAllListeners();
			int captured = index;
			b.onClick.AddListener(delegate
			{
				OnServerRowClicked(captured);
			});
		}

		private void WireChannelClick(ChannelRowView row, int index)
		{
			Button b = row.GetComponentInChildren<Button>(true);
			if (b == null)
			{
				return;
			}
			b.onClick.RemoveAllListeners();
			int captured = index;
			b.onClick.AddListener(delegate
			{
				OnChannelRowClicked(captured);
			});
		}

		private void BuildChannelRows(ClientServerChannel[] channels)
		{
			foreach (ChannelRowView row in _channelRows)
			{
				if (row != null)
				{
					row.SetActive(false);
				}
			}
			int count = channels != null ? channels.Length : 0;
			for (int i = 0; i < count; i++)
			{
				ChannelRowView row;
				if (i < _channelRows.Count)
				{
					row = _channelRows[i];
					row.SetActive(true);
				}
				else
				{
					row = Instantiate(channelRowPrefab, channelContent);
					row.Initialize(this);
					_channelRows.Add(row);
				}
				row.Bind(i, channels[i]);
				WireChannelClick(row, i);
			}
			if (channelScroll != null)
			{
				channelScroll.Apply(count);
			}
			_selectedChannel = -1;
			SetConnectInteractable(false);
			Log("Built " + count + " channel rows.");
		}

		public void OnChannelRowClicked(int index)
		{
			Debug.Log("[ServerSelectUI] OnChannelRowClicked(" + index + ") channels=" + (_channels == null ? "null" : _channels.Length.ToString()) + " connecting=" + _isConnecting, this);
			if (_channels == null || index < 0 || index >= _channels.Length || _isConnecting)
			{
				return;
			}
			SelectedServerChannel(index);
		}

		public void OnBackButtonClicked()
		{
			if (_selectedChannel >= 0 || _channelsPanelToggle.IsVisible)
			{
				BackToServerSelect();
			}
			else
			{
				HideScreen();
				_mainScreenToggle.Set(true);
			}
		}

		public void ServerConnectButton()
		{
			Debug.Log("[ServerSelectUI] ServerConnectButton srv=" + _selectedServer + " ch=" + _selectedChannel + " connecting=" + _isConnecting, this);
			if (_isConnecting || _selectedServer < 0 || _selectedChannel < 0)
			{
				return;
			}
			BeginConnecting();
		}

		private void BeginConnecting()
		{
			_isConnecting = true;
			_connectStartTime = Time.realtimeSinceStartup;
			ShowConnectingModal(true);
			_connectTimeoutCo = StartCoroutine(ConnectTimeout());
			var auth = FindObjectOfType<RANMobile.Authentication.Authentication>();
			if (auth != null)
			{
				auth.serverSelected = _servers[_selectedServer].serverName.ToString();
			}
			if (channelNameLabel != null && _channels != null && _selectedChannel < _channels.Length)
			{
				channelNameLabel.text = ServerSelectLabels.Channel(_channels[_selectedChannel].channelIndex, _channels[_selectedChannel].pk);
			}
			// No separate gate server in this build — the HTTP list is the authority,
			// so a picked channel is treated as an accepted connect.
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
				_loginPanelToggle.Set(true);
				HideScreen();
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
			ShowResponseMessage(reason);
		}

		private void ShowConnectingModal(bool show)
		{
			_connectingToggle.Set(show);
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

		public void ShowResponseMessage(string msg)
		{
			if (responseMsgTxt == null)
			{
				return;
			}
			responseMsgTxt.text = msg;
			_responsePopupToggle.Set(true);
			if (_responseHideCo != null)
			{
				StopCoroutine(_responseHideCo);
			}
			_responseHideCo = StartCoroutine(HideResponseAfterSeconds(3f));
		}

		private IEnumerator HideResponseAfterSeconds(float seconds)
		{
			yield return new WaitForSecondsRealtime(seconds);
			_responsePopupToggle.Set(false);
			_responseHideCo = null;
		}

		private void ClearSelection()
		{
			_selectedServer = -1;
			_selectedChannel = -1;
			foreach (ServerRowView row in _serverRows)
			{
				row.SetSelected(false);
			}
			foreach (ChannelRowView row in _channelRows)
			{
				row.SetSelected(false);
			}
			SetConnectInteractable(false);
		}

		private void CloseChannelPanels()
		{
			_channelsPanelToggle.Set(false);
			_channelParentToggle.Set(false);
			SetServerListX(serverListCenteredX);
		}

		private void SetConnectInteractable(bool interactable)
		{
			if (connectButton != null)
			{
				connectButton.interactable = interactable;
			}
		}

		private void SetServerListX(float x)
		{
			SetAnchoredX(serverListRect, x);
		}

		private void SetChannelListX(float x)
		{
			SetAnchoredX(channelListRect, x);
		}

		private static void SetAnchoredX(RectTransform rect, float x)
		{
			if (rect == null)
			{
				return;
			}
			Vector2 pos = rect.anchoredPosition;
			pos.x = x;
			rect.anchoredPosition = pos;
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

		private IEnumerator FetchServers()
		{
			using (UnityWebRequest req = UnityWebRequest.Get(_apiBaseUrl + "/api/servers"))
			{
				yield return req.SendWebRequest();
				if (req.result != UnityWebRequest.Result.Success)
				{
					ShowResponseMessage("ไม่สามารถเชื่อมต่อเพื่อโหลดรายชื่อเซิร์ฟเวอร์ได้");
					yield break;
				}
				ServerListDto dto = null;
				try
				{
					dto = JsonUtility.FromJson<ServerListDto>(req.downloadHandler.text);
				}
				catch (Exception e)
				{
					Debug.LogError("[ServerSelectUI] Bad /api/servers payload: " + e.Message, this);
				}
				if (dto == null || dto.servers == null)
				{
					ShowResponseMessage("ไม่พบรายชื่อเซิร์ฟเวอร์");
					yield break;
				}
				ClientServerAddress[] mapped = new ClientServerAddress[dto.servers.Length];
				_serverDisplayNames.Clear();
				for (int i = 0; i < dto.servers.Length; i++)
				{
					ServerDto s = dto.servers[i];
					_serverDisplayNames[(byte)s.id] = s.name;
					mapped[i] = new ClientServerAddress
					{
						serverName = ParseServerName(s.name),
						groupId = (byte)s.id,
						agentState = 0,
						agentIpAddress = s.host,
						agentPort = (ushort)s.port,
						totalPlayers = (ushort)s.population,
						channels = null
					};
				}
				OnServerListUpdated(mapped);
			}
		}

		private IEnumerator FetchChannels(ClientServerAddress srv)
		{
			using (UnityWebRequest req = UnityWebRequest.Get(_apiBaseUrl + "/api/servers/" + srv.groupId + "/channels"))
			{
				yield return req.SendWebRequest();
				if (req.result != UnityWebRequest.Result.Success)
				{
					ShowResponseMessage("ไม่สามารถโหลดรายชื่อแชนเนลได้");
					yield break;
				}
				ChannelListDto dto = null;
				try
				{
					dto = JsonUtility.FromJson<ChannelListDto>(req.downloadHandler.text);
				}
				catch (Exception e)
				{
					Debug.LogError("[ServerSelectUI] Bad channels payload: " + e.Message, this);
				}
				if (dto == null || dto.channels == null)
				{
					ShowResponseMessage("ไม่มีแชนเนลที่พร้อมใช้งาน");
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
						channel = (RAN.Mobile.Enumerators.ChannelEnum)c.channel,
						channelStatus = (RAN.Mobile.Enumerators.ChannelStatusEnum)c.channelStatus,
						pk = c.pk
					};
				}
				srv.channels = mapped;
				if (_servers != null)
				{
					for (int i = 0; i < _servers.Length; i++)
					{
						if (_servers[i].groupId == srv.groupId)
						{
							_servers[i].channels = mapped;
							break;
						}
					}
				}
				_channels = mapped;
				BuildChannelRows(_channels);
				_channelsPanelToggle.Set(true);
				_channelParentToggle.Set(true);
			}
		}

		private static RAN.Mobile.Enumerators.ServerEnum ParseServerName(string name)
		{
			if (!string.IsNullOrEmpty(name) &&
				Enum.TryParse(name, true, out RAN.Mobile.Enumerators.ServerEnum parsed))
			{
				return parsed;
			}
			return RAN.Mobile.Enumerators.ServerEnum.Strife;
		}
	}
}
