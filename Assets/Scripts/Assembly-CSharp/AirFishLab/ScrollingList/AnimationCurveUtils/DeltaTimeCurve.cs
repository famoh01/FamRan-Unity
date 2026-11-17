using UnityEngine;

namespace AirFishLab.ScrollingList.AnimationCurveUtils
{
	public class DeltaTimeCurve
	{
		private readonly AnimationCurve _curve;

		private float _timePassed;

		public float timeTotal { get; }

		public DeltaTimeCurve(AnimationCurve curve)
		{
		}

		public void Reset()
		{
		}

		public bool IsTimeOut()
		{
			return false;
		}

		public float Evaluate(float deltaTime)
		{
			return 0f;
		}

		public float CurrentEvaluate()
		{
			return 0f;
		}
	}
}
