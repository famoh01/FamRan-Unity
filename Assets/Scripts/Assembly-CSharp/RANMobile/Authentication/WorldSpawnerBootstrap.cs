using System.Collections.Generic;
using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Populates a freshly-loaded map with monster spawners at runtime. The ripped map
	/// scenes ship no spawn points (the original server owned spawning), so this builds a
	/// ring of spawners around the player's spawn when a world loads — giving the world
	/// living, respawning, loot-dropping crows without editing 34 scenes by hand.
	///
	/// A small built-in roster maps to the mob prefabs that actually exist
	/// (Little/Chief Hooligan + Dummy). Assign richer tables in the inspector or via
	/// MobSpawner.lootTable to override.
	/// </summary>
	public class WorldSpawnerBootstrap : MonoBehaviour
	{
		public static WorldSpawnerBootstrap Instance;

		[Header("Layout")]
		[Tooltip("How many spawner clusters to scatter around the player's spawn.")]
		public int spawnerCount = 6;

		[Tooltip("Distance range from the player at which spawner clusters are placed.")]
		public float minRadius = 60f;

		public float maxRadius = 160f;

		[Tooltip("Mobs per spawner cluster.")]
		public int populationPerSpawner = 3;

		[Tooltip("Seconds before a dead mob respawns.")]
		public float respawnSeconds = 10f;

		[Header("Built-in roster (cycled across the spawners)")]
		public string[] mobPrefabs = new string[] { "Little Hooligan", "Chief Hooligan", "Dummy" };

		public string[] mobNames = new string[] { "Little Hooligan", "Chief Hooligan", "Training Dummy" };

		public int[] mobLevels = new int[] { 4, 9, 1 };

		public int[] mobHp = new int[] { 90, 260, 500 };

		public int[] mobAttack = new int[] { 8, 18, 1 };

		public int[] mobExp = new int[] { 20, 70, 5 };

		private readonly List<MobSpawner> _spawners = new List<MobSpawner>();

		public static WorldSpawnerBootstrap Ensure()
		{
			if (Instance != null)
			{
				return Instance;
			}
			GameObject go = new GameObject("WorldSpawnerBootstrap");
			return go.AddComponent<WorldSpawnerBootstrap>();
		}

		private void Awake()
		{
			Instance = this;
		}

		/// <summary>Called by WorldEntry once the map scene is loaded and the player exists.</summary>
		public void Populate(Vector3 around)
		{
			// Clear any previous clusters (map change) then build fresh ones.
			for (int i = 0; i < _spawners.Count; i++)
			{
				if (_spawners[i] != null)
				{
					Destroy(_spawners[i].gameObject);
				}
			}
			_spawners.Clear();

			for (int i = 0; i < spawnerCount; i++)
			{
				float ang = (360f / Mathf.Max(1, spawnerCount)) * i + Random.Range(-15f, 15f);
				float dist = Random.Range(minRadius, maxRadius);
				Vector3 pos = around + Quaternion.Euler(0f, ang, 0f) * new Vector3(0f, 0f, dist);
				// Snap to walkable ground; skip if we couldn't find any nearby.
				if (!TryGround(ref pos))
				{
					continue;
				}
				MobSpawner spawner = CreateSpawner(pos, i);
				_spawners.Add(spawner);
			}
			Debug.Log("[WorldSpawnerBootstrap] spawned " + _spawners.Count + " spawner clusters around " + around);
		}

		private MobSpawner CreateSpawner(Vector3 pos, int index)
		{
			GameObject go = new GameObject("MobSpawner_" + index);
			go.transform.position = pos;
			MobSpawner s = go.AddComponent<MobSpawner>();
			int r = (mobPrefabs != null && mobPrefabs.Length > 0) ? (index % mobPrefabs.Length) : 0;
			s.prefabName = (mobPrefabs != null && r < mobPrefabs.Length) ? mobPrefabs[r] : "Little Hooligan";
			s.mobName = (mobNames != null && r < mobNames.Length) ? mobNames[r] : s.prefabName;
			s.level = (mobLevels != null && r < mobLevels.Length) ? mobLevels[r] : 1;
			s.maxHp = (mobHp != null && r < mobHp.Length) ? mobHp[r] : 100;
			s.attack = (mobAttack != null && r < mobAttack.Length) ? mobAttack[r] : 8;
			s.expReward = (mobExp != null && r < mobExp.Length) ? mobExp[r] : 15;
			s.population = populationPerSpawner;
			s.respawnSeconds = respawnSeconds;
			s.spawnRadius = 16f;
			// Chief/Dummy hit harder and move slower.
			bool heavy = s.prefabName != null && s.prefabName.IndexOf("Chief", System.StringComparison.OrdinalIgnoreCase) >= 0;
			s.moveSpeed = heavy ? 8f : 11f;
			s.chaseSpeed = heavy ? 14f : 19f;
			s.aggroRange = 26f;
			s.attackRange = 7f;
			return s;
		}

		private static bool TryGround(ref Vector3 pos)
		{
			RaycastHit hit;
			if (Physics.Raycast(pos + Vector3.up * 300f, Vector3.down, out hit, 700f))
			{
				pos.y = hit.point.y;
				return true;
			}
			return false;
		}

		private void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}
	}
}
