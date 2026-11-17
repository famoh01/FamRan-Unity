using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseBodyClient : MonoBehaviour
{
	[Header("Character Select Data")]
	public static CharacterBaseBodyClient instance;

	public GameDataManager gameDataManager;

	public GameObject[] charFace;

	public GameObject[] charHair;

	public SkinnedMeshRenderer charArmor;

	public SkinnedMeshRenderer charPants;

	public SkinnedMeshRenderer charGloves;

	public SkinnedMeshRenderer charBoots;

	public Renderer hairRenderer;

	private bool _appearanceApplied;

	private static readonly int s_colorId;

	private static readonly List<Renderer> s_rendererScratch;

	private readonly List<Material> _ownedHairMaterials;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void ApplyAppearance()
	{
	}

	private static void ApplyHairFaceRendererSettings(GameObject[] objects, int activeIndex)
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

	public static void SetAllInactive(GameObject[] objects)
	{
	}

	public static void ActivateAt(GameObject[] objects, int index)
	{
	}

	private Color GetHairColor(int colorType)
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
