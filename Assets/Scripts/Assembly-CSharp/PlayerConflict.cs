using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConflict : MonoBehaviour
{
	public static PlayerConflict instance;

	private List<GameObject> cylinderPrefabs;

	private void Awake()
	{
	}

	public void CreateCircularBarrier(GameObject cylinderPrefab, Vector3 centerPosition, Action callBack = null)
	{
	}

	public void RemoveCircularBarrier()
	{
	}
}
