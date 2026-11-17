using UnityEngine;

[CreateAssetMenu(fileName = "New Bus Station Table", menuName = "Scriptable Objects/EditorBusTable/New Bus Station Table")]
public class BusStationObject : ScriptableObject
{
	public int stationKeySize;

	public Station[] station;
}
