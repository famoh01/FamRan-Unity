using Models.Library.Shared.Game;
using Newtonsoft.Json;
using RAN.Mobile.GameModels;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Scriptable Objects/EditorSkill/New Skill")]
public class SkillObject : SkillBase
{
	[JsonIgnore]
	public Sprite IconSource;

	[JsonIgnore]
	public Sprite iconSourceRound;

	[JsonIgnore]
	public GameObject skillProjectile;

	[JsonIgnore]
	public bool applySkillArrowProjectile;

	[JsonIgnore]
	[Tooltip("Seconds to wait before firing the FIRST arrow projectile after cast start. Scaled by castSpeed (delay / castSpeed) so fast casters strike sooner. 0 = fire immediately.")]
	public float applySkillArrowProjectileDelayStart;

	[JsonIgnore]
	[Tooltip("If true, fire multiple arrow projectiles per target (volley / multi-shot). Off = single arrow per target (existing behavior).")]
	public bool applySkillArrowProjectileLoop;

	[JsonIgnore]
	[Tooltip("Total number of arrow projectiles fired per target when Loop is on. 1 (or less) is treated as off. First arrow fires after DelayStart; each subsequent arrow waits LoopDelay.")]
	public int applySkillArrowProjectileLoopAmount;

	[JsonIgnore]
	[Tooltip("Seconds between consecutive looped arrow projectiles per target. Scaled by castSpeed (delay / castSpeed) so fast casters volley faster. 0 = same-frame burst.")]
	public float applySkillArrowProjectileLoopDelay;

	[JsonIgnore]
	[Tooltip("If true, this skill's arrow projectile flies at exactly fArrowSpeed — the ArrowShooter flight-time cap (which speeds far arrows up so they land almost immediately with the damage event) is skipped. For deliberately slow / visible projectiles. Off (default) = capped.")]
	public bool ignoreArrowFlightCap;

	public bool overrideSpawner;

	public GameVec3 spawnerPosition;

	[Space]
	[JsonIgnore]
	public EffectsData[] OnPlayerSkillEffect;

	[JsonIgnore]
	public EnemyEffectsData[] OnEnemySkillEffect;

	public Skill CreateItem()
	{
		return null;
	}
}
