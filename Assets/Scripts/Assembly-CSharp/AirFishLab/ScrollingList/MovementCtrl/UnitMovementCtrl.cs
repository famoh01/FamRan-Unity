using System;
using UnityEngine;

namespace AirFishLab.ScrollingList.MovementCtrl
{
	public class UnitMovementCtrl : IMovementCtrl
	{
		private readonly DistanceMovementCurve _unitMovementCurve;

		private readonly DistanceMovementCurve _bouncingMovementCurve;

		private readonly float _bouncingDeltaPos;

		private readonly Func<float> _getAligningDistance;

		private readonly Func<ListPositionCtrl.PositionState> _getPositionState;

		public UnitMovementCtrl(AnimationCurve movementCurve, float bouncingDeltaPos, Func<float> getAligningDistance, Func<ListPositionCtrl.PositionState> getPositionState)
		{
		}

		public void SetMovement(float distanceAdded, bool flag)
		{
		}

		public void SetSelectionMovement(float distance)
		{
		}

		public bool IsMovementEnded()
		{
			return false;
		}

		public float GetDistance(float deltaTime)
		{
			return 0f;
		}

		private bool NeedToAlign(float deltaDistance)
		{
			return false;
		}
	}
}
