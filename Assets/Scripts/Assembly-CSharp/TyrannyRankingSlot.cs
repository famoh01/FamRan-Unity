using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class TyrannyRankingSlot : MonoBehaviour
{
	[Header("Row UI")]
	[SerializeField]
	private TextMeshProUGUI rankText;

	[SerializeField]
	private Image schoolIcon;

	[SerializeField]
	private Image classIcon;

	[SerializeField]
	private TextMeshProUGUI nameText;

	[SerializeField]
	private TextMeshProUGUI damageText;

	[SerializeField]
	private TextMeshProUGUI healText;

	[SerializeField]
	private TextMeshProUGUI killText;

	[SerializeField]
	private TextMeshProUGUI deathText;

	[SerializeField]
	private TextMeshProUGUI resurrectText;

	[SerializeField]
	private TextMeshProUGUI scoreText;

	[SerializeField]
	private TextMeshProUGUI contribText;

	[Header("Self-Row Highlight")]
	[Tooltip("Enabled only when this slot is bound to the viewing player's own row.")]
	[SerializeField]
	private GameObject selfHighlight;

	private int cachedRank;

	private Sprite cachedSchoolSprite;

	private Sprite cachedClassSprite;

	private string cachedName;

	private long cachedDamage;

	private long cachedHeal;

	private long cachedKill;

	private long cachedDeath;

	private long cachedResurrect;

	private long cachedScore;

	private long cachedContrib;

	private bool? cachedIsSelf;

	public void BindData(int rank, Sprite schoolSprite, Sprite classSprite, string playerName, long damage, long heal, long kill, long death, long resurrect, long scoreTotal, long rewardPoint, bool isSelf)
	{
	}

	private static void SetNumberCached(TextMeshProUGUI text, ref long cache, long value)
	{
	}

	private static void SetSpriteCached(Image image, ref Sprite cache, Sprite sprite)
	{
	}
}
