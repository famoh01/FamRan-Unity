using UnityEngine;
using UnityEngine.UI;

public class LoadingSpinner : MonoBehaviour
{
	[Header("UI References")]
	public Image spinnerImage;

	[Header("Animation Settings")]
	public Sprite[] animationFrames;

	public float framesPerSecond;

	private float timer;

	private int currentFrame;

	private void Awake()
	{
	}

	private void Update()
	{
	}
}
