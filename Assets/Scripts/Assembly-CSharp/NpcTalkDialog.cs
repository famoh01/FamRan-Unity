using System;
using Models.Library.Shared.Game;
using Models.Network.Shared.Response;
using TMPro;
using UnityEngine;

public class NpcTalkDialog : MonoBehaviour
{
	public static NpcTalkDialog instance;

	public TextMeshProUGUI talkName;

	public TextMeshProUGUI talkMessage;

	public GameObject talkContent;

	public GameObject talkButton;

	public GameObject selectedNpc;

	private Action _repaintPage;

	private bool _questSyncHooked;

	public const long BrightCleanseCost = 500000L;

	public const int BrightCleansePoints = 30;

	public void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	private void EnsureQuestSyncHook()
	{
	}

	private void OnQuestStateChanged(F2C_QuestStateSync _)
	{
	}

	private void OnQuestProgressChanged(F2C_QuestProgressUpdate _)
	{
	}

	private void RepaintTalkIfOpen()
	{
	}

	public bool CheckClassRequirement(TalkAnswer answer)
	{
		return false;
	}

	public bool CheckLevelRequirement(TalkAnswer answer)
	{
		return false;
	}

	public bool CheckBrightPointsRequirement(TalkAnswer answer)
	{
		return false;
	}

	public bool CheckRequiredRunningQuestRequirement(TalkAnswer answer)
	{
		return false;
	}

	public bool CheckSchoolRequirement(TalkAnswer answer)
	{
		return false;
	}

	public bool CheckRequiredItems(SNativeID itemId, int quantity)
	{
		return false;
	}

	public bool CheckRequiredSkill(SNativeID skillId, int skillLevelReq)
	{
		return false;
	}

	public bool CheckMustNotLearnedSkills(SNativeID skillId)
	{
		return false;
	}

	public bool CheckRequiredBuff(SNativeID skillId, int skillLevel)
	{
		return false;
	}

	public bool ValidRequiredItem(TalkAnswer answer)
	{
		return false;
	}

	public bool ValidRequiredSkill(TalkAnswer answer)
	{
		return false;
	}

	public bool ValidMustNotLearnedSkills(TalkAnswer answer)
	{
		return false;
	}

	public void SetTalkDialog(MobNpcObject mobNpcObject, int array)
	{
	}

	private void OnNpcNextPage(string npcTalkName, int npctalkId, NpctalkObject npcTalkObject)
	{
	}

	public bool CheckAlreadyStartedQuest(int questId)
	{
		return false;
	}

	private static int ProcessQuestId(TalkAnswer answer)
	{
		return 0;
	}

	private Quest FindActiveQuest(int questId)
	{
		return null;
	}

	public bool IsQuestReadyToTurnIn(int questId)
	{
		return false;
	}

	private bool ShouldShowProcessQuest(int questId)
	{
		return false;
	}

	private uint ResolveNpcGaeaId()
	{
		return 0u;
	}

	private void OnStartQuest(int questId)
	{
	}

	private static void Feedback(string message, Color color)
	{
	}

	private void OnProcessQuest(int questId)
	{
	}

	private void OnNpcTradeItem(TalkAnswer answer, ushort stmt, ushort conv, ushort slot)
	{
	}

	private void OnBrightCleanse()
	{
	}

	private void OnNpcShop()
	{
	}

	private void OnNpcFileShop()
	{
	}

	private void OnNpcItemExchange()
	{
	}

	public void OnLocker()
	{
	}

	public void OnLocker(ushort npcId)
	{
	}

	private void OnRideTheBus()
	{
	}

	private void OnCreateClub()
	{
	}

	private void OnUpgradeClub()
	{
	}

	private void OnCertifyClub()
	{
	}

	public void OnExitNpc()
	{
	}

	public void OnTalkNpcDespawned(GameObject npc)
	{
	}

	public void RemoveNpcTalkButtons()
	{
	}

	private void OnSavePosition()
	{
	}

	private void OnHeal()
	{
	}
}
