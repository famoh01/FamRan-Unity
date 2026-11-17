using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RANMobile.Authentication
{
	public class LegalTermsWindow : MonoBehaviour
	{
		[Serializable]
		public class LegalDocument
		{
			[Tooltip("Shown on the window's title label, e.g. 'Terms of Service'.")]
			public string title;

			[Tooltip("Optional: date this revision took effect, e.g. '21 August 2026'. Shown on the effective-date label; leave empty to hide that label.")]
			public string effectiveDate;

			[TextArea(10, 40)]
			[Tooltip("The document body. Supports TMP rich text tags (<b>, <color>, <size>).")]
			public string body;

			[Tooltip("The button that opens this document — the login screen's Terms or Privacy button, or a tab inside the window. Wired on Awake.")]
			public Button openButton;
		}

		[Header("UI References")]
		[Tooltip("Canvas that holds the legal window. Switched on and off — it is the whole window, so nothing else should live under it.")]
		[SerializeField]
		private Canvas _legalCanvas;

		[Tooltip("Label that shows the current document's title.")]
		[SerializeField]
		private TMP_Text _titleLabel;

		[Tooltip("The TMP text inside the scroll view that holds the document body.")]
		[SerializeField]
		private TMP_Text _bodyText;

		[Tooltip("Optional: label for the effective date. Hidden when the document has none.")]
		[SerializeField]
		private TMP_Text _effectiveDateLabel;

		[Tooltip("Optional: scroll view holding the body. Reset to the top whenever a document is shown, so a switched document never opens half way down.")]
		[SerializeField]
		private ScrollRect _scrollRect;

		[Tooltip("Optional: close button. Wired automatically if assigned.")]
		[SerializeField]
		private Button _closeButton;

		[Header("Documents")]
		[Tooltip("The documents this window can show. Each one is opened by its own button; the first is the fallback when something asks for the window without saying which document.")]
		[SerializeField]
		private List<LegalDocument> _documents;

		[Tooltip("Format for the effective-date label. {0} is the document's date.")]
		[SerializeField]
		private string _effectiveDateFormat;

		[Header("Behaviour")]
		[Tooltip("Close the window on the Android back button / Escape key.")]
		[SerializeField]
		private bool _closeOnBackButton;

		[Tooltip("Switch the canvas off on Awake. Leave on — a legal canvas left enabled in the scene covers the login screen until something closes it.")]
		[SerializeField]
		private bool _startHidden;

		private int _currentIndex;

		private GraphicRaycaster _raycaster;

		public int CurrentDocumentIndex => 0;

		public bool IsOpen => false;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void Update()
		{
		}

		public void Open()
		{
		}

		public void OpenDocument(string title)
		{
		}

		public void Close()
		{
		}

		public void Toggle()
		{
		}

		public void ShowDocument(int index)
		{
		}

		private void SetCanvasVisible(bool visible)
		{
		}

		private void ScrollToTop()
		{
		}

		private int IndexOfTitle(string title)
		{
			return 0;
		}
	}
}
