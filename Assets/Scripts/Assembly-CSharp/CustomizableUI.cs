using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomizableUI : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
	public static CustomizableUI Instance;

	public static bool IsEditMode;

	public static CustomizableUI SelectedUI;

	public static Action<CustomizableUI> OnUISelected;

	[Header("Persistence")]
	[SerializeField]
	[Tooltip("Optional stable key used for saved layouts. Set this when multiple customizable objects share the same GameObject name.")]
	private string persistenceId;

	private RectTransform rectTransform;

	private Canvas canvas;

	private Selectable selectable;

	private CanvasGroup canvasGroup;

	private EventTrigger eventTrigger;

	private MonoBehaviour[] nestedDragControls;

	private bool[] nestedDragControlEnabledStates;

	private bool[] nestedDragControlStatesCaptured;

	private Vector2 defaultPosition;

	private Vector2 lastSavedPosition;

	private Vector3 originalScale;

	private Vector3 lastSavedScale;

	private List<EventTrigger.Entry> _storedTriggers;

	private GameObject highlightFrame;

	private Canvas highlightCanvas;

	private Color originalDisabledColor;

	private bool disabledColorCaptured;

	private bool wasInEditMode;

	private bool isInitialized;

	private List<EventTrigger.Entry> storedTriggers => null;

	private void Awake()
	{
	}

	public void InitializeIfNeeded()
	{
	}

	private void Update()
	{
	}

	private void CacheNestedDragControls()
	{
	}

	private bool IsOwnedNestedDragControl(MonoBehaviour behaviour)
	{
		return false;
	}

	private void SuspendNestedDragControls()
	{
	}

	private void RestoreNestedDragControls()
	{
	}

	public void Highlight(bool state)
	{
	}

	public void OnPointerClick(PointerEventData eventData)
	{
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
	}

	public void OnDrag(PointerEventData eventData)
	{
	}

	public void OnEndDrag(PointerEventData eventData)
	{
	}

	public void ApplyScale(float multiplier)
	{
	}

	public float GetCurrentScaleMultiplier()
	{
		return 0f;
	}

	public void LoadPosition(int preset)
	{
	}

	public void SavePosition(int preset)
	{
	}

	public void RevertPosition()
	{
	}

	public void ResetToDefault()
	{
	}

	public void SetOpacity(float alpha)
	{
	}

	public float GetOpacity()
	{
		return 0f;
	}

	private string GetKeyPrefix(int preset)
	{
		return null;
	}
}
