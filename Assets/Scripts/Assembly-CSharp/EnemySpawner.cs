using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CSpawnAfterSeconds_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int seconds;

		public EnemySpawner _003C_003E4__this;

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
		public _003CSpawnAfterSeconds_003Ed__6(int _003C_003E1__state)
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

	public GameObject newEnemy;

	public Transform spawnPoints;

	public int respawnTime;

	public bool someoneIsDead;

	private void Update()
	{
	}

	private void Spawn()
	{
	}

	[IteratorStateMachine(typeof(_003CSpawnAfterSeconds_003Ed__6))]
	private IEnumerator SpawnAfterSeconds(int seconds)
	{
		return null;
	}
}
