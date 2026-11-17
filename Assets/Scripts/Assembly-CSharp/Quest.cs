using System;

[Serializable]
public class Quest
{
	public ushort questId;

	public bool isComplete;

	public bool success;

	public int stepIndex;

	public int killCount;

	public byte stepFlags;

	public const byte StepFlagTalkDone = 1;

	public const byte StepFlagTurnInReady = 64;

	public bool TalkDone => false;

	public bool TurnInReady => false;

	public Quest()
	{
	}

	public Quest(QuestObject quest)
	{
	}
}
