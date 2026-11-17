using System;
using Core.Enumerators;
using RAN.Mobile.GameModels.Item;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Container", menuName = "Scriptable Objects/Containers/New Inventory Container")]
public class InventoryObject : ScriptableObject
{
	public string savePath;

	public InterfaceTypeEnum type;

	public Inventory Container;

	public InventorySlot[] GetSlots => null;

	public int EmptySlotCount => 0;

	public string Rosary => null;

	public void Assign(InventoryObject source)
	{
	}

	public ItemObject GetItemObject(int midId, int subId)
	{
		return null;
	}

	public bool AddItem(Item item, int amount)
	{
		return false;
	}

	public InventorySlot FindItemOnInventory(Item item)
	{
		return null;
	}

	public bool IsItemInInventory(ItemObject item)
	{
		return false;
	}

	public void UpdateEquippedTurnNum(int index, ushort stack)
	{
	}

	public InventorySlot GetEmptySlot()
	{
		return null;
	}

	public bool IsFull()
	{
		return false;
	}

	public void SetActionSlots(InventorySlot item1, InventorySlot item2)
	{
	}

	public void SetConsumableSlots(InventorySlot item1, InventorySlot item2)
	{
	}

	public void LockerStorageSwapItems(InventorySlot item1, InventorySlot item2)
	{
	}

	public void ReqDisguise(InventorySlot item1, InventorySlot item2)
	{
	}

	public void SwapItems(InventorySlot item1, InventorySlot item2)
	{
	}

	private bool CheckItemRequirement(InventorySlot item, ItemObject itemObject)
	{
		return false;
	}

	public bool LockerStorageCombineItem(InventorySlot item1, InventorySlot item2)
	{
		return false;
	}

	public bool CombineItem(InventorySlot item1, InventorySlot item2)
	{
		return false;
	}

	public void DropItem(Item item, int index)
	{
	}

	public void SplitItem(int index, int splitCount, InterfaceTypeEnum type)
	{
	}

	private Vector3 GetRandomGroundPosition(Vector3 center, float range, LayerMask layerMask)
	{
		return default;
	}

	[ContextMenu("Save")]
	public void Save()
	{
	}

	[ContextMenu("Load")]
	public void Load()
	{
	}

	[ContextMenu("Load")]
	public void LoadByte(CrowSaleObject crowSale)
	{
	}

	[ContextMenu("Clear")]
	public void Clear()
	{
	}

	public void GetItems(InventorySlot item1, InventorySlot item2)
	{
	}

	public void ClearInventory()
	{
	}

	public InventorySlot[] FindItemsOnInventory(int mainId, int subId, bool filterNotExpired = false)
	{
		return null;
	}

	public InventorySlot[] FindItemsOnInventoryByName(string name, InterfaceTypeEnum? _type = null)
	{
		return null;
	}

	public bool IsItemExpired(DateTime bornDateTime, ItemExpiration itemExpiration)
	{
		return false;
	}

	public bool HasItemOfType(ItemTypeEnum type)
	{
		return false;
	}

	public InventorySlot FindFirstSlotOfType(ItemTypeEnum type)
	{
		return null;
	}

	public bool HasValidRosary()
	{
		return false;
	}
}
