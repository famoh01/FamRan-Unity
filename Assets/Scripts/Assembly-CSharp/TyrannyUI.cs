using RAN.Mobile.Enumerators;
using TMPro;

internal static class TyrannyUI
{
	public static bool SetTextCached(TextMeshProUGUI text, ref string cache, string value)
	{
		return false;
	}

	public static SchoolEnum ToSchoolEnum(ushort school)
	{
		return SchoolEnum.SacredGate;
	}

	public static string GetSchoolDisplayName(SchoolEnum school)
	{
		return null;
	}

	public static string GetTowerDisplayName(TyrannyScoringManager.TowerIndex tower)
	{
		return null;
	}
}
