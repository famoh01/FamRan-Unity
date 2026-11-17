using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Assets.Scripts;
using Core.Enumerators;
using RAN.Mobile.Enumerators;
using TMPro;
using TrinityRift.Network;
using UnityEngine;
using UnityEngine.UI;

public class UIHUDFunctions : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CBatteryRoutine_003Ed__208 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UIHUDFunctions _003C_003E4__this;

		private WaitForSeconds _003Cwait_003E5__2;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CBatteryRoutine_003Ed__208(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CMoveToServerListRoutine_003Ed__268 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UIHUDFunctions _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CMoveToServerListRoutine_003Ed__268(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CRepeatCoroutine_003Ed__207 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UIHUDFunctions _003C_003E4__this;

		private ServerConnectionManager _003Cscm_003E5__2;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CRepeatCoroutine_003Ed__207(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	public static UIHUDFunctions Instance;

	[Header("Character Info")]
	public Core.Enumerators.CharacterClassEnum charClass;

	[Header("Array's")]
	[SerializeField]
	private Button[] _characterStanceButton;

	[SerializeField]
	private Sprite[] _characterStance;

	public GameObject[] avatar;

	public Sprite[] classAttackBTN;

	public TextMeshProUGUI[] skillTabName;

	public GameObject attackBTN;

	[Header("TextMeshPro")]
	[SerializeField]
	private TextMeshProUGUI hpBarText;

	public TextMeshProUGUI ping;

	public TextMeshProUGUI fps;

	private static readonly WaitForSeconds _pingProbeWait;

	private static readonly WaitForSeconds _pingRepeatWait;

	private int _lastShownPing;

	public TextMeshProUGUI mapInfoText;

	public TextMeshProUGUI mapCoordinates;

	public TextMeshProUGUI dateAndTimeText;

	public TextMeshProUGUI playerLevel;

	public TextMeshProUGUI goldText;

	public TextMeshProUGUI onGoingQuestText;

	public TextMeshProUGUI messageFeedback;

	public TextMeshProUGUI leftHandQuantity;

	public TextMeshProUGUI mapChannel;

	public TextMeshProUGUI serverNameText;

	[Tooltip("Optional build/version label. Leave empty and nothing is written to it.")]
	public TextMeshProUGUI buildVersionText;

	public TextMeshProUGUI totalPlayerOnline;

	[Header("Battery Indicator")]
	[Tooltip("Root object of the battery widget (the outline/body image). Left empty, nothing is hidden — assign it if you want the whole widget to disappear on platforms with no battery (PC/editor).")]
	public GameObject batteryRoot;

	[Tooltip("The image the battery sprite is swapped on. Its Image Type is forced to Simple at runtime so a leftover Filled setup can't clip the artwork.")]
	public Image batteryFill;

	[Tooltip("Optional \"87%\" label. Leave empty for an icon-only widget.")]
	public TextMeshProUGUI batteryText;

	[Tooltip("Optional extra object shown only while the device is charging. Usually not needed — the charging sprite below already replaces the whole icon.")]
	public GameObject batteryChargingIcon;

	[Tooltip("Sprite for a full battery (above 80%).")]
	public Sprite battery100Sprite;

	[Tooltip("Sprite for 61–80%.")]
	public Sprite battery80Sprite;

	[Tooltip("Sprite for 41–60%.")]
	public Sprite battery60Sprite;

	[Tooltip("Sprite for 21–40%.")]
	public Sprite battery40Sprite;

	[Tooltip("Sprite for 0–20%.")]
	public Sprite battery20Sprite;

	[Tooltip("Shown instead of the level sprites while the device is charging. Leave empty to keep showing the level sprite while charging.")]
	public Sprite batteryChargingSprite;

	[Tooltip("Seconds between battery reads. The OS only reports whole percents and updates slowly, so there is nothing to gain from polling faster.")]
	public float batteryPollInterval;

	[Range(0f, 1f)]
	[Tooltip("EDITOR ONLY. The Unity Editor (and most desktop builds) report no battery at all, so the widget would hide itself and you could never see it while working. In the editor this value is shown instead. Change it in Play mode to preview the low/normal colours. It has no effect in a device build — there the real battery is always used.")]
	public float batterySimulatedLevelInEditor;

	[Tooltip("EDITOR ONLY. Preview the charging colour + charging icon with the simulated level above.")]
	public bool batterySimulateChargingInEditor;

	private int _lastBatteryPercent;

	private BatteryStatus _lastBatteryStatus;

	[Header("InputField")]
	public TMP_InputField chatInput;

	[Header("Chat Tab Visuals")]
	public UIMultipleTabsGO chatTabsManager;

	public int whisperTabIndex;

	private string currentMapName;

	private string currentMapLev;

	private bool isAvatarSet;

	private float fpsUpdateInterval;

	private float accum;

	private int frames;

	private float timeleft;

	private Transform _cachedInvDataOwner;

	private InventoryData _cachedInvData;

	private int _lastMinute;

	private int _lastDay;

	private long _lastMoneyShown;

	private string _lastSceneNameScanned;

	private string _lastDisplayMapName;

	private int _lastCoordX;

	private int _lastCoordZ;

	private int _lastChannelShown;

	private RAN.Mobile.Enumerators.ServerEnum _lastServerShown;

	private bool _hasLastServerShown;

	private int _lastLeftHandAmount;

	private ItemTypeEnum _lastLeftHandType;

	private bool _hasLastLeftHandType;

	[Header("Boolean's")]
	[SerializeField]
	private bool _spriteActivatorWalk;

	[SerializeField]
	private bool _spriteActivatorAttack;

	public bool characterRendererActivator;

	[Header("GameObject's")]
	public GameObject lootIcon;

	public GameObject dialogBox;

	public GameObject questParent;

	public GameObject questComParent;

	public GameObject[] skillHotkey;

	public QuestDetails questDetails;

	public QuestDetails questDetailView;

	public GameObject miniPetHUD;

	public GameObject autoPotsButton;

	[Header("Canvas")]
	public Canvas mainView;

	public Canvas skillBtnsView;

	public Canvas overlayPanels;

	public Canvas targetInfo;

	public Canvas NPC;

	public Canvas inventory;

	public Canvas equipment;

	public Canvas upgradeItem;

	public Canvas trade;

	public Canvas adminPanel;

	public Canvas vend;

	public Canvas vendClient;

	public Canvas startVend;

	public Canvas vendInfo;

	public Canvas lockerPanel;

	public Canvas itemBankPanel;

	public Canvas clubPanel;

	public Canvas petCanvas;

	public Canvas autoPotsCanvas;

	public Canvas optionsCanvas;

	public Canvas targetingModeCanvas;

	[Header("Loot Mode Selection")]
	[Tooltip("Reusable Classic / Dual Loot selector opened from Game Options and on first run.")]
	public ModeSelectionPanelController lootModeSelectionController;

	[Header("Targeting Mode Selection")]
	[Tooltip("Reusable Auto Snap / Manual selector opened from Game Options and on first run.")]
	public ModeSelectionPanelController targetingModeSelectionController;

	private bool _botChallengeHudHidden;

	private bool _mainViewEnabledBeforeBotChallenge;

	private bool _skillBtnsViewEnabledBeforeBotChallenge;

	public TMP_Text targetingModeDescription;

	public TMP_Text targetingModeLabel;

	public Slider manualDragSensitivitySlider;

	public TMP_Text manualDragSensitivityText;

	public Toggle tapToCastToggle;

	public Canvas languageCanvas;

	public Canvas merchantCanvas;

	public Canvas capRewardCanvas;

	public Canvas miniPartyCanvas;

	public Canvas partyCanvas;

	public Canvas changeSchoolCanvas;

	public Canvas changeNameCanvas;

	public Canvas certifyCanvas;

	public Canvas killFeedCanvas;

	public Canvas tyrannyMainWindow;

	public Canvas scoringWindow;

	public Canvas rankingWindow;

	public Canvas hairStyleChangerCanvas;

	public Canvas hairColorChangerCanvas;

	public Canvas faceChangerCanvas;

	[Header("Button's")]
	public Button showChatBox;

	public Button openChatButton;

	public Button whisperButton;

	public Button grindButton;

	public Button grindExitButton;

	public Button openMountButton;

	[Tooltip("Object toggled on/off with the ride button. Leave empty to toggle the button's own GameObject; set it when the button lives inside a holder/background that must hide too.")]
	public GameObject openMountButtonRoot;

	[Header("Screenshot")]
	public GameObject restoreUIButton;

	[Header("Transform's")]
	public Transform playerCoordinate;

	[Header("SwitchToCamShowcase")]
	public GameObject playerCam;

	public GameObject showcaseCam;

	private GameObject instantiateCam;

	private bool onShowcase;

	[Header("Chat")]
	public GameObject chatBox;

	public GameObject chatSection;

	[Header("NPC UI Reference")]
	public int npcTabIndex;

	public Button[] npcTabs;

	[Header("Attacks UI Reference")]
	public RectTransform cancelArea;

	[Header("NPC UI Reference")]
	public GameObject touchPad;

	public Button talkButton;

	[Header("Chats UI Reference")]
	public GameObject chatGeneralView;

	public GameObject miniChatGeneralView;

	[Header("Other UI Reference")]
	public GameObject buffsUISection;

	[SerializeField]
	[Tooltip("Optional edit-mode-only image/text object under BuffContents, beside ICONS. Keep ICONS empty in the prefab for runtime buff icons.")]
	private GameObject buffEditPreview;

	private GameObject[] _buffEditPreviewIcons;

	public AutoPotionUIManager autoPotsUIManager;

	public TutorialManager tutorialManager;

	public ItemBoxUI itemBoxUI;

	public Canvas macroPanel;

	[Header("QBox")]
	public GameObject qboxLock;

	public GameObject destinyBoxRays;

	public GameObject powerUp;

	[Tooltip("Big screen splash for the MAX-tier Power Up (QUESTION_ATTACK_UP_M). Leave empty to fall back to the normal Power Up splash until dedicated art is wired up here.")]
	public GameObject powerUpMax;

	public GameObject expGet;

	public GameObject expTime;

	public GameObject crazyTime;

	[Tooltip("Big screen splash for Madness Time — the MAX tier of Crazy Time (QUESTION_MADNESS). Leave empty to fall back to the normal Crazy Time splash until dedicated art is wired up here.")]
	public GameObject madnessTime;

	public GameObject lucky;

	public GameObject speedUp;

	[Tooltip("Big screen splash for the MAX-tier Speed Up (QUESTION_SPEED_UP_M). Leave empty to fall back to the normal Speed Up splash until dedicated art is wired up here.")]
	public GameObject speedUpMax;

	public GameObject ohNo;

	public GameObject bomb;

	[Header("Skill Button")]
	public Button SkillBtn1;

	public Button SkillBtn2;

	public Button SkillBtn3;

	public Button SkillBtn4;

	public Button SkillBtn5;

	public Button SkillBtn6;

	public Button SkillBtn7;

	public Button SkillBtn8;

	public Button SkillBtn9;

	public Button SkillBtn10;

	public Button BasicAttack;

	public Button AttackIdle;

	[Header("Language Settings UI")]
	public Toggle[] languageToggles;

	public GameObject languageRestartNote;

	private readonly Dictionary<string, string> _mapNameDisplayOverrides;

	private static readonly Color ColorGoodPing;

	private static readonly Color ColorModeratePing;

	private static readonly Color ColorBadPing;

	[CompilerGenerated]
	private Action m_OnPickUpItemButtonClick;

	public const float MapCoordCellSize = 25f;

	private bool _exitDialogOpen;

	private Action _exitConfirm;

	private Action _exitCancel;

	private bool _serverListDialogOpen;

	private bool _movingToServerList;

	private Action _serverListConfirm;

	private Action _serverListCancel;

	[Header("Zoom-Blocking Panels")]
	[Tooltip("Panels that, while open, suppress world-camera zoom (mirrors the minimap guard in Zoom). Assign only true windows here — leave transient popups like messageFeedback OUT so they don't block zoom. Works whether a panel toggles via its Canvas.enabled or GameObject active.")]
	public GameObject[] zoomBlockingPanels;

	private const string TargetingAutoSnapDesc = "When you drag attack/skill, it automatically locks onto the nearest valid target and moves you into range. Fast and simple.";

	private const string TargetingManualDesc = "Drag a selector circle on the ground and choose exactly who to hit inside it — including targets on stairs/ledges. More control in a crowd.";

	private const float ManualDragSensitivityDefault = 0.4f;

	public const string TapToCastPref = "TapToCastSkill";

	public bool IsVendWindowOpen => false;

	public static bool IsTapToCastEnabled => false;

	public event Action OnPickUpItemButtonClick
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

	public Behaviour[] GetAllCompanionPanels()
	{
		return null;
	}

	private void Start()
	{
	}

	private void ShowBuildVersion()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Awake()
	{
	}

	private void InitializeBuffEditPreview()
	{
	}

	public void SetBuffEditPreviewVisible(bool visible)
	{
	}

	private static string GetHierarchyPath(Transform t)
	{
		return null;
	}

	public void DestoryNpcButtons()
	{
	}

	private void LateUpdate()
	{
	}

	public void SetBotChallengeHudHidden(bool hidden)
	{
	}

	private void Update()
	{
	}

	private void RefreshMountButton()
	{
	}

	private static bool IsBoardEquipped()
	{
		return false;
	}

	private void HandleShowcaseToggle()
	{
	}

	public void CheckLeftHand()
	{
	}

	private void ShowLeftHandQuantity(ItemTypeEnum type, string label, int amount)
	{
	}

	private void HideLeftHandQuantity()
	{
	}

	private void CalculateFPS()
	{
	}

	[IteratorStateMachine(typeof(_003CRepeatCoroutine_003Ed__207))]
	private IEnumerator RepeatCoroutine()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CBatteryRoutine_003Ed__208))]
	private IEnumerator BatteryRoutine()
	{
		return null;
	}

	public void RefreshBattery()
	{
	}

	private Sprite SpriteForPercent(int percent)
	{
		return null;
	}

	private static Sprite FirstAssigned(params Sprite[] candidates)
	{
		return null;
	}

	private void OnApplicationPause(bool paused)
	{
	}

	public void OpenHairStyleChangerWindow()
	{
	}

	public void CloseHairStyleChangerWindow()
	{
	}

	public void ToggleHairStyleChangerWindow()
	{
	}

	public void OpenHairColorChangerWindow()
	{
	}

	public void CloseHairColorChangerWindow()
	{
	}

	public void ToggleHairColorChangerWindow()
	{
	}

	public void MessageSend()
	{
	}

	public void ScreenInfo()
	{
	}

	public void MapInfo()
	{
	}

	public static int WorldToMapCell(float world)
	{
		return 0;
	}

	public static float MapCellToWorld(int cell)
	{
		return 0f;
	}

	private void UpdateCoordinates()
	{
	}

	public void CharacterAvatar()
	{
	}

	public void OnCharacterSelect()
	{
	}

	public void OnLootTrigger()
	{
	}

	public void OnOpenMount()
	{
	}

	public void OnWalkRun()
	{
	}

	public void OnAttackIdle()
	{
	}

	public void OnCharacterRenderer()
	{
	}

	public void OnExitButton()
	{
	}

	private void ConfirmExit()
	{
	}

	private void CancelExit()
	{
	}

	public void StartMessage()
	{
	}

	public void OnEnterChat()
	{
	}

	public void OpenTradeWindow()
	{
	}

	public void CloseTradeWindow()
	{
	}

	public void OnEndVendButtonClicked()
	{
	}

	public void ApplyVendWindowPanels()
	{
	}

	public void ResetPanelsAfterVend()
	{
	}

	public void OpenClubWindow()
	{
	}

	public void OpenPetWindow()
	{
	}

	public void ClosePetWindow()
	{
	}

	public void TogglePetWindow()
	{
	}

	public void CloseClubWindow()
	{
	}

	public void ToggleClubWindow()
	{
	}

	public void OpenItemBankWindow()
	{
	}

	public void CloseItemBankWindow()
	{
	}

	public void ToggleItemBankWindow()
	{
	}

	public void OpenFaceChangerWindow()
	{
	}

	public void CloseFaceChangerWindow()
	{
	}

	public void ToggleFaceChangerWindow()
	{
	}

	private void OpenChatBox()
	{
	}

	public void OnWhisper()
	{
	}

	public void ServerList()
	{
	}

	private void ConfirmServerList()
	{
	}

	[IteratorStateMachine(typeof(_003CMoveToServerListRoutine_003Ed__268))]
	private IEnumerator MoveToServerListRoutine()
	{
		return null;
	}

	private void CancelServerList()
	{
	}

	public void MoveToServerList()
	{
	}

	private void DestroyObjectsWithTag()
	{
	}

	public void HideItemQuestion()
	{
	}

	public void ResetQBox()
	{
	}

	public bool IsAnyOverlayPanelOpen()
	{
		return false;
	}

	public void EnterScreenshotMode()
	{
	}

	public void ExitScreenshotMode()
	{
	}

	public void EnableSkillButtons(bool enable)
	{
	}

	public void OnValidateIfPlayerCanLockQBox()
	{
	}

	public void OnInventoryCloseButtonClicked()
	{
	}

	public void OpenPrivateStoreSetup()
	{
	}

	public bool TryCancelAndCloseUpgrade()
	{
		return false;
	}

	private void CloseHighlightedPanels()
	{
	}

	public void CloseLanguageWindow()
	{
	}

	public void ApplyLanguageSelection()
	{
	}

	public void OpenTargetingModePanel()
	{
	}

	public void OpenLootModePanel()
	{
	}

	private void RefreshTapToCast()
	{
	}

	private void OnTapToCastChanged(bool isOn)
	{
	}

	private void RefreshManualDragSensitivity()
	{
	}

	private void OnManualDragSensitivityChanged(float value)
	{
	}

	public void CloseTargetingModePanel()
	{
	}

	public void SelectAutoSnapMode()
	{
	}

	public void SelectManualMode()
	{
	}

	private void SetTargetingMode(bool manual)
	{
	}

	private void HandleTargetingModeChanged(TargetingSelectionMode _)
	{
	}

	private void RefreshTargetingModeTexts()
	{
	}
}
