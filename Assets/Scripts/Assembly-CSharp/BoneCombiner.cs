using System.Collections.Generic;
using UnityEngine;

public class BoneCombiner
{
	private struct VfxEntry
	{
		public ParticleSystem ps;

		public bool onRenderer;

		public bool badge;

		public bool nested;

		public int boneIndex;

		public string boneName;

		public Matrix4x4 localInBone;
	}

	private readonly Dictionary<int, Transform> _rootBoneDictionary;

	private readonly Transform[] _boneTransforms;

	private readonly Transform _transform;

	private readonly Transform _equipmentContainer;

	private const string ArmatureString = "armature";

	private const string ContainerName = "Equipment";

	private static readonly HashSet<string> s_warnedMissingPairBones;

	private const string RootBoneName = "Bip01";

	private readonly Dictionary<string, Transform> _anchorHolders;

	private static readonly Dictionary<GameObject, SkinnedMeshRenderer> _sourceRendererCache;

	private static readonly Dictionary<List<string>, int[]> _boneHashCache;

	private static readonly VfxEntry[] s_noEntries;

	private static readonly Dictionary<SkinnedMeshRenderer, VfxEntry[]> _vfxEntryCache;

	private readonly Dictionary<string, LimbVfxLink> _vfxLinks;

	private static readonly HashSet<int> s_warnedDuplicateBones;

	public Transform EquipmentContainer => null;

	public BoneCombiner(GameObject rootObj)
	{
	}

	public Transform AddLimb(GameObject bonedObj, List<string> boneNames, string limbName)
	{
		return null;
	}

	public Transform AddLimbPaired(GameObject bonedObj, string limbName)
	{
		return null;
	}

	public Transform AddLimbAnchored(GameObject bonedObj, string limbName)
	{
		return null;
	}

	private Transform CreateAnchorHolder(Transform parent, string limbName)
	{
		return null;
	}

	private static Transform FindChildByName(Transform root, string name, int maxDepth)
	{
		return null;
	}

	public void RemoveLimbAnchors(string limbName)
	{
	}

	public Transform AddLimb(GameObject bonedObj, List<string> boneNames)
	{
		return null;
	}

	public Transform FindBone(string boneName)
	{
		return null;
	}

	private static SkinnedMeshRenderer GetSourceRenderer(GameObject bonedObj)
	{
		return null;
	}

	private static int[] GetBoneHashes(List<string> boneNames)
	{
		return null;
	}

	private Transform ProcessBonedObject(SkinnedMeshRenderer renderer, int[] boneHashes, string limbName)
	{
		return null;
	}

	private static VfxEntry[] GetVfxEntries(SkinnedMeshRenderer source)
	{
		return null;
	}

	private static bool IsRigBone(Transform t, Transform[] bones, Transform rig)
	{
		return false;
	}

	private static int IndexOfParticle(ParticleSystem[] particles, Transform t)
	{
		return 0;
	}

	private static int NearestBoneSegment(Vector3 p, Transform[] bones, Vector3[] joints, int boneCount, Matrix4x4 toSource)
	{
		return 0;
	}

	private void CopyParticles(SkinnedMeshRenderer source, SkinnedMeshRenderer target)
	{
	}

	private void CopyParticles(SkinnedMeshRenderer source, SkinnedMeshRenderer target, string limbName, bool anchorToBones, int mappedBoneCount)
	{
	}

	private static bool IsBadgeVfx(Transform t, Transform sourceRoot)
	{
		return false;
	}

	private static bool IsNestedUnderParticle(Transform t, Transform sourceRoot)
	{
		return false;
	}

	private Transform ResolveVfxAnchor(in VfxEntry e, int mappedBoneCount)
	{
		return null;
	}

	private LimbVfxLink CreateVfxLink(SkinnedMeshRenderer limbRenderer, string limbName)
	{
		return null;
	}

	private static Transform HolderFor(LimbVfxLink link, Transform anchor)
	{
		return null;
	}

	private static Transform FindMappedParent(Transform sourceParent, Transform sourceRoot, Transform targetRoot)
	{
		return null;
	}

	private static void RetargetShapeModules(Transform root, SkinnedMeshRenderer source, SkinnedMeshRenderer target)
	{
	}

	private void TraverseHierarchy(Transform root)
	{
	}

	private static string GetPath(Transform t)
	{
		return null;
	}
}
