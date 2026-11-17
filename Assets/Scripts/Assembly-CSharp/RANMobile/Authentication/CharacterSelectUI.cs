using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Core.Enumerators;
using Models.Library.Shared.Game;
using Models.Network.Shared.Response;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using StringBuilder = System.Text.StringBuilder;

namespace RANMobile.Authentication
{
	[DisallowMultipleComponent]
	public class CharacterSelectUI : MonoBehaviour
	{
		[Serializable]
		public class SlotConfig
		{
			[Tooltip("Optional list-entry UI for this slot, hidden once a character occupies it.")]
			public GameObject listEntry;

			public Transform maleSpawn;

			public Transform femaleSpawn;

			[Tooltip("Empty transform holding the perfect camera framing for this pedestal.")]
			public Transform cameraView;
		}

		[Serializable]
		public class ClassVisual
		{
			public CharacterClassEnum classEnum;

			[Tooltip("Preview prefab for this class (male and female classes usually share one each).")]
			public GameObject previewPrefab;

			[Tooltip("Spawn under the slot's Female Spawn instead of Male Spawn.")]
			public bool useFemaleSpawn;

			[Tooltip("Resources path, e.g. Animator/Brawler")]
			public string animatorResourcePath;

			[Header("Profile")]
			public Sprite icon;

			public Sprite defaultWeaponIcon;

			public string classDescLocKey;

			public string weaponTypeLocKey;

			public string starterWeaponLocKey;

			[NonSerialized]
			public RuntimeAnimatorController controller;

			[NonSerialized]
			public bool controllerLoaded;

			public RuntimeAnimatorController GetController()
			{
				return null;
			}
		}

		[Serializable]
		public class ActionButtons
		{
			[Tooltip("Deletes the current account. Redirects to the website.")]
			public Button deleteAccountButton;

			[Tooltip("Visible by default. Wire its onClick to CharacterCreateUI.OpenCreation().")]
			public Button createButton;

			[Tooltip("Hidden until a character is selected.")]
			public Button deleteButton;

			[Tooltip("Hidden until a character is selected.")]
			public Button startButton;

			[Tooltip("Clears the selection and returns to the default view.")]
			public Button backButton;

			[Tooltip("Only shown when more than one character exists.")]
			public Button previousCharButton;

			public Button nextCharButton;

			[Tooltip("Disconnects and returns to the server list. Always visible.")]
			public Button serverSelectButton;

			[Tooltip("Text component the remaining-character count is written into.")]
			public TextMeshProUGUI createRemainText;

			[Tooltip("The background Image the remaining text sits inside — THIS is what gets shown/hidden, so the backdrop goes with the text. Leave empty to toggle the text object itself.")]
			public GameObject createRemainRoot;
		}

		[Serializable]
		public class ProfilePanel
		{
			public GameObject root;

			[Header("Summary")]
			public TextMeshProUGUI nameText;

			public TextMeshProUGUI levelText;

			public TextMeshProUGUI classText;

			public TextMeshProUGUI schoolText;

			public TextMeshProUGUI clubText;

			[Tooltip("The ExpBar fill image itself, not the BG or the border.")]
			public Image expBarFill;

			[Header("Lore")]
			public TextMeshProUGUI classNameText;

			public TextMeshProUGUI classDescText;

			public TextMeshProUGUI weaponNameText;

			public TextMeshProUGUI weaponTypeText;

			[Header("Icons")]
			public Image classIcon;

			public Image schoolIcon;

			public Image weaponIcon;

			[Tooltip("Indexed by wSchool: 0 Sacred Gate, 1 Mystic Peak, 2 Phoenix.")]
			public Sprite[] schoolIcons;
		}

		[Serializable]
		public class CameraConfig
		{
			[Tooltip("Left empty, Camera.main is used.")]
			public Camera camera;

			public float zoomDuration;
		}

		[Serializable]
		public class PickingConfig
		{
			[Tooltip("Restrict the pick ray to the layer the character previews live on — the old code tested every collider in the scene.")]
			public LayerMask layerMask;

			public float maxDistance;

			public string characterTag;

			[Tooltip("Turn off if you drive selection purely from UI buttons.")]
			public bool enableWorldClickPicking;
		}

		private sealed class Preview
		{
			public int slot;

			public GameCharData2 data;

			public GameObject go;

			public Transform originalParent;

			public Animator animator;

			public PlayerEquipmentSelect equipment;

			public ClassVisual visual;

			public bool hasWeapon;

			public bool equipped;

			public GameObject effect;
		}

		[CompilerGenerated]
		private sealed class _003CEquipPreviewsWhenItemTableReady_003Ed__56 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public CharacterSelectUI _003C_003E4__this;

			private int _003Ci_003E5__2;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CEquipPreviewsWhenItemTableReady_003Ed__56(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CMoveCamera_003Ed__79 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public CharacterSelectUI _003C_003E4__this;

			public Vector3 targetPos;

			public Quaternion targetRot;

			private Transform _003Ctf_003E5__2;

			private float _003Cduration_003E5__3;

			private Vector3 _003CstartPos_003E5__4;

			private Quaternion _003CstartRot_003E5__5;

			private float _003Celapsed_003E5__6;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CMoveCamera_003Ed__79(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[Header("Slots")]
		public SlotConfig[] slots;

		[Header("Classes")]
		public ClassVisual[] classVisuals;

		[Header("UI")]
		public ActionButtons buttons;

		public ProfilePanel profile;

		[Header("Scene")]
		public CameraConfig cameraSettings;

		public PickingConfig picking;

		[Header("Delete Account Website")]
		[SerializeField]
		private string _websiteBaseUrl;

		[SerializeField]
		private string _deleteAccountPath;

		[SerializeField]
		private float _deleteAccountRetapCooldown;

		[Header("Performance")]
		[Tooltip("Off-screen previews stop writing bone transforms — the three you are not zoomed into cost almost nothing. CullCompletely is cheaper still, but an animator trigger set while the character is off-camera can be dropped.")]
		public AnimatorCullingMode previewCullingMode;

		public readonly List<CharacterSelected> selectedCharacters = new List<CharacterSelected>();

		public bool isInspecting;

		private bool _screenActive;

		private Preview[] _previews;

		private Preview _inspecting;

		private Coroutine _equipRoutine;

		private readonly Dictionary<CharacterClassEnum, ClassVisual> _visualByClass = new Dictionary<CharacterClassEnum, ClassVisual>();

		private readonly Dictionary<AnimationType, int> _idleWithWeaponHash = new Dictionary<AnimationType, int>();

		private static readonly int SelectedWeaponTrigger;

		private int _idleNoWeaponHash;

		private bool _idleNoWeaponHashResolved;

		private Vector3 _cameraHomePosition;

		private Quaternion _cameraHomeRotation;

		private Transform _cameraTransform;

		private Coroutine _cameraMove;

		public static CharacterSelectUI Active { get; private set; }

		[Serializable]
		private class CharEntry
		{
			public string id;

			public string name;

			public int slot;

			public int cls;

			public int school;

			public int gender;

			public int level;

			public int exp;

			public int hair;

			public int face;

			public int hairColor;
		}

		[Serializable]
		private class CharListDto
		{
			public CharDto[] characters;

			public int max;
		}

		[Serializable]
		private class CharDto
		{
			public string id;

			public string name;

			public int slot;

			public int @class;

			public int school;

			public int gender;

			public int level;

			public int exp;

			public int hairType;

			public int faceType;

			public int hairColorType;
		}

		private readonly List<CharEntry> _charList = new List<CharEntry>();

		private int _maxChars = 4;

		private int _selectedSlot = -1;

		private bool _fetched;

		private float _lastPickTime = -10f;

		private int _lastPickedSlot = -1;

		private float _pickGraceUntil;

		private const float DoubleTapThreshold = 0.5f;

		private const float PickGraceSeconds = 0.35f;

		private const string ClassBrawler = "Brawler";

		private const string ClassSwordsman = "Swordsman";

		private const string ClassArcher = "Archer";

		private const string ClassShaman = "Shaman";

		public void SetScreenActive(bool active)
		{
			_screenActive = active;
		}

		public void StopCameraMove()
		{
			if (_cameraMove != null)
			{
				StopCoroutine(_cameraMove);
				_cameraMove = null;
			}
		}

		private void SetPreviewsVisible(bool visible)
		{
			if (_previews == null)
			{
				return;
			}
			for (int i = 0; i < _previews.Length; i++)
			{
				if (_previews[i] != null && _previews[i].go != null)
				{
					_previews[i].go.SetActive(visible);
				}
			}
		}

		private void SnapCameraHome()
		{
			Camera cam = ResolveCamera();
			if (cam == null)
			{
				return;
			}
			StopCameraMove();
			_cameraTransform.SetPositionAndRotation(_cameraHomePosition, _cameraHomeRotation);
		}

		private Camera ResolveCamera()
		{
			if (_cameraTransform != null)
			{
				return _cameraTransform.GetComponent<Camera>();
			}
			Camera cam = (cameraSettings != null && cameraSettings.camera != null) ? cameraSettings.camera : Camera.main;
			if (cam == null)
			{
				return null;
			}
			_cameraTransform = cam.transform;
			_cameraHomePosition = _cameraTransform.position;
			_cameraHomeRotation = _cameraTransform.rotation;
			return cam;
		}

		private void Awake()
		{
			Active = this;
		}

		private void OnEnable()
		{
			// Re-runs after a domain reload mid-play: re-wire the buttons and
			// re-fetch (non-serialized state was wiped, so _fetched is false again).
			Active = this;
			// Scene loads mid-click can deliver a phantom GetMouseButtonDown on
			// the first frame; ignore picks briefly and clear double-tap state.
			_lastPickTime = -10f;
			_lastPickedSlot = -1;
			_pickGraceUntil = Time.unscaledTime + PickGraceSeconds;
			WireButtons();
			// The profile panel is hidden until a character is actually fetched
			// and selected — an account with no characters must not see it.
			if (profile.root != null)
			{
				profile.root.SetActive(false);
			}
			ResolveCamera();
			if (!_fetched)
			{
				FetchCharacterData();
			}
		}

		private void OnDestroy()
		{
		}

		private void Start()
		{
		}

		private void WireButtons()
		{
			AddClick(buttons.createButton, OnCreateClicked);
			AddClick(buttons.deleteButton, OnDeleteClicked);
			AddClick(buttons.startButton, OnStartClicked);
			AddClick(buttons.backButton, OnBackClicked);
			AddClick(buttons.previousCharButton, OnPreviousCharacterClick);
			AddClick(buttons.nextCharButton, OnNextCharacterClick);
			AddClick(buttons.serverSelectButton, OnServerSelectClicked);
			AddClick(buttons.deleteAccountButton, OnDeleteAccountClicked);
		}

		public void OnServerSelectClicked()
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("ClientLoginTemp");
		}

		public void OnDeleteAccountClicked()
		{
			string baseurl = string.IsNullOrEmpty(_websiteBaseUrl) ? "http://127.0.0.1:8080" : _websiteBaseUrl;
			string path = string.IsNullOrEmpty(_deleteAccountPath) ? "/account/delete" : _deleteAccountPath;
			OpenDeleteAccountPage(baseurl.TrimEnd('/') + path);
		}

		private void OpenDeleteAccountPage(string url)
		{
			Debug.Log("[CharacterSelectUI] Delete Account -> " + url);
			Application.OpenURL(url);
		}

		private static void AddClick(Button button, UnityAction action)
		{
			if (button == null || action == null)
			{
				return;
			}
			button.onClick.RemoveListener(action);
			button.onClick.AddListener(action);
		}

		private void ValidateWiring()
		{
		}

		public void OnCreateClicked()
		{
			CharacterCreateUI create = FindObjectOfType<CharacterCreateUI>();
			if (create != null)
			{
				create.OpenCreation();
			}
			else
			{
				Debug.LogWarning("[CharacterSelectUI] Create pressed but no CharacterCreateUI present.");
			}
		}

		public void OnDeleteClicked()
		{
			if (_selectedSlot < 0 || _selectedSlot >= _charList.Count)
			{
				return;
			}
			CharEntry e = _charList[_selectedSlot];
			if (string.IsNullOrEmpty(e.id))
			{
				Debug.LogWarning("[CharacterSelectUI] cannot delete: no character id.");
				return;
			}
			if (string.IsNullOrEmpty(Authentication.SessionToken))
			{
				Debug.LogWarning("[CharacterSelectUI] cannot delete: no session.");
				return;
			}
			Debug.Log("[CharacterSelectUI] deleting character " + e.name + " (" + e.id + ")");
			// There is no Authentication in this scene — spawn a throwaway one so
			// the request runs on a real coroutine host (same trick as creation).
			GameObject authGo = new GameObject("TempDeleteAuth");
			Authentication auth = authGo.AddComponent<Authentication>();
			auth.OnDeleteCharacterFailed.AddListener(delegate(string msg)
			{
				Debug.LogWarning("[CharacterSelectUI] delete failed: " + msg);
				if (buttons != null && buttons.deleteButton != null)
				{
					buttons.deleteButton.interactable = true;
				}
				UnityEngine.Object.Destroy(authGo);
			});
			if (buttons != null && buttons.deleteButton != null)
			{
				buttons.deleteButton.interactable = false;
			}
			auth.DeleteCharacterUser(e.id);
		}

		public void OnStartClicked()
		{
			var login = FindObjectOfType<CharacterLoginManager>();
			if (login != null)
			{
				login.StartGame();
			}
			else
			{
				Debug.LogWarning("[CharacterSelectUI] Start pressed but no CharacterLoginManager present.");
			}
		}

		private void Update()
		{
			// World picking: tapping a standing character selects it; double-tapping
			// the selected character enters the game world (like the live client).
			if (picking == null || !picking.enableWorldClickPicking || _previews == null || !_fetched)
			{
				return;
			}
			if (Time.unscaledTime < _pickGraceUntil)
			{
				return;
			}
			if (!Input.GetMouseButtonDown(0))
			{
				return;
			}
			var es = UnityEngine.EventSystems.EventSystem.current;
			if (es != null && es.IsPointerOverGameObject())
			{
				return;
			}
			Camera cam = ResolveCamera();
			if (cam == null)
			{
				return;
			}
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			int mask = picking.layerMask.value == 0 ? ~0 : picking.layerMask.value;
			float dist = picking.maxDistance > 0f ? picking.maxDistance : 10000f;
			if (!Physics.Raycast(ray, out hit, dist, mask))
			{
				return;
			}
			int slot = -1;
			for (int i = 0; i < _previews.Length; i++)
			{
				Preview p = _previews[i];
				if (p == null || p.go == null)
				{
					continue;
				}
				if (hit.collider.gameObject == p.go || hit.collider.transform.IsChildOf(p.go.transform))
				{
					slot = p.slot;
					break;
				}
			}
			if (slot < 0)
			{
				return;
			}
			float now = Time.unscaledTime;
			if (slot == _lastPickedSlot && now - _lastPickTime <= DoubleTapThreshold)
			{
				_lastPickTime = -10f;
				_lastPickedSlot = -1;
				SelectSlot(slot);
				OnStartClicked();
				return;
			}
			_lastPickTime = now;
			_lastPickedSlot = slot;
			SelectSlot(slot);
		}

		public void FetchCharacterData()
		{
			string token = Authentication.SessionToken;
			string baseurl = Authentication.BaseUrl;
			if (string.IsNullOrEmpty(baseurl))
			{
				baseurl = "http://127.0.0.1:8080";
			}
			if (string.IsNullOrEmpty(token))
			{
				Debug.LogWarning("[CharacterSelectUI] no session token — character fetch skipped.");
				_fetched = true;
				return;
			}
			StartCoroutine(FetchCharactersRoutine(baseurl.TrimEnd('/'), token));
		}

		private IEnumerator FetchCharactersRoutine(string baseurl, string token)
		{
			using (UnityWebRequest req = UnityWebRequest.Get(baseurl + "/api/characters"))
			{
				req.SetRequestHeader("Authorization", "Bearer " + token);
				yield return req.SendWebRequest();
				if (req.result != UnityWebRequest.Result.Success)
				{
					Debug.LogWarning("[CharacterSelectUI] character fetch failed: HTTP " + req.responseCode + " " + req.downloadHandler.text);
					_fetched = true;
					yield break;
				}
				CharListDto dto = null;
				try
				{
					dto = JsonUtility.FromJson<CharListDto>(req.downloadHandler.text);
				}
				catch (Exception e)
				{
					Debug.LogWarning("[CharacterSelectUI] bad characters payload: " + e.Message);
				}
				_fetched = true;
				if (dto != null)
				{
					OnCharactersLoaded(dto);
				}
			}
		}

		private void OnCharactersLoaded(CharListDto dto)
		{
			_charList.Clear();
			if (dto.characters != null)
			{
				foreach (CharDto c in dto.characters)
				{
					_charList.Add(new CharEntry
					{
						id = c.id,
						name = c.name,
						slot = c.slot,
						cls = c.@class,
						school = c.school,
						gender = c.gender,
						level = c.level,
						exp = c.exp,
						hair = c.hairType,
						face = c.faceType,
						hairColor = c.hairColorType
					});
				}
			}
			_maxChars = dto.max > 0 ? dto.max : 4;
			StringBuilder names = new StringBuilder();
			foreach (CharEntry e in _charList)
			{
				if (names.Length > 0)
				{
					names.Append(", ");
				}
				names.Append(e.name);
			}
			Debug.Log("[CharacterSelectUI] fetched " + _charList.Count + "/" + _maxChars + " characters: " + names);
			if (buttons.createRemainText != null)
			{
				buttons.createRemainText.text = "You have (" + (_maxChars - _charList.Count) + ") Remaining Character.";
			}
			// Stand every character on its stage slot, unselected — the live
			// client shows the models first; tapping one selects it.
			ClearPreviews();
			int spawnCount = Mathf.Min(_charList.Count, slots != null ? slots.Length : 0);
			if (spawnCount > 0)
			{
				_previews = new Preview[spawnCount];
				for (int i = 0; i < spawnCount; i++)
				{
					SpawnPreview(i, _charList[i]);
				}
			}
			ClearSelection(false);
		}

		private static string ClassName(int cls)
		{
			switch (cls)
			{
			case 0:
			case 1:
				return ClassBrawler;
			case 2:
				return ClassSwordsman;
			case 4:
				return ClassArcher;
			case 8:
				return ClassShaman;
			default:
				return "Unknown";
			}
		}

		private static int ClassVisualIndex(int cls)
		{
			switch (cls)
			{
			case 0:
			case 1:
				return 0;
			case 2:
				return 1;
			case 4:
				return 2;
			case 8:
				return 3;
			default:
				return 0;
			}
		}

		private static string SchoolName(int school)
		{
			switch (school)
			{
			case 1:
				return "Mystic Peak";
			case 2:
				return "Phoenix";
			default:
				return "Sacred Gate";
			}
		}

		private static string ClassDescription(int cls)
		{
			switch (cls)
			{
			case 0:
			case 1:
				return "A tough close-range fighter who overwhelms enemies with fast hand-to-hand combos.";
			case 2:
				return "A balanced swordfighter who cuts through the front line with precise slashes.";
			case 4:
				return "A nimble ranged attacker who picks off enemies from a safe distance.";
			case 8:
				return "A mystical fighter who channels the power of the elements in combat.";
			default:
				return string.Empty;
			}
		}

		private static string DefaultWeaponName(int cls)
		{
			switch (cls)
			{
			case 0:
			case 1:
				return "Gauntlets";
			case 2:
				return "Short Sword";
			case 4:
				return "Hunting Bow";
			case 8:
				return "Oak Wand";
			default:
				return "Weapon";
			}
		}

		private static string DefaultWeaponType(int cls)
		{
			switch (cls)
			{
			case 0:
			case 1:
				return "Gauntlet";
			case 2:
				return "Sword";
			case 4:
				return "Bow";
			case 8:
				return "Wand";
			default:
				return "Weapon";
			}
		}

		private void PopulateProfile(CharEntry e)
		{
			int ci = ClassVisualIndex(e.cls);
			ClassVisual visual = (ci >= 0 && ci < classVisuals.Length) ? classVisuals[ci] : null;
			if (profile.nameText != null)
			{
				profile.nameText.text = "Name: " + e.name;
			}
			if (profile.levelText != null)
			{
				profile.levelText.text = "Level: " + e.level;
			}
			if (profile.classText != null)
			{
				profile.classText.text = "Class: " + ClassName(e.cls);
			}
			if (profile.schoolText != null)
			{
				profile.schoolText.text = "Faction: " + SchoolName(e.school);
			}
			if (profile.clubText != null)
			{
				profile.clubText.text = "None";
			}
			if (profile.expBarFill != null)
			{
				profile.expBarFill.fillAmount = 0f;
			}
			if (profile.classIcon != null && visual != null && visual.icon != null)
			{
				profile.classIcon.sprite = visual.icon;
			}
			if (profile.schoolIcon != null && profile.schoolIcons != null && e.school >= 0 && e.school < profile.schoolIcons.Length && profile.schoolIcons[e.school] != null)
			{
				profile.schoolIcon.sprite = profile.schoolIcons[e.school];
			}
			if (profile.weaponIcon != null && visual != null && visual.defaultWeaponIcon != null)
			{
				profile.weaponIcon.sprite = visual.defaultWeaponIcon;
			}
			if (profile.classNameText != null)
			{
				profile.classNameText.text = ClassName(e.cls);
			}
			if (profile.classDescText != null)
			{
				profile.classDescText.text = ClassDescription(e.cls);
			}
			if (profile.weaponNameText != null)
			{
				profile.weaponNameText.text = DefaultWeaponName(e.cls);
			}
			if (profile.weaponTypeText != null)
			{
				profile.weaponTypeText.text = DefaultWeaponType(e.cls);
			}
		}

		private void OnCharacterListReceived(CharacterListResponse response)
		{
		}

		private void SpawnPreview(int slotIndex, CharEntry data)
		{
			if (slots == null || classVisuals == null || data == null || _previews == null)
			{
				return;
			}
			if (slotIndex < 0 || slotIndex >= slots.Length || slotIndex >= _previews.Length)
			{
				return;
			}
			int vi = ClassVisualIndex(data.cls);
			if (vi < 0 || vi >= classVisuals.Length)
			{
				return;
			}
			ClassVisual visual = classVisuals[vi];
			if (visual.previewPrefab == null)
			{
				return;
			}
			SlotConfig slot = slots[slotIndex];
			Transform spawn = visual.useFemaleSpawn ? slot.femaleSpawn : slot.maleSpawn;
			if (spawn == null)
			{
				spawn = (slot.maleSpawn != null) ? slot.maleSpawn : slot.femaleSpawn;
			}
			if (spawn == null)
			{
				Debug.LogWarning("[CharacterSelectUI] slot " + slotIndex + " has no spawn point.");
				return;
			}
			GameObject go = UnityEngine.Object.Instantiate(visual.previewPrefab, spawn.position, spawn.rotation);
			go.name = "Preview_" + data.name;
			go.SetActive(true);
			// Show only this character's face + hair variants.
			Transform body = FindChildDeep(go.transform, MaleBodyRoot, 3);
			if (body == null)
			{
				body = FindChildDeep(go.transform, FemaleBodyRoot, 3);
			}
			if (body != null)
			{
				Transform faces = body.Find("Faces");
				if (faces != null)
				{
					SetOnlyActiveChild(faces, Wrap(data.face, faces.childCount));
				}
				Transform hairs = body.Find("Hairs");
				if (hairs != null)
				{
					SetOnlyActiveChild(hairs, Wrap(data.hair, hairs.childCount));
				}
			}
			Animator anim = go.GetComponent<Animator>();
			if (anim != null)
			{
				anim.cullingMode = previewCullingMode;
				if (!string.IsNullOrEmpty(visual.animatorResourcePath))
				{
					var ctrl = Resources.Load<RuntimeAnimatorController>(visual.animatorResourcePath);
					if (ctrl != null)
					{
						anim.runtimeAnimatorController = ctrl;
					}
				}
			}
			_previews[slotIndex] = new Preview
			{
				slot = slotIndex,
				go = go,
				visual = visual,
				animator = anim
			};
			PreviewGearBinder.Apply(go, data.school, visual.useFemaleSpawn, data.cls);
		}

		[IteratorStateMachine(typeof(_003CEquipPreviewsWhenItemTableReady_003Ed__56))]
		private IEnumerator EquipPreviewsWhenItemTableReady()
		{
			return null;
		}

		private static void EquipAll(Preview preview)
		{
		}

		private static SItemCustom GetRightHand(SItemCustom[] items)
		{
			return null;
		}

		private void ClearPreviews()
		{
			if (_previews != null)
			{
				for (int i = 0; i < _previews.Length; i++)
				{
					if (_previews[i] != null && _previews[i].go != null)
					{
						UnityEngine.Object.Destroy(_previews[i].go);
					}
				}
			}
			_previews = null;
		}

		private const string MaleBodyRoot = "m_player";

		private const string FemaleBodyRoot = "w_player";

		private static Transform FindChildDeep(Transform root, string name, int maxDepth)
		{
			if (root == null || maxDepth < 0)
			{
				return null;
			}
			if (root.name == name)
			{
				return root;
			}
			for (int i = 0; i < root.childCount; i++)
			{
				Transform hit = FindChildDeep(root.GetChild(i), name, maxDepth - 1);
				if (hit != null)
				{
					return hit;
				}
			}
			return null;
		}

		private static void SetOnlyActiveChild(Transform parent, int index)
		{
			if (parent == null)
			{
				return;
			}
			for (int i = 0; i < parent.childCount; i++)
			{
				parent.GetChild(i).gameObject.SetActive(i == index);
			}
		}

		private static int Wrap(int value, int count)
		{
			if (count <= 0)
			{
				return 0;
			}
			int r = value % count;
			return r < 0 ? (r + count) : r;
		}

		public List<CharacterSelected> GetAllSelectedCharacters()
		{
			return selectedCharacters;
		}

		public void OnCharListClick(int slotIndex, GameCharData2 _, GameObject __)
		{
			SelectSlot(slotIndex);
		}

		public void SelectSlot(int slotIndex)
		{
			if (slotIndex < 0 || slotIndex >= _charList.Count)
			{
				return;
			}
			_selectedSlot = slotIndex;
			if (profile.root != null)
			{
				profile.root.SetActive(true);
			}
			PopulateProfile(_charList[slotIndex]);
			if (slots != null && slotIndex < slots.Length && slots[slotIndex].cameraView != null)
			{
				MoveCameraTo(slots[slotIndex].cameraView);
			}
			CharacterLoginManager mgr = CharacterLoginManager.instance;
			if (mgr != null)
			{
				mgr.selectedCharacterID = _charList[slotIndex].id;
				mgr.selectedCharacterName = _charList[slotIndex].name;
				mgr.selectedClass = _charList[slotIndex].cls;
				mgr.selectedSchool = _charList[slotIndex].school;
				mgr.selectedHair = _charList[slotIndex].hair;
				mgr.selectedFace = _charList[slotIndex].face;
				int vi = ClassVisualIndex(_charList[slotIndex].cls);
				mgr.selectedGender = (vi >= 0 && vi < classVisuals.Length && classVisuals[vi].useFemaleSpawn) ? 1 : 0;
			}
			RefreshButtons();
		}

		private void RefreshButtons()
		{
			bool has = _selectedSlot >= 0 && _selectedSlot < _charList.Count;
			bool many = _charList.Count > 1;
			// Live-client layout: Back / Delete / Start only when a character is
			// selected; Create replaces Start otherwise; Previous/Next only for
			// multi-character accounts with a selection.
			if (buttons.startButton != null)
			{
				buttons.startButton.gameObject.SetActive(has);
				buttons.startButton.interactable = has;
			}
			if (buttons.deleteButton != null)
			{
				buttons.deleteButton.gameObject.SetActive(has);
				buttons.deleteButton.interactable = has;
			}
			if (buttons.backButton != null)
			{
				buttons.backButton.gameObject.SetActive(has);
			}
			if (buttons.createButton != null)
			{
				buttons.createButton.gameObject.SetActive(!has);
			}
			if (buttons.previousCharButton != null)
			{
				buttons.previousCharButton.gameObject.SetActive(has && many);
				buttons.previousCharButton.interactable = has && many;
			}
			if (buttons.nextCharButton != null)
			{
				buttons.nextCharButton.gameObject.SetActive(has && many);
				buttons.nextCharButton.interactable = has && many;
			}
		}

		public void OnBackClicked()
		{
			ClearSelection(true);
		}

		private void ClearSelection(bool moveCamera)
		{
			_selectedSlot = -1;
			if (moveCamera)
			{
				StopCameraMove();
				SnapCameraHome();
			}
			if (profile.root != null)
			{
				profile.root.SetActive(false);
			}
			CharacterLoginManager mgr = CharacterLoginManager.instance;
			if (mgr != null)
			{
				mgr.selectedCharacterID = null;
				mgr.selectedCharacterName = null;
			}
			RefreshButtons();
		}

		public void OnPreviousCharacterClick()
		{
			StepInspected(-1);
		}

		public void OnNextCharacterClick()
		{
			StepInspected(1);
		}

		private void StepInspected(int direction)
		{
			if (_charList.Count == 0)
			{
				return;
			}
			int next = _selectedSlot + direction;
			if (next < 0)
			{
				next = _charList.Count - 1;
			}
			else if (next >= _charList.Count)
			{
				next = 0;
			}
			SelectSlot(next);
		}

		private void PopulateProfile(Preview preview)
		{
		}

		private void PopulateWeapon(Preview preview)
		{
		}

		private static string BuildClubText(GameCharData2 data)
		{
			return null;
		}

		private static void PushSelectionToManagers(Preview preview)
		{
		}

		private void PlaySelectedPose(Preview preview)
		{
		}

		private void SpawnSelectionEffect(Preview preview)
		{
		}

		private static void RebindAnimator(Preview preview)
		{
		}

		private int IdleWithWeaponHash(AnimationType type)
		{
			return 0;
		}

		private int IdleNoWeaponHash()
		{
			return 0;
		}

		private void MoveCameraTo(Transform view)
		{
			if (view == null)
			{
				return;
			}
			if (ResolveCamera() == null)
			{
				return;
			}
			StopCameraMove();
			_cameraMove = StartCoroutine(MoveCameraRoutine(view.position, view.rotation));
		}

		private IEnumerator MoveCameraRoutine(Vector3 targetPos, Quaternion targetRot)
		{
			if (_cameraTransform == null)
			{
				yield break;
			}
			float duration = (cameraSettings != null) ? Mathf.Max(0.05f, cameraSettings.zoomDuration) : 0.6f;
			Vector3 startPos = _cameraTransform.position;
			Quaternion startRot = _cameraTransform.rotation;
			float t = 0f;
			while (t < 1f)
			{
				t += Time.unscaledDeltaTime / duration;
				float k = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(t));
				_cameraTransform.position = Vector3.Lerp(startPos, targetPos, k);
				_cameraTransform.rotation = Quaternion.Slerp(startRot, targetRot, k);
				yield return null;
			}
			_cameraTransform.SetPositionAndRotation(targetPos, targetRot);
			_cameraMove = null;
		}

		[IteratorStateMachine(typeof(_003CMoveCamera_003Ed__79))]
		private IEnumerator MoveCamera(Vector3 targetPos, Quaternion targetRot)
		{
			return null;
		}

		private static void SetSelectedData(GameCharData2 data)
		{
		}

		private static void SetActive(Component component, bool active)
		{
			if (component != null)
			{
				component.gameObject.SetActive(active);
			}
		}

		private static void SetActive(GameObject go, bool active)
		{
			if (go != null)
			{
				go.SetActive(active);
			}
		}

		private GameObject RemainingCountRoot()
		{
			return buttons.createRemainRoot;
		}
	}
}
