using Core.Enumerators;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
	public static ResourcesManager instance;

	private GameObject _chatBubble;

	private GameObject _nameDisplay;

	private GameObject _nameDisplayPet;

	private GameObject _targetThis;

	private GroundItem _groundItem;

	private GroundGold _groundGold;

	private TextAsset _skillData;

	private GameObject _maleGetPrefabClientGO;

	private GameObject _femaleGetPrefabClientGO;

	private GameObject _maleGetPrefabGO;

	private GameObject _femaleGetPrefabGO;

	private Shader _upgradeEffect;

	private Shader _upgradeWeaponEffect;

	private GameObject _enemyEffect01;

	private GameObject _enemyEffect02;

	private GameObject _duelCylinders;

	private GameObject _arrowPlayerBody;

	private GameObject _arrowCrowBody;

	private GameObject _crowArrowTrail;

	public void Awake()
	{
	}

	public GameObject GetPrefab(CharacterClassEnum charClass)
	{
		return null;
	}

	public GameObject GetPrefabClient(CharacterClassEnum charClass)
	{
		return null;
	}

	public GameObject GetChatBubble()
	{
		return null;
	}

	public GameObject GetArrowPlayerBody()
	{
		return null;
	}

	public GameObject GetArrowCrowBody()
	{
		return null;
	}

	public GameObject GetCrowArrowTrail()
	{
		return null;
	}

	public GameObject GetTargetThis()
	{
		return null;
	}

	public GameObject GetNameDisplay()
	{
		return null;
	}

	public GameObject GetPetNameDisplay()
	{
		return null;
	}

	public GroundItem GetItemDrop()
	{
		return null;
	}

	public GroundGold GetGoldDrop()
	{
		return null;
	}

	public TextAsset GetSkillData(string fileName)
	{
		return null;
	}

	public Shader GetUpgradeArmorEffect()
	{
		return null;
	}

	public Shader GetUpgradeWeaponEffect()
	{
		return null;
	}

	public GameObject GetEnemyGroundEffect()
	{
		return null;
	}

	public GameObject GetEnemyBodyEffect()
	{
		return null;
	}

	public GameObject GetDuelCylinders()
	{
		return null;
	}
}
