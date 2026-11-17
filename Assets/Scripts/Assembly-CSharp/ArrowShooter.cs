using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CLaunchAfterDelay_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public ArrowShooter _003C_003E4__this;

		public GameObject prefab;

		public Vector3 target;

		public float arrowSpeed;

		public bool ignoreFlightCap;

		public Vector3? originOverride;

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
		public _003CLaunchAfterDelay_003Ed__12(int _003C_003E1__state)
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
	private sealed class _003CPlayHitAnimAfter_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public ArrowShooter _003C_003E4__this;

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
		public _003CPlayHitAnimAfter_003Ed__14(int _003C_003E1__state)
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

	private const float MaxFlightTimeSec = 0.08f;

	public ProjectileData projectileData;

	public Transform spawner;

	public float arrowSpeed;

	public float spawnDelay;

	public float arrowOffset;

	private AnimationConfiguration _animConfig;

	private void Awake()
	{
	}

	public void ShootArrow(Vector3 target, float arrowSpeed = 300f, bool ignoreFlightCap = false)
	{
	}

	public void ShootSkillArrow(GameObject arrowEffect, Vector3 target, float arrowSpeed = 300f, bool ignoreFlightCap = false, Vector3? originOverride = null)
	{
	}

	private GameObject ResolveNormalPrefab()
	{
		return null;
	}

	private void Launch(GameObject prefab, Vector3 target, float arrowSpeed, bool ignoreFlightCap, Vector3? originOverride)
	{
	}

	[IteratorStateMachine(typeof(_003CLaunchAfterDelay_003Ed__12))]
	private IEnumerator LaunchAfterDelay(GameObject prefab, Vector3 target, float delay, float arrowSpeed, bool ignoreFlightCap, Vector3? originOverride)
	{
		return null;
	}

	private void Fire(GameObject prefab, Vector3 target, float arrowSpeed, bool ignoreFlightCap, Vector3? originOverride)
	{
	}

	[IteratorStateMachine(typeof(_003CPlayHitAnimAfter_003Ed__14))]
	private IEnumerator PlayHitAnimAfter(float delay)
	{
		return null;
	}
}
