using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenericItemInfoView : MonoBehaviour
{
	public TextMeshProUGUI information;

	public Image itemIcon;

	public TextMeshProUGUI description;

	public TextMeshProUGUI lowerInformation;

	public Button equipButton;

	public Button exitButton;

	private string _defaultEquipButtonLabel;

	public static GenericItemInfoView Instance { get; private set; }

	private void Start()
	{
	}

	private void RepositionDialog()
	{
	}

	private void Awake()
	{
	}

	public void ShowItemInfo(bool hide, string itemInformation, string ItemlowerInformation, string itemDescription, Sprite itemImage, Action callBackYes, Action callBackNo)
	{
	}

	public void ShowSkillInfo(bool hide, string itemInformation, string ItemlowerInformation, string itemDescription, Sprite itemImage, Action callBackYes, Action callBackNo)
	{
	}

	public void Show(GameObject itemPos)
	{
	}

	private void AdjustPositionWithinScreen(RectTransform rectTransform)
	{
	}

	public void Hide()
	{
	}

	public void SetEquipButtonLabel(string label)
	{
	}

	public void OverrideActionAsSell(Action onSell)
	{
	}

	private void AddCallBacks(Action callBackYes, Action callBackNo)
	{
	}

	private void DisableAllAutomaticScrolls()
	{
	}
}
