using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
public class AnnouncementManager : MonoBehaviour
{
	private struct AnnouncementData
	{
		public string Message;

		public Color TextColor;

		public float Duration;
	}

	[CompilerGenerated]
	private sealed class _003CDisplayNextAnnouncement_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AnnouncementManager _003C_003E4__this;

		private AnnouncementData _003Cdata_003E5__2;

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
		public _003CDisplayNextAnnouncement_003Ed__12(int _003C_003E1__state)
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
	private sealed class _003CFade_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AnnouncementManager _003C_003E4__this;

		public float startAlpha;

		public float endAlpha;

		public float duration;

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
		public _003CFade_003Ed__13(int _003C_003E1__state)
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

	public static AnnouncementManager Instance;

	[Header("UI References")]
	[Tooltip("Drag the Canvas component here. If left empty, it will auto-cache from this GameObject.")]
	public Canvas announcementCanvas;

	[Tooltip("Drag the TextMeshPro text component here.")]
	public TextMeshProUGUI announcementText;

	[Header("Settings")]
	public float displayDuration;

	public float fadeDuration;

	private CanvasGroup _canvasGroup;

	private Queue<AnnouncementData> _announcementQueue;

	private bool _isDisplaying;

	private WaitForSeconds _displayWait;

	private void Awake()
	{
	}

	public void AddAnnouncement(string message, Color color, float customDuration = -1f)
	{
	}

	[IteratorStateMachine(typeof(_003CDisplayNextAnnouncement_003Ed__12))]
	private IEnumerator DisplayNextAnnouncement()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CFade_003Ed__13))]
	private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
	{
		return null;
	}
}
