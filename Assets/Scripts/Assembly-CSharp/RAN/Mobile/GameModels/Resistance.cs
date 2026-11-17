using System;
using RAN.Mobile.Enumerators;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public struct Resistance
	{
		public byte Electric;

		public byte Fire;

		public byte Ice;

		public byte Poison;

		public byte Wind;

		public Resistance(byte _nFire, byte _nIce, byte _nElectric, byte _nPoison, byte _nSpirit)
		{
			Electric = 0;
			Fire = 0;
			Ice = 0;
			Poison = 0;
			Wind = 0;
		}

		public void RESET()
		{
		}

		public short GetElement(ElementEnum emElement)
		{
			return 0;
		}

		public static Resistance operator +(Resistance left, Resistance right)
		{
			return default;
		}

		public static Resistance operator +(Resistance left, int scalar)
		{
			return default;
		}

		public void LIMIT()
		{
		}
	}
}
