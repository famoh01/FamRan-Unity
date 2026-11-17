using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendRequestToast : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CFade_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FriendRequestToast _003C_003E4__this;

		public float duration;

		public float to;

		public float from;

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
		public _003CFade_003Ed__19(int _003C_003E1__state)
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
	private sealed class _003CShowQueued_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FriendRequestToast _003C_003E4__this;

		private float _003Cremaining_003E5__2;

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
		public _003CShowQueued_003Ed__18(int _003C_003E1__state)
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

	[Header("Toast")]
	[Tooltip("The visual to show and hide — a CHILD object, not this one. Leave empty to show/hide purely through the CanvasGroup alpha.")]
	public GameObject _root;

	[Tooltip("Message label. '{0}' is replaced with the requester's name.")]
	public TextMeshProUGUI _messageText;

	[Tooltip("Message format. {0} = requester name.")]
	public string _messageFormat;

	[Header("Optional")]
	[Tooltip("CanvasGroup for the fade. Without one the toast just snaps on and off.")]
	public CanvasGroup _canvasGroup;

	[Tooltip("Makes the whole toast tappable — opens the friends panel on the Requests tab.")]
	public Button _tapToOpenButton;

	[Tooltip("The friends panel, so tapping the toast can open it.")]
	public FriendPanelNavigation _friendPanel;

	[Header("Timing (seconds)")]
	[Tooltip("How long the message stays fully visible.")]
	[Range(1f, 10f)]
	public float _holdDuration;

	[Range(0f, 1f)]
	public float _fadeInDuration;

	[Range(0f, 1f)]
	public float _fadeOutDuration;

	private readonly Queue<string> _queue;

	private Coroutine _showRoutine;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	public void Show(string requesterName)
	{
	}

	public void Dismiss()
	{
	}

	public void OpenRequestsTab()
	{
	}

	private void OnFriendRequestReceived(string requesterName)
	{
	}

	[IteratorStateMachine(typeof(_003CShowQueued_003Ed__18))]
	private IEnumerator ShowQueued()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CFade_003Ed__19))]
	private IEnumerator Fade(float from, float to, float duration)
	{
		return null;
	}

	private void SetVisible(bool visible)
	{
	}
}
