using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcItemExchangeManager : MonoBehaviour
{
	public static NpcItemExchangeManager Instance;

	[Header("UI References")]
	public TextMeshProUGUI windowTitleText;

	public ExchangeRowUI[] exchangeRows;

	[Header("Pagination")]
	public TextMeshProUGUI pageText;

	public Button nextPageButton;

	public Button prevPageButton;

	public Button closeButton;

	private NPCItemExchangeObject currentExchangeData;

	private int currentPage;

	private const int ITEMS_PER_PAGE = 5;

	private float lastExchangeTime;

	private const float EXCHANGE_COOLDOWN = 0.5f;

	private InventorySlot cachedDummySlot;

	private void Awake()
	{
	}

	public void OpenExchange(NPCItemExchangeObject exchangeData)
	{
	}

	public void RefreshPage()
	{
	}

	private void BindRowData(ExchangeRowUI rowUI, NPCItemExchangeEntry entry)
	{
	}

	private void ShowItemDetails(ItemObject masterItem)
	{
	}

	private void OnExchangeClicked(NPCItemExchangeEntry entry, bool playerHasAllRequirements)
	{
	}

	public void NextPage()
	{
	}

	public void PrevPage()
	{
	}

	public void CloseWindow()
	{
	}
}
