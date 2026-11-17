using System;
using UnityEngine;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public struct SPlayerKill
	{
		public uint Level;

		public Color32 Color;

		public int PKPoint;

		public string Name;

		public float PKExpRate;

		public uint ItemDropNum;

		public float ItemDropRate;

		public float Shop2BuyRate;

		public float Shop2SaleRate;
	}
}
