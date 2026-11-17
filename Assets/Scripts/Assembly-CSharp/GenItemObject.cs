using Assets.Scripts.Mob_Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "New GenItem", menuName = "Scriptable Objects/EditorGenItem/New GenItem")]
public class GenItemObject : ScriptableObject
{
	[Header("Overall Percentage")]
	public float genRate;

	[Header("[max:5] %sum=100%")]
	public SGenNum[] genNum;

	[Header("[max:30] %sum=100%")]
	public SGItemSpec[] itemSpec;

	private void Reset()
	{
	}
}
