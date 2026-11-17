using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkillTabManager : MonoBehaviour
{
	[Header("Direct Tab Navigation")]
	[Tooltip("Assign your 3 top buttons here. Order matters: Index 0 = Tab 1, Index 1 = Tab 2, etc.")]
	public Button[] directTabButtons;

	[Header("Legacy UI Components")]
	public Button skillTabButton;

	public TextMeshProUGUI skillTabOuterText;

	public TextMeshProUGUI skillTabText;

	[Header("Default Visuals")]
	public Sprite defaultEmptySlotSprite;

	[Header("Tab Highlight Colors")]
	[Tooltip("Tint applied to the active skill-set button — matches the Skills List tab (UIMultipleTabsGO).")]
	public Color selectedTabColor;

	[Tooltip("Default state for inactive skill-set buttons. Pure white = no tint.")]
	public Color unselectedTabColor;

	public static SkillTabManager Instance;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void RefreshSkillHotkeyDisplay()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnMapLoaded(Scene scene, LoadSceneMode mode)
	{
	}

	private void Start()
	{
	}

	private void SwitchToTab(int tabIndex)
	{
	}

	private void SkillTab()
	{
	}

	private void UpdateTabText()
	{
	}

	private void UpdateTabHighlight()
	{
	}

	private void UpdateSKillTab()
	{
	}

	private void DeleteSkillHotKey()
	{
	}
}
