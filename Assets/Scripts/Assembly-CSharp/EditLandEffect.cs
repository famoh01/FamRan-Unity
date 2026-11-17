using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class EditLandEffect
{
	public int type;

	public float value;

	[JsonIgnore]
	public GameObject box;

	public float minX;

	public float minZ;

	public float maxX;

	public float maxZ;
}
