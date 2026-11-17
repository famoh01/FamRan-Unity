using System;
using RAN.Mobile.GameModels;

namespace Assets.Scripts.Mob_Scripts
{
	[Serializable]
	public class NpcInventory
	{
		public SNativeID itemId;

		public Resistance element;

		public int quantity;

		public NpcItemGrade npcItemGrade;
	}
}
