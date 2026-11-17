using UnityEngine;

namespace Assets.Scripts
{
	public abstract class ModeSelectionProvider : MonoBehaviour
	{
		public abstract int CurrentValue { get; }

		public abstract bool HasSavedValue { get; }

		public abstract bool NeedsFirstRunSelection { get; }

		public abstract bool IsSupported(int value);

		public abstract void Select(int value);

		public abstract string GetDefaultTitle(int value);

		public abstract string GetDefaultDescription(int value);
	}
}
