using System;
using RAN.Mobile.GameModels;
using UnityEngine;

[Serializable]
public class NPCShopItemEntry
{
	[Tooltip("wType — which tab this item belongs to.")]
	public ushort type;

	[Tooltip("sidItem — the item id.")]
	public SNativeID itemId;

	public byte damage;

	public byte defense;

	public byte resistFire;

	public byte resistIce;

	public byte resistElec;

	public byte resistPoison;

	public byte resistSpirit;

	[Tooltip("bGenerateRandomValue — roll random options on the granted item.")]
	public bool generateRandomValue;
}
