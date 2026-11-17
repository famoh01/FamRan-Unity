using Models.Library.Shared.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventStateBar : MonoBehaviour
{
	[Header("UI References")]
	[SerializeField]
	private Image fillBar;

	[SerializeField]
	private TMP_Text timerText;

	[SerializeField]
	private GameObject rootPanel;

	[Header("Colors")]
	[SerializeField]
	private Color playTimeBarColor;

	[SerializeField]
	private Color busterBarColor;

	[SerializeField]
	private Color playTimeTextColor;

	[SerializeField]
	private Color busterTextColor;

	private float _localRemainEvent;

	private float _localRemainBuster;

	private int _lastServerEvent;

	private int _lastServerBuster;

	private bool _wasBuster;

	private int _lastDisplaySecond;

	private float _maxPlayDuration;

	private float _maxBusterDuration;

	private void Update()
	{
	}

	private void SyncServerTimers(GameCharLogic cl)
	{
	}

	private void UpdatePlayTimePhase(GameCharLogic cl, float dt)
	{
	}

	private void UpdateBusterPhase(GameCharLogic cl, float dt)
	{
	}

	private void Hide()
	{
	}

	private static string FormatTime(int totalSeconds)
	{
		return null;
	}
}
