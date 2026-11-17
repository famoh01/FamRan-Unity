using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PatchNoteWindow : MonoBehaviour
{
	[Serializable]
	private class NewsItemDto
	{
		public int Id;

		public string Url;

		public string Name;

		public string Description;

		public DateTime Date;

		public int Type;
	}

	[Serializable]
	private class NewsPageDto
	{
		public NewsItemDto[] Items;
	}

	[CompilerGenerated]
	private sealed class _003CFetchThenShow_003Ed__41 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PatchNoteWindow _003C_003E4__this;

		private UnityWebRequest _003Crequest_003E5__2;

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
		public _003CFetchThenShow_003Ed__41(int _003C_003E1__state)
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

		private void _003C_003Em__Finally1()
		{
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CWatchCloseGate_003Ed__61 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PatchNoteWindow _003C_003E4__this;

		private float _003Celapsed_003E5__2;

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
		public _003CWatchCloseGate_003Ed__61(int _003C_003E1__state)
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

	[Header("UI References")]
	[Tooltip("Canvas that holds the patch note window. Toggled on/off.")]
	[SerializeField]
	private Canvas patchNoteCanvas;

	[Tooltip("The TMP text inside the scroll view that holds the patch notes.")]
	[SerializeField]
	private TMP_Text patchNoteText;

	[Tooltip("Optional: displays the patch note version (e.g. 'v1.0.0').")]
	[SerializeField]
	private TMP_Text versionLabel;

	[Tooltip("Optional: the scroll view. Reset to the top every time the window opens.")]
	[SerializeField]
	private ScrollRect scrollRect;

	[Tooltip("Optional: close button. Wired automatically if assigned.")]
	[SerializeField]
	private Button closeButton;

	[Tooltip("Optional: button that opens the window — and closes it again when it is already open. Wired automatically if assigned.")]
	[SerializeField]
	private Button openButton;

	[Header("Patch Note Data")]
	[Tooltip("Bump this string to show the window again to all players.")]
	[SerializeField]
	private string patchNoteVersion;

	[Tooltip("Optional prefix shown on the version label.")]
	[SerializeField]
	private string versionLabelFormat;

	[TextArea(10, 40)]
	[Tooltip("The patch note body. Supports TMP rich text tags. Used as-is when the API is off or unreachable — otherwise it is replaced by the fetched note.")]
	[SerializeField]
	private string patchNoteBody;

	[Header("API")]
	[Tooltip("Pull the newest PatchNotes entry from the website API instead of using the body typed above. The typed body stays as the offline fallback.")]
	[SerializeField]
	private bool fetchFromApi;

	[Tooltip("Root of the public API, e.g. https://api.trinityrift.online — no trailing slash needed.")]
	[SerializeField]
	private string apiBaseUrl;

	[Tooltip("Path of the public news endpoint, appended to the base URL.")]
	[SerializeField]
	private string newsEndpointPath;

	[Tooltip("News type to request. Matches the NewsType enum on the server.")]
	[SerializeField]
	private string newsType;

	[Tooltip("Seconds to wait for the API before falling back to the body typed above.")]
	[SerializeField]
	private float apiTimeoutSeconds;

	[Tooltip("Show the fetched note's title on the version label instead of the version string.")]
	[SerializeField]
	private bool useApiTitleAsVersionLabel;

	[Tooltip("Append the publish date under the title, e.g. 'SEP 08, 2026'.")]
	[SerializeField]
	private bool showApiDate;

	[Tooltip("Colours and sizes used when the API's HTML is converted to TMP rich text.")]
	[SerializeField]
	private HtmlToTmpRichText.Style richTextStyle;

	[Tooltip("Minutes a fetched note stays good on the device before the client asks the API again. The window only shows once per note anyway, so this mostly stops repeated scene loads from re-hitting the endpoint. 0 = fetch every time.")]
	[SerializeField]
	private float cacheMinutes;

	[Header("Save Data")]
	[Tooltip("PlayerPrefs key that stores the last patch note version the player saw.")]
	[SerializeField]
	private string patchNoteSaveKey;

	[Header("Close Gate")]
	[Tooltip("When on, the close button stays disabled until the player scrolls to the bottom of the patch note body. Requires a Scroll Rect reference.")]
	[SerializeField]
	private bool requireScrollToBottom;

	[Tooltip("How close to the bottom counts as 'read'. 0 = exact bottom, 0.02 = within 2%.")]
	[Range(0f, 0.25f)]
	[SerializeField]
	private float scrollBottomThreshold;

	[Tooltip("If on, the close button is hidden entirely until unlocked instead of just being greyed out.")]
	[SerializeField]
	private bool hideCloseButtonUntilUnlocked;

	[Tooltip("Safety net: unlock the close button anyway after this many seconds, so a mis-set up scroll view can never trap the player in the window. Set to 0 to disable (not recommended).")]
	[SerializeField]
	private float failsafeUnlockSeconds;

	[Header("Testing")]
	[SerializeField]
	private bool alwaysShowPatchNote;

	[SerializeField]
	private bool resetPatchNotePrefsOnStart;

	private bool closeUnlocked;

	private Coroutine gateRoutine;

	private string apiVersionLabel;

	private GraphicRaycaster fetchHiddenRaycaster;

	private string CacheBodyKey => null;

	private string CacheVersionKey => null;

	private string CacheLabelKey => null;

	private string CacheStampKey => null;

	private void Start()
	{
	}

	private void ShowIfUnseen()
	{
	}

	public void OpenPatchNote()
	{
	}

	public void TogglePatchNote()
	{
	}

	public bool IsWindowOpen()
	{
		return false;
	}

	public void ClosePatchNote()
	{
	}

	public bool IsCloseUnlocked()
	{
		return false;
	}

	public void UnlockCloseButton()
	{
	}

	private void OnDisable()
	{
	}

	public void SetPatchNoteBody(string body)
	{
	}

	[IteratorStateMachine(typeof(_003CFetchThenShow_003Ed__41))]
	private IEnumerator FetchThenShow()
	{
		return null;
	}

	private void HideDuringFetch()
	{
	}

	private void RestoreAfterFetch()
	{
	}

	private bool TryApplyApiNote(string json)
	{
		return false;
	}

	private bool TryApplyCachedNote(bool ignoreAge)
	{
		return false;
	}

	private void CacheCurrentNote()
	{
	}

	private void ClearCachedNote()
	{
	}

	private string BuildNewsUrl()
	{
		return null;
	}

	public bool HasSeenCurrentVersion()
	{
		return false;
	}

	public void ResetSeenState()
	{
	}

	private void MarkCurrentVersionAsSeen()
	{
	}

	private void StartGateWatch()
	{
	}

	[IteratorStateMachine(typeof(_003CWatchCloseGate_003Ed__61))]
	private IEnumerator WatchCloseGate()
	{
		return null;
	}

	private void EvaluateCloseGate()
	{
	}

	private bool HasReachedBottom()
	{
		return false;
	}

	private void LockCloseButton()
	{
	}

	private void ApplyCloseButtonState()
	{
	}

	private void RefreshContent()
	{
	}

	private void ResetScrollPosition()
	{
	}

	private void SetWindowVisible(bool visible)
	{
	}
}
