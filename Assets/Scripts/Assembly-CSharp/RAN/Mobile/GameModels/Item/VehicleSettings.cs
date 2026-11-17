using System;
using Core.Enumerators;

namespace RAN.Mobile.GameModels.Item
{
	[Serializable]
	public class VehicleSettings
	{
		public VehicleType type;

		public VehicleSubType subType;

		public bool noAccessories;

		public ushort defaultColor1;

		public ushort defaultColor2;
	}
}
