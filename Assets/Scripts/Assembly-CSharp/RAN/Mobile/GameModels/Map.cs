using Models.Library.Shared.Enums;
using Newtonsoft.Json;

namespace RAN.Mobile.GameModels
{
	public class Map
	{
		public bool Use { get; set; }

		public SNativeID MapMidSid { get; set; }

		public string MapName { get; set; }

		public string LevelFile { get; set; }

		public EMDIVISION EmDivision { get; set; }

		public bool PeaceZone { get; set; }

		public bool CommissionFree { get; set; }

		public bool PkZone { get; set; }

		public bool FreePk { get; set; }

		public bool ItemDrop { get; set; }

		public bool Move { get; set; }

		public bool Restart { get; set; }

		public bool PetActivity { get; set; }

		public bool DecExp { get; set; }

		public bool VehicleActivity { get; set; }

		public bool ClubBattleZone { get; set; }

		public bool InstantMap { get; set; }

		public bool QBoxEnable { get; set; }

		public bool LunchBoxForbid { get; set; }

		public bool CpReset { get; set; }

		public bool PkMap { get; set; }

		public bool UiMapSelect { get; set; }

		public bool UiMapInfo { get; set; }

		public bool ClubPkRecord { get; set; }

		public bool OpenPrivateMarket { get; set; }

		public bool PartySparring { get; set; }

		public bool BlockTaxi { get; set; }

		public bool BlockFriendCard { get; set; }

		public bool BlockRecall { get; set; }

		public bool BlockTeleport { get; set; }

		public bool DisableSystemBuffs { get; set; }

		public bool BlockHpPotion { get; set; }

		[JsonIgnore]
		public MapInfo MapInfo { get; set; }

		[JsonIgnore]
		public bool HasCrows { get; set; }

		[JsonIgnore]
		public int CrowsCount { get; set; }

		public override string ToString()
		{
			return null;
		}
	}
}
