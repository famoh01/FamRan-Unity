using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Teambattle", menuName = "Scriptable Objects/Default Configuration/New Teambattle")]
public class TeamBattleDataSO : ScriptableObject
{
	public Dictionary<string, int> TeamScoreData;

	public int GetTeamScore(string _name)
	{
		return 0;
	}

	public void ClearTeamData()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}
}
