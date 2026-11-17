using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Builds the exported KlassX project into an Android APK wired for the local
/// stack: HTTP apiBaseUrl is already patched to 127.0.0.1:8085 in scenes, the
/// zone TCP is hardcoded 127.0.0.1:7777 (adb reverse covers both).
///
/// Run: Unity -batchmode -quit -projectPath &lt;proj&gt; -executeMethod BuildKlassx.Build
/// Exit code 0 = success.
/// </summary>
public static class BuildKlassx
{
    // Separate id from the original com.klab.klassxonline so BOTH apps can be
    // installed side by side for gameplay comparison.
    private const string ApplicationId = "com.klab.klassx.selfhost";
    private const string OutputPath = "Build/KlassXLocal.apk";

    public static void Build()
    {
        try
        {
            PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.Android, ApplicationId);
            PlayerSettings.productName = "KlassX Local";

            // ARM64 IL2CPP = same ABI/backend as the original APK; the emulator
            // reports arm64-v8a support.
            PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;

            // Decompiled/reflection-heavy game code must not be stripped.
            PlayerSettings.SetManagedStrippingLevel(NamedBuildTarget.Android, ManagedStrippingLevel.Disabled);

            // The client talks to http://127.0.0.1:8085 -- allow cleartext on API 28+.
            PlayerSettings.insecureHttpOption = UnityEditor.InsecureHttpOption.AlwaysAllowed;

            // ETC2 is guaranteed on any GLES3 device (host-GPU translation can
            // lack ASTC); keeps textures from coming out broken in the emulator.
            EditorUserBuildSettings.androidBuildSubtarget = MobileTextureSubtarget.ETC2;

            string[] scenes = EditorBuildSettings.scenes
                .Where(s => s.enabled && !string.IsNullOrEmpty(s.path))
                .Select(s => s.path)
                .ToArray();
            if (scenes.Length == 0)
                throw new Exception("no enabled scenes in EditorBuildSettings");

            Debug.Log($"[KlassxBuild] {scenes.Length} scenes -> {OutputPath}");

            var options = new BuildPlayerOptions
            {
                scenes = scenes,
                locationPathName = OutputPath,
                target = BuildTarget.Android,
                targetGroup = BuildTargetGroup.Android,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(options);
            BuildSummary sum = report.summary;

            Debug.Log($"[KlassxBuild] result={sum.result} errors={sum.totalErrors} " +
                      $"time={sum.totalTime} size={sum.totalSize}");

            EditorApplication.Exit(
                sum.result == BuildResult.Succeeded && sum.totalErrors == 0 ? 0 : 1);
        }
        catch (Exception e)
        {
            Debug.LogError("[KlassxBuild] FAILED: " + e);
            EditorApplication.Exit(1);
        }
    }
}
