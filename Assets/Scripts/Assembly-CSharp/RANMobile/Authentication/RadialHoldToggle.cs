using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Click-to-toggle reveal for the radial window menu. Attach to the grid "Tab"
	/// button: one click opens the fanned-out menu buttons (Inventory, Skills, Char,
	/// Bank, Party, Quest, Friends, Club) and they STAY open; clicking the grid button
	/// again closes them. Matches the original, where the menu is revealed on demand
	/// rather than only while the finger is held down.
	/// </summary>
	public class RadialHoldToggle : MonoBehaviour, IPointerDownHandler
	{
		/// <summary>The fanned-out menu buttons to reveal/hide.</summary>
		public readonly List<GameObject> Items = new List<GameObject>();

		private bool _open;

		public bool IsOpen
		{
			get { return _open; }
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			SetOpen(!_open);
		}

		public void SetOpen(bool open)
		{
			_open = open;
			for (int i = 0; i < Items.Count; i++)
			{
				GameObject go = Items[i];
				if (go != null && go.activeSelf != open)
				{
					go.SetActive(open);
				}
			}
		}
	}
}
