using UnityEngine;

namespace Assets.Scripts
{
	[DisallowMultipleComponent]
	public sealed class LootModeSelectionProvider : ModeSelectionProvider
	{
		public override int CurrentValue => 0;

		public override bool HasSavedValue => false;

		public override bool NeedsFirstRunSelection => false;

		public override bool IsSupported(int value)
		{
			return false;
		}

		public override void Select(int value)
		{
		}

		public override string GetDefaultTitle(int value)
		{
			return null;
		}

		public override string GetDefaultDescription(int value)
		{
			return null;
		}
	}
}
