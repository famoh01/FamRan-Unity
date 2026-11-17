using System;
using System.Collections.Generic;
using Models.Library.Shared.Game;
using UnityEngine;
using AnimationInfo = Models.Library.Shared.Game.AnimationInfo;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public struct PlayerConstant
	{
		[SerializeField]
		private float _walkVelocity;

		[SerializeField]
		private float _runVelocity;

		[SerializeField]
		private float _HP_Str;

		[SerializeField]
		private float _MP_Spi;

		[SerializeField]
		private float _SP_Sta;

		[SerializeField]
		private float _hit_Dex;

		[SerializeField]
		private float _avoid_Dex;

		[SerializeField]
		private float _defense_Dex;

		[SerializeField]
		private float _PA_Pow;

		[SerializeField]
		private float _PA_Dex;

		[SerializeField]
		private float _SA_Pow;

		[SerializeField]
		private float _SA_Dex;

		[SerializeField]
		private float _MA_Dex;

		[SerializeField]
		private float _MA_Spi;

		[SerializeField]
		private float _MA_Int;

		[SerializeField]
		private float _conv_AP;

		[SerializeField]
		private float _conv_DP;

		[SerializeField]
		private float _conv_PA;

		[SerializeField]
		private float _conv_SA;

		[SerializeField]
		private float _Begin_AP;

		[SerializeField]
		private float _Begin_DP;

		[SerializeField]
		private float _Begin_PA;

		[SerializeField]
		private float _Begin_SA;

		[SerializeField]
		private float _LevelUp_AP;

		[SerializeField]
		private float _LevelUp_DP;

		[SerializeField]
		private float _LevelUp_PA;

		[SerializeField]
		private float _LevelUp_SA;

		[SerializeField]
		private CharStats _Begin_Stats;

		[SerializeField]
		private CharFloatStats _LevelUp_Stats;

		public List<AnimationInfo> Animations;

		public float WalkVelocity
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float RunVelocity
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float HP_Str
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float MP_Spi
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float SP_Sta
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float Hit_Dex
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float Avoid_Dex
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float Defense_Dex
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float PA_Pow
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float PA_Dex
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float SA_Pow
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float SA_Dex
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float MA_Dex
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float MA_Spi
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float MA_Int
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float Conv_AP
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float Conv_DP
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float Conv_PA
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float Conv_SA
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float Begin_AP
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float Begin_DP
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float Begin_PA
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float Begin_SA
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float LevelUp_AP
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float LevelUp_DP
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float LevelUp_PA
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float LevelUp_SA
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public CharStats Begin_Stats
		{
			get
			{
				return default;
			}
			set
			{
			}
		}

		public CharFloatStats LevelUp_Stats
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
