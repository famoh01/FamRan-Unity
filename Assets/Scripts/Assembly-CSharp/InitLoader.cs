using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitLoader : MonoBehaviour
{
	public bool isDev;

	public GameObject _devMark;

	[SerializeField]
	private Sprite _bootLoadingSprite;

	public static InitLoader Instance { get; private set; }

	public static bool IsDevMode => false;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		UnityEngine.Debug.Log("[Boot] InitLoader.Start timeScale=" + Time.timeScale);
		if (_devMark != null)
		{
			_devMark.SetActive(isDev);
		}
		StartCoroutine(BootLoadRoutine());
	}

	private IEnumerator BootLoadRoutine()
	{
		// Offline build: skip addressables / game data wait and go straight to login.
		// A stubbed script can zero Time.timeScale, so force it back to 1 first.
		Time.timeScale = 1f;
		UnityEngine.Debug.Log("[Boot] waiting1.5s timeScale=" + Time.timeScale);
		yield return new WaitForSeconds(1.5f);
		UnityEngine.Debug.Log("[Boot] wait done timeScale=" + Time.timeScale + " -> loading login");
		SceneManager.LoadScene("ClientLoginTemp");
		UnityEngine.Debug.Log("[Boot] LoadScene(ClientLoginTemp) issued, active=" + SceneManager.GetActiveScene().name);
	}

	private static bool IsAllGameDataReady()
	{
		return true;
	}

	private static float GetDataReadyFraction()
	{
		return 1f;
	}
}
