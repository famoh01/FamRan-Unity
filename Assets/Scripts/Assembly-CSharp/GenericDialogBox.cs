using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenericDialogBox : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CCountdownThenInvoke_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GenericDialogBox _003C_003E4__this;

		public Action onComplete;

		private float _003CelapsedTime_003E5__2;

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
		public _003CCountdownThenInvoke_003Ed__28(int _003C_003E1__state)
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

	public TextMeshProUGUI _dialogMessage;

	public TextMeshProUGUI _dialogButtonYes;

	public TextMeshProUGUI _dialogButtonNo;

	public TMP_InputField _dialogTextField;

	public Button _dialogButtonContinue;

	public Button _dialogButtonClose;

	public GameObject deadPanel;

	public GameObject panelBlocker;

	private Coroutine _countdownCoroutine;

	public static GenericDialogBox Instance { get; private set; }

	private void OnEnable()
	{
	}

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	private static string GetHierarchyPath(Transform t)
	{
		return null;
	}

	private void OnInputValueChanged(string newValue)
	{
	}

	public void ShowDialog(string messageText, string yesText, string noText, bool withInputField, Action callBackYes, Action callBackNo, bool withCountDown = false)
	{
	}

	public void ShowDialog(string messageText, string yesText, bool withInputField, bool withPanelBlocker, Action callBackYes)
	{
	}

	public void ShowDialog(string messageText, string yesText, bool withInputField, Action callBackYes)
	{
	}

	public void ShowDialog(string messageText, string yesText)
	{
	}

	public void ShowDialog(string messageText, string yesText, string noText, Action callBackYes, bool withCountDown = false)
	{
	}

	public void Show(bool inputField)
	{
	}

	public void Hide()
	{
	}

	private void StopCountdown()
	{
	}

	private void AddCallBacks(Action callBackYes, Action callBackNo, bool withCountDown = false)
	{
	}

	private void BeginCommitCountdown(Action onComplete)
	{
	}

	[IteratorStateMachine(typeof(_003CCountdownThenInvoke_003Ed__28))]
	private IEnumerator CountdownThenInvoke(Action onComplete)
	{
		return null;
	}

	private void AddCallSingle(Action callBackYes)
	{
	}

	private void AddCallBacksRevived(Action callBackYes)
	{
	}
}
