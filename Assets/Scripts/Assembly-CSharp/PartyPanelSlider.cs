using UnityEngine;

public class PartyPanelSlider : MonoBehaviour
{
	[Header("UI Elements")]
	public RectTransform partyPanel;

	public GameObject showButton;

	[Header("Settings")]
	public float slideSpeed;

	public bool startOpen;

	[Tooltip("Add extra pixels to push the panel completely off-screen.")]
	public float extraHideOffset;

	[Header("Mobile Safe Area")]
	[Tooltip("Automatically shifts the panel to the right to avoid notches when the game starts.")]
	public bool avoidNotchOnStart;

	[Header("Swipe Settings")]
	public float swipeThreshold;

	private bool isOpen;

	private bool isAnimating;

	private Vector2 targetPos;

	private Vector2 openPos;

	private Vector2 closedPos;

	private bool isSwiping;

	private Vector2 pointerDownPosition;

	private Vector2 pointerUpPosition;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void Update()
	{
	}

	private void ApplySafeArea()
	{
	}

	private void CalculatePositions()
	{
	}

	public void HidePanel()
	{
	}

	public void ShowPanel()
	{
	}

	private void DetectSwipeInput()
	{
	}

	private void EvaluateSwipe()
	{
	}
}
