using UnityEngine;

public class PlayerModal : MonoBehaviour
{
	public bool showRebirthModal;

	private const float CheckInterval = 0.5f;

	private float _checkTimer;

	private const float RebirthRecheckInterval = 5f;

	private float _rebirthRecheckTimer;

	public static PlayerModal instance;

	private void Awake()
	{
	}

	private void FixedUpdate()
	{
	}

	private static bool IsRebirthModalVisible()
	{
		return false;
	}

	private static bool IsGenuinelyDead(PlayerController pc)
	{
		return false;
	}

	public void ShowRebirthModal()
	{
	}

	private void RebirthAction(bool useRosary)
	{
	}

	private void TyrannyReviveAction()
	{
	}

	public void ShowDisconnectionModal()
	{
	}

	private static void OnDisconnectionConfirm()
	{
	}

	public void ShowGenericModal(string message)
	{
	}

	private static void OnGenericModalConfirm()
	{
	}
}
