using UnityEngine;
using UnityEngine.EventSystems;

public class TouchField : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	[HideInInspector]
	public Vector2 TouchDist;

	[HideInInspector]
	public Vector2 PointerOld;

	[HideInInspector]
	protected int PointerId;

	[HideInInspector]
	public bool Pressed;

	private void Update()
	{
	}

	public void PointerValidation()
	{
	}

	private static bool TryGetTouchByFingerId(int fingerId, out Touch found)
	{
		found = default;
		return false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}
}
