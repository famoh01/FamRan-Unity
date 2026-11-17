using UnityEngine;
using UnityEngine.EventSystems;

public class UIDraggablePanel : MonoBehaviour, IDragHandler, IEventSystemHandler, IBeginDragHandler
{
	[Header("Drag Settings")]
	[Tooltip("Drag the main parent panel here. If left empty, it moves itself.")]
	public RectTransform panelToDrag;

	[Header("Mobile Safety")]
	[Tooltip("If true, prevents the player from dragging the panel off the screen.")]
	public bool keepOnScreen;

	private Canvas canvas;

	private RectTransform canvasRect;

	private CustomizableUI customizable;

	private void Awake()
	{
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
	}

	public void OnDrag(PointerEventData eventData)
	{
	}

	private void ClampToScreen()
	{
	}
}
