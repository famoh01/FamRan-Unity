using UnityEngine;

[CreateAssetMenu(fileName = "New Character Creation Data", menuName = "Scriptable Objects/Default Configuration/New Character Creation Data")]
public class CharacterCreationDataSO : ScriptableObject
{
	[SerializeField]
	private string m_name;

	[SerializeField]
	private int m_gender;

	[SerializeField]
	private int m_charClass;

	[SerializeField]
	private int m_school;

	[SerializeField]
	private int m_hairType;

	[SerializeField]
	private int m_hairColorType;

	[SerializeField]
	private int m_faceType;

	public string charCreateName => null;

	public int gender => 0;

	public int charClass => 0;

	public int school => 0;

	public int hairType => 0;

	public int hairColorType => 0;

	public int faceType => 0;

	public string GetName()
	{
		return null;
	}

	public int GetGender()
	{
		return 0;
	}

	public int GetCharClass()
	{
		return 0;
	}

	public int GetSchool()
	{
		return 0;
	}

	public int GetHairType()
	{
		return 0;
	}

	public int GetHairColorType()
	{
		return 0;
	}

	public int GetFaceType()
	{
		return 0;
	}

	public void SetName(string _name)
	{
	}

	public void SetGender(int _gender)
	{
	}

	public void SetCharacterClass(int _charClass)
	{
	}

	public void SetSchool(int _school)
	{
	}

	public void SetHairType(int _hairType)
	{
	}

	public void SetHairColorType(int _hairColorType)
	{
	}

	public void SetFaceType(int _faceType)
	{
	}
}
