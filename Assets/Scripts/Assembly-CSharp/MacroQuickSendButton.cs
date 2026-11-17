using TMPro;
using UnityEngine;

public class MacroQuickSendButton : MonoBehaviour
{
	[Tooltip("Which macro slot this bubble sends (e.g. the MacroChatData asset backing M1).")]
	public MacroChatData buttonData;

	[Tooltip("Main chat bar. Used to prefill \"@target \" if this macro's chat type is Whisper.")]
	public TMP_InputField ChatInputField;

	[Tooltip("Shared hidden TMP_InputField (one per radial menu, referenced by every bubble) used only to run the macro text through DxInCommand's emote/GM-command parser before sending. Never shown to the player.")]
	public TMP_InputField proxyInputField;

	public void OnQuickSend()
	{
	}
}
