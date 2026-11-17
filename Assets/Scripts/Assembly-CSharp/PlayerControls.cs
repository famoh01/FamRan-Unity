using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
	public static RaycastResult LastRayHit;

	public static GameObject map;

	public static Client client;

	private bool _spawned;

	private bool _pendingTransfer;

	public static PlayerControls Instance { get; private set; }

	public bool IsSpawned => false;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	private static string GetHierarchyPath(Transform t)
	{
		return null;
	}

	public void ResetSpawnFlag()
	{
	}

	public void PrepareForTransfer()
	{
	}

	private void FixedUpdate()
	{
	}
}
