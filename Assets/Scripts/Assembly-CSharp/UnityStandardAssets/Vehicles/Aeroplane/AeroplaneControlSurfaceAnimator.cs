using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	public class AeroplaneControlSurfaceAnimator : MonoBehaviour
	{
		[Serializable]
		public class ControlSurface
		{
			public enum Type
			{
				Aileron = 0,
				Elevator = 1,
				Rudder = 2,
				RuddervatorNegative = 3,
				RuddervatorPositive = 4
			}

			public Transform transform;

			public float amount;

			public Type type;

			[HideInInspector]
			public Quaternion originalLocalRotation;
		}

		[SerializeField]
		private float m_Smoothing;

		[SerializeField]
		private ControlSurface[] m_ControlSurfaces;

		private AeroplaneController m_Plane;

		private void Start()
		{
		}

		private void Update()
		{
		}

		private void RotateSurface(ControlSurface surface, Quaternion rotation)
		{
		}
	}
}
