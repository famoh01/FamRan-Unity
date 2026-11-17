using Core.Enumerators;
using Models.Library.Shared.Game;
using UnityEngine;

public class InventoryData : MonoBehaviour
{
	public long money;

	private const int AutoPotSlotCapacity = 6;

	private readonly int[] _potionCounts;

	private readonly float[] _autoPotNextTime;

	private const float AutoPotCooldownSeconds = 0.1f;

	public void OnEnable()
	{
	}

	public InventorySlot FindItemOnInventory(Item item)
	{
		return null;
	}

	public int CountTurnItem(Item item)
	{
		return 0;
	}

	public bool OnSetActionSlot(int index, ItemObject itemObject)
	{
		return false;
	}

	public bool OnUseActionSlot(InventorySlot slot)
	{
		return false;
	}

	public bool OnUseAutoPotsActionSlot(InventorySlot slot, AutoPotSystemManager autoPotsService)
	{
		return false;
	}

	private static bool ConsumableTypeHasHpComponent(ItemConsumableTypeEnum type)
	{
		return false;
	}

	public void FixedUpdate()
	{
	}

	private void Start()
	{
	}

	public bool CharacterHaveWeapon()
	{
		return false;
	}

	public void SetUpEquipmentSlotRequirements()
	{
	}

	public bool FindInsertableSlot(SNativeID itemId, ushort turnNum, int maxPile)
	{
		return false;
	}
}
