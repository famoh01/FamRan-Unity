using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public sealed class ForcePkTargetButton : MonoBehaviour
{
	[Header("Button")]
	[Tooltip("Force PK button. When left empty, the Button on this GameObject is used.")]
	[SerializeField]
	private Button forcePkButton;

	[Header("Optional State Visuals")]
	[Tooltip("Optional child shown while Force PK is ON. Do not assign the button root itself.")]
	[SerializeField]
	private GameObject onStateVisual;

	[Tooltip("Optional child shown while Force PK is OFF. Do not assign the button root itself.")]
	[SerializeField]
	private GameObject offStateVisual;

	[Tooltip("Optional image whose sprite represents the toggle state. Defaults to the Button target graphic when it is an Image.")]
	[SerializeField]
	private Image stateImage;

	[SerializeField]
	private Sprite onSprite;

	[SerializeField]
	private Sprite offSprite;

	private Graphic[] visibilityGraphics;

	private GameObject cachedTargetObject;

	private Unit cachedTarget;

	private uint cachedCharId;

	private ushort cachedGaeaId;

	private bool visibilityInitialized;

	private bool isVisible;

	private bool stateInitialized;

	private bool displayedOn;

	private bool displayedHostile;

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

	public void RefreshNow()
	{
	}

	private void RefreshView(bool force)
	{
	}

	private void OnButtonClicked()
	{
	}

	private bool TryGetSelectedRemotePlayer(out Unit target)
	{
		target = null;
		return false;
	}

	private void SetVisible(bool visible, bool force)
	{
	}

	private void ApplyStateVisuals(bool isOn)
	{
	}

	private static void SetActiveIfChanged(GameObject target, bool active)
	{
	}
}
