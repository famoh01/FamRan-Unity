using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField]
	public Transform targetPlayer;

	[SerializeField]
	public float cameraSmoothSpeed;

	[SerializeField]
	private Vector3 cameraOffset;

	private void FixedUpdate()
	{
	}
}
