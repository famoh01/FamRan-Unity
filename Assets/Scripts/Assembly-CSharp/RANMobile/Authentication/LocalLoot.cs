using System.Collections.Generic;
using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Client-authoritative loot. When a locally-spawned mob dies (WorldRuntime.
	/// ApplyLocalMobDamage), we roll its LootTable and push the results through the
	/// normal DROP_SPAWN queue so WorldDropView renders them exactly like server loot.
	/// Picking one up resolves locally instead of round-tripping to the server.
	/// Falls back to a small generic table when a mob has no LootTable assigned.
	/// </summary>
	public static class LocalLoot
	{
		private static int _nextDropId;

		/// <summary>Per-template override tables (filled by MobSpawner). Key = mob template/prefab name.</summary>
		private static readonly Dictionary<string, LootTable> _tables = new Dictionary<string, LootTable>();

		private static LootTable _fallback;

		public static void RegisterTable(string templateKey, LootTable table)
		{
			if (string.IsNullOrEmpty(templateKey) || table == null)
			{
				return;
			}
			_tables[templateKey] = table;
		}

		public static void Clear()
		{
			_tables.Clear();
			_fallback = null;
			_nextDropId = 0;
		}

		/// <summary>Called by WorldRuntime when a local mob's HP hits zero.</summary>
		public static void OnLocalMobKilled(WorldTcpClient.MobInfo info, Vector3 deathPos)
		{
			if (info == null)
			{
				return;
			}
			LootTable table = ResolveTable(info);
			Vector3 basePos = deathPos;
			if (table == null)
			{
				return;
			}

			// --- gold ---
			if (Random.value <= table.goldChance && table.goldMax > 0)
			{
				int gold = Random.Range(table.goldMin, Mathf.Max(table.goldMin, table.goldMax) + 1);
				if (gold > 0)
				{
					SpawnDrop(info, basePos, 0, 0, gold, 0, 0);
				}
			}

			// --- item entries (each rolls independently) ---
			List<LootTable.Entry> rolled = new List<LootTable.Entry>();
			if (table.entries != null)
			{
				for (int i = 0; i < table.entries.Length; i++)
				{
					LootTable.Entry e = table.entries[i];
					if (e == null)
					{
						continue;
					}
					if (Random.value <= e.rate)
					{
						rolled.Add(e);
					}
				}
			}
			// Guarantee minimum drops by pulling the highest-rate misses.
			if (table.minDrops > rolled.Count && table.entries != null)
			{
				List<LootTable.Entry> misses = new List<LootTable.Entry>();
				for (int i = 0; i < table.entries.Length; i++)
				{
					LootTable.Entry e = table.entries[i];
					if (e != null && !rolled.Contains(e))
					{
						misses.Add(e);
					}
				}
				misses.Sort((a, b) => b.rate.CompareTo(a.rate));
				for (int i = 0; i < misses.Count && rolled.Count < table.minDrops; i++)
				{
					rolled.Add(misses[i]);
				}
			}
			for (int i = 0; i < rolled.Count; i++)
			{
				LootTable.Entry e = rolled[i];
				int count = Random.Range(e.minCount, Mathf.Max(e.minCount, e.maxCount) + 1);
				// Scatter drops slightly around the corpse so they don't overlap.
				Vector3 offset = Random.insideUnitCircle * 3f;
				SpawnDrop(info, basePos + new Vector3(offset.x, 0f, offset.y), e.mainId, e.subId, 0, count, (int)e.rarity);
			}
		}

		private static LootTable ResolveTable(WorldTcpClient.MobInfo info)
		{
			LootTable t;
			string[] keys = { info.template, info.prefab, info.name };
			for (int i = 0; i < keys.Length; i++)
			{
				if (!string.IsNullOrEmpty(keys[i]) && _tables.TryGetValue(keys[i], out t) && t != null)
				{
					return t;
				}
			}
			return DefaultTable();
		}

		private static LootTable DefaultTable()
		{
			if (_fallback != null)
			{
				return _fallback;
			}
			// Build a modest generic table in memory so killing *anything* yields something.
			_fallback = ScriptableObject.CreateInstance<LootTable>();
			_fallback.goldChance = 0.7f;
			_fallback.goldMin = 3;
			_fallback.goldMax = 20;
			_fallback.minDrops = 1;
			_fallback.entries = new[]
			{
				new LootTable.Entry { mainId = 0, subId = 0, rate = 0.35f, minCount = 1, maxCount = 1, rarity = LootTable.Rarity.Common },
				new LootTable.Entry { mainId = 0, subId = 1, rate = 0.15f, minCount = 1, maxCount = 2, rarity = LootTable.Rarity.Uncommon },
				new LootTable.Entry { mainId = 0, subId = 2, rate = 0.05f, minCount = 1, maxCount = 1, rarity = LootTable.Rarity.Rare }
			};
			return _fallback;
		}

		private static void SpawnDrop(WorldTcpClient.MobInfo owner, Vector3 pos, int mainId, int subId, int gold, int count, int rarity)
		{
			WorldTcpClient.DropInfo drop = new WorldTcpClient.DropInfo();
			// Unique, non-colliding id; the local prefix keeps it out of server id space.
			drop.id = "L" + (++_nextDropId) + "_" + owner.id;
			drop.mainId = mainId;
			drop.subId = subId;
			drop.gold = gold;
			drop.count = count;
			drop.x = pos.x;
			drop.y = pos.y;
			drop.z = pos.z;
			drop.local = true;
			drop.rarity = rarity;
			WorldRuntime.QueueDropSpawn(drop);
		}

		/// <summary>Resolve a local pickup: grant gold/item then tell the world to remove it.</summary>
		public static void ResolvePickup(WorldTcpClient.DropInfo drop)
		{
			if (drop == null)
			{
				return;
			}
			if (drop.gold > 0)
			{
				WorldRuntime.Gold += drop.gold;
			}
			else if (drop.count > 0)
			{
				AddLocalItem(drop.mainId, drop.subId, drop.count);
			}
			// Reuse the removal queue so the view is destroyed and the event fires.
			WorldRuntime.QueueDropRemove(drop.id);
			if (drop.gold > 0)
			{
				WorldRuntime.RaiseInventoryChanged();
			}
		}

		/// <summary>Stack into an existing matching stack when possible, else append.</summary>
		private static void AddLocalItem(int mainId, int subId, int count)
		{
			for (int i = 0; i < WorldRuntime.Items.Count; i++)
			{
				WorldTcpClient.InventoryItemInfo it = WorldRuntime.Items[i];
				if (it.mainId == mainId && it.subId == subId)
				{
					it.count += count;
					WorldRuntime.Items[i] = it;
					WorldRuntime.RaiseInventoryChanged();
					return;
				}
			}
			WorldTcpClient.InventoryItemInfo add = new WorldTcpClient.InventoryItemInfo();
			add.mainId = mainId;
			add.subId = subId;
			add.count = count;
			add.slot = -1;
			add.position = 0;
			WorldRuntime.Items.Add(add);
			WorldRuntime.RaiseInventoryChanged();
		}
	}
}
