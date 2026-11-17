using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RAN.Mobile.GameModels;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Scriptable Objects/Databases/New Item Database")]
public class ItemDatabaseObject : ScriptableObject
{
	public class ItemWearingFiles
	{
		public string[] Right;

		public string[] Left;
	}

	public class Item
	{
		[NonSerialized]
		private string _key;

		[NonSerialized]
		private SNativeID _sNativeID;

		public string Name;

		public int ItemLevel;

		public int ItemType;

		public double BuyPrice;

		public double SellPrice;

		public int InvenSizeX;

		public int InvenSizeY;

		public int InvenIconX;

		public int InvenIconY;

		public string IconSource;

		public string IconFieldSource;

		public int[] Id;

		public string Description;

		public ItemWearingFiles WearingFiles;

		public string GetKey => null;
	}

	public class ItemList
	{
		public Item[] Items;
	}

	[JsonIgnore]
	public ItemObject[] ItemObjects;

	[NonSerialized]
	private List<ItemObject> _itemObjectsList;

	public ItemList myItemList;

	public List<ItemObject> GetItemObjectsList => null;

	public void OnValidate()
	{
	}
}
