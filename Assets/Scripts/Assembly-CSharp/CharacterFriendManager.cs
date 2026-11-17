using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Models.Library.Shared.Enums;
using Models.Network.Shared.Response;
using TMPro;
using UnityEngine;

public class CharacterFriendManager : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CBuildFriendRows_003Ed__78 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CharacterFriendManager _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CBuildFriendRows_003Ed__78(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CBuildRows_003Ed__79 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GameObject parent;

		public CharacterFriendManager _003C_003E4__this;

		public GameObject prefabTab;

		public List<FriendRow> cache;

		public List<FriendClientEntry> source;

		public Dictionary<string, FriendRow> map;

		private int _003Ci_003E5__2;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CBuildRows_003Ed__79(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CConfirmAcceptedRequest_003Ed__60 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CharacterFriendManager _003C_003E4__this;

		public string requesterName;

		private float _003Cremaining_003E5__2;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CConfirmAcceptedRequest_003Ed__60(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CRebuildFriendListWhenQuiet_003Ed__86 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CharacterFriendManager _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CRebuildFriendListWhenQuiet_003Ed__86(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CWaitForRemovalUnblock_003Ed__71 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CharacterFriendManager _003C_003E4__this;

		public string charName;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CWaitForRemovalUnblock_003Ed__71(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	public static CharacterFriendManager Instance;

	[Header("UI Parents (scroll content)")]
	public GameObject _friendListParent;

	[Tooltip("Scroll Content of the Requests tab — one row per incoming request.")]
	public GameObject _friendRequestParent;

	[Tooltip("Scroll Content of the Blocked tab — one row per blocked character. Leave empty and blocked entries stay mixed into the friend list, as before.")]
	public GameObject _blockedListParent;

	[Header("Canvases")]
	public GameObject _friendCanvas;

	public GameObject _friendListCanvas;

	[Header("Navigation")]
	[Tooltip("FriendPanelNavigation on FriendsBG — owns open/close and the search bar.")]
	public FriendPanelNavigation _navigation;

	[Header("Request Badge (optional)")]
	[Tooltip("Root object shown only while at least one request is waiting.")]
	public GameObject _requestBadge;

	[Tooltip("Count label inside the badge. Optional — leave empty for a dot-only badge.")]
	public TextMeshProUGUI _requestBadgeText;

	[Header("Friend Count")]
	[Tooltip("Displays current/max friends, e.g. \"12/60\". Optional. Blocked characters are NOT counted — they live on their own tab and shouldn't read as friends.")]
	public TextMeshProUGUI _friendCountText;

	[Tooltip("Displays how many characters are blocked. Optional.")]
	public TextMeshProUGUI _blockedCountText;

	public const int MaxFriends = 50;

	[CompilerGenerated]
	private Action<string> m_OnFriendRequestReceived;

	private GameObject _baseUICharacterFriend;

	private GameObject _friendListTab;

	private GameObject _friendRequestTab;

	private GameObject _blockedTab;

	private readonly List<FriendClientEntry> _friends;

	private readonly List<FriendClientEntry> _listed;

	private readonly List<FriendClientEntry> _blocked;

	private bool _listLoaded;

	private readonly Dictionary<string, FriendRow> _friendRows;

	private readonly Dictionary<string, FriendRow> _blockedRows;

	private readonly List<FriendRow> _rowCache;

	private readonly List<FriendRow> _blockedRowCache;

	private const int RowCreatesPerFrame = 6;

	private Coroutine _rebuildRoutine;

	private float _rebuildDueAt;

	private const float ListRebuildDebounce = 0.15f;

	private readonly List<string> _pendingRequests;

	private readonly HashSet<string> _awaitingAccept;

	private const float AcceptConfirmTimeout = 3f;

	private readonly Dictionary<string, Coroutine> _removalsAwaitingUnblock;

	private readonly HashSet<string> _unblockedRemovalsAwaitingDelete;

	private const float RemoveUnblockTimeout = 10f;

	private const int MinSearchNameLength = 3;

	public int PendingTeleportCardSlot;

	private GameObject BlockedPrefabTab => null;

	public int PendingRequestCount => 0;

	public event Action<string> OnFriendRequestReceived
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

	public void Awake()
	{
	}

	private void OnDisable()
	{
	}

	private void OnEnable()
	{
	}

	private GameObject FindSection(string sectionName)
	{
		return null;
	}

	public void OpenPanel()
	{
	}

	public void ClosePanel()
	{
	}

	public void GetFriends()
	{
	}

	private void RequestFriendListReload()
	{
	}

	public void GetBlockedList()
	{
	}

	public void SendAddFriend(string targetName)
	{
	}

	public void CreateFriendRequest()
	{
	}

	public void SearchAddFriendList(GameObject search)
	{
	}

	public void GetNoneFriends()
	{
	}

	public void GetFriendRequests()
	{
	}

	public void AcceptAllFriendRequest()
	{
	}

	public void RejectAllFriendRequest()
	{
	}

	public void AnswerFriendRequest(string requesterName, bool accept)
	{
	}

	[IteratorStateMachine(typeof(_003CConfirmAcceptedRequest_003Ed__60))]
	private IEnumerator ConfirmAcceptedRequest(string requesterName)
	{
		return null;
	}

	private void AnswerAllRequests(bool accept)
	{
	}

	public void HandleFriendInfo(A2C_FriendInfo msg)
	{
	}

	public void HandleFriendState(A2C_FriendState msg)
	{
	}

	public void HandleFriendAddFeedback(A2C_FriendAddFeedback msg)
	{
	}

	public void HandleFriendAddLure(A2C_FriendAddLure msg)
	{
	}

	private void AddPendingRequest(string requesterName)
	{
	}

	private void RemovePendingRequest(string requesterName)
	{
	}

	private int FindPendingIndex(string requesterName)
	{
		return 0;
	}

	public void RequestRemoveFriend(string charName, bool displayedAsBlocked)
	{
	}

	private void RemoveFriend(string charName, bool unblockFirst)
	{
	}

	[IteratorStateMachine(typeof(_003CWaitForRemovalUnblock_003Ed__71))]
	private IEnumerator WaitForRemovalUnblock(string charName)
	{
		return null;
	}

	public void HandleFriendDelFeedback(A2C_FriendDelFeedback msg)
	{
	}

	public void RequestMoveToFriend(string friendName)
	{
	}

	public void HandleFriendMoveResponse(FriendMoveResultEnum result)
	{
	}

	private int FindTeleportCardSlot()
	{
		return 0;
	}

	public void HandleFriendBlockFeedback(A2C_FriendBlockFeedback msg)
	{
	}

	private static void RefreshTargetBlockButton()
	{
	}

	[IteratorStateMachine(typeof(_003CBuildFriendRows_003Ed__78))]
	private IEnumerator BuildFriendRows()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CBuildRows_003Ed__79))]
	private IEnumerator BuildRows(List<FriendClientEntry> source, GameObject parent, List<FriendRow> cache, Dictionary<string, FriendRow> map, GameObject prefabTab)
	{
		return null;
	}

	private void PartitionFriends()
	{
	}

	private void UpdateCountTexts()
	{
	}

	private void RepaintFriendRow(FriendClientEntry entry)
	{
	}

	private void SortFriends()
	{
	}

	private static FriendDisplayData BuildDisplayData(FriendClientEntry entry)
	{
		return null;
	}

	private void MarkFriendListDirty()
	{
	}

	[IteratorStateMachine(typeof(_003CRebuildFriendListWhenQuiet_003Ed__86))]
	private IEnumerator RebuildFriendListWhenQuiet()
	{
		return null;
	}

	private void ClearFriendRows()
	{
	}

	private void RefreshRequestListUI()
	{
	}

	private void UpdateRequestBadge()
	{
	}

	private int FindFriendIndex(string charName)
	{
		return 0;
	}

	private int ListedFriendCount()
	{
		return 0;
	}

	private static int FindIndex(List<FriendClientEntry> entries, string charName)
	{
		return 0;
	}

	private void ClearChildren(GameObject parent)
	{
	}

	private GameObject InstantiateUICharacterFriend(GameObject parent)
	{
		return null;
	}

	private void SetPrefabTab(GameObject activeTab)
	{
	}

	public void CheckLatestPlayerStatus()
	{
	}

	public bool IsFriend(string charName)
	{
		return false;
	}

	public bool IsBlocked(string charName)
	{
		return false;
	}

	public static string GetTargetPlayerName()
	{
		return null;
	}

	public void BlockTargetPlayer()
	{
	}

	public void TryToggleBlock(string targetName)
	{
	}

	public void SearchFriends(string keyword)
	{
	}

	public bool TrySendAddFriend(string targetName)
	{
		return false;
	}

	private static void Notify(string message, Color? color = null)
	{
	}
}
