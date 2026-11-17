using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public static ObjectPooler SharedInstance;

	public List<ObjectPoolItem> itemsToPool;

	public List<List<GameObject>> pooledObjectsList;

	public List<GameObject> pooledObjects;

	private List<int> positions;

	private void Awake()
	{
	}

	public GameObject GetPooledObject(int index)
	{
		return null;
	}

	public List<GameObject> GetAllPooledObjects(int index)
	{
		return null;
	}

	public int AddObject(GameObject GO, int amt = 3, bool exp = true)
	{
		return 0;
	}

	private void ObjectPoolItemToPooledObject(int index)
	{
	}
}
