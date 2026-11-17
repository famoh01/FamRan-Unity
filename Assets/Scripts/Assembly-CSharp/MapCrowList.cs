using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class MapCrowList : MonoBehaviour
{
	public bool disableRotationPosition;

	private Vector3 spherePosition;

	public List<CrowList> crowList;

	private EnemyAiController[] crowObject;

	[JsonIgnore]
	public MobNpcObject crowToRespawn;

	public void GetScreenPos()
	{
	}

	public void InitializedGenPos()
	{
	}

	public void AddCrow(GameObject crow)
	{
	}

	private Vector3 GenerateRandomWalkPoint(Vector3 basePosition)
	{
		return default;
	}

	private bool IsValidWalkPoint(Vector3 position)
	{
		return false;
	}

	public void LoadCrow()
	{
	}
}
