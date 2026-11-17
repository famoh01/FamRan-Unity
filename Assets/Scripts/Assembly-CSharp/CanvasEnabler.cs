using UnityEngine;
using UnityEngine.Events;

public class CanvasEnabler : MonoBehaviour
{
	private Canvas _mainCanvas;

	public bool HideOnStart;

	[Header("Events")]
	public UnityEvent OnShow;

	public UnityEvent OnHide;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void Hide()
	{
	}

	public void Show()
	{
	}
}
