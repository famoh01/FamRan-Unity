using System;
using RAN.Mobile.GameModels;
using UnityEngine;

[Serializable]
public class CrowList
{
	public string name;

	public SNativeID crow;

	public int genTime;

	public int genMinute;

	public int genHour;

	public int reviveGate;

	public bool sunday;

	public bool monday;

	public bool tuesday;

	public bool wednesday;

	public bool thursday;

	public bool friday;

	public bool saturday;

	public Vector3 rotation;

	public Vector3 genPos;

	public WalkPoints[] walkPoints;

	public bool randomGenEnabled;
}
