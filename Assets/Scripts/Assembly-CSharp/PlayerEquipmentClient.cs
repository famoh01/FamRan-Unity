using System.Collections.Generic;
using Core.Enumerators;
using Models.Library.Shared.Game;
using Models.Network.Shared.Response;
using UnityEngine;

public class PlayerEquipmentClient : MonoBehaviour
{
	[Header("Hide Set Object")]
	public HideSetObject hideUpper;

	public HideSetObject hideLower;

	public HideSetObject hideBoots;

	[Header("Slot Back")]
	public Transform slotRightHand;

	public Transform whalSlot;

	public Transform stickRightSlot;

	[Header("Piece")]
	public Transform pieceRightHand;

	public Transform pieceLeftHand;

	[Header("Weapons")]
	public Transform gum;

	public Transform whal;

	public Transform stick;

	[Header("Effect Containers")]
	public Transform headEffectContainer;

	public Transform floorEffectContainer;

	[Tooltip("Fixed height of the badge VFX above the model's Scene_Root while on foot.")]
	public float badgeHeight;

	[Tooltip("Extra height added to the badge VFX while the player is riding a board/mount.")]
	public float mountedBadgeHeightOffset;

	private BoneCombiner _boneCombiner;

	private SkinnedMeshRenderer _combinedGear;

	private bool _combineDirty;

	private ulong _lastGearSig;

	private bool _hasGearSig;

	private Animator _animatorCache;

	private SkinnedMeshCombiner.PendingCombine _pendingCombine;

	private readonly List<SkinnedMeshRenderer> _gearParts;

	private readonly HashSet<SkinnedMeshRenderer> _previousCombineSources;

	private readonly List<SkinnedMeshRenderer> _noFoldParts;

	public const string HideCostumeSetPrefKey = "HideCostumeSet";

	private const string HideCostumeRootName = "HideCos";

	private static int _hideCostumeSetState;

	private Transform _hideCosRoot;

	private SkinnedMeshRenderer[] _hideCosRenderers;

	private bool _hideCosResolved;

	private Unit _unitCache;

	private CharacterBaseBodyClient _bodyCache;

	private Transform _pants;

	private Transform _gloves;

	private Transform _boots;

	private Transform _chest;

	private Transform _helmet;

	private Transform _mount;

	private Transform _sword;

	private Transform _blade;

	private Transform _dagger;

	private Transform _spear;

	private Transform _stick;

	private Transform _gaunt;

	private Transform _bow;

	private readonly HashSet<WearPositionEnum> _hairHidingSlots;

	private readonly HashSet<WearPositionEnum> _faceHidingSlots;

	private readonly Dictionary<WearPositionEnum, GameObject> _activeVfx;

	public ItemObject itemObject;

	private bool _hasWeapon;

	private PlayerControllerClient _pccCache;

	private AnimationType _lastAniSubType;

	private int _idleWithWeaponHash;

	private bool _idleWithWeaponHashInit;

	private bool _lastAnimWeapon;

	private bool _animWeaponInit;

	private static int[] s_idleWithWeaponHashByType;

	private static int[][] s_weaponAnimHashes;

	private int _lastWeaponCheckFrame;

	private bool _memoryDeferred;

	private const int MaxMergeRetries = 2;

	private int _mergeFailures;

	private Transform _sceneRoot;

	private bool _warnedNoSceneRoot;

	private bool _isMounted;

	private bool _mountBuilt;

	private ushort _mountSkinMain;

	private ushort _mountSkinSub;

	public static bool HideCostumeSet => false;

	private Unit CachedUnit => null;

	private CharacterBaseBodyClient CachedBody => null;

	public bool HasWeapon
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public bool HasCombinedGear => false;

	public bool IsMemoryDeferred => false;

	public bool IsStagedMergeInFlight => false;

	public bool IsMergeWaitingOnJob => false;

	public bool IsGearSettled => false;

	public bool MergeWanted => false;

	public static void SetHideCostumeSet(bool hide)
	{
	}

	private void FixedUpdate()
	{
	}

	public void CheckAnimationSub()
	{
	}

	private static int IdleWithWeaponHash(AnimationType t)
	{
		return 0;
	}

	private static int WeaponAnimHash(AnimationType t, int which)
	{
		return 0;
	}

	public void EmptyWeaponChecker()
	{
	}

	private SlotBack GetWeaponSlotBack()
	{
		return SlotBack.slot_rhand;
	}

	public void Initialize()
	{
	}

	private void OnDestroy()
	{
	}

	private void CancelPendings()
	{
	}

	private void DestroyCombined(ref SkinnedMeshRenderer renderer)
	{
	}

	public void ClearMemoryDeferral()
	{
	}

	public void ReleaseCombinedForMemory()
	{
	}

	private void MarkGearDirty()
	{
	}

	public void RequestDeferredCombine()
	{
	}

	public bool TickPendingCombine()
	{
		return false;
	}

	private void FinalizeMerge(SkinnedMeshCombiner.PendingCombine pending, ref SkinnedMeshRenderer target)
	{
	}

	public bool TryStartCombineSetup()
	{
		return false;
	}

	public bool RunGearCombine()
	{
		return false;
	}

	public void ClearEquipment()
	{
	}

	private static void DestroySlot(ref Transform slot)
	{
	}

	private void CleanupAllVfx()
	{
	}

	private void CleanupVfx(WearPositionEnum wearPos)
	{
	}

	public void OnEquipItem(SItemCustom itemCustom, byte equipSlot)
	{
	}

	public void OnEquipItem(in SDROP_PUTON putOn, byte equipSlot)
	{
	}

	private static int GetSimpleSlotFromItemSlot(byte itemSlot)
	{
		return 0;
	}

	private void CheckCharacterHideSet(WearPositionEnum wearPos, ItemObject realItem, ItemObject visualItem, byte slot, byte upgrade = 0)
	{
	}

	private void OnItemSlotUpdate(WearPositionEnum wearPos, ItemObject realItem, ItemObject visualItem, int index, byte upgrade)
	{
	}

	private void OnRemoveItem(WearPositionEnum wearPos, int index)
	{
	}

	private Transform GetBadgeParent()
	{
		return null;
	}

	private static Transform FindChildByName(Transform root, string name, int maxDepth)
	{
		return null;
	}

	private void ApplyBadgeMountOffset()
	{
	}

	private Transform SetCombiner(Transform tSlot, ItemObject itemObject, WearPositionEnum wPos, byte upgrade)
	{
		return null;
	}

	private void NotifyControllerRenderersChanged()
	{
	}

	public void SetMountMesh(bool on, ushort skinMain, ushort skinSub)
	{
	}

	public void ApplyHideCostumeSet()
	{
	}

	private void ResolveHideCostumeParts()
	{
	}

	private static Transform FindChildDeep(Transform root, string name)
	{
		return null;
	}

	private static void SetLimbVisible(Transform limb, bool visible)
	{
	}

	private static void SetLimbVfxVisible(Transform limb, bool visible)
	{
	}

	private static void SetLimbGlow(Transform limb, bool enabled)
	{
	}

	private void OnItemSlotHideUpdate(WearPositionEnum wearPos, int index)
	{
	}

	private Transform SetHideCombiner(Transform tSlot, HideSetObject itemObject, WearPositionEnum wPos)
	{
		return null;
	}

	private static string GetEquipObjectName(WearPositionEnum wearPos, ItemObject itemObject)
	{
		return null;
	}

	private void CollectGearParts(List<SkinnedMeshRenderer> list)
	{
	}

	private void AddBodyPart(SkinnedMeshRenderer smr, bool visible, List<SkinnedMeshRenderer> list, List<SkinnedMeshRenderer> noFold)
	{
	}

	private void AddActiveBodyParts(GameObject[] gos, List<SkinnedMeshRenderer> list, List<SkinnedMeshRenderer> noFold)
	{
	}

	private void AddLimbRenderer(Transform limb, List<SkinnedMeshRenderer> list, List<SkinnedMeshRenderer> noFold = null)
	{
	}

	private void FlushGearCombine()
	{
	}

	private bool IsGearCombined(Transform limb)
	{
		return false;
	}

	private bool IsGearCombined(SkinnedMeshRenderer renderer)
	{
		return false;
	}

	private static ulong BuildGearSignature(List<SkinnedMeshRenderer> parts)
	{
		return 0uL;
	}
}
