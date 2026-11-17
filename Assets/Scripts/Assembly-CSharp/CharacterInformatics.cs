using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInformatics : MonoBehaviour
{
	public Slider healthBar;

	public Slider manaBar;

	public Slider spBar;

	public Slider expBar;

	[Header("UI")]
	public TextMeshProUGUI hpText;

	public TextMeshProUGUI manaText;

	public TextMeshProUGUI spText;

	[Header("EXP")]
	public Image frontXpBar;

	public Image backXpBar;

	public TextMeshProUGUI levelText;

	public TextMeshProUGUI expText;

	public void SetHealth(float value)
	{
	}

	public void SetMaxHealth(float value)
	{
	}

	public void SetMana(float value)
	{
	}

	public void SetMaxMana(float value)
	{
	}

	public void SetSP(float value)
	{
	}

	public void SetMaxSP(float value)
	{
	}

	public void SetExp(float value)
	{
	}

	public void SetMaxExp(float value)
	{
	}

	public void SetXp(float curr, float max)
	{
	}
}
