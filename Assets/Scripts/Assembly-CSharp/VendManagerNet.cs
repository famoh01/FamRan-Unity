using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Models.Library.Shared.Enums;
using Models.Library.Shared.Game;
using Models.Network.Shared.Response;
using TrinityRift.Network;
using UnityEngine;

public class VendManagerNet : MonoBehaviour
{
	public class VendorMirror
	{
		public string Title;

		public bool IsOpen;

		public readonly SItemCustom[] Items;

		public readonly long[] Prices;

		public readonly uint[] Quantities;

		public readonly bool[] Sold;

		public readonly bool[] Populated;

		public void ResetItems()
		{
		}
	}

	public const int MaxSaleSlots = 24;

	public readonly SItemCustom[] SaleItems;

	public readonly long[] SalePrices;

	public readonly uint[] SaleQuantities;

	public readonly ushort[] SaleInvenSlots;

	public readonly bool[] SaleSold;

	public bool LocalShopOpen;

	public string LocalShopTitle;

	public readonly Dictionary<ushort, VendorMirror> RemoteVendors;

	public ushort CurrentViewedVendor;

	[CompilerGenerated]
	private Action<string> m_OnTitleConfirmed;

	[CompilerGenerated]
	private Action<VendingRegItemResult, ushort, ushort, long, uint> m_OnRegItemResult;

	[CompilerGenerated]
	private Action<ushort> m_OnDisItemConfirmed;

	[CompilerGenerated]
	private Action<VendingOpenResult> m_OnOpenResult;

	[CompilerGenerated]
	private Action<ushort, string> m_OnVendorOpened;

	[CompilerGenerated]
	private Action<ushort> m_OnVendorClosed;

	[CompilerGenerated]
	private Action<VendingBuyResult, ushort, ushort, uint> m_OnBuyResult;

	[CompilerGenerated]
	private Action<ushort, ushort, uint, bool, string> m_OnVendorItemUpdate;

	[CompilerGenerated]
	private Action<ushort> m_OnVendorItemListComplete;

	private ServerConnectionManager _boundScm;

	public static VendManagerNet Instance { get; private set; }

	public static bool IsLocalVending => false;

	public event Action<string> OnTitleConfirmed
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<VendingRegItemResult, ushort, ushort, long, uint> OnRegItemResult
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<ushort> OnDisItemConfirmed
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<VendingOpenResult> OnOpenResult
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<ushort, string> OnVendorOpened
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<ushort> OnVendorClosed
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<VendingBuyResult, ushort, ushort, uint> OnBuyResult
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<ushort, ushort, uint, bool, string> OnVendorItemUpdate
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<ushort> OnVendorItemListComplete
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	private static SItemCustom[] AllocGrid()
	{
		return null;
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	private void TryBindScm()
	{
	}

	private void UnbindScm()
	{
	}

	public void ClearRemoteVendorState()
	{
	}

	public void OnRemotePlayerDespawned(ushort gaeaId)
	{
	}

	public void ResetLocalState()
	{
	}

	public void SendTitle(string title)
	{
	}

	public void SendRegItem(ushort invenSlot, long price, uint quantity)
	{
	}

	public void SendDisItem(ushort saleSlot)
	{
	}

	public void SendOpen(ushort permitSlot)
	{
	}

	public void SendClose()
	{
	}

	public void SendItemInfo(ushort vendorGaeaId)
	{
	}

	public void SendBuy(ushort vendorGaeaId, ushort saleSlot, uint quantity)
	{
	}

	private void HandleTitleFB(F2C_PMarketTitleFB msg)
	{
	}

	private void HandleRegItemFB(F2C_PMarketRegItemFB msg)
	{
	}

	private void HandleDisItemFB(F2C_PMarketDisItemFB msg)
	{
	}

	private void HandleOpenFB(F2C_PMarketOpenFB msg)
	{
	}

	private void HandleOpenBrd(F2C_PMarketOpenBrd msg)
	{
	}

	private void HandleCloseBrd(F2C_PMarketCloseBrd msg)
	{
	}

	private void HandleBuyFB(F2C_PMarketBuyFB msg)
	{
	}

	private void HandleItemInfoBrd(F2C_PMarketItemInfoBrd msg)
	{
	}

	private void HandleItemUpdateBrd(F2C_PMarketItemUpdateBrd msg)
	{
	}
}
