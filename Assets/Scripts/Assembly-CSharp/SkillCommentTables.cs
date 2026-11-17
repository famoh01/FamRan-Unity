using Core.Enumerators;

public static class SkillCommentTables
{
	public static readonly string[] SkillRole;

	public static readonly string[] SkillActionType;

	public static readonly string[] Bright;

	public static readonly string[] SkillTypes;

	public static readonly float[] SkillTypesScale;

	public static readonly string[] BlowName;

	public static readonly string[] BlowVar1Name;

	public static readonly float[] BlowVar1Scale;

	public static readonly string[] BlowVar2Name;

	public static readonly float[] BlowVar2Scale;

	public static readonly string[] ImpactAddon;

	public static readonly float[] ImpactAddonScale;

	public static readonly string[] SpecAddon;

	public static readonly string[] SpecAddonVar1;

	public static readonly float[] SpecAddonVar1Scale;

	public static readonly string[] SpecAddonVar2;

	public static readonly float[] SpecAddonVar2Scale;

	public static readonly string[] SkillAttack;

	public static readonly (CharacterClassEnum flag, string name)[] CharClassFlagToName;

	public static string GetSkillRole(SkillRoleEnum r)
	{
		return null;
	}

	public static string GetSkillActionType(SkillActionEnum a)
	{
		return null;
	}

	public static string GetBright(BrightEnum b)
	{
		return null;
	}

	public static string GetSkillType(SkillEMTypesEnum t)
	{
		return null;
	}

	public static float GetSkillTypeScale(SkillEMTypesEnum t)
	{
		return 0f;
	}

	public static bool IsSkillTypePercent(SkillEMTypesEnum t)
	{
		return false;
	}

	public static string GetBlowName(StateBlowEnum b)
	{
		return null;
	}

	public static string GetBlowVar1Name(StateBlowEnum b)
	{
		return null;
	}

	public static float GetBlowVar1Scale(StateBlowEnum b)
	{
		return 0f;
	}

	public static bool IsBlowVar1Percent(StateBlowEnum b)
	{
		return false;
	}

	public static string GetBlowVar2Name(StateBlowEnum b)
	{
		return null;
	}

	public static float GetBlowVar2Scale(StateBlowEnum b)
	{
		return 0f;
	}

	public static bool IsBlowVar2Percent(StateBlowEnum b)
	{
		return false;
	}

	public static string GetImpactAddon(SkillImpactAddOnEnum t)
	{
		return null;
	}

	public static float GetImpactAddonScale(SkillImpactAddOnEnum t)
	{
		return 0f;
	}

	public static bool IsImpactAddonPercent(SkillImpactAddOnEnum t)
	{
		return false;
	}

	public static string GetSpecAddon(SkillSpecAddOnEnum t)
	{
		return null;
	}

	public static string GetSpecAddonVar1(SkillSpecAddOnEnum t)
	{
		return null;
	}

	public static float GetSpecAddonVar1Scale(SkillSpecAddOnEnum t)
	{
		return 0f;
	}

	public static bool IsSpecAddon1Percent(SkillSpecAddOnEnum t)
	{
		return false;
	}

	public static string GetSpecAddonVar2(SkillSpecAddOnEnum t)
	{
		return null;
	}

	public static float GetSpecAddonVar2Scale(SkillSpecAddOnEnum t)
	{
		return 0f;
	}

	public static bool IsSpecAddon2Percent(SkillSpecAddOnEnum t)
	{
		return false;
	}

	public static string GetSkillAttack(SkillAttackTypeEnum a)
	{
		return null;
	}

	private static string Lookup(string[] arr, int idx, string fallback)
	{
		return null;
	}

	private static float LookupScale(float[] arr, int idx)
	{
		return 0f;
	}
}
