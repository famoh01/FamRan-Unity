using System.Collections.Generic;
using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Applies the class default preparatory gear (coat / pants|skirt / boots / weapon)
	/// to character-select and character-create preview models. The original combined-
	/// mesh pipeline is stripped from the decompile, so this binds each SkinPieceObject's
	/// donor SkinnedMeshRenderer directly onto the model's Bip01 skeleton by bone name.
	/// </summary>
	public static class PreviewGearBinder
	{
		private const string ArmorRoot = "_ranclient/data/item skinobjects armor/";

		private const string WeaponRoot = "_ranclient/data/item skinobjects weapon/";

		private const string GearContainerName = "__gear";

		/// <param name="attachWeapon">False to skip the class-default weapon (the world player
		/// shows its actual equipped weapon instead, so the default one would double up).</param>
		public static void Apply(GameObject model, int school, bool female, int classId, bool attachWeapon = true)
		{
			if (model == null)
			{
				return;
			}
			Transform existing = FindDeep(model.transform, GearContainerName, 0);
			if (existing != null)
			{
				Object.Destroy(existing.gameObject);
			}
			Transform bip = FindDeep(model.transform, "Bip01", 0);
			if (bip == null)
			{
				Debug.LogWarning("[PreviewGearBinder] no Bip01 skeleton on " + model.name);
				return;
			}
			var bones = new Dictionary<string, Transform>();
			foreach (Transform t in bip.GetComponentsInChildren<Transform>(true))
			{
				bones[t.name] = t;
			}
			GameObject container = new GameObject(GearContainerName);
			container.transform.SetParent(model.transform, false);
			container.layer = model.layer;

			string prefix = SchoolPrefix(school);
			string g = female ? "w" : "m";
			string lower = female ? "_preparatory_skirt" : "_preparatory_pants";
			string[] armorPieces =
			{
				prefix + "_" + g + "_preparatory_body",
				prefix + "_" + g + lower,
				prefix + "_" + g + "_preparatory_boots"
			};
			bool upperOn = false;
			bool lowerOn = false;
			bool bootsOn = false;
			for (int i = 0; i < armorPieces.Length; i++)
			{
				if (AddPiece(container, bones, ArmorRoot + armorPieces[i], model.layer))
				{
					if (i == 0)
					{
						upperOn = true;
					}
					else if (i == 1)
					{
						lowerOn = true;
					}
					else
					{
						bootsOn = true;
					}
				}
			}
			string weapon = WeaponForClass(classId);
			if (attachWeapon && !string.IsNullOrEmpty(weapon))
			{
				AddWeapon(container, bones, WeaponRoot + weapon, model.layer);
			}
			CharacterBaseBody baseBody = model.GetComponentInChildren<CharacterBaseBody>(true);
			if (baseBody != null)
			{
				if (baseBody.charArmor != null)
				{
					baseBody.charArmor.enabled = !upperOn;
				}
				if (baseBody.charPants != null)
				{
					baseBody.charPants.enabled = !lowerOn;
				}
				if (baseBody.charBoots != null)
				{
					baseBody.charBoots.enabled = !bootsOn;
				}
			}
		}

		private static bool AddPiece(GameObject container, Dictionary<string, Transform> bones, string resourcePath, int layer)
		{
			SkinPieceObject skin = Resources.Load<SkinPieceObject>(resourcePath);
			if (skin == null || skin.skinMesh == null)
			{
				Debug.LogWarning("[PreviewGearBinder] missing skin piece: " + resourcePath);
				return false;
			}
			GameObject donor = Object.Instantiate(skin.skinMesh, container.transform, false);
			donor.name = skin.name;
			SetLayerRecursive(donor, layer);
			RebindSkinnedRenderers(donor, bones);
			return true;
		}

		private static void AddWeapon(GameObject container, Dictionary<string, Transform> bones, string resourcePath, int layer)
		{
			SkinPieceObject skin = Resources.Load<SkinPieceObject>(resourcePath);
			if (skin == null || skin.skinMesh == null)
			{
				Debug.LogWarning("[PreviewGearBinder] missing weapon skin: " + resourcePath);
				return;
			}
			Transform socket = FindSocket(bones, skin.slotBack);
			Transform parent = socket != null ? socket : container.transform;
			GameObject donor = Object.Instantiate(skin.skinMesh, parent, false);
			donor.name = skin.name;
			SetLayerRecursive(donor, layer);
			RebindSkinnedRenderers(donor, bones);
			BindWeaponToRigFrame(donor, bones);
			if (socket != null)
			{
				donor.transform.localPosition = Vector3.zero;
				donor.transform.localRotation = Quaternion.identity;
			}
		}

		/// <summary>
		/// The character rigs ship EMPTY weapon attach-frames (L_Hand/whal/BDN0023 →
		/// NoFrameName0 ...) named by each weapon's material code — the original binds
		/// weapon meshes to those frames (that's the mesh's declared [COMBINED] bone).
		/// Generic bone-name rebinding can't do this (stripped names are ambiguous), so
		/// the donor's skinning is bound explicitly: material code → rig frame child.
		/// This is what put the archer's bow in the right place.
		/// </summary>
		private static void BindWeaponToRigFrame(GameObject donor, Dictionary<string, Transform> bones)
		{
			SkinnedMeshRenderer smr = donor.GetComponentInChildren<SkinnedMeshRenderer>(true);
			if (smr == null || smr.bones == null || smr.bones.Length == 0)
			{
				return;
			}
			string code = null;
			if (smr.sharedMaterial != null && smr.sharedMaterial.name != null)
			{
				code = smr.sharedMaterial.name.Split('.')[0];
			}
			if (string.IsNullOrEmpty(code))
			{
				return;
			}
			Transform anchor;
			if (!bones.TryGetValue(code, out anchor))
			{
				Debug.LogWarning("[PreviewGearBinder] no rig frame '" + code + "' for " + donor.name);
				return;
			}
			// frame = the anchor's stripped child (the mesh's authored bone)
			Transform frame = null;
			for (int i = 0; i < anchor.childCount; i++)
			{
				Transform c = anchor.GetChild(i);
				if (IsStrippedBoneName(c.name))
				{
					frame = c;
					break;
				}
			}
			if (frame == null)
			{
				frame = anchor;
			}
			var rebound = new Transform[smr.bones.Length];
			for (int i = 0; i < rebound.Length; i++)
			{
				rebound[i] = frame;
			}
			smr.bones = rebound;
			smr.rootBone = frame;
			Debug.Log("[Gear] weapon bound to rig frame " + code + "/" + frame.name + " (donor " + donor.name + ")");
		}

		/// <summary>
		/// Resolves the hold socket from the skin's declared slotBack. The original
		/// switches by mode (DxCharPart): ATTACK mode follows the mesh-declared HAND
		/// bone — bow meshes use 'whal' (Bip01_L_Hand), stick meshes 'stick'
		/// (Bip01_R_Hand) — while PEACE mode uses the where-back table
		/// (DxPieceDefine.h: 0 slot_rhand / 1 whal_slot / 2 stick_rslot). Our game
		/// runs field/combat, so hand bones first; the where-back slots remain the
		/// fallback chain (original whal_slot → slot_lhand included).
		/// </summary>
		private static Transform FindSocket(Dictionary<string, Transform> bones, SlotBack slotBack)
		{
			string[] chain;
			switch (slotBack)
			{
				case SlotBack.whal_slot:
					// bow family — rides the LEFT hand so the combat stance carries it
					chain = new string[] { "whal", "whal_slot", "slot_lhand", "slot_rhand" };
					break;
				case SlotBack.stick_rslot:
					// stick family — rides the RIGHT hand
					chain = new string[] { "stick", "stick_rslot", "slot_rhand" };
					break;
				case SlotBack.r_spear_slot:
					chain = new string[] { "r_spear_slot", "stick", "slot_rhand" };
					break;
				case SlotBack.l_spear_slot:
					chain = new string[] { "l_spear_slot", "whal", "slot_rhand" };
					break;
				default:
					// slot_rhand (0) / none (5): unchanged for sword/gauntlet/dagger
					chain = new string[] { "slot_rhand", "slot_lhand", "whal_slot", "stick_rslot" };
					break;
			}
			for (int i = 0; i < chain.Length; i++)
			{
				Transform t;
				if (bones.TryGetValue(chain[i], out t))
				{
					return t;
				}
			}
			return null;
		}

		/// <summary>Rip-stripped bone names (AssetRipper placeholders) are NOT real rig
		/// references — rebinding them to a same-named player bone warps weapon meshes
		/// onto unrelated sub-rigs (that's what hid the archer's bow). Real bones
		/// (Bip01_*, whal, ...) still rebind so armor follows the body.</summary>
		private static bool IsStrippedBoneName(string n)
		{
			return !string.IsNullOrEmpty(n) &&
				(n.StartsWith("NoFrameName") || n == "blank" || n.StartsWith("blank "));
		}

		private static void RebindSkinnedRenderers(GameObject root, Dictionary<string, Transform> bones)
		{
			SkinnedMeshRenderer[] renderers = root.GetComponentsInChildren<SkinnedMeshRenderer>(true);
			for (int r = 0; r < renderers.Length; r++)
			{
				SkinnedMeshRenderer smr = renderers[r];
				Transform[] oldBones = smr.bones;
				if (oldBones == null || oldBones.Length == 0)
				{
					continue;
				}
				var rebound = new Transform[oldBones.Length];
				int missing = 0;
				for (int i = 0; i < oldBones.Length; i++)
				{
					Transform target;
					if (oldBones[i] != null && !IsStrippedBoneName(oldBones[i].name) &&
						bones.TryGetValue(oldBones[i].name, out target))
					{
						rebound[i] = target;
					}
					else
					{
						rebound[i] = oldBones[i];
						missing++;
					}
				}
				smr.bones = rebound;
				if (missing > 0 && !IsStrippedBoneName(oldBones[0] != null ? oldBones[0].name : ""))
				{
					Debug.LogWarning("[PreviewGearBinder] " + root.name + ": " + missing + " bones unresolved.");
				}
				if (smr.rootBone != null && !IsStrippedBoneName(smr.rootBone.name))
				{
					Transform rb;
					if (bones.TryGetValue(smr.rootBone.name, out rb))
					{
						smr.rootBone = rb;
					}
				}
			}
		}

		private static string SchoolPrefix(int school)
		{
			switch (school)
			{
				case 1:
					return "mp";
				case 2:
					return "phx";
				default:
					return "sg";
			}
		}

		private static string WeaponForClass(int classId)
		{
			switch (classId)
			{
				case 4:
					return "Short Bow";
				case 8:
					return "Stick";
				case 1:
				case 2:
					return "Club";
				default:
					return null;
			}
		}

		private const string WorldWeaponName = "__world_weapon";

		/// <summary>Show a character's equipped weapon in-hand (world player). The skin mesh is
		/// bound to the Bip01 hand socket by bone name, exactly like the preview binder, so the
		/// held weapon follows the arm animation.</summary>
		public static void ApplyEquippedWeapon(GameObject model, int mainId, int subId)
		{
			ClearWeapon(model);
			if (model == null)
			{
				return;
			}
			ItemObject io;
			if (!CharacterStatBlock.TryGetItem(mainId, subId, out io) || io == null)
			{
				UnityEngine.Debug.Log("[Gear] no ItemObject for " + mainId + "/" + subId);
				return;
			}
			SkinPieceObject skin = io.characterDisplay;
			if (skin == null || skin.skinMesh == null)
			{
				UnityEngine.Debug.Log("[Gear] no skinMesh for " + mainId + "/" + subId + " (" + io.name + ")");
				return;
			}
			Transform bip = FindDeep(model.transform, "Bip01", 0);
			if (bip == null)
			{
				UnityEngine.Debug.LogWarning("[Gear] no Bip01 skeleton on " + model.name);
				return;
			}
			var bones = new Dictionary<string, Transform>();
			foreach (Transform t in bip.GetComponentsInChildren<Transform>(true))
			{
				bones[t.name] = t;
			}
			Transform socket = FindSocket(bones, skin.slotBack);
			Transform parent = (socket != null) ? socket : model.transform;
			GameObject donor = Object.Instantiate(skin.skinMesh, parent, false);
			donor.name = WorldWeaponName;
			SetLayerRecursive(donor, model.layer);
			RebindSkinnedRenderers(donor, bones);
			BindWeaponToRigFrame(donor, bones);
			if (socket != null)
			{
				donor.transform.localPosition = Vector3.zero;
				donor.transform.localRotation = Quaternion.identity;
			}
			UnityEngine.Debug.Log("[Gear] weapon '" + skin.name + "' socket=" + (socket != null ? socket.name : "ROOT") + " slotBack=" + skin.slotBack);
			var smr = donor.GetComponentInChildren<SkinnedMeshRenderer>();
			if (smr != null)
			{
				UnityEngine.Debug.Log("[Gear] smr enabled=" + smr.enabled +
					" mat=" + (smr.sharedMaterial != null ? smr.sharedMaterial.name : "NULL") +
					" shader=" + (smr.sharedMaterial != null && smr.sharedMaterial.shader != null ? smr.sharedMaterial.shader.name : "NULL") +
					" bounds=" + smr.bounds.size + " center=" + smr.bounds.center +
					" layer=" + donor.layer + " scale=" + donor.transform.localScale.x +
					" rootBone=" + (smr.rootBone != null ? smr.rootBone.name : "NULL") +
					" bones=" + (smr.bones != null ? smr.bones.Length : -1));
			}
			else
			{
				UnityEngine.Debug.LogWarning("[Gear] weapon has NO SkinnedMeshRenderer! children=" + donor.GetComponentsInChildren<Renderer>(true).Length);
			}
		}

		/// <summary>Remove the world in-hand weapon visual (called on unequip / no weapon).</summary>
		public static void ClearWeapon(GameObject model)
		{
			if (model == null)
			{
				return;
			}
			Transform prev = FindDeep(model.transform, WorldWeaponName, 0);
			if (prev != null)
			{
				UnityEngine.Debug.Log("[Gear] cleared previous weapon at " + RelPath(model.transform, prev));
				UnityEngine.Object.Destroy(prev.gameObject);
			}
			else
			{
				UnityEngine.Debug.Log("[Gear] ClearWeapon: none found");
			}
		}

		private static string RelPath(Transform root, Transform t)
		{
			string p = t.name;
			while (t != null && t != root)
			{
				t = t.parent;
				if (t != null)
				{
					p = t.name + "/" + p;
				}
			}
			return p;
		}

		private static void SetLayerRecursive(GameObject go, int layer)
		{
			foreach (Transform t in go.GetComponentsInChildren<Transform>(true))
			{
				t.gameObject.layer = layer;
			}
		}

		private static Transform FindDeep(Transform root, string name, int depth)
		{
			if (root == null || depth > 20)
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
	}
}
