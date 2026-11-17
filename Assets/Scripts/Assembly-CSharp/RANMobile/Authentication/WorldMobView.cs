using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Scene view for one server mob: body (mob prefab or fallback capsule),
	/// nameplate and hp text. Position/dir/hp are driven from
	/// WorldRuntime.MobInfos (updated by MOB_MOVE/MOB_HP) and lerped here.
	/// </summary>
	public class WorldMobView : MonoBehaviour
	{
		public string Id;

		public bool Alive
		{
			get { return _alive; }
		}

		private WorldTcpClient.MobInfo _info;

		private bool _alive;

		private TextMesh _label;

		private TextMesh _hpLabel;

		private float _labelHeight = 18f;

		public static WorldMobView Create(WorldTcpClient.MobInfo info)
		{
			GameObject prefab = null;
			if (!string.IsNullOrEmpty(info.prefab))
			{
				prefab = Resources.Load<GameObject>("mobsprefab/" + info.prefab);
			}
			if (prefab == null && !string.IsNullOrEmpty(info.template))
			{
				prefab = Resources.Load<GameObject>("mobsprefab/" + info.template);
			}
			GameObject go;
			if (prefab != null)
			{
				go = Object.Instantiate(prefab, new Vector3(info.x, info.y, info.z), Quaternion.Euler(0f, info.dir, 0f));
			}
			else
			{
				go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
				go.transform.position = new Vector3(info.x, info.y, info.z);
				go.transform.rotation = Quaternion.Euler(0f, info.dir, 0f);
				go.transform.localScale = new Vector3(6f, 8f, 6f);
			_RendererColor(go, new Color(0.75f, 0.45f, 0.85f));
			}
			go.name = "Mob_" + info.name + "_" + info.id;
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
			WorldMobView view = go.AddComponent<WorldMobView>();
			view.Init(info);
			return view;
		}

		private static void _RendererColor(GameObject go, Color c)
		{
			foreach (Renderer r in go.GetComponentsInChildren<Renderer>(true))
			{
				r.material.color = c;
			}
		}

		private void Init(WorldTcpClient.MobInfo info)
		{
			_info = info;
			Id = info.id;
			_alive = true;
			foreach (TextMesh tm in GetComponentsInChildren<TextMesh>(true))
			{
				tm.gameObject.SetActive(false);
			}
			Transform trainingBot = transform.Find("TrainingBot");
			if (trainingBot != null)
			{
				trainingBot.gameObject.SetActive(false);
			}
			Transform nameDisplay = transform.Find("NameDisplay");
			if (nameDisplay != null)
			{
				nameDisplay.gameObject.SetActive(false);
			}
			float height = 15f;
			bool first = true;
			float minY = 0f;
			float maxY = 0f;
			foreach (Renderer r in GetComponentsInChildren<Renderer>(true))
			{
				if (r is SkinnedMeshRenderer smr && smr.sharedMesh == null)
				{
					continue;
				}
				if (first)
				{
					minY = r.bounds.min.y;
					maxY = r.bounds.max.y;
					first = false;
				}
				else
				{
					minY = Mathf.Min(minY, r.bounds.min.y);
					maxY = Mathf.Max(maxY, r.bounds.max.y);
				}
			}
			if (!first)
			{
				height = maxY - minY;
			}
			if (first || height < 2f)
			{
				height = 15f;
			}
			_labelHeight = height + 2f;
			_label = _MakeLabel("name", info.name, 44, new Color(1f, 0.55f, 0.45f));
			_hpLabel = _MakeLabel("hp", "Lv" + info.level + "  " + info.hp + "/" + info.maxHp, 36, Color.white);
			_label.transform.localPosition = new Vector3(0f, _labelHeight, 0f);
			_hpLabel.transform.localPosition = new Vector3(0f, _labelHeight + 2.6f, 0f);
		}

		private TextMesh _MakeLabel(string suffix, string text, int fontSize, Color color)
		{
			GameObject go = new GameObject(suffix);
			go.transform.SetParent(transform, false);
			TextMesh mesh = go.AddComponent<TextMesh>();
			mesh.text = text;
			mesh.fontSize = fontSize;
			mesh.characterSize = 0.12f;
			mesh.anchor = TextAnchor.LowerCenter;
			mesh.alignment = TextAlignment.Center;
			mesh.color = color;
			return mesh;
		}

		public void Refresh(WorldTcpClient.MobInfo info)
		{
			_info = info;
			_alive = true;
			if (!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}
			if (_label != null)
			{
				_label.text = info.name;
				_label.color = new Color(1f, 0.55f, 0.45f);
			}
			UpdateHpText(info.hp, info.maxHp);
		}

		public void OnDeath()
		{
			_alive = false;
			gameObject.SetActive(false);
		}

		private void UpdateHpText(int hp, int maxHp)
		{
			if (_hpLabel == null)
			{
				return;
			}
			_hpLabel.text = "Lv" + _info.level + "  " + hp + "/" + maxHp;
			float ratio = (maxHp > 0) ? ((float)hp / maxHp) : 1f;
			_hpLabel.color = Color.Lerp(new Color(1f, 0.3f, 0.25f), new Color(0.45f, 1f, 0.45f), ratio);
		}

		private void Update()
		{
			if (!_alive)
			{
				return;
			}
			WorldTcpClient.MobInfo info;
			if (!WorldRuntime.MobInfos.TryGetValue(Id, out info))
			{
				return;
			}
			_info = info;
			Vector3 target = new Vector3(info.x, info.y, info.z);
			if (Mathf.Abs(info.y) <= 0.01f)
			{
				RaycastHit hit;
				if (Physics.Raycast(target + Vector3.up * 50f, Vector3.down, out hit, 200f))
				{
					target = new Vector3(target.x, hit.point.y, target.z);
				}
			}
			transform.position = Vector3.Lerp(transform.position, target, 8f * Time.deltaTime);
			Quaternion rot = Quaternion.Euler(0f, info.dir, 0f);
			transform.rotation = Quaternion.Slerp(transform.rotation, rot, 8f * Time.deltaTime);
			UpdateHpText(info.hp, info.maxHp);
			if (_label != null && WorldRuntime.WorldCamera != null)
			{
				// keep the nameplate readable from any angle: TextMesh renders on its
				// +Z face, so +Z must point TOWARD the camera (label - camera reads it
				// from behind = mirrored text).
				_label.transform.rotation = Quaternion.LookRotation(WorldRuntime.WorldCamera.transform.position - _label.transform.position);
				if (_hpLabel != null)
				{
					_hpLabel.transform.rotation = _label.transform.rotation;
				}
			}
		}
	}
}
