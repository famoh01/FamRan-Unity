using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Models.Library.Shared.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetSkillUIHandler : MonoBehaviour
{
	[Header("UI Dropdown References")]
	public TMP_Dropdown skillDropdownA;

	public TMP_Dropdown skillDropdownB;

	[Header("UI Slot References")]
	[Tooltip("Drag the Image component of your Slot Prefab A here.")]
	public Image slotIconA;

	[Tooltip("Drag the Image component of your Slot Prefab B here.")]
	public Image slotIconB;

	[CompilerGenerated]
	private Action<int, SNativeID> m_OnSkillASelected;

	[CompilerGenerated]
	private Action<int, SNativeID> m_OnSkillBSelected;

	private List<PetSkillOption> _currentOptionsA;

	private List<PetSkillOption> _currentOptionsB;

	private readonly List<string> _scratchNames;

	public event Action<int, SNativeID> OnSkillASelected
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<int, SNativeID> OnSkillBSelected
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void PopulateDropdownA(List<PetSkillOption> skillOptions)
	{
	}

	public void PopulateDropdownB(List<PetSkillOption> skillOptions)
	{
	}

	private void PopulateDropdown(TMP_Dropdown dropdown, Image slotIcon, List<PetSkillOption> options)
	{
	}

	private void HandleSelectionA(int index)
	{
	}

	private void HandleSelectionB(int index)
	{
	}

	public void SetSelectedIndexSilent(int slot, int index)
	{
	}

	public void SetSlotBInteractable(bool interactable)
	{
	}

	private void UpdateSlotVisual(Image targetIcon, Sprite newSprite)
	{
	}
}
