using System;
using System.Text;
using Core.Enumerators;
using Models.Library.Shared.Game;
using RAN.Mobile.GameModels;
using UnityEngine;

public class ItemInfoService : MonoBehaviour
{
	private const string COL_HEADER = "#87cefa";

	private const string COL_NAME = "#98fb98";

	private const string COL_FAIL = "#f44336";

	private const string COL_ORANGE = "#ffa500";

	private const string COL_GOLD = "#ffd700";

	private const string COL_GREY = "#d3d3d3";

	private const string COL_AQUA = "#7fffd4";

	private static readonly StringBuilder _sb;

	private const string COL_UP = "#00ff00";

	private const string COL_DOWN = "#ff4444";

	private static StringBuilder AcquireSB()
	{
		return null;
	}

	public static string GetItemInfo(InventorySlot slot, string namePrefix = null)
	{
		return null;
	}

	private static void AppendNpcSellPrice(StringBuilder sb, ItemObject item, InventorySlot slot)
	{
	}

	public static long ApplyPkBuyRate(long basePrice)
	{
		return 0L;
	}

	public static long ApplyPkSaleRate(long basePrice)
	{
		return 0L;
	}

	private static void AppendCooldownInfo(StringBuilder sb, ItemObject item, InventorySlot slot)
	{
	}

	public static string GetExpirationText(ItemObject item, SItemCustom itemInfo)
	{
		return null;
	}

	private static void AppendExpiration(StringBuilder sb, ItemObject item, SItemCustom itemInfo)
	{
	}

	private static void AppendExpiryLine(StringBuilder sb, ItemExpiration expiration, long bornTicks, string label)
	{
	}

	private static void AppendDuration(StringBuilder sb, TimeSpan ts)
	{
	}

	public static string GetItemLowerInformation(InventorySlot slot)
	{
		return null;
	}

	public static string GetItemDescription(InventorySlot slot)
	{
		return null;
	}

	public static bool IsLocalAdmin()
	{
		return false;
	}

	private static string GetEquipmentInfo(InventorySlot slot)
	{
		return null;
	}

	private static void ResolveEquippedItem(InventorySlot slot, ItemObject invItem, out ItemObject eqItem, out SItemCustom eqCustom)
	{
		eqItem = null;
		eqCustom = null;
	}

	private static void AppendStatWithDiff(StringBuilder sb, string label, int invVal, int eqVal)
	{
	}

	private static void AppendResistancesWithDiff(StringBuilder sb, ItemObject item, SItemCustom itemInfo, ItemObject eqItem, SItemCustom eqCustom)
	{
	}

	private static void AppendResistWithDiff(StringBuilder sb, string label, int baseVal, int addVal, int eqBaseVal, int eqAddVal)
	{
	}

	private static string GetAmmoInfo(InventorySlot slot)
	{
		return null;
	}

	private static string GetCureInfo(InventorySlot slot)
	{
		return null;
	}

	private static string GetGrindingInfo(InventorySlot slot)
	{
		return null;
	}

	private static string GetExpCardInfo(InventorySlot slot)
	{
		return null;
	}

	private static string GetItemCardInfo(InventorySlot slot)
	{
		return null;
	}

	private static string GetPetCardInfo(InventorySlot slot)
	{
		return null;
	}

	private static string GetPetFoodInfo(InventorySlot slot)
	{
		return null;
	}

	private static string GetRandomOptionCardInfo(InventorySlot slot)
	{
		return null;
	}

	private static string GetDefaultItemInfo(InventorySlot slot)
	{
		return null;
	}

	public static string GetItemInformationBox(InventorySlot slot)
	{
		return null;
	}

	private static bool AppendRandomBoxContents(StringBuilder sb, ItemObject item)
	{
		return false;
	}

	private static string BoxItemName(ItemObject boxItem)
	{
		return null;
	}

	private static void AppendHeader(StringBuilder sb, string title)
	{
	}

	private static void AppendColored(StringBuilder sb, string color, string label, int value)
	{
	}

	private static void AppendSign(StringBuilder sb, int value)
	{
	}

	private static void ResolveDamage(ItemObject item, SItemCustom custom, out int min, out int max)
	{
		min = default;
		max = default;
	}

	private static int ResolveDefense(ItemObject item, SItemCustom custom)
	{
		return 0;
	}

	private static void AppendWeaponDamageWithDiff(StringBuilder sb, InventorySlot slot, ItemObject item, SItemCustom itemInfo, ItemObject eqItem, SItemCustom eqCustom)
	{
	}

	private static void AppendArmorDefenseWithDiff(StringBuilder sb, InventorySlot slot, ItemObject item, SItemCustom itemInfo, ItemObject eqItem, SItemCustom eqCustom)
	{
	}

	private static void AppendWeaponDamage(StringBuilder sb, InventorySlot slot, ItemObject item, SItemCustom itemInfo)
	{
	}

	private static void AppendArmorDefense(StringBuilder sb, InventorySlot slot, ItemObject item, SItemCustom itemInfo)
	{
	}

	public static string GetUpgradeWeaponInfo(InventorySlot slot)
	{
		return null;
	}

	public static string GetUpgradeArmorInfo(InventorySlot slot)
	{
		return null;
	}

	private static void AppendClassRequirement(StringBuilder sb, ItemObject item)
	{
	}

	private static int ComputeAptTolerance(ItemObject item)
	{
		return 0;
	}

	private static void AppendStatRequirements(StringBuilder sb, ItemObject item)
	{
	}

	private static void AppendStatLine(StringBuilder sb, string name, int required, int current)
	{
	}

	private static void AppendAddonEffects(StringBuilder sb, EffectAdd eff)
	{
	}

	private static bool HasAddon(EffectAddOn a)
	{
		return false;
	}

	private static void AppendAddon(StringBuilder sb, EffectAddOn addon)
	{
	}

	private static string GetAddonName(EffectAddOnEnum type)
	{
		return null;
	}

	private static void AppendSpecialEffects(StringBuilder sb, ItemObject item)
	{
	}

	private static string GetRateName(EffectRateEnum type)
	{
		return null;
	}

	private static string GetVolumeName(EffectVolumeEnum type)
	{
		return null;
	}

	private static void AppendRandomOptions(StringBuilder sb, SItemCustom itemInfo)
	{
	}

	private static void AppendDisguiseInfo(StringBuilder sb, SItemCustom itemInfo)
	{
	}

	private static void AppendExpMultiple(StringBuilder sb, ItemObject item)
	{
	}

	private static void AppendRandOpt(StringBuilder sb, RandomOptionEnum optType, float value)
	{
	}

	private static bool IsPercentageOpt(RandomOptionEnum opt)
	{
		return false;
	}

	private static string GetRandomOptName(RandomOptionEnum opt)
	{
		return null;
	}

	private static void AppendRandomOptComparison(StringBuilder sb, SItemCustom inv, SItemCustom eq)
	{
	}

	private static float GetOptValueByType(SItemCustom custom, byte optType)
	{
		return 0f;
	}

	private static void CompareDisguise(StringBuilder sb, SItemCustom inv, SItemCustom eq)
	{
	}

	private static ItemSlotEnum WearPosToSlot(WearPositionEnum wearPos)
	{
		return ItemSlotEnum.SLOT_HEADGEAR;
	}

	private static void AppendTurnInfo(StringBuilder sb, InventorySlot slot, ItemObject item)
	{
	}

	public static string GetStatusFlags(InventorySlot slot)
	{
		return null;
	}

	private static void AppendStatusFlags(StringBuilder sb, ItemObject item, SItemCustom custom)
	{
	}

	private static void AppendFlag(StringBuilder sb, string label, bool enabled, bool first)
	{
	}

	private static string GetDrugName(ItemConsumableTypeEnum drug)
	{
		return null;
	}
}
