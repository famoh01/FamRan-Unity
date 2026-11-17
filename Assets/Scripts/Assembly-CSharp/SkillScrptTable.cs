using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Models.Library.Shared.Game;
using Newtonsoft.Json;
using UnityEngine;

public class SkillScrptTable : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CBuildAsync_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SkillScrptTable _003C_003E4__this;

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
		public _003CBuildAsync_003Ed__9(int _003C_003E1__state)
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
	private sealed class _003CBuildLookupAndLoadCoroutine_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SkillScrptTable _003C_003E4__this;

		private SkillObject[] _003Clist_003E5__2;

		private GameSkillDataHandler _003Chandler_003E5__3;

		private JsonSerializer _003Cserializer_003E5__4;

		private int _003Ci_003E5__5;

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
		public _003CBuildLookupAndLoadCoroutine_003Ed__11(int _003C_003E1__state)
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

	public SkillDatabaseObject database;

	public static SkillScrptTable instance;

	private Dictionary<(int mainId, int subId), SkillObject> _skillLookup;

	private const int LoadBatchSize = 256;

	public bool IsDataReady { get; private set; }

	private void Awake()
	{
	}

	[IteratorStateMachine(typeof(_003CBuildAsync_003Ed__9))]
	private IEnumerator BuildAsync()
	{
		return null;
	}

	private void InitializeDatabase()
	{
	}

	[IteratorStateMachine(typeof(_003CBuildLookupAndLoadCoroutine_003Ed__11))]
	private IEnumerator BuildLookupAndLoadCoroutine()
	{
		return null;
	}

	public SkillObject GetSkillObject(int midId, int subId)
	{
		return null;
	}
}
