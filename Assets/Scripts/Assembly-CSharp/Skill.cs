using System;
using Models.Library.Shared.Game;

[Serializable]
public class Skill
{
	public ushort mainId;

	public ushort subId;

	public Skill()
	{
	}

	public Skill(SkillObject skill)
	{
	}

	public bool IsEqual(in SNativeID sNativeID)
	{
		return false;
	}

	public bool IsNotEqual(in SNativeID sNativeID)
	{
		return false;
	}

	public bool IsValidSkill()
	{
		return false;
	}
}
