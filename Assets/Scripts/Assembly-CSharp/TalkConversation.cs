using System;

[Serializable]
public class TalkConversation
{
	public int id;

	public string conversation;

	public TalkCondition condition;

	public TalkAnswer[] answer;
}
