using Cinemachine;
using UnityEngine;

public class SwitchCinemachine : MonoBehaviour
{
	public static SwitchCinemachine instance;

	[SerializeField]
	private CinemachineFreeLook playerCam;

	[SerializeField]
	private CinemachineVirtualCamera npcCam;

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
