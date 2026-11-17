using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class ScrollStepButtons : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CHoldRoutine_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ScrollStepButtons _003C_003E4__this;

		public int direction;

		private float _003Cwait_003E5__2;

		private float _003Celapsed_003E5__3;

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
		public _003CHoldRoutine_003Ed__28(int _003C_003E1__state)
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
	private sealed class _003CScrollRoutine_003Ed__25 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ScrollStepButtons _003C_003E4__this;

		public float target;

		private float _003Cstart_003E5__2;

		private float _003Celapsed_003E5__3;

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
		public _003CScrollRoutine_003Ed__25(int _003C_003E1__state)
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

	[Header("References")]
	public ScrollRect scrollRect;

	public Button upButton;

	public Button downButton;

	[Header("Stepping")]
	[Tooltip("Rows moved per tap.")]
	[Min(1f)]
	public int rowsPerStep;

	[Tooltip("Row height in pixels. Leave at 0 to read it from the content's GridLayoutGroup (cell + spacing) or VerticalLayoutGroup (first row + spacing).")]
	[Min(0f)]
	public float rowHeightOverride;

	[Tooltip("Land on exact row boundaries. With this on, a step after a free drag tidies the list up instead of carrying the drag's half-row offset forever.")]
	public bool snapToRows;

	[Header("Feel")]
	[Tooltip("Seconds to glide to the new row. 0 jumps instantly.")]
	[Min(0f)]
	public float stepDuration;

	[Tooltip("Holding a button keeps stepping.")]
	public bool holdToRepeat;

	[Min(0.05f)]
	public float holdDelay;

	[Min(0.02f)]
	public float holdInterval;

	[Tooltip("Grey the buttons out at the top / bottom of the list.")]
	public bool disableAtEnds;

	private Coroutine _scrollRoutine;

	private Coroutine _holdRoutine;

	private void Reset()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Bind(Button button, int direction)
	{
	}

	private static void AddTrigger(EventTrigger trigger, EventTriggerType type, UnityAction<BaseEventData> action)
	{
	}

	public void StepUp()
	{
	}

	public void StepDown()
	{
	}

	public void Step(int direction)
	{
	}

	private float ScrollableHeight()
	{
		return 0f;
	}

	private float RowHeight()
	{
		return 0f;
	}

	private static float FirstActiveChildHeight(RectTransform content)
	{
		return 0f;
	}

	private void ScrollTo(float normalized)
	{
	}

	[IteratorStateMachine(typeof(_003CScrollRoutine_003Ed__25))]
	private IEnumerator ScrollRoutine(float target)
	{
		return null;
	}

	private void StartHold(int direction)
	{
	}

	private void StopHold()
	{
	}

	[IteratorStateMachine(typeof(_003CHoldRoutine_003Ed__28))]
	private IEnumerator HoldRoutine(int direction)
	{
		return null;
	}

	private void OnScrolled(Vector2 _)
	{
	}

	private void RefreshInteractable()
	{
	}
}
