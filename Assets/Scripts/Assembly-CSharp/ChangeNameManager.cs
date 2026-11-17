using Core.Enumerators;
using TMPro;
using TrinityRift.Network;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNameManager : MonoBehaviour
{
	public static ChangeNameManager Instance;

	[Header("UI References")]
	public Canvas changeNameCanvas;

	public TMP_InputField nameInputField;

	[Header("Footer Buttons")]
	public Button cancelButton;

	public Button saveButton;

	public int cardSlotIndex;

	private bool waitingForServer;

	private bool uiInitialized;

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

	public void OpenChangeName(int slotIndex)
	{
	}

	public void SaveNewName()
	{
	}

	public void CloseCanvas()
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

	private void HandleChangeNameResult(ChangeNameResultEnum result)
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

	private string GetResultMessage(ChangeNameResultEnum result)
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
