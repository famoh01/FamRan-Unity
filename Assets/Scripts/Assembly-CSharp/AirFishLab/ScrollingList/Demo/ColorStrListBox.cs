using UnityEngine;
using UnityEngine.UI;

namespace AirFishLab.ScrollingList.Demo
{
	public class ColorStrListBox : ListBox
	{
		[SerializeField]
		private Image _contentImage;

		[SerializeField]
		private Text _contentText;

		protected override void UpdateDisplayContent(object content)
		{
		}
	}
}
