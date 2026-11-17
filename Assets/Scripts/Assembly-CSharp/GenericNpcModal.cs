using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenericNpcModal : MonoBehaviour
{
	public TextMeshProUGUI message;

	public TMP_InputField amountField;

	public Button buttonProceed;

	public Button buttonCancel;

	[Header("Item Information")]
	public Image icon;

	public TextMeshProUGUI price;

	public TextMeshProUGUI tax;

	public TextMeshProUGUI information;

	public TextMeshProUGUI description;

	public TextMeshProUGUI lowerInformation;

	private bool _validateInput;

	private long _itemBuyPrice;

	private long _itemCount;

	private long _goldTextValue;

	private string _defaultProceedLabel;

	public static GenericNpcModal Instance { get; private set; }

	private void Awake()
	{
	}

	private void ResetValidationState()
	{
	}

	private void SetProceedLabel(string label)
	{
	}

	private void OnInputValueChanged(string newValue)
	{
	}

	public void ShowDialog(bool withInputField, Sprite itemIcon, string messageText, string itemPrice, string itemInformation, string itemLowerInformation, string itemDescription, Action callBackYes, Action callBackNo)
	{
	}

	public void Show(bool inputField)
	{
	}

	public void Hide()
	{
	}

	private void AddCallBack(Action callBackYes, Action callBackNo)
	{
	}

	public void ShowVendDialog(bool withInputField, Sprite itemIcon, string messageText, string itemPrice, string itemInformation, string itemLowerInformation, string itemDescription, Action callBackYes, Action callBackNo, string buttonProceedLabel, bool buttonProceedIsActive, int itemCount, long itemBuyPrice, long goldTextValue, bool validatePurchase = true)
	{
	}

	private void CanBuyValidation(int itemToBuy = 0)
	{
	}
}
