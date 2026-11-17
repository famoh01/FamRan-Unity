using UnityEngine;

public class InventEquipBlocker : MonoBehaviour
{
	[Tooltip("Extra canvases OUTSIDE this GameObject that should also raise the gate. The direct-child window canvases (Inventory Canvas, Equipment Canvas, …) are picked up automatically — only list canvases that live elsewhere here.")]
	[SerializeField]
	private Canvas[] additionalCanvases;

	private Canvas[] watchedCanvases;

	public static bool IsBlocking { get; private set; }

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void LateUpdate()
	{
	}

	private bool IsAnyWatchedCanvasOpen()
	{
		return false;
	}
}
