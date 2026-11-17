using System;
using Newtonsoft.Json;
using RAN.Mobile.GameModels;
using UnityEngine;

[Serializable]
public class EditGate
{
	public GateInfo gateIdName;

	public bool gateIn;

	public bool gateOut;

	public SNativeID toMapId;

	public int toGateId;

	public GateGenInterval genInterval;

	[JsonIgnore]
	public GameObject point;

	public Vector3 gatePoint;
}
