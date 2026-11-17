using Models.Network.Shared.Response;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class ItemUpgradeManager : MonoBehaviour
	{
		public static ItemUpgradeManager Instance;

		public Button grindButton;

		public Button grindExitButton;

		private void Awake()
		{
		}

		public void GrindingRegisterItem(InventorySlot item1, InventorySlot item2)
		{
		}

		private void OnGrindItem()
		{
		}

		private void OnGrindItemExit()
		{
		}

		public void CancelAndCloseUpgrade()
		{
		}

		public void GrindingRemoveItem(InventorySlot grindSlot)
		{
		}

		public void ClearGrindingSlots()
		{
		}

		public void OnGrindingResponse(F2C_GrindingResponse msg)
		{
		}
	}
}
