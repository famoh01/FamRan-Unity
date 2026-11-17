using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class OptionButtonRow
{
	[Tooltip("Optional. Allow Switch Off is forced off on it, so exactly one button stays selected. Leave empty only if the three buttons are grouped some other way.")]
	public ToggleGroup group;

	public Toggle high;

	public Toggle medium;

	public Toggle low;

	public void Bind()
	{
	}

	public void Push(int index)
	{
	}

	public int Read(string prefKey, int fallback)
	{
		return 0;
	}

	private Toggle For(int index)
	{
		return null;
	}
}
