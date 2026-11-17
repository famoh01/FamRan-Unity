using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class IGNWithVendScript : MonoBehaviour
{
	private static readonly List<RaycastResult> s_sharedRaycastResults;

	private static PointerEventData s_sharedPointerEventData;

	private static EventSystem s_sharedEventSystem;

	private static int s_sharedRaycastFrame;

	private static int s_sharedClickedVendorId;

	private static int s_handledVendorClickFrame;

	public TextMeshProUGUI _charName;

	public TextMeshProUGUI _vendingName;

	public GameObject _vendingBtn;

	private Unit _unit;

	private RectTransform _vendingBtnRect;

	private Canvas _vendCanvas;

	private Billboard _vendBillboard;

	private WorldSpaceUiConstantSize _vendConstantSize;

	private PlayerIGNScript _vendDisplaySwitcher;

	private bool _shopLabelVisible;

	private bool _generalRenderVisible;

	private bool _renderVisible;

	private bool _vendChatVisible;

	private string _lastCharNameText;

	private string _lastVendingNameText;

	private bool _lastOwnerRed;

	private string _lastOwnerName;

	private string _currentTitle;

	private bool _isLocalPlayer;

	internal int _driverVendIndex;

	private NameplateContentDriver _driver;

	private int _lastContentGeneration;

	private float _contentBackstopAt;

	private VendManagerNet _boundNet;

	private void Awake()
	{
	}

	private Unit ResolveUnit()
	{
		return null;
	}

	private ushort GetOwnerGaeaId()
	{
		return 0;
	}

	private string GetOwnerCharName()
	{
		return null;
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void VendTick(int generation, float now)
	{
	}

	private void TryBindNet()
	{
	}

	public void SyncFromMirror()
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

	private void ApplyTitle()
	{
	}

	private void ApplyTitle(Unit unit)
	{
	}

	private void SetLabelVisible(bool visible)
	{
	}

	public void SetRenderVisible(bool generalVisible, bool vendorVisible)
	{
	}

	public void SetVendChatVisible(bool visible)
	{
	}

	private void ApplyRenderState()
	{
	}

	private void UpdateDriverRegistration()
	{
	}

	private void TryHandleVendClick()
	{
	}

	private static int GetSharedClickedVendorId()
	{
		return 0;
	}
}
