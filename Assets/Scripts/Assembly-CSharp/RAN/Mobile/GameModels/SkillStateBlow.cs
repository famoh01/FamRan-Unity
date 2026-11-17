using System;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public struct SkillStateBlow
	{
		public float rate;

		public float var1;

		public float var2;

		public static SkillStateBlow Create()
		{
			return default;
		}
	}
}
