using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Models.Library.Shared.Game;
using UnityEngine;

public class ItemScptTable : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CBuildAsync_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ItemScptTable _003C_003E4__this;

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
		public _003CBuildAsync_003Ed__10(int _003C_003E1__state)
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
	private sealed class _003CBuildLookupAndLoadCoroutine_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ItemScptTable _003C_003E4__this;

		private ItemObject[] _003Clist_003E5__2;

		private GameItemDataHandler _003Chandler_003E5__3;

		private int _003Ci_003E5__4;

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
		public _003CBuildLookupAndLoadCoroutine_003Ed__12(int _003C_003E1__state)
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

	public ItemDatabaseObject database;

	public static ItemScptTable instance;

	private Dictionary<(int mainId, int subId), ItemObject> _itemLookup;

	private Dictionary<(int mainId, int subId), (int mainId, int subId)> _scrollSkillLookup;

	private const int LoadBatchSize = 256;

	public bool IsDataReady { get; private set; }

	private void Awake()
	{
	}

	[IteratorStateMachine(typeof(_003CBuildAsync_003Ed__10))]
	private IEnumerator BuildAsync()
	{
		return null;
	}

	private void InitializeDatabase()
	{
	}

	[IteratorStateMachine(typeof(_003CBuildLookupAndLoadCoroutine_003Ed__12))]
	private IEnumerator BuildLookupAndLoadCoroutine()
	{
		return null;
	}

	public ItemObject GetItemObject(int midId, int subId)
	{
		return null;
	}

	public List<ItemObject> GetItemObject(string name)
	{
		return null;
	}

	public bool TryGetSkillForScroll(int scrollMainId, int scrollSubId, out (int mainId, int subId) skillId)
	{
		skillId = default;
		return false;
	}

	public static bool TryGetScrollSkill(ItemObject item, out (ushort mainId, ushort subId) skillId)
	{
		skillId = default;
		return false;
	}
}
