using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	[RequireComponent(typeof(AeroplaneController))]
	public class AeroplaneUserControl4Axis : MonoBehaviour
	{
		public float maxRollAngle;

		public float maxPitchAngle;

		private AeroplaneController m_Aeroplane;

		private float m_Throttle;

		private bool m_AirBrakes;

		private float m_Yaw;

		private void Awake()
		{
		}

		private void FixedUpdate()
		{
		}

		private void AdjustInputForMobileControls(ref float roll, ref float pitch, ref float throttle)
		{
		}
	}
}
