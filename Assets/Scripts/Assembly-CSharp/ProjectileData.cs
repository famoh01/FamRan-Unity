using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Data", menuName = "Scriptable Objects/ETC/New Projectile Data")]
public class ProjectileData : ScriptableObject
{
	[Header("Attack Prefabs")]
	public GameObject normalPrefab;

	public GameObject firePrefab;

	public GameObject icePrefab;

	public GameObject elecPrefab;

	public GameObject poisnPrefab;

	public GameObject stonePrefab;

	[Header("Elements")]
	public bool isNormal;

	public bool isFire;

	public bool isIce;

	public bool isElec;

	public bool isPoisn;

	public bool isStone;

	public GameObject GetProjectile(bool isFireElement, bool isIceElement)
	{
		return null;
	}
}
