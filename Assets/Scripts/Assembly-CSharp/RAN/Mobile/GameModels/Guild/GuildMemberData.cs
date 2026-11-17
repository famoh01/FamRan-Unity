using System;
using RAN.Mobile.Enumerators;

namespace RAN.Mobile.GameModels.Guild
{
	[Serializable]
	public class GuildMemberData
	{
		public string CharId { get; set; }

		public string Name { get; set; }

		public CharacterClassEnum Class { get; set; }

		public long Position { get; set; }

		public ChannelEnum Channel { get; set; }

		public string AliasName { get; set; }
	}
}
