using System;
using Core.Enumerators;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public struct SkillImpacts
	{
		public SkillImpactAddOnEnum impactAddOnEnum;

		public float[] addOnVar;

		public float[] addOnVar2;

		public static SkillImpacts Create()
		{
			return default;
		}
	}
}
