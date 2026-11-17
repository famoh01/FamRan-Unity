using Assets.Scripts.Mob_Scripts;
using Core.Enumerators;
using Newtonsoft.Json;
using RAN.Mobile.GameModels;
using UnityEngine;

[CreateAssetMenu(fileName = "New MobNpc", menuName = "Scriptable Objects/EditorMobNpc/New MobNpc")]
public class MobNpcObject : ScriptableObject
{
	[Header("Settings")]
	public MobNpc data;

	public string Name;

	[JsonIgnore]
	public Sprite IconSource;

	public TribeEnum crowTribe;

	public CrowType crowType;

	public BrightEnum crowBright;

	public PetType petType;

	public SummonType summonType;

	public NpcType npcType;

	[Header("Flags")]
	public bool overlapAttack;

	public bool shortReactionTarget;

	public bool cdCertify;

	public bool invisible;

	public bool detectInvisible;

	public bool obstacle;

	public bool holdPosition;

	public bool holdDirection;

	public bool genNotice;

	public bool bossMob;

	public bool busStation;

	public bool ignoreShock;

	public bool hideName;

	public bool noticeRespawn;

	public bool noticeRespawn2;

	public bool noticeDefeat;

	public bool noticeDefeat2;

	[Header("Stats")]
	public uint hp;

	public ushort mp;

	public ushort sp;

	public float hpRecovery;

	public float mpRecovery;

	public float spRecovery;

	public byte level;

	public ushort defense;

	public ushort avoidRate;

	public ushort hitRate;

	public ushort viewRange;

	public ushort genTime;

	public float bonusExp;

	public ushort bonusCp;

	[Header("ETC")]
	[JsonIgnore]
	public CHFObject skinFile;

	[Tooltip("Resource path for lazy-loading NPC talk data (set via editor tool)")]
	[JsonIgnore]
	public string talkFilePath;

	public CrowSaleObject[] crowSale;

	[Header("NPC Shop (C++ file-backed — supersedes crowSale)")]
	[Tooltip("C++ SCROWACTION.m_strShopFile — the .npcshop filename the ZONE loads at runtime (gamedata/logic/*.npcshop). Serialized to crow.json so the zone's ShopResolver finds it. Set this manually per NPC.")]
	public string shopFileName;

	[Tooltip("Client display copy of the shop, generated from the .npcshop by the NpcShop importer. NOT exported to crow.json — the zone reads the encrypted .npcshop file directly.")]
	[JsonIgnore]
	public NPCShopObject npcShop;

	[Header("NPC Item Exchange (C++ file-backed barter — .npcex)")]
	[Tooltip("C++ SCROWACTION exchange file — the .npcex filename the ZONE loads at runtime (gamedata/logic/*.npcex). Serialized to crow.json so the zone's ExchangeResolver finds it. Set this manually per NPC.")]
	public string exchangeFileName;

	[Tooltip("Client display copy of the item-exchange, generated from the .npcex by the NpcItemExchange importer. NOT exported to crow.json — the zone reads the encrypted .npcex file directly.")]
	[JsonIgnore]
	public NPCItemExchangeObject npcItemExchange;

	public float bodyRadius;

	public ushort walkSpeed;

	public bool run;

	public ushort runSpeed;

	public bool autoDropout;

	public ushort dropoutTime;

	public bool afterDeathNobody;

	[Header("Gen")]
	public string genItemFileName;

	public string qGenItemFileName;

	[Header("GenerateMoney")]
	public ushort moneyValue;

	public ushort moneyRate;

	[Header("Money Luck")]
	public ushort moneyLuckRate;

	public ushort moneyLuckMaxNum;

	[Header("Attack Info")]
	public CrowAttack[] crowAttack;

	public CrowPattern[] crowPattern;

	[Header("Skill")]
	public SNativeID skill;

	public byte skillLevel;

	public CrowSkillTarget skillTarget;

	public Resistance resistance;

	public int reviveGate;

	[Header("Nameplate")]
	[Tooltip("Per-mob vertical nudge for the IGN nameplate, in world units. Negative lowers it; 0 = stock placement, which is correct for every mob whose model is a single mesh sitting on the origin.\n\nNameDisplayPosController takes the encapsulated bounds of EVERY SkinnedMeshRenderer under the mob and uses bounds.size.y as the head height, so a mob carrying a second mesh parked away from the body measures far taller than it looks. Totoy Bato (s_mob_ps_02.fbx) is the known case: its throwable boulder is a 42-vert skinned mesh whose bind pose sits at Y -72..-61, well below the feet, inflating size.y from the body's 23.2 to 95.2 — hence the -72 on that asset. The boulder is real gameplay content, so it can't just be deleted.\n\nCLIENT-ONLY — never exported to crow.json.")]
	[JsonIgnore]
	public float nameplateHeightOffset;

	public MobNpc CreateMobNpc()
	{
		return null;
	}
}
