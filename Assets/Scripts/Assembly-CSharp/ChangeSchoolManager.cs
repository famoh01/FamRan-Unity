using System;
using System.Collections.Generic;
using Core.Enumerators;
using TMPro;
using TrinityRift.Network;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSchoolManager : MonoBehaviour
{
	[Serializable]
	public class SchoolData
	{
		public byte schoolValue;

		public string schoolName;

		[TextArea]
		public string mapDirection;

		[TextArea]
		public string description;

		public Button schoolButton;
	}

	public static ChangeSchoolManager Instance;

	[Header("UI References")]
	public Canvas changeSchoolCanvas;

	public TextMeshProUGUI selectedSchoolNameText;

	public TextMeshProUGUI locationTxt;

	public TextMeshProUGUI descriptionsTxt;

	[Header("School Buttons")]
	public Button sgButton;

	public Button mpButton;

	public Button pnxButton;

	[Header("Footer Buttons")]
	public Button cancelButton;

	public Button saveButton;

	[Header("School Data")]
	public SchoolData[] schools;

	[Header("Selection")]
	public Color selectedButtonColor;

	public int cardSlotIndex;

	private const byte NO_SCHOOL_SELECTED = 255;

	private Dictionary<Button, Color> originalButtonColors;

	private byte selectedSchool;

	private bool waitingForServer;

	private bool uiInitialized;

	private bool sgButtonWired;

	private bool mpButtonWired;

	private bool pnxButtonWired;

	private bool cancelButtonWired;

	private bool saveButtonWired;

	private ServerConnectionManager subscribedServer;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnDestroy()
	{
	}

	public void OpenChangeSchool(int slotIndex)
	{
	}

	public void SelectSG()
	{
	}

	public void SelectMP()
	{
	}

	public void SelectPNX()
	{
	}

	public void SelectSchool(byte schoolValue)
	{
	}

	public void SaveSelectedSchool()
	{
	}

	public void CloseCanvas()
	{
	}

	private void RefreshSchoolDetails()
	{
	}

	private void RefreshButtonState()
	{
	}

	private void InitializeUi()
	{
	}

	private void WireButtonEvents()
	{
	}

	private void HandleChangeSchoolResult(ChangeSchoolResultEnum result, byte newSchool)
	{
	}

	private void ResolveCanvasReference()
	{
	}

	private void ResolveUiReferencesFromCanvas()
	{
	}

	private T FindCanvasComponent<T>(Transform canvasTransform, string path) where T : Component
	{
		return null;
	}

	private void AssignDefaultButtonsToSchools()
	{
	}

	private void CacheButtonColors()
	{
	}

	private SchoolData FindSchool(byte schoolValue)
	{
		return null;
	}

	private byte GetFirstSchoolValue()
	{
		return 0;
	}

	private byte GetCurrentSchool()
	{
		return 0;
	}

	private string GetSchoolName(byte schoolValue)
	{
		return null;
	}

	private string GetResultMessage(ChangeSchoolResultEnum result, byte newSchool)
	{
		return null;
	}

	private void SubscribeToServer()
	{
	}

	private void UnsubscribeFromServer()
	{
	}

	private void ShowFeedback(string message)
	{
	}

	private void ShowFeedback(string message, Color textColor)
	{
	}
}
