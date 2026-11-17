using Models.Library.Shared.Game.Club;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClubMemberDetail : MonoBehaviour
{
	[Header("Player Details")]
	public Image _onlineStatus;

	[Tooltip("Sprite to show when the player is online")]
	public Sprite onlineSprite;

	[Tooltip("Sprite to show when the player is offline")]
	public Sprite offlineSprite;

	public GameObject _player;

	[Header("Player Info Section")]
	[Tooltip("Displays: [Channel][Role]IGN")]
	public TextMeshProUGUI _playerIGN;

	public TextMeshProUGUI _playerCoordinates;

	public TextMeshProUGUI _joinDate;

	public GameObject _founderIcon;

	[Header("Buttons")]
	public Button _masterButton;

	public Button _officeButton;

	public Button _addPartyButton;

	public Button _dismissButton;

	public Button _whisperButton;

	[Header("Class Icons (Assign in Inspector)")]
	public Sprite iconBrawler;

	public Sprite iconSwordsman;

	public Sprite iconArcher;

	public Sprite iconShaman;

	private uint _selectedCharId;

	private string _selectedName;

	private byte _selectedFlags;

	public void Awake()
	{
	}

	public void SetMember(SClubMember m, uint masterCharId, bool isFounder = false)
	{
	}

	private void SetNewMaster()
	{
	}

	private void OpenOfficerPrompt()
	{
	}

	private void SendPartyInvite()
	{
	}

	private void WhisperToMember()
	{
	}

	private void KickOrLeave()
	{
	}
}
