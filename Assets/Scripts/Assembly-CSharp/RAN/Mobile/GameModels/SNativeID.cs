using System;
using UnityEngine;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class SNativeID
	{
		private static SNativeID idNull;

		[SerializeField]
		private ushort mainId;

		[SerializeField]
		private ushort subId;

		public static SNativeID ID_Null
		{
			get
			{
				if (idNull == null)
				{
					idNull = new SNativeID(0, 0);
				}
				return idNull;
			}
			set
			{
				idNull = value;
			}
		}

		public ushort MainId
		{
			get
			{
				return mainId;
			}
			set
			{
				mainId = value;
			}
		}

		public ushort SubId
		{
			get
			{
				return subId;
			}
			set
			{
				subId = value;
			}
		}

		public SNativeID()
		{
		}

		public SNativeID(ushort id)
		{
			// packed DWORD: high word = main, low word = sub (SNATIVEID::dwID convention)
			mainId = (ushort)(id >> 16);
			subId = (ushort)(id & 0xFFFF);
		}

		public SNativeID(ushort mainId, ushort subId)
		{
			this.mainId = mainId;
			this.subId = subId;
		}

		public override string ToString()
		{
			return mainId + ":" + subId;
		}

		public bool IsEqual(SNativeID sNativeID)
		{
			return sNativeID != null && sNativeID.mainId == mainId && sNativeID.subId == subId;
		}

		public bool IsNotEqual(SNativeID sNativeID)
		{
			return !IsEqual(sNativeID);
		}

		public bool IsValidNativeID()
		{
			return mainId != 0xFFFF && subId != 0xFFFF;
		}

		public void Reset()
		{
			mainId = 0;
			subId = 0;
		}

		public static implicit operator bool(SNativeID obj)
		{
			return obj != null && obj.IsValidNativeID();
		}
	}
}
