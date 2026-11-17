using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chat Macro", menuName = "Scriptable Objects/ETC/New Chat Macro List")]
public class MacroChatDataList : ScriptableObject
{
	[Serializable]
	private class ChatMacroSaveData
	{
		public int version;

		public string[] texts;
	}

	private const string SaveFileName = "chatMacro.save";

	private const string DefaultMacroText = "MACRO TEXT HERE";

	private const int SaveVersion = 1;

	public MacroChatData[] chatMacro;

	public static MacroChatDataList instance;

	public void Awake()
	{
	}

	private static string GetSaveFilePath()
	{
		return null;
	}

	[ContextMenu("Save")]
	public void Save()
	{
	}

	public void Load()
	{
	}

	[ContextMenu("Clear")]
	public void Clear()
	{
	}
}
