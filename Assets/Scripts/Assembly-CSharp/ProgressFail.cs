using System;
using RAN.Mobile.GameModels;
using UnityEngine;

[Serializable]
public class ProgressFail
{
	public ProgressType type;

	public SNativeID mapId;

	public Vector2 pos;

	public int gateId;
}
