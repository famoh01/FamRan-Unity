using System;
using Core.Enumerators;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public struct SkillSpecs
	{
		public SkillSpecAddOnEnum specAddOnEnum;

		public Spec[] spec;

		public static SkillSpecs Create()
		{
			return default;
		}
	}
}
