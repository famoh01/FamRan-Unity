using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace RANMobile.Authentication
{
	[DisallowMultipleComponent]
	public class CharacterCreateUI : MonoBehaviour
	{
		public enum Step
		{
			SchoolGenderClass = 0,
			Customization = 1
		}

		[Serializable]
		public class ScreenCanvases
		{
			[Tooltip("CharacterSelectionCanvas — the sibling canvas this screen swaps with.")]
			public Canvas selectionCanvas;

			[Tooltip("CharacterCreationCanvas — the canvas this script drives.")]
			public Canvas creationCanvas;
		}

		[Serializable]
		public class CreationPanels
		{
			[Tooltip("SchoolGenderClassSelection — step 1.")]
			public GameObject schoolGenderClassPanel;

			[Tooltip("CharacterCustomization — step 2.")]
			public GameObject customizationPanel;

			[Tooltip("ZoomButtons — the parent group, revealed once a gender is picked.")]
			public GameObject zoomButtons;

			[Tooltip("ZoomButtons/FaceZoom. Subscribed in code to CharCreationCamZoom.ZoomOnFace().")]
			public Button faceZoomButton;

			[Tooltip("ZoomButtons/DefaultZoom. Subscribed in code to CharCreationCamZoom.DefaultZoom().")]
			public Button defaultZoomButton;

			[Tooltip("Assign the SAME field to CharacterCreateManager so its RanFilter check reads the same text.")]
			public TMP_InputField nameInput;
		}

		[Serializable]
		public class CreationButtons
		{
			[Tooltip("Step 1 → step 2. Interactable only once school + gender + class are chosen.")]
			public Button nextButton;

			[Tooltip("Step 2 → step 1. Hidden on step 1.")]
			public Button backButton;

			[Tooltip("Leaves creation and re-enables the selection canvas. Hidden on step 2.")]
			public Button backToSelectButton;

			[Tooltip("Commits the character. Hidden on step 1; disabled until the name is filled in.")]
			public Button createCharacterButton;
		}

		[Serializable]
		public class DescriptionPanels
		{
			public GameObject root;

			public GameObject schoolPanel;

			public GameObject genderPanel;

			public GameObject classPanel;

			[Header("School")]
			public Image schoolIcon;

			public TMP_Text schoolName;

			public TMP_Text schoolText;

			[Header("Gender")]
			public Image genderIcon;

			public TMP_Text genderName;

			[Header("Class")]
			public Image classIcon;

			public TMP_Text className;

			public TMP_Text classText;
		}

		[Serializable]
		public class SchoolEntry
		{
			[Tooltip("Needs a Button component on the tile — the prefab tiles are Images only. Set its Transition to None.")]
			public Button button;

			[Tooltip("Tile graphic that gets dimmed while unpicked. Auto-resolved from the button if empty.")]
			public Image tileImage;

			[Tooltip("Optional 'Border' child, shown only on the current pick.")]
			public GameObject selectedBorder;

			public Sprite icon;

			public string displayName;

			[TextArea(3, 8)]
			public string description;
		}

		[Serializable]
		public class GenderEntry
		{
			public Button button;

			[Tooltip("Tile graphic that gets dimmed while unpicked. Auto-resolved from the button if empty.")]
			public Image tileImage;

			[Tooltip("Optional 'Border' child, shown only on the current pick.")]
			public GameObject selectedBorder;

			public Sprite descriptionIcon;

			public string displayName;

			[Header("Model")]
			[Tooltip("Hairs / Faces / Preparatory are located automatically under m_player or w_player — no paths to fill in.")]
			public GameObject prefab;

			[Header("Optional display names — overrides the stepper's label mode")]
			[Tooltip("One per hair style, in the same order as the Hairs children. Leave empty to fall back to the stepper's label mode.")]
			public string[] hairNames;

			[Tooltip("One per face, in the same order as the Faces children. Leave empty to fall back to the stepper's label mode.")]
			public string[] faceNames;
		}

		[Serializable]
		public class ClassEntry
		{
			public Button button;

			[Tooltip("Tile graphic that gets dimmed while unpicked. Auto-resolved from the button if empty.")]
			public Image tileImage;

			[Tooltip("Optional 'Border' child, shown only on the current pick.")]
			public GameObject selectedBorder;

			public Sprite icon;

			public string displayName;

			[TextArea(3, 8)]
			public string description;

			[Header("Rules")]
			[Tooltip("Written to CreationData.SetCharacterClass — 1 Brawler, 2 Swordsman, 4 Archer, 8 Shaman.")]
			public int classId;

			[Tooltip("Index into the Genders list this class belongs to (0 = first entry, 1 = second).")]
			public int genderIndex;

			public int defaultHairType;

			[Range(0f, 8f)]
			public int defaultHairColorIndex;
		}

		public enum StepperLabelMode
		{
			TypeLetter = 0,
			Numeric = 1,
			ObjectName = 2
		}

		[Serializable]
		public class Stepper
		{
			public Button prevButton;

			public Button nextButton;

			[Tooltip("The header's Text (TMP).")]
			public TMP_Text valueLabel;

			public StepperLabelMode labelMode;
		}

		[Serializable]
		public class ColorStepper
		{
			public Button prevButton;

			public Button nextButton;

			[Tooltip("The header Image, tinted to the selected hair colour.")]
			public Image colorSwatch;
		}

		private sealed class Model
		{
			public GameObject go;

			public Transform hairs;

			public Transform faces;

			public Transform schools;

			public CharacterBaseBody body;
		}

		private const string MaleBodyRoot = "m_player";

		private const string FemaleBodyRoot = "w_player";

		private const string HairsContainer = "Hairs";

		private const string FacesContainer = "Faces";

		private const string SchoolContainer = "Preparatory";

		private static readonly Color[] HairColors = new Color[9]
		{
			new Color(0.10f, 0.10f, 0.10f),
			new Color(0.23f, 0.14f, 0.08f),
			new Color(0.42f, 0.27f, 0.14f),
			new Color(0.65f, 0.49f, 0.32f),
			new Color(0.85f, 0.70f, 0.50f),
			new Color(0.94f, 0.82f, 0.50f),
			new Color(0.75f, 0.25f, 0.13f),
			new Color(0.91f, 0.91f, 0.91f),
			new Color(0.56f, 0.56f, 0.56f)
		};

		[Header("Screens")]
		public ScreenCanvases screens;

		[Header("Panels & Buttons")]
		public CreationPanels panels;

		public CreationButtons buttons;

		public DescriptionPanels description;

		[Header("Content")]
		public SchoolEntry[] schools;

		public GenderEntry[] genders;

		public ClassEntry[] classes;

		[Header("Tile tint — nothing is ever hidden, only dimmed")]
		[Tooltip("The tile that IS the current pick.")]
		public Color pickedTint;

		[Tooltip("Every tile that is not the current pick. Darkened so it reads as 'not chosen'. Drop the alpha instead if you want true transparency.")]
		public Color unpickedTint;

		[Header("Customization steppers")]
		public Stepper faceStepper;

		public Stepper hairStepper;

		public ColorStepper hairColorStepper;

		[Header("Scene")]
		[Tooltip("The creation stage world root (e.g. CharCreationContainer) — the object that HOLDS CharCreationSpawn. Switched on with the creation screen and off again on the way back.")]
		public GameObject creationStageRoot;

		[Tooltip("Empty transform the character models are parented to (the old 'CharCreationSpawn').")]
		public Transform charCreationSpawn;

		[Tooltip("Optional. Left empty, resolved through CharCreationCamZoom.instance.")]
		public CharCreationCamZoom camZoom;

		private Model[] _models;

		private Step _step;

		private int _school = -1;

		private int _gender = -1;

		private int _class = -1;

		private int _face;

		private int _hair;

		private int _hairColor;

		private const int HairColorCount = 9;

		private GraphicRaycaster _selectionRaycaster;

		private GraphicRaycaster _creationRaycaster;

		private MaterialPropertyBlock _tintBlock;

		private static readonly int TintColorId;

		private readonly List<Renderer> _rendererScratch = new List<Renderer>();

		private const string TypeLabelPrefix = "Type ";

		public static CharacterCreateUI Active { get; private set; }

		private void Awake()
		{
			Active = this;
			if (screens != null)
			{
				if (screens.selectionCanvas != null)
				{
					_selectionRaycaster = screens.selectionCanvas.GetComponent<GraphicRaycaster>();
				}
				if (screens.creationCanvas != null)
				{
					_creationRaycaster = screens.creationCanvas.GetComponent<GraphicRaycaster>();
				}
			}
			ResolveTileImages();
		}

		private void OnDestroy()
		{
			if (Active == this)
			{
				Active = null;
			}
		}

		private void OnEnable()
		{
			// Domain reloads wipe runtime listeners — re-wire every time.
			Active = this;
			ResolveTileImages();
			WireListeners();
		}

		private void Start()
		{
		}

		private void ValidateWiring()
		{
		}

		private void ResolveTileImages()
		{
			if (schools != null)
			{
				foreach (SchoolEntry s in schools)
				{
					if (s != null && s.tileImage == null && s.button != null)
					{
						s.tileImage = s.button.GetComponent<Image>();
					}
				}
			}
			if (genders != null)
			{
				foreach (GenderEntry g in genders)
				{
					if (g != null && g.tileImage == null && g.button != null)
					{
						g.tileImage = g.button.GetComponent<Image>();
					}
				}
			}
			if (classes != null)
			{
				foreach (ClassEntry c in classes)
				{
					if (c != null && c.tileImage == null && c.button != null)
					{
						c.tileImage = c.button.GetComponent<Image>();
					}
				}
			}
		}

		private void WireListeners()
		{
			if (schools != null)
			{
				for (int i = 0; i < schools.Length; i++)
				{
					SchoolEntry entry = schools[i];
					int captured = i;
					if (entry != null)
					{
						AddClick(entry.button, delegate { OnSchoolClick(captured); });
					}
				}
			}
			if (genders != null)
			{
				for (int i = 0; i < genders.Length; i++)
				{
					GenderEntry entry = genders[i];
					int captured = i;
					if (entry != null)
					{
						AddClick(entry.button, delegate { OnGenderClick(captured); });
					}
				}
			}
			if (classes != null)
			{
				for (int i = 0; i < classes.Length; i++)
				{
					ClassEntry entry = classes[i];
					int captured = i;
					if (entry != null)
					{
						AddClick(entry.button, delegate { OnClassClick(captured); });
					}
				}
			}
			if (buttons != null)
			{
				AddClick(buttons.nextButton, OnNextToCustomizationClick);
				AddClick(buttons.backButton, OnBackClick);
				AddClick(buttons.backToSelectButton, OnBackToSelectionClick);
				AddClick(buttons.createCharacterButton, OnFinishCustomizationClick);
			}
			if (panels != null)
			{
				AddClick(panels.faceZoomButton, OnFaceZoomClick);
				AddClick(panels.defaultZoomButton, OnDefaultZoomClick);
				if (panels.nameInput != null)
				{
					panels.nameInput.onValueChanged.RemoveListener(OnNameChanged);
					panels.nameInput.onValueChanged.AddListener(OnNameChanged);
				}
			}
			if (faceStepper != null)
			{
				AddClick(faceStepper.prevButton, delegate { StepFace(-1); });
				AddClick(faceStepper.nextButton, delegate { StepFace(1); });
			}
			if (hairStepper != null)
			{
				AddClick(hairStepper.prevButton, delegate { StepHair(-1); });
				AddClick(hairStepper.nextButton, delegate { StepHair(1); });
			}
			if (hairColorStepper != null)
			{
				AddClick(hairColorStepper.prevButton, delegate { StepHairColor(-1); });
				AddClick(hairColorStepper.nextButton, delegate { StepHairColor(1); });
			}
		}

		private void OnNameChanged(string _)
		{
			RefreshButtons();
		}

		private static void AddClick(Button button, UnityAction action)
		{
			if (button == null || action == null)
			{
				return;
			}
			// RemoveAllListeners, not RemoveListener: method-group conversions
			// create a fresh delegate each WireListeners pass, so RemoveListener
			// would never drop the previous one and one click would double-fire.
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(action);
		}

		public void OnFaceZoomClick()
		{
			CharCreationCamZoom zoom = CamZoom();
			if (zoom != null)
			{
				zoom.ZoomOnFace();
			}
		}

		public void OnDefaultZoomClick()
		{
			CharCreationCamZoom zoom = CamZoom();
			if (zoom != null)
			{
				zoom.DefaultZoom();
			}
		}

		public void OpenCreation()
		{
			Active = this;
			ResetCreationState();
			SetCanvas(screens != null ? screens.selectionCanvas : null, _selectionRaycaster, false);
			SetCanvas(screens != null ? screens.creationCanvas : null, _creationRaycaster, true);
			SetActive(creationStageRoot, true);
			GoToStep(Step.SchoolGenderClass);
			Debug.Log("[CharacterCreateUI] creation opened.");
		}

		public void OnBackToSelectionClick()
		{
			ShowSelectionScreen();
		}

		private void ShowSelectionScreen()
		{
			SetActive(creationStageRoot, false);
			HideAllModels();
			SetCanvas(screens != null ? screens.creationCanvas : null, _creationRaycaster, false);
			SetCanvas(screens != null ? screens.selectionCanvas : null, _selectionRaycaster, true);
			Debug.Log("[CharacterCreateUI] back to selection.");
		}

		private static void SetCanvas(Canvas canvas, GraphicRaycaster raycaster, bool on)
		{
			if (canvas != null)
			{
				canvas.enabled = on;
				canvas.gameObject.SetActive(on);
			}
			if (raycaster != null)
			{
				raycaster.enabled = on;
			}
		}

		public void OnNextToCustomizationClick()
		{
			if (!IsSelectionComplete())
			{
				return;
			}
			GoToStep(Step.Customization);
		}

		public void OnBackClick()
		{
			GoToStep(Step.SchoolGenderClass);
		}

		private void GoToStep(Step step)
		{
			_step = step;
			bool customization = step == Step.Customization;
			SetActive(panels != null ? panels.schoolGenderClassPanel : null, !customization);
			SetActive(panels != null ? panels.customizationPanel : null, customization);
			RefreshButtons();
		}

		private void RefreshButtons()
		{
			if (buttons == null)
			{
				return;
			}
			bool step1 = _step == Step.SchoolGenderClass;
			if (buttons.nextButton != null)
			{
				buttons.nextButton.gameObject.SetActive(step1);
				buttons.nextButton.interactable = IsSelectionComplete();
			}
			if (buttons.backToSelectButton != null)
			{
				buttons.backToSelectButton.gameObject.SetActive(step1);
			}
			if (buttons.backButton != null)
			{
				buttons.backButton.gameObject.SetActive(!step1);
			}
			if (buttons.createCharacterButton != null)
			{
				buttons.createCharacterButton.gameObject.SetActive(!step1);
				string name = panels != null && panels.nameInput != null ? panels.nameInput.text.Trim() : string.Empty;
				buttons.createCharacterButton.interactable = name.Length > 0;
			}
		}

		private bool IsSelectionComplete()
		{
			return _school >= 0 && _gender >= 0 && _class >= 0;
		}

		private void ResetCreationState()
		{
			_school = -1;
			_gender = -1;
			_class = -1;
			_face = 0;
			_hair = 0;
			_hairColor = 0;
			if (panels != null && panels.nameInput != null)
			{
				panels.nameInput.text = string.Empty;
			}
			HideAllModels();
			RefreshSchoolBorders();
			RefreshGenderTiles();
			RefreshClassTiles();
			if (panels != null)
			{
				SetActive(panels.zoomButtons, false);
			}
			if (description != null)
			{
				SetActive(description.root, false);
			}
		}

		public void OnFinishCustomizationClick()
		{
			if (!IsSelectionComplete())
			{
				Debug.LogWarning("[CharacterCreateUI] cannot create: pick school, gender and class first.");
				return;
			}
			string charName = panels != null && panels.nameInput != null ? panels.nameInput.text.Trim() : string.Empty;
			if (!IsValidName(charName))
			{
				Debug.LogWarning("[CharacterCreateUI] invalid name: 3-16 chars, starts with a letter, letters/digits/space/_/- only.");
				return;
			}
			string token = Authentication.SessionToken;
			if (string.IsNullOrEmpty(token))
			{
				Debug.LogWarning("[CharacterCreateUI] no session token — cannot create.");
				return;
			}
			ClassEntry entry = classes[_class];
			if (buttons != null && buttons.createCharacterButton != null)
			{
				buttons.createCharacterButton.interactable = false;
			}
			Debug.Log("[CharacterCreateUI] creating '" + charName + "' class=" + entry.classId + " school=" + _school + " hair=" + _hair + " hairColor=" + _hairColor + " face=" + _face);
			// Route through Authentication's own request path (the exact one the
			// working login POST uses) — a bare UnityWebRequest POST from this
			// component failed with 'Cannot connect to destination host'.
			GameObject authGo = new GameObject("TempCreateAuth");
			Authentication auth = authGo.AddComponent<Authentication>();
			auth.OnCreateCharacterFailed.AddListener(delegate(string msg)
			{
				Debug.LogWarning("[CharacterCreateUI] create failed: " + msg);
				if (buttons != null && buttons.createCharacterButton != null)
				{
					buttons.createCharacterButton.interactable = true;
				}
				UnityEngine.Object.Destroy(authGo);
			});
			auth.CreateCharacterUser(charName, entry.classId, _school, _hair, _hairColor, _face);
		}

		private static bool IsValidName(string name)
		{
			if (string.IsNullOrEmpty(name) || name.Length < 3 || name.Length > 16)
			{
				return false;
			}
			if (!char.IsLetter(name[0]))
			{
				return false;
			}
			for (int i = 0; i < name.Length; i++)
			{
				char c = name[i];
				bool ok = char.IsLetterOrDigit(c) || c == ' ' || c == '_' || c == '-';
				if (!ok)
				{
					return false;
				}
			}
			return true;
		}

		private static string EscapeJson(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			return s.Replace("\\", "\\\\").Replace("\"", "\\\"");
		}

		public void OnSchoolClick(int index)
		{
			if (index < 0 || schools == null || index >= schools.Length)
			{
				return;
			}
			_school = index;
			RefreshSchoolBorders();
			ApplySchoolToModel();
			SchoolEntry entry = schools[index];
			if (description != null)
			{
				SetActive(description.root, true);
				SetActive(description.schoolPanel, true);
				SetActive(description.genderPanel, false);
				SetActive(description.classPanel, false);
				if (description.schoolIcon != null && entry.icon != null)
				{
					description.schoolIcon.sprite = entry.icon;
				}
				if (description.schoolName != null)
				{
					description.schoolName.text = entry.displayName;
				}
				if (description.schoolText != null)
				{
					description.schoolText.text = entry.description;
				}
			}
			RefreshButtons();
		}

		private void RefreshSchoolBorders()
		{
			if (schools == null)
			{
				return;
			}
			for (int i = 0; i < schools.Length; i++)
			{
				SchoolEntry entry = schools[i];
				if (entry == null)
				{
					continue;
				}
				if (entry.selectedBorder != null)
				{
					entry.selectedBorder.SetActive(i == _school);
				}
				Tint(entry.tileImage, i == _school);
			}
		}

		private void Tint(Image image, bool picked)
		{
			if (image != null)
			{
				image.color = picked ? pickedTint : unpickedTint;
			}
		}

		private void ApplySchoolToModel()
		{
			if (_gender < 0 || genders == null || _gender >= genders.Length)
			{
				return;
			}
			Model model = GetOrSpawnModel(_gender);
			if (model == null || model.go == null)
			{
				return;
			}
			int classId = 0;
			if (_class >= 0 && classes != null && _class < classes.Length && classes[_class] != null)
			{
				classId = classes[_class].classId;
			}
			PreviewGearBinder.Apply(model.go, _school < 0 ? 0 : _school, _gender == 1, classId);
		}

		public void OnGenderClick(int index)
		{
			if (index < 0 || genders == null || index >= genders.Length)
			{
				return;
			}
			_gender = index;
			// Class picks are gender-locked — drop a pick that no longer matches.
			if (_class >= 0 && classes != null && classes[_class] != null && classes[_class].genderIndex != index)
			{
				_class = -1;
			}
			RefreshGenderTiles();
			RefreshClassTiles();
			GenderEntry entry = genders[index];
			if (description != null)
			{
				SetActive(description.root, true);
				SetActive(description.schoolPanel, false);
				SetActive(description.genderPanel, true);
				SetActive(description.classPanel, false);
				if (description.genderIcon != null && entry.descriptionIcon != null)
				{
					description.genderIcon.sprite = entry.descriptionIcon;
				}
				if (description.genderName != null)
				{
					description.genderName.text = entry.displayName;
				}
			}
			if (panels != null)
			{
				SetActive(panels.zoomButtons, true);
			}
			Model model = GetOrSpawnModel(index);
			ShowOnlyModel(index);
			ApplySchoolToModel();
			if (model != null)
			{
				if (model.hairs != null)
				{
					SetOnlyChildActive(model.hairs, Wrap(_hair, CountOf(model.hairs)));
				}
				if (model.faces != null)
				{
					SetOnlyChildActive(model.faces, Wrap(_face, CountOf(model.faces)));
				}
			}
			RefreshAllSteppers();
			RefreshButtons();
		}

		private void RefreshGenderTiles()
		{
			if (genders == null)
			{
				return;
			}
			for (int i = 0; i < genders.Length; i++)
			{
				GenderEntry entry = genders[i];
				if (entry == null)
				{
					continue;
				}
				if (entry.selectedBorder != null)
				{
					entry.selectedBorder.SetActive(i == _gender);
				}
				Tint(entry.tileImage, i == _gender);
			}
		}

		public void OnClassClick(int index)
		{
			if (index < 0 || classes == null || index >= classes.Length)
			{
				return;
			}
			ClassEntry entry = classes[index];
			if (_gender < 0 || entry == null || entry.genderIndex != _gender)
			{
				// Class belongs to the other gender — ignore the click.
				return;
			}
			_class = index;
			ApplyClassDefaults(entry);
			ApplySchoolToModel();
			RefreshClassTiles();
			if (description != null)
			{
				SetActive(description.root, true);
				SetActive(description.schoolPanel, false);
				SetActive(description.genderPanel, false);
				SetActive(description.classPanel, true);
				if (description.classIcon != null && entry.icon != null)
				{
					description.classIcon.sprite = entry.icon;
				}
				if (description.className != null)
				{
					description.className.text = entry.displayName;
				}
				if (description.classText != null)
				{
					description.classText.text = entry.description;
				}
			}
			RefreshAllSteppers();
			RefreshButtons();
		}

		private void RefreshClassTiles()
		{
			if (classes == null)
			{
				return;
			}
			for (int i = 0; i < classes.Length; i++)
			{
				ClassEntry entry = classes[i];
				if (entry == null)
				{
					continue;
				}
				if (entry.selectedBorder != null)
				{
					entry.selectedBorder.SetActive(i == _class);
				}
				// Only dim tiles that are not the pick; classes of the other gender
				// always read as unpicked.
				bool picked = i == _class;
				Tint(entry.tileImage, picked);
			}
		}

		private void ApplyClassDefaults(ClassEntry entry)
		{
			_hair = entry.defaultHairType;
			_hairColor = entry.defaultHairColorIndex;
			Model model = CurrentModel();
			if (model != null)
			{
				if (model.hairs != null)
				{
					int count = CountOf(model.hairs);
					if (count > 0)
					{
						_hair = Wrap(_hair, count);
						SetOnlyChildActive(model.hairs, _hair);
					}
				}
				if (model.faces != null)
				{
					int count2 = CountOf(model.faces);
					if (count2 > 0)
					{
						_face = Wrap(_face, count2);
						SetOnlyChildActive(model.faces, _face);
					}
				}
			}
			RefreshColorStepper();
		}

		public void StepFace(int delta)
		{
			Model model = CurrentModel();
			int count = (model != null && model.faces != null) ? CountOf(model.faces) : 0;
			if (count == 0)
			{
				return;
			}
			_face = Wrap(_face + delta, count);
			SetOnlyChildActive(model.faces, _face);
			GenderEntry gender = CurrentGenderEntry();
			RefreshStepper(faceStepper, _face, count, model.faces, gender != null ? gender.faceNames : null);
		}

		public void StepHair(int delta)
		{
			Model model = CurrentModel();
			int count = (model != null && model.hairs != null) ? CountOf(model.hairs) : 0;
			if (count == 0)
			{
				return;
			}
			_hair = Wrap(_hair + delta, count);
			SetOnlyChildActive(model.hairs, _hair);
			GenderEntry gender = CurrentGenderEntry();
			RefreshStepper(hairStepper, _hair, count, model.hairs, gender != null ? gender.hairNames : null);
		}

		public void StepHairColor(int delta)
		{
			_hairColor = Wrap(_hairColor + delta, HairColorCount);
			ApplyHairColor(_hairColor);
			RefreshColorStepper();
		}

		private void RefreshAllSteppers()
		{
			Model model = CurrentModel();
			GenderEntry gender = CurrentGenderEntry();
			int faceCount = (model != null && model.faces != null) ? CountOf(model.faces) : 0;
			int hairCount = (model != null && model.hairs != null) ? CountOf(model.hairs) : 0;
			if (faceCount > 0)
			{
				RefreshStepper(faceStepper, _face, faceCount, model.faces, gender != null ? gender.faceNames : null);
			}
			if (hairCount > 0)
			{
				RefreshStepper(hairStepper, _hair, hairCount, model.hairs, gender != null ? gender.hairNames : null);
			}
			RefreshColorStepper();
		}

		private GenderEntry CurrentGenderEntry()
		{
			if (genders == null || _gender < 0 || _gender >= genders.Length)
			{
				return null;
			}
			return genders[_gender];
		}

		private static void RefreshStepper(Stepper stepper, int index, int count, Transform container, string[] customNames)
		{
			if (stepper == null || stepper.valueLabel == null)
			{
				return;
			}
			stepper.valueLabel.text = ResolveOptionName(stepper, index, container, customNames);
		}

		private static string ResolveOptionName(Stepper stepper, int index, Transform container, string[] customNames)
		{
			if (customNames != null && index >= 0 && index < customNames.Length && !string.IsNullOrEmpty(customNames[index]))
			{
				return customNames[index];
			}
			switch (stepper.labelMode)
			{
			case StepperLabelMode.Numeric:
				return (index + 1).ToString();
			case StepperLabelMode.ObjectName:
				if (container != null && index >= 0 && index < container.childCount)
				{
					return container.GetChild(index).name;
				}
				return (index + 1).ToString();
			default:
				return TypeLabelPrefix + (index + 1);
			}
		}

		private void RefreshColorStepper()
		{
			if (hairColorStepper != null && hairColorStepper.colorSwatch != null)
			{
				hairColorStepper.colorSwatch.color = HairColors[Wrap(_hairColor, HairColorCount)];
			}
			ApplyHairColor(_hairColor);
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

		private static int CountOf(Transform container)
		{
			return (container != null) ? container.childCount : 0;
		}

		private Model CurrentModel()
		{
			if (_models == null || _gender < 0 || _gender >= _models.Length)
			{
				return null;
			}
			return _models[_gender];
		}

		private void ApplyHairColor(int colorIndex)
		{
			// Renderer tinting needs the model's shader support — the colour the
			// player picks is stored and sent to the server; the swatch shows it.
		}

		private void TintActiveHairViaPropertyBlock(Model model, Color color)
		{
		}

		private Model GetOrSpawnModel(int genderIndex)
		{
			if (genders == null || genderIndex < 0 || genderIndex >= genders.Length)
			{
				return null;
			}
			if (_models == null)
			{
				_models = new Model[genders.Length];
			}
			if (_models[genderIndex] == null)
			{
				GameObject prefab = genders[genderIndex].prefab;
				if (prefab == null || charCreationSpawn == null)
				{
					return null;
				}
				GameObject go = UnityEngine.Object.Instantiate(prefab, charCreationSpawn);
				go.SetActive(false);
				Transform body = ResolveBodyRoot(go.transform);
				Model model = new Model
				{
					go = go,
					hairs = FindContainer(body, HairsContainer),
					faces = FindContainer(body, FacesContainer),
					schools = FindContainer(body, SchoolContainer)
				};
				_models[genderIndex] = model;
			}
			return _models[genderIndex];
		}

		private static Transform ResolveBodyRoot(Transform root)
		{
			Transform found = FindDeep(root, MaleBodyRoot, 0);
			if (found == null)
			{
				found = FindDeep(root, FemaleBodyRoot, 0);
			}
			return found != null ? found : root;
		}

		private static Transform FindDeep(Transform root, string name, int depth)
		{
			if (root == null || depth > 4)
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

		private static Transform FindContainer(Transform body, string childName)
		{
			if (body == null)
			{
				return null;
			}
			Transform direct = body.Find(childName);
			if (direct != null)
			{
				return direct;
			}
			return FindDeep(body, childName, 0);
		}

		private void HideAllModels()
		{
			if (_models == null)
			{
				return;
			}
			for (int i = 0; i < _models.Length; i++)
			{
				if (_models[i] != null && _models[i].go != null)
				{
					_models[i].go.SetActive(false);
				}
			}
		}

		private void ShowOnlyModel(int genderIndex)
		{
			HideAllModels();
			if (_models != null && genderIndex >= 0 && genderIndex < _models.Length && _models[genderIndex] != null && _models[genderIndex].go != null)
			{
				_models[genderIndex].go.SetActive(true);
			}
		}

		private static void SetOnlyChildActive(Transform parent, int index)
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

		private CharCreationCamZoom CamZoom()
		{
			if (camZoom != null)
			{
				return camZoom;
			}
			return CharCreationCamZoom.instance;
		}

		private static void SetActive(GameObject go, bool active)
		{
			if (go != null)
			{
				go.SetActive(active);
			}
		}

		private static void SetActive(Component component, bool active)
		{
			if (component != null)
			{
				component.gameObject.SetActive(active);
			}
		}
	}
}
