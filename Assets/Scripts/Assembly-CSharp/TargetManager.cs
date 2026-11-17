using System.Collections.Generic;
using Core.Enumerators;
using UnityEngine;
using UnityEngine.UI;

public class TargetManager : MonoBehaviour
{
	public static TargetManager Instance;

	public Button TargetAllowButton;

	private readonly HashSet<uint> _manualForcePkTargets;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void CheckInteractable(ushort targetGaeaId, SchoolEnum school)
	{
	}

	public void SetPKTargets()
	{
	}

	public void SetPKTargets(ushort target)
	{
	}

	public bool IsManualForcePkEnabled(uint charId)
	{
		return false;
	}

	public bool IsForcePkActive(Unit target)
	{
		return false;
	}

	public bool CanUseManualForcePkOnCurrentMap()
	{
		return false;
	}

	public bool CanUseManualForcePkFor(Unit target)
	{
		return false;
	}

	public void SetManualForcePk(Unit target, bool enabled)
	{
	}

	public void ToggleManualForcePk(Unit target)
	{
	}

	public void ConfirmServerHostile(uint charId)
	{
	}

	public void ForgetPlayer(uint charId)
	{
	}

	public void ClearManualForcePkTargets()
	{
	}

	private static bool IsRemotePlayer(Unit target)
	{
		return false;
	}

	public bool IsHostile(uint charId)
	{
		return false;
	}

	public bool IsOffender(ushort targetGaeaId)
	{
		return false;
	}
}
