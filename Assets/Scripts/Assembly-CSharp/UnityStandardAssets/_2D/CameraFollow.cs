using UnityEngine;

namespace UnityStandardAssets._2D
{
	public class CameraFollow : MonoBehaviour
	{
		public float xMargin;

		public float yMargin;

		public float xSmooth;

		public float ySmooth;

		public Vector2 maxXAndY;

		public Vector2 minXAndY;

		private Transform m_Player;

		private void Awake()
		{
		}

		private bool CheckXMargin()
		{
			return false;
		}

		private bool CheckYMargin()
		{
			return false;
		}

		private void Update()
		{
		}

		private void TrackPlayer()
		{
		}
	}
}
