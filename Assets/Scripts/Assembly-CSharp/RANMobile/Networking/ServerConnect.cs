using UnityEngine;

namespace RANMobile.Networking
{
	public class ServerConnect : MonoBehaviour
	{
		private GameDataManager gameDataManager;

		public bool Reconnecting { get; set; }

		public string RedirectScene { get; set; }

		private void Awake()
		{
		}
	}
}
