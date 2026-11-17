using RAN.Mobile.GameModels;
using UnityEngine;

public class MapCondition : MonoBehaviour
{
	public RequirementLevel reqLevel;

	public SNativeID reqItem;

	public SNativeID reqSkill;

	public int reqComQuest;

	public int reqActQuest;

	public RequirementLifePoint reqLifePoint;

	public RequirementBrightPoint reqBrightPoint;

	public RequirementActivityPoint reqActPoint;

	public RequirementContributionPoint reqConPoint;

	public bool partyMember;

	public void Start()
	{
	}
}
