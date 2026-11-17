using UnityEngine;
using UnityEngine.UI;

public class InGameTools : MonoBehaviour
{
	[Header("BATTERY")]
	[Space(10f)]
	[SerializeField]
	private Slider _batterSlider;

	[SerializeField]
	private Color _highBatteryColor;

	[SerializeField]
	private Color _midBatteryColor;

	[SerializeField]
	private Color _lowBatteryColor;

	private Image _batteryFill;

	private float currentLevel;

	[Header("PING")]
	[Space(10f)]
	[SerializeField]
	private Canvas _pingHigh;

	[SerializeField]
	private Canvas _pingSemiHigh;

	[SerializeField]
	private Canvas _pingMid;

	[SerializeField]
	private Canvas _pingLow;

	[SerializeField]
	private Canvas _pingNoSignal;

	[SerializeField]
	private int currentPing;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void BatteryLevel(float batterLevel)
	{
	}

	private void PingLevel(int _ping)
	{
	}
}
