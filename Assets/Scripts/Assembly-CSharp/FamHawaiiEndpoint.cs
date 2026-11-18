using System;
using UnityEngine;

/// <summary>
/// Build-time endpoint configuration for the supplied FamHawaii client.
/// An empty configuration preserves the original local-only development setup.
/// HTTPS is required for non-loopback API hosts. The original game TCP protocol
/// is only obfuscated; it still needs secure transport before public deployment.
/// </summary>
public static class FamHawaiiEndpoint
{
    [Serializable]
    private class Settings
    {
        public string apiBaseUrl = "";
        public string gameHost = "";
        public int gamePort = 7777;
    }

    private static Settings _settings;
    private static Settings Config
    {
        get
        {
            if (_settings != null) return _settings;
            _settings = new Settings();
            TextAsset text = Resources.Load<TextAsset>("FamHawaiiEndpoint");
            if (text != null)
            {
                try { _settings = JsonUtility.FromJson<Settings>(text.text) ?? new Settings(); }
                catch (Exception e) { Debug.LogError("FamHawaii endpoint configuration invalid: " + e.Message); }
            }
            return _settings;
        }
    }

    public static string ApiBaseUrl(string localFallback)
    {
        // Serialized Unity scene values are untrusted configuration too: apply
        // the same HTTPS/loopback policy even when the JSON override is empty.
        string configured = string.IsNullOrWhiteSpace(Config.apiBaseUrl)
            ? localFallback : Config.apiBaseUrl;
        if (string.IsNullOrWhiteSpace(configured))
        {
            Debug.LogError("FamHawaii API URL is missing; refusing connection.");
            return "";
        }
        Uri uri;
        if (!Uri.TryCreate(configured.Trim(), UriKind.Absolute, out uri) ||
            !string.IsNullOrEmpty(uri.UserInfo) || !string.IsNullOrEmpty(uri.Query) ||
            !string.IsNullOrEmpty(uri.Fragment) ||
            (uri.Scheme != Uri.UriSchemeHttps && !(uri.Scheme == Uri.UriSchemeHttp && uri.IsLoopback)))
        {
            Debug.LogError("FamHawaii API URL is invalid or insecure; refusing connection.");
            return "";
        }
        return uri.AbsoluteUri.TrimEnd('/');
    }

    public static string GameHost
    {
        get
        {
            string host = Config.gameHost;
            if (string.IsNullOrWhiteSpace(host)) return "127.0.0.1";
            host = host.Trim();
            if (Uri.CheckHostName(host) == UriHostNameType.Unknown)
            {
                Debug.LogError("FamHawaii game host is invalid; refusing connection.");
                return "";
            }
            return host;
        }
    }

    public static int GamePort
    {
        get
        {
            int p = Config.gamePort;
            return p >= 1 && p <= 65535 ? p : 0;
        }
    }
}
