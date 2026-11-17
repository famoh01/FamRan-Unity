using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts
{
	[DisallowMultipleComponent]
	public sealed class FirstRunModeSelectionCoordinator : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CRunSequence_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public FirstRunModeSelectionCoordinator _003C_003E4__this;

			private int _003Ci_003E5__2;

			private ModeSelectionPanelController _003Cprompt_003E5__3;

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
			public _003CRunSequence_003Ed__5(int _003C_003E1__state)
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

		[SerializeField]
		private TutorialManager tutorialManager;

		[Tooltip("First-run mode panels in the exact order they should be shown.")]
		[SerializeField]
		private ModeSelectionPanelController[] orderedPrompts;

		private Coroutine _sequenceRoutine;

		private void Start()
		{
		}

		private void OnDestroy()
		{
		}

		[IteratorStateMachine(typeof(_003CRunSequence_003Ed__5))]
		private IEnumerator RunSequence()
		{
			return null;
		}
	}
}
