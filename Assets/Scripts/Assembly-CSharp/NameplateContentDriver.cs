using System.Collections.Generic;
using UnityEngine;

public sealed class NameplateContentDriver : MonoBehaviour
{
	private static NameplateContentDriver _instance;

	private static int s_generation;

	public const float BackstopInterval = 0.25f;

	private readonly List<IGNScript> _registry;

	private readonly List<IGNWithVendScript> _vendRegistry;

	private readonly List<WorldSpaceUiConstantSize> _scaleRegistry;

	private readonly List<WorldSpaceHPGauge> _hpRegistry;

	public static NameplateContentDriver Instance => null;

	public static int Generation => 0;

	public static void MarkAllDirty()
	{
	}

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void Register(IGNScript ign)
	{
	}

	public void Unregister(IGNScript ign)
	{
	}

	public void RegisterVend(IGNWithVendScript vend)
	{
	}

	public void UnregisterVend(IGNWithVendScript vend)
	{
	}

	public void RegisterScaler(WorldSpaceUiConstantSize scaler)
	{
	}

	public void UnregisterScaler(WorldSpaceUiConstantSize scaler)
	{
	}

	public void RegisterHpGauge(WorldSpaceHPGauge gauge)
	{
	}

	public void UnregisterHpGauge(WorldSpaceHPGauge gauge)
	{
	}

	private void Update()
	{
	}
}
