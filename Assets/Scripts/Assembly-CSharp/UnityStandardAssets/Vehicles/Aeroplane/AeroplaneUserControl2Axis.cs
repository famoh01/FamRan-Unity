using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	[RequireComponent(typeof(AeroplaneController))]
	public class AeroplaneUserControl2Axis : MonoBehaviour
	{
		public float maxRollAngle;

		public float maxPitchAngle;

		private AeroplaneController m_Aeroplane;

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
