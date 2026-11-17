using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapNpcList : MonoBehaviour
{
	private class NpcRow
	{
		public string name;

		public Vector3 position;

		public string place;

		public string direction;
	}

	[Header("Wiring")]
	[Tooltip("Row button prefab. Any TMP_Text under it receives the NPC name.")]
	public Button rowPrefab;

	[Tooltip("Rect the rows are parented to — the scroll view's Content (needs a VerticalLayoutGroup).")]
	public RectTransform contentParent;

	[Tooltip("Panel rect used to swallow taps. Leave empty to use this object's own rect. The map's click-to-walk reads raw Input, so it has to be told which area belongs to the list.")]
	public RectTransform panelRect;

	[Tooltip("The map's Exit button (GameMap/Exit_Map). Leave empty to find it by name under the map canvas at runtime.")]
	public Button exitMapButton;

	[Tooltip("Optional label shown when the current map has no NPCs.")]
	public GameObject emptyState;

	[Header("Behaviour")]
	[Tooltip("Close the game map after tapping a row, so the player can watch the walk.")]
	public bool closeMapOnTravel;

	[Tooltip("Order rows by distance from the player instead of alphabetically. Alphabetical is easier when hunting for a known NPC by name; distance is better for 'what's near me'.")]
	public bool sortByDistance;

	private static MapNpcList _instance;

	private readonly List<Button> _pool;

	private readonly List<NpcRow> _sceneRows;

	private string _builtForScene;

	private const float UnitsPerMetre = 10f;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnDestroy()
	{
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
	}

	private void Invalidate()
	{
	}

	public void Rebuild(bool force = false)
	{
	}

	private void BuildSceneRows(string scene)
	{
	}

	private void BindRows()
	{
	}

	private Button RowAt(int index)
	{
		return null;
	}

	private void HideAllRows()
	{
	}

	private static void BuildRows(NpcMapLocationDatabase.MapNpcs map, List<NpcRow> rows)
	{
	}

	private static string NearestPlace(NpcMapLocationDatabase.MapNpcs map, Vector3 position, string ownName, Dictionary<string, int> nameCounts)
	{
		return null;
	}

	private static void DisambiguatePlaces(List<NpcRow> rows)
	{
	}

	private static void NumberRemainingTies(List<NpcRow> group)
	{
	}

	private string BuildHint(NpcRow row, Vector3 playerPos)
	{
		return null;
	}

	private static string FormatDistance(float worldUnits)
	{
		return null;
	}

	private static string Compass(Vector3 offset)
	{
		return null;
	}

	private void Sort(List<NpcRow> rows, Vector3 from)
	{
	}

	private void TravelTo(Vector3 destination)
	{
	}

	private void CloseMap(UIMinimap minimap)
	{
	}

	private static Vector3 PlayerPosition()
	{
		return default;
	}

	private void ShowEmptyState(bool show)
	{
	}

	public static void EnsureBuilt()
	{
	}

	public static bool IsPointerOverList(Vector2 screenPoint)
	{
		return false;
	}
}
