using UnityEngine;

[CreateAssetMenu(fileName = "New Bus Main Table", menuName = "Scriptable Objects/EditorBusTable/New Bus Main Table")]
public class BusMainObject : ScriptableObject
{
	public int randomFlag;

	public int randomCount;

	public StationInfo[] stationInfo;
}
