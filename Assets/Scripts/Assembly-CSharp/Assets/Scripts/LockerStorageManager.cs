using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class LockerStorageManager : MonoBehaviour
	{
		public static LockerStorageManager Instance;

		public byte tabIndex;

		public long money;

		private ushort _currentNpcId;

		private float _lastTabSwitchTime;

		private const float TabCooldown = 0.5f;

		private const int ChannelCount = 5;

		private readonly long[] _channelExpirationTicks;

		private Behaviour _panelToRestoreOnClose;

		[Header("UI References")]
		public Canvas locker;

		public TMP_Text moneyTxt;

		public Button tab1;

		public Button tab2;

		public Button tab3;

		public Button tab4;

		public Button tab5;

		public Button inventoryMoneyButton;

		public Button moneyButton;

		public Canvas lockerInventoryCanvas;

		[Header("Tab Visuals")]
		[Tooltip("Place your 5 normal/unselected tab sprites here (Order: Tab 1 to 5)")]
		public Sprite[] unselectedTabSprites;

		[Tooltip("Place your 5 active/selected tab sprites here (Order: Tab 1 to 5)")]
		public Sprite[] selectedTabSprites;

		private void Awake()
		{
		}

		public void InitializeLockerStorage(ushort npcId)
		{
		}

		public void OpenLockerInventory(Behaviour panelToRestore = null)
		{
		}

		public void CloseLockerInventory()
		{
		}

		private bool RequireGetLockerStorage(byte channel, ushort npcId)
		{
			return false;
		}

		public void SetStorageMoney(long money)
		{
		}

		private void DepositMoney()
		{
		}

		private void WithdrawMoney()
		{
		}

		public void ShowLockerStorage(byte channel = 0)
		{
		}

		private void ResetLockerStorageSlots()
		{
		}

		public void SetChannelExpirations(long[] ticks)
		{
		}

		public void SetChannelExpiration(byte channel, long ticks)
		{
		}

		public bool IsChannelRented(byte channel)
		{
			return false;
		}

		private void ApplyChannelDim(byte channel)
		{
		}

		private void UpdateTabVisuals(byte selectedChannel)
		{
		}
	}
}
