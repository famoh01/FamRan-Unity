using System;
using Models.Library.Shared.Game;

[Serializable]
public class Station
{
	public bool isUsed;

	public int stationId;

	public SNativeID mapId;

	public int gateId;

	public string mapName;

	public string gateName;
}
