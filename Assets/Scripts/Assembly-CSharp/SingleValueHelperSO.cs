using UnityEngine;

[CreateAssetMenu(fileName = "New Developer Helper", menuName = "Scriptable Objects/ETC/Developer/New Developer Helper")]
public class SingleValueHelperSO : ScriptableObject
{
	[SerializeField]
	private string m_stringValue;

	[SerializeField]
	private int m_intValue;

	public static SingleValueHelperSO Instance { get; private set; }

	public string stringValue => null;

	public int intValue => 0;

	private void Awake()
	{
	}

	public string GetStringValue()
	{
		return null;
	}

	public void SetStringValue(string _stringValue)
	{
	}

	public int GetintValue()
	{
		return 0;
	}

	public void SetintValue(int _intValue)
	{
	}
}
