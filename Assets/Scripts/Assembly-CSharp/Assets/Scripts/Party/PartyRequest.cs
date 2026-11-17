using UnityEngine;

namespace Assets.Scripts.Party
{
	public class PartyRequest : MonoBehaviour
	{
		public static PartyRequest Instance;

		public GameObject _partyBtn;

		private GameObject _askPartyCanvas;

		private GameObject _partyItemDropdown;

		private GameObject _coinItemDropdown;

		private GameObject _partyQuestion;

		private GameObject _partyOkayBtn;

		private GameObject _partyCancelBtn;

		public void Awake()
		{
		}

		private void ShowAskPartyCanvas()
		{
		}

		public void ShowAskPartyCanvas(Unit targetUnit)
		{
		}

		private void SendPartyInvite(Unit targetUnit)
		{
		}

		private void CancelPartyRequest()
		{
		}
	}
}
