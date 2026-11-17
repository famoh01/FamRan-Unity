using System;
using Models.Network.Shared.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class BotChallengeDialog : MonoBehaviour
{
	private static BotChallengeDialog _instance;

	private const int DigitCount = 10;

	private const float SubmitCooldownSec = 0.75f;

	private static readonly string[] DigitTexts;

	[Header("Dialog")]
	[SerializeField]
	private Canvas _dialogRoot;

	[SerializeField]
	private RawImage _image;

	[Header("Text")]
	[SerializeField]
	private TMP_Text _title;

	[SerializeField]
	private TMP_Text _prompt;

	[SerializeField]
	private TMP_Text _attempts;

	[SerializeField]
	private TMP_Text _status;

	[Header("Input")]
	[SerializeField]
	private TMP_InputField _input;

	[Tooltip("Assign exactly 10 buttons. Their positions stay fixed, but their digits are shuffled each time the dialog opens.")]
	[SerializeField]
	private Button[] _digitButtons;

	[SerializeField]
	private Button _clear;

	[SerializeField]
	private Button _submit;

	[SerializeField]
	private TMP_Text _submitLabel;

	[Header("Timer")]
	[SerializeField]
	private Image _timer;

	private Texture2D _texture;

	private uint _challengeId;

	private float _duration;

	private float _remaining;

	private float _submitCooldown;

	private bool _closing;

	private Action<uint, string> _send;

	private readonly int[] _digitValues;

	private readonly TMP_Text[] _digitLabels;

	private readonly UnityAction[] _digitClickActions;

	private const float VerdictGraceSec = 10f;

	private float _graceElapsed;

	public static bool IsOpen => false;

	private bool IsDialogOpen => false;

	public static void Show(uint challengeId, byte[] png, byte timeoutSec, byte attemptsLeft, Action<uint, string> send)
	{
	}

	public static void OnResult(uint challengeId, BotChallengeResult result, byte attemptsLeft)
	{
	}

	public static void Dismiss()
	{
	}

	private void Awake()
	{
	}

	private bool HasRequiredReferences()
	{
		return false;
	}

	private void Open(uint challengeId, byte[] png, byte timeoutSec, byte attemptsLeft, Action<uint, string> send)
	{
	}

	private void Update()
	{
	}

	private void Submit()
	{
	}

	private void ApplyResult(BotChallengeResult result, byte attemptsLeft)
	{
	}

	private void Close()
	{
	}

	private void OnDestroy()
	{
	}

	private void ReleaseTexture()
	{
	}

	private void UpdateTimerFill()
	{
	}

	private void SetStatus(string message)
	{
	}

	private void WireKeypad()
	{
	}

	private void UnwireKeypad()
	{
	}

	private bool HasValidKeypadReferences()
	{
		return false;
	}

	private void ShuffleKeypad()
	{
	}

	private void AppendDigit(int buttonIndex)
	{
	}

	private void ClearInput()
	{
	}

	private static string DigitsOnly(string s)
	{
		return null;
	}
}
