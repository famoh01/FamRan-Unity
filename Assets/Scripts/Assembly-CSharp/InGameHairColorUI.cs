using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InGameHairColorUI : MonoBehaviour
{
	private const string MaleHairsPath = "m_player/Hairs";

	private const string FemaleHairsPath = "w_player/Hairs";

	private static readonly Color[] HairColors;

	[Header("Color Stepper")]
	public Button previousButton;

	public Button nextButton;

	[Tooltip("The image between the arrows. It is tinted to the selected hair color.")]
	public Image selectedColorImage;

	public Button confirmButton;

	public Button closeButton;

	[Header("Network Data")]
	[HideInInspector]
	public int cardSlotIndex;

	private Transform targetHairParent;

	private int originalColorIndex;

	private int previewColorIndex;

	private readonly List<Material> cachedHairMaterials;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void InitializeForLocalPlayer()
	{
	}

	public void StepHairColor(int delta)
	{
	}

	private void ApplyPreview()
	{
	}

	public void ConfirmChange()
	{
	}

	public void CancelAndClose()
	{
	}

	private static void CloseWindow()
	{
	}

	private static void AddClick(Button button, UnityAction action)
	{
	}

	private static int Wrap(int value, int count)
	{
		return 0;
	}
}
