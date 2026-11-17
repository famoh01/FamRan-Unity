using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public static class VideoSettings
{
	public const string EffectsKey = "VideoEffects";

	public const string ReflexKey = "VideoReflex";

	public const string GlowKey = "VideoGlow";

	public const string BackgroundShadowKey = "VideoBGShadow";

	public const string RefractKey = "VideoRefract";

	public const string PostProcessKey = "VideoPostProcess";

	public const string TargetEffectKey = "VideoTargetEffect";

	private static bool _targetEffect;

	private static bool _reflex;

	private static bool _glow;

	private static bool _backgroundShadow;

	private static bool _refract;

	private static bool _postProcess;

	private static bool _applied;

	private static bool _hooked;

	private static Volume _glowVolume;

	private static Bloom _bloom;

	private const float GlowIntensity = 1.1f;

	private const float GlowThreshold = 0.9f;

	private const float RenderScaleHigh = 1f;

	private const float RenderScaleMedium = 0.85f;

	private const float RenderScaleLow = 0.7f;

	private const int ShadowmapResolutionHigh = 1024;

	private static bool _urpStateCaptured;

	private static float _authoredRenderScale;

	private static float _authoredShadowDistance;

	private static int _authoredShadowmapResolution;

	private static int _authoredCascadeCount;

	private static bool _authoredOpaqueTexture;

	private static readonly string[] CharacterLayerNames;

	private static int _characterMask;

	private static readonly Dictionary<Renderer, ShadowCastingMode> _shadowOriginals;

	private static readonly Dictionary<Terrain, ShadowCastingMode> _terrainShadowOriginals;

	public static bool Effects { get; set; }

	public static bool TargetEffect
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public static bool Reflex
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public static bool Glow
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public static bool BackgroundShadow
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public static bool Refract
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public static bool PostProcess
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Bootstrap()
	{
	}

	public static void Load()
	{
	}

	public static void Save()
	{
	}

	private static bool ReadPref(string key, bool fallback)
	{
		return false;
	}

	private static void WritePref(string key, bool value)
	{
	}

	private static void ApplyAll()
	{
	}

	private static void HookSceneEvents()
	{
	}

	private static void ApplyReflex()
	{
	}

	private static void ApplyRefract()
	{
	}

	private static void ApplyPostProcess()
	{
	}

	private static void ApplyGlow()
	{
	}

	private static void EnsureGlowVolume()
	{
	}

	public static void ApplyDetailRenderScale(int detailIndex)
	{
	}

	public static void ApplyShadowQuality(int shadowIndex)
	{
	}

	private static void CaptureAuthoredUrpState(UniversalRenderPipelineAsset urp)
	{
	}

	private static void RestoreAuthoredUrpState()
	{
	}

	private static void ApplyBackgroundShadow()
	{
	}
}
