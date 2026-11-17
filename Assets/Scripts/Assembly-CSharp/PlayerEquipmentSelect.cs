using System.Collections.Generic;
using Core.Enumerators;
using Models.Library.Shared.Game;
using UnityEngine;

public class PlayerEquipmentSelect : MonoBehaviour
{
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

	private BoneCombiner _boneCombiner;

	private CharacterBaseBody _bodyCache;

	private Transform _pants;

	private Transform _gloves;

	private Transform _boots;

	private Transform _chest;

	private Transform _helmet;

	private Transform _sword;

	private Transform _blade;

	private Transform _dagger;

	private Transform _spear;

	private Transform _stick;

	private Transform _gaunt;

	private Transform _bow;

	public bool HasWeapon;

	public GameObject baseBody;

	public GameObject baseFace;

	public GameObject baseHair;

	public CharacterClassEnum charClass;

	public int hairType;

	public int faceType;

	private readonly HashSet<WearPositionEnum> _hairHidingSlots;

	private readonly HashSet<WearPositionEnum> _faceHidingSlots;

	private ItemObject itemObjectGlobal;

	public AnimationType ANISUBTYPECLIENT;

	private Animator _animatorCache;

	private AnimationType _lastAniSubType;

	private int _idleWithWeaponHash;

	private bool _idleWithWeaponHashInit;

	private bool _lastAnimWeapon;

	private bool _animWeaponInit;

	private static int[] s_idleWithWeaponHashByType;

	private CharacterBaseBody CachedBody => null;

	private SlotBack GetWeaponSlotBack()
	{
		return SlotBack.slot_rhand;
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

	public void EmptyWeaponChecker()
	{
	}

	private void Start()
	{
	}

	public void Initialize()
	{
	}

	public void OnEquipItem(SItemCustom item, int slotIndex, WearPositionEnum wearPos)
	{
	}

	private void OnRemoveItem(WearPositionEnum wearPos, int index)
	{
	}

	private Transform SetCombiner(Transform tSlot, ItemObject itemObject, WearPositionEnum wPos, byte upgrade)
	{
		return null;
	}

	private static string GetEquipObjectName(WearPositionEnum wearPos)
	{
		return null;
	}
}
