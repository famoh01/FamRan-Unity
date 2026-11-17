using Models.Library.Shared.Game;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Container", menuName = "Scriptable Objects/Containers/New Skill Container")]
public class SkillWindowObject : ScriptableObject
{
	public string savePath;

	public string savePathJson;

	public InterfaceType type;

	public int SkillTabIndex;

	[SerializeField]
	private SkillWindow Container;

	public SkillSlot[] GetSlots => null;

	public int EmptySlotCount => 0;

	public SkillObject GetSkillObject(int midId, int subId)
	{
		return null;
	}

	public bool AddSkill(Skill skill)
	{
		return false;
	}

	public SkillSlot FindSkillOnSkillWindow(Skill skill)
	{
		return null;
	}

	public SkillSlot GetSkillInSkillWinow(in SNativeID skillID)
	{
		return null;
	}

	public SkillSlot GetSkillInSkillWinow(ushort mainId, ushort subId)
	{
		return null;
	}

	public bool IsSkillInSkillWindow(SkillObject skill)
	{
		return false;
	}

	public SkillSlot GetEmptySlot()
	{
		return null;
	}

	public void DisableSkillToMove()
	{
	}

	public void SwapSkills(SkillSlot skill1, SkillSlot skill2)
	{
	}

	[ContextMenu("Load")]
	public void Load(string fileName)
	{
	}

	[ContextMenu("Save")]
	public void Save()
	{
	}

	public void SaveHotKeyTab(int index)
	{
	}

	public void LoadHotkeyTab(int index)
	{
	}

	public void LoadHotkey()
	{
	}

	[ContextMenu("Clear")]
	public void Clear()
	{
	}

	public void ResetAllHotkeyTabs(int tabCount = 3)
	{
	}
}
