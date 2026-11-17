using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
	public struct LanguageInfo
	{
		public readonly string Code;

		public readonly string DisplayName;

		public readonly SystemLanguage System;

		public LanguageInfo(string code, string displayName, SystemLanguage system)
		{
			Code = null;
			DisplayName = null;
			System = SystemLanguage.Afrikaans;
		}
	}

	public static LocalizationManager instance;

	public const string LanguagePrefKey = "LanguageIndex";

	private const string ResourceFolder = "_Localization/ui_";

	public static readonly LanguageInfo[] Languages;

	private Dictionary<string, string> _active;

	private Dictionary<string, string> _fallback;

	public bool IsDataReady { get; private set; }

	public int CurrentLanguageIndex { get; private set; }

	public string CurrentLanguageCode => null;

	private void Awake()
	{
	}

	private static Dictionary<string, string> Load(string code)
	{
		return null;
	}

	private string Resolve(string key)
	{
		return null;
	}

	public static string Get(string key)
	{
		return null;
	}

	public static string Get(string key, params object[] args)
	{
		return null;
	}

	public static void SetLanguagePref(int index)
	{
	}
}
