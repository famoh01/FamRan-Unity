using TMPro;
using UnityEngine;

public class PlayerIGNScript : MonoBehaviour
{
	private enum ViewState
	{
		Unknown = 0,
		NormalName = 1,
		VendName = 2,
		NormalChat = 3,
		VendChat = 4
	}

	public GameObject _ignDisplay;

	public GameObject _ignWithVendDisplay;

	[Header("Optional explicit refs (auto-resolved from child[0]/child[1] if null)")]
	[SerializeField]
	private GameObject _normalNameContainer;

	[SerializeField]
	private GameObject _normalChatContainer;

	[SerializeField]
	private GameObject _vendNameContainer;

	[SerializeField]
	private GameObject _vendChatContainer;

	[SerializeField]
	private TextMeshProUGUI _normalChatText;

	[SerializeField]
	private TextMeshProUGUI _vendChatText;

	private Unit _unit;

	private IGNScript _ignScript;

	private IGNWithVendScript _vendLabel;

	private bool _refsResolved;

	private bool _isChatActive;

	private string _chatLine;

	private bool _vendNameRenderVisible;

	private ViewState _appliedState;

	private VendManagerNet _boundNet;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void TryBindNet()
	{
	}

	private void UnbindNet()
	{
	}

	private void HandleVendorOpened(ushort vendorGaeaId, string title)
	{
	}

	private void HandleVendorClosed(ushort vendorGaeaId)
	{
	}

	public void RefreshState()
	{
	}

	public bool ShowChatBubble(string characterName, string message)
	{
		return false;
	}

	public void HideChatBubble()
	{
	}

	public void SetVendNameRenderVisible(bool visible)
	{
	}

	private void NotifyVendLabelChatState()
	{
	}

	private ViewState BuildTargetState(bool hasVend)
	{
		return ViewState.Unknown;
	}

	private void ApplyState(ViewState state)
	{
	}

	private void UpdateChatTexts()
	{
	}

	private void ResolveDisplayRefs()
	{
	}

	private static void ResolveFromRoot(GameObject root, ref GameObject nameContainer, ref GameObject chatContainer, ref TextMeshProUGUI chatText)
	{
	}

	private static void SafeSetActive(GameObject go, bool active)
	{
	}

	private Unit ResolveUnit()
	{
		return null;
	}

	private IGNScript ResolveIgnScript()
	{
		return null;
	}

	private ushort GetOwnerGaeaId()
	{
		return 0;
	}

	private bool ResolveHasVend()
	{
		return false;
	}
}
