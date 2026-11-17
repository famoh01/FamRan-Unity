using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class UIKillReport : MonoBehaviour
{
	[Header("Killer UI")]
	public Image killerIMG;

	public Image killerSchool;

	public TextMeshProUGUI killerName;

	[Header("Victim UI")]
	public Image victimIMG;

	public Image victimSchool;

	public TextMeshProUGUI victimName;

	[Header("Misc")]
	public Image deathImg;

	private string lastKillerName;

	private string lastVictimName;

	public void BindData(string killerStr, string victimStr, Sprite killerPortrait, Sprite killerSchoolSprite, Sprite victimPortrait, Sprite victimSchoolSprite)
	{
	}

	private static void SetTextIfChanged(TextMeshProUGUI text, ref string cachedValue, string value)
	{
	}

	private static void SetImage(Image image, Sprite sprite)
	{
	}
}
