using System.Collections.Generic;
using RAN.Mobile.GameModels;
using RAN.Mobile.Helper.Models;
using UnityEngine;

public class GameMapHandler : MonoBehaviour
{
	public static GameMapHandler Instance;

	private List<Map> _maps;

	public List<Map> Maps => null;

	public int Count => 0;

	private void Awake()
	{
	}

	public OperationResult<MapRoot> Load(string fullPath, string levelFolder)
	{
		return null;
	}

	public OperationResult<MapInfo> LoadLevelFile(string levelFolderPath, string filename)
	{
		return null;
	}

	public Map GetMap(SNativeID sNativeID)
	{
		return null;
	}
}
