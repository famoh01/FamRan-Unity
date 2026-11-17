using System;
using Core.Enumerators;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public struct SkillApply
	{
		public SkillEMTypesEnum basicType;

		public ElementEnum element;

		public SkillApplyDataLevel[] dataLevel;

		public uint cureFlag;

		public StateBlowEnum stateBlow;

		public SkillStateBlow[] stateBlows;

		public SkillImpacts[] impacts;

		public SkillSpecs[] specs;

		public uint unknownData;

		public static SkillApply Create()
		{
			return default;
		}
	}
}
