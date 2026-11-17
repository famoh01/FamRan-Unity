using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Models.Network.Shared.Enums;
using TrinityRift.Network;
using UnityEngine;

public class ReconnectManager : MonoBehaviour
{
	public enum ReconnectState
	{
		Idle = 0,
		Connecting = 1,
		LoggingIn = 2,
		SelectingChar = 3,
		Joining = 4,
		Failed = 5
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass53_0
	{
		public bool? connectOk;

		internal void _003CReconnectRoutine_003Eb__5(bool ok)
		{
		}

		internal bool _003CReconnectRoutine_003Eb__6()
		{
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class _003CReconnectRoutine_003Ed__53 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ReconnectManager _003C_003E4__this;

		private _003C_003Ec__DisplayClass53_0 _003C_003E8__1;

		private ServerConnectionManager _003Cscm_003E5__2;

		private string _003Chost_003E5__3;

		private int _003Cport_003E5__4;

		private bool _003Cconnected_003E5__5;

		private int _003Cattempt_003E5__6;

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
		public _003CReconnectRoutine_003Ed__53(int _003C_003E1__state)
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
	private sealed class _003CResumeWatchdog_003Ed__50 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ReconnectManager _003C_003E4__this;

		private ServerConnectionManager _003Cscm_003E5__2;

		private long _003CpongsBefore_003E5__3;

		private bool _003Cresent_003E5__4;

		private float _003Ct_003E5__5;

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
		public _003CResumeWatchdog_003Ed__50(int _003C_003E1__state)
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
	private sealed class _003CWaitUntil_003Ed__66 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Func<bool> cond;

		public float timeout;

		private float _003Ct_003E5__2;

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
		public _003CWaitUntil_003Ed__66(int _003C_003E1__state)
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

	[Header("Optional overlay (assign a passive 'Reconnecting…' canvas; a dim fallback is built at runtime if left empty)")]
	[SerializeField]
	private GameObject reconnectingOverlay;

	[Header("Set true ONLY to test the reconnect flow in the Windows editor")]
	[SerializeField]
	private bool enableInEditorForTesting;

	private const float BackgroundResumeReconnectThresholdSeconds = 10f;

	private const float ForceReloginBackgroundSeconds = 120f;

	private const float ResumeWatchdogSeconds = 3f;

	private const float ResumeProbeSeconds = 4f;

	private const float RecentResumeWindowSeconds = 20f;

	private const int MaxReconnectsPerWindow = 2;

	private const float ReconnectWindowSeconds = 30f;

	private const float ConnectTimeout = 10f;

	private const float LoginTimeout = 10f;

	private const float CharSelectTimeout = 10f;

	private const float JoinTimeout = 20f;

	private const float ReachabilityWaitTimeout = 15f;

	private const int MaxConnectAttempts = 3;

	private const float ConnectRetryDelay = 1.5f;

	private const int MaxLoginAttempts = 4;

	private const float AlreadyOnlineRetryDelay = 1.5f;

	private bool _isBackgrounded;

	private DateTime _backgroundedAtUtc;

	private float _lastResumeRealtime;

	private double _lastBackgroundSeconds;

	private bool _serverDisconnectPending;

	private int _reconnectsInWindow;

	private float _reconnectWindowStart;

	private string _cachedUsername;

	private string _cachedPassword;

	private bool _loginGot;

	private LoginResultEnum _loginStatus;

	private string _loginToken;

	private bool _charSelGot;

	private int _charSelStatus;

	private bool _zoneIdentified;

	private bool _joinFailed;

	private string _joinErr;

	public static ReconnectManager Instance { get; private set; }

	public ReconnectState State { get; private set; }

	private bool ReconnectEnabled => false;

	public bool IsInGame => false;

	public bool HasCachedCredentials => false;

	private static LoaderScene Loader => null;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void CacheCredentials(string username, string password)
	{
	}

	public void ClearCredentials()
	{
	}

	private void OnApplicationPause(bool paused)
	{
	}

	private void OnApplicationFocus(bool hasFocus)
	{
	}

	private void EnterBackground(string source)
	{
	}

	private void ExitBackground(string source)
	{
	}

	[IteratorStateMachine(typeof(_003CResumeWatchdog_003Ed__50))]
	private IEnumerator ResumeWatchdog()
	{
		return null;
	}

	private void ForceReturnToInit(string reason)
	{
	}

	public bool TryBeginReconnect(string reason, bool suspectHalfOpen = false)
	{
		return false;
	}

	[IteratorStateMachine(typeof(_003CReconnectRoutine_003Ed__53))]
	private IEnumerator ReconnectRoutine()
	{
		return null;
	}

	private void WireZoneHandlers(ServerConnectionManager scm)
	{
	}

	private static void ClearZoneHandlers(ServerConnectionManager scm)
	{
	}

	private bool TryGetAgentEndpoint(out string host, out int port)
	{
		host = null;
		port = default;
		return false;
	}

	private void Fail(string why)
	{
	}

	[IteratorStateMachine(typeof(_003CWaitUntil_003Ed__66))]
	private static IEnumerator WaitUntil(Func<bool> cond, float timeout)
	{
		return null;
	}

	public void ShowReconnectingOverlay()
	{
	}

	public void HideReconnectingOverlay()
	{
	}

	private void EnsureOverlay()
	{
	}
}
