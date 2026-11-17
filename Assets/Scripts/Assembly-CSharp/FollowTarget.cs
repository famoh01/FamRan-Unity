using UnityEngine;

public class FollowTarget : MonoBehaviour
{
	private Transform _playerSelf;

	private EffectsData _skillEffect;

	private Transform _target;

	private Vector3 fixedPlayerPosition;

	private Vector3 fixedPlayerForward;

	private bool _enemyNoParent;

	private void Start()
	{
	}

	public void SetTarget(Transform playerSelf, EffectsData skillEffect)
	{
	}

	public void SetTargetEnemy(bool enemyNoParent)
	{
	}

	private void CachePlayerTransform()
	{
	}

	private void FixedUpdate()
	{
	}
}
