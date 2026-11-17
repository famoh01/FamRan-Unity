using Models.Library.Shared.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour
{
	public static DisplayStats Instance;

	public Canvas playerStatsCanvas;

	public TextMeshProUGUI nameTxt;

	public TextMeshProUGUI skillPoint;

	[Header("EXP")]
	public Image xpBar;

	public TextMeshProUGUI xpTxt;

	[Header("Main Stats")]
	public TextMeshProUGUI powStat;

	public TextMeshProUGUI dexStat;

	public TextMeshProUGUI intStat;

	public TextMeshProUGUI vitStat;

	public TextMeshProUGUI stmStat;

	public TextMeshProUGUI statsPoints;

	public GameObject[] statsBtns;

	[Header("Character Condition")]
	public TextMeshProUGUI lvlTxt;

	public TextMeshProUGUI atkTxt;

	public TextMeshProUGUI defTxt;

	public TextMeshProUGUI meleeTxt;

	public TextMeshProUGUI rangeTxt;

	public TextMeshProUGUI nrgTxt;

	public TextMeshProUGUI hpTxt;

	public TextMeshProUGUI mpTxt;

	public TextMeshProUGUI spTxt;

	public TextMeshProUGUI hitRateTxt;

	public TextMeshProUGUI avoidTxt;

	public TextMeshProUGUI dayTxt;

	[Header("Elemental Resistance")]
	public TextMeshProUGUI fireTxt;

	public TextMeshProUGUI elecTxt;

	public TextMeshProUGUI windTxt;

	public TextMeshProUGUI iceTxt;

	public TextMeshProUGUI poisonTxt;

	[Header("Character Personality")]
	public TextMeshProUGUI schoolTxt;

	public TextMeshProUGUI classTxt;

	public TextMeshProUGUI lpTxt;

	public TextMeshProUGUI contribTxt;

	public TextMeshProUGUI rankTxt;

	public TextMeshProUGUI statusTxt;

	public Slider atrBar;

	public TextMeshProUGUI pkScorTxt;

	public TextMeshProUGUI pkDeathTxt;

	public TextMeshProUGUI pkResuTxt;

	public TextMeshProUGUI gameTimeTxt;

	private bool isCanvasOpen;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	public void SetXp(float curr, float max)
	{
	}

	public void SetAtr(float value)
	{
	}

	public void RequireStatsUp(SCharStats stats)
	{
	}

	public void OnClickAddPow()
	{
	}

	public void OnClickAddDex()
	{
	}

	public void OnClickAddInt()
	{
	}

	public void OnClickAddVit()
	{
	}

	public void OnClickAddStm()
	{
	}

	public void CloseCanvas()
	{
	}
}
