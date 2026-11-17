using System;
using System.Collections.Generic;
using Core.Enumerators;
using Models.Library.Shared.Game;
using Newtonsoft.Json;
using RAN.Mobile.GameModels;
using RAN.Mobile.GameModels.Item;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/EditorItem/New Item")]
public class ItemObject : ScriptableObject
{
	[Serializable]
	public class RandomBoxEntry
	{
		public ushort mainId;

		public ushort subId;

		public float rate;
	}

	public Item data;

	public string Name;

	public byte ItemLevel;

	public uint BuyPrice;

	public uint SellPrice;

	public short specId;

	public bool isGenEnable;

	public bool stackable;

	public ItemTypeEnum type;

	public WearPositionEnum wearPosition;

	public ItemAttackType attackType;

	public ushort attackRange;

	public ushort requiredSP;

	public ItemFlagsObject flags;

	public Models.Library.Shared.Game.ItemExpiration expiration;

	public ClassRequirementFlag characterClassRequirements;

	public UpgradeSettings upgradeSettings;

	public CharacterSchoolRequirements characterSchoolRequirements;

	public CharacterConditionRequirements characterConditionRequirements;

	public PetSettings petSettings;

	public VehicleSettings vehicleSettings;

	public RAN.Mobile.GameModels.Item.ItemCooldownSettings itemCooldownSettings;

	public NpcRecallSettings npcRecallSettings;

	public TeleportMapSettings teleportMapSettings;

	[JsonIgnore]
	public Sprite IconSource;

	[JsonIgnore]
	public GameObject IconFieldSource;

	[JsonIgnore]
	public SkinPieceObject characterDisplay;

	[JsonIgnore]
	public SkinPieceObject characterDisplayF;

	public string randomOptionFileName;

	public RAN.Mobile.GameModels.PAData damage;

	public short defense;

	public short hitRate;

	public short avoidRate;

	public RAN.Mobile.GameModels.Item.ItemStateBlow blow;

	public float expMultiple;

	public RAN.Mobile.GameModels.Resistance elements;

	public Models.Library.Shared.Game.SNativeID skillScroll;

	[Header("Wrapper")]
	[Tooltip("For type=Wrapper: SNativeID of the WrapperBox produced when this card is applied. Mirrors C++ SITEM::sBasicOp::sidWrapperBox.")]
	public Models.Library.Shared.Game.SNativeID wrapperBoxId;

	public RandomBoxEntry[] randomBox;

	public bool twoHanded;

	public bool bigHanded;

	public bool broom;

	public bool rightHand;

	public bool leftHand;

	public bool bothHand;

	[JsonIgnore]
	[TextArea(15, 20)]
	public string description;

	[Header("Consumable")]
	public ItemConsumableTypeEnum consumableType;

	public ushort maxPile;

	public ushort volume;

	public bool ratioVolume;

	[Header("Box")]
	public ItemBox itemBox;

	[JsonIgnore]
	public List<string> boneNames;

	[JsonIgnore]
	public List<string> boneNamesF;

	[Header("Effects AddOn")]
	public EffectAdd effectAddOn;

	[Header("Effects(Rate)")]
	public EffectRate effectRate;

	[Header("Effects(Volume)")]
	public EffectVolume effectVolume;

	[Header("Abnormal Condition")]
	public AbnormalCondition abnormalCondition;

	[Header("Question Item")]
	public RAN.Mobile.GameModels.Item.ItemQuestion itemQuestion;

	public SCharStats requiredStats;

	public RAN.Mobile.GameModels.PAData requiredLevel;

	[JsonIgnore]
	public TeleportMapSettings TeleportMapSettings
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public NpcRecallSettings NpcRecallSettings
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public RAN.Mobile.GameModels.Item.ItemCooldownSettings ItemCooldownSettings
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public VehicleSettings VehicleSettings
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public PetSettings PetSettings
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public CharacterConditionRequirements CharacterConditionRequirements
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public CharacterSchoolRequirements CharacterSchoolRequirements
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public UpgradeSettings UpgradeSettings
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public ClassRequirementFlag CharacterClassRequirements
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public Models.Library.Shared.Game.ItemExpiration Expiration
	{
		get
		{
			return default;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public RAN.Mobile.GameModels.Resistance Elements
	{
		get
		{
			return default;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public ItemFlagsObject Flags
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public RAN.Mobile.GameModels.PAData RequiredLevel
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public SCharStats RequiredStats
	{
		get
		{
			return default;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public RAN.Mobile.GameModels.Item.ItemStateBlow Blow
	{
		get
		{
			return default;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public short HitRate
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public short AvoidRate
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public short Defense
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public RAN.Mobile.GameModels.PAData Damage
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public bool TwoHanded
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public bool BothHand
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public bool LeftHand
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public bool RightHand
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public bool Broom
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public bool BigHanded
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public ItemTypeEnum Type
	{
		get
		{
			return ItemTypeEnum.Food;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public ItemAttackType AttackType
	{
		get
		{
			return ItemAttackType.Nothing;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public ushort AttackRange
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public ushort RequiredSP
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public RAN.Mobile.GameModels.Item.ItemQuestion ItemQuestion
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public EffectAdd EffectAddOn
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public EffectRate EffectRate
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	[JsonIgnore]
	public EffectVolume EffectVolume
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public bool IsWrappable()
	{
		return false;
	}

	public ushort GETAPPLYNUM()
	{
		return 0;
	}

	public uint GETSELLPRICE(ushort turnNum)
	{
		return 0u;
	}

	public bool ISINSTANCE()
	{
		return false;
	}

	public bool ISPILE()
	{
		return false;
	}

	public bool IsTIMELIMIT()
	{
		return false;
	}

	public bool CanEquip(CharacterClassEnum @class)
	{
		return false;
	}

	public Item CreateItem()
	{
		return null;
	}

	private void OnValidate()
	{
	}
}
