using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.SceneUtils
{
	public class SlowMoButton : MonoBehaviour
	{
		public Sprite FullSpeedTex;

		public Sprite SlowSpeedTex;

		public float fullSpeed;

		public float slowSpeed;

		public Button button;

		private bool m_SlowMo;

		private void Start()
		{
		}

		private void OnDestroy()
		{
		}

		public void ChangeSpeed()
		{
		}
	}
}
