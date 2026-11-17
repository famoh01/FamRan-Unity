using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(fileName = "New MobNpc Database", menuName = "Scriptable Objects/Databases/New MobNpc Database")]
public class MobNpcDatabaseObject : ScriptableObject
{
	[JsonIgnore]
	public MobNpcObject[] mobNpcObject;

	[NonSerialized]
	private List<MobNpcObject> _mobNpcObjectList;

	public List<MobNpcObject> GetMobNpcObjectList => null;

	public void OnValidate()
	{
	}
}
