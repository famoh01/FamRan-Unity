using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(EventTrigger))]
public abstract class UserInterfaceSkill : MonoBehaviour
{
	public SkillWindowObject skillWindow;

	private SkillWindowObject _previousSkillWindow;

	public Dictionary<GameObject, SkillSlot> slotsOnInterface;

	[Header("Player")]
	private Vector3 initialClickPosition;

	private float maxDragDistance;

	[Header("Default Visuals")]
	public Sprite defaultEmptySlotSprite;

	private bool isIndicatorInEnemySkl;

	private GameObject skillIndicator;

	private int skillButtonTouchId;

	private bool _dragSkillOffensive;

	private GameObject _lastUnitPointerTarget;

	private bool isHolding;

	private bool isDragging;

	private Vector2 holdStartPos;

	private const float HoldMoveThresholdSqr = 400f;

	public void OnEnable()
	{
	}

	private void OnDestroy()
	{
	}

	public abstract void CreateSkillSlots();

	public void UpdateSkillWindowLinks()
	{
	}

	public void OnSlotUpdate(SkillSlot slot)
	{
	}

	public void Update()
	{
	}

	protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
	{
	}

	public void OnEnter(GameObject obj)
	{
	}

	public void OnEnterInterface(GameObject obj)
	{
	}

	public void OnExitInterface(GameObject obj)
	{
	}

	public void OnExit(GameObject obj)
	{
	}

	public void OnSkillHoldEnter(GameObject obj)
	{
	}

	public void OnSkillHoldExit(GameObject obj)
	{
	}

	private void ShowSkillInfoFor(GameObject obj)
	{
	}

	public string TextColor(string color, string text)
	{
		return null;
	}

	public void OnDragStart(GameObject obj, PointerEventData eventData)
	{
	}

	private GameObject CreateTempSkill(GameObject parentObj)
	{
		return null;
	}

	public void OnDragEnd(GameObject obj, PointerEventData eventData)
	{
	}

	public void OnDrag(GameObject obj, PointerEventData eventData)
	{
	}

	public void OnClick(GameObject obj)
	{
	}

	public void OnClickRemove(GameObject obj, GameObject outerObj)
	{
	}

	public void OnDragTargetStart(GameObject obj, PointerEventData eventData)
	{
	}

	public void OnDragTargetEnd(GameObject obj, PointerEventData eventData)
	{
	}

	public void OnTargetDrag(PointerEventData eventData)
	{
	}

	private Touch GetTargetTouch()
	{
		return default;
	}

	private float GetSkillTargetReach(SkillSlot slot)
	{
		return 0f;
	}

	private void FindClosestEnemyToPosition(Vector3 position)
	{
	}
}
