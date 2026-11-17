using Models.Network.Shared.Response;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
	public Transform onGoingPanel;

	public Transform completedPanel;

	public Transform questDetailPanel;

	public Transform questDetailViewPanel;

	public GameObject questItem;

	public Button OnQuest;

	public Button OnQuestExit;

	public Button OnQuestDetailBack;

	public Button OnQuestDetailViewBack;

	public Button OnQuestDetailExit;

	public Button OnQuestDetailViewExit;

	public Canvas parent;

	public GameObject questDetailObj;

	public GameObject questDetailViewObj;

	[Tooltip("The Mission Canvas's UIMultipleTabs (Ongoing / Completed). Assign the one whose Tabs Buttons are onGoingBtn / completedBtn. Optional — leave empty and the tab highlight is left alone.")]
	public UIMultipleTabs missionTabs;

	private void OnDestroy()
	{
	}

	public void RequestGiveUp(int questId)
	{
	}

	public void RequestComplete(int questId)
	{
	}

	private void HandleQuestStateSync(F2C_QuestStateSync sync)
	{
	}

	private void HandleQuestProgress(F2C_QuestProgressUpdate prog)
	{
	}

	private void RepaintIfOpen()
	{
	}

	private void HandleQuestActionResponse(F2C_QuestActionResponse resp)
	{
	}

	public void Start()
	{
	}

	public void ClearQuests(params Transform[] questPanels)
	{
	}

	private void OpenQuestLog()
	{
	}

	public void InitializeQuests()
	{
	}

	private void HandleTextClick(QuestObject questObj, Quest state)
	{
	}

	private void ShowSections(QuestDetails ui, QuestObject questObj, Quest state)
	{
	}

	private void BindTrack(QuestDetails ui, QuestObject questObj, Quest state, bool sectionsWired)
	{
	}

	private static void UpdateTrackLabel(QuestDetails ui, ushort questId)
	{
	}

	private static void PopulateRewards(QuestDetails uiDisplay, QuestObject questObj)
	{
	}

	private static void BindRewardInfo(Transform slot, ItemObject itemObj)
	{
	}

	private static void ShowRewardInfo(ItemObject itemObj)
	{
	}

	private void BindGiveUp(QuestDetails uiDisplay, QuestObject questObj, Quest state)
	{
	}

	private void QuestStepDetail(QuestObject questObj, Quest state)
	{
	}

	private void PopulateQuestSteps(Transform parent, QuestStep[] steps, Quest state, bool useObjectiveText = false)
	{
	}

	private static TextMeshProUGUI FindLabel(Transform row, string childName)
	{
		return null;
	}

	public static string DescribeObjective(QuestStep step)
	{
		return null;
	}

	public static string StripTrailingCount(string text)
	{
		return null;
	}
}
