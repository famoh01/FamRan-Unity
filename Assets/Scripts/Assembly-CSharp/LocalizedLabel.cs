using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class LocalizedLabel : MonoBehaviour
{
	[Tooltip("Localization key, e.g. \"common.cancel\". Resolved from Resources/_Localization/ui_<lang>.json.")]
	[SerializeField]
	private string key;

	private TMP_Text _text;

	public string Key
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void Apply()
	{
	}
}
