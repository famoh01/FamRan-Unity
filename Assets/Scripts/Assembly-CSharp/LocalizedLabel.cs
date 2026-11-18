using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class LocalizedLabel : MonoBehaviour
{
    [Tooltip("Localization key in Resources/_localization/ui_<lang>.json")]
    [SerializeField] private string key;
    private TMP_Text _text;

    public string Key
    {
        get { return key; }
        set { key = value; if (isActiveAndEnabled) Apply(); }
    }

    private void Awake() { _text = GetComponent<TMP_Text>(); }
    private void OnEnable()
    {
        LocalizationManager.LanguageChanged += Apply;
        Apply();
    }
    private void OnDisable() { LocalizationManager.LanguageChanged -= Apply; }
    private void Apply()
    {
        if (_text == null) _text = GetComponent<TMP_Text>();
        if (_text != null && !string.IsNullOrEmpty(key)) _text.text = LocalizationManager.Get(key);
    }
}
