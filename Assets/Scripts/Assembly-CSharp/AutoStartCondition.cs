using System;
using RAN.Mobile.GameModels;

[Serializable]
public class AutoStartCondition
{
	public bool brawlerM;

	public bool swordsmanM;

	public bool archerM;

	public bool shamanM;

	public bool brawlerF;

	public bool swordsmanF;

	public bool archerF;

	public bool shamanF;

	public int requiredLevel;

	public QuestSchoolCondition school;

	public SNativeID item;

	public SNativeID skill;

	public QuestConditionMap map;
}
