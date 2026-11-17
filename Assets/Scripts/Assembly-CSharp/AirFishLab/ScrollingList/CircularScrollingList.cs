using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AirFishLab.ScrollingList
{
	public class CircularScrollingList : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IScrollHandler
	{
		public enum ListType
		{
			Circular = 0,
			Linear = 1
		}

		public enum ControlMode
		{
			Drag = 0,
			Function = 1,
			MouseWheel = 2
		}

		public enum Direction
		{
			Vertical = 0,
			Horizontal = 1
		}

		public static CircularScrollingList instance;

		[SerializeField]
		[Tooltip("The game object that stores the contents for the list to display. It should be derived from the class BaseListBank.")]
		private BaseListBank _listBank;

		[SerializeField]
		[Tooltip("The game objects that used for displaying the content. They should be derived from the class ListBox")]
		private List<ListBox> _listBoxes;

		[SerializeField]
		[Tooltip("The setting of this list")]
		private CircularScrollingListSetting _setting;

		[Tooltip("Lock the scroll")]
		public bool _isLock;

		private RectTransform _rectTransform;

		private Camera _canvasRefCamera;

		public ListPositionCtrl _listPositionCtrl;

		public ListContentManager _listContentManager;

		private bool _isInitialized;

		public bool _hasNoContent;

		public BaseListBank listBank => null;

		public CircularScrollingListSetting setting => null;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		public void Initialize()
		{
		}

		private void GetComponentReference()
		{
		}

		private void InitializeListComponents()
		{
		}

		public ListBox GetCenteredBox()
		{
			return null;
		}

		public int GetCenteredContentID()
		{
			return 0;
		}

		public void MoveOneUnitUp()
		{
		}

		public void MoveOneUnitDown()
		{
		}

		public void Refresh(int centeredContentID = -1)
		{
		}

		public void SelectContentID(int contentID)
		{
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		public void OnDrag(PointerEventData eventData)
		{
		}

		public void OnEndDrag(PointerEventData eventData)
		{
		}

		public void OnScroll(PointerEventData eventData)
		{
		}

		private void Update()
		{
		}

		private void LateUpdate()
		{
		}
	}
}
