using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	public class LandingGear : MonoBehaviour
	{
		private enum GearState
		{
			Raised = -1,
			Lowered = 1
		}

		public float raiseAtAltitude;

		public float lowerAtAltitude;

		private GearState m_State;

		private Animator m_Animator;

		private Rigidbody m_Rigidbody;

		private AeroplaneController m_Plane;

		private void Start()
		{
		}

		private void Update()
		{
		}
	}
}
