using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeRowUI : MonoBehaviour
{
	[Header("Optimization")]
	public CanvasGroup canvasGroup;

	[Header("Requirements (Left Side)")]
	public ExchangeRequirementSlot[] requirementSlots;

	[Header("Result Side (Right Side)")]
	public TextMeshProUGUI resultNameText;

	public Image resultIcon;

	public Button resultIconButton;

	[Header("Action")]
	public Button exchangeButton;
}
