using System.Collections.Generic;
using Core.Enumerators;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerEnemyClicker : MonoBehaviour
{
	public static PlayerEnemyClicker Instance;

	public float npcRange;

	public float sightRange;

	public float attackRange;

	public string enemyTag;

	public LayerMask whatIsEnemy;

	public LayerMask whatIsNPC;

	public bool npcInSightRange;

	public bool hasTarget;

	private GameObject nonBlockingPanel;

	private bool onTalkNpc;

	public Button npcInteractionButton;

	private GameObject npcInRange;

	public GameObject target;

	private GameObject lastTarget;

	private PlayerStatistics _statistics;

	private PlayerFollowToAttack _pfta;

	private readonly Collider[] _npcBuffer;

	private bool _targetHudSyncPending;

	private PointerEventData _clickEventData;

	private readonly List<RaycastResult> _clickResults;

	public CrowType targetCrowType { get; private set; }

	private PlayerFollowToAttack GetFollowToAttackComponent => null;

	private void Awake()
	{
	}

	private void FixedUpdate()
	{
	}

	private bool IsTargetValid(GameObject t, CrowType type)
	{
		return false;
	}

	private void ClearTarget(bool updateTargetHud = true)
	{
	}

	private static void HideTargetHud()
	{
	}

	public void SetTarget(GameObject t, CrowType type)
	{
	}

	public bool AcquireNearestTargetIfNeeded(float maxRange)
	{
		return false;
	}

	public void SelectSelf(GameObject self)
	{
	}

	public void SelectRemotePlayer(Unit remoteUnit)
	{
	}

	public void TargetHealthDisplay(CrowType crowType, GameObject target)
	{
	}

	private void SelectTarget()
	{
	}

	public void TalkToNPC(GameObject npc)
	{
	}

	public void ReinitiallizeTalk(GameObject npc)
	{
	}

	private void GetValidDialogue(MobNpcObject mobNpcObject)
	{
	}

	private bool ValidQuest(TalkConversation basic)
	{
		return false;
	}

	private bool ValidRequiredItem(TalkConversation basic)
	{
		return false;
	}

	private bool ValidRequiredSkill(TalkConversation basic)
	{
		return false;
	}

	private bool ValidMustNotLearnedSkills(TalkConversation basic)
	{
		return false;
	}

	public bool CheckClassRequirement(TalkConversation basic)
	{
		return false;
	}

	public bool CheckLevelRequirement(TalkConversation basic)
	{
		return false;
	}

	public bool CheckBrightPointsRequirement(TalkConversation basic)
	{
		return false;
	}

	public bool CheckRequiredRunningQuestRequirement(TalkConversation basic)
	{
		return false;
	}

	public bool CheckSchoolRequirement(TalkConversation basic)
	{
		return false;
	}

	private void OnDrawGizmosSelected()
	{
	}

	public void InstantDamage()
	{
	}
}
