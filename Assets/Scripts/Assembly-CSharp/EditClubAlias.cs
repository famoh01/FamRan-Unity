using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditClubAlias : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDeselectNextFrameCoro_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

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
		public _003CDeselectNextFrameCoro_003Ed__12(int _003C_003E1__state)
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

	[Header("UI Elements")]
	[SerializeField]
	private TMP_Text readOnlyText;

	[SerializeField]
	private TMP_InputField editField;

	[SerializeField]
	private Button editButton;

	private string originalText;

	private bool commitRequested;

	private void Awake()
	{
	}

	private void BeginEdit()
	{
	}

	private void OnSubmit(string _)
	{
	}

	private void OnEndEdit(string _)
	{
	}

	private void Commit()
	{
	}

	private void Cancel()
	{
	}

	private void DeselectNextFrame()
	{
	}

	[IteratorStateMachine(typeof(_003CDeselectNextFrameCoro_003Ed__12))]
	private static IEnumerator DeselectNextFrameCoro()
	{
		return null;
	}

	private void ToggleEditing(bool editing)
	{
	}

	private void ForceLTR()
	{
	}
}
