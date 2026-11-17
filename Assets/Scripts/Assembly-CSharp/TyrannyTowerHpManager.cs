using System.Collections.Generic;
using TrinityRift.Network;
using UnityEngine;
using UnityEngine.UI;

public sealed class TyrannyTowerHpManager : MonoBehaviour
{
	private const string LogPrefix = "[TyrannyTowerHp] ";

	private const ushort NoOwner = 3;

	[Header("UI")]
	[SerializeField]
	private Canvas hpCanvas;

	[SerializeField]
	private Image sacredGateFill;

	[SerializeField]
	private Image mysticPeakFill;

	[SerializeField]
	private Image phoenixFill;

	[Header("Captured-By Medallion Overlays (sibling of each fill image)")]
	[SerializeField]
	private GameObject sacredGateMedallion;

	[SerializeField]
	private GameObject mysticPeakMedallion;

	[SerializeField]
	private GameObject phoenixMedallion;

	[Header("Debug")]
	[Tooltip("Log tracked-target changes. Leave OFF in production (zero cost when off).")]
	[SerializeField]
	private bool enableLogging;

	private ServerConnectionManager subscribedServerManager;

	private readonly Dictionary<ushort, Vector3> pctByCrow;

	private readonly Dictionary<ushort, ushort> ownerByCrow;

	private readonly HashSet<ushort> knownTowerCrows;

	private bool hasTrackedCrow;

	private ushort trackedCrowId;

	private float cachedSgPct;

	private float cachedMpPct;

	private float cachedPhxPct;

	private ushort cachedOwner;

	private GameObject lastTargetObject;

	private EnemyAiController lastTargetEnemy;

	private bool hasLastPhase;

	private TyrannyManager.TyrannyPhase lastPhase;

	private bool wasOnTyrannyMap;

	public static TyrannyTowerHpManager Instance { get; private set; }

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	private void DetectNewWarInstance()
	{
	}

	private void ResetCrowState(string reason)
	{
	}

	private void HandleCrowOwner(ushort crowGlobId, ushort owner)
	{
	}

	private void HandleCrowDamage(ushort crowGlobId, float pctSG, float pctMP, float pctPHX)
	{
	}

	private void RefreshTrackedTarget()
	{
	}

	private static bool IsEligibleToShow()
	{
		return false;
	}

	private void ApplyFills(float pctSG, float pctMP, float pctPHX, bool forceRefresh)
	{
	}

	private static void SetFill(Image image, ref float cache, float pct, bool forceRefresh)
	{
	}

	private void ApplyOwner(ushort owner, bool forceRefresh)
	{
	}

	private static void SetMedallion(GameObject medallion, bool active)
	{
	}

	private void ShowWidget()
	{
	}

	private void HideWidget()
	{
	}

	private void Log(string message)
	{
	}
}
