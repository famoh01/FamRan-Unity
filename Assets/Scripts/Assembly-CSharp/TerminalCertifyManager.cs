using Core.Enumerators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TerminalCertifyManager : MonoBehaviour
{
	public static TerminalCertifyManager Instance;

	[Header("Root Canvas")]
	public Canvas clubWarCanvas;

	[Header("Certify UI")]
	public GameObject certifyGroup;

	public Image progressBar;

	[Header("Timer UI")]
	public GameObject timerGroup;

	public TextMeshProUGUI timeText;

	private const float CERTIFY_DURATION = 60f;

	private float currentProgress;

	private bool uiInitialized;

	private bool isCertifying;

	private float localRemainSeconds;

	private bool isTimerRunning;

	private int lastDisplayedSecond;

	public bool IsCertifying => false;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	private void UpdateTimerUI()
	{
	}

	public void CancelCertifyingLocally()
	{
	}

	private void HandleCertifyResult(ClubWarCertifyResultEnum result)
	{
	}

	private void StartCertifying()
	{
	}

	private void StopCertifying()
	{
	}

	private void Update()
	{
	}

	private void HandleClubWarRemainUpdated(uint seconds)
	{
	}

	private void InitializeUi()
	{
	}

	private void CheckCanvasState()
	{
	}

	private void SubscribeToServer()
	{
	}

	private void UnsubscribeFromServer()
	{
	}

	private void ResolveCanvasReference()
	{
	}
}
