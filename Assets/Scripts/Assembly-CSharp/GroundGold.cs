using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GroundGold : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDestroyObjectCoroutine_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GroundGold _003C_003E4__this;

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
		public _003CDestroyObjectCoroutine_003Ed__5(int _003C_003E1__state)
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

	public LinkedListNode<GroundGold> node;

	public DropClientMoney data;

	public GameObject goldObject;

	[Header("States")]
	public string soundEffect;

	public void DropMoney()
	{
	}

	[IteratorStateMachine(typeof(_003CDestroyObjectCoroutine_003Ed__5))]
	private IEnumerator DestroyObjectCoroutine()
	{
		return null;
	}

	public void DestroyObject()
	{
	}

	public Vector3 GetRandomGroundPosition(Vector3 center, float range)
	{
		return default;
	}
}
