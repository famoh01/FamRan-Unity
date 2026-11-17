using UnityEngine;
using UnityEngine.UI;

namespace TrinityRift.UI.ServerSelect
{
	[DisallowMultipleComponent]
	public class AutoScrollView : MonoBehaviour
	{
		[Tooltip("Leave empty to use the ScrollRect on this object.")]
		[SerializeField]
		private ScrollRect scrollRect;

		[Tooltip("The Scrollbar GameObject to show/hide. Usually the ScrollRect's vertical scrollbar.")]
		[SerializeField]
		private GameObject scrollbar;

		[Tooltip("Scrolling and the scrollbar appear only when the row count is GREATER than this. 3 = a list of 1-3 rows is static, 4+ scrolls.")]
		[SerializeField]
		private int rowsBeforeScroll = 3;

		[Tooltip("Scroll axis this list uses. Vertical for both the server and the channel list.")]
		[SerializeField]
		private bool horizontalList;

		[Header("Optional — grow the view until the threshold is reached")]
		[Tooltip("When on, the viewport is resized to fit the rows (up to rowsBeforeScroll) so a 1-row list is not a tall box of empty space.")]
		[SerializeField]
		private bool resizeViewport;

		[Tooltip("Rect to resize. Leave empty to use the ScrollRect's own rect.")]
		[SerializeField]
		private RectTransform resizeTarget;

		[SerializeField]
		private float rowSize = 60f;

		[SerializeField]
		private float rowSpacing = 6f;

		[SerializeField]
		private float padding = 10f;

		private int _appliedCount;

		private bool _cached;

		private bool _pendingReset;

		public int RowsBeforeScroll => rowsBeforeScroll;

		private void OnEnable()
		{
			if (_pendingReset)
			{
				_pendingReset = false;
				Apply(_appliedCount);
			}
		}

		private void Cache()
		{
			if (_cached)
			{
				return;
			}
			if (scrollRect == null)
			{
				scrollRect = GetComponent<ScrollRect>();
			}
			_cached = true;
		}

		public void Apply(int rowCount)
		{
			Cache();
			_appliedCount = rowCount;
			if (!gameObject.activeInHierarchy)
			{
				_pendingReset = true;
				return;
			}
			bool shouldScroll = rowCount > rowsBeforeScroll;
			if (scrollRect != null)
			{
				scrollRect.horizontal = horizontalList && shouldScroll;
				scrollRect.vertical = !horizontalList && shouldScroll;
				scrollRect.movementType = shouldScroll ? ScrollRect.MovementType.Elastic : ScrollRect.MovementType.Unrestricted;
			}
			if (scrollbar != null)
			{
				scrollbar.SetActive(shouldScroll);
			}
			if (resizeViewport)
			{
				ResizeToFit(rowCount);
			}
			ResetPosition();
		}

		private void ResizeToFit(int rowCount)
		{
			RectTransform target = resizeTarget;
			if (target == null && scrollRect != null)
			{
				target = scrollRect.GetComponent<RectTransform>();
			}
			if (target == null)
			{
				return;
			}
			int visible = Mathf.Min(rowCount, rowsBeforeScroll);
			if (visible < 1)
			{
				visible = 1;
			}
			float size = padding * 2f + visible * rowSize + (visible - 1) * rowSpacing;
			Vector2 dims = target.sizeDelta;
			if (horizontalList)
			{
				dims.x = size;
			}
			else
			{
				dims.y = size;
			}
			target.sizeDelta = dims;
		}

		public void ResetPosition()
		{
			Cache();
			if (scrollRect != null)
			{
				scrollRect.normalizedPosition = horizontalList ? new Vector2(0f, 0.5f) : new Vector2(0.5f, 1f);
			}
		}

		public void Invalidate()
		{
			_cached = false;
		}
	}
}
