using System;
using Newtonsoft.Json;

namespace Assets.Scripts.Mob_Scripts
{
	[Serializable]
	public class SGItemSpec
	{
		public short specId;

		[NonSerialized]
		[JsonIgnore]
		public float fRateLow;

		public float fRateHigh;

		public bool isSpecId;
	}
}
