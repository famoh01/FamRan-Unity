using System;
using Core.Enumerators;

namespace RAN.Mobile.GameModels.Item
{
	[Serializable]
	public class ItemQuestion
	{
		public ItemQuestionTypeEnum type;

		public ushort param1;

		public ushort param2;

		public float param3;

		public float param4;
	}
}
