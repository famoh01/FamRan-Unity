using System.Text;
using Core.Enumerators;
using Models.Library.Shared.Game;
using UnityEngine;

public class ScrollInfoService : MonoBehaviour
{
	private const string COLOR_LIGHTSKYBLUE = "#87cefa";

	private const string COLOR_PALEGREEN = "#98fb98";

	private const string COLOR_RED = "#f44336";

	private const string COLOR_PRIVATE = "#ffd700";

	private const string COLOR_DEFAULT = "#ffffff";

	private const string COLOR_ENABLE = "#98fb98";

	private const string COLOR_LEARNED_TAG = "#ffa500";

	private static readonly StringBuilder _sb;

	public static string GetScrollInformation(InventorySlot slot)
	{
		return null;
	}

	public static string GetSkillInformation(SkillObject skill, bool includeDescription = true, bool previewNextLevel = false)
	{
		return null;
	}

	private static void BuildSkillInfoInto(SkillObject skill, bool includeDescription, bool previewNextLevel)
	{
	}

	private static void BuildSkillInfo(SkillObject skill, bool hasCharSkill, SCharSkill charSkill, bool isMaster, GameCharLogic charLogic, bool includeDescription, bool previewNextLevel)
	{
	}

	private static string BuildClassList(CharacterClassEnum flags)
	{
		return null;
	}

	private static string BuildBlowFlagList(uint flag)
	{
		return null;
	}

	private static void AppendLearnableStatus(GameCharLogic charLogic, in SNativeID skillID)
	{
	}

	private static void AppendLine(string text, string color)
	{
	}
}
