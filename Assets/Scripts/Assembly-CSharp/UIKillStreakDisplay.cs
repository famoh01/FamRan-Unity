using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public sealed class UIKillStreakDisplay : MonoBehaviour
{
	private sealed class Ghost
	{
		public GameObject go;

		public RectTransform rt;

		public Image img;

		public CanvasGroup cg;
	}

	[CompilerGenerated]
	private sealed class _003CFadeCanvasGroup_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float duration;

		public CanvasGroup cg;

		public float to;

		public float from;

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
		public _003CFadeCanvasGroup_003Ed__36(int _003C_003E1__state)
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
	private sealed class _003CFadeGhost_003Ed__35 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Ghost g;

		public float from;

		public float to;

		public float duration;

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
		public _003CFadeGhost_003Ed__35(int _003C_003E1__state)
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
	private sealed class _003CPlayRANSequence_003Ed__30 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public KillStreakTier tier;

		public UIKillStreakDisplay _003C_003E4__this;

		private Sprite _003Csprite_003E5__2;

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
		public _003CPlayRANSequence_003Ed__30(int _003C_003E1__state)
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
	[Tooltip("The Image that shows the tier sprite (the front layer).")]
	public Image streakImage;

	[Tooltip("CanvasGroup driving the front sprite's fade. Auto-added if left empty.")]
	public CanvasGroup canvasGroup;

	[Tooltip("Optional AudioSource for tier sounds. If empty, sounds are skipped.")]
	public AudioSource audioSource;

	[Header("Ghost Echo (RAN multi-layer)")]
	[Tooltip("Scale of the biggest background echo (spawns first, fades first).")]
	public float biggestScale;

	[Tooltip("Scale of the mid background echo (spawns after a beat, fades second).")]
	public float midScale;

	[Tooltip("Scale of the front sprite (stays constant through the effect).")]
	public float smallestScale;

	[Range(0f, 1f)]
	[Tooltip("Resting opacity for the echoes and the settled front sprite. 1 = solid, 0 = invisible.")]
	public float baseOpacity;

	[Header("Timings")]
	[Tooltip("Beat between the biggest echo and the mid echo appearing.")]
	public float delayBeforeMidSpawn;

	[Tooltip("Fade-out duration for each echo layer.")]
	public float ghostFadeDuration;

	[Tooltip("Pause between the biggest echo fading and the mid echo fading.")]
	public float delayBetweenFades;

	[Tooltip("Duration of the front sprite's flash from full opacity back to base opacity.")]
	public float finalFadeToHalfDuration;

	[Tooltip("How long the settled sprite stays on screen before fading away.")]
	[FormerlySerializedAs("holdDuration")]
	public float stayDuration;

	[Tooltip("Fade-out duration when the popup finally leaves the screen.")]
	[FormerlySerializedAs("fadeOutDuration")]
	public float endFadeDuration;

	[Header("Protected Popups")]
	[Tooltip("Minimum time a PROTECTED popup (e.g. First Blood) is guaranteed on screen before a newer popup can replace it. During this window incoming popups are held (latest wins) and shown the instant it ends — so First Blood is always seen even amid a rapid burst.")]
	public float protectedGuaranteedTime;

	[Header("Edit Mode Preview")]
	[Tooltip("Sprite shown while the UI layout editor (CustomizableUI edit mode) is active, so this popup can be positioned.")]
	public Sprite editPreviewSprite;

	private Ghost ghostBiggest;

	private Ghost ghostMid;

	private Vector3 baseScale;

	private Coroutine playRoutine;

	private bool isShowingPreview;

	private bool currentProtected;

	private float protectedUntil;

	private KillStreakTier pendingTier;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	public void Show(KillStreakTier tier, bool protect = false)
	{
	}

	private void StartPlay(KillStreakTier tier, bool protect)
	{
	}

	private bool IsProtectedWindowOpen()
	{
		return false;
	}

	public void Clear()
	{
	}

	[IteratorStateMachine(typeof(_003CPlayRANSequence_003Ed__30))]
	private IEnumerator PlayRANSequence(KillStreakTier tier)
	{
		return null;
	}

	private void EnsureGhosts()
	{
	}

	private Ghost CreateGhost(string name)
	{
		return null;
	}

	private void SyncGhost(Ghost g, Sprite sprite, float scaleMul, float alpha)
	{
	}

	private static void HideGhost(Ghost g)
	{
	}

	[IteratorStateMachine(typeof(_003CFadeGhost_003Ed__35))]
	private static IEnumerator FadeGhost(Ghost g, float from, float to, float duration)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CFadeCanvasGroup_003Ed__36))]
	private static IEnumerator FadeCanvasGroup(CanvasGroup cg, float from, float to, float duration)
	{
		return null;
	}

	private void HideImmediate()
	{
	}

	private void ShowPreview()
	{
	}

	private void HidePreview()
	{
	}
}
