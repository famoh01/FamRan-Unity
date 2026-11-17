using System;
using UnityEngine;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class DefaultCharSetUpgrade
	{
		[SerializeField]
		private byte attack;

		[SerializeField]
		private byte defense;

		public byte Attack
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public byte Defense
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}
	}
}
