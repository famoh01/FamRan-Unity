using System;
using Models.Library.Shared.Game;
using Newtonsoft.Json;

[Serializable]
public class Item
{
	public int mainId;

	public int subId;

	[NonSerialized]
	[JsonIgnore]
	public SItemCustom sItemCustom;

	[NonSerialized]
	[JsonIgnore]
	public int attackRefine;

	[NonSerialized]
	[JsonIgnore]
	public int defenseRefine;

	public Item()
	{
	}

	public Item(ItemObject item)
	{
	}

	public Item(SItemCustom itemCustom)
	{
	}

	public bool IsEqual(in SNativeID sNativeID)
	{
		return false;
	}

	public bool IsNotEqual(in SNativeID sNativeID)
	{
		return false;
	}

	public bool IsValidItem()
	{
		return false;
	}
}
