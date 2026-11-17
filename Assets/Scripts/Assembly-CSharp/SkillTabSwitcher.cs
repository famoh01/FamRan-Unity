using UnityEngine;
using UnityEngine.UI;

public class SkillTabSwitcher : MonoBehaviour
{
	public static SkillTabSwitcher Instance;

	public Canvas canvas1;

	public Canvas canvas2;

	[Header("Image Indicator Settings")]
	public Image canvasIndicatorImage;

	public Sprite tab1Sprite;

	public Sprite tab2Sprite;

	private bool canvas1Active;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void ToggleCanvas()
	{
	}

	private void UpdateCanvasIndicator()
	{
	}

	public void ResetSkillCanvas()
	{
	}
}
