using System;
using Models.Library.Shared.Game;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public struct SkillLearnLevel
	{
		public uint level;

		public SCharStats stats;

		public uint skp;

		public uint skillLevel;

		public static SkillLearnLevel Create()
		{
			return default;
		}
	}
}
