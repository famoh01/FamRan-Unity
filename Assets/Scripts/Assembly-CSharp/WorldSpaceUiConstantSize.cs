using UnityEngine;

public class WorldSpaceUiConstantSize : MonoBehaviour
{
	public float initialDistance;

	public float scaleValue;

	public float minScaleValue;

	public Camera mainCamera;

	public Canvas canvas;

	private Transform _camT;

	private Transform _canvasT;

	private float _lastScale;

	private bool _initialized;

	private NameplateContentDriver _driver;

	internal int _driverScaleIndex;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Awake()
	{
	}

	public void SetVisibilityUpdatesEnabled(bool visible)
	{
	}

	private void EnsureInitialized()
	{
	}

	internal void ScaleTick()
	{
	}
}
