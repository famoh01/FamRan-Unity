using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SkillGhostTrail : MonoBehaviour
{
	private struct GhostSource
	{
		public Renderer renderer;

		public SkinnedMeshRenderer skinned;

		public MeshFilter staticFilter;

		public Material[] sourceMaterials;

		public float targetAlpha;

		public bool combined;
	}

	[Header("Trail Settings")]
	[Tooltip("Ilang afterimage ang kabuuang maispawn bawat cast.")]
	public int ghostCount;

	[Tooltip("Seconds pagitan ng mga spawn.")]
	public float ghostInterval;

	[Tooltip("Kung gaano kabilis mag-fade ang ISANG afterimage.\nBuong fade time = 1 / ghostFadeSpeed seconds (2 = 0.5s, 4 = 0.25s).")]
	[Range(0.5f, 20f)]
	public float ghostFadeSpeed;

	[Header("Opacity Settings")]
	[Range(0f, 1f)]
	public float playerInitialAlpha;

	[Range(0f, 1f)]
	public float equipmentInitialAlpha;

	private const float MinSpawnInterval = 0.02f;

	private Shader transparentShader;

	private GameObject targetPlayer;

	private readonly List<GhostSource> sources;

	private GhostTrailTicker activeTicker;

	private static readonly int AtlasRegionID;

	private static readonly int SrcBlendPropID;

	private static readonly int DstBlendPropID;

	private static readonly int ZWritePropID;

	private static readonly int SurfacePropID;

	private static readonly int BlendPropID;

	private static readonly int ColorPropID;

	private static readonly int BaseColorPropID;

	private static readonly int MainTexPropID;

	private static readonly int BaseMapPropID;

	private static readonly int GlowTex0PropID;

	private static readonly int SlotCountPropID;

	private static readonly int Tex15PropID;

	private static readonly int GhostAlphaPropID;

	private static MaterialPropertyBlock s_blockScratch;

	private static readonly List<Material> s_matScratch;

	private static readonly List<Vector2> s_uvScratch;

	private static Material GetHiddenMaterial(Shader shader)
	{
		return null;
	}

	private static bool TryGetAtlasRegion(Renderer r, out Vector4 region)
	{
		region = default;
		return false;
	}

	private void OnEnable()
	{
	}

	internal int DebugSourceCount()
	{
		return 0;
	}

	private int IndexOfCombinedSource()
	{
		return 0;
	}

	private bool IsAlreadySourced(Renderer r)
	{
		return false;
	}

	private bool RefreshCombinedSource()
	{
		return false;
	}

	private void PruneDeadSources()
	{
	}

	private void ResolveTargetPlayer()
	{
	}

	private static bool NameContains(string name, string token)
	{
		return false;
	}

	private void BuildSources()
	{
	}

	public void CreateGhostSnapshot()
	{
	}

	[Conditional("GHOST_TRAIL_DEBUG")]
	private static void LogSourceOffChain(Renderer r, bool combinedSource)
	{
	}

	[Conditional("GHOST_TRAIL_DEBUG")]
	private void LogLaunch()
	{
	}

	[Conditional("GHOST_TRAIL_DEBUG")]
	private void LogEmptySnapshot()
	{
	}

	private void SetupURPTransparentMaterial(Material mat, float alpha)
	{
	}
}
