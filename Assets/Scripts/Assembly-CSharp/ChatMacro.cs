using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatMacro : MonoBehaviour
{
	public const int CharacterLimit = 75;

	private static readonly HashSet<char> AllowedPunctuation;

	public MacroChatData buttonData;

	public TMP_Text buttonText;

	public TMP_InputField inputField;

	public TMP_InputField ChatInputField;

	private bool isBeingEdited;

	private const int MacroMaxSendsPerWindow = 5;

	private const float MacroSendWindowSeconds = 6f;

	private const float MacroMuteSeconds = 10f;

	private static readonly float[] _macroSendTimes;

	private static int _macroSendIndex;

	private static float _macroMutedUntil;

	private static float _lastMacroWarnTime;

	private static bool CheckMacroRateLimit()
	{
		return false;
	}

	private static void WarnMacroThrottled(string text)
	{
	}

	private void Awake()
	{
	}

	private void OnInputValueChanged(string value)
	{
	}

	private static string SanitizeAllowedCharacters(string s)
	{
		return null;
	}

	public static bool FailsContentValidation(string text)
	{
		return false;
	}

	public static string ClampToCharacterLimit(string text)
	{
		return null;
	}

	public void OnButtonClick()
	{
	}

	public static void SendMacroText(MacroChatData buttonData, TMP_InputField proxyInputField, TMP_InputField chatInputField)
	{
	}

	public void OnEndEdit()
	{
	}
}
