using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Zoom : MonoBehaviour
{
	private enum TouchOrigin
	{
		Joystick = 0,
		Look = 1,
		Other = 2
	}

	private CinemachineFreeLook cineCam;

	[Header("Touch Areas")]
	public RectTransform panelRect;

	public RectTransform joystickRect;

	public Canvas map;

	[Header("Zoom Settings")]
	public float pinchZoomPerInch;

	public float pinchDeadZonePixels;

	public float maxZoomSpeedPerSecond;

	public float scrollZoomSpeed;

	public float zoomSmoothTime;

	public bool enableRotate;

	private readonly Dictionary<int, TouchOrigin> touchOrigins;

	private readonly HashSet<int> activeTouchIds;

	private readonly List<int> staleIds;

	private bool pinchInitialized;

	private int pinchFingerA;

	private int pinchFingerB;

	private float lastPinchDistance;

	private readonly float[] targetHeights;

	private readonly float[] targetRadii;

	private readonly float[] heightVel;

	private readonly float[] radiusVel;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void LateUpdate()
	{
	}

	private void SyncTouchOrigins()
	{
	}

	private TouchOrigin ClassifyTouchOrigin(Vector2 screenPoint)
	{
		return TouchOrigin.Joystick;
	}

	private void TouchControl()
	{
	}

	private bool HasSingleLookOriginTouch()
	{
		return false;
	}

	private bool TryGetTwoLookOriginTouches(out Touch first, out Touch second)
	{
		first = default;
		second = default;
		return false;
	}

	private void ProcessPinchZoom(Touch touchA, Touch touchB)
	{
	}

	private void ScrollControl()
	{
	}

	private void UpdateTargetZoom(float zoomAmount)
	{
	}

	private void ResetPinch()
	{
	}

	private bool IsInLookArea(Vector2 screenPoint)
	{
		return false;
	}

	private bool IsInJoystickArea(Vector2 screenPoint)
	{
		return false;
	}

	private Camera GetCanvasCamera(RectTransform rect)
	{
		return null;
	}

	private bool SameFingerPair(int a1, int b1, int a2, int b2)
	{
		return false;
	}
}
