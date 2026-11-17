using UnityEngine;

[CreateAssetMenu(fileName = "New ItemRandomValue", menuName = "Scriptable Objects/EditorItemRandomValue/New ItemRandomValue")]
public class ItemRandomValueObject : ScriptableObject
{
	public float rate;

	public int setNumber;

	public NewOption[] newOption;
}
