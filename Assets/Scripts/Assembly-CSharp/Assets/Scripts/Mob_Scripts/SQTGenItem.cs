using System;
using Newtonsoft.Json;
using RAN.Mobile.GameModels;

namespace Assets.Scripts.Mob_Scripts
{
	[Serializable]
	public class SQTGenItem
	{
		public SQTGenItemModel[] item;

		[NonSerialized]
		[JsonIgnore]
		public int itemNum;

		public static implicit operator bool(SQTGenItem obj)
		{
			return false;
		}
	}
}
