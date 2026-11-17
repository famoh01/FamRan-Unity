using System;
using UnityEngine;

[Serializable]
public class SkillSlot
{
	public int expSkills;

	[NonSerialized]
	public UserInterfaceSkill parent;

	[NonSerialized]
	public GameObject slotDisplay;

	[NonSerialized]
	public GameObject outerSlotDisplay;

	[NonSerialized]
	public SkillData skillHotkeyData;

	[NonSerialized]
	public Action<SkillSlot> onAfterUpdated;

	[NonSerialized]
	public Action<SkillSlot> onBeforeUpdated;

	public Skill skill;

	public float delayTime;

	public SkillObject GetSkillObject()
	{
		return null;
	}

	public SkillSlot(Skill skill)
	{
	}

	public SkillSlot(int key)
	{
	}

	public void RemoveSkill()
	{
	}

	public void UpdateSlot(Skill skillValue)
	{
	}

	public void UpdateSkillOnScroll(int key)
	{
	}

	public void UpdateSkillUI()
	{
	}

	public void OnSkill(SkillSlot skillSlot)
	{
	}
}
