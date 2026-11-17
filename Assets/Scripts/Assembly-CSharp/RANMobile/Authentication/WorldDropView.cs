using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Ground loot: server-driven spawn (DROP_SPAWN), spins and bobs, auto
	/// pickups within 5 units, tap-to-pickup beyond that (world raycast from
	/// WorldHUD). Removal (DROP_REMOVE) toasts what you looted.
	/// </summary>
	public class WorldDropView : MonoBehaviour
	{
		public WorldTcpClient.DropInfo Info;

		private bool _pickupSent;

		private Vector3 _basePos;

		private TextMesh _label;

		public static WorldDropView Create(WorldTcpClient.DropInfo info)
		{
			bool gold = info.gold > 0;
			string[] paths = gold
				? new string[] { "mobsprefab/gold_drop", "itemobject/prefabs/gold_drop", "itemobject/prefabs/GoldDrop" }
				: new string[] { "mobsprefab/item_drop", "itemobject/prefabs/item_drop", "itemobject/prefabs/ItemDrop" };
			GameObject prefab = null;
			for (int i = 0; i < paths.Length; i++)
			{
				prefab = Resources.Load<GameObject>(paths[i]);
				if (prefab != null)
				{
					break;
				}
			}
			GameObject go;
			if (prefab != null)
			{
				go = Object.Instantiate(prefab, new Vector3(info.x, info.y + 1.5f, info.z), Quaternion.identity);
			}
			else
			{
				go = GameObject.CreatePrimitive(gold ? PrimitiveType.Sphere : PrimitiveType.Cube);
				go.transform.position = new Vector3(info.x, info.y + 1.5f, info.z);
				go.transform.localScale = gold ? new Vector3(1.8f, 1.8f, 1.8f) : new Vector3(1.6f, 1.6f, 1.6f);
				foreach (Renderer r in go.GetComponentsInChildren<Renderer>(true))
				{
					r.material.color = gold ? new Color(1f, 0.85f, 0.2f) : new Color(0.9f, 0.9f, 1f);
				}
			}
			go.name = "Drop_" + info.id;
			var agent = go.GetComponent<UnityEngine.AI.NavMeshAgent>();
			if (agent != null)
			{
				agent.enabled = false;
			}
			var cc = go.GetComponent<CharacterController>();
			if (cc != null)
			{
				cc.enabled = false;
			}
			var rb = go.GetComponent<Rigidbody>();
			if (rb != null)
			{
				rb.isKinematic = true;
			}
			if (go.GetComponent<Collider>() == null)
			{
				SphereCollider col = go.AddComponent<SphereCollider>();
				col.radius = 1.5f;
				col.isTrigger = false;
			}
			WorldDropView view = go.AddComponent<WorldDropView>();
			view.Info = info;
			view._basePos = go.transform.position;
			GameObject labelGo = new GameObject("label");
			labelGo.transform.SetParent(go.transform, false);
			TextMesh mesh = labelGo.AddComponent<TextMesh>();
			mesh.text = view.LootText();
			mesh.fontSize = 34;
			mesh.characterSize = 0.12f;
			mesh.anchor = TextAnchor.LowerCenter;
			mesh.alignment = TextAlignment.Center;
			mesh.color = gold ? new Color(1f, 0.9f, 0.35f) : RarityColors[Mathf.Clamp(info.rarity, 0, RarityColors.Length - 1)];
			labelGo.transform.localPosition = new Vector3(0f, 2.4f, 0f);
			view._label = mesh;
			return view;
		}

		private string LootText()
		{
			if (Info.gold > 0)
			{
				return "Gold x" + Info.gold;
			}
			return WorldRuntime.ItemName(Info.mainId, Info.subId) + ((Info.count > 1) ? (" x" + Info.count) : string.Empty);
		}

		private static readonly Color[] RarityColors = new Color[5]
		{
			new Color(0.9f, 0.9f, 0.9f),   // Common — near white
			new Color(0.45f, 0.9f, 0.45f), // Uncommon — green
			new Color(0.45f, 0.7f, 1f),    // Rare — blue
			new Color(0.8f, 0.45f, 1f),    // Epic — purple
			new Color(1f, 0.75f, 0.2f)     // Legendary — orange
		};

		private void Update()
		{
			transform.Rotate(0f, 60f * Time.deltaTime, 0f, Space.World);
			transform.position = _basePos + Vector3.up * (Mathf.Sin(Time.unscaledTime * 2.5f) * 0.5f);
			if (_pickupSent || WorldRuntime.LocalPlayer == null)
			{
				return;
			}
			// Auto-pickup radius: local loot uses the same proximity rule as server loot.
			float autoSqr = Info.local ? 36f : 25f;
			if ((WorldRuntime.LocalPlayer.position - _basePos).sqrMagnitude <= autoSqr)
			{
				SendPickup();
			}
		}

		public void TryPickup()
		{
			if (_pickupSent || WorldRuntime.LocalPlayer == null)
			{
				return;
			}
			if ((WorldRuntime.LocalPlayer.position - _basePos).sqrMagnitude > 64f)
			{
				if (WorldHUD.Instance != null)
				{
					WorldHUD.Instance.ShowToast("Too far away");
				}
				return;
			}
			SendPickup();
		}

		private void SendPickup()
		{
			if (_pickupSent)
			{
				return;
			}
			// Local loot resolves entirely on the client — no server round-trip.
			if (Info.local)
			{
				_pickupSent = true;
				LocalLoot.ResolvePickup(Info);
				return;
			}
			WorldTcpClient client = WorldNet.Client;
			if (client == null)
			{
				return;
			}
			_pickupSent = true;
			client.SendPickup(Info.id);
		}

		public void OnRemoved()
		{
			if (_pickupSent && WorldHUD.Instance != null)
			{
				AudioManager.PlaySfx("pick");
				WorldHUD.Instance.ShowToast("Picked up " + LootText());
			}
			Destroy(gameObject);
		}
	}
}
