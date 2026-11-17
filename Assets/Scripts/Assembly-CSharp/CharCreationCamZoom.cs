using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CharCreationCamZoom : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CMoveCamera_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CharCreationCamZoom _003C_003E4__this;

		public Vector3 targetCamPos;

		public Quaternion targetCamRot;

		private Transform _003CcamTransform_003E5__2;

		private Vector3 _003CstartCamPos_003E5__3;

		private Quaternion _003CstartCamRot_003E5__4;

		private float _003Ctime_003E5__5;

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
		public _003CMoveCamera_003Ed__21(int _003C_003E1__state)
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

	public static CharCreationCamZoom instance;

	public Camera cam;

	[Header("Zoom Targets")]
	public Transform defaultViewPos;

	public Transform faceZoomMalePos;

	public Transform faceZoomFemalePos;

	private Vector3 initialPosition;

	private Quaternion initialRotation;

	[Space]
	public bool onMale;

	public bool onFemale;

	[Header("ZoomButtons")]
	public GameObject defaultZoomBtn;

	public GameObject faceZoomBtn;

	public float transitionDuration;

	private Coroutine currentCoroutine;

	public bool isZoomedOnFace;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void StopCameraMove()
	{
	}

	public void SnapToDefaultView()
	{
	}

	public void DefaultZoom()
	{
	}

	public void ZoomOnFace()
	{
	}

	public void UpdateZoomForGender()
	{
	}

	[IteratorStateMachine(typeof(_003CMoveCamera_003Ed__21))]
	private IEnumerator MoveCamera(Vector3 targetCamPos, Quaternion targetCamRot)
	{
		return null;
	}
}
