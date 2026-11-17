using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FriendPanelNavigation : MonoBehaviour
{
	private enum Tab
	{
		Friends = 0,
		Requests = 1,
		Blocked = 2
	}

	[Header("Panel")]
	[Tooltip("_FriendsCanvas — the Canvas enabled/disabled to show or hide the panel.")]
	public Canvas _panelCanvas;

	[Tooltip("Exit button on FriendsBG.")]
	public Button _exitButton;

	[Tooltip("Optional: the HUD button that opens the panel (_ETCButtons/Controller/FriendsBtn).")]
	public Button _openButton;

	[Header("Tabs")]
	public Button _friendListTabButton;

	public Button _requestsTabButton;

	[Tooltip("Optional third tab. Leave the button and canvas empty and the panel keeps its two tabs, with blocked characters listed among the friends.")]
	public Button _blockedTabButton;

	public Canvas _friendListCanvas;

	public Canvas _requestsCanvas;

	public Canvas _blockedCanvas;

	[Tooltip("Colour of the tab you're on.")]
	public Color _selectedTint;

	[Tooltip("Colour of the tab you're not on.")]
	public Color _defaultTint;

	[Header("Search / Add")]
	[Tooltip("The search box. Typing filters the friend list live.")]
	public TMP_InputField _searchBar;

	[Tooltip("The button beside the box — sends a friend request to the typed name.")]
	public Button _addFriendButton;

	[Tooltip("Clear the box after a request is sent.")]
	public bool _clearSearchAfterAdd;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private static void Bind(Button button, UnityAction action)
	{
	}

	public void Open()
	{
	}

	public void OpenOnRequests()
	{
	}

	public void Close()
	{
	}

	public void ShowFriendList()
	{
	}

	public void ShowRequests()
	{
	}

	public void ShowBlocked()
	{
	}

	private void ApplyTab(Tab tab)
	{
	}

	private void Tint(Button button, bool selected)
	{
	}

	public void AddTypedFriend()
	{
	}

	private void ClearSearch()
	{
	}
}
