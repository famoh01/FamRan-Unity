using UnityEngine;

[CreateAssetMenu(fileName = "New MMP", menuName = "Scriptable Objects/EditorMMP/New MMP")]
public class MinimapSettingObject : ScriptableObject
{
	public Sprite minimapName;

	public Vector2 multiply;

	public Vector2 division;

	public Vector2 additionToPosition;

	[Tooltip("Z rotation offset (degrees) added to the player indicator on the map.")]
	public float playerInMap;

	[Tooltip("X and Y rotation (degrees) applied to the player indicator on the map.")]
	public Vector2 playerInMapRotation;

	public Vector2 indicatorSizes;
}
