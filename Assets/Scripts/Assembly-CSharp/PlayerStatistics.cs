using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Core.Enumerators;
using Models.Library.Shared.Game;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CRemoveAfterSeconds_003Ed__67 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int seconds;

		public GameObject obj;

		public PlayerStatistics _003C_003E4__this;

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
		public _003CRemoveAfterSeconds_003Ed__67(int _003C_003E1__state)
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

	private DisplayStats displayStats;

	public UIHUDFunctions UIFunctions;

	[Header("Character Condition")]
	public float maxHealth;

	public float currentHealth;

	public float currentMana;

	public float currentSp;

	public float currentExp;

	public float maxExp;

	public int level;

	private CharacterInformatics characterInfo;

	private uint _cachedHP;

	private int _cachedMP;

	private int _cachedSP;

	private long _cachedExpMin;

	private long _cachedExpMax;

	private ushort _cachedLevel;

	private ushort _cachedPanelLevel;

	private int _cachedForceLow;

	private int _cachedForceHigh;

	private int _cachedDefense;

	private ushort _cachedPA;

	private ushort _cachedSA;

	private ushort _cachedMA;

	private uint _cachedPanelHP;

	private uint _cachedPanelMaxHP;

	private int _cachedHit;

	private int _cachedAvoid;

	private int _cachedPanelMP;

	private int _cachedPanelMaxMP;

	private int _cachedPanelSP;

	private int _cachedPanelMaxSP;

	private short _cachedFire;

	private short _cachedElec;

	private short _cachedWind;

	private short _cachedIce;

	private short _cachedPoison;

	private int _cachedBright;

	private uint _cachedPKScore;

	private uint _cachedPKDeath;

	private uint _cachedPKResu;

	private int _cachedLiving;

	private long _cachedContrib;

	private long _cachedPanelExpMin;

	private long _cachedPanelExpMax;

	private ushort _cachedStatsPoint;

	private SCharStats _cachedSumStats;

	private SCharStats _cachedAddStats;

	private ushort _cachedSchool;

	private uint _cachedRank;

	private CharacterClassEnum _cachedClass;

	private Coroutine messageFeedbackHideRoutine;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void UpdateHUD(GameCharLogic charLogic)
	{
	}

	private void UpdateStatsPanel(GameCharLogic charLogic)
	{
	}

	private void SetPoint(GameCharLogic charLogic)
	{
	}

	private void SetSta(GameCharLogic charLogic)
	{
	}

	private void SetStr(GameCharLogic charLogic)
	{
	}

	private void SetSpi(GameCharLogic charLogic)
	{
	}

	private void SetDex(GameCharLogic charLogic)
	{
	}

	private void SetPow(GameCharLogic charLogic)
	{
	}

	public void InformaticsInitialized()
	{
	}

	private void InvalidateAllCaches()
	{
	}

	public void ReceiveDamage(int damage)
	{
	}

	public void ReceiveDamage(int damage, DamageTypeEnum flag)
	{
	}

	public void MessageFeedback(string message, Color textColor)
	{
	}

	[IteratorStateMachine(typeof(_003CRemoveAfterSeconds_003Ed__67))]
	private IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
	{
		return null;
	}
}
