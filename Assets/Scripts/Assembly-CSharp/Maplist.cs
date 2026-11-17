using System;
using System.Collections.Generic;
using UnityEngine;

public class Maplist : MonoBehaviour
{
	public static Maplist instance;

	public MaplistObject mapList;

	private Dictionary<int, Maps> _byMainId;

	private Dictionary<string, Maps> _bySceneName;

	private Maps[] _indexedArray;

	private void Awake()
	{
	}

	private void EnsureIndex()
	{
	}

	public void CharactorMoveToMap(int mapId, int gateId, bool isEnter, Action callBack = null)
	{
	}

	public Maps FindMapById(int mapId)
	{
		return null;
	}

	public int GetCurrentMapID()
	{
		return 0;
	}

	private void LoadMap(Maps mapData, bool isEnter, int gateId, Action callBack = null)
	{
	}

	private void MovePlayerToGate(GateList gateList, int gateId)
	{
	}
}
