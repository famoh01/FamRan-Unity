using System;
using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Drop table for one mob (or a shared table referenced by many mobs). Each entry
	/// rolls independently against its own rate, so a mob can drop several items plus
	/// gold in one kill — mirroring the original's loot behaviour. Create via
	/// Assets > Create > Scriptable Objects > Databases > Loot Table.
	/// </summary>
	[CreateAssetMenu(fileName = "New Loot Table", menuName = "Scriptable Objects/Databases/Loot Table")]
	public class LootTable : ScriptableObject
	{
		public enum Rarity
		{
			Common = 0,
			Uncommon = 1,
			Rare = 2,
			Epic = 3,
			Legendary = 4
		}

		[Serializable]
		public class Entry
		{
			[Tooltip("Item mainId (matches the ItemDatabase / server item id).")]
			public int mainId;

			[Tooltip("Item subId (variant / grade).")]
			public int subId;

			[Range(0f, 1f)]
			[Tooltip("Independent drop chance for this entry (0.25 = 25%).")]
			public float rate = 0.25f;

			[Tooltip("Minimum stack dropped on success.")]
			public int minCount = 1;

			[Tooltip("Maximum stack dropped on success (inclusive random).")]
			public int maxCount = 1;

			[Tooltip("Rarity tier — drives the ground label colour.")]
			public Rarity rarity = Rarity.Common;
		}

		[Header("Gold")]
		[Tooltip("Chance that any gold drops at all.")]
		[Range(0f, 1f)]
		public float goldChance = 0.6f;

		public int goldMin = 5;

		public int goldMax = 25;

		[Header("Guaranteed minimum drops")]
		[Tooltip("Always drop at least this many rolled entries (fills from highest-rate first when randoms miss).")]
		public int minDrops;

		[Header("Items")]
		public Entry[] entries = new Entry[0];
	}
}
