using System;
using UnityEngine;

[Serializable]
public class ItemBuff
{
	public Attributes stat;

	public int value;

	[SerializeField]
	private int min;

	[SerializeField]
	private int max;

	public int Min => 0;

	public int Max => 0;

	public ItemBuff(int _min, int _max)
	{
	}

	public void AddValue(ref int v)
	{
	}

	public void GenerateField()
	{
	}
}
