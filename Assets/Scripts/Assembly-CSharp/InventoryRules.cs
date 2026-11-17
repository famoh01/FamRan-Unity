using Core.Enumerators;

public static class InventoryRules
{
	public static readonly (ushort mainId, ushort subId)[] LockerNonTradeMoveExempt;

	public static bool CanSellToNpc(InterfaceTypeEnum source, InterfaceTypeEnum target)
	{
		return false;
	}

	public static bool CanUpgradeItem(InterfaceTypeEnum target)
	{
		return false;
	}

	public static bool CanMoveToTrade(InterfaceTypeEnum source, InterfaceTypeEnum target)
	{
		return false;
	}

	public static bool CanMoveToPotionHotkey(InterfaceTypeEnum source, InterfaceTypeEnum target, InventorySlot targetSlot, ItemObject item)
	{
		return false;
	}

	public static bool CanMoveToConsumableHotkey(InterfaceTypeEnum source, InterfaceTypeEnum target, InventorySlot targetSlot, ItemObject item)
	{
		return false;
	}

	public static bool IsConsumableQuickBarSlot(InventorySlot slot)
	{
		return false;
	}

	public static bool CanCombineCostume(InterfaceTypeEnum sourceType, InventorySlot sourceSlot, ItemObject sourceItem, InterfaceTypeEnum targetType, InventorySlot targetSlot, ItemObject targetItem)
	{
		return false;
	}

	private static bool ClassesOverlap(ItemObject a, ItemObject b)
	{
		return false;
	}

	public static bool CanRemoveCostume(InterfaceTypeEnum sourceType, ItemObject sourceItem, InterfaceTypeEnum targetType, InventorySlot targetSlot, ItemObject targetItem)
	{
		return false;
	}

	public static bool IsLockerNonTradeMoveExempt(ushort mainId, ushort subId)
	{
		return false;
	}

	public static bool IsLockerInteraction(InterfaceTypeEnum source, InterfaceTypeEnum target)
	{
		return false;
	}

	public static bool CanUseGarbageCard(InterfaceTypeEnum sourceType, ItemObject sourceItem, InterfaceTypeEnum targetType, ItemObject targetItem)
	{
		return false;
	}

	public static bool CanUsePetFoodOnCard(InterfaceTypeEnum sourceType, ItemObject sourceItem, InterfaceTypeEnum targetType, ItemObject targetItem)
	{
		return false;
	}

	public static bool CanUseNonDropCard(InterfaceTypeEnum sourceType, ItemObject sourceItem, InterfaceTypeEnum targetType, InventorySlot targetSlot, ItemObject targetItem)
	{
		return false;
	}

	public static bool CanUseWrapper(InterfaceTypeEnum sourceType, ItemObject sourceItem, InterfaceTypeEnum targetType, InventorySlot targetSlot, ItemObject targetItem)
	{
		return false;
	}
}
