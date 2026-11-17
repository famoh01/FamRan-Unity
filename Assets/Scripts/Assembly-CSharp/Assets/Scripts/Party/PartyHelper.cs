using RAN.Mobile.GameModels;
using UnityEngine;

namespace Assets.Scripts.Party
{
	public static class PartyHelper
	{
		private static int _cachedMapId;

		private static int _cachedMapIdFrame;

		public static bool IsMemberOnLocalMap(PartyMemberInfo member)
		{
			return false;
		}

		private static int CurrentMapId()
		{
			return 0;
		}

		public static void FormatPartyQuestion(GameObject partyQuestionGameObject, string target)
		{
		}

		public static string FormatPartyPlayerPosition(float positionX, float positionZ, string map)
		{
			return null;
		}

		public static string FormatPartyPlayerItemAllocation()
		{
			return null;
		}

		public static string FormatPartyPlayerCoinAllocation()
		{
			return null;
		}
	}
}
