using System.Collections.Generic;
using UnityEngine;

public class GhostInstance : MonoBehaviour
{
	private float fadeSpeed;

	private float age;

	private bool active;

	private readonly List<Material> materials;

	private readonly List<bool> isSlotsMat;

	private readonly List<float> startAlphas;

	private readonly List<int> colorPropIDs;

	private readonly List<Mesh> bakedMeshes;

	private static readonly int ColorPropID;

	private static readonly int BaseColorPropID;

	private static readonly int SlotCountPropID;

	private static readonly int Tex15PropID;

	private static readonly int GhostAlphaPropID;

	private const int NoColorProp = 0;

	public void Setup(float speed)
	{
	}

	public void TrackMaterial(Material m)
	{
	}

	public void TrackBakedMesh(Mesh m)
	{
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
	}

	internal void ReturnResources()
	{
	}
}
