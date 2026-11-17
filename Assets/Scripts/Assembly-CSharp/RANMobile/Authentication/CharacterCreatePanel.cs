using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RANMobile.Authentication
{
	public class CharacterCreatePanel : MonoBehaviour
	{
		private enum Gender
		{
			None = 0,
			Male = 1,
			Female = 2
		}

		private const string MaleHairsPath = "m_player/Hairs";

		private const string MaleFacesPath = "m_player/Faces";

		private const string MaleSchoolPath = "m_player/Preparatory";

		private const string FemaleHairsPath = "w_player/Hairs";

		private const string FemaleFacesPath = "w_player/Faces";

		private const string FemaleSchoolPath = "w_player/Preparatory";

		private static readonly Color[] HairColors;

		[Header("Character Selection UI to Hide")]
		[Tooltip("Drag the Create Button, Server Select Button, and Remaining Text here!")]
		public GameObject[] selectionElementsToHide;

		[Header("UI Panels (New 2-Step Flow)")]
		public GameObject selectionPanel;

		public Canvas customizationCanvas;

		public GameObject zoomButtonsPanel;

		public GameObject topRightBtnsPanel;

		[Header("Final Creation UI")]
		[Tooltip("Drag your new inline Name Input Field here")]
		public TMP_InputField characterNameInput;

		[Header("Description Panel (Parent & Sub-Panels)")]
		public GameObject descriptionPanel;

		public GameObject schoolDescPanel;

		public GameObject genderDescPanel;

		public GameObject classDescPanel;

		[Header("Description Panel Lore - School")]
		public Image descSchoolIcon;

		public TMP_Text descSchoolName;

		public TMP_Text descSchoolText;

		public Sprite[] schoolIcons;

		public string[] schoolNames;

		[TextArea(3, 10)]
		public string[] schoolDescriptions;

		[Header("Description Panel Lore - Gender")]
		public Image descGenderIcon;

		public TMP_Text descGenderName;

		public Sprite[] genderIcons;

		public string[] genderNames;

		[Header("Description Panel Lore - Class")]
		public Image descClassIcon;

		public TMP_Text descClassName;

		public TMP_Text descClassText;

		public Sprite[] classIcons;

		public string[] classNames;

		[TextArea(3, 10)]
		public string[] classDescriptions;

		[Header("Eye Button Movement")]
		public RectTransform eyeButtonRect;

		[Header("State Tracking")]
		private int currentSchoolID;

		private int currentClassID;

		private Gender currentGender;

		private bool isGenderSelected;

		private bool isMaleSelected;

		private bool isSchoolSelected;

		[Header("Game Object Elements")]
		public Transform CharCreationSpawn;

		public GameObject playerMalePrefab;

		public GameObject playerFemalePrefab;

		private GameObject playerMale;

		private GameObject playerFemale;

		[Header("UI Buttons")]
		public Button[] charClassBtn;

		public Button[] charSchoolBtn;

		public Button[] charHairBtn;

		public Button[] charFaceBtn;

		public Button[] charHairColorBtn;

		public Button[] charClassBtnBG;

		[Header("Gender Selection Buttons")]
		public Button maleGenderBtn;

		public Button femaleGenderBtn;

		[Header("Sprites & Images")]
		public Sprite[] charSpriteMaleLogoHair;

		public Sprite[] charSpriteFemaleLogoHair;

		public Sprite[] charSpriteMaleLogoFace;

		public Sprite[] charSpriteFemaleLogoFace;

		[Header("Dummy / Locked Slots")]
		public Sprite lockedSprite;

		public Image[] charHairRenderer;

		public Image[] charFaceRenderer;

		private Renderer hairRenderer;

		[Header("Hair Color Slider")]
		public Slider hairHueSlider;

		public Image hairSliderHandle;

		public GameObject hairColorLabel;

		public GameObject hairTypeLabel;

		private static readonly Color DisabledGreyTint;

		private void Start()
		{
		}

		public void ToggleHairTabUI(bool isHairTabActive)
		{
		}

		public void OnHairHueSliderChanged(float sliderValue)
		{
		}

		public void OnNextToCustomizationClick()
		{
		}

		public void OnBackToSelectionClick()
		{
		}

		public void OnFinishCustomizationClick()
		{
		}

		public void OnSacredGateClick()
		{
		}

		public void OnMysticPeakClick()
		{
		}

		public void OnPhoenixClick()
		{
		}

		private void SelectSchool(int schoolIndex)
		{
		}

		public void OnMaleButtonClick()
		{
		}

		public void OnFemaleButtonClick()
		{
		}

		private void ApplyGender(Gender gender)
		{
		}

		private void UpdateGenderButtonVisuals()
		{
		}

		private void SetAllClassButtonsInteractable(bool interactable)
		{
		}

		private void SetClassButtonInteractable(int index, bool interactable)
		{
		}

		private void HighlightSelectedClassBanner(int selectedIndex)
		{
		}

		private void UpdateClassButtonsForGender()
		{
		}

		private void UpdateClassDescription(int index)
		{
		}

		private void ClearClassDescription()
		{
		}

		public void OnBrawlerClick()
		{
		}

		public void OnSwordsmanClick()
		{
		}

		public void OnArcherClick()
		{
		}

		public void OnShamanClick()
		{
		}

		private bool CanSelectClass(int buttonIndex)
		{
			return false;
		}

		private int GetButtonIndexFromClassID(int classID)
		{
			return 0;
		}

		private void DisableAllChildren(Transform parent)
		{
		}

		private void SetActiveChild(Transform parent, int index)
		{
		}

		public void OnHairChanged(int index)
		{
		}

		public void OnFaceChanged(int index)
		{
		}

		public void OnSchoolChanged(int index)
		{
		}

		public void OnHairChangedFemale(int index)
		{
		}

		public void OnFaceChangedFemale(int index)
		{
		}

		public void OnSchoolChangedFemale(int index)
		{
		}

		private void OnHairClick(int index)
		{
		}

		private void OnFaceClick(int index)
		{
		}

		public void OnHairColorButtonClick(int colorIndex)
		{
		}

		private void ChangeAllHairColors(Color color)
		{
		}

		private void HighlightSelectedSchoolBanner(int selectedIndex)
		{
		}

		private void ApplyClassDefaults(int defaultHairType, int defaultColorIndex)
		{
		}
	}
}
