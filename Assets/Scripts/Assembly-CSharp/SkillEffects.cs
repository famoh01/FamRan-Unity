using System;
using Core.Enumerators;
using UnityEngine;

[Serializable]
public class SkillEffects
{
	public AbnormalConditionEnum EffectType;

	[Header(null)]
	[Header("TARGETS")]
	public GameObject target1;

	public EffectTime targetTime1;

	public EffectPosition targetPos1;

	[Header(null)]
	public GameObject target2;

	public EffectTime targetTime2;

	public EffectPosition targetPos2;

	[Header(null)]
	public GameObject target3;

	public EffectTime targetTime3;

	public EffectPosition targetPos3;

	[Header(null)]
	[Header("SELFS")]
	public GameObject self1;

	public EffectTime selfTime1;

	public EffectPosition selfPos1;

	[Header(null)]
	public GameObject self2;

	public EffectTime selfTime2;

	public EffectPosition selfPos2;

	[Header(null)]
	public GameObject self3;

	public EffectTime selfTime3;

	public EffectPosition selfPos3;

	[Header(null)]
	[Header("TO TARGET")]
	public GameObject toTarget;

	public EffectTime toTargetTime;

	public EffectPosition toTargetPosA;

	public EffectPosition toTargetPosB;

	[Header(null)]
	[Header("BODY")]
	public GameObject selfBody;

	public EffectTime selfBodyTime;

	[Header(null)]
	public GameObject targetBody1;

	public EffectTime tagetBodyTime1;

	[Header(null)]
	public GameObject targetBody2;

	public EffectTime tagetBodyTime2;

	[Header(null)]
	public GameObject defense;
}
