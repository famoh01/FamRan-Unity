using RAN.Mobile.Enumerators;
using TMPro;
using TrinityRift.Network;
using UnityEngine;

public sealed class TyrannyWinnerNoticeManager : MonoBehaviour
{
	private const string LogPrefix = "[TyrannyWinnerNotice] ";

	[Header("UI")]
	[SerializeField]
	private Canvas noticeCanvas;

	[SerializeField]
	private TextMeshProUGUI noticeText;

	[Header("Timing")]
	[Tooltip("Seconds the notice stays up before auto-dismissing (plan §2.8: auto-dismiss after ~5s).")]
	[SerializeField]
	private float displaySeconds;

	[Header("Debug")]
	[Tooltip("Log each winner notice shown/hidden. Leave OFF in production (zero cost when off).")]
	[SerializeField]
	private bool enableLogging;

	private ServerConnectionManager subscribedServerManager;

	private string cachedText;

	private float hideTimer;

	private bool isShowing;

	public static TyrannyWinnerNoticeManager Instance { get; private set; }

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

	private void HandleTyrannyWinner(ushort winnerSchool, bool fullCapture)
	{
	}

	private void ShowNotice(SchoolEnum winnerSchool)
	{
	}

	private void HideNotice()
	{
	}

	private void Log(string message)
	{
	}
}
