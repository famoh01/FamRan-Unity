using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDeleteCountdown : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CCountdownRoutine_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CharacterDeleteCountdown _003C_003E4__this;

		public string characterName;

		private float _003Cremaining_003E5__2;

		private int _003ClastShown_003E5__3;

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
		public _003CCountdownRoutine_003Ed__23(int _003C_003E1__state)
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

	[Header("Modal")]
	[Tooltip("Root object toggled on/off. Leave unassigned to use this GameObject.")]
	public GameObject modalRoot;

	[Tooltip("The '\"Name\" will be deleted' line. Optional.")]
	public TextMeshProUGUI messageText;

	[Tooltip("Live seconds remaining. Optional — without it the seconds are appended to messageText.")]
	public TextMeshProUGUI countdownText;

	[Tooltip("The only button on this modal. Wired from code; leave its Inspector OnClick empty.")]
	public Button cancelButton;

	[Tooltip("Optional full-screen raycast blocker, forced on while the countdown runs.")]
	public GameObject panelBlocker;

	[Header("Timing")]
	[Tooltip("Seconds the player has to change their mind before the delete request is sent.")]
	[Min(1f)]
	public float countdownSeconds;

	[Tooltip("Optional Image (Image Type: Filled) that drains from 1 to 0 across the countdown.")]
	public Image progressFill;

	private Coroutine _routine;

	private Action _onElapsed;

	private Action _onCancelled;

	private bool _opening;

	public static CharacterDeleteCountdown Instance { get; private set; }

	private GameObject Root => null;

	public bool IsCounting => false;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void Begin(string characterName, Action onElapsed, Action onCancelled = null)
	{
	}

	public void Cancel()
	{
	}

	[IteratorStateMachine(typeof(_003CCountdownRoutine_003Ed__23))]
	private IEnumerator CountdownRoutine(string characterName)
	{
		return null;
	}

	private void Render(string characterName, int secondsRemaining, float normalized)
	{
	}

	private void OnDisable()
	{
	}

	private void StopRoutine()
	{
	}

	private void ClearCallbacks()
	{
	}

	private void Close()
	{
	}
}
