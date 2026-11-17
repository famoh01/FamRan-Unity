using Models.Network.Shared.Response;
using TMPro;
using UnityEngine;

public class DynamicInterfaceSkill : UserInterfaceSkill
{
	public static DynamicInterfaceSkill Instance;

	public GameObject skillPrefab;

	public UIHUDFunctions uiFunct;

	public GameObject dexParent;

	public GameObject intParent;

	public GameObject powParent;

	public GameObject etcParent;

	[Header("Tab Texts")]
	public TextMeshProUGUI tab1Text;

	public TextMeshProUGUI tab2Text;

	public TextMeshProUGUI tab3Text;

	public TextMeshProUGUI tab4Text;

	private void Awake()
	{
	}

	private new void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnSkillListSync()
	{
	}

	private void OnSkillLearnResponse(F2C_SkillLearnResponse msg)
	{
	}

	private void OnPlayerLevelUp(F2C_LevelUp msg)
	{
	}

	private void OnStatsUp(F2C_StatsUpResponse msg)
	{
	}

	private void RefreshAllSlots()
	{
	}

	private void SyncSlotExpSkills()
	{
	}

	public override void CreateSkillSlots()
	{
	}

	public void UpdateSkills()
	{
	}

	private void UpgradeSkill()
	{
	}
}
