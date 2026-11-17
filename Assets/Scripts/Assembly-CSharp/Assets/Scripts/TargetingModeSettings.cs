using System;
using System.Runtime.CompilerServices;

namespace Assets.Scripts
{
	public static class TargetingModeSettings
	{
		public const string ModePrefKey = "TargetingMode";

		public const string PromptRevisionPrefKey = "TargetingModePromptRevision";

		public const int CurrentPromptRevision = 1;

		[CompilerGenerated]
		private static Action<TargetingSelectionMode> m_Changed;

		public static TargetingSelectionMode Current => TargetingSelectionMode.AutoSnap;

		public static bool HasSavedMode => false;

		public static bool NeedsSelectionPrompt => false;

		public static event Action<TargetingSelectionMode> Changed
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

		public static void Select(TargetingSelectionMode mode)
		{
		}

		private static TargetingSelectionMode Normalize(TargetingSelectionMode mode)
		{
			return TargetingSelectionMode.AutoSnap;
		}

		private static bool IsSupported(int value)
		{
			return false;
		}
	}
}
