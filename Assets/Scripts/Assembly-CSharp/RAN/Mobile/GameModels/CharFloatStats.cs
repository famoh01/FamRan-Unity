using System;
using RAN.Mobile.Enumerators;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public struct CharFloatStats
	{
		public float Pow;

		public float Str;

		public float Spi;

		public float Dex;

		public float Int;

		public float Sta;

		public CharFloatStats(float _wPow, float _wStr, float _wSpi, float _wDex, float _wfloat, float _wSta)
		{
			Pow = 0f;
			Str = 0f;
			Spi = 0f;
			Dex = 0f;
			Int = 0f;
			Sta = 0f;
		}

		public CharFloatStats Clone()
		{
			return default;
		}

		public float GET(CharStatsEnum emID)
		{
			return 0f;
		}

		public static CharFloatStats operator +(CharFloatStats lvalue, int rvalue)
		{
			return default;
		}

		public static CharFloatStats operator *(CharFloatStats lvalue, float rvalue)
		{
			return default;
		}

		public static CharFloatStats operator *(CharFloatStats lvalue, int rvalue)
		{
			return default;
		}

		public CharFloatStats AddStat(CharStatsEnum statsEnum, float value)
		{
			return default;
		}
	}
}
