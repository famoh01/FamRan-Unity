using System;
using Core.Enumerators;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public class EffectVolume
	{
		public EffectVolumeEnum type;

		public float volume;
	}
}
