using System.Collections.Generic;
using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// A monster spawn point. Place these in a map scene (or let WorldSpawnerBootstrap
	/// generate them at runtime) to populate the world with crows that wander, aggro,
	/// fight and drop loot — the piece the ripped client left entirely to the server.
	///
	/// Each spawner keeps <see cref="population"/> mobs alive and respawns a mob
	/// <see cref="respawnSeconds"/> after it dies. Mobs are pushed through the normal
	/// WorldRuntime mob pipeline (QueueMobSpawn) with <c>local = true</c>, so they use
	/// the same views/nameplates as server mobs and a MobBrain is attached automatically.
	/// </summary>
	public class MobSpawner : MonoBehaviour
	{
		[Header("Mob identity")]
		[Tooltip("mobsprefab resource name (e.g. 'Little Hooligan'). Leave blank to use the raw template below as a capsule.")]
		public string prefabName = "Little Hooligan";

		[Tooltip("Display name shown on the nameplate.")]
		public string mobName = "Little Hooligan";

		[Tooltip("Mob level shown on the hp label and used to scale stats.")]
		public int level = 5;

		[Header("Population")]
		public int population = 4;

		[Tooltip("Seconds between a mob's death and its respawn.")]
		public float respawnSeconds = 8f;

		[Tooltip("Radius around this transform that mobs spawn (and are considered home).")]
		public float spawnRadius = 20f;

		[Header("Stats")]
		public int maxHp = 120;

		public int attack = 10;

		public float moveSpeed = 10f;

		public float chaseSpeed = 18f;

		public float aggroRange = 28f;

		public float attackRange = 7f;

		[Header("Exp")]
		public int expReward = 25;

		[Header("Loot")]
		[Tooltip("Optional drop table. A generic fallback is used when empty.")]
		public LootTable lootTable;

		private readonly List<string> _alive = new List<string>();

		private readonly Dictionary<string, float> _pendingRespawn = new Dictionary<string, float>();

		private static int _nextSpawnerId;

		private int _spawnerId;

		private int _nextLocalId;

		private void OnEnable()
		{
			_spawnerId = ++_nextSpawnerId;
			// Register the loot table so LocalLoot can resolve drops for our template keys.
			if (lootTable != null)
			{
				LocalLoot.RegisterTable(prefabName, lootTable);
				LocalLoot.RegisterTable(mobName, lootTable);
			}
			WorldRuntime.MobSpawned += OnMobSpawned;
		}

		private void OnDisable()
		{
			WorldRuntime.MobSpawned -= OnMobSpawned;
		}

		private void Start()
		{
			// Initial population.
			for (int i = 0; i < population; i++)
			{
				SpawnOne();
			}
		}

		private void Update()
		{
			// Respawn anything whose timer elapsed (skip while the world isn't ready).
			if (WorldRuntime.LocalPlayer == null)
			{
				return;
			}
			if (_pendingRespawn.Count == 0)
			{
				return;
			}
			List<string> due = null;
			foreach (KeyValuePair<string, float> kv in _pendingRespawn)
			{
				if (Time.time >= kv.Value)
				{
					if (due == null)
					{
						due = new List<string>();
					}
					due.Add(kv.Key);
				}
			}
			if (due == null)
			{
				return;
			}
			for (int i = 0; i < due.Count; i++)
			{
				_pendingRespawn.Remove(due[i]);
				SpawnOne();
			}
		}

		private void SpawnOne()
		{
			Vector3 pos = PickSpawnPosition();
			string id = "S" + _spawnerId + "_" + (++_nextLocalId);
			WorldTcpClient.MobInfo info = new WorldTcpClient.MobInfo();
			info.id = id;
			info.template = prefabName;
			info.name = mobName;
			info.prefab = prefabName;
			info.x = pos.x;
			info.y = pos.y;
			info.z = pos.z;
			info.dir = Random.Range(0f, 360f);
			info.level = level;
			info.maxHp = maxHp;
			info.hp = maxHp;
			info.local = true;
			info.exp = expReward;
			_alive.Add(id);
			WorldRuntime.QueueMobSpawn(info);
		}

		private Vector3 PickSpawnPosition()
		{
			Vector2 c = Random.insideUnitCircle * spawnRadius;
			Vector3 pos = transform.position + new Vector3(c.x, 0f, c.y);
			// Snap to the ground the same way WorldEntry does.
			RaycastHit hit;
			if (Physics.Raycast(pos + Vector3.up * 200f, Vector3.down, out hit, 500f))
			{
				pos.y = hit.point.y;
			}
			return pos;
		}

		/// <summary>Attach AI to every mob we own (fired by WorldRuntime on create/refresh).</summary>
		private void OnMobSpawned(WorldMobView view)
		{
			if (view == null || view.Id == null)
			{
				return;
			}
			// Only our mobs.
			if (!_alive.Contains(view.Id))
			{
				return;
			}
			MobBrain brain = view.GetComponent<MobBrain>();
			if (brain == null)
			{
				brain = view.gameObject.AddComponent<MobBrain>();
			}
			brain.View = view;
			brain.level = level;
			brain.attack = attack;
			brain.moveSpeed = moveSpeed;
			brain.chaseSpeed = chaseSpeed;
			brain.aggroRange = aggroRange;
			brain.attackRange = attackRange;
			brain.ResetHome(view.transform.position);
			// Watch for this mob's death so we can schedule a respawn.
			WorldRuntime.MobDied -= OnAnyMobDied;
			WorldRuntime.MobDied += OnAnyMobDied;
		}

		private void OnAnyMobDied(string id)
		{
			if (!_alive.Remove(id))
			{
				return;
			}
			_pendingRespawn[id] = Time.time + respawnSeconds;
		}

		private void OnDestroy()
		{
			WorldRuntime.MobDied -= OnAnyMobDied;
		}

#if UNITY_EDITOR
		// Visualise the spawn radius in the editor.
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = new Color(1f, 0.3f, 0.3f, 0.6f);
			Gizmos.DrawWireSphere(transform.position, spawnRadius);
		}
#endif
	}
}
