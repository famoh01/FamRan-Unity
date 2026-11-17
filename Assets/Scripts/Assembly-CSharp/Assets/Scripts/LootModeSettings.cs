using System;
using System.Runtime.CompilerServices;

namespace Assets.Scripts
{
	public static class LootModeSettings
	{
		public const string ModePrefKey = "LootMode";

		public const string PromptRevisionPrefKey = "LootModePromptRevision";

		public const int CurrentPromptRevision = 1;

		[CompilerGenerated]
		private static Action<LootMode> m_Changed;

		public static LootMode Current => LootMode.Classic;

		public static bool HasSavedMode => false;

		public static bool NeedsSelectionPrompt => false;

		public static event Action<LootMode> Changed
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static void Select(LootMode mode)
		{
		}

		private static LootMode Normalize(LootMode mode)
		{
			return LootMode.Classic;
		}

		private static bool IsSupported(int value)
		{
			return false;
		}
	}
}
