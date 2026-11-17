using System;
using Models.Library.Shared.Game;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public struct Spec
	{
		public float var1;

		public float var2;

		public float var3;

		public float var4;

		public uint flag;

		public Models.Library.Shared.Game.SNativeID nativeId;

		public static Spec Create()
		{
			return default;
		}
	}
}
