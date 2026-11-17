using System;
using Core.Enumerators;

namespace RAN.Mobile.GameModels.Item
{
	[Serializable]
	public struct ItemStateBlow
	{
		public StateBlowEnum stateBlow;

		public float rate;

		public float life;

		public float var1;

		public float var2;
	}
}
