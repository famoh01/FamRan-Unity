using System;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class DamageSpec
	{
		public float psyDamageReduce;

		public float psyDamageReflection;

		public float psyDamageReflectionRate;

		public float magicDamageReduce;

		public float magicDamageReflection;

		public float magicDamageReflectionRate;

		public void Reset()
		{
		}
	}
}
