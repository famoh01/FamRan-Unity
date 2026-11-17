using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InGameFaceUI : MonoBehaviour
{
	private const string MaleFacesPath = "m_player/Faces";

	private const string FemaleFacesPath = "w_player/Faces";

	[Header("Face Stepper")]
	public Button previousButton;

	public Button nextButton;

	[Tooltip("The text inside the image between the arrows.")]
	public TMP_Text selectedFaceText;

	public Button confirmButton;

	public Button closeButton;

	[Header("Optional Display Names")]
	public string[] maleFaceNames;

	public string[] femaleFaceNames;

	private Transform targetFaceParent;

	private int originalFaceIndex;

	private int previewFaceIndex;

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

	public void StepFace(int delta)
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

	private static void AddClick(Button button, UnityAction action)
	{
	}

	private static int Wrap(int value, int count)
	{
		return 0;
	}
}
