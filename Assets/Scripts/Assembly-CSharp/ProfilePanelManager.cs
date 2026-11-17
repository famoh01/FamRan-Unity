using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePanelManager : MonoBehaviour
{
	public TextMeshProUGUI nameText;

	public TextMeshProUGUI levelText;

	public TextMeshProUGUI classText;

	public TextMeshProUGUI factionText;

	public TextMeshProUGUI classDescText;

	public Image portraitImage;

	public Image skill1Image;

	public Image skill2Image;

	public Image skill3Image;

	public Image weaponImage;

	public Image strengthBarFill;

	public Image agilityBarFill;

	public Image spiritBarFill;

	public static ProfilePanelManager Instance { get; private set; }

	private void Awake()
	{
	}

	public void ShowProfile(CharacterDataHolder data)
	{
	}
}
