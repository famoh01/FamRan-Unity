using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public LinkedListNode<Unit> node;

	public int gaeaId;

	public uint charId;

	public string charName;

	public byte school;

	public short charClass;

	public uint userRole;

	public int faceType;

	public int hairType;

	public int hairColorType;

	public bool isPlayersUnit;

	public int nBright;

	public GameObject self;

	private UIHUDFunctions UIFunctions;

	public string vendingName;

	public string vendingData;

	public int? InDuelWith;

	[NonSerialized]
	public PlayerControllerClient controllerClient;

	public long GuildId;

	public string GuildName;

	public byte ClubFlags;

	public uint ClubMasterCharId;

	public uint GuildMarkVer;

	public string GuildAliasName;

	public byte[] GuildBadge;

	public uint PartyId;

	public bool IsPartyMaster;

	[NonSerialized]
	public bool NameplateDirty;

	public bool IsMasterGuild => false;

	public void Awake()
	{
	}

	public void Start()
	{
	}
}
