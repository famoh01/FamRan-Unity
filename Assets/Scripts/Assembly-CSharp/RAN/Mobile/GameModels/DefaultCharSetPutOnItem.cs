using System;
using Core.Enumerators;
using UnityEngine;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class DefaultCharSetPutOnItem
	{
		[SerializeField]
		private ItemSlotEnum position;

		[SerializeField]
		private SNativeID item;

		[SerializeField]
		private ushort quantity;

		[SerializeField]
		private DefaultCharSetUpgrade upgrade;

		[SerializeField]
		private Resistance element;

		public ItemSlotEnum Position
		{
			get
			{
				return ItemSlotEnum.SLOT_HEADGEAR;
			}
			set
			{
			}
		}

		public SNativeID Item
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public ushort Quantity
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public DefaultCharSetUpgrade Upgrade
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public Resistance Element
		{
			get
			{
				return default;
			}
			set
			{
			}
		}
	}
}
