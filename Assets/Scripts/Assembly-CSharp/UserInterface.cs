using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Core.Enumerators;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public abstract class UserInterface : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003COnHoldDownRoutine_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float holdTime;

		public UserInterface _003C_003E4__this;

		public GameObject obj;

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
		public _003COnHoldDownRoutine_003Ed__16(int _003C_003E1__state)
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

	public InventoryObject inventory;

	private InventoryObject _previousInventory;

	public Dictionary<GameObject, InventorySlot> slotsOnInterface;

	private bool isPressed;

	private bool hasHeld;

	private Coroutine holdCoroutine;

	private GameObject selectedSlot;

	private bool isDataLoaded;

	public void OnEnable()
	{
	}

	public void OnDisable()
	{
	}

	private static bool AnyPointerHeld()
	{
		return false;
	}

	public void FixedUpdate()
	{
	}

	public abstract void CreateSlots();

	public void UpdateInventoryLinks()
	{
	}

	public void OnSlotUpdate(InventorySlot slot)
	{
	}

	protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
	{
	}

	[IteratorStateMachine(typeof(_003COnHoldDownRoutine_003Ed__16))]
	private IEnumerator OnHoldDownRoutine(GameObject obj, float holdTime)
	{
		return null;
	}

	private void HandleSplitItem(GameObject obj)
	{
	}

	public void OnEnterInterface(GameObject obj)
	{
	}

	public void OnExitInterface(GameObject obj)
	{
	}

	private Image GetItemImage(GameObject obj)
	{
		return null;
	}

	private void HandleItemByType(GameObject obj, ItemTypeEnum itemType, string information, string lowerInformation, string description, Sprite sprite, InventorySlot inventSlot)
	{
	}

	private void ClickChangeNameCard(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private void ClickQuestionItem(GameObject obj, string information, string lowerInformation, string description, Sprite img, InventorySlot inventSlot)
	{
	}

	private void ClickChangeSchoolCard(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private static bool IsEquipableWearPosition(WearPositionEnum wp)
	{
		return false;
	}

	private static int ResolveEquipmentSlot(ItemObject itemObj)
	{
		return 0;
	}

	private static bool IsLeftHandItem(ItemTypeEnum t)
	{
		return false;
	}

	private static int PickRingSlot()
	{
		return 0;
	}

	private static int FindEmptyInventorySlot()
	{
		return 0;
	}

	private static void ShowEquipFeedback(string text)
	{
	}

	private void ClickEquip(GameObject obj, string information, string lowerInformation, string description, Sprite img, InventorySlot inventSlot)
	{
	}

	private void ClickEquipVehicle(GameObject obj, string information, string lowerInformation, string description, Sprite img, InventorySlot inventSlot)
	{
	}

	private void ClickUnequip(GameObject obj, string information, string lowerInformation, string description, Sprite img, InventorySlot inventSlot)
	{
	}

	private bool IsHoldAction()
	{
		return false;
	}

	private void StopHoldCoroutine()
	{
	}

	private bool IsInvalidClick(GameObject obj)
	{
		return false;
	}

	private void UpdateSelectedSlot(GameObject obj)
	{
	}

	private bool IsTradeDisabled(GameObject obj)
	{
		return false;
	}

	private void DisableUsability(GameObject obj)
	{
	}

	private void ResetDefaultView(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private void ClickPetCard(GameObject obj, string information, string lowerInformation, string description, Sprite img, InventorySlot inventSlot)
	{
	}

	private void ClickLockerRemote(GameObject obj, string information, string lowerInformation, string description, Sprite img, InventorySlot inventSlot)
	{
	}

	private void ClickLockerRental(GameObject obj, string information, string lowerInformation, string description, Sprite img, InventorySlot inventSlot)
	{
	}

	private void ClickPotion(GameObject obj, string information, string lowerInformation, string description, Sprite img, InventorySlot inventSlot)
	{
	}

	private void ClickInventoryRowExpand(GameObject obj, string information, string lowerInformation, string description, Sprite img, InventorySlot inventSlot)
	{
	}

	private void ClickMoveMapCard(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private void ClickRecallCard(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private void ClickSkillPointReset(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private void ClickStatPointReset(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private void ClickSkillStatPointReset(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private void ClickTeleportCard(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private void ClickPrivateStore(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private void ClickItemBox(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private void ClickRandomBox(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private void ClickWrapperBox(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private void ClickBankItem(GameObject obj, string information, string lowerInformation, string description, Sprite img, InventorySlot inventSlot)
	{
	}

	private void ClickGrindingItem(GameObject obj, string info, string lowerInfo, string des, Sprite img)
	{
	}

	private void ClickLearnSkill(GameObject obj, string info, string lowerInfo, string des, Sprite img)
	{
	}

	private void ClickLearnPetSkill(GameObject obj, string info, string lowerInfo, string des, Sprite img)
	{
	}

	private void ClickPetDualSkillCard(GameObject obj, string info, string lowerInfo, string des, Sprite img)
	{
	}

	private void ClickPetFood(GameObject obj, string info, string lowerInfo, string des, Sprite img)
	{
	}

	private void DragDownPrivateStore(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private void DragDownDropItem(InventorySlot slot)
	{
	}

	private void DragDownItemSell(GameObject obj)
	{
	}

	private void DragDownItemGrinding(InventorySlot slot1, InventorySlot slot2)
	{
	}

	private void DragDownItemToTrade(InventorySlot slot1, InventorySlot slot2)
	{
	}

	private void DragDownItemToPotion(InventorySlot slot1, InventorySlot slot2)
	{
	}

	private void DragDownItemToConsumable(InventorySlot slot1, InventorySlot slot2)
	{
	}

	private void DragDownItemCombineCostume(InventorySlot slot1, InventorySlot slot2)
	{
	}

	private void DragDownItemRemoveCostume(InventorySlot slot1, InventorySlot slot2)
	{
	}

	private void DragDownItemUseGarbageCard(InventorySlot slot1, InventorySlot slot2)
	{
	}

	private void DragDownItemUsePetFood(InventorySlot foodSlot, InventorySlot petCardSlot)
	{
	}

	private void DragDownItemUseNonDropCard(InventorySlot slot1, InventorySlot slot2)
	{
	}

	private void DragDownItemUseWrapper(InventorySlot slot1, InventorySlot slot2)
	{
	}

	private void DragDownLockerSwapItem(InventorySlot slot1, InventorySlot slot2)
	{
	}

	private void DragDownCombinePile(InventorySlot slot1, InventorySlot slot2)
	{
	}

	public void OnItemClick(GameObject obj)
	{
	}

	private void ClickCharacterHairStyleCard(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private void ClickCharacterHairColorCard(GameObject obj, string information, string lowerInformation, string description, Sprite img)
	{
	}

	private void TryConfigureNpcSellButton(GameObject obj)
	{
	}

	public void OnDragEnd(GameObject obj)
	{
	}

	public void OnPointerEnter(GameObject obj)
	{
	}

	public void OnPointerExit(GameObject obj)
	{
	}

	public void OnDragBegin(GameObject obj)
	{
	}

	public void OnDrag(GameObject obj)
	{
	}

	public void OnHoldPointerDown(GameObject obj)
	{
	}

	public void OnHoldPointerUp(GameObject obj)
	{
	}
}
