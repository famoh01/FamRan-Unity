using System;
using Core.Enumerators;

namespace RAN.Mobile.GameModels.Item
{
	[Serializable]
	public class CharacterConditionRequirements
	{
		public BrightEnum attribute;

		public ushort pow;

		public ushort str;

		public ushort spi;

		public ushort dex;

		public ushort @int;

		public ushort sta;

		public ushort fighatValue;

		public ushort shootValue;

		public ushort levelMin;

		public ushort levelMax;
	}
}
