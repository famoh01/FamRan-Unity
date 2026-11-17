using Assets.Scripts.Inventory.Scripts;
using Models.Network.Shared.Response;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class TradeManager : MonoBehaviour
	{
		public static TradeManager instance;

		public TMP_Text myTextName;

		public TMP_Text targetTextName;

		public TMP_Text myTextMoney;

		public TMP_Text targetTextMoney;

		public Button tradeButton;

		public Button tradeMoneyButton;

		public Button tradeAcceptButton;

		public Button tradeRefuseButton;

		public Button tradeLockButton;

		public Button targetAcceptButton;

		public Button targetTradeLockButton;

		public bool bTrade;

		public Trade myTrade;

		public Trade targetTrade;

		public uint masterGaeaId;

		public string targetName;

		public string myName;

		public Canvas inventoryCanvas;

		public static TradeManager Instance => null;

		public string GetTargetName()
		{
			return null;
		}

		public string GetMyName()
		{
			return null;
		}

		public bool Valid()
		{
			return false;
		}

		private void Awake()
		{
		}

		public void SetTradeInfo(string targetName, uint gaeaId)
		{
		}

		public bool ResetData()
		{
			return false;
		}

		public bool FrameMove(float fEllapsedTime = 0f)
		{
			return false;
		}

		public void UpdateUI()
		{
		}

		private void LockTrade()
		{
		}

		private void AcceptTrade()
		{
		}

		public bool IsValidTradeInvenSpace()
		{
			return false;
		}

		public bool StartTrade(uint targetGaeaId)
		{
			return false;
		}

		public void SendTradeRequest()
		{
		}

		public void CancelTrade()
		{
		}

		public void BlockAgree()
		{
		}

		public bool IsAgreeAble()
		{
			return false;
		}

		public bool IsItemInTradeBox(InventorySlot inventorySlot)
		{
			return false;
		}

		public bool RemoveItem(InventorySlot tradeSlot)
		{
			return false;
		}

		private void TradeMoney()
		{
		}

		public bool MoveItemToTrade(InventorySlot inventSlot, InventorySlot tradeSlot)
		{
			return false;
		}

		public void OnTradeTargetResponse(F2C_TradeTargetResponse msg)
		{
		}

		public bool OnTradeFeedbackResponse(F2C_TradeFeedbackResponse msg)
		{
			return false;
		}

		public void OnTradeCancelResponse(F2C_TradeCancelResponse msg)
		{
		}

		public void OnTradeMoneyResponse(F2C_TradeMoneyResponse msg)
		{
		}

		public void OnTradeItemResponse(F2C_TradeItemResponse msg)
		{
		}

		public void OnTradeLockResponse(F2C_TradeLockResponse msg)
		{
		}

		public void OnTradeAgreeResponse(F2C_TradeAgreeResponse msg)
		{
		}

		public void OnTradeCompleteResponse(F2C_TradeCompleteResponse msg)
		{
		}
	}
}
