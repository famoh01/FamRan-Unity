using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HideSet", menuName = "Scriptable Objects/HideSetEditor/New HideSet")]
public class HideSetObject : ScriptableObject
{
	public SkinPieceObject characterDisplayMale;

	public SkinPieceObject characterDisplayFemale;

	public List<string> boneNamesMale;

	public List<string> boneNamesFemale;

	private void OnValidate()
	{
	}
}
