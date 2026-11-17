using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class ModifiableInt
{
	[SerializeField]
	private int _baseValue;

	[SerializeField]
	private int _modifiedValue;

	[CompilerGenerated]
	private Action m_ValueModified;

	public int BaseValue
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int ModifiedValue
	{
		get
		{
			return 0;
		}
		private set
		{
		}
	}

	private event Action ValueModified
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

	public ModifiableInt(Action method = null)
	{
	}

	public void RegisterModEvent(Action method)
	{
	}

	public void UnregisterModEvent(Action method)
	{
	}
}
