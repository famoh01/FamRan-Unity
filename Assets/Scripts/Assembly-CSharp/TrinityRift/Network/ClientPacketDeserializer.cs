using Models.Network.Shared;
using Models.Network.Shared.Enums;
using Models.Network.Shared.Interface;

namespace TrinityRift.Network
{
	public static class ClientPacketDeserializer
	{
		private static readonly PacketReadFromHandler[] _handlers;

		public static void Register(EventTypesEnum opcode, PacketReadFromHandler reader)
		{
		}

		public static IMessage Deserialize(ushort opcode, byte[] data, int offset, int length)
		{
			return null;
		}

		public static void RegisterAll()
		{
		}
	}
}
