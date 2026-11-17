using System.Collections.Generic;
using UnityEngine;

public sealed class SkillEffectPool
{
	public static readonly SkillEffectPool Instance;

	private const int MaxPerPrefab = 10;

	private readonly Dictionary<int, Stack<GameObject>> _pools;

	private readonly Dictionary<int, int> _instanceToPrefab;

	private readonly Dictionary<int, int> _generation;

	private Transform _root;

	private static readonly List<ParticleSystem> s_psBuffer;

	private static readonly List<TrailRenderer> s_trailBuffer;

	private static readonly List<Renderer> s_rendererBuffer;

	private Transform Root => null;

	public GameObject Get(GameObject prefab, Transform parent)
	{
		return null;
	}

	public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		return null;
	}

	public int GetGeneration(GameObject obj)
	{
		return 0;
	}

	public void Return(GameObject obj, int expectedGeneration = -1)
	{
	}

	public void Clear()
	{
	}

	private GameObject TryDequeue(GameObject prefab)
	{
		return null;
	}

	private void Activate(GameObject obj)
	{
	}

	private void BumpGeneration(GameObject obj)
	{
	}

	private GameObject CreateAndTrack(GameObject prefab, Transform parent)
	{
		return null;
	}

	private GameObject CreateAndTrack(GameObject prefab, Vector3 pos, Quaternion rot)
	{
		return null;
	}
}
