using UnityEngine;
using UnityEngine.EventSystems;

public class UIZoomImage : MonoBehaviour, IScrollHandler, IEventSystemHandler
{
	private Vector3 initialScale;

	[SerializeField]
	private float zoomSpeed;

	[SerializeField]
	private float maxZoom;

	private void Start()
	{
	}

	private void Awake()
	{
	}

	public void OnScroll(PointerEventData eventData)
	{
	}

	private Vector3 ClampDesiredScale(Vector3 desiredScale)
	{
		return default;
	}
}
