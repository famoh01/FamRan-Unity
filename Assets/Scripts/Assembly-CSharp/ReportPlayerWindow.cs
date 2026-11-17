using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReportPlayerWindow : MonoBehaviour
{
	[Serializable]
	public class CategoryOption
	{
		[Tooltip("The chip itself.")]
		public Button _button;

		[Tooltip("What tapping it means.")]
		public ReportCategory _category;

		[Tooltip("Optional — turned on while this chip is the chosen one.")]
		public GameObject _selectedIndicator;

		[Tooltip("Optional — swapped between the two colours below.")]
		public Image _background;
	}

	public static ReportPlayerWindow Instance;

	[Header("Window")]
	[Tooltip("The window's Canvas. Shown and hidden by enabling it, like every other window here. Left empty, a Canvas on this object — or the nearest one above it — is used.")]
	public Canvas _root;

	private GraphicRaycaster _rootRaycaster;

	[Header("References")]
	[Tooltip("Read-only label showing who is being reported.")]
	public TextMeshProUGUI _targetNameText;

	[Tooltip("Optional free text. Never required — the category carries the report.")]
	public TMP_InputField _detailsInput;

	[Tooltip("Optional — shows how much of the note is used, as \"used / limit\". Repainted on every keystroke. Leave empty and the limit still holds, the player just cannot see it.")]
	public TextMeshProUGUI _detailsCountText;

	[Tooltip("Sends the report. Stays disabled until a reason is chosen.")]
	public Button _sendButton;

	[Tooltip("Closes without sending.")]
	public Button _cancelButton;

	[Tooltip("Optional — shows why a send failed, in place.")]
	public TextMeshProUGUI _statusText;

	[Header("Reasons")]
	public List<CategoryOption> _categories;

	[Tooltip("Chip background while chosen.")]
	public Color _selectedColor;

	[Tooltip("Chip background while not chosen.")]
	public Color _unselectedColor;

	[Header("Text")]
	public string _sendingText;

	public string _sentText;

	public string _pickReasonText;

	public string _noTargetText;

	public const int MaxDetailsLength = 200;

	[Header("Limits")]
	[Tooltip("Seconds before another report may be sent, so a mistap cannot double-file. The API also refuses a repeat against the same player within ten minutes.")]
	public float _cooldownSeconds;

	private static float s_nextAllowedSendTime;

	private string _targetName;

	private ReportCategory? _chosenCategory;

	private bool _sending;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void Open(string targetName)
	{
	}

	public void Close()
	{
	}

	private void SetVisible(bool visible)
	{
	}

	private void ChooseCategory(ReportCategory category)
	{
	}

	private void OnSendClicked()
	{
	}

	private void RepaintCategories()
	{
	}

	private void OnDetailsChanged(string _)
	{
	}

	private void RepaintDetailsCount()
	{
	}

	private void RepaintSendButton()
	{
	}

	private void SetStatus(string message)
	{
	}

	private static void Confirm(string message)
	{
	}

	private static void Notify(string message)
	{
	}
}
