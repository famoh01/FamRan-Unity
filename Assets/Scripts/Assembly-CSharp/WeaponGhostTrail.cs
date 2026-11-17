using System.Collections.Generic;
using UnityEngine;

public class WeaponGhostTrail : MonoBehaviour
{
	private struct GhostSource
	{
		public Renderer renderer;

		public SkinnedMeshRenderer skinned;

		public MeshFilter staticFilter;

		public Material[] sourceMaterials;

		public float targetAlpha;
	}

	[Header("Trail Settings")]
	public float activeDuration;

	public float ghostInterval;

	public float ghostFadeSpeed;

	[Header("Opacity Settings")]
	[Range(0f, 1f)]
	public float weaponInitialAlpha;

	private const float TrailTailDuration = 0.1f;

	private Shader transparentShader;

	private GameObject targetPlayer;

	private readonly List<GhostSource> sources;

	private float timer;

	private float elapsed;

	private static readonly int MainTexPropID;

	private static readonly int BaseMapPropID;

	private static readonly int ColorPropID;

	private static readonly int BaseColorPropID;

	private static readonly int SurfacePropID;

	private static readonly int BlendPropID;

	private static readonly int SrcBlendPropID;

	private static readonly int DstBlendPropID;

	private static readonly int ZWritePropID;

	private void OnEnable()
	{
	}

	private void ResolveTargetPlayer()
	{
	}

	private void BuildWeaponSources()
	{
	}

	private static bool NameContains(string haystack, string needle)
	{
		return false;
	}

	private void Update()
	{
	}

	private void CreateWeaponGhostSnapshot()
	{
	}
}
