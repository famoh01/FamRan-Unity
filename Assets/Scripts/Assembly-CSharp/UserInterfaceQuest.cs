using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class UserInterfaceQuest : MonoBehaviour
{
	public Dictionary<GameObject, QuestSlot> slotsOnInterface;

	public abstract void CreateQuestSlots();

	protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
	{
	}

	public void OnClick(GameObject obj)
	{
	}
}
