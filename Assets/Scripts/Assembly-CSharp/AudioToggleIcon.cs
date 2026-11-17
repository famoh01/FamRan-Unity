using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class AudioToggleIcon : MonoBehaviour
{
	[Header("UI References")]
	public Image targetIcon;

	[Header("Sprites")]
	public Sprite audioOnSprite;

	public Sprite audioMuteSprite;

	private Toggle toggle;

	private void Awake()
	{
	}

	private void UpdateIcon(bool isOn)
	{
	}
}
