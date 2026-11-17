using System;
using Models.Library.Shared.Game;
using Models.Network.Shared.Enums;
using Models.Network.Shared.Interface;

namespace TrinityRift.Network
{
	public class S_CharacterInventoryData : IMessage
	{
		public const int EquipSlotCount = 20;

		public const int EquipBlobSize = 1360;

		public SItemCustom[] EquipItems;

		public SItemCustom[] InvenItems;

		public EventTypesEnum Type => EventTypesEnum.None;

		public static IMessage ReadFrom(ReadOnlySpan<byte> data)
		{
			return null;
		}

		public void Reset()
		{
		}
	}
}
