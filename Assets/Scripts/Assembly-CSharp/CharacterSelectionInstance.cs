using RANMobile.Authentication;
using TMPro;
using UnityEngine;

public class CharacterSelectionInstance : MonoBehaviour
{
	public static CharacterSelectionInstance instance;

	[Header("Shared UI")]
	public GameObject webLoader;

	public TextMeshProUGUI errorText;

	[Header("Managers")]
	[Tooltip("Assign in the Inspector to skip the scene scan. Left empty, it is found once on Awake.")]
	public Authentication authentication;

	public CharacterDeleteManager characterDeleteManager;

	public CharacterLoginManager characterLoginManager;

	public void Awake()
	{
	}
}
