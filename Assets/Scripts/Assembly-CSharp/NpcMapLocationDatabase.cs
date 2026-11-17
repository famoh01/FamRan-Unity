using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcMapLocations", menuName = "Scriptable Objects/Databases/NPC Map Locations")]
public class NpcMapLocationDatabase : ScriptableObject
{
	[Serializable]
	public class NpcSpawn
	{
		[Tooltip("MobNpcObject.data.mainId — pair with subId to resolve the MobNpcObject for icon/shop data.")]
		public ushort mainId;

		public ushort subId;

		[Tooltip("Display name copied from the MobNpcObject at bake time.")]
		public string npcName;

		[Tooltip("World-space spawn position (CrowData.genPos) — feed straight to UIMinimap.GetScaledPosition.")]
		public Vector3 position;
	}

	[Serializable]
	public class Landmark
	{
		public string name;

		public Vector3 position;
	}

	[Serializable]
	public class MapNpcs
	{
		[Tooltip("Level filename from maplist.json (e.g. w_city_s_01) — the bake key.")]
		public string levelFile;

		[Tooltip("Unity scene name this level file maps to, resolved at bake time. Empty when the client has no scene for the map (server-only maps) — those entries are simply never matched.")]
		public string sceneName;

		[Tooltip("Human-readable map name from maplist.json, for editor inspection only.")]
		public string mapName;

		public List<NpcSpawn> npcs;

		[Tooltip("Named places on this map, from the level file's gates.")]
		public List<Landmark> landmarks;
	}

	public const string ResourcePath = "_RANClient/Data/NpcMap/NpcMapLocations";

	public List<MapNpcs> maps;

	private static NpcMapLocationDatabase _instance;

	public static NpcMapLocationDatabase Instance => null;

	public List<NpcSpawn> GetForScene(string sceneName)
	{
		return null;
	}

	public MapNpcs GetMapForScene(string sceneName)
	{
		return null;
	}
}
