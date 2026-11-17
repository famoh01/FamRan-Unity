using UnityEngine;

public class StaticAudioManager : MonoBehaviour
{
	public AudioClip audioClip;

	[Header("Audio Settings")]
	public bool loop;

	public bool playOnAwake;

	public bool mute;

	public float minDistance;

	public float maxDistance;

	public void Awake()
	{
	}
}
