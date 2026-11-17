using System.Collections.Generic;
using Core.Enumerators;
using Models.Library.Shared.Game;
using RAN.Mobile.GameModels;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Select Data", menuName = "Scriptable Objects/Default Configuration/New Character Select Data")]
public class CharacterSelectDataSO : ScriptableObject
{
	[SerializeField]
	private uint m_id;

	[SerializeField]
	private string m_userId;

	[SerializeField]
	private string m_name;

	[SerializeField]
	private CharacterClassEnum m_charClass;

	[SerializeField]
	private int m_school;

	[SerializeField]
	private int m_hairType;

	[SerializeField]
	private int m_hairColorType;

	[SerializeField]
	private int m_faceType;

	[SerializeField]
	private int m_level;

	[SerializeField]
	private int m_lastMap;

	[SerializeField]
	private int m_unitId;

	[SerializeField]
	private Vector3 m_position;

	[SerializeField]
	private int m_gateId;

	[SerializeField]
	private List<CharScroll> m_charScroll;

	[SerializeField]
	private List<SCharSkill> m_charSkill;

	[SerializeField]
	private int? m_partyId;

	[SerializeField]
	private int? m_partyLeaderId;

	[SerializeField]
	private int m_savePointMap;

	[SerializeField]
	private int m_savePointGate;

	[SerializeField]
	private int m_previousMap;

	[SerializeField]
	private int m_previousGate;

	[SerializeField]
	private Vector3 m_previousPosition;

	private GameCharData2 m_gameCharData2;

	private SItemCustom[] m_zoneEquipItems;

	private SItemCustom[] m_zoneInvenItems;

	public uint id => 0u;

	public string userId => null;

	public string charSelectName => null;

	public CharacterClassEnum charClass => CharacterClassEnum.GLCC_NONE;

	public int school => 0;

	public int hairType => 0;

	public int hairColorType => 0;

	public int faceType => 0;

	public int level => 0;

	public int lastMap => 0;

	public int unitId => 0;

	public Vector3 position => default;

	public int gateId => 0;

	public List<CharScroll> charScroll => null;

	public List<SCharSkill> charSkill => null;

	public int? partyId => null;

	public int? partyLeaderId => null;

	public int savePointMap => 0;

	public int savePointGate => 0;

	public int previousMap => 0;

	public int previousGate => 0;

	public Vector3 previousPosition => default;

	public uint GetID()
	{
		return 0u;
	}

	public string GetUserID()
	{
		return null;
	}

	public string GetName()
	{
		return null;
	}

	public CharacterClassEnum GetCharClass()
	{
		return CharacterClassEnum.GLCC_NONE;
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

	public int GetLevel()
	{
		return 0;
	}

	public int GetLastMap()
	{
		return 0;
	}

	public int GetUnitID()
	{
		return 0;
	}

	public Vector3 GetPosition()
	{
		return default;
	}

	public int GetGateID()
	{
		return 0;
	}

	public int GetSavePointMap()
	{
		return 0;
	}

	public int GetSavePointGate()
	{
		return 0;
	}

	public int GetPreviousMap()
	{
		return 0;
	}

	public int GetPreviousGate()
	{
		return 0;
	}

	public Vector3 GetPreviousPosition()
	{
		return default;
	}

	public GameCharData2 GetGameCharData2()
	{
		return null;
	}

	public void SetGameCharData2(GameCharData2 data)
	{
	}

	public SItemCustom[] GetZoneEquipItems()
	{
		return null;
	}

	public void SetZoneEquipItems(SItemCustom[] items)
	{
	}

	public SItemCustom[] GetZoneInvenItems()
	{
		return null;
	}

	public void SetZoneInvenItems(SItemCustom[] items)
	{
	}

	public void SetID(uint _id)
	{
	}

	public void SetUserID(string _userId)
	{
	}

	public void SetName(string _name)
	{
	}

	public void SetCharacterClass(CharacterClassEnum _charClass)
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

	public void SetLevel(int _level)
	{
	}

	public void SetLastMap(int _lastMap)
	{
	}

	public void SetUnitID(int _unitId)
	{
	}

	public void SetPostion(Vector3 _position)
	{
	}

	public void SetGateID(int _gateId)
	{
	}

	public void SetCharScroll(List<CharScroll> _scroll)
	{
	}

	public void SetCharSkill(List<SCharSkill> _skill)
	{
	}

	public int? SetPartyId(int? _partyId)
	{
		return null;
	}

	public int? SetPartyLeaderId(int? _partyLeaderId)
	{
		return null;
	}

	public void SetSavePointMap(int _savePointMap)
	{
	}

	public void SetSavePointGate(int _savePointGate)
	{
	}

	public void SetPreviousMap(int _previousMap)
	{
	}

	public void SetPreviousGate(int _previousGate)
	{
	}

	public void SetPreviousPosition(Vector3 _previousPosition)
	{
	}
}
