using System;
using UnityEngine;
using UnityEngine.Events;

namespace AirFishLab.ScrollingList
{
	[Serializable]
	public class CircularScrollingListSetting
	{
		[SerializeField]
		[Tooltip("The type of the list.")]
		private CircularScrollingList.ListType _listType;

		[SerializeField]
		[Tooltip("The controlling mode of the list.")]
		private CircularScrollingList.ControlMode _controlMode;

		[SerializeField]
		[Tooltip("Should a box align in the middle of the list after sliding?")]
		private bool _alignMiddle;

		[SerializeField]
		[Tooltip("Whether to reverse the scrolling direction or not")]
		private bool _reverseDirection;

		[SerializeField]
		[Tooltip("The major moving direction of the list.")]
		private CircularScrollingList.Direction _direction;

		[SerializeField]
		[Tooltip("Specify the initial content ID for the centered box.")]
		private int _centeredContentID;

		[SerializeField]
		[Tooltip("Whether to center the selected box or not")]
		private bool _centerSelectedBox;

		[SerializeField]
		[Tooltip("To show the list in the reverse order")]
		private bool _reverseOrder;

		[SerializeField]
		[Tooltip("Whether to initialize the list on Start or not. If set to false, manually call Initialize() to initialize the list.")]
		private bool _initializeOnStart;

		[SerializeField]
		[Tooltip("The factor that adjusting the distance between boxes. The larger, the closer.")]
		private float _boxDensity;

		[SerializeField]
		[Tooltip("The curve specifying the passive position of the box. The x axis is the major position of the box, which is mapped to [-1, 1]. The y axis defines the factor of the passive position of the box. Point (0, 0) is the center of the list layout.")]
		private AnimationCurve _boxPositionCurve;

		[SerializeField]
		[Tooltip("The curve specifying the box scale. The x axis is the major position of the box, which is mapped to [-1, 1]. The y axis specifies the value of 'localScale' of the box at the corresponding position.")]
		private AnimationCurve _boxScaleCurve;

		[SerializeField]
		[Tooltip("The curve specifying the velocity factor of the box after releasing. The x axis is the the moving duration in seconds, which starts from 0. The y axis is the factor of releasing velocity.")]
		private AnimationCurve _boxVelocityCurve;

		[SerializeField]
		[Tooltip("The curve specifying the movement factor of the box. The x axis is the moving duration in seconds, which starts from 0. The y axis is the factor for reaching the target position.")]
		private AnimationCurve _boxMovementCurve;

		[SerializeField]
		[Tooltip("The callback to be invoked when a box is clicked. The registered callbacks will be added to the 'onClick' event of boxes, therefore, boxes should be 'Button's.")]
		private ListBoxIntEvent _onBoxClick;

		[SerializeField]
		[Tooltip("The callback to be invoked when the centered content is changed. The int parameter is the new content ID.")]
		private ListBoxIntEvent _onCenteredContentChanged;

		[SerializeField]
		[Tooltip("The callback to be invoked when the movement is ended")]
		private UnityEvent _onMovementEnd;

		public CircularScrollingList.ListType listType => CircularScrollingList.ListType.Circular;

		public CircularScrollingList.ControlMode controlMode => CircularScrollingList.ControlMode.Drag;

		public bool alignMiddle => false;

		public bool reverseDirection => false;

		public CircularScrollingList.Direction direction => CircularScrollingList.Direction.Vertical;

		public int centeredContentID => 0;

		public bool centerSelectedBox => false;

		public bool reverseOrder => false;

		public bool initializeOnStart => false;

		public float boxDensity => 0f;

		public AnimationCurve boxPositionCurve => null;

		public AnimationCurve boxScaleCurve => null;

		public AnimationCurve boxVelocityCurve => null;

		public AnimationCurve boxMovementCurve => null;

		public ListBoxIntEvent onBoxClick => null;

		public ListBoxIntEvent onCenteredContentChanged => null;

		public UnityEvent onMovementEnd => null;
	}
}
