using System;

[Serializable]
public class SkillWindow
{
	public SkillSlot[] Slots;

	public void Clear()
	{
	}

	public bool ContainsSkill(SkillObject skillObject)
	{
		return false;
	}

	public bool ContainsSkill(int midId, int subId)
	{
		return false;
	}
}
