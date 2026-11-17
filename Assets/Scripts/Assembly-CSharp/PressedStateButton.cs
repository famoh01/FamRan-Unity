using UnityEngine;
using UnityEngine.UI;

public class PressedStateButton : MonoBehaviour
{
	[SerializeField]
	private Color pressedColor;

	[SerializeField]
	private Color normalColor;

	private static PressedStateButton currentlyPressedButton;

	private Button button;

	private void Start()
	{
	}

	private void OnButtonClicked()
	{
	}

	private void SetPressedState()
	{
	}

	private void SetNormalState()
	{
	}
}
