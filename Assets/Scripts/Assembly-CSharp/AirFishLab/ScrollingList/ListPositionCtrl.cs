using System;
using System.Collections.Generic;
using AirFishLab.ScrollingList.MovementCtrl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AirFishLab.ScrollingList
{
	public class ListPositionCtrl
	{
		public enum PositionState
		{
			Top = 0,
			Middle = 1,
			Bottom = 2
		}

		private readonly CircularScrollingListSetting _listSetting;

		private readonly Camera _canvasRefCamera;

		private readonly RectTransform _rectTransform;

		private readonly List<ListBox> _listBoxes;

		private ListBox _centeredBox;

		private Func<Vector2, float> _getFactor;

		private Action<PointerEventData, TouchPhase> _inputPositionHandler;

		private Action<Vector2> _scrollHandler;

		private Action<float> _scrollDirectionHandler;

		private bool _toRunLateUpdate;

		private IMovementCtrl _movementCtrl;

		private Vector2 _lastInputLocalPos;

		private float _lastDraggingTime;

		private float _deltaInputDistance;

		private float _deltaDistanceToCenter;

		private PositionState _positionState;

		private readonly int _maxNumOfDisabledBoxes;

		private int _selectionDistanceFactor;

		private int _scrollFactor;

		private bool _isEndingMovement;

		public float unitPos { get; private set; }

		public float lowerBoundPos { get; private set; }

		public float upperBoundPos { get; private set; }

		public int numOfUpperDisabledBoxes { get; set; }

		public int numOfLowerDisabledBoxes { get; set; }

		public ListPositionCtrl(CircularScrollingListSetting listSetting, RectTransform rectTransform, Camera canvasRefCamera, List<ListBox> listBoxes)
		{
		}

		private void InitializePositionVars()
		{
		}

		private void InitializeInputFunction()
		{
		}

		public void InitialImageSorting()
		{
		}

		public void InputPositionHandler(PointerEventData eventData, TouchPhase phase)
		{
		}

		public void ScrollHandler(PointerEventData eventData)
		{
		}

		public void SetSelectionMovement(int idDiff)
		{
		}

		private void DragPositionHandler(PointerEventData pointer, TouchPhase state)
		{
		}

		private Vector2 ScreenToLocalPos(Vector2 screenPos)
		{
			return default;
		}

		private float GetDeltaInputDistance(Vector2 pointerLocalPos)
		{
			return 0f;
		}

		private void ScrollDeltaHandler(Vector2 mouseScrollDelta)
		{
		}

		private void ScrollVertically(float scrollDelta)
		{
		}

		private void ScrollHorizontally(float scrollDelta)
		{
		}

		public void Update()
		{
		}

		public void LateUpdate()
		{
		}

		private void FindDeltaDistanceToCenter()
		{
		}

		public void SetUnitMove(int unit)
		{
		}

		private void UpdatePositionState()
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
	}
}
