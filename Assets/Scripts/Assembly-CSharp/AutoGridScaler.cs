using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(GridLayoutGroup))]
public class AutoGridScaler : MonoBehaviour
{
	public enum ScaleMode
	{
		Scrollable = 0,
		FitToBox = 1
	}

	[Header("Behavior Mode")]
	[Tooltip("Scrollable for Inventories. FitToBox for static panels like the Vend UI.")]
	public ScaleMode mode;

	[Header("Grid Settings")]
	public int columns;

	[Tooltip("Only used in FitToBox mode")]
	public int rows;

	public float spacing;

	[Header("Slot Shape (Scrollable Only)")]
	public float aspectRatio;

	[Header("Overrides")]
	[Tooltip("Leave empty to auto-detect. If the scale is wrong, drag the Viewport (for scrolling) or the main container here.")]
	public RectTransform referenceRect;

	private GridLayoutGroup grid;

	private void Start()
	{
	}

	private void OnRectTransformDimensionsChange()
	{
	}

	private void OnValidate()
	{
	}

	[ContextMenu("Force Update Grid")]
	public void UpdateGridSize()
	{
	}
}
