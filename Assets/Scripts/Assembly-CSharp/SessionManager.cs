using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass3_0
	{
		public SessionPromptHandler handler;

		internal bool _003CPromptWarning_003Eb__0()
		{
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class _003CPromptWarning_003Ed__3 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		private _003C_003Ec__DisplayClass3_0 _003C_003E8__1;

		public string msg;

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
		public _003CPromptWarning_003Ed__3(int _003C_003E1__state)
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

	public static SessionManager instance;

	[Header("Reference")]
	[SerializeField]
	private AccountDataSO _accountDataSO;

	private void Awake()
	{
	}

	[IteratorStateMachine(typeof(_003CPromptWarning_003Ed__3))]
	public IEnumerator PromptWarning(string msg)
	{
		return null;
	}
}
