using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Ensures a working EventSystem + StandaloneInputModule exists in every scene.
/// The AssetRipper export shipped this as an empty stub, which broke all UI
/// clicks (no EventSystem = no raycasting = buttons never fire).
/// </summary>
public class EventSystemManager : MonoBehaviour
{
	private static EventSystemManager instance;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		EnsureEventSystem();
	}

	private void EnsureEventSystem()
	{
		EventSystem es = Object.FindFirstObjectByType<EventSystem>();
		if (es == null)
		{
			GameObject go = new GameObject("EventSystem");
			es = go.AddComponent<EventSystem>();
		}
		if (es.GetComponent<StandaloneInputModule>() == null)
		{
			es.gameObject.AddComponent<StandaloneInputModule>();
		}
		es.enabled = true;
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		EnsureEventSystem();
	}
}
