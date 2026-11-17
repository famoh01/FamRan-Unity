using System.Collections.Generic;
using Models.Library.Shared.Game;
using Newtonsoft.Json;
using RAN.Mobile.GameModels;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Charset", menuName = "Scriptable Objects/EditorCharset/New Default Charset")]
public class DefaultCharsetObject : ScriptableObject
{
	public string schoolName00;

	public Models.Library.Shared.Game.SNativeID startMap00;

	public byte startGate00;

	public string schoolName01;

	public Models.Library.Shared.Game.SNativeID startMap01;

	public byte startGate01;

	public string schoolName02;

	public Models.Library.Shared.Game.SNativeID startMap02;

	public byte startGate02;

	public bool partyToOtherSchool;

	public bool clubToOtherSchool;

	public byte maxLevel;

	public ushort maxStatsP;

	public CharsetObject[] classInit0;

	public CharsetObject[] classInit1;

	public CharsetObject[] classInit2;

	[JsonIgnore]
	public ParticleSystem psLEVELUP_EFFECT;

	[JsonIgnore]
	public ParticleSystem psSELECT_CHAR;

	[JsonIgnore]
	public ParticleSystem psREBIRTH_EFFECT;

	[JsonIgnore]
	public ParticleSystem psMOB_GEM_EFFECT;

	[JsonIgnore]
	public ParticleSystem psTARGET_EFFECT;

	[JsonIgnore]
	public ParticleSystem psOnHitMob_EFFECT;

	[JsonIgnore]
	public ParticleSystem psSKILL_LEARN_EFFECT;

	[JsonIgnore]
	public ParticleSystem psMOB_DIE_BLOOD;

	[JsonIgnore]
	public ParticleSystem psIce_EFFECT;

	[JsonIgnore]
	public ParticleSystem psFire_EFFECT;

	[JsonIgnore]
	public ParticleSystem psElectric_EFFECT;

	[JsonIgnore]
	public ParticleSystem psPoison_EFFECT;

	[JsonIgnore]
	public ParticleSystem psStone_EFFECT;

	[JsonIgnore]
	public ParticleSystem psTYRANNY_TOWER_SG;

	[JsonIgnore]
	public ParticleSystem psTYRANNY_TOWER_MP;

	[JsonIgnore]
	public ParticleSystem psTYRANNY_TOWER_PHNX;

	[JsonIgnore]
	public AudioClip sPICKUP_ITEM;

	[JsonIgnore]
	public AudioClip GRINDING_SUCCEED;

	[JsonIgnore]
	public AudioClip GRINDING_FAIL;

	[JsonIgnore]
	public AudioClip GRINDING_RESET;

	[JsonIgnore]
	public AudioClip GRINDING_BROKEN;

	public ushort maxItemHold;

	public ushort maxItemAge;

	public long regionalChatGoldCost;

	public float bodyRadius;

	public float mobTracing;

	public float hPIncreasePer;

	public float mPIncreasePer;

	public float sPIncreasePer;

	public float hPIncrease;

	public float mPIncrease;

	public float sPIncrease;

	public float reactValidScale;

	public ushort maxAttackRangeShort;

	public ushort maxMobTracking;

	public int criticalMax;

	public int criticalDamage;

	public float lowSeedDamage;

	public float damageDecRate;

	public float damageGradeK;

	public float lowSPDamage;

	public float lowSPHitDrop;

	public int basicDisSP;

	public float pkPointDecreasePhy;

	public float pkPointDecreaseMagic;

	public float recExpRate;

	public float expScale;

	public float killExpRate;

	public float confrontSchoolExpScale;

	public ushort levelStatsPoint;

	public ushort levelSkillPoint;

	public float moneyDropScale;

	public float itemDropScale;

	public float itemDropLowerScale;

	public float moneyLuckRate;

	public float resistG;

	public float resistPhysicG;

	public uint crushingBlowMax;

	public uint crushingBlowDamage;

	public float crushBlowRange;

	public int[] stateBlowLevelList;

	public List<RAN.Mobile.GameModels.SPlayerKill> sPlayerKill;

	public ushort gradeMax;

	public ushort[] useGradeNum;

	public ushort gradeMaxRegi;

	public ushort damageGrade;

	public ushort defenseGrade;

	public ushort fDamageGrade;

	public ushort fDefenseGrade;

	public ushort gradeBeforeMaxAddDamage;

	public ushort gradeMaxAddDamage;

	public ushort gradeBeforeMaxAddDefense;

	public ushort gradeMaxAddDefense;

	public float[] fDamageGradeTop;

	public float[] fDefenseGradeTop;

	public ushort resistancFireGrade;

	public ushort resistancIceGrade;

	public ushort resistancElecGrade;

	public ushort resistancPoisonGrade;

	public ushort resistancSpiritGrade;

	public float[] fGradeRate;

	public float[] fGradeResetRate;

	public float[] fGradeTerminateRate;

	public bool enchantTerminateItem;

	public List<long> expTable;

	public long[] lnEXP_MAX_TABLE;

	public long[] lnEXP_MAX_TABLE_2nd;

	public List<float> fEXP_RATE_TABLE;

	public float fVehicleBoosterAddSpeed;

	public ushort wSCHOOLNUM;

	public float fMAX_COMMISSION;

	public float fDEFAULT_COMMISSION;

	public List<float> fDIE_DECEXP;

	public List<float> fDIE_RECOVERYEXP;

	public List<float> fEXP_RATE_MONEY;

	public float fPK_JUSTNESS_TIME;

	public float fPK_ITEM_DROP;

	public float fNONPK_ITEM_DROP;

	public ushort wCONFRONT_SCHOOL_LIMIT_NUM;

	public ushort wCONFRONT_SCHOOL_LIMIT_LEVEL;

	public float fCONFRONT_SCHOOL_EXP_SCALE;

	public ushort wCONFRONT_SCHOOL_WIN_NUM;

	public ushort wCONFRONT_SCHOOL_WIN_RATE;

	public float fCONFRONT_SCHOOL_GENITEM;

	public float fPARTYEXPRANGE;

	public float fPARTYEXP_S;

	public List<float> aPARTYEXP_ONE;

	public List<float> aPARTYKILLEXP_ONE;

	public bool bPKLESS;

	public bool bPK_MODE;

	public int nPK_LIMIT_LEVEL;

	public uint dwPK_RECALL_ENABLE_LEVEL;

	public uint dwPK_DRUG_ENABLE_LEVEL;

	public bool bSCHOOL_FREE_PK;

	public bool bSCHOOL_FREE_PK_ALWAYS;

	public bool bSCHOOL_FREE_PK_Z_FORCED;

	public bool bSCHOOL_FREE_PK_TIME_REAL;

	public int dwSCHOOL_FREE_PK_TIME_START;

	public int dwSCHOOL_FREE_PK_TIME_TERM;

	public float fSCHOOL_FREE_PK_ITEM_DROP;

	public uint dwCLUB_PARTYNUM;

	public uint dwCLUB_DISSOLUTION_DAY;

	public uint dwCLUB_JOINBLOCK_DAY;

	public uint dwCLUB_ALLIANCE_SEC;

	public uint dwCLUB_ALLIANCE_DIS;

	public uint dwCLUB_ALLIANCE_NUM;

	public uint dwMAX_CLUBRANK;

	public List<ClubRank> ClubRanks;

	public float fPK_SAFE_TIME;
}
