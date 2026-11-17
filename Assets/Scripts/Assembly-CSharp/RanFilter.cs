using UnityEngine;

public class RanFilter : MonoBehaviour
{
	public static RanFilter instance;

	public BINObject[] binFilter;

	private void Awake()
	{
	}

	public string FilterEmoticon(string chat, string[] characters)
	{
		return null;
	}

	public string FilterChat(string chat)
	{
		return null;
	}

	public bool FilterIGN(string ign)
	{
		return false;
	}

	public bool ContainsSpecialCharacters(string message)
	{
		return false;
	}
}
