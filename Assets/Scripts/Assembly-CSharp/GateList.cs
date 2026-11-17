using System;
using RAN.Mobile.GameModels;
using UnityEngine;

[Serializable]
public class GateList : MonoBehaviour
{
	public EditGate[] editGate;

	public static GateList instance;

	public void OnValidate()
	{
	}

	public void AddEditGate(EditGate newEditGate, GameObject sphere)
	{
	}

	public void AddGate(GameObject sphere)
	{
	}

	public void MoveToMap(int mapId, int gateId)
	{
	}

	private bool IsLevelRequirementMet(PlayerStatistics playerStats, LevelRequirement reqLevel)
	{
		return false;
	}

	private bool IsItemRequirementMet(InventorySlot[] slots, SNativeID reqItem)
	{
		return false;
	}

	private bool IsSkillRequirementMet(SkillSlot[] slots, SNativeID reqSkill)
	{
		return false;
	}
}
