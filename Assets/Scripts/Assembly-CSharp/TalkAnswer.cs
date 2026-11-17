using System;
using UnityEngine;

[Serializable]
public class TalkAnswer
{
	public int id;

	public string answer;

	public TalkType actionType;

	public int actionData;

	public TalkBasicDesc function;

	[Space]
	[Header("If data is Trade Item")]
	public TalkTradeItem tradeItem;

	[Space]
	[Header("If data is Random NPCTalk")]
	public TalkRandomTalk randomTalk;

	[Space]
	[Header("If type is Start Quest")]
	public int sQuestID;

	[Space]
	[Header("if type is Progress Quest")]
	public int pQuestId;

	public int questStep;

	[Space]
	public TalkCondition talkCondition;
}
