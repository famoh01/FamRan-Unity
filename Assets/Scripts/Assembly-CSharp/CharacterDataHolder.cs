using UnityEngine;

public class CharacterDataHolder : MonoBehaviour
{
	public string charName;

	public int charLevel;

	public string charClass;

	public string charFaction;

	[Range(0f, 1f)]
	public float strength;

	[Range(0f, 1f)]
	public float agility;

	[Range(0f, 1f)]
	public float spirit;

	[TextArea]
	public string classDescription;

	public Sprite portrait;

	public Sprite skill1;

	public Sprite skill2;

	public Sprite skill3;

	public Sprite equippedWeapon;

	public int characterID;
}
