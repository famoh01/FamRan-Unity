using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CLoadSceneAsync_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int sceneID;

		public SceneLoader _003C_003E4__this;

		private AsyncOperation _003Coperation_003E5__2;

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
		public _003CLoadSceneAsync_003Ed__6(int _003C_003E1__state)
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

	public GameObject loadingScreen;

	public Image loadingImage;

	public Image loadingBarFill;

	public Sprite[] loadingSprite;

	private void Start()
	{
	}

	public void LoadScene(int sceneID)
	{
	}

	[IteratorStateMachine(typeof(_003CLoadSceneAsync_003Ed__6))]
	private IEnumerator LoadSceneAsync(int sceneID)
	{
		return null;
	}
}
