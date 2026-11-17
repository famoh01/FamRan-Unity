using System;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class SPassiveSkillData
	{
		public int hP;

		public int mP;

		public int sP;

		public int damage;

		public int defense;

		public int hit;

		public int avoid;

		public int pierce;

		public float targetRange;

		public float moveVelocity;

		public float attackVelocity;

		public float skillDelay;

		public float increaseRateHP;

		public float increaseRateMP;

		public float increaseRateSP;

		public float damageRate;

		public float defenseRate;

		public DamageSpec damageSpec;

		public int pA;

		public int sA;

		public int mA;

		public float hPRate;

		public float mPRate;

		public float sPRate;

		public Resistance sumResist;

		public float sumSkillAttackRange;

		public float sumSkillApplyRange;
	}
}
