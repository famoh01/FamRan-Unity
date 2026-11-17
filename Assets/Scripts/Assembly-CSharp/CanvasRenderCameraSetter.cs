using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasRenderCameraSetter : MonoBehaviour
{
	[SerializeField]
	private Camera renderCamera;

	private void Awake()
	{
	}
}
