using UnityEngine;

public class AnimationStrings : MonoBehaviour
{
	public static AnimationStrings instance;

	public AnimationType ANISUBTYPE;

	[Header("Run")]
	public string runningNoWeapon;

	public string runningWithWeapon;

	[Header("Walk")]
	public string walkingNoWeapon;

	public string walkingWithWeapon;

	[Header("Idle")]
	public string idleNoWeapon;

	public string idleWithWeapon;

	[Header("Attack")]
	public string attackNoWeapon;

	public string attackWithWeapon;

	[Header("Animation Name")]
	public string animIdleNoWeapon;

	public string animIdleWithWeapon;

	[Header("ETC")]
	public string died;

	public string winning;

	public bool isUsingMount;

	private void Awake()
	{
	}
}
