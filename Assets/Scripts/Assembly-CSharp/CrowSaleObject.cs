using Assets.Scripts.Mob_Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "New CrowSale", menuName = "Scriptable Objects/EditorCrowSale/New CrowSale")]
public class CrowSaleObject : ScriptableObject
{
	public string saleType;

	public NpcInventory[] npcInventory;
}
