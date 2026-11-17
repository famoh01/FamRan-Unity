using UnityEngine;

[CreateAssetMenu(fileName = "New NPCItemExchange", menuName = "Scriptable Objects/NPCItemExchange/New NPCItemExchange")]
public class NPCItemExchangeObject : ScriptableObject
{
	[Tooltip("NPC_EXCHANGE_TITLE — the exchange window title.")]
	public string title;

	[Tooltip("NPC_EXCHANGE_LIST — every barter line (result item + required items).")]
	public NPCItemExchangeEntry[] entries;
}
