using System.Collections.Generic;
using Core.Enumerators;
using UnityEngine;

public class PopUpDamageEntity : MonoBehaviour
{
	public static PopUpDamageEntity instance;

	public GameObject popupDamages;

	public Vector3 popupDamageOffset;

	[Header("Pool")]
	[SerializeField]
	private int prewarmCount;

	[SerializeField]
	private int poolMaxSize;

	[Header("Damage Atlas (sliced from number.png + charinven03.png)")]
	[Tooltip("Index 0..9 = damage_red_0..9 from number.png. UI_ATTACK / bAttack=true.")]
	public Sprite[] redDigits;

	[Tooltip("Index 0..9 = damage_blue_0..9 from number.png. UI_UNDERATTACK / bAttack=false.")]
	public Sprite[] blueDigits;

	[Tooltip("damage_critical from number.png.")]
	public Sprite criticalSprite;

	[Tooltip("damage_crushing_blow from charinven03.png.")]
	public Sprite crushingBlowSprite;

	[Tooltip("damage_miss from number.png.")]
	public Sprite missSprite;

	private Transform _effectListPage;

	private readonly Stack<PopupDamage> _pool;

	private bool _prewarmed;

	private const string EffectListPageTag = "EffectListPage";

	public const float DefaultAnchorHeight = 17f;

	public static float MaxPopupDist;

	public static float NearAlwaysDist;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void OnPopUpDamage(int damage, Transform pos)
	{
	}

	public void OnPopUpDamage(int damage, Transform pos, DamageTypeEnum flag)
	{
	}

	public void OnPopUpDamage(int damage, Transform pos, DamageTypeEnum flag, bool bAttack)
	{
	}

	public void OnPopUpDamage(int damage, Transform pos, DamageTypeEnum flag, bool bAttack, float anchorHeight)
	{
	}

	private static bool WorthShowing(Transform pos, bool bAttack)
	{
		return false;
	}

	private void DoPopUpDamage(int damage, Transform pos, DamageTypeEnum flag, bool bAttack, float anchorHeight)
	{
	}

	private PopupDamage Rent(Vector3 worldPos, Quaternion worldRot)
	{
		return null;
	}

	public void Return(PopupDamage popup)
	{
	}

	private void TryPrewarm()
	{
	}

	private PopupDamage CreateNewInstance()
	{
		return null;
	}

	private Transform GetEffectListPageParent()
	{
		return null;
	}
}
