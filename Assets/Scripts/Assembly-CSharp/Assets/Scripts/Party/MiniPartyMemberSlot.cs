using RAN.Mobile.GameModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Party
{
	public class MiniPartyMemberSlot : MonoBehaviour
	{
		[Header("UI Components")]
		public Image memberIcon;

		public TextMeshProUGUI ignText;

		public Slider healthBar;

		private int _cachedPlayerId;

		private Button _button;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		public void OnSlotClicked()
		{
		}

		public void UpdateSlot(PartyMemberInfo member, Sprite[] memberNumberSprites, int slotIndex, bool onLocalMap)
		{
		}

		public void ClearSlot()
		{
		}
	}
}
