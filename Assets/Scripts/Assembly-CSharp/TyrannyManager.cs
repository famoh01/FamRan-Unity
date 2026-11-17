using System;
using System.Runtime.CompilerServices;
using Models.Library.Shared.Game.Tyranny;
using RAN.Mobile.Enumerators;
using TMPro;
using TrinityRift.Network;
using UnityEngine;
using UnityEngine.UI;

public sealed class TyrannyManager : MonoBehaviour
{
	public enum TyrannyPhase
	{
		Ended = 0,
		Register = 1,
		Battle = 2,
		Reward = 3
	}

	[Serializable]
	public struct TyrannySchedule
	{
		public int battleStartHour;

		public int battleStartMinute;

		public int battleEndHour;

		public int battleEndMinute;

		public int prevStartHour;

		public int prevStartMinute;

		public int prevEndHour;

		public int prevEndMinute;
	}

	[CompilerGenerated]
	private Action<TyrannyPhase> m_PhaseChanged;

	private const int DefaultChannel = 0;

	private const int WarnUnderSeconds = 300;

	private const int TyrannyTowerCount = 3;

	private const string LogPrefix = "[Tyranny] ";

	[Header("Rules / Info Text")]
	[SerializeField]
	private TextMeshProUGUI goalText;

	[SerializeField]
	private TextMeshProUGUI playerLimitText;

	[SerializeField]
	private TextMeshProUGUI levelReqText;

	[Header("Window")]
	[Tooltip("Closes the Tyranny main window (disables UIHUDFunctions.tyrannyMainWindow).")]
	[SerializeField]
	private Button exitButton;

	[Header("Main HUD Entry Button")]
	[Tooltip("Main HUD button that opens the Tyranny window (plan §2.1 CCompetitionNotifyButton). Stays visible but non-interactable until the server reports an upcoming battle schedule (plan §1.2/§2.3.2 — nextSchedule.battleStartHour/Minute come back as 00:00 when there is no schedule yet).")]
	[SerializeField]
	private Button tyrannyWarButton;

	[Tooltip("CanvasGroup driving the button's blink alpha. Leave empty to auto-resolve from tyrannyWarButton's GameObject (adds one if missing).")]
	[SerializeField]
	private CanvasGroup tyrannyWarButtonBlinkGroup;

	[Tooltip("Alpha at the bottom of the blink pulse (1 = no blink).")]
	[SerializeField]
	private float blinkMinAlpha;

	[Tooltip("Blink speed in full pulses per second.")]
	[SerializeField]
	private float blinkSpeedHz;

	[Header("Battle Status Panel")]
	[SerializeField]
	private TextMeshProUGUI channelText;

	[SerializeField]
	private TextMeshProUGUI startTimeText;

	[SerializeField]
	private TextMeshProUGUI endTimeText;

	[Tooltip("Label shown above the registration timer, e.g. \"Registration\" / \"Battle Progress\" / \"Reward Progress\".")]
	[SerializeField]
	private TextMeshProUGUI registrationLabelText;

	[SerializeField]
	private TextMeshProUGUI registrationTimerText;

	[SerializeField]
	private TextMeshProUGUI registrationStatusText;

	[SerializeField]
	private TextMeshProUGUI queueNumberText;

	[SerializeField]
	private Button registerButton;

	[SerializeField]
	private TextMeshProUGUI registerButtonLabel;

	[Header("Previous Record Panel")]
	[SerializeField]
	private TextMeshProUGUI durationText;

	[SerializeField]
	private TextMeshProUGUI winnerSchoolText;

	[SerializeField]
	private TextMeshProUGUI topPlayerText;

	[Tooltip("Single image that displays the winning school's sprite, swapped dynamically from the 3 sprites below.")]
	[SerializeField]
	private Image winnerSchoolIcon;

	[Tooltip("Sprite shown in winnerSchoolIcon when the winner was Sacred Gate (index 0).")]
	[SerializeField]
	private Sprite sacredGateWinnerSprite;

	[Tooltip("Sprite shown in winnerSchoolIcon when the winner was Mystic Peak (index 1).")]
	[SerializeField]
	private Sprite mysticPeakWinnerSprite;

	[Tooltip("Sprite shown in winnerSchoolIcon when the winner was Phoenix (index 2).")]
	[SerializeField]
	private Sprite phoenixWinnerSprite;

	[Tooltip("Beside the No.1 player: opens the full ranking window for the previous war. Auto-disabled until there's finalized ranking data to show.")]
	[SerializeField]
	private Button previousRankingButton;

	[Header("Debug")]
	[Tooltip("Log registration/state changes. Leave OFF in production (zero cost when off).")]
	[SerializeField]
	private bool enableLogging;

	private TyrannyPhase phase;

	private float remainingSeconds;

	private int levelRequirement;

	private int playerLimitPerSchool;

	private int currentChannel;

	private bool registered;

	private bool queued;

	private int queuePosition;

	private bool isInBattleMap;

	private bool hasUpcomingSchedule;

	private readonly TyrannyCountdown countdown;

	private readonly TyrannyHudCanvas mainWindow;

	private ServerConnectionManager subscribedManager;

	private string cachedRegistrationStatusText;

	private string cachedRegisterButtonLabel;

	private string cachedQueueNumberText;

	private string cachedChannelText;

	private string cachedStartTimeText;

	private string cachedEndTimeText;

	private string cachedDurationText;

	private string cachedWinnerSchoolText;

	private string cachedTopPlayerText;

	private string cachedPlayerLimitText;

	private string cachedLevelReqText;

	private string cachedRegistrationLabelText;

	private int cachedPlayerLimitValue;

	private int cachedLevelReqValue;

	private SchoolEnum? cachedWinnerIconSchool;

	public static TyrannyManager Instance { get; private set; }

	public TyrannyPhase CurrentPhase => TyrannyPhase.Ended;

	public bool IsRegistered => false;

	public bool IsQueued => false;

	public int QueuePosition => 0;

	public float RemainingSeconds => 0f;

	public bool IsInBattleMap => false;

	public bool IsMainWindowOpen => false;

	public bool HasUpcomingSchedule => false;

	private static ushort LocalCharLevel => 0;

	public event Action<TyrannyPhase> PhaseChanged
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

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

	private void SyncInBattleMapFromLand()
	{
	}

	public void OpenMainWindow()
	{
	}

	public void CloseMainWindow()
	{
	}

	public void ToggleMainWindow()
	{
	}

	private void SetMainWindowVisible(bool visible)
	{
	}

	public void ApplyFullStateSnapshot(TyrannyPhase newPhase, float newRemainingSeconds, TyrannySchedule newSchedule, int newLevelRequirement, int newPlayerLimitPerSchool, bool newRegistered, bool newQueued, int newQueuePosition)
	{
	}

	public void ApplyStateChanged(TyrannyPhase newPhase, float newRemainingSeconds)
	{
	}

	public void SetInBattleMap(bool inBattleMap)
	{
	}

	public void ApplySchedule(TyrannySchedule newSchedule)
	{
	}

	public void ApplyRegistrationResult(bool newRegistered, bool newQueued, int newQueuePosition)
	{
	}

	public void ApplyQueuePositionUpdate(int newQueuePosition)
	{
	}

	public void ApplyRejoinResult(bool success)
	{
	}

	public void ApplyPreviousRecordSummary(SchoolEnum winnerSchool, string topPlayerName)
	{
	}

	private void OnPreviousRankingButtonClicked()
	{
	}

	private void RefreshPreviousRankingButton()
	{
	}

	public void SetCurrentChannel(int channel)
	{
	}

	public void SetPlayerLimit(int limitPerSchool)
	{
	}

	public void SetLevelRequirement(int level)
	{
	}

	private void OnRegisterButtonClicked()
	{
	}

	private void RequestRegister()
	{
	}

	private void RequestCancelRegistration()
	{
	}

	private void RequestJoinBattle()
	{
	}

	private void HandleTyrannyStateChanged(TyrannyState newState, float remainSeconds)
	{
	}

	private void HandleTyrannyBattleInfo()
	{
	}

	private void HandleTyrannyNextSched()
	{
	}

	private void HandleTyrannyRegisterFb(TyrannyRegisterFb fb, ushort queueNum)
	{
	}

	private void HandleTyrannyQueueMoved()
	{
	}

	private void HandleTyrannyQueueUpdate(ushort queueNum)
	{
	}

	private void HandleTyrannyRejoinFb(TyrannyRejoinFb fb)
	{
	}

	private void HandleTyrannyRewardInfo()
	{
	}

	private static TyrannyPlayerData FindTopRankedReward(ServerConnectionManager scm)
	{
		return null;
	}

	private void HandleTyrannyRankingUpdated()
	{
	}

	private void HandleTyrannyRankingFinalized()
	{
	}

	private void RefreshPreviousRecordFromRankingBuffer()
	{
	}

	private static TyrannyPlayerData FindTopRankedFromRankingBuffer(ServerConnectionManager scm)
	{
		return null;
	}

	private static TyrannyPhase ToPhase(TyrannyState state)
	{
		return TyrannyPhase.Ended;
	}

	private static TyrannySchedule BuildSchedule(TyrannyScheduleNext next)
	{
		return default;
	}

	private void RefreshRegistrationStatus()
	{
	}

	private void GetRegistrationStatus(out string text, out Color color)
	{
		text = null;
		color = default;
	}

	private void RefreshProgressLabel()
	{
	}

	private void RefreshWinnerIcon(SchoolEnum winnerSchool)
	{
	}

	private void RefreshQueueNumber()
	{
	}

	private void RefreshRegisterButton()
	{
	}

	private void RefreshTyrannyWarButton()
	{
	}

	private void UpdateWarButtonBlink()
	{
	}

	private void RefreshCountdownDisplay(bool force)
	{
	}

	private static string FormatHourMinute(int hour, int minute)
	{
		return null;
	}

	private void Log(string message)
	{
	}

	private void PlayerFeedback(string message, Color textColor)
	{
	}
}
