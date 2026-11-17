using System;
using UnityEngine;

namespace AirFishLab.ScrollingList.MovementCtrl
{
	public class FreeMovementCtrl : IMovementCtrl
	{
		private readonly VelocityMovementCurve _releasingMovementCurve;

		private readonly DistanceMovementCurve _aligningMovementCurve;

		private bool _isDragging;

		private float _draggingDistance;

		private readonly bool _toAlign;

		private float _overGoingDistance;

		private readonly float _overGoingDistanceThreshold;

		private const float _stopVelocityThreshold = 200f;

		private readonly Func<float> _getAligningDistance;

		private readonly Func<ListPositionCtrl.PositionState> _getPositionState;

		public FreeMovementCtrl(AnimationCurve releasingCurve, bool toAlign, float overGoingDistanceThreshold, Func<float> getAligningDistance, Func<ListPositionCtrl.PositionState> getPositionState)
		{
		}

		public void SetMovement(float value, bool isDragging)
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

		private bool NeedToAlign(float distance)
		{
			return false;
		}

		private bool IsGoingTooFar(float distance)
		{
			return false;
		}
	}
}
