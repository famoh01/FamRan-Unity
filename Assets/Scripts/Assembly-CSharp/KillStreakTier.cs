using System;
using UnityEngine;

[Serializable]
public sealed class KillStreakTier
{
	[Tooltip("Display name — used for logging / testing only. e.g. \"Double Kill\", \"Godlike\".")]
	public string label;

	[Tooltip("Which counter drives this tier.")]
	public KillStreakCategory category;

	[Tooltip("Kill count at which this tier triggers. Ignored for FirstBlood. MultiKill counts kills inside the time window; Spree counts kills since last death.")]
	[Min(1f)]
	public int threshold;

	[Tooltip("The image shown on screen for this tier.")]
	public Sprite image;

	[Tooltip("Optional sounds for this tier. If more than one is assigned, a random variation is played each time the tier triggers. Leave empty for no sound.")]
	public AudioClip[] sounds;

	public AudioClip PickSound()
	{
		return null;
	}
}
