using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class DestinyBoxTextFX : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CFadeCanvasGroup_003Ed__30 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CanvasGroup cg;

		public float from;

		public float to;

		public float duration;

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
		public _003CFadeCanvasGroup_003Ed__30(int _003C_003E1__state)
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
	private sealed class _003CPlayRANSequence_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DestinyBoxTextFX _003C_003E4__this;

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
		public _003CPlayRANSequence_003Ed__29(int _003C_003E1__state)
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

	[Header("RAN Online Multi-Layer Settings")]
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

	[Header("Sounds")]
	[SerializeField]
	private bool ohNoQbox;

	[SerializeField]
	private bool bombQbox;

	[SerializeField]
	private AudioClip qBoxSFX;

	private Vector3 originalScale;

	private AudioClip originalBGM;

	private AudioManager audioManager;

	private CanvasGroup mainCanvasGroup;

	private bool isCloneGhost;

	private GameObject ghostBiggest;

	private CanvasGroup ghostBiggestCG;

	private GameObject ghostMid;

	private CanvasGroup ghostMidCG;

	private WaitForSeconds waitBeforeMidSpawn;

	private WaitForSeconds waitBetweenFades;

	private WaitForSeconds waitStayDuration;

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

	private void OnDestroy()
	{
	}

	[IteratorStateMachine(typeof(_003CPlayRANSequence_003Ed__29))]
	private IEnumerator PlayRANSequence()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CFadeCanvasGroup_003Ed__30))]
	private static IEnumerator FadeCanvasGroup(CanvasGroup cg, float from, float to, float duration)
	{
		return null;
	}

	private void ChangeBGM()
	{
	}

	private void RestoreOriginalBGM()
	{
	}
}
