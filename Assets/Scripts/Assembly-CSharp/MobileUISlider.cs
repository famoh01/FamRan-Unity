using UnityEngine;
using UnityEngine.UI;

public class MobileUISlider : MonoBehaviour
{
	[Header("UI Elements")]
	public RectTransform chatBox;

	public RectTransform etcButtons;

	[Header("Toggle Button Visuals")]
	public Image toggleButtonImage;

	public Sprite openIcon;

	public Sprite closedIcon;

	[Header("Settings")]
	public float slideSpeed;

	public bool startOpen;

	private bool isOpen;

	private Vector2 chatTargetPos;

	private Vector2 buttonsTargetPos;

	private Vector2 chatOpenPos;

	private Vector2 buttonsOpenPos;

	private Vector2 chatClosedPos;

	private Vector2 buttonsClosedPos;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ToggleMenu()
	{
	}
}
