using System;
using AirFishLab.ScrollingList.AnimationCurveUtils;
using UnityEngine;

namespace AirFishLab.ScrollingList.BoxTransformCtrl
{
	public class LinearBoxTransformCtrl : IBoxTransformCtrl
	{
		private readonly float _unitPos;

		private readonly float _lowerBoundPos;

		private readonly float _upperBoundPos;

		private readonly float _changeSideLowerBoundPos;

		private readonly float _changeSideUpperBoundPos;

		private readonly RangeMappingCurve _positionCurve;

		private readonly RangeMappingCurve _scaleCurve;

		private readonly Func<Vector2, float> _getMajorFactor;

		private readonly Func<float, float, float, Vector3> _getLocalPosition;

		public LinearBoxTransformCtrl(ListPositionCtrl positionCtrl, AnimationCurve boxPositionCurve, AnimationCurve boxScaleCurve, CircularScrollingList.Direction direction)
		{
		}

		public void SetInitialTransform(Transform boxTransform, int boxID, int numOfBoxes)
		{
		}

		public void SetLocalTransform(Transform boxTransform, float delta, out bool needToUpdateToLastContent, out bool needToUpdateToNextContent)
		{
			needToUpdateToLastContent = default;
			needToUpdateToNextContent = default;
		}

		private Vector3 GetLocalPositionYMajor(float majorPos, float passivePos, float z)
		{
			return default;
		}

		private Vector3 GetLocalPositionXMajor(float majorPos, float passivePos, float z)
		{
			return default;
		}

		private float GetMajorPosition(float positionValue, out bool needToUpdateToLastContent, out bool needToUpdateToNextContent)
		{
			needToUpdateToLastContent = default;
			needToUpdateToNextContent = default;
			return 0f;
		}

		private float GetPassivePosition(float majorPosition)
		{
			return 0f;
		}

		private float GetScaleValue(float majorPosition)
		{
			return 0f;
		}
	}
}
