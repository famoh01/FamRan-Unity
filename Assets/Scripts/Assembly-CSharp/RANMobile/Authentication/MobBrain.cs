using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Client-side AI for a locally-spawned mob (attached by MobSpawner through the
	/// WorldRuntime.MobSpawned event). Behaves like the original crows: idle wander
	/// around the spawn point, aggro when the player enters detection, chase, melee on
	/// a cooldown, and leash back home. Movement is transform-driven (the prefab's
	/// NavMeshAgent/CharacterController are disabled by WorldMobView) with a downward
	/// raycast to stay glued to the ground.
	/// </summary>
	public class MobBrain : MonoBehaviour
	{
		public enum State
		{
			Idle,
			Chase,
			Attack,
			Return
		}

		public WorldMobView View;

		[Header("Stats")]
		public int level = 1;

		public int attack = 12;

		public float moveSpeed = 12f;

		public float chaseSpeed = 22f;

		[Header("Detection / leash")]
		public float aggroRange = 30f;

		public float leashRange = 70f;

		public float attackRange = 7f;

		[Header("Timers")]
		public float attackCooldown = 1.6f;

		public float wanderInterval = 3.5f;

		public float wanderRadius = 12f;

		private State _state = State.Idle;

		private Vector3 _spawnPos;

		private Vector3 _wanderTarget;

		private float _nextWander;

		private float _nextAttack;

		private float _groundY;

		private void Start()
		{
			_spawnPos = transform.position;
			_groundY = transform.position.y;
			_wanderTarget = _spawnPos;
			PickWanderTarget();
		}

		/// <summary>Re-anchor the home point (used when the spawner repositions a respawned mob).</summary>
		public void ResetHome(Vector3 home)
		{
			_spawnPos = home;
			_state = State.Idle;
			_wanderTarget = home;
			_nextWander = Time.time + Random.Range(0.5f, wanderInterval);
		}

		private void Update()
		{
			if (View == null || !View.Alive)
			{
				return;
			}
			Transform player = WorldRuntime.LocalPlayer;
			float dt = Time.deltaTime;
			Vector3 toPlayer = Vector3.positiveInfinity;
			float playerDist = float.MaxValue;
			if (player != null)
			{
				toPlayer = player.position - transform.position;
				toPlayer.y = 0f;
				playerDist = toPlayer.magnitude;
			}

			switch (_state)
			{
				case State.Idle:
					TickIdle(dt);
					// Acquire target.
					if (player != null && playerDist <= aggroRange)
					{
						_state = State.Chase;
					}
					break;

				case State.Chase:
					if (player == null || playerDist > leashRange)
					{
						_state = State.Return;
						break;
					}
					if (playerDist <= attackRange)
					{
						_state = State.Attack;
						break;
					}
					MoveToward(player.position, chaseSpeed, dt, toPlayer);
					Face(toPlayer);
					break;

				case State.Attack:
					if (player == null || playerDist > attackRange * 1.25f)
					{
						_state = State.Chase;
						break;
					}
					Face(toPlayer);
					if (Time.time >= _nextAttack)
					{
						_nextAttack = Time.time + attackCooldown;
						// Mob swing → original CALCDAMAGE normal path vs the player's
						// real defense (derived from class stats + gear).
						WorldRuntime.ApplyLocalPlayerDamage(RollMobDamage(), transform.position);
					}
					break;

				case State.Return:
					// Go home, then drop back to idle once there.
					Vector3 homeDelta = _spawnPos - transform.position;
					homeDelta.y = 0f;
					if (homeDelta.magnitude <= 1.5f)
					{
						_state = State.Idle;
						_nextWander = Time.time + Random.Range(0.5f, wanderInterval);
						break;
					}
					MoveToward(_spawnPos, moveSpeed, dt, homeDelta);
					Face(homeDelta);
					// Player re-aggros on the way back only if still very close.
					if (player != null && playerDist <= aggroRange * 0.6f)
					{
						_state = State.Chase;
					}
					break;
			}
			SnapToGround();
		}

		private void TickIdle(float dt)
		{
			if (Time.time >= _nextWander)
			{
				PickWanderTarget();
			}
			Vector3 delta = _wanderTarget - transform.position;
			delta.y = 0f;
			if (delta.magnitude <= 1f)
			{
				return;
			}
			MoveToward(_wanderTarget, moveSpeed * 0.5f, dt, delta);
			Face(delta);
		}

		private void PickWanderTarget()
		{
			_nextWander = Time.time + Random.Range(wanderInterval * 0.6f, wanderInterval * 1.6f);
			Vector2 c = Random.insideUnitCircle * wanderRadius;
			_wanderTarget = _spawnPos + new Vector3(c.x, 0f, c.y);
		}

		private void MoveToward(Vector3 target, float speed, float dt, Vector3 flatDir)
		{
			if (flatDir.sqrMagnitude < 0.0001f)
			{
				return;
			}
			Vector3 step = flatDir.normalized * speed * dt;
			transform.position += step;
			PushMobInfo();
		}

		private void Face(Vector3 flatDir)
		{
			if (flatDir.sqrMagnitude < 0.0001f)
			{
				return;
			}
			Quaternion want = Quaternion.LookRotation(flatDir.normalized, Vector3.up);
			transform.rotation = Quaternion.Slerp(transform.rotation, want, 10f * Time.deltaTime);
			PushMobInfo();
		}

		private void SnapToGround()
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position + Vector3.up * 60f, Vector3.down, out hit, 200f))
			{
				_groundY = hit.point.y;
			}
			Vector3 p = transform.position;
			p.y = Mathf.Lerp(p.y, _groundY, 12f * Time.deltaTime);
			transform.position = p;
		}

		/// <summary>Keep the authoritative MobInfo in sync so the view + server stay consistent.</summary>
		private void PushMobInfo()
		{
			WorldTcpClient.MobInfo info;
			if (View != null && WorldRuntime.MobInfos.TryGetValue(View.Id, out info))
			{
				info.x = transform.position.x;
				info.y = transform.position.y;
				info.z = transform.position.z;
				info.dir = transform.eulerAngles.y;
			}
		}

		/// <summary>
		/// Mob melee roll — the original GLCROWLOGIC::CALCDAMAGE shape: the mob's attack
		/// range (+light variance, as m_nDX_DAMAGE would) rolled against the local player's
		/// derived defense (body + gear), with the shared crit/defense/seed math.
		/// </summary>
		private int RollMobDamage()
		{
			DerivedCharacterStats pd = WorldRuntime.Derived;
			GearTerms gear = CharacterStatBlock.GearFromEquipped(WorldRuntime.Equipped);

			DamageCalculator.AttackContext ctx = new DamageCalculator.AttackContext();
			ctx.Level = level;
			ctx.HpPercent = (WorldRuntime.MaxHp > 0) ? (WorldRuntime.Hp * 100 / WorldRuntime.MaxHp) : 100;
			// Mob attack is a flat range (attack .. attack + swing variance).
			int swing = Mathf.Max(1, attack / 5);
			ctx.DamageLow = attack;
			ctx.DamageHigh = attack + swing;
			ctx.AttackAdd = 0;
			ctx.IsSkill = false;
			ctx.GearCrushPercent = 0;
			ctx.StrikeCount = 1;

			DamageTarget target = new DamageTarget();
			target.Level = WorldRuntime.Level;
			target.Defense = pd.Defense;
			target.BodyDefense = pd.DefenseBody;
			target.ItemDefense = gear.Defense;
			target.HpPercent = (WorldRuntime.MaxHp > 0) ? (WorldRuntime.Hp * 100 / WorldRuntime.MaxHp) : 100;

			DamageFlags flags;
			return DamageCalculator.ComputeDamage(ctx, target, out flags);
		}
	}
}
