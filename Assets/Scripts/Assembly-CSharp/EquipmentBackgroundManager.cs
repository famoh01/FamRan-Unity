using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentBackgroundManager : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayedBackgroundUpdate_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public EquipmentBackgroundManager _003C_003E4__this;

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
		public _003CDelayedBackgroundUpdate_003Ed__7(int _003C_003E1__state)
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

	[Header("UI References")]
	public Image backgroundImage;

	[Header("Class Backgrounds")]
	public Sprite swordsmanBackground;

	public Sprite brawlerBackground;

	public Sprite archerBackground;

	public Sprite shamanBackground;

	public Sprite defaultBackground;

	private void OnEnable()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayedBackgroundUpdate_003Ed__7))]
	private IEnumerator DelayedBackgroundUpdate()
	{
		return null;
	}

	public void UpdateBackground()
	{
	}
}
