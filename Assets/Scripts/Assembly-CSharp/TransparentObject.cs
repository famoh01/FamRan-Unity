using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CFadeToOriginal_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TransparentObject _003C_003E4__this;

		public GameObject obj;

		public Material[] originalMats;

		private Renderer _003Crenderer_003E5__2;

		private float _003CelapsedTime_003E5__3;

		private Material[] _003ClerpedMats_003E5__4;

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
		public _003CFadeToOriginal_003Ed__21(int _003C_003E1__state)
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
	private sealed class _003CFadeToTransparent_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TransparentObject _003C_003E4__this;

		public GameObject obj;

		private Material[] _003ClerpedMats_003E5__2;

		private float _003CelapsedTime_003E5__3;

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
		public _003CFadeToTransparent_003Ed__22(int _003C_003E1__state)
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

	private Dictionary<GameObject, Material[]> originalMaterials;

	private Dictionary<GameObject, Shader[]> originalShaders;

	private Dictionary<GameObject, Material[]> fadeMaterials;

	private HashSet<GameObject> fadedObjects;

	private Dictionary<GameObject, float> objectHitTimers;

	private Dictionary<GameObject, Coroutine> activeFadeCoroutines;

	private Shader cachedFadeShader;

	[Header("Fade Settings")]
	[SerializeField]
	private float opacityValue;

	[SerializeField]
	private float fadeDuration;

	[SerializeField]
	private float fadeActivationTime;

	public Transform player;

	public float maxDistance;

	private bool isFading;

	private void Awake()
	{
	}

	private void FixedUpdate()
	{
	}

	private void OnDestroy()
	{
	}

	private void CleanupAllFadeMaterials()
	{
	}

	private void CleanupFadeMaterial(GameObject obj)
	{
	}

	private void RevertAllFadedObjects()
	{
	}

	private void RevertToOriginalMaterial(GameObject obj)
	{
	}

	private void LowerOpacity(GameObject obj)
	{
	}

	[IteratorStateMachine(typeof(_003CFadeToOriginal_003Ed__21))]
	private IEnumerator FadeToOriginal(GameObject obj, Material[] originalMats)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CFadeToTransparent_003Ed__22))]
	private IEnumerator FadeToTransparent(GameObject obj)
	{
		return null;
	}

	private void DisableChildObjects(GameObject obj)
	{
	}

	private void EnableChildObjects(GameObject obj)
	{
	}
}
