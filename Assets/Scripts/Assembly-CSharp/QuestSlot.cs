using System;

[Serializable]
public class QuestSlot
{
	[NonSerialized]
	public UserInterfaceQuest parent;

	[NonSerialized]
	public Action<QuestSlot> onAfterUpdated;

	[NonSerialized]
	public Action<QuestSlot> onBeforeUpdated;

	public Quest quest;

	public QuestObject GetQuestObject()
	{
		return null;
	}

	public QuestSlot()
	{
	}

	public QuestSlot(Quest quest, bool isComplete)
	{
	}

	public void RemoveQuest()
	{
	}

	public void UpdateSlot(Quest questSlot, bool isCompleted)
	{
	}
}
