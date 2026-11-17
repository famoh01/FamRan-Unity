using Enumerators;
using TMPro;
using UnityEngine;

public class DxInCommand : MonoBehaviour
{
	public static DxInCommand instance;

	private static bool _localNonRebirth;

	private static readonly string[] s_randOptNames;

	private void Awake()
	{
	}

	private static bool IsLocalAdmin()
	{
		return false;
	}

	private static bool IsAdminOnlyCommand(string text)
	{
		return false;
	}

	private static void LogGmCommandType(string chatText)
	{
	}

	public bool Command(TMP_InputField chatInput)
	{
		return false;
	}

	private static bool TryParseMountCommand(string text)
	{
		return false;
	}

	private static bool TryParseEventFactCommand(string text, out GMEventTypeEnum evType, out ushort value)
	{
		evType = default;
		value = default;
		return false;
	}

	private static bool TryParseStatsUpCommand(string text)
	{
		return false;
	}

	private static bool TryParseGridMoveCommand(string text)
	{
		return false;
	}

	private static bool TryParseGridDeltaCommand(string text)
	{
		return false;
	}

	private static bool TryParseEventStateCommand(string text)
	{
		return false;
	}

	private static bool TryParseSchoolFreePKCommand(string text)
	{
		return false;
	}

	private static bool TryParseTyrannyCommand(string text)
	{
		return false;
	}

	private static bool TryParseClubWarCommand(string text)
	{
		return false;
	}

	private static bool TryParseBrightEventCommand(string text)
	{
		return false;
	}

	private static bool TryParseAddFriendCommand(string text)
	{
		return false;
	}

	private static bool TryParseNonRebirthCommand(string text)
	{
		return false;
	}

	private static bool TryParsePetActionCommand(string text)
	{
		return false;
	}

	private static bool TryParsePetFeedCommand(string text)
	{
		return false;
	}

	private static bool TryParsePetSkillSlotCommand(string text)
	{
		return false;
	}

	private static bool TryParsePetCustomizeCommand(string text)
	{
		return false;
	}

	private static bool TryParsePetAccessoryCommand(string text)
	{
		return false;
	}

	private static bool TryParseSkillCommand(string text)
	{
		return false;
	}

	private static bool TryParseM2PCommand(string text)
	{
		return false;
	}

	private static bool TryParseM2CCommand(string text)
	{
		return false;
	}

	private static bool TryParseGenCharCommand(string text)
	{
		return false;
	}

	private static bool TryParseGetItemCommand(string text)
	{
		return false;
	}

	private static bool TryParseRandomOptType(string token, out byte type)
	{
		type = default;
		return false;
	}

	private static bool TryParseMobGenCommand(string text)
	{
		return false;
	}

	private static bool TryParseMobDelCommand(string text)
	{
		return false;
	}

	private static bool TryParseKickCommand(string text)
	{
		return false;
	}

	private static bool TryParseEventRateCommand(string text)
	{
		return false;
	}
}
