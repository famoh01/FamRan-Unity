using UnityEngine;
using UnityEngine.UI;

public class ToggleTargetPlayer : MonoBehaviour
{
	public static ToggleTargetPlayer Instance;

	public const string CanTargetPlayerPrefKey = "CanTargetPlayer";

	public bool canTargetPlayer;

	[Header("UI References")]
	public Toggle myToggle;

	public bool CanTargetPlayerNow => false;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void OnCheckboxToggled(bool isChecked)
	{
	}
}
