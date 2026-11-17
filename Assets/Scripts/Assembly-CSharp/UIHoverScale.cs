using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverScale : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	[Header("Scale Settings")]
	public float hoverScale;

	public float normalScale;

	public float lerpSpeed;

	private Vector3 targetScale;

	private Canvas myCanvas;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}

	private void OnDisable()
	{
	}

	private void ScaleUp()
	{
	}

	private void ResetScale()
	{
	}
}
