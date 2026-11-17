using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ModernToggle : MonoBehaviour
{
	[Header("UI References")]
	public RectTransform handle;

	public Image backgroundImage;

	[Header("Colors")]
	public Color onColor;

	public Color offColor;

	[Header("Settings")]
	public float handleOffset;

	public float slideSpeed;

	private Toggle toggle;

	private float defaultY;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void UpdateVisuals(bool snapInstant)
	{
	}
}
