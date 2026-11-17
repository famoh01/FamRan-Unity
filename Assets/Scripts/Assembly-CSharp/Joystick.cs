using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler, IPointerUpHandler
{
	[SerializeField]
	private float handleRange;

	[SerializeField]
	private float deadZone;

	[SerializeField]
	private AxisOptions axisOptions;

	[SerializeField]
	private bool snapX;

	[SerializeField]
	private bool snapY;

	[SerializeField]
	protected RectTransform background;

	[SerializeField]
	private RectTransform handle;

	private RectTransform baseRect;

	private Canvas canvas;

	private Camera cam;

	private Vector2 input;

	private int joystickTouchId;

	private bool inputSuppressed;

	private CustomizableUI customizableUI;

	public float Horizontal => 0f;

	public float Vertical => 0f;

	public Vector2 Direction => default;

	public float HandleRange
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	public float DeadZone
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	public AxisOptions AxisOptions
	{
		get
		{
			return AxisOptions.Both;
		}
		set
		{
		}
	}

	public bool SnapX
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public bool SnapY
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	protected virtual void Start()
	{
	}

	private bool BlockedByEditMode()
	{
		return false;
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnDrag(PointerEventData eventData)
	{
	}

	public virtual void OnPointerUp(PointerEventData eventData)
	{
	}

	protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
	{
	}

	private void FormatInput()
	{
	}

	private float SnapFloat(float value, AxisOptions snapAxis)
	{
		return 0f;
	}

	public void SetInputSuppressed(bool suppressed)
	{
	}

	public void ResetJoystickPosition()
	{
	}

	protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
	{
		return default;
	}
}
