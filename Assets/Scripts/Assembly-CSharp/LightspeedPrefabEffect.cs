using System.Collections.Generic;
using UnityEngine;

public class LightspeedPrefabEffect : MonoBehaviour
{
	[Header("Color Settings")]
	[Tooltip("Multiplied onto the caster's existing colours (white = unchanged).")]
	public Color lightspeedColor;

	[Tooltip("Seconds between checks for a rebuilt combined mesh / buff expiry while the tint is active.")]
	public float recheckInterval;

	[Header("Buff Binding")]
	[Tooltip("Keep the tint for as long as the buff's skill fact is active on the player (not just while this VFX object is alive).")]
	public bool holdWhileBuffActive;

	[Tooltip("Skill id of the buff. Leave both at 0 to auto-detect: the SkillScrptTable entry whose OnPlayerSkillEffect references this prefab.")]
	public int skillMainId;

	public int skillSubId;

	[Tooltip("Seconds to wait for the buff fact to arrive from the server after the cast before giving up on it.")]
	public float factGraceSeconds;

	[Tooltip("Seconds the buff fact may be missing (server re-sync, fact list rebuild) before the tint is considered expired.")]
	public float factLostGraceSeconds;

	[Tooltip("When the buff cannot be bound (unknown skill / no fact), keep the tint this many seconds after the VFX is released. 0 = end with the VFX.")]
	public float fallbackHoldSeconds;

	private LightspeedTintHolder _holder;

	private static readonly Dictionary<string, (int main, int sub)> s_skillByPrefabName;

	public void SetupCaster(Transform casterRoot)
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Begin(Transform root)
	{
	}

	private void ResolveSkillId(out int mainId, out int subId)
	{
		mainId = default;
		subId = default;
	}

	private static Transform FindPlayerRoot(Transform current)
	{
		return null;
	}

	internal static bool Contains(string source, string value)
	{
		return false;
	}
}
