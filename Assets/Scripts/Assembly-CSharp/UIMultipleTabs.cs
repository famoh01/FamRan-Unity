using UnityEngine;
using UnityEngine.UI;

public class UIMultipleTabs : MonoBehaviour
{
	[Header("Core References")]
	public Button[] tabsButtons;

	public Canvas[] tabsCanvases;

	[Header("Tab Tints")]
	[Tooltip("The color overlay added to the button when you click it.")]
	public Color selectedTint;

	[Tooltip("The default state.")]
	public Color defaultTint;

	[Header("Compatibility")]
	[Tooltip("Tick this when the tab Buttons use a ColorTint transition.\n\nA ColorTint Button OWNS targetGraphic.color — Selectable repaints it with its own normal / highlighted / selected colour on every state change, which silently overwrites the tint this script writes. (Deselecting a tab repaints it with normalColor, so a tab whose normalColor is already the highlight shade looks stuck on forever.)\n\nWith this on, the tint is written into the Button's ColorBlock instead, so Selectable applies OUR colour in every state and the two can't fight. Also clears the EventSystem selection so a previously clicked tab doesn't linger in its Selected state.\n\nOFF by default so panels already relying on the old direct-color behaviour are unaffected.")]
	public bool tintViaColorBlock;

	private void Start()
	{
	}

	private void SelectTab(int selectedIndex)
	{
	}

	private static void ApplyColorBlockTint(Button button, Color tint)
	{
	}

	public void ResetTabsToDefault()
	{
	}
}
