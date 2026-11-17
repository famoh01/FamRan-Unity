using System;
using Core.Enumerators;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class ClassRequirementFlag
	{
		public BrightEnum attribute;

		public bool brawlerMale;

		public bool swordsmanMale;

		public bool archerMale;

		public bool shamanMale;

		public bool brawlerFemale;

		public bool swordsmanFemale;

		public bool archerFemale;

		public bool shamanFemale;
	}
}
