using UnityEngine;

[CreateAssetMenu(fileName = "New SkinPiece", menuName = "Scriptable Objects/EditorSkinPiece/New SkinPiece")]
public class SkinPieceObject : ScriptableObject
{
	public Piece piece;

	public SlotBack slotBack;

	public GameObject skinMesh;

	public bool hideHair;

	public bool hideFace;

	[Header("EffectCharacterLevel")]
	public bool used;

	public Texture2D material;

	public Texture2D effectTexture;

	public float power;

	[Header("=")]
	public Texture2D material2;

	public Texture2D effectTexture2;

	public float power2;

	[Header("=")]
	public Texture2D material3;

	public Texture2D effectTexture3;

	public float power3;

	[Header("EffectWeaponCharacterLevel")]
	public Texture2D element1;

	public Texture2D element2;

	public float toggle;

	public float opacity;
}
