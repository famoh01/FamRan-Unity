using System;
using Core.Enumerators;

namespace RAN.Mobile.GameModels.Item
{
	[Serializable]
	public class ItemCooldownSettings
	{
		public ItemCooldownTypeEnum type;

		public ushort cooldown;
	}
}
