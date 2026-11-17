using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomUILayoutManager : MonoBehaviour
{
	[Serializable]
	public struct MirroredUIPair
	{
		public CustomizableUI first;

		public CustomizableUI second;
	}

	[CompilerGenerated]
	private sealed class _003CShowSaveMessage_003Ed__52 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomUILayoutManager _003C_003E4__this;

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
		public _003CShowSaveMessage_003Ed__52(int _003C_003E1__state)
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

	public static CustomUILayoutManager Instance;

	[Header("References")]
	public Canvas optionsCanvas;

	public GameObject editIndicator;

	public GameObject editHUD;

	public GameObject editModeBG;

	public Button enterEditButton;

	public Button saveButton;

	public Button cancelButton;

	public Button resetButton;

	public Image defaultSizeMarker;

	public GameObject[] forceEnableOnEdit;

	[Header("Size Controls")]
	public Slider sizeSlider;

	public TextMeshProUGUI sizePercentageText;

	[Header("Opacity Controls")]
	public Slider opacitySlider;

	public TextMeshProUGUI opacityPercentageText;

	[Header("Feedback")]
	public TextMeshProUGUI saveMessageText;

	public float saveMessageDuration;

	[Header("Exit Confirmation Modal")]
	public GameObject exitConfirmModal;

	public TextMeshProUGUI exitConfirmMessage;

	public Button exitYesButton;

	public Button exitNoButton;

	public Button exitCancelButton;

	[Header("Preset Manager")]
	public CustomUIPresetManager presetManager;

	[Header("Mirrored Skill Buttons")]
	public MirroredUIPair[] mirroredSkillButtons;

	private Dictionary<GameObject, bool> originalActiveStates;

	private Dictionary<Behaviour, bool> originalComponentStates;

	private CustomizableUI[] customizableElements;

	private bool hasUnsavedChanges;

	[HideInInspector]
	public int activePreset;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void ForceMirror(CustomizableUI source)
	{
	}

	private void MirrorLayoutFrom(CustomizableUI source)
	{
	}

	private void MirrorOpacityFrom(CustomizableUI source)
	{
	}

	private CustomizableUI GetMirroredUI(CustomizableUI source)
	{
		return null;
	}

	private void SetupButtons()
	{
	}

	private void SetupHUD()
	{
	}

	private void SetupSizeSlider()
	{
	}

	private void SetupOpacitySlider()
	{
	}

	private void OnEnterEditMode()
	{
	}

	private static bool ShouldForceEnableForCurrentMode(GameObject target)
	{
		return false;
	}

	private void ResetSliders()
	{
	}

	private void OnSizeSliderChanged(float value)
	{
	}

	private void OnOpacitySliderChanged(float value)
	{
	}

	private void HandlePresetSwitch(int presetIndex)
	{
	}

	private void SwitchPreset(int presetIndex)
	{
	}

	private void LoadPreset(int preset)
	{
	}

	private void HandleUISelected(CustomizableUI ui)
	{
	}

	private void OnSaveLayout()
	{
	}

	[IteratorStateMachine(typeof(_003CShowSaveMessage_003Ed__52))]
	private IEnumerator ShowSaveMessage()
	{
		return null;
	}

	private void OnExitLayout()
	{
	}

	private void OnResetLayout()
	{
	}

	private void ExitEditMode()
	{
	}

	public void MarkUnsaved()
	{
	}

	private void RevertAllChanges()
	{
	}

	private bool IsLayoutDriven(CustomizableUI ui)
	{
		return false;
	}

	private void RebuildAllLayouts()
	{
	}
}
