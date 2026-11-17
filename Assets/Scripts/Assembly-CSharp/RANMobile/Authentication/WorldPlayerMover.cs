using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Third-person WASD controller for the world scene. Right-drag orbits the
	/// camera, scroll zooms, LeftShift runs (speeds from class CharConstant:
	/// walk 14 / run 37 in RAN world units). RAN models are ~15 units tall.
	/// </summary>
	public class WorldPlayerMover : MonoBehaviour
	{
		public Camera followCamera;

		public Animator animator;

		public bool RunOverride;

		public bool IsRunning
		{
			get
			{
				return RunOverride || Input.GetKey(KeyCode.LeftShift);
			}
		}

		private CharacterController _cc;

		private float _yaw;

		private float _pitch = 18f;

		private float _distance = 34f;

		private const float WalkSpeed = 14f;

		private const float RunSpeed = 37f;

		private const float Gravity = 120f;

		private static readonly int IsStayParam = Animator.StringToHash("isStay");

		private static readonly int IsWalkParam = Animator.StringToHash("isWalk");

		private static readonly int IsRunParam = Animator.StringToHash("isRun");

		private bool _hasLocomotionParams;

		private Vector3 _lastMousePos;

		private bool _hasMousePos;

		private int _comboIndex;

		private float _comboExpire;

		private static readonly int IsAttack1Param = Animator.StringToHash("isAttack1");

		private static readonly int IsAttack2Param = Animator.StringToHash("isAttack2");

		private static readonly int IsAttack3Param = Animator.StringToHash("isAttack3");

		// Per-weapon-family animator params. Every class controller carries
		// {FAMILY}isStayWeapon / isWalkWeapon / isRunWeapon (locomotion) and {FAMILY}isWeapon1..3
		// (attacks) so the idle + attacks reflect the held weapon (GAUNT / ONESWORD / BOW / SPEAR / ...).
		// WorldPlayerMover previously only drove the shared base params, so every class sat in the
		// same unarmed idle regardless of the weapon held.
		private string _weaponFamily;
		private int _famStay;
		private int _famWalk;
		private int _famRun;
		private readonly int[] _famAtk = new int[3];
		private bool _famParamsReady;
		private float _nextFamilyLog;

		/// <summary>Resolve (and cache) the equipped weapon's animator family params.</summary>
		private void RefreshWeaponFamily()
		{
			string fam = WorldRuntime.EquippedWeaponFamily();
			if (fam == _weaponFamily)
			{
				return; // unchanged (null = unarmed, or this family already resolved)
			}
			_weaponFamily = fam;
			_famParamsReady = false;
			if (string.IsNullOrEmpty(fam) || animator == null)
			{
				return;
			}
			_famStay = Animator.StringToHash(fam + "isStayWeapon");
			_famWalk = Animator.StringToHash(fam + "isWalkWeapon");
			_famRun = Animator.StringToHash(fam + "isRunWeapon");
			_famAtk[0] = Animator.StringToHash(fam + "isWeapon1");
			_famAtk[1] = Animator.StringToHash(fam + "isWeapon2");
			_famAtk[2] = Animator.StringToHash(fam + "isWeapon3");
			foreach (AnimatorControllerParameter p in animator.parameters)
			{
				if (p.nameHash == _famStay)
				{
					_famParamsReady = true;
					break;
				}
			}
		}

		private void Awake()
		{
			_cc = GetComponent<CharacterController>();
			if (_cc == null)
			{
				_cc = gameObject.AddComponent<CharacterController>();
				// Model is ~15 units tall with feet at pivot.
				_cc.height = 14f;
				_cc.radius = 3f;
				_cc.center = new Vector3(0f, 7f, 0f);
				_cc.skinWidth = 0.3f;
			}
			if (animator == null)
			{
				animator = GetComponent<Animator>();
			}
			if (animator != null)
			{
				foreach (AnimatorControllerParameter p in animator.parameters)
				{
					if (p.nameHash == IsWalkParam)
					{
						_hasLocomotionParams = true;
						break;
					}
				}
			}
			if (followCamera == null)
			{
				followCamera = Camera.main;
			}
			if (followCamera != null)
			{
				Vector3 toCam = followCamera.transform.position - transform.position;
				if (toCam.sqrMagnitude > 1f)
				{
					_yaw = Quaternion.LookRotation(toCam).eulerAngles.y;
				}
			}
		}

		private void Update()
		{
			// Rotate with left OR right drag over empty screen area (the original client
			// rotates from the play field; left-drag is the touch equivalent). UI and the
			// joystick are raycastable, so the pointer-over-UI guard keeps taps there from
			// spinning the camera.
			bool rotate =
				Input.GetMouseButton(1) ||
				(Input.GetMouseButton(0) && UnityEngine.EventSystems.EventSystem.current != null &&
					!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject());
			if (rotate)
			{
				Vector3 mp = Input.mousePosition;
				if (_hasMousePos)
				{
					Vector3 d = mp - _lastMousePos;
					_yaw += d.x * 0.25f;
					_pitch = Mathf.Clamp(_pitch - d.y * 0.25f, 5f, 65f);
				}
				_lastMousePos = mp;
				_hasMousePos = true;
			}
			else
			{
				_hasMousePos = false;
			}
			float scroll = Input.GetAxis("Mouse ScrollWheel");
			if (Mathf.Abs(scroll) > 0.001f)
			{
				_distance = Mathf.Clamp(_distance - scroll * 40f, 14f, 80f);
			}
			float h = Input.GetAxisRaw("Horizontal") + WorldHUD.MoveAxis.x;
			float v = Input.GetAxisRaw("Vertical") + WorldHUD.MoveAxis.y;
			if (WorldHUD.ChatOpen)
			{
				h = 0f;
				v = 0f;
			}
			Vector3 wish = Quaternion.Euler(0f, _yaw, 0f) * new Vector3(h, 0f, v);
			if (wish.sqrMagnitude > 1f)
			{
				wish.Normalize();
			}
			bool running = IsRunning;
			float speed = running ? RunSpeed : WalkSpeed;
			Vector3 motion = wish * speed;
			motion.y = -Gravity;
			_cc.Move(motion * Time.deltaTime);
			if (wish.sqrMagnitude > 0.01f)
			{
				Quaternion target = Quaternion.LookRotation(wish, Vector3.up);
				transform.rotation = Quaternion.Slerp(transform.rotation, target, 12f * Time.deltaTime);
			}
			if (_hasLocomotionParams && animator != null)
			{
				RefreshWeaponFamily();
				// probe: is the weapon-family idle actually live in-world? (bow position
				// complaints trace back to this state never activating)
				if (_famParamsReady && Time.unscaledTime >= _nextFamilyLog)
				{
					_nextFamilyLog = Time.unscaledTime + 2f;
					UnityEngine.AnimatorStateInfo st = animator.GetCurrentAnimatorStateInfo(0);
					UnityEngine.Debug.Log("[WeaponAnim] family=" + _weaponFamily +
						" famStay=" + animator.GetBool(_famStay) +
						" inBowStay=" + st.IsName("BOWStay Weapon") +
						" inGauntStay=" + st.IsName("GAUNTStay Weapon") +
						" inStay=" + st.IsName("Stay") + " inIdle=" + st.IsName("Idle"));
				}
				bool moving = wish.sqrMagnitude > 0.01f;
				if (_famParamsReady)
				{
					// Armed: drive the per-weapon-family locomotion (idle/walk/run reflect the
					// held weapon) and clear the base params so the unarmed idle can't win.
					animator.SetBool(_famStay, !moving);
					animator.SetBool(_famWalk, moving && !running);
					animator.SetBool(_famRun, moving && running);
					animator.SetBool(IsStayParam, false);
					animator.SetBool(IsWalkParam, false);
					animator.SetBool(IsRunParam, false);
				}
				else
				{
					animator.SetBool(IsStayParam, !moving);
					animator.SetBool(IsWalkParam, moving && !running);
					animator.SetBool(IsRunParam, moving && running);
				}
			}
			if (Input.GetKeyDown(KeyCode.J))
			{
				AttackOnce();
			}
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				if (WorldHUD.Instance != null) { WorldHUD.Instance.TryUseSkillIndex(0); }
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				if (WorldHUD.Instance != null) { WorldHUD.Instance.TryUseSkillIndex(1); }
			}
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				if (WorldHUD.Instance != null) { WorldHUD.Instance.TryUseSkillIndex(2); }
			}
			if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				if (WorldHUD.Instance != null) { WorldHUD.Instance.TryUseSkillIndex(3); }
			}
			// T is the original client's quest-window shortcut (handled by the HUD)
			if (animator != null && Time.unscaledTime > _comboExpire)
			{
				animator.SetBool(IsAttack1Param, false);
				animator.SetBool(IsAttack2Param, false);
				animator.SetBool(IsAttack3Param, false);
			}
			if (followCamera != null)
			{
				Quaternion orbit = Quaternion.Euler(_pitch, _yaw, 0f);
				Vector3 pivot = transform.position + Vector3.up * 8f;
				Vector3 desired = pivot + orbit * (Vector3.back * _distance);
				followCamera.transform.position = Vector3.Lerp(followCamera.transform.position, desired, 10f * Time.deltaTime);
				followCamera.transform.rotation = Quaternion.Slerp(followCamera.transform.rotation, Quaternion.LookRotation(pivot - followCamera.transform.position), 10f * Time.deltaTime);
			}
		}

		/// <summary>Basic attack (J key or the ATK button) — targets the nearest live mob in range.</summary>
		public void AttackOnce()
		{
			if (Time.unscaledTime > _comboExpire)
			{
				_comboIndex = 1;
			}
			else
			{
				_comboIndex = (_comboIndex % 3) + 1;
			}
			_comboExpire = Time.unscaledTime + 0.6f;
			WorldTcpClient client = WorldNet.Client;
			string target = null;
			WorldMobView mob = WorldRuntime.NearestMob(transform.position, 30f);
			if (mob != null)
			{
				target = mob.Id;
				// Local-authoritative mobs resolve damage here instead of on the server.
				WorldTcpClient.MobInfo info;
				if (WorldRuntime.MobInfos.TryGetValue(mob.Id, out info) && info.local)
				{
					int dmg = RollBasicDamage(mob);
					Vector3 hitPos = mob.transform.position + Vector3.up * 12f;
					WorldRuntime.ApplyLocalMobDamage(mob.Id, dmg, hitPos);
				}
			}
			if (client != null)
			{
				client.SendAttack(_comboIndex, transform.eulerAngles.y, target);
			}
			if (animator != null)
			{
				RefreshWeaponFamily();
				int idx = (_comboIndex == 2) ? 1 : ((_comboIndex == 3) ? 2 : 0); // 0/1/2 -> attack1/2/3
				if (_famParamsReady)
				{
					// Armed: fire the weapon-family attack trigger and clear the base ones.
					animator.SetTrigger(_famAtk[idx]);
					animator.ResetTrigger(IsAttack1Param);
					animator.ResetTrigger(IsAttack2Param);
					animator.ResetTrigger(IsAttack3Param);
				}
				else
				{
					// Base attack params are TRIGGERs — the old SetBool never fired them.
					animator.SetTrigger(idx == 0 ? IsAttack1Param : (idx == 1 ? IsAttack2Param : IsAttack3Param));
				}
			}
			AudioManager.PlaySfx(WorldRuntime.ClassAttackSfx(WorldRuntime.ClassId));
			Debug.Log("[WorldPlayerMover] attack combo " + _comboIndex + (target != null ? (" -> " + target) : string.Empty));
		}

		/// <summary>
		/// Local-authority damage for a basic hit — the original CALCDAMAGE normal-attack
		/// path: (AP + weapon damage + PA/SA) rolled vs the target's defense, with the real
		/// crit/chance math from DamageCalculator. Used only against locally-spawned mobs
		/// (server mobs resolve damage server-side).
		/// </summary>
		private int RollBasicDamage(WorldMobView mob)
		{
			DerivedCharacterStats d = WorldRuntime.Derived;
			GearTerms gear = CharacterStatBlock.GearFromEquipped(WorldRuntime.Equipped);

			DamageCalculator.AttackContext ctx = new DamageCalculator.AttackContext();
			ctx.Level = WorldRuntime.Level;
			ctx.HpPercent = (WorldRuntime.MaxHp > 0) ? (WorldRuntime.Hp * 100 / WorldRuntime.MaxHp) : 100;
			ctx.DamageLow = d.DamageLow + gear.DamageLow;
			ctx.DamageHigh = d.DamageHigh + gear.DamageHigh;
			// Ranged weapons scale with SA, melee with PA (VAR_PARAM is additive).
			ctx.AttackAdd = WorldRuntime.WeaponIsRanged() ? d.Sa : d.Pa;
			ctx.IsSkill = false;
			ctx.IsRanged = WorldRuntime.WeaponIsRanged();
			ctx.GearCritPercent = 0;
			ctx.StrikeCount = 1;

			DamageTarget target = MobTargetFor(mob);
			DamageFlags flags;
			// Crit/crush/shock already fold into the returned magnitude (×1.2 / ×1.3);
			// popup styling stays with ApplyLocalMobDamage.
			return DamageCalculator.ComputeDamage(ctx, target, out flags);
		}

		/// <summary>Build the damage-target view of a local mob (level + defense from its info).</summary>
		private static DamageTarget MobTargetFor(WorldMobView mob)
		{
			DamageTarget t = new DamageTarget();
			WorldTcpClient.MobInfo info;
			if (mob != null && WorldRuntime.MobInfos.TryGetValue(mob.Id, out info))
			{
				t.Level = Mathf.Max(1, info.level);
			}
			else if (mob != null)
			{
				t.Level = WorldRuntime.Level;
			}
			// Local mobs don't carry defense in MobInfo yet — use a level-scaled default
			// so low-level mobs fold fast and bosses feel meatier.
			t.Defense = 2 + t.Level * 2;
			t.BodyDefense = t.Defense;
			t.ItemDefense = 0;
			t.HpPercent = 100;
			return t;
		}
	}
}
