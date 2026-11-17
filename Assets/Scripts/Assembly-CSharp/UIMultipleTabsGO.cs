using UnityEngine;
using UnityEngine.UI;

public class UIMultipleTabsGO : MonoBehaviour
{
	public Button[] tabsButtons;

	public GameObject[] tabsGameObj;

	[Header("Tab Colors")]
	public Color selectedColor;

	public Color unselectedColor;

	private void Start()
	{
	}

	public void SelectTab(int selectedIndex)
	{
	}
}
