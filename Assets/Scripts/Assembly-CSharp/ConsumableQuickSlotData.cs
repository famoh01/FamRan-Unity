using UnityEngine;

public class ConsumableQuickSlotData : MonoBehaviour
{
	private readonly int[] _counts;

	private const float RefreshIntervalSeconds = 0.25f;

	private float _nextRefreshTime;

	private bool _wired;

	private static InventoryObject Container => null;

	public void OnEnable()
	{
	}

	private bool EnsureWired()
	{
		return false;
	}

	public bool SetSlot(int index, ItemObject itemObject)
	{
		return false;
	}

	public void UseSlot(InventorySlot quickSlot)
	{
	}

	public void ClearSlot(InventorySlot quickSlot)
	{
	}

	public void FixedUpdate()
	{
	}

	private static InventorySlot FindInInventory(int mainId, int subId)
	{
		return null;
	}

	private static void Feedback(string message)
	{
	}
}
