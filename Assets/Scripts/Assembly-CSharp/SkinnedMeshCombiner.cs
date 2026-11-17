using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public static class SkinnedMeshCombiner
{
	private sealed class CachedCombine
	{
		public int key;

		public Mesh mesh;

		public string[] boneNames;

		public readonly List<MatSlot> matSlots;

		public bool hasElementUv;

		public int[] contentKey;

		public List<SlotBinding> slotBindings;

		public string rootBoneName;

		public int users;

		public int lastUseFrame;

		public bool orphaned;

		public long bytes;
	}

	public sealed class CombineResult
	{
		public Mesh mesh;

		public Transform[] bones;

		public Material[] materials;

		public Transform rootBone;
	}

	private readonly struct MatSlot
	{
		public readonly bool isNull;

		public readonly bool isSlotsMat;

		public readonly bool isSlotsGlow;

		public readonly int meshId;

		public readonly string matName;

		public readonly int subIdx;

		public static readonly MatSlot Null;

		public static readonly MatSlot Slots;

		public static readonly MatSlot SlotsGlow;

		private MatSlot(bool nul, bool slots, bool glow, int mesh, string name, int sub = 0)
		{
			isNull = false;
			isSlotsMat = false;
			isSlotsGlow = false;
			meshId = 0;
			matName = null;
			subIdx = 0;
		}

		public static MatSlot Source(int meshId, string matName, int subIdx)
		{
			return default;
		}
	}

	public sealed class SlotBinding
	{
		public int meshId;

		public string matName;

		public int subIdx;

		public int slotIdx;
	}

	internal struct BlockPlan
	{
		public int slot;

		public int overlaySlot;

		public int slotUv;

		public bool elementBlock;
	}

	private sealed class SourceMeshData
	{
		public int readableCount;

		public Vector3[] verts;

		public BoneWeight[] weights;

		public Vector3[] normals;

		public Vector4[] tangents;

		public Vector2[] uv0;

		public Vector2[] uv1;

		public Vector2[] uv2;

		public Vector2[] uv3;

		public Color[] colors;

		public int[][] subTriangles;

		public long bytes;

		public int lastUseFrame;
	}

	internal sealed class CombineBuffers
	{
		public Vector3[] verts;

		public Vector3[] normals;

		public Vector4[] tangents;

		public Vector2[] uv0;

		public Vector2[] uv1;

		public Vector2[] uv2;

		public Vector2[] uv3;

		public Color[] colors;

		public int[] tris;

		public int[] blockPos;

		public int[] unique;
	}

	public sealed class PendingCombine
	{
		public Transform parent;

		public string name;

		public bool disableSources;

		public ICollection<SkinnedMeshRenderer> noFold;

		public int cacheKey;

		public int stage;

		public int partIdx;

		public bool done;

		public CombineResult result;

		public SkinnedMeshRenderer renderer;

		public readonly List<SkinnedMeshRenderer> live;

		public Transform[][] bonesOf;

		public Material[][] matsOf;

		public string[][] matNamesOf;

		public readonly List<Transform> masterBones;

		public readonly Dictionary<Transform, int> masterIndex;

		public readonly List<int> masterNext;

		public Mesh[] sourceMeshes;

		public int[] readableCounts;

		public Vector3[][] srcVerts;

		public BoneWeight[][] srcWeights;

		public Vector3[][] srcNormals;

		public Vector4[][] srcTangents;

		public Vector2[][] srcUvs;

		public Vector2[][] srcUv2s;

		public Vector2[][] srcUv3s;

		public Vector2[][] srcUv4s;

		public Color[][] srcColors;

		public int[][][] srcSubTris;

		public Matrix4x4[][] srcBindposes;

		public int[][] reindexMaps;

		public bool anyNormals;

		public bool anyTangents;

		public bool anyUv;

		public bool anyUv3;

		public bool anyUv4;

		public bool anyColors;

		public Matrix4x4[] masterBindposes;

		public readonly List<Material> allMaterials;

		public Material[][] slotMaterials;

		public readonly List<Material> foldMats;

		public readonly Dictionary<Material, bool> isFold;

		public readonly Dictionary<Material, Texture2D> glowTexOf;

		public readonly Dictionary<Material, int> slotOf;

		public int slotCount;

		public Material slotMat;

		public int slotMatIdx;

		public Material slotGlowMat;

		public int slotGlowIdx;

		public readonly List<SlotBinding> slotBindings;

		public readonly List<Material> combinedMaterials;

		public readonly Dictionary<Material, int> individualSlot;

		public int nullSlot;

		public int[] contentKey;

		public Material elementMat;

		public int[][][] srcSubTriangles;

		public int[][][] blockVerts;

		public int[][] blockOffsets;

		public int[][] blockSlots;

		public int[][] blockSubs;

		public int[] combinedTriCounts;

		public int totalVertices;

		public bool hasNormals;

		public bool hasTangents;

		public bool hasUv0;

		public bool hasUv2;

		public bool hasUv3;

		public bool hasColors;

		internal CombineBuffers buf;

		public BoneWeight[] combinedWeights;

		public readonly List<int> subTriStarts;

		public readonly List<int> subTriLens;

		public readonly List<Material> finalMaterials;

		public readonly List<int> fillPos;

		public int[] subIndexMap;

		public bool boundsValid;

		public Vector3 boundsMin;

		public Vector3 boundsMax;

		internal BlockPlan[][] blockPlans;

		public Matrix4x4[][] corrections;

		public bool hasElement;

		internal int jobState;

		internal bool cancelled;

		internal readonly object jobLock;

		public Mesh combined;

		public bool IsWaitingOnJob => false;
	}

	private static readonly Dictionary<int, CachedCombine> s_combinedCache;

	private static readonly Dictionary<Mesh, CachedCombine> s_cachedMeshes;

	private const int MaxCachedCombines = 1024;

	private static long s_totalCacheBytes;

	private static long s_lastDeclinedBytes;

	private static readonly List<CachedCombine> s_evictScratch;

	private static readonly int s_idMainTex;

	private static readonly int s_idColor;

	private static readonly int s_idCutoff;

	private static readonly int s_idGlowTex;

	private static readonly int s_idGlowShineStrength;

	private static readonly int s_idGlowShineColor;

	private static readonly int s_idGlowShineSpeed;

	private static readonly int s_idGlowOpacity;

	private static readonly int s_idElementTex;

	private static readonly int s_idElementTex2;

	private static readonly int s_idElementMove;

	private static readonly int s_idElementOpacity;

	private static readonly int s_idElementToggle;

	private static readonly int s_idHasElement;

	private static readonly int s_idElementUvMode;

	private static readonly Vector2 s_elementSentinel;

	private const int MaxSlots = 16;

	private const string SlotsMainShaderName = "Custom/CombinedSlotsMain";

	private const string SlotsOverlayShaderName = "Custom/CombinedSlotsOverlay";

	public static long MaxCacheBytes;

	public static bool UpdateWhenOffscreen;

	private const float BindposeEpsilon = 0.0001f;

	private static Texture2D s_blackTile;

	private static readonly int[] s_idTexK;

	private static readonly int[] s_idSTK;

	private static readonly int[] s_idColorK;

	private static readonly int[] s_idCutK;

	private static readonly int[] s_idGlowTexK;

	private static readonly int[] s_idGlowColorK;

	private static readonly int[] s_idGlowStrengthK;

	private static readonly int[] s_idGlowSpeedK;

	private static readonly int[] s_idGlowOpacityK;

	private static readonly int s_idSlotCount;

	private static Shader s_mainSlotShader;

	private static Shader s_overlaySlotShader;

	private static readonly HashSet<string> s_neverFoldShaders;

	private const MeshUpdateFlags UploadFlags = MeshUpdateFlags.DontNotifyMeshUsers | MeshUpdateFlags.DontRecalculateBounds;

	internal const int StageRead = 0;

	internal const int StageMaterials = 1;

	internal const int StagePrepare = 2;

	internal const int StageBuild = 3;

	internal const int StageUploadVertices = 4;

	internal const int StageUploadIndices = 5;

	internal const int StageCache = 6;

	internal const int JobIdle = 0;

	internal const int JobRunning = 1;

	internal const int JobDone = 2;

	internal const int JobFailed = 3;

	public static int MaxUncachedReadsPerTick;

	private static readonly WaitCallback s_buildJob;

	public static int MaxConcurrentBuilds;

	private static int s_runningBuilds;

	private static readonly List<Vector2> s_uvScratch;

	private static readonly List<int> s_triScratch;

	private static readonly BoneWeight s_parkedTail;

	private static readonly Dictionary<int, SourceMeshData> s_sourceCache;

	private static long s_sourceCacheBytes;

	public static long MaxSourceCacheBytes;

	private static readonly Dictionary<int, Matrix4x4[]> s_bindposeCache;

	private static readonly Dictionary<int, string> s_nameCache;

	private const int MaxCachedNames = 262144;

	private static readonly Stack<CombineBuffers> s_bufferPool;

	private const int MaxPooledBuffers = 4;

	private static readonly object s_bufferPoolLock;

	private static MaterialPropertyBlock s_tintBlock;

	public static long CacheBytes => 0L;

	public static long LiveMergeBudgetBytes => 0L;

	public static bool OverLiveMergeBudget => false;

	public static bool LastBeginDeclinedForMemory { get; private set; }

	private static Shader MainSlotShader => null;

	private static Shader OverlaySlotShader => null;

	public static void ClearUnusedCaches()
	{
	}

	public static void DestroyCombined(SkinnedMeshRenderer renderer)
	{
	}

	private static void TrimCaches()
	{
	}

	private static long EstimateTotalCacheBytes()
	{
		return 0L;
	}

	private static void RegisterCacheBytes(CachedCombine e)
	{
	}

	private static long EstimateMergeBytes(List<SkinnedMeshRenderer> live)
	{
		return 0L;
	}

	private static long EstimateCacheEntryBytes(CachedCombine e)
	{
		return 0L;
	}

	private static void EvictCacheEntry(CachedCombine e)
	{
	}

	private static void DisposeCacheEntry(CachedCombine e)
	{
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void ResetStatics()
	{
	}

	private static void OnLowMemory()
	{
	}

	private static void OnSceneUnloaded(Scene _)
	{
	}

	private static int BuildCombineKey(IList<SkinnedMeshRenderer> live, Transform[][] bonesOf, Material[][] matsOf, string[][] matNamesOf)
	{
		return 0;
	}

	private static bool BoneNamesMatch(string[] expected, List<Transform> bones)
	{
		return false;
	}

	private static int[] BuildContentKey(IList<SkinnedMeshRenderer> live, Material[][] matsOf, string[][] matNamesOf)
	{
		return null;
	}

	private static bool ContentKeyMatches(int[] expected, int[] actual)
	{
		return false;
	}

	private static Material FindElementMaterial(PendingCombine p)
	{
		return null;
	}

	public static PendingCombine BeginCombineInto(IList<SkinnedMeshRenderer> sources, Transform parent, string name, bool disableSources = true, ICollection<SkinnedMeshRenderer> noFold = null, bool exemptFromMemoryBudget = false)
	{
		return null;
	}

	public static bool TickCombine(PendingCombine p)
	{
		return false;
	}

	public static void CancelCombine(PendingCombine p)
	{
	}

	private static bool BindposeEquals(in Matrix4x4 a, in Matrix4x4 b)
	{
		return false;
	}

	private static void DestroyObject(Object obj)
	{
	}

	private static Transform ResolveRootBoneByName(List<Transform> masterBones, string rootName)
	{
		return null;
	}

	private static Transform ResolveRootBone(IList<SkinnedMeshRenderer> live, IList<Transform> masterBones)
	{
		return null;
	}

	private static int[] BuildSlotIds(string prefix)
	{
		return null;
	}

	private static bool IsNeverFoldMaterial(Material m)
	{
		return false;
	}

	private static Texture2D BlackTile()
	{
		return null;
	}

	private static void TickMaterials(PendingCombine p)
	{
	}

	private static bool IsGlowOn(Material m)
	{
		return false;
	}

	private static Texture2D OverlayGlowTexture(Material m)
	{
		return null;
	}

	private static bool HasOverlayGlow(Material m)
	{
		return false;
	}

	private static int ResolveSlotFor(PendingCombine p, Material m)
	{
		return 0;
	}

	private static bool IsOverlayPart(Material m, Dictionary<Material, Texture2D> glowTexOf)
	{
		return false;
	}

	private static int ResolveOverlaySlotFor(PendingCombine p, Material m)
	{
		return 0;
	}

	private static (int, string, int) FindSourceKey(PendingCombine p, Material m)
	{
		return default;
	}

	private static string MaterialNameOf(PendingCombine p, Material m)
	{
		return null;
	}

	private static Material FindIndividualMaterial(PendingCombine p, int meshId, string matName, int subIdx)
	{
		return null;
	}

	private static bool BuildSlotMaterial(PendingCombine p)
	{
		return false;
	}

	private static void BuildSlotMaterialsFromBindings(List<SlotBinding> bindings, PendingCombine p, out Material mainMat, out Material glowMat)
	{
		mainMat = null;
		glowMat = null;
	}

	private static void CollectMaterials(ICollection<SkinnedMeshRenderer> from, HashSet<Material> into)
	{
	}

	private static void TickPrepareBuild(PendingCombine p)
	{
	}

	private static Matrix4x4[] BuildCorrections(PendingCombine p, int s)
	{
		return null;
	}

	private static void TickBuild(PendingCombine p)
	{
	}

	private static void RunBuildJob(object state)
	{
	}

	private static void SplitAllParts(PendingCombine p)
	{
	}

	private static void SplitPart(PendingCombine p, int s, int[] blockPos, int[] unique)
	{
	}

	private static void AllocateCombinedArrays(PendingCombine p)
	{
	}

	private static void CopyAllParts(PendingCombine p)
	{
	}

	private static void CopyPart(PendingCombine p, int s)
	{
	}

	private static void TickUploadVertices(PendingCombine p)
	{
	}

	private static void TickUploadIndices(PendingCombine p)
	{
	}

	private static void TickFinalizeCache(PendingCombine p)
	{
	}

	private static bool IsSourceCached(SkinnedMeshRenderer src)
	{
		return false;
	}

	private static SourceMeshData GetSourceData(Mesh mesh)
	{
		return null;
	}

	private static void TrimSourceCache()
	{
	}

	private static void ClearSourceCache()
	{
	}

	private static Matrix4x4[] GetBindposes(Mesh mesh)
	{
		return null;
	}

	private static string CachedName(Transform o)
	{
		return null;
	}

	private static T[] NullIfEmpty<T>(T[] arr)
	{
		return null;
	}

	private static CombineBuffers RentBuffers()
	{
		return null;
	}

	private static void ReturnBuffers(PendingCombine p)
	{
	}

	private static void EnsureSize<T>(ref T[] arr, int n)
	{
	}

	private static void TickReadPart(PendingCombine p)
	{
	}

	private static int RemapIndex(int localIndex, int[] reindex)
	{
		return 0;
	}

	private static Vector2[] ReadUV(Mesh mesh, int channel)
	{
		return null;
	}

	private static SkinnedMeshRenderer CreateCombinedRenderer(PendingCombine p, CombineResult result)
	{
		return null;
	}

	private static Bounds MakeGenerousBounds(Bounds meshBounds)
	{
		return default;
	}
}
