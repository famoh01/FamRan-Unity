using System;
using Core.Enumerators;
using Models.Library.Shared.Game;
using Newtonsoft.Json;
using UnityEngine;
using AnimationInfo = Models.Library.Shared.Game.AnimationInfo;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class SkillBase : ScriptableObject
	{
		[SerializeField]
		private Models.Library.Shared.Game.SNativeID data;

		[SerializeField]
		private string Name;

		[SerializeField]
		private uint grade;

		[SerializeField]
		private uint maxLevel;

		[SerializeField]
		private ushort targetRange;

		[SerializeField]
		private float delayDamage;

		public float fArrowSpeed;

		[SerializeField]
		[TextArea(5, 5)]
		[JsonIgnore]
		private string description;

		[SerializeField]
		private SkillRoleEnum skillRole;

		[SerializeField]
		private SkillActionEnum skillAction;

		[SerializeField]
		private SkillApplyEnum skillApply;

		[SerializeField]
		private SkillTargetEnum skillTarget;

		[SerializeField]
		private SkillAreaEnum skillArea;

		[SerializeField]
		private SkillSideEnum skillSide;

		[SerializeField]
		private SkillAttackTypeEnum useLeftItem;

		[SerializeField]
		private SkillAttackTypeEnum useRightItem;

		[Header("Animation")]
		public AnimationInfo Animation;

		[SerializeField]
		private string animationKey;

		[Header("Settings")]
		public SkillApply skillApplySettings;

		[Header("Learn Skill Requirements")]
		public SkillLearn skillLearnSettings;

		public float DelayDamage
		{
			get
			{
				return delayDamage;
			}
			set
			{
				delayDamage = value;
			}
		}

		public SkillAttackTypeEnum UseLeftItem
		{
			get
			{
				return useLeftItem;
			}
			set
			{
				useLeftItem = value;
			}
		}

		public SkillAttackTypeEnum UseRightItem
		{
			get
			{
				return useRightItem;
			}
			set
			{
				useRightItem = value;
			}
		}

		public string AnimationKey
		{
			get
			{
				return animationKey;
			}
			set
			{
				animationKey = value;
			}
		}

		public SkillSideEnum SkillSide
		{
			get
			{
				return skillSide;
			}
			set
			{
				skillSide = value;
			}
		}

		public SkillAreaEnum SkillArea
		{
			get
			{
				return skillArea;
			}
			set
			{
				skillArea = value;
			}
		}

		public SkillTargetEnum SkillTarget
		{
			get
			{
				return skillTarget;
			}
			set
			{
				skillTarget = value;
			}
		}

		public SkillApplyEnum SkillApply
		{
			get
			{
				return skillApply;
			}
			set
			{
				skillApply = value;
			}
		}

		public SkillActionEnum SkillAction
		{
			get
			{
				return skillAction;
			}
			set
			{
				skillAction = value;
			}
		}

		public SkillRoleEnum SkillRole
		{
			get
			{
				return skillRole;
			}
			set
			{
				skillRole = value;
			}
		}

		public ushort TargetRange
		{
			get
			{
				return targetRange;
			}
			set
			{
				targetRange = value;
			}
		}

		public uint MaxLevel
		{
			get
			{
				return maxLevel;
			}
			set
			{
				maxLevel = value;
			}
		}

		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;
			}
		}

		public Models.Library.Shared.Game.SNativeID Data
		{
			get
			{
				return data;
			}
			set
			{
				data = value;
			}
		}

		public string SkillName
		{
			get
			{
				return Name;
			}
			set
			{
				Name = value;
			}
		}

		public uint Grade
		{
			get
			{
				return grade;
			}
			set
			{
				grade = value;
			}
		}
	}
}
