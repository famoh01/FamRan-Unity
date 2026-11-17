using System.Collections.Generic;
using UnityEngine;

public class SlotVisualManager : MonoBehaviour
{
	public const string ItemBorderName = "ItemBorder";

	public static void SetItemBorder(GameObject slotDisplay, bool hasItem)
	{
	}

	public static void ReImageSlot(GameObject slotDisplay, bool isClicked)
	{
	}

	public static void SetSlotColor(Dictionary<GameObject, InventorySlot> slotsOnInterface, GameObject obj)
	{
	}

	public static void ResetSlotColor(Dictionary<GameObject, InventorySlot> slotsOnInterface, GameObject obj)
	{
	}

	public static GameObject CreateTempItem(Dictionary<GameObject, InventorySlot> slotsOnInterface, GameObject obj, Transform tf)
	{
		return null;
	}
}
