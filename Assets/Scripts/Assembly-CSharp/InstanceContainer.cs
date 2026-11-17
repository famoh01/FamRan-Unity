using UnityEngine;

public class InstanceContainer : MonoBehaviour
{
	public static InstanceContainer instance;

	public bool isDevMode;

	public InventoryObject inventory;

	public InventoryObject equipment;

	public InventoryObject inventoryHotkey;

	public InventoryObject inventoryConsumableHotkey;

	public InventoryObject inventoryNPC;

	public InventoryObject inventoryTrade;

	public InventoryObject inventoryTradeClient;

	public InventoryObject inventoryVend;

	public InventoryObject locker;

	public InventoryObject bank;

	public InventoryObject itemUpgrade;

	public SkillWindowObject skill;

	public SkillWindowObject skillHotkey;

	public QuestHolderObject quest;

	public MacroChatDataList macroChatDataList;

	public ItemShopObject itemShop;

	private void Start()
	{
	}

	private void Awake()
	{
	}

	public void OnApplicationQuit()
	{
	}
}
