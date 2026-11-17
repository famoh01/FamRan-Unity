using System.Collections.Generic;
using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// One-shot particle VFX helper. Loads a prefab from Resources, plays it at a
	/// world position and auto-destroys it once every emitter has finished. Used for
	/// mob death / respawn bursts so kills feel like the original game instead of a
	/// silent SetActive(false).
	/// </summary>
	public static class WorldFx
	{
		private static readonly Dictionary<string, GameObject> _prefabCache = new Dictionary<string, GameObject>();

		private static Transform _root;

		/// <summary>Play a one-shot effect by Resources path. Safe to call with a missing prefab.</summary>
		public static void PlayAt(Vector3 position, string resourcesPath, float destroyAfter = 6f)
		{
			if (string.IsNullOrEmpty(resourcesPath))
			{
				return;
			}
			GameObject prefab;
			if (!_prefabCache.TryGetValue(resourcesPath, out prefab))
			{
				prefab = Resources.Load<GameObject>(resourcesPath);
				_prefabCache[resourcesPath] = prefab;
			}
			if (prefab == null)
			{
				return;
			}
			if (_root == null)
			{
				GameObject root = new GameObject("WorldFx");
				Object.DontDestroyOnLoad(root);
				_root = root.transform;
			}
			GameObject go = Object.Instantiate(prefab, position, Quaternion.identity, _root);
			go.name = System.IO.Path.GetFileName(resourcesPath);
			AutoDestroy ad = go.AddComponent<AutoDestroy>();
			ad.Lifetime = destroyAfter;
		}

		/// <summary>Destroy the host object after Lifetime seconds, or sooner once all particles end.</summary>
		public class AutoDestroy : MonoBehaviour
		{
			public float Lifetime = 6f;

			private ParticleSystem[] _systems;

			private void Awake()
			{
				_systems = GetComponentsInChildren<ParticleSystem>(true);
				// Ensure emitters actually run even if the prefab shipped disabled.
				for (int i = 0; i < _systems.Length; i++)
				{
					_systems[i].Play(true);
				}
			}

			private void Update()
			{
				Lifetime -= Time.deltaTime;
				if (Lifetime <= 0f)
				{
					Destroy(gameObject);
					return;
				}
				// If we have systems and every one has finished, we're done.
				if (_systems != null && _systems.Length > 0)
				{
					bool anyPlaying = false;
					for (int i = 0; i < _systems.Length; i++)
					{
						if (_systems[i] != null && _systems[i].IsAlive(true))
						{
							anyPlaying = true;
							break;
						}
					}
					if (!anyPlaying)
					{
						Destroy(gameObject);
					}
				}
			}
		}
	}
}
