using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class QuestTrackerEntry : MonoBehaviour
{
	[Tooltip("Legacy title label. The tracker no longer shows the quest name — if this is still wired on the prefab it is hidden on bind, so old prefabs don't need re-authoring.")]
	public TextMeshProUGUI questTitle;

	[Tooltip("Progress next to the objective — 'Complete!' when the quest is ready to hand in, otherwise the current step's kill count, e.g. '0/20'. Leave unassigned for an objective-only row.")]
	public TextMeshProUGUI questStatus;

	[Header("Objective")]
	[Tooltip("The current step's objective line, e.g. 'Kill Hunk Baldie'. Give it word wrap, and either a ContentSizeFitter or a fixed height — the tracker measures the row to size its viewport.")]
	[FormerlySerializedAs("objectivesText")]
	[FormerlySerializedAs("descriptionText")]
	public TextMeshProUGUI objectiveText;

	[Header("Ready-to-turn-in colour")]
	[Tooltip("Objective + progress turn this colour once the quest is ready to hand in (the zone's turn-in-ready flag). Any other time they revert to the colour set on the prefab.")]
	public Color completeColor;

	[Tooltip("Turn off to leave the objective alone and only recolour the progress text.")]
	[FormerlySerializedAs("colorTitleWhenComplete")]
	public bool colorObjectiveWhenComplete;

	[Header("Optional")]
	[Tooltip("Tapping the row. QuestTrackerHUD binds this to open the quest log.")]
	public Button rowButton;

	[Tooltip("Shown while the quest is ready to be handed in.")]
	public GameObject turnInReadyBadge;

	private Color _baseObjectiveColor;

	private Color _baseStatusColor;

	private bool _hasBaseColors;

	public ushort QuestId { get; private set; }

	public void Bind(QuestObject questObj, Quest state)
	{
	}

	private void CacheBaseColors()
	{
	}

	private void ApplyCompleteColor(bool turnInReady)
	{
	}

	private static string BuildObjective(QuestObject questObj, Quest state)
	{
		return null;
	}

	private static string BuildStatus(QuestObject questObj, Quest state, bool turnInReady)
	{
		return null;
	}

	private static QuestStep CurrentStep(QuestObject questObj, Quest state)
	{
		return null;
	}
}
