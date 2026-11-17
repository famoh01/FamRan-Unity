using UnityEngine;

public class JobManager : MonoBehaviour
{
	private static JobManager _instance;

	public static JobManager instance => null;

	private void OnApplicationQuit()
	{
	}
}
