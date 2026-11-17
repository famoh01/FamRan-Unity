using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Scriptable Objects/EditorQuest/New Quest")]
public class QuestObject : ScriptableObject
{
	public Quest data;

	public QuestArea questArea;

	public string questTitle;

	[TextArea(3, 10)]
	public string questDescription;

	public bool isRepeatable;

	public bool isCanRetry;

	public bool isCanGiveUp;

	public bool isParty;

	public bool isPartyQuest;

	public bool isOnlyProgress;

	public PracticeCondition practiceCondition;

	public PreCondition preCondition;

	public AutoStartCondition autoStartCondition;

	public QuestReward reward;

	public QuestProgress questProgress;

	public QuestStep[] questStep;
}
