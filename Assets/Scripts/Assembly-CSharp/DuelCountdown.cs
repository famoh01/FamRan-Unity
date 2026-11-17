using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DuelCountdown : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CCountdownCoroutine_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DuelCountdown _003C_003E4__this;

		private int _003Ci_003E5__2;

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
		public _003CCountdownCoroutine_003Ed__8(int _003C_003E1__state)
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
	private sealed class _003CHideFightIndicatorAfterDelay_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DuelCountdown _003C_003E4__this;

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
		public _003CHideFightIndicatorAfterDelay_003Ed__11(int _003C_003E1__state)
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

	[Header("Display")]
	public Sprite[] countdownSprites;

	public Image fightIndicator;

	[Header("Timing")]
	public float countdownTime;

	public float fightIndicatorDuration;

	private Image _countdownImage;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void StartCountdown()
	{
	}

	[IteratorStateMachine(typeof(_003CCountdownCoroutine_003Ed__8))]
	private IEnumerator CountdownCoroutine()
	{
		return null;
	}

	private void ShowCountdownSprite(int index)
	{
	}

	private void ShowFightIndicator()
	{
	}

	[IteratorStateMachine(typeof(_003CHideFightIndicatorAfterDelay_003Ed__11))]
	private IEnumerator HideFightIndicatorAfterDelay()
	{
		return null;
	}

	private void PlayAudio(string audioName)
	{
	}

	public void HideCountdown()
	{
	}

	private void HideFightIndicator()
	{
	}
}
