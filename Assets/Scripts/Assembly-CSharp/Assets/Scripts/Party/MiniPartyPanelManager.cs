using System.Collections.Generic;
using RAN.Mobile.GameModels;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Party
{
	public class MiniPartyPanelManager : MonoBehaviour
	{
		public static MiniPartyPanelManager Instance;

		[Header("Canvas & Layout")]
		public Canvas miniPartyCanvas;

		[Header("Dynamic Resizing")]
		public RectTransform panelBaseRect;

		public GameObject memberSlotContainer;

		public float emptyPanelHeight;

		public float singleSlotHeight;

		public float maxPanelHeight;

		[Header("Pre-allocated Slots")]
		public MiniPartyMemberSlot[] memberSlots;

		public Sprite[] memberNumberSprites;

		[Header("Text & Button Visuals")]
		public TextMeshProUGUI toggleButtonText;

		public TextMeshProUGUI headerTitleText;

		private List<PartyMemberInfo> _currentMembers;

		private bool isMinimized;

		private readonly List<PartyMemberInfo> _displayOrder;

		private void Awake()
		{
		}

		public void UpdatePartyData(List<PartyMemberInfo> members)
		{
		}

		public void MarkDirty()
		{
		}

		public void ToggleMinimize()
		{
		}

		private void RefreshPanel()
		{
		}
	}
}
