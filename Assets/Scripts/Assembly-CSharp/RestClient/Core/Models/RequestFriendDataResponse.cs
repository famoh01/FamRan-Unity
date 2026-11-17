using RAN.Mobile.Enumerators;

namespace RestClient.Core.Models
{
	public class RequestFriendDataResponse
	{
		public string Id;

		public string Name;

		public int Level;

		public CharacterClassEnum Class;

		public SchoolEnum School;

		public ushort SaveMap;

		public float SavePosX;

		public float SavePosY;

		public float SavePosZ;

		public ushort ReturnMap;

		public float ReturnPosX;

		public float ReturnPosY;

		public float ReturnPosZ;

		public string BlockedId;

		public bool CanBeAdd;

		public int? Channel;

		public bool IsOnline;
	}
}
