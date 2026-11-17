using System.Collections.Generic;
using Assets.Scripts.Party;
using RAN.Mobile.GameModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyGetMemberList : MonoBehaviour
{
	public static PartyGetMemberList Instance;

	public GameObject _partyBtn;

	[Header("Canvas")]
	public GameObject _partyCanvas;

	public GameObject _partyMembers;

	public GameObject _partyCloseBtn;

	[Header("Leader")]
	public GameObject _leaderIGN;

	public Slider _leaderHealthBar;

	public GameObject _itemAllocation;

	public GameObject _coinAllocation;

	public GameObject _dishBtn;

	public GameObject _partyCount;

	public PartyLeaderSlot _partyLeaderSlot;

	private Canvas _canvasComponent;

	private Button _partyBtnComponent;

	private Button _dishBtnComponent;

	private TextMeshProUGUI _leaderIGNText;

	private TextMeshProUGUI _partyCountText;

	private TextMeshProUGUI _itemAllocText;

	private TextMeshProUGUI _coinAllocText;

	private GameObject _baseMemberSlot;

	private List<PartyMemberInfo> _currentMembers;

	private readonly List<PartyMemberSlot> _slotCache;

	private int _activeSlotCount;

	private readonly Dictionary<int, RectTransform> _minimapIcons;

	private readonly Dictionary<int, RectTransform> _bigmapIcons;

	private bool _dirty;

	public void Awake()
	{
	}

	public void FixedUpdate()
	{
	}

	public void MarkDirty()
	{
	}

	private void ShowPartyCanvas()
	{
	}

	private void ClosePartyCanvas()
	{
	}

	public void EnablePartyButton(bool enable)
	{
	}

	public void ResetPartyMemberInfoIsSetUp()
	{
	}

	public void PartyPlayerGetInfoRequest(int? leaderId = null)
	{
	}

	public void SetUpMemberInfo()
	{
	}

	private void UpdatePlayerInfo()
	{
	}

	public void UpdateMemberMinimapIcon(PartyMemberInfo member)
	{
	}

	public void HandlePartyInMap(PartyMemberInfo member, int index)
	{
	}

	private void UpdateLocationPartyIcon(PartyMemberInfo member)
	{
	}

	private void RemovePartyInMap(int memberCharId)
	{
	}

	private void RemovePartyInMapOnDisband()
	{
	}

	private void ClickDisband()
	{
	}

	public void DisbandParty()
	{
	}

	public void RemoveCurrentMembers(int member)
	{
	}

	public void UpdateCurrentMember(List<PartyMemberInfo> members)
	{
	}
}
