using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

public class Job
{
	[CompilerGenerated]
	private sealed class _003CdoWork_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Job _003C_003E4__this;

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
		public _003CdoWork_003Ed__16(int _003C_003E1__state)
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
	private sealed class _003CstartAsCoroutine_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Job _003C_003E4__this;

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
		public _003CstartAsCoroutine_003Ed__21(int _003C_003E1__state)
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
	private Action<bool> m_jobComplete;

	private bool _running;

	private bool _paused;

	private IEnumerator _coroutine;

	private bool _jobWasKilled;

	private Stack<Job> _childJobStack;

	public bool running => false;

	public bool paused => false;

	public event Action<bool> jobComplete
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public Job(IEnumerator coroutine)
	{
	}

	public Job(IEnumerator coroutine, bool shouldStart)
	{
	}

	public static Job make(IEnumerator coroutine)
	{
		return null;
	}

	public static Job make(IEnumerator coroutine, bool shouldStart)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CdoWork_003Ed__16))]
	private IEnumerator doWork()
	{
		return null;
	}

	public Job createAndAddChildJob(IEnumerator coroutine)
	{
		return null;
	}

	public void addChildJob(Job childJob)
	{
	}

	public void removeChildJob(Job childJob)
	{
	}

	public void start()
	{
	}

	[IteratorStateMachine(typeof(_003CstartAsCoroutine_003Ed__21))]
	public IEnumerator startAsCoroutine()
	{
		return null;
	}

	public void pause()
	{
	}

	public void unpause()
	{
	}

	public void kill()
	{
	}

	public void kill(float delayInSeconds)
	{
	}
}
