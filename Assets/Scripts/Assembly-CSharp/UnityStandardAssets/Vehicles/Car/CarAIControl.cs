using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	[RequireComponent(typeof(CarController))]
	public class CarAIControl : MonoBehaviour
	{
		public enum BrakeCondition
		{
			NeverBrake = 0,
			TargetDirectionDifference = 1,
			TargetDistance = 2
		}

		[SerializeField]
		[Range(0f, 1f)]
		private float m_CautiousSpeedFactor;

		[SerializeField]
		[Range(0f, 180f)]
		private float m_CautiousMaxAngle;

		[SerializeField]
		private float m_CautiousMaxDistance;

		[SerializeField]
		private float m_CautiousAngularVelocityFactor;

		[SerializeField]
		private float m_SteerSensitivity;

		[SerializeField]
		private float m_AccelSensitivity;

		[SerializeField]
		private float m_BrakeSensitivity;

		[SerializeField]
		private float m_LateralWanderDistance;

		[SerializeField]
		private float m_LateralWanderSpeed;

		[SerializeField]
		[Range(0f, 1f)]
		private float m_AccelWanderAmount;

		[SerializeField]
		private float m_AccelWanderSpeed;

		[SerializeField]
		private BrakeCondition m_BrakeCondition;

		[SerializeField]
		private bool m_Driving;

		[SerializeField]
		private Transform m_Target;

		[SerializeField]
		private bool m_StopWhenTargetReached;

		[SerializeField]
		private float m_ReachTargetThreshold;

		private float m_RandomPerlin;

		private CarController m_CarController;

		private float m_AvoidOtherCarTime;

		private float m_AvoidOtherCarSlowdown;

		private float m_AvoidPathOffset;

		private Rigidbody m_Rigidbody;

		private void Awake()
		{
		}

		private void FixedUpdate()
		{
		}

		private void OnCollisionStay(Collision col)
		{
		}

		public void SetTarget(Transform target)
		{
		}
	}
}
