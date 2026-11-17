using UnityEngine;
using UnityEngine.UI;

public sealed class TyrannyPhaseNoticeManager : MonoBehaviour
{
	private const string LogPrefix = "[TyrannyPhaseNotice] ";

	[Header("UI")]
	[SerializeField]
	private Canvas noticeCanvas;

	[SerializeField]
	private Image noticeImage;

	[Header("Sprites")]
	[SerializeField]
	private Sprite tyrannyBeginSprite;

	[SerializeField]
	private Sprite tyrannyEndSprite;

	[Header("Timing")]
	[Tooltip("Seconds the notice stays up before auto-dismissing.")]
	[SerializeField]
	private float displaySeconds;

	[Header("Debug")]
	[Tooltip("Log each phase notice shown/hidden. Leave OFF in production (zero cost when off).")]
	[SerializeField]
	private bool enableLogging;

	private TyrannyManager subscribedTyrannyManager;

	private Sprite cachedSprite;

	private float hideTimer;

	private bool isShowing;

	private TyrannyManager.TyrannyPhase knownPhase;

	private TyrannyManager.TyrannyPhase announcedPhase;

	private bool wasOnTyrannyMap;

	public static TyrannyPhaseNoticeManager Instance { get; private set; }

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

	private void HandlePhaseChanged(TyrannyManager.TyrannyPhase newPhase)
	{
	}

	private void EvaluateAndAnnounce(TyrannyManager.TyrannyPhase newPhase)
	{
	}

	private static bool IsOnTyrannyMap()
	{
		return false;
	}

	private void ShowNotice(Sprite sprite, string label)
	{
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
