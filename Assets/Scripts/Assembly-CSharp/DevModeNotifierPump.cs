using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DevModeNotifierPump : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CWatch_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Func<bool> shown;

		public Action onTimeout;

		private float _003Cdeadline_003E5__2;

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
		public _003CWatch_003Ed__7(int _003C_003E1__state)
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

	private const float NativeDialogTimeout = 3f;

	private static DevModeNotifierPump _instance;

	private static readonly Queue<Action> _queue;

	public static void Ensure()
	{
	}

	public static void Enqueue(Action action)
	{
	}

	public static void WatchNativeDialog(Func<bool> shown, Action onTimeout)
	{
	}

	private void Awake()
	{
	}

	[IteratorStateMachine(typeof(_003CWatch_003Ed__7))]
	private IEnumerator Watch(Func<bool> shown, Action onTimeout)
	{
		return null;
	}

	private void Update()
	{
	}
}
