using UnityEngine;
using UnityEngine.UI;

public class ReportPlayerButton : MonoBehaviour
{
	public static ReportPlayerButton Instance;

	[Header("References")]
	[Tooltip("The button. Left empty, a Button on this object is used.")]
	public Button _button;

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
}
