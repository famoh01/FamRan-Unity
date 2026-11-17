using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetMiniHudHandler : MonoBehaviour
{
	[Header("UI References")]
	public GameObject petDisplayPanel;

	public Image petIconImage;

	public Image fullnessFillBar;

	public Slider petHpSlider;

	[Header("Text References")]
	public TextMeshProUGUI fullnessText;

	public TextMeshProUGUI hpText;

	[Header("Low Stat Visuals")]
	[Tooltip("Threshold percentage to trigger the red visuals (e.g., 20)")]
	[Range(0f, 100f)]
	public int lowStatThreshold;

	[Header("Debug Testing")]
	[Tooltip("Check this in the Inspector to force pet health to 20%")]
	public bool forceLowHealthTest;

	[Space(5f)]
	public Color normalFullnessColor;

	public Color lowFullnessColor;

	[Space(5f)]
	[Tooltip("Drag the 'Fill' image from your Slider hierarchy here")]
	public Image petHpFillImage;

	public Sprite normalHpSprite;

	public Sprite lowHpSprite;

	private uint _trackedPetId;

	private float _lastLoggedFullness;

	private void Update()
	{
	}

	private void UpdatePetIcon()
	{
	}
}
