using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	public class AeroplanePropellerAnimator : MonoBehaviour
	{
		[SerializeField]
		private Transform m_PropellorModel;

		[SerializeField]
		private Transform m_PropellorBlur;

		[SerializeField]
		private Texture2D[] m_PropellorBlurTextures;

		[SerializeField]
		[Range(0f, 1f)]
		private float m_ThrottleBlurStart;

		[SerializeField]
		[Range(0f, 1f)]
		private float m_ThrottleBlurEnd;

		[SerializeField]
		private float m_MaxRpm;

		private AeroplaneController m_Plane;

		private int m_PropellorBlurState;

		private const float k_RpmToDps = 60f;

		private Renderer m_PropellorModelRenderer;

		private Renderer m_PropellorBlurRenderer;

		private void Awake()
		{
		}

		private void Update()
		{
		}
	}
}
