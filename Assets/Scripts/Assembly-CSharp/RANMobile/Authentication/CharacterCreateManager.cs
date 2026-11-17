using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace RANMobile.Authentication
{
	public class CharacterCreateManager : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CRemoveAfterSeconds_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public int seconds;

			public GameObject obj;

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
			public _003CRemoveAfterSeconds_003Ed__10(int _003C_003E1__state)
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
		private TMP_InputField _nameField;

		public static CharacterCreateManager Instance { get; private set; }

		private void Awake()
		{
		}

		private void Start()
		{
		}

		public void CreateCharacter()
		{
		}

		public void OpenURL(string url)
		{
		}

		public void FetchResponseStatus(string res)
		{
		}

		[IteratorStateMachine(typeof(_003CRemoveAfterSeconds_003Ed__10))]
		private IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
		{
			return null;
		}

		public void CharacterCreateSuccess()
		{
		}

		public void OpenSettings()
		{
		}
	}
}
