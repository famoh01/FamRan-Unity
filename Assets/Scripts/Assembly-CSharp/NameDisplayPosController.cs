using UnityEngine;

public class NameDisplayPosController : MonoBehaviour
{
	private Transform parentObject;

	private SkinnedMeshRenderer[] skinnedMeshRenderers;

	private IGNScript ignScript;

	private PlayerController localOwner;

	private PlayerControllerClient remoteOwner;

	[Header("Base Position")]
	public float heightOffset;

	[Header("Guild Adjustment")]
	public float guildExtraOffset;

	[Header("Mount Adjustment")]
	public float mountExtraOffset;

	private float objectHeight;

	private Vector3 initialLocalPosition;

	private float _lastY;

	private void OnEnable()
	{
	}

	private void Start()
	{
	}

	private void LateUpdate()
	{
	}

	private bool IsOwnerMounted()
	{
		return false;
	}

	private Bounds CalculateTotalBounds(SkinnedMeshRenderer[] renderers)
	{
		return default;
	}
}
