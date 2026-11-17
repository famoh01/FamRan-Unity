using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Models.Library.Shared.Enums;
using Models.Library.Shared.Game;
using Models.Network.Shared.Response;
using RAN.Mobile.GameModels;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CRestoreSnapshotRoutine_003Ed__51 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PartyManager _003C_003E4__this;

		private float _003Ct_003E5__2;

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
		public _003CRestoreSnapshotRoutine_003Ed__51(int _003C_003E1__state)
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

	public static PartyManager Instance;

	[Header("Party Ask Canvas")]
	public GameObject _partyAskCanvas;

	public GameObject _partyItemDropdown;

	public GameObject _partyCoinItemDropdown;

	public GameObject _partyQuestion;

	public GameObject _partyOkayBtn;

	public GameObject _partyCancelBtn;

	public int _partyMaxMember;

	private List<PartyMemberInfo> _partyMembers;

	private Unit _targetUnit;

	private int? _partyId;

	private string _pendingInviterName;

	private ushort _pendingInviterGaeaId;

	private PartyOption _pendingOptions;

	private static readonly PartyItemOption[] _itemOptByIndex;

	private static readonly PartyMoneyOption[] _moneyOptByIndex;

	private static List<PartyMemberInfo> _sessionSnapshot;

	private static int _snapshotPartyId;

	private static uint _snapshotCharId;

	public static bool RestorePartyAfterReconnect;

	private const float RestoreReadyTimeoutSeconds = 20f;

	private bool _restoreRunning;

	public int MemberCount => 0;

	public void Awake()
	{
	}

	private void Start()
	{
	}

	private void PopulateOptionDropdowns()
	{
	}

	public static PartyItemOption ItemOptionFromDropdown(int index)
	{
		return PartyItemOption.Default;
	}

	public static PartyMoneyOption MoneyOptionFromDropdown(int index)
	{
		return PartyMoneyOption.Default;
	}

	public static int DropdownIndexFromItemOption(PartyItemOption opt)
	{
		return 0;
	}

	public static int DropdownIndexFromMoneyOption(PartyMoneyOption opt)
	{
		return 0;
	}

	public PartyMemberInfo GetPartyLeader()
	{
		return default;
	}

	public List<PartyMemberInfo> GetPartyMembers()
	{
		return null;
	}

	public void SetTarget(Unit targetUnit)
	{
	}

	public Unit GetTarget()
	{
		return null;
	}

	public string GetPartyItem()
	{
		return null;
	}

	public string GetCoinItem()
	{
		return null;
	}

	public bool InParty(int playerId)
	{
		return false;
	}

	public bool InSameParty(int player1, int player2)
	{
		return false;
	}

	public int GetMemberPlayerId(int index)
	{
		return 0;
	}

	public int? GetCurrentPlayerPartyId()
	{
		return null;
	}

	public bool CanInviteTarget(string targetCharName)
	{
		return false;
	}

	public void AddPartyMember(PartyMemberInfo info)
	{
	}

	public void RemovePartyMember(int removeMember)
	{
	}

	public void DisbandParty(bool showNotification = true)
	{
	}

	public void EnableDropdown(bool enable)
	{
	}

	public void ResetSessionState()
	{
	}

	private static uint CurrentSelectedCharId()
	{
		return 0u;
	}

	private void CaptureSessionSnapshot()
	{
	}

	public static void ClearSessionSnapshot()
	{
	}

	public void RequestSnapshotRestore()
	{
	}

	[IteratorStateMachine(typeof(_003CRestoreSnapshotRoutine_003Ed__51))]
	private IEnumerator RestoreSnapshotRoutine()
	{
		return null;
	}

	private static bool RestoreTargetsReady()
	{
		return false;
	}

	private void PublishPartyState(uint partyId)
	{
	}

	public void HandlePartyLureFeedback(A2C_PartyLureFeedback msg)
	{
	}

	public void HandlePartyLureTarget(A2C_PartyLureTarget msg)
	{
	}

	public void AcceptPartyInvite()
	{
	}

	public void DenyPartyInvite()
	{
	}

	private void RefreshMinimapIcons()
	{
	}

	public void HandlePartyNew(F2C_PartyNew msg)
	{
	}

	public void HandlePartyAdd(F2C_PartyAdd msg)
	{
	}

	public void HandlePartyDel(F2C_PartyDel msg)
	{
	}

	public void HandlePartyDissolve(F2C_PartyDissolve msg)
	{
	}

	public void HandlePartyAuthority(F2C_PartyAuthority msg)
	{
	}

	public void HandlePartyMasterRenew(F2C_PartyMasterRenew msg)
	{
	}

	public void HandlePartyMbrPoint(F2C_PartyMbrPoint msg)
	{
	}

	public void HandlePartyItemPickup(F2C_PartyItemPickupBrd msg)
	{
	}

	public void HandlePartyMbrPos(F2C_PartyMbrPos msg)
	{
	}
}
