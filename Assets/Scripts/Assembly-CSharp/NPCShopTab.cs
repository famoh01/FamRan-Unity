using System;
using UnityEngine;

[Serializable]
public class NPCShopTab
{
	[Tooltip("wID — the tab key referenced by NPCShopItemEntry.type.")]
	public ushort id;

	[Tooltip("strName — the tab label shown in the window.")]
	public string name;
}
