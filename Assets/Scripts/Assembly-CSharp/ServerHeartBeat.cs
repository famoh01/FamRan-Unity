using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ServerHeartBeat : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CCheckInternetConnectionPeriodically_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ServerHeartBeat _003C_003E4__this;

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
		public _003CCheckInternetConnectionPeriodically_003Ed__11(int _003C_003E1__state)
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
	private sealed class _003CSendAgentHeartbeatPeriodically_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		private WaitForSeconds _003Cwait_003E5__2;

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
		public _003CSendAgentHeartbeatPeriodically_003Ed__10(int _003C_003E1__state)
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
	private sealed class _003CZoneKeepaliveAndWatchdog_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		private WaitForSeconds _003Cwait_003E5__2;

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
		public _003CZoneKeepaliveAndWatchdog_003Ed__9(int _003C_003E1__state)
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

	public static ServerHeartBeat instance;

	private float checkInterval;

	private const float ReachabilityGraceSeconds = 6f;

	private float _unreachableSince;

	private const float AgentHeartbeatInterval = 5f;

	private const float ZoneKeepaliveInterval = 5f;

	private const float ZonePongTimeoutSeconds = 20f;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CZoneKeepaliveAndWatchdog_003Ed__9))]
	private IEnumerator ZoneKeepaliveAndWatchdog()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CSendAgentHeartbeatPeriodically_003Ed__10))]
	private IEnumerator SendAgentHeartbeatPeriodically()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CCheckInternetConnectionPeriodically_003Ed__11))]
	private IEnumerator CheckInternetConnectionPeriodically()
	{
		return null;
	}

	private bool IsSceneOpen(string scene)
	{
		return false;
	}

	public void Update()
	{
	}

	private void CheckConnectionAndLog()
	{
	}

	private void OnApplicationFocus(bool hasFocus)
	{
	}

	private void OnApplicationPause(bool paused)
	{
	}

	public bool CheckInternetConnection()
	{
		return false;
	}
}
