using TMPro;
using UnityEngine;

public class ItemBoxUI : UserInterface
{
	[Header("Item Box UI Setup")]
	[Tooltip("Drag the actual ItemBoxCanvas here")]
	public Canvas itemBoxCanvas;

	[Tooltip("Drag your Slot Prefab here")]
	public GameObject slotPrefab;

	[Tooltip("Drag the 'ItemsContainer' GameObject here")]
	public Transform itemsContainer;

	[Tooltip("Drag the 'ItemBoxName' Text (TMP) GameObject here")]
	public TextMeshProUGUI itemBoxNameText;

	private int currentBoxSlotIndex;

	public override void CreateSlots()
	{
	}

	public void OpenItemBox(ItemObject itemBoxObject, int sourceSlotIndex)
	{
	}

	public void OnUnpackButtonClicked()
	{
	}

	public void CloseUI()
	{
	}
}
