using System;
using Core.Enumerators;
using UnityEngine;

[Serializable]
public class InventorySlot
{
	public ItemTypeEnum[] AllowedItems;

	public WearPositionEnum[] ItemPosition;

	public ItemAttackType[] ItemAttackType;

	[NonSerialized]
	public UserInterface parent;

	[NonSerialized]
	public GameObject slotDisplay;

	[NonSerialized]
	public GameObject outerSlotDisplay;

	[NonSerialized]
	public InventoryData inventoryData;

	[NonSerialized]
	public NpcInteractControl npcInteractControl;

	[NonSerialized]
	public ConsumableQuickSlotData consumableQuickSlotData;

	[NonSerialized]
	public Action<InventorySlot> onAfterUpdated;

	[NonSerialized]
	public Action<InventorySlot> onBeforeUpdated;

	[NonSerialized]
	public int index;

	[NonSerialized]
	public byte referenceIndex;

	public Item item;

	public int amount;

	public ItemObject GetItemObject()
	{
		return null;
	}

	public void SetReference(byte reference)
	{
	}

	public byte GetReference()
	{
		return 0;
	}

	public InventorySlot()
	{
	}

	public InventorySlot(Item item, int amount)
	{
	}

	public void RemoveItem()
	{
	}

	public void DropItem()
	{
	}

	public void AddAmount(int value)
	{
	}

	public void UpdateSlot(Item itemValue, int amountValue, byte reference = 255)
	{
	}

	public void UpdateSlot(Item itemValue, int amountValue, int index)
	{
	}

	public void UpdateStack(int amountValue)
	{
	}

	public void UpdateTurnNum(int amount)
	{
	}

	public bool SetPotionSlot(int index, ItemObject itemObject)
	{
		return false;
	}

	public void UseEtc(InventorySlot slot)
	{
	}

	public bool SetConsumableSlot(int index, ItemObject itemObject)
	{
		return false;
	}

	public void UseConsumable(InventorySlot slot)
	{
	}

	public void UseNpcSell(InventorySlot slot)
	{
	}

	public void UseScroll(InventorySlot slot)
	{
	}

	public void UseNpcBuy(InventorySlot slot, ushort amount)
	{
	}

	public bool CanPlaceInSlot(ItemObject itemObject)
	{
		return false;
	}
}
