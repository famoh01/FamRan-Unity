using UnityEngine;

[CreateAssetMenu(fileName = "New NPCShop", menuName = "Scriptable Objects/NPCShop/New NPCShop")]
public class NPCShopObject : ScriptableObject
{
	[Tooltip("SHOP_TITLE — the shop window title.")]
	public string title;

	[Tooltip("SHOP_OPTION — currency of the whole shop. 0 = gold (only branch implemented).")]
	public ushort shopType;

	[Tooltip("SHOP_TYPE_LIST — the category tabs.")]
	public NPCShopTab[] tabs;

	[Tooltip("SHOP_ITEM_LIST — every sellable item, each tagged with its tab (NPCShopItemEntry.type).")]
	public NPCShopItemEntry[] items;
}
