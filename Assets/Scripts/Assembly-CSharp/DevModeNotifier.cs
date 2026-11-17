using System;
using TMPro;
using UnityEngine;

public static class DevModeNotifier
{
	public const string Title = "Developer Mode Detected";

	private static bool _isShowing;

	private static GameObject _modalRoot;

	private static AndroidJavaObject _activity;

	private static AndroidJavaObject _dialog;

	private static DevModeDialogListener _listener;

	private static bool _nativeDialogShown;

	public static bool IsShowing => false;

	public static void Show(string message, Action onDismissed = null)
	{
	}

	private static bool ShowAndroid(string message, Action onDismissed)
	{
		return false;
	}

	private static void ShowModal(string title, string message, Action onDismissed)
	{
	}

	private static void CloseModal()
	{
	}

	private static RectTransform CreateRect(string name, Transform parent)
	{
		return null;
	}

	private static void SetRect(RectTransform rect, Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax)
	{
	}

	private static void CreateLabel(Transform parent, string name, string text, float size, FontStyles style, TextAlignmentOptions alignment, Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax)
	{
	}
}
