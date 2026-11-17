using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Core.Enumerators;
using Models.Library.Shared.Game;
using Models.Network.Shared.Response;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CInitializeWhenReady_003Ed__64 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PlayerEquipment _003C_003E4__this;

		private float _003Cwaited_003E5__2;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CInitializeWhenReady_003Ed__64(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	private InventoryObject _equipment;

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

	public Transform rightHandEffectContainer;

	public Transform leftHandEffectContainer;

	public Transform rightFootEffectContainer;

	public Transform leftFootEffectContainer;

	public Transform mainBodyEffectContainer;

	public Transform mainBodyParentEffectContainer;

	public Transform floorEffectContainer;

	[Tooltip("Fixed height of the badge VFX above the model's Scene_Root while on foot.")]
	public float badgeHeight;

	[Tooltip("Extra height added to the badge VFX while the player is riding a board/mount.")]
	public float mountedBadgeHeightOffset;

	public Transform projectileContainer;

	public Transform swordEffectContainer;

	public Transform swordBoneEffectContainer;

	public Transform stickEffectContainer;

	public Transform stickBoneEffectContainer;

	private BoneCombiner _boneCombiner;

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

	private SlotBack _weaponSlotBack;

	private SkinnedMeshRenderer _combinedGear;

	private bool _combineDirty;

	private ulong _lastGearSig;

	private bool _hasGearSig;

	private SkinnedMeshCombiner.PendingCombine _pendingCombine;

	private readonly List<SkinnedMeshRenderer> _gearParts;

	private readonly HashSet<SkinnedMeshRenderer> _previousCombineSources;

	private readonly List<SkinnedMeshRenderer> _noFoldParts;

	private readonly HashSet<WearPositionEnum> _hairHidingSlots;

	private readonly HashSet<WearPositionEnum> _faceHidingSlots;

	private readonly Dictionary<WearPositionEnum, GameObject> _activeVfx;

	private Animator _animatorCache;

	private PlayerController _playerControllerCache;

	private InventoryData _inventoryDataCache;

	private AnimationType _lastAniSubType;

	private int _idleWithWeaponHash;

	private bool _idleWithWeaponHashInit;

	private static readonly int[] _itemSlotToSimpleSlot;

	private Transform _sceneRoot;

	private bool _warnedNoSceneRoot;

	private bool _isMounted;

	private const int MaxMergeRetries = 2;

	private int _mergeFailures;

	private void FixedUpdate()
	{
	}

	private void InitializeItems()
	{
	}

	public void OnItemSlotUpdateNew(F2C_ItemSlotUpdateResponse resp)
	{
	}

	public void OnItemDropToFieldConfirmed(ItemSlotTypeEnum slotType, int index, SItemCustom emptyItem)
	{
	}

	public void UpdateSlotFromResponse(ItemSlotTypeEnum slotType, int index, SItemCustom itemCustom)
	{
	}

	public void EmptyWeaponChecker()
	{
	}

	private void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CInitializeWhenReady_003Ed__64))]
	private IEnumerator InitializeWhenReady()
	{
		return null;
	}

	private void OnDestroy()
	{
	}

	private void LateUpdate()
	{
	}

	private void MarkGearDirty()
	{
	}

	private void OnEquipItem(InventorySlot slot)
	{
	}

	private void OnRemoveItem(InventorySlot slot)
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

	private Transform SetCombiner(Transform tSlot, InventorySlot iItem)
	{
		return null;
	}

	public void SetMountMesh(bool on)
	{
	}

	private static void HideBaseBodyPart(WearPositionEnum wearPos)
	{
	}

	private void CleanupVfx(WearPositionEnum wearPos)
	{
	}

	private static SlotBack GetSlotBack(ItemObject itemObject)
	{
		return SlotBack.slot_rhand;
	}

	private static string GetEquipObjectName(WearPositionEnum wearPos, ItemObject itemObject)
	{
		return null;
	}

	private void FlushGearCombine()
	{
	}

	private void OnMergeFailed()
	{
	}

	private bool FinalizePending()
	{
		return false;
	}

	public void ApplyHairTintToCombined(Color color)
	{
	}

	private void CollectContainerRenderers(List<SkinnedMeshRenderer> list)
	{
	}

	private void AddBodyPart(SkinnedMeshRenderer smr, bool visible, List<SkinnedMeshRenderer> list, List<SkinnedMeshRenderer> noFold)
	{
	}

	private void AddActiveBodyParts(GameObject[] gos, List<SkinnedMeshRenderer> list, List<SkinnedMeshRenderer> noFold)
	{
	}

	private void AddTrackedLimb(Transform limb, List<SkinnedMeshRenderer> list, List<SkinnedMeshRenderer> noFold = null)
	{
	}

	private static ulong BuildGearSignature(List<SkinnedMeshRenderer> parts)
	{
		return 0uL;
	}
}
