using System;
using UnityEngine;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class PAFData
	{
		[SerializeField]
		private long _min;

		[SerializeField]
		private long _max;

		public long Min
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		public long Max
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		public PAFData()
		{
		}

		public PAFData(long low, long high)
		{
		}

		public void Limit()
		{
		}

		public void CheckMinimum(int minimum)
		{
		}

		public bool IsOver()
		{
			return false;
		}

		public void Decrease(int value)
		{
		}
	}
}
