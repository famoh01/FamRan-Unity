using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ChatEntryClickHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	private TextMeshProUGUI _chatText;

	private void Awake()
	{
	}

	public void OnPointerClick(PointerEventData eventData)
	{
	}

	private static string ParseIGN(string text)
	{
		return null;
	}
}
