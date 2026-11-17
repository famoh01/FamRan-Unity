using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BusterPopupFX : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CPlayRANSequence_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BusterPopupFX _003C_003E4__this;

		private float _003Ctimer_003E5__2;

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
		public _003CPlayRANSequence_003Ed__19(int _003C_003E1__state)
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

	[Header("Multi-Layer Scale Settings")]
	[SerializeField]
	private float biggestScale;

	[SerializeField]
	private float midScale;

	[SerializeField]
	private float smallestScale;

	[Tooltip("Adjust this if 50% looks too faint. 1.0 is solid, 0.0 is invisible.")]
	[Range(0f, 1f)]
	[SerializeField]
	private float baseOpacity;

	[Header("Animation Timings")]
	[SerializeField]
	private float delayBeforeMidSpawn;

	[SerializeField]
	private float ghostFadeDuration;

	[SerializeField]
	private float delayBetweenFades;

	[SerializeField]
	private float finalFadeToHalfDuration;

	[SerializeField]
	private float stayDuration;

	private Vector3 originalScale;

	private CanvasGroup mainCanvasGroup;

	private GameObject ghostBiggest;

	private CanvasGroup ghostBiggestCG;

	private GameObject ghostMid;

	private CanvasGroup ghostMidCG;

	private void Awake()
	{
	}

	private void CreateGhosts()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	[IteratorStateMachine(typeof(_003CPlayRANSequence_003Ed__19))]
	private IEnumerator PlayRANSequence()
	{
		return null;
	}
}
