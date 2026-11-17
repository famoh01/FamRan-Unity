using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InGameHairStyleUI : MonoBehaviour
{
	private const string MaleHairsPath = "m_player/Hairs";

	private const string FemaleHairsPath = "w_player/Hairs";

	[Header("Hair Style Stepper")]
	public Button previousButton;

	public Button nextButton;

	[Tooltip("The text inside the image between the arrows.")]
	public TMP_Text selectedHairStyleText;

	public Button confirmButton;

	public Button closeButton;

	[Header("Optional Display Names")]
	[Tooltip("Independent from character creation; this array may cover every in-game male hairstyle.")]
	public string[] maleHairStyleNames;

	[Tooltip("Independent from character creation; this array may cover every in-game female hairstyle.")]
	public string[] femaleHairStyleNames;

	[Header("Network Data")]
	[HideInInspector]
	public int cardSlotIndex;

	private Transform targetHairParent;

	private int originalHairIndex;

	private int previewHairIndex;

	private bool isMale;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void InitializeForLocalPlayer()
	{
	}

	public void StepHairStyle(int delta)
	{
	}

	private void RefreshStepper()
	{
	}

	private string ResolveDisplayName(int index)
	{
		return null;
	}

	public void ConfirmChange()
	{
	}

	public void CancelAndClose()
	{
	}

	private static void SetOnlyChildActive(Transform parent, int index)
	{
	}

	private static int GetActiveChildIndex(Transform parent)
	{
		return 0;
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
