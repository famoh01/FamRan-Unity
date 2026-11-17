using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Models.Library.Shared.Enums;
using RAN.Mobile.GameModels;
using TMPro;
using UnityEngine;

public class VendManager : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CReassertShopUiNextFrame_003Ed__50 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public VendManager _003C_003E4__this;

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
		public _003CReassertShopUiNextFrame_003Ed__50(int _003C_003E1__state)
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

	public static VendManager instance;

	[Header("Start Vending")]
	public GameObject VendingName;

	public GameObject VendingStart;

	[Header("Info Vending")]
	public GameObject SetVendingInfo;

	public GameObject CancelVendingInfo;

	public GameObject CloseVendingInfo;

	public GameObject UnitPrice;

	public GameObject UnitCount;

	public GameObject UnitCountLabel;

	public GameObject InfoVendingCanvas;

	public TMP_Text VendingItemName;

	[Header("Vending")]
	public GameObject VendingDetails;

	public GameObject VendingContent;

	public GameObject BeginVendingBtn;

	public GameObject EndVendingBtn;

	public Dictionary<int, VendItem> VendItems;

	public Dictionary<string, int> VendItemIdAndCount;

	private SNativeID SelectedItemId;

	private int SelectedItemCount;

	private ushort? CurrentSelectedPlayer;

	private bool isBuying;

	private string _viewedVendorName;

	private bool _endConfirmPending;

	private Coroutine _shopUiGuard;

	private bool _selectedIsWrapped;

	private int _selectedSlotIndex;

	private int _selectedMaxPile;

	private int _selectedSlotRemaining;

	private readonly Dictionary<int, int> _saleSlotToVendSlot;

	private static readonly Color SoldTint;

	private const ushort PermitMainId = 84;

	private const ushort PermitSubId = 5;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private int CommittedFromInventorySlot(int inventorySlotIndex)
	{
		return 0;
	}

	private ushort FindPermitSlot()
	{
		return 0;
	}

	public void EnableSetUnitPrice()
	{
	}

	public bool ProcessItemForVend(ItemObject itemObject, int index, Action callBack)
	{
		return false;
	}

	public void AddItemToVend(Item item, int index, bool isWrapped = false)
	{
	}

	public void CloseVendingInfoCanvas()
	{
	}

	public void ResetVendingInfo()
	{
	}

	public void EnableVendingStart()
	{
	}

	public void StartVending()
	{
	}

	public void BeginVending()
	{
	}

	public void EndVending()
	{
	}

	private void ConfirmEndVending()
	{
	}

	private void CancelEndVending()
	{
	}

	private void ClearEndConfirmState()
	{
	}

	private void RestartShopUiGuard()
	{
	}

	[IteratorStateMachine(typeof(_003CReassertShopUiNextFrame_003Ed__50))]
	private IEnumerator ReassertShopUiNextFrame()
	{
		return null;
	}

	private void CloseVending()
	{
	}

	private void ResetOwnShopUi()
	{
	}

	public void BuyersViewVending(Unit unit)
	{
	}

	public void BuyVendItem(InventorySlot slot, VendItem vendItem, int? itemCount = null)
	{
	}

	public void BuyVendItemWithCount(InventorySlot slot, VendItem vendItem)
	{
	}

	private void HandlePMarketOpenResult(VendingOpenResult result)
	{
	}

	private void HandlePMarketRegItemResult(VendingRegItemResult result, ushort invSlot, ushort saleSlot, long price, uint qty)
	{
	}

	private void HandlePMarketBuyResult(VendingBuyResult result, ushort vendor, ushort saleSlot, uint qty)
	{
	}

	private void HandleVendorItemListUpdate(ushort vendorGaea)
	{
	}

	private void HandleVendorItemUpdate(ushort vendorGaea, ushort saleSlot, uint qty, bool sold, string buyerName)
	{
	}

	private void SetVendingHeader(string charName, string title)
	{
	}

	private void HandleVendorOpenedTitle(ushort vendorGaea, string title)
	{
	}

	private void HandleVendorClosed(ushort vendorGaea)
	{
	}

	private void RenderVendorGrid(ushort vendorGaea)
	{
	}

	public void RemoveVendItem(InventorySlot slot)
	{
	}

	public ushort GetVendorCharId()
	{
		return 0;
	}
}
