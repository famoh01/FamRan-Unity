using UnityEngine;

public class FriendList : MonoBehaviour
{
	public GameObject _chatBtn;

	public GameObject _moveBtn;

	public GameObject _deleteBtn;

	public GameObject _blockBtn;

	public GameObject _onlineStatus;

	[Header("Status Icons")]
	[Tooltip("Sprite to show when the friend is online")]
	public Sprite onlineSprite;

	[Tooltip("Sprite to show when the friend is offline")]
	public Sprite offlineSprite;

	[Header("Assign block-un.png sprite on prefab")]
	public Sprite _unblockSprite;

	private Sprite _blockSprite;

	private bool _blockSpriteCaptured;

	public void SetupFriend(string charName, FriendDisplayData data, GameObject characterFriend)
	{
	}

	public void SetPlayerOnlineStatus(bool isOnline, int? channel)
	{
	}

	public void SetMoveEnable(bool isOnline)
	{
	}

	private void Chat(string name)
	{
	}

	private void Block(string charName)
	{
	}

	private void Unblock(string charName)
	{
	}
}
