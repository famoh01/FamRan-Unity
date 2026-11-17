using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Database", menuName = "Scriptable Objects/Databases/New Skill Database")]
public class SkillDatabaseObject : ScriptableObject
{
	[JsonIgnore]
	public SkillObject[] skillObjectList;

	[NonSerialized]
	private List<SkillObject> _skillObjectsList;

	public List<SkillObject> GetSkillObjectsList => null;

	public void OnValidate()
	{
	}
}
