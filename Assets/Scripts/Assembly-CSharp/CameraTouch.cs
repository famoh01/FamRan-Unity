using Cinemachine;
using UnityEngine;

public class CameraTouch : MonoBehaviour
{
	[Header("Camera Touch")]
	[SerializeField]
	private CinemachineFreeLook cineCam;

	public TouchField touchField;

	[SerializeField]
	private float SensitivityX;

	[SerializeField]
	private float SensitivityY;

	private float currentSensitivityMultiplier;

	private Transform playerTransform;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Start()
	{
	}

	private void UpdateSensitivityMultiplier(float newMultiplier)
	{
	}

	private void Update()
	{
	}
}
