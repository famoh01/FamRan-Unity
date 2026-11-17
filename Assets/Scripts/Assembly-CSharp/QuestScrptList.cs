using System.Collections.Generic;
using UnityEngine;

public class QuestScrptList : MonoBehaviour
{
	public QuestDatabaseObject database;

	public static QuestScrptList instance;

	private Dictionary<int, QuestObject> _questLookup;

	private void Awake()
	{
	}

	private void BuildLookup()
	{
	}

	public QuestObject GetQuestObject(int questId)
	{
		return null;
	}
}
