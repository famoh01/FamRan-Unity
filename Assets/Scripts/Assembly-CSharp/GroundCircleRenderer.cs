using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GroundCircleRenderer : MonoBehaviour
{
	public enum Highlight
	{
		None = 0,
		Direct = 1,
		Assisted = 2
	}

	[Header("Shape")]
	[SerializeField]
	private int segments;

	[SerializeField]
	private float radius;

	[SerializeField]
	private float yOffset;

	[SerializeField]
	private Color color;

	[SerializeField]
	private float width;

	[Header("Reticle feel")]
	[SerializeField]
	private bool showCenterDot;

	[SerializeField]
	private bool showSweep;

	[SerializeField]
	private bool showGlow;

	[SerializeField]
	private float glowWidthScale;

	[Range(0f, 1f)]
	[SerializeField]
	private float glowAlpha;

	[SerializeField]
	private float pulseSpeed;

	[Range(0f, 0.9f)]
	[SerializeField]
	private float pulseDepth;

	[SerializeField]
	private float sweepSpeedDeg;

	[SerializeField]
	private float sweepArcDeg;

	[Header("Target feedback")]
	[SerializeField]
	private Color directColor;

	[SerializeField]
	private Color assistedColor;

	[SerializeField]
	private float highlightLerp;

	private Color _baseColor;

	private Color _wantColor;

	private LineRenderer _ring;

	private LineRenderer _dot;

	private LineRenderer _sweep;

	private LineRenderer _glow;

	private static Material _sharedMat;

	private static Material _sharedGlowMat;

	private Material _customMat;

	private Vector3[] _ringPos;

	private float[] _unitX;

	private float[] _unitZ;

	private int _builtSegments;

	private Vector3[] _dotPos;

	private const int DotSegments = 16;

	private Vector3[] _sweepPos;

	private const int SweepSegments = 14;

	private float _sweepAngle;

	private float _cachedGroundY;

	private float _lastGx;

	private float _lastGz;

	public float Radius
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	public Transform FollowTarget { get; set; }

	public void ConfigureAsRangeRing(float r, Color c, float lineWidth)
	{
	}

	public void Configure(float r, Color c, float lineWidth, Material customMat = null)
	{
	}

	public void SetHighlight(Highlight state)
	{
	}

	private void Awake()
	{
	}

	private void EnsureLines()
	{
	}

	private LineRenderer CreateChildLine(string childName, bool loop, bool glow)
	{
		return null;
	}

	private void SetupLine(LineRenderer lr, bool loop, bool glow)
	{
	}

	private static Material GetBaseMaterial()
	{
		return null;
	}

	private static Material GetGlowMaterial()
	{
		return null;
	}

	private void ApplyMaterial()
	{
	}

	private void EnsureBuffers()
	{
	}

	private void LateUpdate()
	{
	}
}
