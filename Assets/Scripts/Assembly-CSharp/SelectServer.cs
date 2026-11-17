using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectServer : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CSlideInServerPanel_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SelectServer _003C_003E4__this;

		private RectTransform _003CpanelTransform_003E5__2;

		private Vector3 _003CtargetPosition_003E5__3;

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
		public _003CSlideInServerPanel_003Ed__15(int _003C_003E1__state)
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

	public static SelectServer instance;

	public Button selected;

	public GameObject serverIndicator;

	public GameObject serverPanel;

	public bool isSlide;

	private static Button selectedButton;

	public float slideSpeed;

	public GameObject ssPanel;

	public Vector2 newPosition;

	[Header("Text Colors")]
	public Color selectedTextColor;

	public Color unselectedTextColor;

	private TextMeshProUGUI buttonText;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void SelectServerButton(Button button = null)
	{
	}

	[IteratorStateMachine(typeof(_003CSlideInServerPanel_003Ed__15))]
	private IEnumerator SlideInServerPanel()
	{
		return null;
	}

	private void MoveUIPosition()
	{
	}

	public static void DeselectServerButton()
	{
	}
}
