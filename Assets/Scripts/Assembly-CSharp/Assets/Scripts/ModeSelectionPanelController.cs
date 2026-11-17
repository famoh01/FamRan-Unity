using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	[DisallowMultipleComponent]
	public sealed class ModeSelectionPanelController : MonoBehaviour
	{
		[Header("System")]
		[SerializeField]
		private ModeSelectionProvider settingsProvider;

		[Header("Panel")]
		[SerializeField]
		private Canvas panelRoot;

		[SerializeField]
		private Button closeButton;

		[Tooltip("Optional label/object shown only while the player must make an initial choice.")]
		[SerializeField]
		private GameObject requiredChoiceMessage;

		[Header("Options")]
		[Tooltip("Add one ModeSelectionOptionCard for every value supported by the provider.")]
		[SerializeField]
		private ModeSelectionOptionCard[] optionCards;

		private bool _requiresSelection;

		public bool IsOpen => false;

		public bool NeedsFirstRunSelection => false;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		public void OpenFromOptions()
		{
		}

		public bool TryOpenFirstRun()
		{
			return false;
		}

		public void Close()
		{
		}

		private void HandleOptionSelected(int value)
		{
		}

		private void RefreshCards()
		{
		}

		private void RefreshPromptState()
		{
		}

		private bool ValidateConfiguration()
		{
			return false;
		}

		private void SetVisible(bool visible)
		{
		}
	}
}
