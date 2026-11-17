using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditClubNotice : MonoBehaviour
{
	[Header("UI References")]
	[SerializeField]
	private TMP_Text readOnlyText;

	[SerializeField]
	private TMP_InputField editField;

	[SerializeField]
	private ScrollRect scrollRect;

	[Space]
	[SerializeField]
	private Button editButton;

	[SerializeField]
	private Button saveButton;

	[SerializeField]
	private Button cancelButton;

	private string originalText;

	private void Awake()
	{
	}

	public void OnEditButton()
	{
	}

	public void OnSaveButton()
	{
	}

	public void OnCancelButton()
	{
	}

	public void OnKeyboardDone(string _)
	{
	}

	private void OnEditPreviewChanged(string newText)
	{
	}

	private void ToggleEditingUI(bool isEditing)
	{
	}
}
