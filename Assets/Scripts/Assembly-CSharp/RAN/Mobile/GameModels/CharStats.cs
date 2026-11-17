using System;
using RAN.Mobile.Enumerators;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public struct CharStats
	{
		public ushort Pow;

		public ushort Str;

		public ushort Spi;

		public ushort Dex;

		public ushort Int;

		public ushort Sta;

		public CharStats(ushort _wPow, ushort _wStr, ushort _wSpi, ushort _wDex, ushort _wInt, ushort _wSta)
		{
			Pow = 0;
			Str = 0;
			Spi = 0;
			Dex = 0;
			Int = 0;
			Sta = 0;
		}

		public bool CHECK_REQ(CharStats sREQ, int waccept = 0)
		{
			return false;
		}

		public void RESET()
		{
		}

		public int GET(CharStatsEnum emID)
		{
			return 0;
		}

		public int GetTotal()
		{
			return 0;
		}

		public bool IsZERO()
		{
			return false;
		}

		public CharStats Clone()
		{
			return default;
		}

		public static CharStats operator +(CharStats lvalue, CharStats rvalue)
		{
			return default;
		}

		public static CharStats operator *(CharStats lvalue, float rvalue)
		{
			return default;
		}

		public static CharStats operator +(CharStats lvalue, int rvalue)
		{
			return default;
		}

		public static CharStats operator *(CharStats lvalue, int rvalue)
		{
			return default;
		}

		public static CharStats operator +(CharStats lvalue, CharFloatStats rvalue)
		{
			return default;
		}
	}
}
