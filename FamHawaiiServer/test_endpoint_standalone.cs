// Standalone API validation test; Unity resource loading is stubbed. This does
// not test the Unity scene, TLS transport, or Android runtime.
using System;
using System.Reflection;
namespace UnityEngine
{
    public class TextAsset { public string text; }
    public static class Resources { public static T Load<T>(string name) where T : class { return null; } }
    public static class JsonUtility { public static T FromJson<T>(string value) where T : new() { return new T(); } }
    public static class Debug { public static void LogError(object message) { } }
}
class Test
{
    static void Require(bool v, string label) { if (!v) throw new Exception(label); }
    static void SetConfig(string api, string host, int port)
    {
        var type = typeof(FamHawaiiEndpoint).GetNestedType("Settings", BindingFlags.NonPublic);
        var obj = Activator.CreateInstance(type, true);
        type.GetField("apiBaseUrl").SetValue(obj, api);
        type.GetField("gameHost").SetValue(obj, host);
        type.GetField("gamePort").SetValue(obj, port);
        typeof(FamHawaiiEndpoint).GetField("_settings", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, obj);
    }
    static void Main()
    {
        SetConfig("", "", 7777);
        Require(FamHawaiiEndpoint.ApiBaseUrl("http://127.0.0.1:8085/") == "http://127.0.0.1:8085", "local fallback");
        Require(FamHawaiiEndpoint.ApiBaseUrl("http://remote.example.test") == "", "reject insecure scene fallback");
        Require(FamHawaiiEndpoint.ApiBaseUrl(null) == "", "reject missing scene fallback");
        Require(FamHawaiiEndpoint.GameHost == "127.0.0.1" && FamHawaiiEndpoint.GamePort == 7777, "local game endpoint");
        SetConfig("https://game.example.test/api/", "game.example.test", 7777);
        Require(FamHawaiiEndpoint.ApiBaseUrl("unused") == "https://game.example.test/api", "HTTPS url");
        Require(FamHawaiiEndpoint.GameHost == "game.example.test", "remote host");
        SetConfig("http://game.example.test", "bad host!", -1);
        Require(FamHawaiiEndpoint.ApiBaseUrl("unused") == "", "reject plaintext remote API");
        Require(FamHawaiiEndpoint.GameHost == "" && FamHawaiiEndpoint.GamePort == 0, "reject invalid host and port");
        SetConfig("https://user:password@game.example.test", "", 7777);
        Require(FamHawaiiEndpoint.ApiBaseUrl("unused") == "", "reject URL credentials");
        Console.WriteLine("Endpoint validation tests passed");
    }
}
