using TMPro;
using UnityEngine;

namespace AirFishLab.ScrollingList.Demo
{
	public class IntListBox : ListBox
	{
		[SerializeField]
		private TextMeshProUGUI _contentText;

		protected override void UpdateDisplayContent(object content)
		{
		}
	}
}
