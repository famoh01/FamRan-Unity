using UnityEngine;

namespace XftWeapon
{
	public class SplineControlPoint
	{
		public Vector3 Position;

		public Vector3 Normal;

		public int ControlPointIndex;

		public int SegmentIndex;

		public float Dist;

		protected Spline mSpline;

		public SplineControlPoint NextControlPoint => null;

		public SplineControlPoint PreviousControlPoint => null;

		public Vector3 NextPosition => default;

		public Vector3 PreviousPosition => default;

		public Vector3 NextNormal => default;

		public Vector3 PreviousNormal => default;

		public bool IsValid => false;

		private Vector3 GetNext2Position()
		{
			return default;
		}

		private Vector3 GetNext2Normal()
		{
			return default;
		}

		public Vector3 Interpolate(float localF)
		{
			return default;
		}

		public Vector3 InterpolateNormal(float localF)
		{
			return default;
		}

		public void Init(Spline owner)
		{
		}
	}
}
