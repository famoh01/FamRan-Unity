using UnityEngine;
using UnityEngine.UI;

public class BusterEventUI : MonoBehaviour
{
	public static BusterEventUI Instance;

	[Header("UI References")]
	public GameObject rootPanel;

	[Tooltip("Set Image Type to Filled, Fill Method to Horizontal, Fill Origin to Left")]
	public Image playTimeFillBar;

	[Tooltip("Set Image Type to Filled, Fill Method to Horizontal, Fill Origin to Right")]
	public Image busterFillBar;

	public Image statusImage;

	[Header("Popups")]
	public GameObject busterStartPopup;

	private bool wasBusterActive;

	[Header("Settings")]
	private float dynamicMaxPlayTime;

	private float dynamicMaxBusterTime;

	private float localTimer;

	private float currentMaxTimer;

	private bool isBusterActive;

	private bool wasInLayoutEditMode;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void CheckLoginState()
	{
	}

	public void SyncWithServer()
	{
	}

	private void Update()
	{
	}

	private void UpdateVisuals()
	{
	}
}
