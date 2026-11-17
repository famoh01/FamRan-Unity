using UnityEngine;
using UnityEngine.UI;

namespace AirFishLab.ScrollingList.Demo
{
	public class StringStarListBox : ListBox
	{
		[SerializeField]
		private Text _text;

		[SerializeField]
		private GameObject[] _stars;

		protected override void UpdateDisplayContent(object content)
		{
		}
	}
}
