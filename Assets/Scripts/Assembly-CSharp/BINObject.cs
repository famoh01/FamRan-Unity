using UnityEngine;

[CreateAssetMenu(fileName = "New BIN", menuName = "Scriptable Objects/EditorBIN/New BIN Filter")]
public class BINObject : ScriptableObject
{
	public string[] specialCharacters;

	public string[] characters;
}
