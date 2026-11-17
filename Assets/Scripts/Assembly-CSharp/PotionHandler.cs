using UnityEngine;

public class PotionHandler : UserInterface
{
	public GameObject[] slots;

	public GameObject[] outerSlots;

	[Header("Default Visuals")]
	public Sprite defaultEmptySlotSprite;

	public new void OnEnable()
	{
	}

	public new void OnDisable()
	{
	}

	public override void CreateSlots()
	{
	}

	public void OnClickPotionRemove(GameObject obj)
	{
	}

	public void OnClickPotion(GameObject obj)
	{
	}

	private void SafePotionSlotUpdate(InventorySlot slot)
	{
	}
}
