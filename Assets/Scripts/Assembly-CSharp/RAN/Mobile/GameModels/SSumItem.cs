using System;
using UnityEngine;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class SSumItem
	{
		[SerializeField]
		private PAData _damage;

		[SerializeField]
		private int _defense;

		[SerializeField]
		private int _avoidRate;

		[SerializeField]
		private int _hitRate;

		[SerializeField]
		private int _hp;

		[SerializeField]
		private int _mp;

		[SerializeField]
		private int _sp;

		[SerializeField]
		private float _increaseRateHP;

		[SerializeField]
		private float _increaseRateMP;

		[SerializeField]
		private float _increaseRateSP;

		[SerializeField]
		private float _increaseHP;

		[SerializeField]
		private float _increaseMP;

		[SerializeField]
		private float _increaseSP;

		[SerializeField]
		private CharStats _stats;

		[SerializeField]
		private Resistance _resistance;

		[SerializeField]
		private int _pa;

		[SerializeField]
		private int _sa;

		[SerializeField]
		private int _ma;

		[SerializeField]
		private float _increaseRateMoveSpeed;

		[SerializeField]
		private float _increaseRateAttackSpeed;

		[SerializeField]
		private float _increaseRateCritical;

		[SerializeField]
		private float _increaseRateCrushingBlow;

		[SerializeField]
		private float _increaseMoveSpeed;

		[SerializeField]
		private float _increaseAttackSpeed;

		[SerializeField]
		private float _increaseCritical;

		[SerializeField]
		private float _increaseCrushingBlow;

		[SerializeField]
		private int _hpPotionRate;

		[SerializeField]
		private int _mpPotionRate;

		[SerializeField]
		private int _spPotionRate;

		[SerializeField]
		private float _rateHitPer;

		[SerializeField]
		private float _rateAvoidPer;

		public PAData Damage
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public int Defense
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int AvoidRate
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int HitRate
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int HP
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int MP
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int SP
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public float IncreaseRateHP
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float IncreaseRateMP
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float IncreaseRateSP
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float IncreaseHP
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float IncreaseMP
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float IncreaseSP
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public CharStats Stats
		{
			get
			{
				return default;
			}
			set
			{
			}
		}

		public Resistance Resistance
		{
			get
			{
				return default;
			}
			set
			{
			}
		}

		public int PA
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int SA
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int MA
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public float IncreaseRateMoveSpeed
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float IncreaseRateAttackSpeed
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float IncreaseRateCritical
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float IncreaseRateCrushingBlow
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float IncreaseMoveSpeed
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float IncreaseAttackSpeed
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float IncreaseCritical
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float IncreaseCrushingBlow
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public int HPPotionRate
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int MPPotionRate
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int SPPotionRate
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public float RateHitPer
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float RateAvoidPer
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}
	}
}
