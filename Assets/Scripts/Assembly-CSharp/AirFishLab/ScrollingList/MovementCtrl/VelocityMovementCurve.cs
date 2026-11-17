using AirFishLab.ScrollingList.AnimationCurveUtils;
using UnityEngine;

namespace AirFishLab.ScrollingList.MovementCtrl
{
	internal class VelocityMovementCurve
	{
		private readonly DeltaTimeCurve _velocityFactorCurve;

		private float _baseVelocity;

		public float lastVelocity { get; private set; }

		public VelocityMovementCurve(AnimationCurve factorCurve)
		{
		}

		public void SetMovement(float baseVelocity)
		{
		}

		public bool IsMovementEnded()
		{
			return false;
		}

		public void EndMovement()
		{
		}

		public float GetDistance(float deltaTime)
		{
			return 0f;
		}
	}
}
