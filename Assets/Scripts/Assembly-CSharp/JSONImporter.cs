using System.Collections.Generic;
using RAN.Mobile.GameModels;
using RAN.Mobile.Helper.Models;
using UnityEngine;

[ExecuteInEditMode]
public class JSONImporter : MonoBehaviour
{
	public static JSONImporter Instance;

	private List<Map> _maps;

	private MobNpcDatabaseObject database;

	public List<Map> Maps => null;

	public int Count => 0;

	private void OnValidate()
	{
	}

	public MobNpcObject GetMobNpcObject(int midId, int subId)
	{
		return null;
	}

	public void TriggerOnValidate()
	{
	}

	public OperationResult<MapRoot> Load(string fullPath, string levelFolder)
	{
		return null;
	}

	public OperationResult<MapInfo> LoadLevelFile(string levelFolderPath)
	{
		return null;
	}

	public Map GetMap(SNativeID sNativeID)
	{
		return null;
	}
}
