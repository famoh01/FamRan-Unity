using System;
using RAN.Mobile.GameModels;
using UnityEngine;

[Serializable]
public class NPCItemExchangeEntry
{
	[Tooltip("wID — the trade id the client sends to the zone.")]
	public ushort id;

	[Header("Result (what you receive)")]
	public SNativeID resultItem;

	public ushort resultNum;

	public byte damage;

	public byte defense;

	public byte resistFire;

	public byte resistIce;

	public byte resistElec;

	public byte resistPoison;

	public byte resistSpirit;

	[Tooltip("bGenerateRandomValue — roll random options on the result item.")]
	public bool generateRandomValue;

	[Header("Cost (what you give up — up to 5)")]
	public NPCItemExchangeRequire[] requires;
}
