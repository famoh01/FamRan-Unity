using System.Collections.Generic;
using Models.Library.Shared.Enums;
using Models.Library.Shared.Game.Club;
using Models.Network.Shared.Response;
using TMPro;
using TrinityRift.Network;
using UnityEngine;
using UnityEngine.UI;

public class GuildManager : MonoBehaviour
{
	public static GuildManager Instance;

	[Header("Create-club input panel")]
	public Canvas guildInputFieldPanel;

	public TextMeshProUGUI title;

	public Button btnOk;

	public Button btnCancel;

	public TMP_InputField nameInput;

	[Header("Club Info")]
	public Canvas clubCanvas;

	public TextMeshProUGUI clubName;

	public Image clubBadgeImage;

	public Button clubBadgeUpload;

	public TextMeshProUGUI clubMaster;

	public TextMeshProUGUI clubFoundation;

	public TextMeshProUGUI clubLevel;

	public TextMeshProUGUI clubResult;

	public TextMeshProUGUI clubAlias;

	public Button clubAliasEdit;

	public Button clubDismiss;

	public TextMeshProUGUI clubDismissText;

	public TextMeshProUGUI notice;

	public Button noticeEdit;

	public Button noticeSave;

	public Button noticeCancel;

	public GameObject memberCanvas;

	public GameObject allianceCanvas;

	public GameObject hostileCanvas;

	public Button exit;

	private GameObject _baseUIClubMember;

	[Header("Guild Invite")]
	public Button inviteButton;

	[Header("v2 — Member list online filter")]
	public Toggle clubOnlineOnlyToggle;

	private bool _onlineOnlyFilter;

	private readonly List<SClubMember> _sortedMembers;

	[Header("v2 Progression — optional Inspector slots")]
	public TextMeshProUGUI clubLivingPoints;

	public TextMeshProUGUI clubRankUpPreview;

	public Button clubRankUpButton;

	public Button clubDonateButton;

	public TMP_InputField clubDonateAmount;

	[Header("Officer Permission Prompt")]
	public GameObject clubOfficerPrompt;

	public TextMeshProUGUI playerIGN;

	public Button officerSave;

	public Toggle recruit;

	public Toggle announce;

	public Toggle authenticate;

	public Toggle expel;

	public Toggle badge;

	public Toggle clubChallege;

	public Button officerCancel;

	public Image DefaultClubBadge;

	[Header("v3 Alliance UI")]
	public GameObject allianceListContent;

	public TextMeshProUGUI allianceStatusText;

	public Button allianceSecedeButton;

	public Button allianceDissolveButton;

	private GameObject _baseUIAllianceMember;

	private ushort _pendingNpcGaeaId;

	public uint SelectedOfficerCharId;

	public TextMeshProUGUI clubMemberCountText;

	private bool _wired;

	private uint _pendingAllianceChiefCharId;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void EnsureWired()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void Visibility(bool state)
	{
	}

	public void Visibility(bool state, ushort npcGaeaId)
	{
	}

	public void OpenClubPanel()
	{
	}

	private void StartGuildCreate()
	{
	}

	public void SaveNoticeChanges()
	{
	}

	public void SaveAliasChanges(string alias)
	{
	}

	private static void ShowToast(string text)
	{
	}

	public static bool ShowCooldownToastIfActive(ServerConnectionManager scm, ServerConnectionManager.ClubOp op, string label)
	{
		return false;
	}

	public void DismissOrLeaveClub()
	{
	}

	public void SendGuildInvite()
	{
	}

	public void CheckIfCanInvite()
	{
	}

	private void SaveOfficerFlags()
	{
	}

	private void OnClubCreateResult(F2C_ClubCreateResponse m)
	{
	}

	private void OnClubInfoReceived(F2C_ClubInfoResponse m)
	{
	}

	private void OnClubInviteAsk(F2C_ClubInviteAsk m)
	{
	}

	private void OnClubInviteResult(F2C_ClubInviteResult m)
	{
	}

	private void OnMemberAdded(F2C_ClubMemberAdd m)
	{
	}

	private void OnMemberRemoved(F2C_ClubMemberRemove m)
	{
	}

	private void OnMasterChanged(F2C_ClubMasterChange m)
	{
	}

	private void OnMemberFlagsUpdated(F2C_ClubMemberFlagsUpdate m)
	{
	}

	private void OnNoticeChanged(F2C_ClubNoticeUpdate m)
	{
	}

	private void OnAliasChanged(F2C_ClubAliasUpdate m)
	{
	}

	private void OnMemberStateChanged(F2C_ClubMemberStateUpdate m)
	{
	}

	public void DonateGoldToClub(long goldAmount)
	{
	}

	private void OnDonateButtonClicked()
	{
	}

	public void RequestRankUpFromNpc(ushort npcGaeaId)
	{
	}

	private void OnLivingPointsChanged(F2C_ClubLivingPointsUpdate m)
	{
	}

	private void OnRankChanged(F2C_ClubRankUpdate m)
	{
	}

	public void ToggleClubRelation(uint targetClubId, bool isHostile, bool isAdd)
	{
	}

	private static void AllianceMsg(string msg)
	{
	}

	private bool RequireGuildMaster(out ServerConnectionManager scm, out SClub my)
	{
		scm = null;
		my = null;
		return false;
	}

	private void ConfirmSecedeAlliance()
	{
	}

	private void ConfirmDissolveAlliance()
	{
	}

	public void ProposeAlliance(uint targetCharId)
	{
	}

	public void AnswerAlliance(bool accept)
	{
	}

	public void SecedeAlliance()
	{
	}

	public void DissolveAlliance()
	{
	}

	public void DelAllianceMember(uint targetClubId)
	{
	}

	private void OnAllianceAsk(F2C_ClubAllianceAsk m)
	{
	}

	private void OnAllianceChanged()
	{
	}

	private void OnAllianceResult(F2C_ClubAllianceResult m)
	{
	}

	private static string AllianceResultText(ClubAllianceResultEnum r)
	{
		return null;
	}

	private void RefreshAlliancePanel()
	{
	}

	private void SpawnAllianceRow(uint clubId, string clubName, bool isChiefRow, bool callerIsChief)
	{
	}

	private static string ResolveAllianceGuildName(SClub my, uint clubId)
	{
		return null;
	}

	public void RenameClub(string newName)
	{
	}

	private void OnClubRenamed(F2C_ClubRenamed m)
	{
	}

	private void OnRelationChanged(F2C_ClubRelationChanged m)
	{
	}

	private void OnClubDissolved(F2C_ClubDissolveNotify m)
	{
	}

	private void OnMembershipResult(F2C_ClubMembershipResponse m)
	{
	}

	private void OnMarkResult(F2C_ClubMarkResult m)
	{
	}

	private void OnMarkUpdated(uint clubId)
	{
	}

	private void FormatClubInfo(SClub data)
	{
	}

	private void LoadMemberList(SClub data)
	{
	}

	private static HashSet<uint> ComputeFounderSet(SClub data)
	{
		return null;
	}

	public void SetOnlineOnlyFilter(bool onlineOnly)
	{
	}
}
