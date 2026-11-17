using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	public class AeroplaneAudio : MonoBehaviour
	{
		[Serializable]
		public class AdvancedSetttings
		{
			public float engineMinDistance;

			public float engineMaxDistance;

			public float engineDopplerLevel;

			[Range(0f, 1f)]
			public float engineMasterVolume;

			public float windMinDistance;

			public float windMaxDistance;

			public float windDopplerLevel;

			[Range(0f, 1f)]
			public float windMasterVolume;
		}

		[SerializeField]
		private AudioClip m_EngineSound;

		[SerializeField]
		private float m_EngineMinThrottlePitch;

		[SerializeField]
		private float m_EngineMaxThrottlePitch;

		[SerializeField]
		private float m_EngineFwdSpeedMultiplier;

		[SerializeField]
		private AudioClip m_WindSound;

		[SerializeField]
		private float m_WindBasePitch;

		[SerializeField]
		private float m_WindSpeedPitchFactor;

		[SerializeField]
		private float m_WindMaxSpeedVolume;

		[SerializeField]
		private AdvancedSetttings m_AdvancedSetttings;

		private AudioSource m_EngineSoundSource;

		private AudioSource m_WindSoundSource;

		private AeroplaneController m_Plane;

		private Rigidbody m_Rigidbody;

		private void Awake()
		{
		}

		private void Update()
		{
		}
	}
}
