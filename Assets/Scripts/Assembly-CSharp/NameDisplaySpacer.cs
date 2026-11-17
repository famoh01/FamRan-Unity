using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class NameDisplaySpacer : MonoBehaviour
{
	public TextMeshProUGUI targetText;

	[Header("UI Settings")]
	[Tooltip("Adds extra length to both sides of the name display when selected.")]
	public int extraLength;

	private HorizontalLayoutGroup layoutGroup;

	private bool isSelected;

	private void Awake()
	{
	}

	public void SetSelected(bool selected)
	{
	}

	public void UpdatePadding()
	{
	}

	private int CalculatePadding(int charCount)
	{
		return 0;
	}
}
