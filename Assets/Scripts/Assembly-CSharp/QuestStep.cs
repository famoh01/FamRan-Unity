using System;

[Serializable]
public class QuestStep
{
	public string title;

	public string description;

	public TalkToNPC talkToNpc;

	public KillMob killMob;

	public QuestReachMap reachMap;

	public QuestDefendNPC defendNpc;

	public ushort reachLevel;

	public MoveToMap moveMap;

	public QuestDefendMap defendMap;

	public GainItemByQuest[] gainItemByQuest;

	public RemoveItemByQuest[] removeItemByQuest;

	public MobGenerateQuestItem mobGenerateQuestItem;
}
