using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceHPGauge : MonoBehaviour
{
	private Image _hpGauge;

	private float _lastFill;

	internal int _driverHpIndex;

	private NameplateContentDriver _driver;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void Tick()
	{
	}
}
