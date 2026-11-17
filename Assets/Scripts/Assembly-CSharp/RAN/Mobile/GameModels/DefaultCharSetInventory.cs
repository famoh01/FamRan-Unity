using System;
using UnityEngine;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class DefaultCharSetInventory
	{
		[SerializeField]
		private SNativeID item;

		[SerializeField]
		private ushort quantity;

		[SerializeField]
		private DefaultCharSetUpgrade upgrade;

		[SerializeField]
		private Resistance element;

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
