using UnityEngine;

public class Billboard : MonoBehaviour
{
	public Camera targetCamera;

	private Transform _camT;

	private Quaternion _lastCamRot;

	private Quaternion _lastParentRot;

	private bool _hasLast;

	public Transform CacheTransform { get; private set; }

	private void OnEnable()
	{
	}

	private bool SetupCamera()
	{
		return false;
	}

	private void LateUpdate()
	{
	}
}
