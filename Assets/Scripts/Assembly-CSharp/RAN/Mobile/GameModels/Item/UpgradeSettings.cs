using System;
using Core.Enumerators;

namespace RAN.Mobile.GameModels.Item
{
	[Serializable]
	public class UpgradeSettings
	{
		public ushort remodelCount;

		public GrindingLevelEnum levelEnum;

		public GrindingClassEnum classEnum;

		public GrindingTypeEnum typeEnum;

		public bool noFail;
	}
}
