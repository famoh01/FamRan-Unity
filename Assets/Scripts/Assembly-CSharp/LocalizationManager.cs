using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>Loads the localization tables supplied with the project.</summary>
public class LocalizationManager : MonoBehaviour
{
    public struct LanguageInfo
    {
        public readonly string Code;
        public readonly string DisplayName;
        public readonly SystemLanguage System;
        public LanguageInfo(string code, string displayName, SystemLanguage system)
        {
            Code = code;
            DisplayName = displayName;
            System = system;
        }
    }

    public static LocalizationManager instance;
    public const string LanguagePrefKey = "LanguageIndex";
    // Resource lookup is case-sensitive on Android. Match the folder in the RAR.
    private const string ResourceFolder = "_localization/ui_";
    public static readonly LanguageInfo[] Languages = {
        new LanguageInfo("en", "English", SystemLanguage.English),
        new LanguageInfo("th", "ไทย", SystemLanguage.Thai),
        new LanguageInfo("id", "Bahasa Indonesia", SystemLanguage.Indonesian),
        new LanguageInfo("jp", "日本語", SystemLanguage.Japanese),
        new LanguageInfo("ko", "한국어", SystemLanguage.Korean),
        new LanguageInfo("ms", "Bahasa Melayu", SystemLanguage.Unknown),
        new LanguageInfo("zh", "中文", SystemLanguage.Chinese)
    };

    private Dictionary<string, string> _active;
    private Dictionary<string, string> _fallback;
    public bool IsDataReady { get; private set; }
    public int CurrentLanguageIndex { get; private set; }
    public string CurrentLanguageCode => Languages[CurrentLanguageIndex].Code;
    public static event Action LanguageChanged;

    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Select(PlayerPrefs.GetInt(LanguagePrefKey, 1)); // Thai for a new install
    }

    private static LocalizationManager Ensure()
    {
        if (instance == null)
        {
            var go = new GameObject("LocalizationManager");
            go.AddComponent<LocalizationManager>();
        }
        return instance;
    }

    private static Dictionary<string, string> Load(string code)
    {
        TextAsset text = Resources.Load<TextAsset>(ResourceFolder + code);
        if (text == null) return new Dictionary<string, string>();
        try { return JsonConvert.DeserializeObject<Dictionary<string, string>>(text.text) ?? new Dictionary<string, string>(); }
        catch (Exception e)
        {
            Debug.LogError("Could not load localization " + code + ": " + e.Message);
            return new Dictionary<string, string>();
        }
    }

    private void Select(int index)
    {
        CurrentLanguageIndex = index >= 0 && index < Languages.Length ? index : 1;
        _fallback = Load("en");
        _active = Load(CurrentLanguageCode);
        IsDataReady = _active.Count > 0;
        LanguageChanged?.Invoke();
    }

    private string Resolve(string key)
    {
        if (string.IsNullOrEmpty(key)) return string.Empty;
        string value;
        if (_active != null && _active.TryGetValue(key, out value)) return value;
        if (_fallback != null && _fallback.TryGetValue(key, out value)) return value;
        return key;
    }

    public static string Get(string key) => Ensure().Resolve(key);
    public static string Get(string key, params object[] args)
    {
        string value = Get(key);
        if (args == null || args.Length == 0) return value;
        try { return string.Format(value, args); }
        catch (FormatException) { return value; }
    }

    public static void SetLanguagePref(int index)
    {
        var manager = Ensure();
        if (index < 0 || index >= Languages.Length) index = 1;
        PlayerPrefs.SetInt(LanguagePrefKey, index);
        PlayerPrefs.Save();
        manager.Select(index);
    }
}
