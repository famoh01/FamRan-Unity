using System;
using UnityEngine;

[Serializable]
public class LandEffectList : MonoBehaviour
{
	public EditLandEffect[] editLandEffect;

	public void OnValidate()
	{
	}

	private static Bounds GetRendererBounds(GameObject go)
	{
		return default;
	}
}
