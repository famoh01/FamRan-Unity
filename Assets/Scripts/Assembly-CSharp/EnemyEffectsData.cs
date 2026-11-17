using System;
using Core.Enumerators;
using UnityEngine;

[Serializable]
public class EnemyEffectsData
{
	public ElementEnum effectType;

	public GameObject effect;

	[Header("Effect Containers")]
	public bool enemyMainBody;

	public bool enemyFloor;

	public bool enemyNonParent;

	public float skillSpawnDelay;

	[Header("Bool")]
	public bool canBeCancel;

	[Header("Floats")]
	public float effectLifetime;

	public float delayTime;

	public int intValue;

	public EffectTime effectTime;
}
