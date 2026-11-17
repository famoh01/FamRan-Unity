using System;
using UnityEngine;

[Serializable]
public class ObjectPoolItem
{
	public GameObject objectToPool;

	public int amountToPool;

	public bool shouldExpand;

	public ObjectPoolItem(GameObject obj, int amt, bool exp = true)
	{
	}
}
