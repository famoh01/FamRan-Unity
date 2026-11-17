using RAN.Mobile.Enumerators;
using TrinityRift.Network;
using UnityEngine;
using UnityEngine.UI;

public sealed class TyrannyCaptureNoticeManager : MonoBehaviour
{
	private const int TowerCount = 3;

	private const string LogPrefix = "[TyrannyCaptureNotice] ";

	[Header("UI")]
	[SerializeField]
	private Canvas noticeCanvas;

	[SerializeField]
	private Image schoolLogo;

	[SerializeField]
	private Image capturedBySprite;

	[Header("School Logos (SG / MP / PNX)")]
	[SerializeField]
	private Sprite sacredGateLogo;

	[SerializeField]
	private Sprite mysticPeakLogo;

	[SerializeField]
	private Sprite phoenixLogo;

	[Header("Captured By Sprites (SG / MP / PNX)")]
	[SerializeField]
	private Sprite capturedBySacredGate;

	[SerializeField]
	private Sprite capturedByMysticPeak;

	[SerializeField]
	private Sprite capturedByPhoenix;

	[Header("Timing")]
	[Tooltip("Seconds the notice stays up before auto-dismissing (plan §2.8: auto-dismiss after ~5s).")]
	[SerializeField]
	private float displaySeconds;

	[Header("Debug")]
	[Tooltip("Log each capture notice shown/hidden. Leave OFF in production (zero cost when off).")]
	[SerializeField]
	private bool enableLogging;

	private ServerConnectionManager subscribedServerManager;

	private Sprite cachedSchoolLogo;

	private Sprite cachedCapturedBySprite;

	private float hideTimer;

	private bool isShowing;

	public static TyrannyCaptureNoticeManager Instance { get; private set; }

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	private void HandleTowerCaptured(ushort school, ushort towerIndex, ushort towerScore)
	{
	}

	private void ShowNotice(SchoolEnum school)
	{
	}

	private Sprite GetSchoolLogo(SchoolEnum school)
	{
		return null;
	}

	private Sprite GetCapturedBySprite(SchoolEnum school)
	{
		return null;
	}

	private static void SetSprite(Image image, ref Sprite cache, Sprite sprite)
	{
	}

	private void HideNotice()
	{
	}

	private void Log(string message)
	{
	}
}
