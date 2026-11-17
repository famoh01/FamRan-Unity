using System.Collections.Generic;
using UnityEngine;

public class LightspeedTintHolder : MonoBehaviour
{
	private struct TintedRenderer
	{
		public Renderer renderer;

		public Color originalColor;

		public Color tintedColor;

		public bool hadColor;

		public bool hadBaseColor;
	}

	private const int MaxSlots = 16;

	private static readonly int BaseColorId;

	private static readonly int LegacyColorId;

	private static readonly int[] SlotColorIds;

	private Color _tint;

	private float _recheckInterval;

	private bool _holdWhileBuffActive;

	private int _skillMainId;

	private int _skillSubId;

	private float _factGraceSeconds;

	private float _factLostGraceSeconds;

	private float _fallbackHoldSeconds;

	private bool _effectActive;

	private bool _factSeen;

	private float _startedAt;

	private float _releasedAt;

	private float _factLostAt;

	private float _nextRecheck;

	private Material _tintedSlotMaterial;

	private readonly Color[] _originalSlotColors;

	private readonly Color[] _tintedSlotColors;

	private readonly bool[] _slotTinted;

	private readonly List<TintedRenderer> _modifiedRenderers;

	private readonly List<Renderer> _scanBuffer;

	private readonly List<Material> _matBuffer;

	private MaterialPropertyBlock _propBlock;

	private PlayerController _localPlayer;

	private PlayerControllerClient _remotePlayer;

	private bool _isApplied;

	private static int[] BuildSlotColorIds()
	{
		return null;
	}

	public void Begin(LightspeedPrefabEffect settings, int skillMainId, int skillSubId)
	{
	}

	public void EffectReleased()
	{
	}

	private void Update()
	{
	}

	private bool ShouldKeep()
	{
		return false;
	}

	private bool BuffFactActive()
	{
		return false;
	}

	private void OnDisable()
	{
	}

	private void Apply()
	{
	}

	private void TintCombinedMesh()
	{
	}

	private void VerifySlotTint()
	{
	}

	private static bool IsHairSlot(List<SkinnedMeshCombiner.SlotBinding> bindings, int slotIdx)
	{
		return false;
	}

	private void CollectAndTint(Transform container, bool skipWeapons)
	{
	}

	private int IndexOfTracked(Renderer ren)
	{
		return 0;
	}

	private void VerifyRendererTint(int index)
	{
	}

	private void RescanRenderers()
	{
	}

	private void Remove()
	{
	}

	private static void FindCharacterContainers(Transform root, out Transform faces, out Transform equipment, out Transform baseBody)
	{
		faces = null;
		equipment = null;
		baseBody = null;
	}

	private static void ScanContainers(Transform current, ref Transform faces, ref Transform equipment, ref Transform baseBody)
	{
	}
}
