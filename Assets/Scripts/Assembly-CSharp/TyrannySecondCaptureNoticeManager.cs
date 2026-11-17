using RAN.Mobile.Enumerators;
using TrinityRift.Network;
using UnityEngine;
using UnityEngine.UI;

public sealed class TyrannySecondCaptureNoticeManager : MonoBehaviour
{
	private const int TowerCount = 3;

	private const int SecondCaptureThreshold = 2;

	private const string LogPrefix = "[TyrannySecondCapture] ";

	[Header("UI")]
	[SerializeField]
	private Canvas noticeCanvas;

	[SerializeField]
	private Image noticeImage;

	[Header("Sprites")]
	[Tooltip("Shown to the capturing school itself.")]
	[SerializeField]
	private Sprite twoDownSprite;

	[Tooltip("Shown to everyone else when Sacred Gate captures its 2nd tower.")]
	[SerializeField]
	private Sprite sacredGateFinalCaptureSprite;

	[Tooltip("Shown to everyone else when Mystic Peak captures its 2nd tower.")]
	[SerializeField]
	private Sprite mysticPeakFinalCaptureSprite;

	[Tooltip("Shown to everyone else when Phoenix captures its 2nd tower.")]
	[SerializeField]
	private Sprite phoenixFinalCaptureSprite;

	[Header("Timing")]
	[Tooltip("Seconds the notice stays up before auto-dismissing (plan §2.8: auto-dismiss after ~5s).")]
	[SerializeField]
	private float displaySeconds;

	[Header("Debug")]
	[Tooltip("Log each danger notice shown/hidden. Leave OFF in production (zero cost when off).")]
	[SerializeField]
	private bool enableLogging;

	private ServerConnectionManager subscribedServerManager;

	private Sprite cachedSprite;

	private float hideTimer;

	private bool isShowing;

	public static TyrannySecondCaptureNoticeManager Instance { get; private set; }

	private static SchoolEnum LocalSchool => SchoolEnum.SacredGate;

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

	private void ShowNotice(SchoolEnum capturingSchool)
	{
	}

	private Sprite GetFinalCaptureSprite(SchoolEnum capturingSchool)
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
