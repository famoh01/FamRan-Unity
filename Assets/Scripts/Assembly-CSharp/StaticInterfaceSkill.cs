using UnityEngine;
using UnityEngine.UI;

public class StaticInterfaceSkill : UserInterfaceSkill
{
	public static StaticInterfaceSkill Instance;

	public GameObject[] slots;

	public GameObject[] outerSlots;

	[Header("UI Skill Targeting")]
	public GameObject dragIndicatorPrefab;

	private GameObject activeDragIndicator;

	private RectTransform outerCircle;

	private RectTransform innerStick;

	private Image outerImage;

	private Image innerImage;

	private Color originalOuterColor;

	private Color originalInnerColor;

	public Color cancelColor;

	public Vector2 NormalizedDeflection { get; private set; }

	public bool HasActiveDragIndicator => false;

	private void Awake()
	{
	}

	public override void CreateSkillSlots()
	{
	}

	public void ShowDragIndicator(Transform targetButton)
	{
	}

	private float RingRadiusPx()
	{
		return 0f;
	}

	public void UpdateDragIndicator(Vector2 currentDragScreenPos)
	{
	}

	public void HideDragIndicator()
	{
	}
}
