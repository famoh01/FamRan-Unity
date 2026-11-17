using AirFishLab.ScrollingList.AnimationCurveUtils;
using UnityEngine;

namespace AirFishLab.ScrollingList.MovementCtrl
{
	internal class DistanceMovementCurve
	{
		private readonly DeltaTimeCurve _distanceFactorCurve;

		private float _distanceTotal;

		private float _lastDistance;

		public float distanceRemaining => 0f;

		public DistanceMovementCurve(AnimationCurve factorCurve)
		{
		}

		public void SetMovement(float totalDistance)
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
