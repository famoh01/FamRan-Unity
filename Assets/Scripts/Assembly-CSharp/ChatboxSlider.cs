using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatboxSlider : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CSnapToLatest_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ChatboxSlider _003C_003E4__this;

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
		public _003CSnapToLatest_003Ed__14(int _003C_003E1__state)
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

	[Header("UI Elements To Toggle")]
	[Tooltip("The main chatbox panel that will be shown/hidden. Its GameObject is what gets toggled — the RectTransform is never moved, so its position in the prefab is simply where it lives when visible.")]
	public RectTransform chatBox;

	[Header("Button Visuals")]
	[Tooltip("The text component inside your static toggle button.")]
	public TextMeshProUGUI toggleButtonText;

	public string showChatText;

	public string hideChatText;

	[Header("Settings")]
	public bool startOpen;

	private bool isOpen;

	private ScrollRect scrollRect;

	private Coroutine snapRoutine;

	private void Start()
	{
	}

	public void ToggleChat()
	{
	}

	public void Show()
	{
	}

	public void Hide()
	{
	}

	public void SetOpen(bool open)
	{
	}

	private void ApplyState()
	{
	}

	[IteratorStateMachine(typeof(_003CSnapToLatest_003Ed__14))]
	private IEnumerator SnapToLatest()
	{
		return null;
	}

	private void UpdateTextLabel()
	{
	}
}
