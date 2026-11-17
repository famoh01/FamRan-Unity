using UnityEngine;

public static class DevModeDetector
{
	public static bool IsSupportedPlatform => false;

	public static bool IsDeveloperModeEnabled()
	{
		return false;
	}

	private static bool IsAndroidDevMode()
	{
		return false;
	}

	private static int ReadSetting(AndroidJavaObject contentResolver, string settingsClass)
	{
		return 0;
	}
}
