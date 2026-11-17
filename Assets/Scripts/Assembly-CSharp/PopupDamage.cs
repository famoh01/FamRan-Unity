using System;
using Core.Enumerators;
using UnityEngine;
using UnityEngine.UI;

public class PopupDamage : MonoBehaviour
{
	private enum Phase
	{
		Hold = 0,
		Dim = 1,
		Rise = 2,
		TopHold = 3
	}

	public const int MaxDigits = 5;

	[Header("Setup")]
	public Canvas canvas;

	public CanvasGroup textCanvasGroup;

	[Tooltip("Left-to-right digit Image slots. Length must be >= 5 (C++ nMAXDIGIT).")]
	public Image[] digitImages;

	public Image criticalImage;

	public Image crushingImage;

	public Image missImage;

	[Header("Motion (mirrors C++ CDamageDisplay)")]
	[Tooltip("Phase 1: seconds parked at the target at full opacity so the player can read it.")]
	public float movingDelay;

	[Tooltip("Phase 2: seconds spent easing alpha down to riseAlpha before the launch.")]
	public float fadeTime;

	[Tooltip("Opacity held from the start of the rise until removal. Never fades below this — the number is still readable at the top when it vanishes.")]
	[Range(0f, 1f)]
	public float riseAlpha;

	[Tooltip("Phase 3: seconds to travel from the target to the top of the screen. This is a snap — keep it very small.")]
	public float riseTime;

	[Tooltip("Phase 4: seconds parked at the top of the screen before the popup is removed. Alpha eases riseAlpha -> 0 across this window, so keep it short — this is the clear-out, not a read window.")]
	public float topHoldTime;

	[Tooltip("Initial velocity term of the rise curve. Only its ratio to accel matters when screenRelativeRise is ON (the curve is normalized to riseTime).")]
	public float velocity;

	[Tooltip("Acceleration term of the rise curve. At snap speeds keep this low relative to velocity so the travel reads as near-linear rather than easing in.")]
	public float accel;

	[Header("Screen-relative rise (RAN-style: target -> top of screen)")]
	[Tooltip("ON: rise a screen-measured distance so the popup ends near the top of the viewport, like the original RAN. OFF: legacy raw velocity/accel world translate.")]
	public bool screenRelativeRise;

	[Tooltip("Viewport Y the popup's TOP EDGE lands on. 1 = flush against the actual top of the screen; lower it only if you want breathing room under a top-anchored HUD.")]
	[Range(0f, 1f)]
	public float topViewportY;

	[Tooltip("Floor on travel distance, as a fraction of screen height. Keeps popups spawned already near the top of the screen from standing still.")]
	[Range(0f, 1f)]
	public float minRiseFraction;

	[NonSerialized]
	public bool _inPool;

	private Phase _phase;

	private float _phaseTime;

	private float _animVel;

	private bool _animActive;

	private Camera _riseCamera;

	private Transform _camTf;

	private Vector3 _riseStart;

	private Vector3 _riseDir;

	private float _riseDistance;

	private float _targetViewportY;

	private bool _riseResolved;

	private float _profileScale;

	private Billboard _billboard;

	private bool _billboardLookedUp;

	private float _cachedFov;

	private float _fovHeightFactor;

	private Transform _tf;

	private void Awake()
	{
	}

	public void Configure(int damage, DamageTypeEnum flag, bool bAttack, Sprite[] redDigits, Sprite[] blueDigits, Sprite criticalSprite, Sprite crushingSprite, Sprite missSprite)
	{
	}

	private void AssignDigitsZeroAlloc(int damage, Sprite[] palette)
	{
	}

	private void ResetRenderData()
	{
	}

	private void OnEnable()
	{
	}

	private void SetupRise()
	{
	}

	private float WorldScreenHeightAt(Vector3 worldPos)
	{
		return 0f;
	}

	private bool TryGetGapToTop(Vector3 worldPos, out float gap, out float worldScreenHeight)
	{
		gap = default;
		worldScreenHeight = default;
		return false;
	}

	private float WorldHalfHeight()
	{
		return 0f;
	}

	private void PinToTop()
	{
	}

	private Camera ResolveCamera()
	{
		return null;
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	private void EnterPhase(Phase next, float previousDuration)
	{
	}

	private void SetAlpha(float a)
	{
	}

	private void Despawn()
	{
	}
}
