using System;
using UnityEngine;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class PAData
	{
		[SerializeField]
		private ushort _min;

		[SerializeField]
		private ushort _max;

		public ushort Min
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public ushort Max
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public PAData()
		{
		}

		public PAData(ushort low, ushort high)
		{
		}

		public void Limit()
		{
		}

		public void CheckMinimum(int minimum)
		{
		}

		public void ToFull()
		{
		}

		public void Decrease(ushort value)
		{
		}

		public void Increase(ushort value, bool rate = false)
		{
		}

		public ushort Variation(int value)
		{
			return 0;
		}

		public void VarParam(int value)
		{
		}

		public void Reset()
		{
		}
	}
}
