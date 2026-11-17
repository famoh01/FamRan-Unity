using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCameraFacingController : MonoBehaviour
{
	[Serializable]
	public struct MapCenterOverride
	{
		public string sceneName;

		public Vector3 center;
	}

	[SerializeField]
	private MapCenterOverride[] centerOverrides;

	private CinemachineFreeLook _cineCam;

	private int _pendingFrames;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
	}

	private Vector3? GetMapCenter()
	{
		return null;
	}

	private void Update()
	{
	}
}
