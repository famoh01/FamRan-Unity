using System;
using Models.Library.Shared.Enums;
using Models.Library.Shared.Game;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class Maps
{
	public bool use;

	public byte zoneNumber;

	public SNativeID MapMidSid;

	public string mapName;

	public string levelFile;

	public EMDIVISION emDivision;

	public MapSize mapSize;

	[JsonIgnore]
	public AudioClip bgmFile;

	[JsonIgnore]
	public Sprite loadingImage;

	public bool peaceZone;

	public bool commissionFree;

	public bool pkZone;

	public bool freePk;

	public bool itemDrop;

	public bool move;

	public bool restart;

	public bool petActivity;

	public bool decExp;

	public bool vehicleActivity;

	public bool clubBattleZone;

	public bool instantMap;

	public bool qBoxEnable;

	public bool lunchBoxForbid;

	public bool cpReset;

	public bool pkMap;

	public bool uiMapSelect;

	public bool uiMapInfo;

	public bool clubPkRecord;

	public bool openPrivateMarket;

	public bool partySparring;

	public bool blockTaxi;

	public bool blockFriendCard;

	public bool blockRecall;

	public bool blockTeleport;

	public bool disableSystemBuffs;

	public bool blockHpPotion;
}
