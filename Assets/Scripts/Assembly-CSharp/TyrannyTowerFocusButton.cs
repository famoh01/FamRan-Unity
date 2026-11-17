using UnityEngine;
using UnityEngine.UI;

public sealed class TyrannyTowerFocusButton : MonoBehaviour
{
	[Header("UI")]
	[Tooltip("The button the player taps to focus + engage the nearest tower.")]
	[SerializeField]
	private Button focusButton;

	[Tooltip("Root object toggled for visibility. Defaults to the button's own GameObject when left empty.")]
	[SerializeField]
	private GameObject buttonRoot;

	private bool controlsOwnGameObject;

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

	private bool IsEligibleToShow()
	{
		return false;
	}

	private void SetVisible(bool visible)
	{
	}

	private void OnFocusClicked()
	{
	}
}
