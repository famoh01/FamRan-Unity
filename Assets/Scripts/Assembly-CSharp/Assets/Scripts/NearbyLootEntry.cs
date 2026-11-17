using System.Collections.Generic;
using Core.Enumerators;
using UnityEngine;

namespace Assets.Scripts
{
	public readonly struct NearbyLootEntry
	{
		private sealed class EntryDistanceComparer : IComparer<NearbyLootEntry>
		{
			public int Compare(NearbyLootEntry x, NearbyLootEntry y)
			{
				return 0;
			}
		}

		internal static readonly IComparer<NearbyLootEntry> DistanceComparer;

		public CrowType CrowType { get; }

		public ushort GlobalId { get; }

		public ItemObject ItemObject { get; }

		public ushort Quantity { get; }

		public long MoneyAmount { get; }

		public Vector3 WorldPosition { get; }

		public float DistanceSquared { get; }

		public bool IsMoney => false;

		public float Distance => 0f;

		internal uint Key => 0u;

		private NearbyLootEntry(CrowType crowType, ushort globalId, ItemObject itemObject, ushort quantity, long moneyAmount, Vector3 worldPosition, float distanceSquared)
		{
			CrowType = CrowType.CROWPC;
			GlobalId = 0;
			ItemObject = null;
			Quantity = 0;
			MoneyAmount = 0L;
			WorldPosition = default;
			DistanceSquared = 0f;
		}

		internal static NearbyLootEntry ForItem(ushort globalId, ItemObject itemObject, ushort quantity, Vector3 worldPosition, float distanceSquared)
		{
			return default;
		}

		internal static NearbyLootEntry ForMoney(ushort globalId, long amount, Vector3 worldPosition, float distanceSquared)
		{
			return default;
		}
	}
}
