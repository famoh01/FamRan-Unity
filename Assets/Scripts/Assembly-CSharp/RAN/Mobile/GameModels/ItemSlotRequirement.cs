using Core.Enumerators;

namespace RAN.Mobile.GameModels
{
	public class ItemSlotRequirement
	{
		public ItemTypeEnum[] AllowedItems { get; set; }

		public WearPositionEnum[] ItemPositions { get; set; }

		public ItemAttackType[] ItemAttackTypes { get; set; }
	}
}
