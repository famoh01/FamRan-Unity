using System;
using Core.Enumerators;
using Models.Library.Shared.Game;
using RAN.Mobile.GameModels;
using UnityEngine;
using AnimationInfo = Models.Library.Shared.Game.AnimationInfo;

namespace Assets.Scripts.Mob_Scripts
{
	[Serializable]
	public class CrowAttack
	{
		[SerializeField]
		private bool isUse;

		[SerializeField]
		private RangeType rangeType;

		[SerializeField]
		private ushort attackRange;

		[SerializeField]
		private RAN.Mobile.GameModels.PAData damage;

		[SerializeField]
		private float delay;

		[SerializeField]
		private ushort spUsage;

		[SerializeField]
		private AnimationInfo _animationInfo;

		[SerializeField]
		private RAN.Mobile.GameModels.SNativeID skillId;

		[SerializeField]
		private SkillTargetEnum skillTarget;

		public bool IsUse
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public RangeType RangeType
		{
			get
			{
				return RangeType.ShortRange;
			}
			set
			{
			}
		}

		public ushort AttackRange
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public RAN.Mobile.GameModels.PAData Damage
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public float Delay
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public ushort SpUsage
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public RAN.Mobile.GameModels.SNativeID SkillId
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public SkillTargetEnum SkillTarget
		{
			get
			{
				return SkillTargetEnum.TAR_SELF;
			}
			set
			{
			}
		}

		public AnimationInfo AnimationInfo
		{
			get
			{
				return default;
			}
			set
			{
			}
		}
	}
}
