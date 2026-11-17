using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetChecker : MonoBehaviour
{
	public static EnemyTargetChecker Instance;

	public GameObject player;

	public Transform targetThis;

	public Dictionary<GameObject, Transform> enemyTargetThisMap;

	public GameObject enemyTarget;

	public bool isEnemyWithinCollider;

	private string target;

	private CircleRegionProjector _projector;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
	}

	private void OnTriggerExit(Collider other)
	{
	}

	private void CheckClosestEnemy()
	{
	}

	public void OnTargetThis(GameObject enemy)
	{
	}

	public void OffTargetThis(GameObject enemy)
	{
	}

	public void ChangeTargetingColor(int color)
	{
	}
}
