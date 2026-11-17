using System.Collections.Generic;
using Models.Library.Shared.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusTravelManager : MonoBehaviour
{
	public static BusTravelManager instance;

	[Header("Panel")]
	public Canvas busTravelPanel;

	public Transform mainParent;

	public Transform stationParent;

	public GameObject mapMainSection;

	public GameObject mapStationSection;

	public Button moveToMap;

	public Button exit;

	private static readonly Color HighlightColor;

	private static readonly Color UnhighlightColor;

	private readonly List<GameObject> _mainPool;

	private readonly List<GameObject> _stationPool;

	private ushort _npcGaeaId;

	private TextMeshProUGUI _highlightedMain;

	private TextMeshProUGUI _highlightedStation;

	private SNativeID _selectedMapId;

	private int _selectedGateId;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void BeginSession(ushort npcGaeaId)
	{
	}

	public void OnBusMain(SNativeID mapId, TextMeshProUGUI buttonText)
	{
	}

	private void OnConfirmTravel()
	{
	}

	public void OnBusStation(SNativeID mapId, int gateId)
	{
	}

	public void OnExitMapPanel()
	{
	}

	public void HighlightMainButtonText(TextMeshProUGUI buttonText)
	{
	}

	public void HighlightStationButtonText(TextMeshProUGUI buttonText)
	{
	}

	public GameObject RentMainButton()
	{
		return null;
	}

	private GameObject RentStationButton()
	{
		return null;
	}

	public void DespawnMains()
	{
	}

	public void DespawnStations()
	{
	}

	private void UnhighlightMain()
	{
	}

	private void UnhighlightStation()
	{
	}

	private void ClearSelection()
	{
	}
}
