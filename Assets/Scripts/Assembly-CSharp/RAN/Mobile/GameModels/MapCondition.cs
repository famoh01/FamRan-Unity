namespace RAN.Mobile.GameModels
{
	public class MapCondition
	{
		public LevelRequirement ReqLevel { get; set; }

		public SNativeID ReqItem { get; set; }

		public SNativeID ReqSkill { get; set; }

		public int ReqComQuest { get; set; }

		public int ReqActQuest { get; set; }

		public BasicPointRequirement ReqLifePoint { get; set; }

		public BasicPointRequirement ReqBrightPoint { get; set; }

		public BasicPointRequirement ReqActPoint { get; set; }

		public BasicPointRequirement ReqConPoint { get; set; }

		public bool PartyMember { get; set; }
	}
}
