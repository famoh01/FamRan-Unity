using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class LimbVfxLink : MonoBehaviour
{
	[NonSerialized]
	public string holderName;

	[NonSerialized]
	public readonly List<Transform> holders;

	public void SetHoldersActive(bool active)
	{
	}

	public void DestroyHolders()
	{
	}

	private void OnDestroy()
	{
	}
}
