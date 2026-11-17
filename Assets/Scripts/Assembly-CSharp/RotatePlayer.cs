using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
	[Header("Setup")]
	public Transform charCreationSpawn;

	[Header("Settings")]
	public float speed;

	private bool _rightIsPressed;

	private bool _leftIsPressed;

	private float _direction;

	private void Update()
	{
	}

	public void RightTogglePressed(bool value)
	{
	}

	public void LeftTogglePressed(bool value)
	{
	}

	private void RecalculateDirection()
	{
	}
}
