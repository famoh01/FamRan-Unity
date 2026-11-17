using System.Collections.Generic;
using AirFishLab.ScrollingList.BoxTransformCtrl;
using UnityEngine;

namespace AirFishLab.ScrollingList
{
	public class ListBox : MonoBehaviour
	{
		private CircularScrollingListSetting _listSetting;

		private ListPositionCtrl _positionCtrl;

		private ListContentManager _contentManager;

		private List<ListBox> _listBoxes;

		private IBoxTransformCtrl _boxTransformCtrl;

		public int listBoxID { get; private set; }

		public ListBox lastListBox { get; private set; }

		public ListBox nextListBox { get; private set; }

		public int contentID { get; private set; }

		public void Initialize(CircularScrollingListSetting setting, ListPositionCtrl listPositionCtrl, ListContentManager listContentManager, List<ListBox> listBoxes, int listBoxID)
		{
		}

		private void InitializePosition()
		{
		}

		private void InitializeBoxDependency()
		{
		}

		private void AddClickEvent()
		{
		}

		public void UpdatePosition(float delta)
		{
		}

		public void PopToFront()
		{
		}

		public void PushToBack()
		{
		}

		private void InitializeContent()
		{
		}

		private void CheckToBeDisabled()
		{
		}

		private void UpdateToLastContent()
		{
		}

		private void UpdateToNextContent()
		{
		}

		protected virtual void UpdateDisplayContent(object content)
		{
		}

		private void UpdateDisplayContentPrivate()
		{
		}

		public void Refresh(int centerBoxID, int centerContentID)
		{
		}
	}
}
