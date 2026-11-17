using UnityEngine;
using UnityEngine.UI;

public class InnerInventoryHandler : UserInterface
{
	public GameObject inventoryPrefab;

	[Header("Slot Visuals")]
	[Tooltip("The default background for an open, usable slot.")]
	public Sprite normalSlotSprite;

	[Tooltip("The image to display when the slot is locked (e.g., a padlock or red X).")]
	public Sprite lockedSlotSprite;

	[Header("Cooldown Overlay")]
	[Tooltip("Color of the cooldown overlay (radial fill). Default is dark with ~60% alpha so the icon shows through.")]
	public Color cooldownOverlayColor;

	[Tooltip("Optional sprite for the cooldown overlay. Leave null to use a solid colored fill.")]
	public Sprite cooldownOverlaySprite;

	private Image[] _cooldownOverlays;

	public static InnerInventoryHandler PlayerInventory { get; private set; }

	public static InnerInventoryHandler LockerInventory { get; private set; }

	public override void CreateSlots()
	{
	}

	private Image CreateCooldownOverlay(GameObject slotGo)
	{
		return null;
	}

	public new void FixedUpdate()
	{
	}

	private new void OnDisable()
	{
	}

	public void RefreshDisabledRows()
	{
	}
}
