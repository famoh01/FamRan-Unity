using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	public class CarController : MonoBehaviour
	{
		[SerializeField]
		private CarDriveType m_CarDriveType;

		[SerializeField]
		private WheelCollider[] m_WheelColliders;

		[SerializeField]
		private GameObject[] m_WheelMeshes;

		[SerializeField]
		private WheelEffects[] m_WheelEffects;

		[SerializeField]
		private Vector3 m_CentreOfMassOffset;

		[SerializeField]
		private float m_MaximumSteerAngle;

		[Range(0f, 1f)]
		[SerializeField]
		private float m_SteerHelper;

		[Range(0f, 1f)]
		[SerializeField]
		private float m_TractionControl;

		[SerializeField]
		private float m_FullTorqueOverAllWheels;

		[SerializeField]
		private float m_ReverseTorque;

		[SerializeField]
		private float m_MaxHandbrakeTorque;

		[SerializeField]
		private float m_Downforce;

		[SerializeField]
		private SpeedType m_SpeedType;

		[SerializeField]
		private float m_Topspeed;

		[SerializeField]
		private static int NoOfGears;

		[SerializeField]
		private float m_RevRangeBoundary;

		[SerializeField]
		private float m_SlipLimit;

		[SerializeField]
		private float m_BrakeTorque;

		private Quaternion[] m_WheelMeshLocalRotations;

		private Vector3 m_Prevpos;

		private Vector3 m_Pos;

		private float m_SteerAngle;

		private int m_GearNum;

		private float m_GearFactor;

		private float m_OldRotation;

		private float m_CurrentTorque;

		private Rigidbody m_Rigidbody;

		private const float k_ReversingThreshold = 0.01f;

		public bool Skidding { get; private set; }

		public float BrakeInput { get; private set; }

		public float CurrentSteerAngle => 0f;

		public float CurrentSpeed => 0f;

		public float MaxSpeed => 0f;

		public float Revs { get; private set; }

		public float AccelInput { get; private set; }

		private void Start()
		{
		}

		private void GearChanging()
		{
		}

		private static float CurveFactor(float factor)
		{
			return 0f;
		}

		private static float ULerp(float from, float to, float value)
		{
			return 0f;
		}

		private void CalculateGearFactor()
		{
		}

		private void CalculateRevs()
		{
		}

		public void Move(float steering, float accel, float footbrake, float handbrake)
		{
		}

		private void CapSpeed()
		{
		}

		private void ApplyDrive(float accel, float footbrake)
		{
		}

		private void SteerHelper()
		{
		}

		private void AddDownForce()
		{
		}

		private void CheckForWheelSpin()
		{
		}

		private void TractionControl()
		{
		}

		private void AdjustTorque(float forwardSlip)
		{
		}

		private bool AnySkidSoundPlaying()
		{
			return false;
		}
	}
}
