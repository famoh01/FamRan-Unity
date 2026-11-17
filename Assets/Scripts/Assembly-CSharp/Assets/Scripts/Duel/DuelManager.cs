using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Models.Network.Shared.Response;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Duel
{
	public class DuelManager : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CPendingRequestTimeout_003Ed__34 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public DuelManager _003C_003E4__this;

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
			public _003CPendingRequestTimeout_003Ed__34(int _003C_003E1__state)
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

		public static DuelManager instance;

		[Header("Duel Trigger Button")]
		public GameObject Duel;

		[Header("Duel Panel")]
		public GameObject Canvas;

		public TextMeshProUGUI QuestionText;

		public GameObject RecoveryDoes;

		public GameObject PlayerHpRate;

		public GameObject EnemyHpRate;

		public Toggle RestrictedRange;

		public GameObject OkayDuel;

		public GameObject CancelDuel;

		[Header("Duel Countdown")]
		public GameObject DuelCountdown;

		[Header("Duel OutCome")]
		public GameObject DuelOutcomeWin;

		public GameObject DuelOutcomeLose;

		public GameObject DuelOutcomeDraw;

		private Button _duelButton;

		private Canvas _canvas;

		private TMP_Dropdown _recoveryDropdown;

		private TMP_Dropdown _playerHpDropdown;

		private TMP_Dropdown _enemyHpDropdown;

		private Button _okButton;

		private Button _cancelButton;

		private DuelCountdown _countdownScript;

		private bool _hasPendingRequest;

		private Coroutine _pendingTimeoutCo;

		private const float PendingRequestTimeoutSeconds = 30f;

		private Vector3 DuelCenterPosition;

		private ushort _pendingRequesterGaeaId;

		private void Awake()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public void CheckPlayerCanBeDuel()
		{
		}

		public void ShowDuelCanvasRequest(bool createRequest)
		{
		}

		public void ShowDuelCanvasRequest()
		{
		}

		private void SendDuelRequest(ushort targetGaeaId)
		{
		}

		[IteratorStateMachine(typeof(_003CPendingRequestTimeout_003Ed__34))]
		private IEnumerator PendingRequestTimeout()
		{
			return null;
		}

		private void OnConfrontForward(F2C_ConfrontForward msg)
		{
		}

		private void AcceptDuel()
		{
		}

		private void DeclineDuel()
		{
		}

		private void OnConfrontFeedback(F2C_ConfrontFeedback msg)
		{
		}

		private void OnConfrontStart(F2C_ConfrontStart msg)
		{
		}

		private void OnConfrontFight()
		{
		}

		public void StartDuel()
		{
		}

		private void OnConfrontEnd(F2C_ConfrontEnd msg, bool isParticipant)
		{
		}

		private void PlayConfrontAnim(ushort gaeaId, ushort myGaeaId, string anim)
		{
		}

		private void ShowDuelOutcomeLabel(bool isWin, bool isDraw)
		{
		}

		private void HideDuelOutcomeLabel()
		{
		}

		private void ResetDuel()
		{
		}

		private void ResetBtnListener()
		{
		}

		private void PlayAudio(string audioName)
		{
		}

		public bool CanAttackTarget(ushort targetGaeaId)
		{
			return false;
		}

		public bool IsInActiveDuel()
		{
			return false;
		}
	}
}
