using System.Collections.Generic;
using Models.Library.Shared.Game;
using UnityEngine;

[RequireComponent(typeof(PetSkillUIHandler))]
public class PetSkillPanelController : MonoBehaviour
{
	[Header("Dependencies")]
	[Tooltip("Auto-resolved from this GameObject if left null.")]
	public PetSkillUIHandler skillUI;

	private readonly List<PetSkillOption> _bufA;

	private readonly List<PetSkillOption> _bufB;

	private bool _scmHooked;

	private bool _uiHooked;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	private void TryHook()
	{
	}

	private void Unhook()
	{
	}

	public void Refresh()
	{
	}

	private static int FindOptionIndex(List<PetSkillOption> opts, SNativeID active)
	{
		return 0;
	}

	private static PetSkillOption NoneOption()
	{
		return default;
	}

	private static void BuildOptions(IReadOnlyList<SNativeID> learned, List<PetSkillOption> dst)
	{
	}

	private void HandleSelectionChanged(int slot, SNativeID id)
	{
	}
}
