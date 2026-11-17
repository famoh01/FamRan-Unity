using UnityEngine;

public class CharacterEmoticon : MonoBehaviour
{
	public static CharacterEmoticon instance;

	public EmoticonObject emote;

	public void Awake()
	{
	}

	public bool IsValid(ushort index)
	{
		return false;
	}
}
