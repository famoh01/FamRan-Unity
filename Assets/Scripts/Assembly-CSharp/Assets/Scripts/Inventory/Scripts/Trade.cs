namespace Assets.Scripts.Inventory.Scripts
{
	public class Trade
	{
		public uint targetId;

		public bool isAgree;

		public bool isLock;

		public float blockTimer;

		public InventoryObject tradeBox;

		public long money;

		public void Reset()
		{
		}

		public bool IsValid()
		{
			return false;
		}

		public void SetTarget(uint targetID)
		{
		}

		public uint GetTarget()
		{
			return 0u;
		}

		public bool IsAgreeAble()
		{
			return false;
		}

		public bool SetAgree(bool value = true)
		{
			return false;
		}

		public void ReSetAgree()
		{
		}

		public bool GetAgree()
		{
			return false;
		}

		public void SetLock()
		{
		}

		public void ReSetLock()
		{
		}

		public bool GetLock()
		{
			return false;
		}

		public void SetBlockTime()
		{
		}

		public void BlockAgree()
		{
		}

		public void SetMoney(long nMoney)
		{
		}

		public long GetMoney()
		{
			return 0L;
		}

		public bool FrameMove(float fEllapsedTime = 0f)
		{
			return false;
		}
	}
}
