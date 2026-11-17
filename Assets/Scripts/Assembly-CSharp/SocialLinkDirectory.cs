using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SocialLinks", menuName = "Scriptable Objects/UI/Social Link Directory")]
public class SocialLinkDirectory : ScriptableObject
{
	[Serializable]
	public class SocialLinkEntry
	{
		[Tooltip("How buttons refer to this link. Case-insensitive. e.g. 'discord', 'facebook', 'website'.")]
		public string id;

		[Tooltip("Optional caption a button can display. Leave empty for icon-only buttons.")]
		public string displayName;

		[Tooltip("The full address, including https://. An entry with no URL is treated as unavailable and its buttons hide themselves.")]
		public string url;

		[Tooltip("Untick to take this link down without deleting the entry — its buttons hide, exactly as though the URL were empty. Useful for a Discord that is temporarily invite-only.")]
		public bool enabled;

		public bool IsUsable => false;
	}

	[Tooltip("Every link the client can open.")]
	public List<SocialLinkEntry> links;

	public const string DefaultResourcePath = "SocialLinks";

	private static SocialLinkDirectory _default;

	private static bool _defaultSearched;

	private Dictionary<string, SocialLinkEntry> _lookup;

	public static SocialLinkDirectory Default => null;

	public SocialLinkEntry Find(string id)
	{
		return null;
	}

	public string GetUrl(string id)
	{
		return null;
	}

	private void BuildLookup()
	{
	}

	private void OnValidate()
	{
	}

	private void OnEnable()
	{
	}
}
