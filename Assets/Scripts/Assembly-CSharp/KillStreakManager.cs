using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TrinityRift.Network;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public sealed class KillStreakManager : MonoBehaviour
{
	[Header("Display")]
	[Tooltip("The on-screen popup that renders tier images.")]
	public UIKillStreakDisplay display;

	[Header("Tiers")]
	[Tooltip("All kill-streak levels. Assign the 15 images + thresholds here.\nOrder is free — a rung's strength is its threshold, so listing them out of order (or inserting one later) can't break the escalation check.")]
	public List<KillStreakTier> tiers;

	[Header("Rules")]
	[Tooltip("Max seconds between kills to keep a multi-kill combo (PK Combo) alive. A kill within this window advances the combo (Double → Triple …); a longer gap starts it over at 1. RAN uses 15s (fPKCOMBO_RANGE_TIME); a shorter window makes 'N at once' feel tighter.")]
	[FormerlySerializedAs("multiKillWindow")]
	public float comboWindow;

	[Tooltip("Show cumulative Spree tiers (Killing Spree / Dominating / Godlike…). Turn OFF to display multi-kill sprites only.")]
	public bool enableSpreeTrack;

	[Tooltip("Seconds of NO kills before the cumulative spree expires on its own (RAN: 15s). Re-armed by every kill. Set to 0 to make the spree last until death.")]
	public float spreeWindow;

	[Tooltip("Also clear the multi-kill combo when the local player dies. RAN does NOT do this — its PK Combo is purely 'kills landed in the last N seconds', independent of life state. Leave OFF to match RAN; turn ON if a death should visibly cut a burst short.")]
	public bool comboResetsOnDeath;

	[Tooltip("Reset First Blood eligibility each time the local player dies (so the first kill after each respawn counts). Off = fires only once per session.")]
	public bool firstBloodResetsOnDeath;

	[Header("Announcements (server hooks)")]
	[Tooltip("Streak count at which a rung is considered server-wide news rather than a local popup (RAN broadcasts from 6). Only sets KillStreakEvent.WorthBroadcasting — this manager never sends anything itself; a listener decides what to do with it.")]
	[Min(1f)]
	public int broadcastRungThreshold;

	[Tooltip("Lowest spree count that gets a per-rung lifetime counter (RAN: 8 = Godlike upward). Read them back with GetLifetimeRungCount / LifetimeRungCounts to upload as stats.")]
	[Min(1f)]
	public int lifetimeStatMinRank;

	[Header("Server Authority")]
	[Tooltip("OFF (now): counters run on this client from the PK kill feed, so streaks work without server support. ON (once the server logic exists): local counting stops entirely and the streak is driven only by ApplyServerCombo / ApplyServerSpree / EndServer* calls made from the packet handlers.")]
	public bool useServerAuthority;

	[Header("Debug")]
	public bool enableLogging;

	private const string LogPrefix = "[KillStreak] ";

	[CompilerGenerated]
	private Action<KillStreakEvent> m_OnRungReached;

	[CompilerGenerated]
	private Action<KillStreakEvent> m_OnStreakEnded;

	private int spreeCount;

	private int lastSpreeThresholdShown;

	private float lastSpreeKillTime;

	private int multiKillCount;

	private int lastMultiThresholdShown;

	private float lastKillTime;

	private bool firstBloodConsumed;

	private readonly Dictionary<string, int> lifetimeRungCounts;

	private ServerConnectionManager subscribedManager;

	public int CurrentSpree => 0;

	public int CurrentCombo => 0;

	public IReadOnlyDictionary<string, int> LifetimeRungCounts => null;

	public event Action<KillStreakEvent> OnRungReached
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public event Action<KillStreakEvent> OnStreakEnded
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnMapLoaded(Scene scene, LoadSceneMode mode)
	{
	}

	private void Update()
	{
	}

	private void HandleKill(ServerConnectionManager.PKKillFeedEntry entry)
	{
	}

	private void RegisterLocalKill()
	{
	}

	private void AnnounceForCounts(ushort gaeaId, KillStreakSource source)
	{
	}

	private void ResetOnDeath(KillStreakSource source)
	{
	}

	private void EndSpree(KillStreakSource source)
	{
	}

	private void EndCombo(KillStreakSource source)
	{
	}

	public void ApplyServerCombo(ushort gaeaId, int count, float remainingSeconds = 0f)
	{
	}

	public void EndServerCombo(ushort gaeaId)
	{
	}

	public void ApplyServerSpree(ushort gaeaId, int count)
	{
	}

	public void EndServerSpree(ushort gaeaId, int finalCount = 0)
	{
	}

	private static ushort LocalGaeaId()
	{
		return 0;
	}

	private static bool IsLocalPlayer(ushort gaeaId)
	{
		return false;
	}

	private void RecordLifetimeRung(KillStreakTier tier, int count)
	{
	}

	private void RaiseRungReached(KillStreakCategory category, int count, KillStreakTier tier, ushort gaeaId, KillStreakSource source)
	{
	}

	private KillStreakTier FindHighestTier(KillStreakCategory category, int count)
	{
		return null;
	}

	private KillStreakTier FindFirstBloodTier()
	{
		return null;
	}

	private void Announce(KillStreakTier tier)
	{
	}

	public void ResetAll()
	{
	}

	public void DebugSimulateLocalKill()
	{
	}

	public void DebugSimulateLocalDeath()
	{
	}

	private void Log(string message)
	{
	}
}
