using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	[DisallowMultipleComponent]
	public sealed class ModeSelectionOptionCard : MonoBehaviour
	{
		[Header("Option")]
		[SerializeField]
		private int optionValue;

		[Header("Content")]
		[SerializeField]
		private TMP_Text titleLabel;

		[SerializeField]
		private TMP_Text descriptionLabel;

		[SerializeField]
		private Image previewImage;

		[SerializeField]
		private Sprite previewSprite;

		[Header("Localization (Optional)")]
		[Tooltip("Leave empty to use the fallback title below.")]
		[SerializeField]
		private string titleLocalizationKey;

		[Tooltip("Leave empty to use the fallback description below.")]
		[SerializeField]
		private string descriptionLocalizationKey;

		[SerializeField]
		private string fallbackTitle;

		[TextArea(3, 8)]
		[SerializeField]
		private string fallbackDescription;

		[Header("Interaction")]
		[SerializeField]
		private Button selectButton;

		[Tooltip("Optional object enabled only while this is the saved selection.")]
		[SerializeField]
		private GameObject selectedIndicator;

		private Action<int> _selectionHandler;

		private ModeSelectionProvider _settingsProvider;

		public int OptionValue => 0;

		public bool IsConfigured => false;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		public void Bind(Action<int> selectionHandler, ModeSelectionProvider settingsProvider)
		{
		}

		public void SetSelected(bool selected)
		{
		}

		private void ApplyContent()
		{
		}

		private static void ApplyLocalizedText(TMP_Text label, string localizationKey, string fallback)
		{
		}

		private void HandleSelected()
		{
		}
	}
}
