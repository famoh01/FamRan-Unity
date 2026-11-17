using System.Collections.Generic;
using RAN.Mobile.Enumerators;
using TrinityRift.Network;
using UnityEngine;

public sealed class UIKillFeedManager : MonoBehaviour
{
	[Header("Cached UI Slots")]
	[Tooltip("Drag the 5 UIKillReport objects from your Canvas Layout Group into here.")]
	public UIKillReport[] feedSlots;

	[Header("Class Icon References")]
	public Sprite brawlerIcon;

	public Sprite swordsmanIcon;

	public Sprite archerIcon;

	public Sprite shamanIcon;

	[Header("School Icon References")]
	public Sprite sacredGateIcon;

	public Sprite mysticPeakIcon;

	public Sprite phoenixIcon;

	[Header("Settings")]
	[Tooltip("How many seconds before the Kill Feed hides itself if no new kills happen?")]
	public float timeToHide;

	[Header("Debug")]
	[Tooltip("Log kill-feed lifecycle + per-kill events. Leave OFF in production (zero cost when off).")]
	public bool enableLogging;

	private const string LogPrefix = "[KillFeed] ";

	private readonly Queue<UIKillReport> slotQueue;

	private Canvas killFeedCanvas;

	private float hideTimer;

	private bool isShowingDummy;

	private ServerConnectionManager subscribedManager;

	private bool warnedNoCanvas;

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

	private void HandleNewKill(ServerConnectionManager.PKKillFeedEntry entry)
	{
	}

	private void ShowDummyFeedIfNeeded()
	{
	}

	private void HideDummyFeed()
	{
	}

	private void GetKillSprites(ServerConnectionManager.PKKillFeedEntry entry, out Sprite killerPortrait, out Sprite killerSchool, out Sprite victimPortrait, out Sprite victimSchool)
	{
		killerPortrait = null;
		killerSchool = null;
		victimPortrait = null;
		victimSchool = null;
	}

	private void CacheCanvas()
	{
	}

	private void SetKillFeedVisible(bool visible)
	{
	}

	private void HideAllSlots()
	{
	}

	private static void SetSlotActive(UIKillReport slot, bool active)
	{
	}

	private Sprite GetClassSprite(CharacterClassEnum classEnum)
	{
		return null;
	}

	private Sprite GetSchoolSprite(SchoolEnum schoolEnum)
	{
		return null;
	}

	private void Log(string message)
	{
	}
}
