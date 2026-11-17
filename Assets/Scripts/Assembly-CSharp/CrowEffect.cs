using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CrowEffect : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CSpawnSkillArrowWithDelay_003Ed__32 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float spawnDelay;

		public CrowEffect _003C_003E4__this;

		public GameObject arrowEffect;

		public Vector3 target;

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
		public _003CSpawnSkillArrowWithDelay_003Ed__32(int _003C_003E1__state)
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

	[Header("Crow Parts")]
	public Transform head;

	public Transform body;

	public Transform leftHand;

	public Transform rightHand;

	public Transform leftWeapon;

	public Transform rightWeapon;

	public Transform leftFoot;

	public Transform rightFoot;

	public Transform arrow01;

	[Space]
	[Header("On Awake Effect")]
	public ParticleSystem headAwake;

	public ParticleSystem bodyAwake;

	public ParticleSystem leftHandAwake;

	public ParticleSystem rightHandAwake;

	public ParticleSystem leftWeaponAwake;

	public ParticleSystem rightWeaponAwake;

	public ParticleSystem leftFootAwake;

	public ParticleSystem rightFootAwake;

	[Space]
	[Space]
	[Header("On Attack Effect")]
	public ParticleSystem headEffect;

	public ParticleSystem bodyEffect;

	public ParticleSystem leftHandEffect;

	public ParticleSystem rightHandEffect;

	public ParticleSystem leftWeaponEffect;

	public ParticleSystem rightWeaponEffect;

	public ParticleSystem leftFootEffect;

	public ParticleSystem rightFootEffect;

	private const float StuckArrowLifetimeSec = 5f;

	private const float SkillArrowSpeed = 300f;

	private readonly List<GameObject> _stuckArrows;

	public void InsertArrow(Transform direction, GameObject arrowPrefab)
	{
	}

	private void OnDisable()
	{
	}

	public void ClearStuckArrows()
	{
	}

	public void CrowShootTrail(GameObject arrowEffect, Vector3 target)
	{
	}

	[IteratorStateMachine(typeof(_003CSpawnSkillArrowWithDelay_003Ed__32))]
	public IEnumerator SpawnSkillArrowWithDelay(GameObject arrowEffect, Vector3 target, float spawnDelay)
	{
		return null;
	}
}
