using System;
using UnityEngine;

[Serializable]
public class GameObjectData
{
	public GameObject gameObject;

	[Header("Effect Containers")]
	public bool rightHand;

	public bool leftHand;

	public bool rightFoot;

	public bool leftFoot;

	[Header("Bool")]
	public bool canBeCancel;

	[Header("Floats")]
	public float delayTime;

	public int intValue;

	public EffectTime effectTime;
}
