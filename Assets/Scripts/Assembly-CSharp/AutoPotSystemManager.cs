using UnityEngine;
using UnityEngine.UI;

public class AutoPotSystemManager : MonoBehaviour
{
	public static AutoPotSystemManager Instance;

	[Header("UI References")]
	[Tooltip("Drag the actual HUD button GameObject here")]
	public Button openButton;

	[Tooltip("Drag your main Auto Potion CANVAS here")]
	public Canvas autoPotCanvas;

	[Header("Sprite Configuration")]
	[Tooltip("The Image component of the HUD button that will change sprites")]
	public Image buttonImage;

	public Sprite enabledSprite;

	public Sprite disabledSprite;

	public bool IsEnabled { get; set; }

	public float HPThreshold { get; set; }

	public float MPThreshold { get; set; }

	public float SPThreshold { get; set; }

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void LoadPrefs()
	{
	}

	public void SavePrefs()
	{
	}

	private void ToggleWindow()
	{
	}

	public void UpdateVisual(bool isEnabled)
	{
	}

	private bool CheckPlayerSubscription()
	{
		return false;
	}
}
