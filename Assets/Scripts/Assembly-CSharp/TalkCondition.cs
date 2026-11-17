using System;

[Serializable]
public class TalkCondition
{
	public bool isEnable;

	public ClassRequiremnet classRequiremnet;

	public QuestSchoolCondition school;

	public QuestLevel level;

	public QuestElemental elemental;

	public QuestBrightPoints brightPoints;

	public QuestTime time;

	public double minimumMoney;

	public int minimumPartyMembers;

	public QuestRequiredItem[] requiredItem;

	public QuestRequiredSkill[] requiredSkill;

	public QuestMustNotLearnedSkills[] mustNotLearnedSkills;

	public QuestRequiredCompletedQuests[] requiredCompletedQuests;

	public QuestMustNotRunningQuests[] mustNotRunningQuests;

	public QuestRequiredBuff[] requiredBuff;

	public int pQuestId;

	public int questStep;
}
