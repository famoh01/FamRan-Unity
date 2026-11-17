using Cinemachine;
using UnityEngine;

public class ShowcaseCameraHandler : MonoBehaviour
{
	public static ShowcaseCameraHandler instance;

	[SerializeField]
	private CinemachineVirtualCamera cam1;

	[SerializeField]
	private CinemachineVirtualCamera cam2;

	[SerializeField]
	private CinemachineVirtualCamera cam3;

	[SerializeField]
	private CinemachineVirtualCamera cam4;

	[SerializeField]
	private CinemachineVirtualCamera cam5;

	public GameObject centerObject;

	private GameObject _playerObject;

	public bool onPlayerCam;

	public bool onNpcCam;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	public void SwitchToNpcCam()
	{
	}

	public void SwitchToPlayerCam()
	{
	}
}
