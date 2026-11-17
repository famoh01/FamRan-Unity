using TMPro;
using UnityEngine;

internal sealed class TyrannyCountdown
{
	private readonly int warnThresholdSeconds;

	private readonly Color normalColor;

	private readonly Color warnColor;

	private int cachedWholeSeconds;

	private bool cachedWarn;

	private bool hasWarnState;

	public int CachedWholeSeconds => 0;

	public TyrannyCountdown(int warnThresholdSeconds, Color normalColor, Color warnColor)
	{
	}

	public void Invalidate()
	{
	}

	public void Render(TextMeshProUGUI label, int totalSeconds)
	{
	}
}
