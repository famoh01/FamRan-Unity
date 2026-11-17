using Cinemachine;
using UnityEngine;

public class CmCamFollow : MonoBehaviour
{
	[Tooltip("Object holding UIHUDFunctions (source of playerCoordinate). Falls back to UIHUDFunctions.Instance.")]
	public GameObject targetObject;

	private CinemachineFreeLook freeCamera;

	private UIHUDFunctions _hud;

	private Transform _boundPlayer;

	private void Awake()
	{
	}

	private void Update()
	{
	}
}
