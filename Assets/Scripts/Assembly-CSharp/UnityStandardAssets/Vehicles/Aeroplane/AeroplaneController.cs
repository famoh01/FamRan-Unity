using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	[RequireComponent(typeof(Rigidbody))]
	public class AeroplaneController : MonoBehaviour
	{
		[SerializeField]
		private float m_MaxEnginePower;

		[SerializeField]
		private float m_Lift;

		[SerializeField]
		private float m_ZeroLiftSpeed;

		[SerializeField]
		private float m_RollEffect;

		[SerializeField]
		private float m_PitchEffect;

		[SerializeField]
		private float m_YawEffect;

		[SerializeField]
		private float m_BankedTurnEffect;

		[SerializeField]
		private float m_AerodynamicEffect;

		[SerializeField]
		private float m_AutoTurnPitch;

		[SerializeField]
		private float m_AutoRollLevel;

		[SerializeField]
		private float m_AutoPitchLevel;

		[SerializeField]
		private float m_AirBrakesEffect;

		[SerializeField]
		private float m_ThrottleChangeSpeed;

		[SerializeField]
		private float m_DragIncreaseFactor;

		private float m_OriginalDrag;

		private float m_OriginalAngularDrag;

		private float m_AeroFactor;

		private bool m_Immobilized;

		private float m_BankedTurnAmount;

		private Rigidbody m_Rigidbody;

		private WheelCollider[] m_WheelColliders;

		public float Altitude { get; private set; }

		public float Throttle { get; private set; }

		public bool AirBrakes { get; private set; }

		public float ForwardSpeed { get; private set; }

		public float EnginePower { get; private set; }

		public float MaxEnginePower => 0f;

		public float RollAngle { get; private set; }

		public float PitchAngle { get; private set; }

		public float RollInput { get; private set; }

		public float PitchInput { get; private set; }

		public float YawInput { get; private set; }

		public float ThrottleInput { get; private set; }

		private void Start()
		{
		}

		public void Move(float rollInput, float pitchInput, float yawInput, float throttleInput, bool airBrakes)
		{
		}

		private void ClampInputs()
		{
		}

		private void CalculateRollAndPitchAngles()
		{
		}

		private void AutoLevel()
		{
		}

		private void CalculateForwardSpeed()
		{
		}

		private void ControlThrottle()
		{
		}

		private void CalculateDrag()
		{
		}

		private void CaluclateAerodynamicEffect()
		{
		}

		private void CalculateLinearForces()
		{
		}

		private void CalculateTorque()
		{
		}

		private void CalculateAltitude()
		{
		}

		public void Immobilize()
		{
		}

		public void Reset()
		{
		}
	}
}
