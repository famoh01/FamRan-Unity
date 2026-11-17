using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameOption : MonoBehaviour
{
	public static GameOption instance;

	[CompilerGenerated]
	private static Action<float> m_OnSensitivityChangedEvent;

	[Header("Audio Settings")]
	public Slider SFXSlider;

	public Slider ENVSlider;

	public Slider BGMSlider;

	public Toggle SFXToggle;

	public Toggle ENVToggle;

	public Toggle BGMToggle;

	[Header("Video Settings — Detail")]
	[Tooltip("Render scale 1.0 / 0.85 / 0.7, plus the QualitySettings level for textures, skinning and particles. The HUD is a Screen Space - Overlay canvas, so it stays sharp at every step.")]
	public OptionButtonRow detailButtons;

	[Header("Video Settings — Shadow")]
	[Tooltip("High = one cascade at 1024. Medium = one cascade at 512. Low = shadows off entirely.")]
	public OptionButtonRow shadowButtons;

	[Header("Video Settings — FPS")]
	[Tooltip("Target frame rate. TWO buttons only: High = 60, Medium = 30. Leave Low unassigned — the old 24 fps step is retired and any saved value for it folds into 30.")]
	public OptionButtonRow fpsButtons;

	[Header("Video Settings — Effect Toggles")]
	[Tooltip("Master switch for skill / spell VFX (SkillSpecial spawns). Off = no skill effects spawn at all.")]
	public Toggle effectsToggle;

	[Tooltip("Realtime reflections (reflection probes + planar-reflection water).")]
	public Toggle reflexToggle;

	[Tooltip("Bloom / glow. Requires Post Process to be on as well — glow IS a post-process pass.")]
	public Toggle glowToggle;

	[Tooltip("Shadows cast by scenery/terrain. Character shadows are unaffected.")]
	public Toggle bgShadowToggle;

	[Tooltip("Screen-space refraction (water, glass, heat distortion). Drives the URP opaque texture.")]
	public Toggle refractToggle;

	[Tooltip("Master post-processing switch on the gameplay camera. Off = no fullscreen post passes (cheapest).")]
	public Toggle postProcessToggle;

	[Tooltip("The TargetThis_Indicator ring shown under the currently selected target.")]
	public Toggle targetEffectToggle;

	[Header("In-Game Settings")]
	public Toggle refuseDuel;

	public Toggle refuseRevive;

	public Toggle hideDeadBody;

	public Toggle refuseTrade;

	public Toggle statsPrivate;

	public Toggle refuseParty;

	public Toggle refuseRide;

	public Toggle hideSkillEffects;

	public Toggle refuseFriend;

	public Toggle transmission;

	public Toggle helpInfo;

	public Toggle hpMiniGuage;

	[Tooltip("\"Hide Costume Set\" — replaces every REMOTE player's worn upper/lower/gloves/boots with the plain HideCos costume on the player prefab. Headgear is never hidden (the costume has no headgear piece).")]
	public Toggle hideCostumeSet;

	[Tooltip("\"Hide Map by default\" — the minimap starts HIDDEN every time the player logs in and enters the game, with the menu buttons slid into their no-minimap position. The in-game minimap toggle button still works normally during the session; this only decides the starting state.")]
	public Toggle hideMapDefault;

	public Toggle setDefault;

	[Header("Auto Pots References")]
	public Button openAutoPotsButton;

	[Header("Targeting Settings")]
	public Toggle targetingToggle;

	public Button openTargetingModeButton;

	[Header("Loot Settings")]
	[Tooltip("Opens the reusable Classic / Dual Loot selection panel on the in-game HUD.")]
	public Button openLootModeButton;

	public Slider sensitivitySlider;

	public TMP_Text sensitivityText;

	[Header("Language Settings")]
	public Button openLanguageButton;

	[Header("Tutorial Button")]
	public Button openTutorialButton;

	[Header("Macro Settings")]
	[Tooltip("\"Edit Macro\" button placed in the Options UI.")]
	public Button openMacroButton;

	[Header("Screenshot Settings")]
	[Tooltip("\"Screenshot\" button placed in the Options UI. Hides the skill buttons + overlay panels for a clean shot.")]
	public Button screenshotButton;

	[Header("In-Game Only Objects")]
	[Tooltip("Objects that only make sense during gameplay (Tutorial, Photo/Screenshot, Macro, Auto Pots, ...). The same Options UI is also shown in the login / server-select scenes, where the in-game HUD (UIHUDFunctions) does not exist — everything listed here stays VISIBLE in those scenes but is made non-interactable (greyed out) instead of being hidden.")]
	public List<GameObject> inGameOnlyObjects;

	[Tooltip("CanvasGroup alpha applied to the in-game-only rows while they are non-interactable. 1 = no dimming (Selectables still show their own disabled tint); lower values fade the whole row, labels included.")]
	[Range(0.2f, 1f)]
	public float inGameOnlyDisabledAlpha;

	private AutoPotionUIManager autoPotsUIManager;

	private const string DefaultsVersionKey = "OptionDefaultsVersion";

	private const int DefaultsVersion = 2;

	internal const string HideSkillEffectsPrefKey = "HideSkillEffects";

	public const string DetailIndexPrefKey = "DetailIndex";

	public const string ShadowIndexPrefKey = "ShadowIndex";

	public const string FpsIndexPrefKey = "FPSIndex";

	public const int IndexHigh = 2;

	private const float MinimumLodBias = 0.7f;

	public static event Action<float> OnSensitivityChangedEvent
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

	public static void ApplyDetailLevel(int sliderValue)
	{
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void ApplyShippedDefaults()
	{
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void ApplyLiveAudioSettings()
	{
	}

	public void RefreshInGameOnlyObjects()
	{
	}

	private static string GetHierarchyPath(Transform t)
	{
		return null;
	}

	public void OpenAutoPotsMenu()
	{
	}

	public void OpenLanguageMenu()
	{
	}

	public void OpenMacroPanel()
	{
	}

	public void EnterScreenshotMode()
	{
	}

	public void OpenTutorial()
	{
	}

	private void LoadSavedOption()
	{
	}

	public void SaveAppleOption()
	{
	}

	public void ApplyLanguageSettings()
	{
	}

	public void LanguageOption(bool isLoad)
	{
	}

	public void HandleLanguageToggle(bool isOn, int index)
	{
	}

	public void InitialOption()
	{
	}

	public void VideoOption(bool isLoad)
	{
	}

	private void PushVideoTogglesToUI()
	{
	}

	private void PullVideoTogglesFromUI()
	{
	}

	private static void SetToggleWithoutNotify(Toggle toggle, bool value)
	{
	}

	private static bool ReadToggle(Toggle toggle, bool fallback)
	{
		return false;
	}

	public static int FpsIndexToTarget(int fpsIndex)
	{
		return 0;
	}

	public static int NormalizeFpsIndex(int fpsIndex)
	{
		return 0;
	}

	public static int DefaultFpsIndexForDevice()
	{
		return 0;
	}

	private void ApplyFPSSettings(int fpsIndex)
	{
	}

	private void ApplyShadowSettings(int index)
	{
	}

	private static void ApplyPlatformVSync()
	{
	}

	private void SetToggleState(Toggle toggle, string key)
	{
	}

	private void SetToggleStateInitial(Toggle toggle, string key)
	{
	}

	private void SetTargetStateInitial(Toggle toggle, string key)
	{
	}

	private void SetSliderValue(Slider slider, string key)
	{
	}

	private void SetSliderValueInitial(Slider slider, string key)
	{
	}

	public void AudioOption(bool isLoad)
	{
	}

	public void InGameOption(bool isLoad)
	{
	}

	private void ApplyHideCostumeSet()
	{
	}

	private void SetPlayerPrefs(string key, bool value)
	{
	}

	private void SetVolume(float volume, AudioMixerGroup mixer)
	{
	}

	public void Mute(bool isMuted, AudioMixerGroup mixer)
	{
	}

	private void HandleSensitivityChange(float newValue)
	{
	}
}
