using System;
using Newtonsoft.Json;

namespace Assets.Scripts.Mob_Scripts
{
	[Serializable]
	public class SGenNum
	{
		public ushort num;

		[NonSerialized]
		[JsonIgnore]
		public float fRateLow;

		public float fRateHigh;
	}
}
