using System;
using Core.Enumerators;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class AbnormalCondition
	{
		public AbnormalConditionEnum type;

		public float param1;

		public float param2;

		public float param3;

		public float param4;
	}
}
