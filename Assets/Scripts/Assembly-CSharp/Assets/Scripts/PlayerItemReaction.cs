using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Core.Enumerators;
using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerItemReaction : MonoBehaviour
	{
		private readonly struct PendingItemPickup
		{
			public readonly int Quantity;

			public readonly float SentAt;

			public PendingItemPickup(int quantity, float sentAt)
			{
				Quantity = 0;
				SentAt = 0f;
			}
		}

		private readonly struct PendingMoneyPickup
		{
			public readonly long Amount;

			public readonly float SentAt;

			public PendingMoneyPickup(long amount, float sentAt)
			{
				Amount = 0L;
				SentAt = 0f;
			}
		}

		public DropClientItem lootData;

		public bool nearItemDrop;

		private TargetInfoEx _nearest;

		private bool _hasNearest;

		private LootMode _lootMode;

		private readonly List<NearbyLootEntry> _nearbyLoot;

		private readonly List<uint> _publishedLootKeys;

		private readonly Queue<PendingItemPickup> _pendingItemPickups;

		private readonly Queue<PendingMoneyPickup> _pendingMoneyPickups;

		private const int MaxPendingPickups = 16;

		private const float PendingPickupLifetime = 5f;

		[CompilerGenerated]
		private Action m_NearbyLootChanged;

		private const float AutoLootQBoxWindow = 2f;

		private float _autoLootQBoxDeadline;

		public static PlayerItemReaction Instance { get; private set; }

		public IReadOnlyList<NearbyLootEntry> NearbyLoot => null;

		public event Action NearbyLootChanged
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public void RequestAutoLootQBox()
		{
		}

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleLootModeChanged(LootMode mode)
		{
		}

		private void SubscribeToHud()
		{
		}

		private void Instance_OnPickUpItemButtonClick()
		{
		}

		public bool TryPickupNearest()
		{
			return false;
		}

		public bool TryPickup(CrowType crowType, ushort globalId)
		{
			return false;
		}

		internal int CompletePendingItemPickup()
		{
			return 0;
		}

		internal long CompletePendingMoneyPickup()
		{
			return 0L;
		}

		private void TrackPendingItemPickup(int quantity)
		{
		}

		private void TrackPendingMoneyPickup(long amount)
		{
		}

		private void PrunePendingPickups()
		{
		}

		private bool ScanNearbyDrops()
		{
			return false;
		}

		private void PublishNearbyLootIfChanged()
		{
		}

		private static float GetPickupRangeSq()
		{
			return 0f;
		}

		private bool IsInsidePickupRange(Vector3 dropPosition)
		{
			return false;
		}

		private void FixedUpdate()
		{
		}

		private bool IsNearestQuestionItem()
		{
			return false;
		}
	}
}
