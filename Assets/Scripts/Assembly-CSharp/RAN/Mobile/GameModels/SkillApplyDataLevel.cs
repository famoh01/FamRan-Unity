using System;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public struct SkillApplyDataLevel
	{
		public float delayTime;

		public float life;

		public ushort applyRange;

		public ushort applyNum;

		public ushort applyAngle;

		public ushort pierceNum;

		public ushort tarNum;

		public float basicVar;

		public ushort useArrowNum;

		public ushort useCharmNum;

		public ushort useBulletNum;

		public ushort useExp;

		public ushort useHP;

		public ushort useMP;

		public ushort useSP;

		public ushort useCP;

		public uint data;

		public ushort useHPPty;

		public ushort useMPPty;

		public ushort useSPPty;

		public static SkillApplyDataLevel Create()
		{
			return default;
		}
	}
}
