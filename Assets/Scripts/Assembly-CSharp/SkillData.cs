using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Assets.Scripts;
using Assets.Scripts.Enums;
using Core.Enumerators;
using Models.Library.Shared.DTO;
using Models.Library.Shared.Game;
using UnityEngine;
using UnityEngine.UI;

public class SkillData : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDestoryOnCast_003Ed__68 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float time;

		public SkillData _003C_003E4__this;

		public GameObject objet;

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
		public _003CDestoryOnCast_003Ed__68(int _003C_003E1__state)
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
	private sealed class _003CGetCurrentAnimation_003Ed__43 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public SkillData _003C_003E4__this;

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
		public _003CGetCurrentAnimation_003Ed__43(int _003C_003E1__state)
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
	private sealed class _003CInstantiateWithDelay_003Ed__42 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public GameObject effect;

		public Vector3 position;

		public Quaternion rotation;

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
		public _003CInstantiateWithDelay_003Ed__42(int _003C_003E1__state)
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
	private sealed class _003CStartBuffTimer_003Ed__40 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Image durationFillImage;

		public float duration;

		public GameObject buffIndicatorObj;

		private float _003CelapsedTime_003E5__2;

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
		public _003CStartBuffTimer_003Ed__40(int _003C_003E1__state)
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

	private Animator _animator;

	public SkillSlot skillSlot;

	private bool _toMove;

	private SkillSlot selectedSkillSlot;

	private Button[] skillButton;

	private static List<TargetDistance> targetListTemp;

	private GameObject defenseEffect;

	private string KeyAnimation;

	private Button tempButton;

	private float skillCooldown;

	private float lastHpVal;

	[Header("Buff Indicator")]
	[SerializeField]
	private GameObject buffIndicatorPrefab;

	[SerializeField]
	private Transform buffIndicatorParent;

	private GameObject castedEnemyEffectsMB;

	private GameObject castedEnemyEffectsFL;

	private GameObject DcastedEnemyEffectsMB;

	private GameObject DcastedEnemyEffectsFL;

	private Dictionary<Vector2, GameObject> previousInstantiatedEffects;

	private Dictionary<Vector2, GameObject> previousInstantiatedIcon;

	private Vector3 _lastPathedTarget;

	private const float MinRepathDistanceSqr = 0.25f;

	[Header("Projectile")]
	public float arrowOffset;

	[Header("Boolean")]
	private bool isCasted;

	public bool willCastSkill;

	[Header("Skill Anim")]
	private float animClipLength;

	private string animClipName;

	public float animationStartTime;

	public bool startAnimTime;

	private FixedJoystick _joystick;

	public bool toMove
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	private void Start()
	{
	}

	public void InitSkill()
	{
	}

	public void FixedUpdate()
	{
	}

	public void DestroyWhenRepeat(Vector2 skillID, int i, Transform effectContainer)
	{
	}

	public void DestroyIconWhenRepeat(Vector2 skillID)
	{
	}

	public GameObject PlayerEffectInstantiator(int effectIndex, Transform effectContainer)
	{
		return null;
	}

	public GameObject PlayerEffectInstantiator(int effectIndex, Vector3 position, Quaternion rotation)
	{
		return null;
	}

	public GameObject SpawnBuffIndicator(SkillSlot skill)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CStartBuffTimer_003Ed__40))]
	private IEnumerator StartBuffTimer(float duration, Image durationFillImage, GameObject buffIndicatorObj)
	{
		return null;
	}

	public void EnemyEffectInstantiator(GameObject effectObj, bool effectCasted, int effectIndex, Transform effectContainer)
	{
	}

	[IteratorStateMachine(typeof(_003CInstantiateWithDelay_003Ed__42))]
	private IEnumerator InstantiateWithDelay(GameObject effect, Vector3 position, Quaternion rotation, float delay)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CGetCurrentAnimation_003Ed__43))]
	private IEnumerator GetCurrentAnimation(float delay)
	{
		return null;
	}

	public void OnEnable()
	{
	}

	public void Initiallization(SkillSlot skill)
	{
	}

	internal static bool IsResurrectionSkill(SkillObject skill)
	{
		return false;
	}

	public PlayerAttackSkillActionEnum DoCastSkill(SkillSlot skill)
	{
		return PlayerAttackSkillActionEnum.OK;
	}

	private void ShowSkillCheckError(PlayerStatistics message, SkillCheckEnum emSkillCheck, SkillAttackTypeEnum useRightItem)
	{
	}

	private bool HasNavMeshLineOfSight(Vector3 start, Vector3 end)
	{
		return false;
	}

	public PlayerAttackSkillActionEnum DoSkillAction()
	{
		return PlayerAttackSkillActionEnum.OK;
	}

	private static bool IsSelfEnemyTarget(TargetInfoEx targetInfo, in SkillDTO skillData)
	{
		return false;
	}

	public bool SelectSkillTarget(TargetInfoEx targetInfo, Vector3 targetPosition)
	{
		return false;
	}

	private bool SKT_TARZONEANYBODY(TargetInfoEx targetInfo, Vector3 targetPosition, in SkillDTO skillData, ushort level)
	{
		return false;
	}

	private bool SKT_TARZONEENERMY(TargetInfoEx targetInfo, Vector3 targetPosition, in SkillDTO skillData, ushort level)
	{
		return false;
	}

	private bool SKT_TARZONEOUR(TargetInfoEx targetInfo, Vector3 targetPosition, in SkillDTO skillData, ushort level)
	{
		return false;
	}

	private bool SKT_TARSELFTOSPEC(TargetInfoEx targetInfo, Vector3 targetPosition, in SkillDTO skillData, ushort level)
	{
		return false;
	}

	private bool SKT_TARSPECFANWIZE(TargetInfoEx targetInfo, Vector3 targetPosition, in SkillDTO skillData, ushort level)
	{
		return false;
	}

	private bool SKT_TARSPECANYBODY(TargetInfoEx targetInfo, Vector3 targetPosition, in SkillDTO skillData, ushort level)
	{
		return false;
	}

	private bool SKT_TARSPECENERMY(TargetInfoEx targetInfo, Vector3 targetPosition, in SkillDTO skillData, ushort level)
	{
		return false;
	}

	private bool SKT_TARSPECOUR(TargetInfoEx targetInfo, Vector3 targetPosition, in SkillDTO skillData, ushort level)
	{
		return false;
	}

	private bool SKT_TARSPECANYBODY_SELF(TargetInfoEx targetInfo, Vector3 targetPosition, in SkillDTO skillData, ushort level)
	{
		return false;
	}

	private bool SKT_TARSPECENERMY_SELF(TargetInfoEx targetInfo, Vector3 targetPosition, in SkillDTO skillData, ushort level)
	{
		return false;
	}

	private bool SKT_TARSPECOUR_SELF(TargetInfoEx targetInfo, Vector3 targetPosition, in SkillDTO skillData, ushort level)
	{
		return false;
	}

	private bool SKT_SELFZONANYBODY(TargetInfoEx targetInfo, Vector3 targetPosition, in SkillDTO skillData, ushort level)
	{
		return false;
	}

	private bool SKT_SELFZONEENERMY(TargetInfoEx targetInfo, Vector3 targetPosition, in SkillDTO skillData, ushort level)
	{
		return false;
	}

	private bool SKT_SELFZONEOUR(TargetInfoEx targetInfo, Vector3 targetPosition, in SkillDTO skillData, ushort level)
	{
		return false;
	}

	private void AddPartyMembersInRange(PlayerController player, Vector3 center, float rangeSq)
	{
	}

	[IteratorStateMachine(typeof(_003CDestoryOnCast_003Ed__68))]
	public IEnumerator DestoryOnCast(GameObject objet, float time)
	{
		return null;
	}

	public Vector3 UpdateSkillDirection(Vector3 myPosition, Vector3 myDirection, in SNativeID skillID, Vector3 targetPosition, in TargetInfoID[] targetIDs)
	{
		return default;
	}
}
