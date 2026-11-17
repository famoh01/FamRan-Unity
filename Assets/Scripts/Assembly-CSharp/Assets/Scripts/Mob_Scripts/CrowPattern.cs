using System;
using Core.Enumerators;
using UnityEngine;

namespace Assets.Scripts.Mob_Scripts
{
	[Serializable]
	public class CrowPattern
	{
		[SerializeField]
		private float dnRate;

		[SerializeField]
		private CrowActUp actPattern;

		[SerializeField]
		private ushort attackSet;

		public float DNRate
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public CrowActUp ActPattern
		{
			get
			{
				return CrowActUp.EMCROWACT_UP_IDLE;
			}
			set
			{
			}
		}

		public ushort AttackSet
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}
	}
}
