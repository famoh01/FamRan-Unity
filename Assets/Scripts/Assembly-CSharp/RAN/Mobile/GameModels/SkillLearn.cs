using System;
using Core.Enumerators;
using Models.Library.Shared.Game;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public struct SkillLearn
	{
		public CharacterClassEnum @class;

		public BrightEnum bright;

		public Models.Library.Shared.Game.SNativeID skill;

		public SkillLearnLevel[] levelStep;

		public static SkillLearn Create()
		{
			return default;
		}
	}
}
