using System.Collections;
using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Spawns after the TCP enter-world handshake succeeds. Survives the scene
	/// change, loads the map scene for the server's map id, then drops a camera +
	/// the player's preview model (with starter gear) at the returned position.
	/// </summary>
	public class WorldEntry : MonoBehaviour
	{
		public WorldTcpClient.ZoneInfo zone;

		public string characterName;

		public int classId;

		public int school;

		public int hair;

		public int face;

		public bool female;

		private static readonly int[] MapIds = new int[64]
		{
			2, 5, 8, 22, 0, 4, 7, 3, 23, 18,
			161, 162, 11, 12, 13, 14, 15, 6, 9, 16,
			10, 17, 19, 20, 21, 27, 26, 28, 254, 102,
			100, 101, 103, 104, 105, 106, 107, 120, 121, 122,
			123, 124, 125, 126, 130, 131, 134, 135, 136, 137,
			138, 141, 142, 30, 34, 150, 151, 152, 153, 154,
			155, 204, 214, 222
		};

		private static readonly string[] SceneNames = new string[64]
		{
			"w_school_01", "w_school_02", "w_school_03", "w_tradezone1", "innerzone_01", "innerzone_21", "innerzone_31", "w_city_s_01", "suhak", "w_city_D_01",
			"ev_underpark", "ev_underpark2", "w_school_04_in_1f", "w_school_04_in_2f", "w_school_04_in_3f", "w_school_04_in_b1", "w_school_04_in_b2", "w_city_s_02", "w_city_s_03", "w_city_c_01",
			"w_school_04", "w_city_s_tunnel", "w_blue_in_1F", "w_blue_in_2F", "w_blue_in_3F", "w_prison_01", "W_Tunnel_3", "w_city_c_01", "w_SchoolWar_01", "w_school_01",
			"w_school_01", "w_school_01", "w_school_01", "w_school_01", "w_school_01", "w_school_01", "w_school_01", "w_school_02", "w_school_02", "w_school_02",
			"w_school_02", "w_school_02", "w_school_02", "w_school_02", "w_school_03", "w_school_03", "w_school_03", "w_school_03", "w_school_03", "w_school_03",
			"w_school_03", "w_school_03", "w_school_03", "ran_event_sibuya", "ran_event_sibuya", "w_blue_in_1F", "w_blue_in_1F", "w_blue_in_2F", "w_blue_in_2F", "w_blue_in_3F",
			"w_blue_in_3F", "w_club_c_in", "w_prison_01", "w_club_c_out"
		};

		public static string SceneForMap(int mapId)
		{
			for (int i = 0; i < MapIds.Length; i++)
			{
				if (MapIds[i] == mapId)
				{
					return SceneNames[i];
				}
			}
			return "w_school_01";
		}

		private IEnumerator Start()
		{
			string scene = SceneForMap(zone.map);
			Debug.Log("[WorldEntry] entering map " + zone.map + " gate " + zone.gate + " pos (" + zone.x + "," + zone.y + "," + zone.z + ") -> scene " + scene);
			var op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
			if (op == null)
			{
				Debug.LogError("[WorldEntry] LoadSceneAsync failed for " + scene);
				Destroy(gameObject);
				yield break;
			}
			yield return op;
			yield return null;
			SpawnCameraAndPlayer(FindGround(new Vector3(zone.x, zone.y, zone.z)));
			Destroy(gameObject);
		}

		private void SpawnCameraAndPlayer(Vector3 ground)
		{
			GameObject player = SpawnPlayer(ground);
			Camera cam = SpawnCamera(ground, player);
			if (player != null && cam != null)
			{
				WorldRuntime.LocalName = string.IsNullOrEmpty(characterName) ? "Player" : characterName;
				WorldRuntime.ClassId = classId;
				WorldRuntime.ClassName = ClassDisplay(classId);
				WorldPlayerMover mover = player.GetComponent<WorldPlayerMover>();
				if (mover == null)
				{
					mover = player.AddComponent<WorldPlayerMover>();
				}
				mover.followCamera = cam;
				mover.animator = player.GetComponent<Animator>();
				WorldNetBehaviour net = player.GetComponent<WorldNetBehaviour>();
				if (net == null)
				{
					net = player.AddComponent<WorldNetBehaviour>();
				}
				net.Init(player.transform);
				GameObject rtGo = new GameObject("WorldRuntime");
				WorldRuntime runtime = rtGo.AddComponent<WorldRuntime>();
				runtime.Init(player.transform, cam);
				GameObject hudGo = new GameObject("WorldHUD");
				WorldHUD hud = hudGo.AddComponent<WorldHUD>();
				hud.Init(player.transform, mover, cam);
				OriginalHudDriver.Spawn(hud, mover);
				// P5 fidelity: day/night cycle on the game clock (original runs one)
				WorldDayNight.Ensure();
				UnityEngine.Debug.Log("[WorldEntry] SpawnCameraAndPlayer client=" + (WorldNet.Client == null ? "NULL" : "OK") + " zone=" + zone.map);
				if (WorldNet.Client != null)
				{
					WorldNet.Client.SendInventoryRequest();
					WorldNet.Client.SendSkillListRequest();
				}
				// Populate the world with living, respawning, loot-dropping mobs.
				WorldSpawnerBootstrap spawners = WorldSpawnerBootstrap.Ensure();
				spawners.Populate(ground);
			}
		}

		private Vector3 FindGround(Vector3 pos)
		{
			if (pos.y > 1f)
			{
				return pos;
			}
			Vector3 from = new Vector3(pos.x, 200f, pos.z);
			RaycastHit hit;
			if (Physics.Raycast(from, Vector3.down, out hit, 500f))
			{
				return hit.point;
			}
			return new Vector3(pos.x, 2f, pos.z);
		}

		private GameObject SpawnPlayer(Vector3 ground)
		{
			GameObject prefab = Resources.Load<GameObject>("prefabs/characterselection/" + (female ? "w_playerS" : "m_playerS"));
			if (prefab == null)
			{
				Debug.LogWarning("[WorldEntry] player prefab missing.");
				return null;
			}
			GameObject go = Object.Instantiate(prefab, ground + Vector3.up * 0.1f, Quaternion.identity);
			go.name = "Player_" + characterName;
			// CharacterController drives movement — the prefab's NavMeshAgent would fight it.
			var agent = go.GetComponent<UnityEngine.AI.NavMeshAgent>();
			if (agent != null)
			{
				agent.enabled = false;
			}
			// Armor only — the local player shows its actual equipped weapon (added by
			// WorldRuntime.RefreshLocalPlayerWeapon), so the class-default weapon must not stack.
			PreviewGearBinder.Apply(go, school, female, classId, attachWeapon: false);
			ApplyFaceHair(go);
			if (go.GetComponent<AnimationConfiguration>() == null)
			{
				go.AddComponent<AnimationConfiguration>();
			}
			Animator anim = go.GetComponent<Animator>();
			if (anim != null)
			{
				string path = "Animator/" + ClassFolder(classId);
				var ctrl = Resources.Load<RuntimeAnimatorController>(path);
				if (ctrl != null)
				{
					anim.runtimeAnimatorController = ctrl;
				}
			}
			return go;
		}

		private void ApplyFaceHair(GameObject go)
		{
			Transform body = FindDeep(go.transform, female ? "w_player" : "m_player", 0);
			if (body == null)
			{
				return;
			}
			Transform faces = body.Find("Faces");
			if (faces != null && faces.childCount > 0)
			{
				SetOnlyActiveChild(faces, Wrap(face, faces.childCount));
			}
			Transform hairs = body.Find("Hairs");
			if (hairs != null && hairs.childCount > 0)
			{
				SetOnlyActiveChild(hairs, Wrap(hair, hairs.childCount));
			}
		}

		private static int Wrap(int value, int count)
		{
			if (count <= 0)
			{
				return 0;
			}
			int v = value % count;
			return v < 0 ? v + count : v;
		}

		private static void SetOnlyActiveChild(Transform parent, int index)
		{
			for (int i = 0; i < parent.childCount; i++)
			{
				parent.GetChild(i).gameObject.SetActive(i == index);
			}
		}

		private static Transform FindDeep(Transform root, string name, int depth)
		{
			if (root == null || depth > 6)
			{
				return null;
			}
			if (root.name == name)
			{
				return root;
			}
			for (int i = 0; i < root.childCount; i++)
			{
				Transform hit = FindDeep(root.GetChild(i), name, depth + 1);
				if (hit != null)
				{
					return hit;
				}
			}
			return null;
		}

		private static string ClassFolder(int classId)
		{
			switch (classId)
			{
				case 2:
					return "Swordsman";
				case 4:
					return "Archer";
				case 8:
					return "Shaman";
				default:
					return "Brawler";
			}
		}

		/// <summary>Human-readable class name for the Character window.</summary>
		private static string ClassDisplay(int classId)
		{
			return ClassFolder(classId);
		}

		private Camera SpawnCamera(Vector3 playerPos, GameObject player)
		{
			// Ripped map lights carry UniversalAdditionalCameraData; URP adds a
			// real Camera to satisfy it, which then renders from the light's
			// ground-level transform. Strip those and any other stale cameras.
			foreach (Light l in FindObjectsOfType<Light>(true))
			{
				// The URP data component depends on Camera — remove it first.
				var urp = l.GetComponent("UniversalAdditionalCameraData");
				if (urp != null)
				{
					Destroy(urp);
				}
				var rogue = l.GetComponent<Camera>();
				if (rogue != null)
				{
					Destroy(rogue);
				}
			}
			foreach (Camera c in FindObjectsOfType<Camera>(true))
			{
				if (c.gameObject.name != "WorldCamera" && c.GetComponent<Light>() == null)
				{
					Destroy(c.gameObject);
				}
			}
			Camera existing = Camera.main;
			if (existing != null && existing.gameObject.name != "WorldCamera")
			{
				Destroy(existing.gameObject);
			}
			GameObject camGo = new GameObject("WorldCamera");
			camGo.tag = "MainCamera";
			Camera cam = camGo.AddComponent<Camera>();
			cam.clearFlags = CameraClearFlags.Skybox;
			cam.fieldOfView = 50f;
			// RAN preview models are ~15 units tall — scale the follow distance
			// from the rendered bounds instead of a human-scale offset.
			float height = 15f;
			if (player != null)
			{
				Bounds b = new Bounds(player.transform.position, Vector3.zero);
				bool first = true;
				foreach (Renderer r in player.GetComponentsInChildren<Renderer>(true))
				{
					if (r is SkinnedMeshRenderer smr && smr.sharedMesh == null)
					{
						continue;
					}
					if (first)
					{
						b = r.bounds;
						first = false;
					}
					else
					{
						b.Encapsulate(r.bounds);
					}
				}
				if (!first && b.size.y > 1f)
				{
					height = b.size.y;
				}
			}
			Vector3 offset = new Vector3(0f, height * 0.62f, -height * 1.35f);
			camGo.transform.position = playerPos + offset;
			camGo.transform.LookAt(playerPos + Vector3.up * height * 0.45f);
			if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
			{
				new GameObject("EventSystem").AddComponent<UnityEngine.EventSystems.EventSystem>();
			}
			return cam;
		}
	}
}
