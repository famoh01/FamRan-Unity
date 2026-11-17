using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// One-shot scene fixup: ripped maps contain BoxColliders under negatively
/// scaled transforms (Unity warns the effective box is forced positive and the
/// geometry may be wrong). Replace those colliders with a convex MeshCollider
/// built from the same object's mesh so the collision matches the visual.
/// Run via the MCP bridge: find type WorldSceneFixup, invoke FixActiveScene.
/// </summary>
public static class WorldSceneFixup
{
	public static string FixActiveScene()
	{
		int fixedCount = 0;
		int removedCount = 0;
		GameObject[] roots = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
		foreach (GameObject root in roots)
		{
			foreach (Transform t in root.GetComponentsInChildren<Transform>(true))
			{
				if (!HasNegativeScale(t))
				{
					continue;
				}
				BoxCollider box = t.GetComponent<BoxCollider>();
				if (box == null)
				{
					continue;
				}
				MeshFilter filter = t.GetComponent<MeshFilter>();
				Mesh mesh = (filter != null) ? filter.sharedMesh : null;
				if (mesh != null)
				{
					MeshCollider convex = t.gameObject.AddComponent<MeshCollider>();
					convex.sharedMesh = mesh;
					convex.convex = true;
					Object.DestroyImmediate(box);
					fixedCount++;
					Debug.Log("[WorldSceneFixup] BoxCollider -> convex MeshCollider on " + ScenePath(t));
				}
				else
				{
					Object.DestroyImmediate(box);
					removedCount++;
					Debug.LogWarning("[WorldSceneFixup] removed BoxCollider (no mesh) on " + ScenePath(t));
				}
			}
		}
		if (fixedCount + removedCount > 0)
		{
			EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
		}
		return "fixed=" + fixedCount + " removed=" + removedCount;
	}

	/// <summary>
	/// Batch: open each scene path, run FixActiveScene, save. Returns a summary.
	/// Caller must be okay with the active scene changing.
	/// </summary>
	public static string FixScenes(string[] scenePaths)
	{
		var summary = new System.Text.StringBuilder();
		foreach (string path in scenePaths)
		{
			if (!System.IO.File.Exists(path))
			{
				summary.AppendLine(path + ": MISSING");
				continue;
			}
			EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
			string result = FixActiveScene();
			if (result != "fixed=0 removed=0")
			{
				EditorSceneManager.SaveScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
			}
			summary.AppendLine(path + ": " + result);
		}
		return summary.ToString();
	}

	private static bool HasNegativeScale(Transform t)
	{
		Vector3 s = t.localScale;
		return s.x < 0f || s.y < 0f || s.z < 0f;
	}

	private static string ScenePath(Transform t)
	{
		string path = t.name;
		while (t.parent != null)
		{
			t = t.parent;
			path = t.name + "/" + path;
		}
		return path;
	}
}
