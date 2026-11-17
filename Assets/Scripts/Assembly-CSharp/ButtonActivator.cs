using Core.Enumerators;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonActivator : MonoBehaviour, IUpdateSelectedHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler
{
	public static ButtonActivator instance;

	public ChatTypeEnum chatType;

	public Button chatTypeRight;

	public Button chatTypeLeft;

	public TextMeshProUGUI chatTypePlaceHolder;

	private bool isPressed;

	[SerializeField]
	private GameObject _quickEmote;

	private int chatTypeEnum;

	private void Awake()
	{
	}

	public void ChatRightButtonChanged()
	{
	}

	public void ChatLeftButtonChanged()
	{
	}

	private void UpdateChatContainer()
	{
	}

	private string GetChatDisplayName(ChatTypeEnum type)
	{
		return null;
	}

	public void OnUpdateSelected(BaseEventData data)
	{
	}

	public void OnPointerDown(PointerEventData data)
	{
	}

	public void OnPointerUp(PointerEventData data)
	{
	}

	public void SetChatSection(int chatIndex)
	{
	}

	public void SetWhisperSection()
	{
	}

	public void OnMouseDrag()
	{
	}
}
