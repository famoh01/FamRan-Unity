using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AllianceMemberDetail : MonoBehaviour
{
	[Header("Guild Info")]
	public TextMeshProUGUI guildName;

	public GameObject chiefIcon;

	[Header("Buttons")]
	public Button removeButton;

	private uint _clubId;

	private string _clubName;

	public void Awake()
	{
	}

	public void SetMember(uint clubId, string clubName, bool isChief, bool callerIsChief)
	{
	}

	private void ConfirmRemove()
	{
	}
}
