using UnityEngine;

public class GameDataManager : MonoBehaviour
{
	public static GameDataManager instance;

	[Header("Player Game data")]
	[SerializeField]
	private PlayerDataSO _playerDataSO;

	[Header("User Account Data")]
	[SerializeField]
	private AccountDataSO _accountDataSO;

	[Header("Team Battle Data")]
	[SerializeField]
	private TeamBattleDataSO _teamBattleDataSO;

	[Header("Character Creation Data")]
	[SerializeField]
	private CharacterCreationDataSO _characterCreationDataSO;

	[Header("Character Select Data")]
	[SerializeField]
	private CharacterSelectDataSO _characterSelectDataSO;

	public PlayerDataSO PlayerData => null;

	public AccountDataSO AccountData => null;

	public TeamBattleDataSO TeamData => null;

	public CharacterCreationDataSO CreationData => null;

	public CharacterSelectDataSO SelectData => null;

	private void Start()
	{
	}

	public void ClearData()
	{
	}

	public void ClearDatas()
	{
	}

	private void Update()
	{
	}
}
