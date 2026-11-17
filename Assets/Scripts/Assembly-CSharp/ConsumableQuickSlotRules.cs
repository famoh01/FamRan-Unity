using Core.Enumerators;

public static class ConsumableQuickSlotRules
{
	public const int MaxConsumableSlot = 6;

	private static readonly ItemTypeEnum[] Allowed;

	public static int GetUnlockedSlotCount()
	{
		return 0;
	}

	public static bool IsEligible(ItemObject item)
	{
		return false;
	}
}
