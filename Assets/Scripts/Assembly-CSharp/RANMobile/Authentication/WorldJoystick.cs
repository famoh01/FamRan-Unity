using UnityEngine;
using UnityEngine.EventSystems;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Virtual joystick (bottom-left). Writes a normalized vector into
	/// WorldHUD.MoveAxis, which WorldPlayerMover merges with WASD input.
	/// </summary>
	public class WorldJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
	{
		public RectTransform BaseRect;

		public RectTransform KnobRect;

		public float Radius = 70f;

		public void OnPointerDown(PointerEventData eventData)
		{
			UpdateAxis(eventData);
		}

		public void OnDrag(PointerEventData eventData)
		{
			UpdateAxis(eventData);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			WorldHUD.MoveAxis = Vector2.zero;
			if (KnobRect != null)
			{
				KnobRect.anchoredPosition = Vector2.zero;
			}
		}

		private void UpdateAxis(PointerEventData eventData)
		{
			if (BaseRect == null)
			{
				return;
			}
			Vector2 local;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(BaseRect, eventData.position, eventData.pressEventCamera, out local))
			{
				return;
			}
			Vector2 axis = (local - BaseRect.rect.center) / Radius;
			if (axis.magnitude > 1f)
			{
				axis.Normalize();
			}
			WorldHUD.MoveAxis = axis;
			if (KnobRect != null)
			{
				KnobRect.anchoredPosition = axis * Radius;
			}
		}
	}
}
