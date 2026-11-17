using System;

[Serializable]
public class Inventory
{
	public InventorySlot[] Slots;

	public void Clear()
	{
	}

	public bool ContainsItem(ItemObject itemObject)
	{
		return false;
	}

	public bool ContainsItem(int midId, int subId)
	{
		return false;
	}
}
