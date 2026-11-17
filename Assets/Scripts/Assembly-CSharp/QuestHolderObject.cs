using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Container", menuName = "Scriptable Objects/Containers/New Quest Container")]
public class QuestHolderObject : ScriptableObject
{
	public string savePath;

	public QuestDatabaseObject database;

	[SerializeField]
	private QuestHolder Container;

	public QuestSlot[] GetQuest => null;

	[ContextMenu("Save")]
	public void Save()
	{
	}

	[ContextMenu("Load")]
	public void Load()
	{
	}

	[ContextMenu("Clear")]
	public void Clear()
	{
	}
}
