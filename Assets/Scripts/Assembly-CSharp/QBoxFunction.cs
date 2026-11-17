using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class QBoxFunction : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CStartQboxTimer_003Ed__24 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GameObject qBoxIndicatorObj;

		public Image durationFillImage;

		public float duration;

		private float _003CelapsedTime_003E5__2;

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
		public _003CStartQboxTimer_003Ed__24(int _003C_003E1__state)
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

	public static QBoxFunction instance;

	[Header("QBox Sprite")]
	public GameObject qBoxImg;

	public GameObject qBoxPrefab;

	public Sprite normalQBox;

	public Sprite powerUpSprite;

	public Sprite powerMaxSprite;

	public Sprite expTimeSprite;

	public Sprite speedUpSprite;

	public Sprite speedMaxSprite;

	public Sprite crazyTimeSprite;

	public Sprite madTimeSprite;

	public Sprite luckySprite;

	private GameObject currentQBoxIndicator;

	private void Awake()
	{
	}

	public void ResetQBoxIndicator()
	{
	}

	public void SpawnQBoxPowerUpIndicator(float duration)
	{
	}

	public void SpawnQBoxPowerMaxIndicator(float duration)
	{
	}

	public void SpawnQBoxExpTimeIndicator(float duration)
	{
	}

	public void SpawnQBoxSpeedUpIndicator(float duration)
	{
	}

	public void SpawnQBoxSpeedMaxIndicator(float duration)
	{
	}

	public void SpawnQBoxCrazyTimeIndicator(float duration)
	{
	}

	public void SpawnQBoxMadTimeIndicator(float duration)
	{
	}

	public void SpawnQBoxLuckyIndicator(float duration)
	{
	}

	public GameObject SpawnQBoxIndicator(Sprite sprite, float? duration)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CStartQboxTimer_003Ed__24))]
	private IEnumerator StartQboxTimer(float duration, Image durationFillImage, GameObject qBoxIndicatorObj)
	{
		return null;
	}
}
