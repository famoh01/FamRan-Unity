using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseBody : MonoBehaviour
{
	[Header("Character Select Data")]
	public static CharacterBaseBody instance;

	public GameObject[] charFace;

	public GameObject[] charHair;

	public SkinnedMeshRenderer charArmor;

	public SkinnedMeshRenderer charPants;

	public SkinnedMeshRenderer charGloves;

	public SkinnedMeshRenderer charBoots;

	public Renderer hairRenderer;

	private static readonly int s_colorId;

	private static readonly List<Renderer> s_rendererScratch;

	private readonly List<Material> _ownedHairMaterials;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void RenderHairColor(int colorData)
	{
	}

	public static void SetAllInactive(GameObject[] objects)
	{
	}

	public void SetHairAllInActive()
	{
	}

	public void ActivateHair(int index)
	{
	}

	public void SetFaceAllInActive()
	{
	}

	public void ActivateFace(int index)
	{
	}

	private Color GetHairColor(int colorData)
	{
		return default;
	}

	private void OnDestroy()
	{
	}

	private void ChangeAllHairColors(Color color)
	{
	}

	private void TintOwnMaterials(Renderer renderer, Color color)
	{
	}
}
