using System;
using RAN.Mobile.GameModels;

[Serializable]
public class ItemShop
{
	public SNativeID itemId;

	public ItemShopSection section;

	public int price;

	public int stock;

	public ItemShopCategory category;
}
