using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AirFishLab.ScrollingList.Demo
{
	public class ListIteration : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CIterationLoop_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ListIteration _003C_003E4__this;

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
			public _003CIterationLoop_003Ed__5(int _003C_003E1__state)
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
		private CircularScrollingList _list;

		[SerializeField]
		private int _step;

		[SerializeField]
		private float _stepInterval;

		private int _currentID;

		private void Start()
		{
		}

		[IteratorStateMachine(typeof(_003CIterationLoop_003Ed__5))]
		private IEnumerator IterationLoop()
		{
			return null;
		}
	}
}
