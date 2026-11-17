using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetPanelHandler : MonoBehaviour
{
	[Header("UI References")]
	public RawImage petPreviewImage;

	public TextMeshProUGUI petNameText;

	public TextMeshProUGUI petKindText;

	[Header("Showcase Settings")]
	[Tooltip("Where the preview clone is parked — far from the playable world so nothing else is in shot.")]
	public Vector3 showcasePosition;

	[Tooltip("Render texture size. Dropping to 256 quarters the fill cost if the panel is small.")]
	[Min(64f)]
	public int previewResolution;

	[Range(5f, 60f)]
	public float fieldOfView;

	[Tooltip("Empty space around the pet. 1 = it exactly fills the frame; BELOW 1 zooms in and crops the edges (a long tail, a wingtip), which is usually what reads best in a small panel.")]
	[Min(0.3f)]
	public float framingPadding;

	[Header("Framing")]
	[Tooltip("Keep re-fitting the camera for this long after a pet is shown, so a hover/idle animation that moves the model can't push it out of frame. 0 = fit once.")]
	[Min(0f)]
	public float frameSettleTime;

	[Tooltip("Seconds between re-fit samples during the settle window. Fitting reads every renderer's bounds, so there's no reason to do it every frame.")]
	[Min(0.02f)]
	public float frameSampleInterval;

	private Camera showcaseCamera;

	private RenderTexture renderTexture;

	private GameObject petClone;

	private Renderer[] petRenderers;

	private readonly List<Material> ownedMaterials;

	private float fitRadius;

	private float fitMinY;

	private float fitMaxY;

	private bool hasFit;

	private float fitFreezeTime;

	private float nextSampleTime;

	private bool showcaseVisible;

	private int lastPetInstanceID;

	private static Shader s_cutoutShader;

	private static bool s_cutoutShaderResolved;

	private static Shader CutoutShader => null;

	private void OnEnable()
	{
	}

	private void Update()
	{
	}

	private bool IsPreviewVisible()
	{
		return false;
	}

	private void ApplyShowcaseVisibility(bool visible)
	{
	}

	private void LateUpdate()
	{
	}

	private void OnDisable()
	{
	}

	public void RefreshPetPanel()
	{
	}

	private static string SpeciesName(MobNpcObject crowObj)
	{
		return null;
	}

	private void Create3DShowcase(MobNpcObject crowObj)
	{
	}

	private void EnsureRenderTarget()
	{
	}

	private void EnsureCamera()
	{
	}

	private void FitCameraToPet()
	{
	}

	private static float MaxHorizontalDistance(Bounds bounds, Vector3 axis)
	{
		return 0f;
	}

	private bool TryCalculatePetBounds(out Bounds bounds)
	{
		bounds = default;
		return false;
	}

	private static void SetLayerRecursively(GameObject obj, int newLayer)
	{
	}

	private void DestroyClone()
	{
	}

	private void ReleaseRenderTarget()
	{
	}

	private void CleanupShowcase()
	{
	}

	public void RotateModel(float horizontalDelta, float sensitivity)
	{
	}
}
