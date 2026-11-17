using TMPro;
using UnityEngine;

public class PetNameDisplay : MonoBehaviour
{
	[Header("TEXTS/OwnerName/Owner_Text")]
	public TextMeshProUGUI ownerText;

	[Header("TEXTS/PetName/IGN_Text")]
	public TextMeshProUGUI petNameText;

	[Header("Fallback")]
	[Tooltip("The prefab ships IGN_Text reading 'PET'. Any plate still showing this is one nobody has named yet, and gets the pet's species name instead.")]
	public string placeholderName;

	private RectTransform _rect;

	private bool _resizePending;

	private PetMover _pet;

	private bool _petResolved;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	public void Set(string ownerName, string petName, Color color)
	{
	}

	private string ResolvePetName(string petName)
	{
		return null;
	}

	private bool IsPlaceholder(string value)
	{
		return false;
	}

	private void ApplyFallbackIfPlaceholder()
	{
	}

	private string ResolveSpeciesName()
	{
		return null;
	}

	public void SetOwnerName(string ownerName)
	{
	}

	public void ResizeToText()
	{
	}
}
