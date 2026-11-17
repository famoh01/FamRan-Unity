using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableHotkeyHandler : UserInterface
{
	public GameObject[] slots;

	public GameObject[] outerSlots;

	[Header("Default Visuals")]
	public Sprite defaultEmptySlotSprite;

	[Header("Slot Number Guide")]
	[Tooltip("Name of the extra label in the slot prefab that shows the slot's number (Slot Prefab -> Image, Text, <this>). Looked up by name on both the inner and the outer slot; when no child matches, the LAST TextMeshProUGUI child is used instead.")]
	public string slotNumberLabelName;

	[Tooltip("Write 1..N into the number labels when the bar is built, so inner and outer always agree. Turn off to keep whatever text the prefab already has.")]
	public bool autoNumberSlots;

	[Header("Cooldown Overlay")]
	[Tooltip("Color of the cooldown overlay (radial fill). Default is dark with ~60% alpha so the icon shows through.")]
	public Color cooldownOverlayColor;

	[Tooltip("Optional sprite for the cooldown overlay. Leave null to use a solid colored fill.")]
	public Sprite cooldownOverlaySprite;

	private Image[] _cooldownOverlays;

	private TextMeshProUGUI[] _innerNumberLabels;

	private TextMeshProUGUI[] _outerNumberLabels;

	public new void OnEnable()
	{
	}

	public new void OnDisable()
	{
	}

	public override void CreateSlots()
	{
	}

	private TextMeshProUGUI FindNumberLabel(GameObject slotGo)
	{
		return null;
	}

	private void RefreshNumberLabels(int index, bool hasItem)
	{
	}

	public new void FixedUpdate()
	{
	}

	public void OnClickConsumable(GameObject obj)
	{
	}

	public void OnClickConsumableRemove(GameObject obj)
	{
	}

	private void SafeConsumableSlotUpdate(InventorySlot slot)
	{
	}
}
