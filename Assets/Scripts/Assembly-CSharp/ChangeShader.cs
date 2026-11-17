using System.Collections.Generic;
using UnityEngine;

public class ChangeShader : MonoBehaviour
{
	private readonly List<Material> _owned;

	private readonly Dictionary<Material, Color> _glowColorOf;

	private readonly Dictionary<Material, float> _elementToggleOf;

	private bool _glowEnabled;

	private SkinnedMeshRenderer _renderer;

	private SkinnedMeshRenderer Renderer => null;

	public void ApplyCustomShader(Shader shader, float power, Texture material, Texture effectTexture, byte grade = 0)
	{
	}

	public void ApplyWeaponCustomShader(Shader shader, float power, Texture material, Texture effectTexture, Texture element1, Texture element2, float toggle, float opacity, byte grade = 0)
	{
	}

	private void Install(Material glowMaterial, Texture targetTexture)
	{
	}

	public void SetGlowEnabled(bool enabled)
	{
	}

	private void ApplyGlowState(Material mat, bool enabled)
	{
	}

	private void Release(Material mat)
	{
	}

	private void OnDestroy()
	{
	}

	private Color GetGradeColor(byte grade)
	{
		return default;
	}
}
