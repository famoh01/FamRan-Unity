using System.Collections.Generic;
using UnityEngine;

public sealed class CombinedGearMesh : MonoBehaviour
{
	private readonly List<Material> _ownedMaterials;

	public Material slotMaterial;

	public List<SkinnedMeshCombiner.SlotBinding> slotBindings;

	public void TrackOwnedMaterial(Material mat)
	{
	}

	private void OnDestroy()
	{
	}
}
