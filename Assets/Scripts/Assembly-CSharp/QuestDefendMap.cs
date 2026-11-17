using System;
using RAN.Mobile.GameModels;
using UnityEngine;

[Serializable]
public class QuestDefendMap
{
	public string description;

	public SNativeID map;

	public Vector2 position;

	public int radius;

	public int time_minutes;
}
