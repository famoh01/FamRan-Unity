using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDetails : MonoBehaviour
{
	public TextMeshProUGUI questTitle;

	public TextMeshProUGUI questDesc;

	public Transform questTaskCont;

	public GameObject questTaskObj;

	public Button questDetail;

	[Tooltip("Optional caption on the track button. Left alone if unassigned — assign it and the button reads trackLabelText / untrackLabelText depending on whether this quest is pinned.")]
	public TextMeshProUGUI questDetailLabel;

	public string trackLabelText;

	public string untrackLabelText;

	public Button questGiveUp;

	public Transform rewardItemsCont;

	public GameObject taskSection;

	public GameObject rewardSection;

	public Button questReward;

	public void Clear()
	{
	}
}
