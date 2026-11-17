using UnityEngine;
using UnityEngine.UI;

public class LoaderScene : MonoBehaviour
{
	public static LoaderScene instance;

	private Canvas _canvas;

	[SerializeField]
	private Slider _slider;

	[SerializeField]
	private Image _backgroundImage;

	public Image BackgroundImage => null;

	public bool IsShowing => false;

	public void Show()
	{
	}

	public void Hide()
	{
	}

	public void SliderValueChange(float x)
	{
	}

	public void SliderValueReset()
	{
	}

	public void SetSliderValue(float v)
	{
	}

	public void SetBackgroundImage(Sprite s)
	{
	}

	private void Awake()
	{
	}
}
