using UnityEngine;
using UnityEngine.UI;

public class MapZoom : MonoBehaviour
{
	public float zoomSpeed;

	public float minZoom;

	public float maxZoom;

	public float smoothTime;

	[SerializeField]
	private ScrollRect scrollRect;

	private RectTransform contentRectTransform;

	private Vector3 zoomVelocity;

	private bool isPinching;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetDefaultZoom()
	{
	}
}
