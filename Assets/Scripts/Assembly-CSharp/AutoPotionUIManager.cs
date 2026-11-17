using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoPotionUIManager : MonoBehaviour
{
	[Header("Window Reference")]
	[Tooltip("Drag your Auto Potion CANVAS here")]
	public Canvas autoPotCanvas;

	[Header("Global Toggle")]
	public Toggle enableAutoPotToggle;

	[Header("HP Settings")]
	public Slider hpSlider;

	public TextMeshProUGUI hpPercentText;

	[Header("MP Settings")]
	public Slider mpSlider;

	public TextMeshProUGUI mpPercentText;

	[Header("SP Settings")]
	public Slider spSlider;

	public TextMeshProUGUI spPercentText;

	private bool initialAutoPotEnabled;

	private float initialHPThreshold;

	private float initialMPThreshold;

	private float initialSPThreshold;

	private bool cachedHpRestricted;

	private bool hasCachedHpRestricted;

	private void Start()
	{
	}

	private void UpdateText(TextMeshProUGUI textComponent, float value)
	{
	}

	private void OnToggleStateChanged(bool isAutoPotOn)
	{
	}

	private void Update()
	{
	}

	private void ApplyHpRestrictionVisual(bool restricted)
	{
	}

	private void RefreshHpText()
	{
	}

	public void CaptureCurrentValues()
	{
	}

	public void CloseWindowWithoutSaving()
	{
	}

	public void PrepareForOpen()
	{
	}

	private void CaptureInitialValues()
	{
	}

	private void RestoreInitialValues()
	{
	}
}
