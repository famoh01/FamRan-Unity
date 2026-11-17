using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Scene view for one static quest NPC (P5): character-model body with the class
	/// idle animation, a floating name label and a talk collider. Clicking it (via
	/// WorldHUD's ground raycast) opens the server-driven NPC talk dialog.
	/// </summary>
	public class WorldNpcView : MonoBehaviour
	{
		public string NpcId;

		public string DisplayName;

		public static WorldNpcView Create(WorldTcpClient.NpcInfo info)
		{
			GameObject go;
			GameObject prefab = Resources.Load<GameObject>("prefabs/characterselection/m_playerS");
			if (prefab != null)
			{
				go = Object.Instantiate(prefab, new Vector3(info.x, 0f, info.z), Quaternion.identity);
				go.name = "Npc_" + info.id;
				// strip runtime controllers/agents the prefab may carry
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
				// clothes + idle animation (any class controller plays a standing idle)
				PreviewGearBinder.Apply(go, 0, false, 2, attachWeapon: false);
				Animator anim = go.GetComponent<Animator>();
				if (anim != null)
				{
					RuntimeAnimatorController ctrl =
						Resources.Load<RuntimeAnimatorController>("Animator/Brawler");
					if (ctrl != null)
					{
						anim.runtimeAnimatorController = ctrl;
					}
					anim.SetBool("isStay", true);
				}
			}
			else
			{
				go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
				go.transform.position = new Vector3(info.x, 8f, info.z);
				go.transform.localScale = new Vector3(6f, 8f, 6f);
				go.name = "Npc_" + info.id;
			}
			// talk collider: a trigger box around the body that the HUD raycast can hit
			GameObject trigger = new GameObject("TalkTrigger", typeof(RectTransform));
			trigger.transform.SetParent(go.transform, false);
			trigger.transform.localPosition = new Vector3(0f, 8f, 0f);
			trigger.transform.localScale = Vector3.one;
			BoxCollider box = trigger.AddComponent<BoxCollider>();
			box.isTrigger = true;
			box.size = new Vector3(10f, 20f, 10f);

			WorldNpcView view = go.AddComponent<WorldNpcView>();
			view.NpcId = info.id;
			view.DisplayName = info.name;
			view.BuildLabel(info.name);
			return view;
		}

		private void BuildLabel(string name)
		{
			GameObject labelGo = new GameObject("NpcLabel");
			labelGo.transform.SetParent(transform, false);
			labelGo.transform.localPosition = new Vector3(0f, 22f, 0f);
			TextMesh tm = labelGo.AddComponent<TextMesh>();
			tm.text = name;
			tm.fontSize = 48;
			tm.characterSize = 1.2f;
			tm.anchor = TextAnchor.LowerCenter;
			tm.alignment = TextAlignment.Center;
			tm.color = new Color(0.6f, 1f, 0.7f, 1f);
			MeshRenderer mr = labelGo.GetComponent<MeshRenderer>();
			if (mr != null)
			{
				Shader shader = Shader.Find("GUI/Text Shader");
				if (shader != null)
				{
					mr.sharedMaterial = new Material(shader);
				}
			}
		}
	}
}
