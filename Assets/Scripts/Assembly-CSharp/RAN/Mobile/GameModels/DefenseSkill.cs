using System;
using UnityEngine;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public struct DefenseSkill
	{
		[SerializeField]
		private SNativeID skillID;

		[SerializeField]
		private ushort level;

		[SerializeField]
		private float fRate;

		[SerializeField]
		private bool isActive;

		public SNativeID SkillID
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public ushort Level
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public float Rate
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public bool IsActive
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public void Reset()
		{
		}
	}
}
