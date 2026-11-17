using UnityEngine;

[CreateAssetMenu(fileName = "New CHF", menuName = "Scriptable Objects/EditorCHF/New CHF")]
public class CHFObject : ScriptableObject
{
	public RuntimeAnimatorController AnimatorController;

	public Avatar AnimatorAvatar;

	public GameObject skin;
}
