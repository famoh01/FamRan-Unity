using UnityEngine;

public class DevModeGuard : MonoBehaviour
{
	private const string Message = "Please turn off Developer Mode in your device settings and restart the game.";

	private bool initialized;

	private bool wasEnabled;

	private void Start()
	{
	}

	private void OnApplicationFocus(bool hasFocus)
	{
	}

	private void OnApplicationPause(bool paused)
	{
	}

	private void Check()
	{
	}

	private static void Quit()
	{
	}
}
