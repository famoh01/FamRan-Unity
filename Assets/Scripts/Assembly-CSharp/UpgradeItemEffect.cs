using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemEffect : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CBlink_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UpgradeItemEffect _003C_003E4__this;

		private Color _003CoriginalColor_003E5__2;

		private int _003CblinkCount_003E5__3;

		private int _003CmaxBlinkCount_003E5__4;

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
		public _003CBlink_003Ed__21(int _003C_003E1__state)
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
	private sealed class _003CShowAndFadeText_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GameObject textObject;

		public float showDuration;

		public UpgradeItemEffect _003C_003E4__this;

		private CanvasGroup _003CcanvasGroup_003E5__2;

		private float _003CfadeDuration_003E5__3;

		private float _003Ctimer_003E5__4;

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
		public _003CShowAndFadeText_003Ed__23(int _003C_003E1__state)
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
	private sealed class _003CShrink_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UpgradeItemEffect _003C_003E4__this;

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
		public _003CShrink_003Ed__22(int _003C_003E1__state)
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
	private sealed class _003CSpin_003Ed__20 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UpgradeItemEffect _003C_003E4__this;

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
		public _003CSpin_003Ed__20(int _003C_003E1__state)
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

	public static UpgradeItemEffect instance;

	public bool successResult;

	[Header("Spin Settings")]
	public float spinSpeed;

	public float enlargeSpeed;

	public float spinDelay;

	public int spinRotations;

	[Header("Blink Settings")]
	public float blinkDuration;

	public float blinkDelay;

	public float blinkSpeed;

	public Color blinkColor;

	[Space]
	public Vector3 startScale;

	public Vector3 normalScale;

	private Image imageComponent;

	public GameObject successText;

	public GameObject failText;

	public Toggle skipAnimationToggle;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Animate()
	{
	}

	[IteratorStateMachine(typeof(_003CSpin_003Ed__20))]
	private IEnumerator Spin()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CBlink_003Ed__21))]
	private IEnumerator Blink()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CShrink_003Ed__22))]
	private IEnumerator Shrink()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CShowAndFadeText_003Ed__23))]
	private IEnumerator ShowAndFadeText(GameObject textObject, float showDuration)
	{
		return null;
	}

	private void UpgradeVerdict()
	{
	}

	public void UpgradeSucess()
	{
	}

	public void UpgradeFail()
	{
	}

	public void ToggleResult()
	{
	}
}
