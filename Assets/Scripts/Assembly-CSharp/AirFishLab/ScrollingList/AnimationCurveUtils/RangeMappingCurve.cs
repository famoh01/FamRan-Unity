using UnityEngine;

namespace AirFishLab.ScrollingList.AnimationCurveUtils
{
	public class RangeMappingCurve
	{
		private readonly AnimationCurve _curve;

		private readonly float _curveXMin;

		private readonly float _curveXMax;

		private readonly float _customXMin;

		private readonly float _customXMax;

		public RangeMappingCurve(AnimationCurve curve, float curveXMin, float curveXMax, float customXMin, float customXMax)
		{
		}

		public float Evaluate(float value)
		{
			return 0f;
		}
	}
}
