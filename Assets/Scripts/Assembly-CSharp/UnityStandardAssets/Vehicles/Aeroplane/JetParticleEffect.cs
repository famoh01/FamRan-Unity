using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	[RequireComponent(typeof(ParticleSystem))]
	public class JetParticleEffect : MonoBehaviour
	{
		public Color minColour;

		private AeroplaneController m_Jet;

		private ParticleSystem m_System;

		private float m_OriginalStartSize;

		private float m_OriginalLifetime;

		private Color m_OriginalStartColor;

		private void Start()
		{
		}

		private void Update()
		{
		}

		private AeroplaneController FindAeroplaneParent()
		{
			return null;
		}
	}
}
