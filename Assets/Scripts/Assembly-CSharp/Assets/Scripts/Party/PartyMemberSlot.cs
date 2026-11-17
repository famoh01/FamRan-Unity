using System.Collections.Generic;
using Models.Library.Shared.Game;
using RAN.Mobile.GameModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Party
{
	public class PartyMemberSlot : MonoBehaviour
	{
		public GameObject _memberIGN;

		public GameObject _memberRole;

		public GameObject _memberCount;

		[Header("Party Slot Icons")]
		public Image _memberPartyIcon;

		public Sprite[] memberNumberSprites;

		public GameObject _memberAssistBtn;

		public GameObject _memberLeaveBtn;

		public Slider _memberHealthBar;

		[Header("Buffs")]
		public Transform _buffIconsContainer;

		public GameObject _buffIconPrefab;

		private int _memberSlotIndex;

		private PartyMemberInfo _currentMemberInfo;

		private Canvas _panelCanvas;

		private Dictionary<Vector2, GameObject> _activeBuffIcons;

		private Dictionary<Vector2, Image> _activeBuffFills;

		private Dictionary<Vector2, SkillObject> _activeBuffSkills;

		private List<Vector2> _buffRemoveBuffer;

		private readonly Stack<GameObject> _buffIconPool;

		private TextMeshProUGUI _memberIGNText;

		private TextMeshProUGUI _memberCountText;

		private Button _memberAssistButton;

		private Button _memberLeaveButton;

		private readonly HashSet<Vector2> _currentBuffsThisFrame;

		private TargetInfoEx _cachedTargetInfo;

		private ushort _cachedTargetInfoPlayerId;

		private bool _hasCachedTargetInfo;

		private ushort _assistMemberId;

		private ushort _leaveMemberId;

		private const int BuffIconImageIndex = 1;

		private const int BuffFillImageIndex = 2;

		private void Awake()
		{
		}

		private void CacheComponents()
		{
		}

		private TargetInfoEx GetCachedTargetInfo(ushort playerId)
		{
			return default;
		}

		public void InitializeMemberSlot(GameObject partyCanvas, GameObject partyMembers, int memberSlotIndex)
		{
		}

		public void SetMemberDetail(int leaderPlayerId, PartyMemberInfo partyMemberInfo)
		{
		}

		public void UpdatePlayerInfo(PartyMemberInfo member)
		{
		}

		private void Update()
		{
		}

		private void SyncBuffsCentralized(SkillFact[] skillFacts)
		{
		}

		private void ClearAllBuffs()
		{
		}

		private GameObject RentBuffIcon()
		{
			return null;
		}

		private void ReturnBuffIcon(GameObject icon)
		{
		}

		private void OnAssistClicked()
		{
		}

		private void OnLeaveClicked()
		{
		}

		private void Assist(ushort memberGaeaId)
		{
		}

		private void Leave(ushort memberGaeaId)
		{
		}

		private void OnDestroy()
		{
		}
	}
}
