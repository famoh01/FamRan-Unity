using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestItemDisplay : MonoBehaviour
{
	public static QuestItemDisplay Instance;

	public TextMeshProUGUI questTitle;

	public TextMeshProUGUI questCount;

	public Image questHeader;

	public Image questProgressBar;

	public Sprite success;

	public Sprite onGoing;

	public void Awake()
	{
	}
}
