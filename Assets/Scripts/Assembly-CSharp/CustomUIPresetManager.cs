using System;
using TMPro;
using UnityEngine;

public class CustomUIPresetManager : MonoBehaviour
{
	[Header("Preset UI")]
	public TMP_Dropdown presetDropdown;

	public Action<int> OnPresetClicked;

	private void Start()
	{
	}

	public void HighlightActivePreset(int index)
	{
	}
}
