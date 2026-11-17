using UnityEngine;
using UnityEngine.UI;

namespace AirFishLab.ScrollingList.Demo
{
	public class DisplayAndSelectExample : MonoBehaviour
	{
		[SerializeField]
		private CircularScrollingList _list;

		[SerializeField]
		private Text _displayText;

		[SerializeField]
		private Text _centeredContentText;

		public void DisplayCenteredContent()
		{
		}

		public void GetSelectedContentID(int selectedContentID)
		{
		}

		public void OnListCenteredContentChanged(int centeredContentID)
		{
		}

		public void OnMovementEnd()
		{
		}
	}
}
