using Core.Enumerators;
using Models.Library.Shared.Game;

public struct CrowSpawnData
{
	public SNativeID sNativeID;

	public GameVec3 vPos;

	public GameVec3 vDir;

	public uint dwGlobalID;

	public uint dwNowHP;

	public ushort wNowMP;

	public uint dwActState;

	public ActionEnums emAction;

	public uint targetId;

	public GameVec3 targetPos;

	public uint dwFLAGS;

	public GameVec3 vStartPos;

	public int cornerIndex;
}
