using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlockTargetButton : MonoBehaviour
{
	public static BlockTargetButton Instance;

	[Header("References")]
	[Tooltip("The button. Left empty, a Button on this object is used.")]
	public Button _button;

	[Tooltip("Optional label — swapped between the two texts below.")]
	public TextMeshProUGUI _label;

	[Tooltip("Optional icon — swapped between the two sprites below.")]
	public Image _icon;

	[Header("Blocked state")]
	public string _blockText;

	public string _unblockText;

	public Sprite _blockSprite;

	public Sprite _unblockSprite;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	private void OnEnable()
	{
	}

	private void OnClick()
	{
	}

	public void Refresh()
	{
	}

	public void Refresh(string targetName)
	{
	}

	public static void RefreshTarget(string targetName)
	{
	}
}
