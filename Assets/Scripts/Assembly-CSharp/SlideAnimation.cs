using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SlideAnimation : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CAnimateSlideIn_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SlideAnimation _003C_003E4__this;

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
		public _003CAnimateSlideIn_003Ed__14(int _003C_003E1__state)
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
	private sealed class _003CAnimateSlideOut_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SlideAnimation _003C_003E4__this;

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
		public _003CAnimateSlideOut_003Ed__15(int _003C_003E1__state)
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

	public static SlideAnimation Instance;

	public float SlideSpeed;

	[Header("Slide Directions")]
	public bool slideDown;

	public bool slideUp;

	public bool slideRight;

	public bool slideLeft;

	[Space]
	public GameObject showButton;

	public GameObject hideButton;

	private Vector3 originalPosition;

	private Vector3 targetPosition;

	public bool isAnimating;

	public void Awake()
	{
	}

	private void OnEnable()
	{
	}

	public void DisableOtherBtns()
	{
	}

	[IteratorStateMachine(typeof(_003CAnimateSlideIn_003Ed__14))]
	private IEnumerator AnimateSlideIn()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CAnimateSlideOut_003Ed__15))]
	private IEnumerator AnimateSlideOut()
	{
		return null;
	}

	private void Update()
	{
	}
}
