using System;
using Core.Enumerators;
using UnityEngine;

[Serializable]
public class EffectsData
{
	public ElementEnum effectType;

	public GameObject effect;

	[Header("Effect Containers")]
	public bool head;

	public bool rightHand;

	public bool leftHand;

	public bool rightFoot;

	public bool leftFoot;

	public bool mainBody;

	public bool mainBodyParent;

	public bool floor;

	public float skillSpawnDelay;

	[Header("Non Parent")]
	public bool nonParent;

	public float distance;

	public float npOffsetX;

	public float npOffsetY;

	public float npOffsetZ;

	[Header("Weapon Container")]
	public bool swordEffect;

	public bool swordBoneEffect;

	public bool stickEffect;

	public bool stickBoneEffect;

	[Header("Bool")]
	public bool canBeDestroy;

	[Header("Floats")]
	public float effectLifetime;

	public float delayTime;

	public int intValue;

	public EffectTime effectTime;
}
