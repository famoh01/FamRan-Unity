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
    // GameCI checks this output directory after Unity exits. Keep the custom
    // build output there so its validation sees the actual APK.
    private const string OutputPath = "build/Android/Android.apk";

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

            // IL2CPP cannot link the unused Windows-only interop stub in the
            // original Library.Shared.dll when every method is preserved.
            // Low stripping may remove that unused stub; runtime behavior still
            // needs testing on Android, especially reflection-based code.
            PlayerSettings.SetManagedStrippingLevel(NamedBuildTarget.Android, ManagedStrippingLevel.Low);

            // The client talks to http://127.0.0.1:8085 -- allow cleartext on API 28+.
            PlayerSettings.insecureHttpOption = UnityEditor.InsecureHttpOption.AlwaysAllowed;

            // ETC2 is guaranteed on any GLES3 device (host-GPU translation can
            // lack ASTC); keeps textures from coming out broken in the emulator.
            EditorUserBuildSettings.androidBuildSubtarget = MobileTextureSubtarget.ETC2;

            // Release signing with our own keystore (decoded by CI from the
            // ANDROID_KEYSTORE_BASE64 secret into the project root).
            string keystorePath = Environment.GetEnvironmentVariable("ANDROID_KEYSTORE_PATH");
            string keystorePass = Environment.GetEnvironmentVariable("ANDROID_KEYSTORE_PASS");
            string keyaliasName = Environment.GetEnvironmentVariable("ANDROID_KEYALIAS_NAME");
            string keyaliasPass = Environment.GetEnvironmentVariable("ANDROID_KEYALIAS_PASS");
            if (!string.IsNullOrEmpty(keystorePath) && System.IO.File.Exists(keystorePath))
            {
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = keystorePath;
                PlayerSettings.Android.keystorePass = keystorePass;
                PlayerSettings.Android.keyaliasName = keyaliasName;
                PlayerSettings.Android.keyaliasPass = keyaliasPass;
                Debug.Log("[KlassxBuild] custom keystore signing enabled");
            }
            else
            {
                Debug.Log("[KlassxBuild] no custom keystore found, using debug key");
            }

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

            bool succeeded = sum.result == BuildResult.Succeeded && sum.totalErrors == 0 &&
                System.IO.File.Exists(OutputPath) && new System.IO.FileInfo(OutputPath).Length > 0;
            // GameCI's validator requires this exact marker when a custom build
            // method is used. Emit it only after Unity and file checks succeed.
            if (succeeded)
                Debug.Log("Build succeeded!");
            else
                Debug.LogError("[KlassxBuild] Build report or output validation failed.");
            EditorApplication.Exit(succeeded ? 0 : 1);
        }
        catch (Exception e)
        {
            Debug.LogError("[KlassxBuild] FAILED: " + e);
            EditorApplication.Exit(1);
        }
    }
}
