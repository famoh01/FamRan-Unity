using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectServerSound : MonoBehaviour
{
	[Tooltip("The exact string name of the sound in the AudioManager")]
	public string soundName;

	private void Awake()
	{
	}

	private void PlayClickSound()
	{
	}
}
