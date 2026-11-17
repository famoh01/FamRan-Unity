using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Database", menuName = "Scriptable Objects/Databases/New Quest Database")]
public class QuestDatabaseObject : ScriptableObject
{
	public QuestObject[] questObject;

	[NonSerialized]
	private List<QuestObject> _questObjectList;

	public List<QuestObject> GetQuestObjectList => null;

	public void OnValidate()
	{
	}
}
