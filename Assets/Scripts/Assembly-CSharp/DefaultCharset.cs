using System.Collections.Generic;
using Core.Enumerators;
using UnityEngine;

public class DefaultCharset : MonoBehaviour
{
	private struct ActiveVfx
	{
		public ParticleSystem instance;

		public ParticleSystem prefab;

		public float releaseAt;
	}

	public static DefaultCharset instance;

	public DefaultCharsetObject defaultCharset;

	private GameObject targetEffect;

	private const float VfxLifetimeSeconds = 5f;

	private const float HitVfxLifetimeSeconds = 2f;

	private Transform _poolRoot;

	private Transform _effectRoot;

	private readonly Dictionary<ParticleSystem, Stack<ParticleSystem>> _vfxPools;

	private readonly List<ActiveVfx> _activeVfx;

	private readonly Dictionary<ushort, ParticleSystem> _towerAuras;

	private Transform EffectRoot => null;

	public static Transform ActiveUnitPointer => null;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void LoadGameConstChar()
	{
	}

	private void SpawnTimedVfx(ParticleSystem prefab, Vector3 position, Quaternion rotation, float lifetime = 5f)
	{
	}

	private ParticleSystem RentFromPool(ParticleSystem prefab)
	{
		return null;
	}

	private void ReturnToPool(ParticleSystem prefab, ParticleSystem instance)
	{
	}

	public void OnEnemyDeath(Transform transform)
	{
	}

	public void OnEnemyRespawn(Transform transform)
	{
	}

	public void OnPetRespawn(Transform transform)
	{
	}

	public void OnPlayerRespawn(float x, float y, float z, Transform transform)
	{
	}

	public void OnPlayerLevelUp(Transform transform)
	{
	}

	public void OnLearnSkill(Transform transform)
	{
	}

	public void OnHitMob(Transform transform)
	{
	}

	public void OnHitElemental(Transform transform, ElementEnum element)
	{
	}

	private void SpawnHitVfx(Transform transform, ParticleSystem prefab)
	{
	}

	private ParticleSystem GetElementalPrefab(ElementEnum element)
	{
		return null;
	}

	public static ElementEnum GetElementFromBlow(StateBlowEnum blow)
	{
		return ElementEnum.EMELEMENT_SPIRIT;
	}

	public void SetTyrannyTowerOwner(ushort crowGlobId, Transform tower, ushort ownerSchool)
	{
	}

	public void ClearTyrannyTowerOwner(ushort crowGlobId)
	{
	}

	public void ClearAllTyrannyTowerAuras()
	{
	}

	private ParticleSystem GetTyrannyTowerPrefab(ushort school)
	{
		return null;
	}

	public void OnUnitPointer(Transform transform)
	{
	}

	public void ClearUnitPointer()
	{
	}

	public void OnDestroyParticle(GameObject particle)
	{
	}
}
