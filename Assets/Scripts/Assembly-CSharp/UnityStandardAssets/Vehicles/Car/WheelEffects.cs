using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	[RequireComponent(typeof(AudioSource))]
	public class WheelEffects : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CStartSkidTrail_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public WheelEffects _003C_003E4__this;

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
			public _003CStartSkidTrail_003Ed__18(int _003C_003E1__state)
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

		public Transform SkidTrailPrefab;

		public static Transform skidTrailsDetachedParent;

		public ParticleSystem skidParticles;

		private AudioSource m_AudioSource;

		private Transform m_SkidTrail;

		private WheelCollider m_WheelCollider;

		public bool skidding { get; private set; }

		public bool PlayingAudio { get; private set; }

		private void Start()
		{
		}

		public void EmitTyreSmoke()
		{
		}

		public void PlayAudio()
		{
		}

		public void StopAudio()
		{
		}

		[IteratorStateMachine(typeof(_003CStartSkidTrail_003Ed__18))]
		public IEnumerator StartSkidTrail()
		{
			return null;
		}

		public void EndSkidTrail()
		{
		}
	}
}
