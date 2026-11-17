using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MerchantItemSlotUI : MonoBehaviour
{
	[Header("UI Components")]
	public Image itemIcon;

	public Button iconButton;

	public TextMeshProUGUI itemNameText;

	public TextMeshProUGUI priceText;

	public Image selectionHighlight;

	public Button slotButton;

	public CanvasGroup canvasGroup;

	[HideInInspector]
	public NPCShopItemEntry currentShopItem;
}
