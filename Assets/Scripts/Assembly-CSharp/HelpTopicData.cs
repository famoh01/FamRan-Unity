using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HelpTopicData
{
	[Serializable]
	public class Section
	{
		[Tooltip("Bolded line above the body. Leave empty for a body-only paragraph.")]
		public string heading;

		[Tooltip("The explanatory copy. TMP rich text is allowed.")]
		[TextArea(2, 8)]
		public string body;
	}

	[Header("Header")]
	public string title;

	[Tooltip("Optional icon shown next to the title. Leave empty to hide the icon.")]
	public Sprite icon;

	[Header("Content")]
	[Tooltip("Rendered in order. Each section is an optional bold heading followed by its body.")]
	public List<Section> sections;

	[Header("Link")]
	[Tooltip("Optional. When set, the panel shows a button that opens this address in-app (InAppBrowser: Safari sheet on iOS, device browser elsewhere). Leave empty and no button appears.\n\nNote this is a real Button, NOT a <link> tag in the body text: TextMeshPro renders <link> but never handles taps on it, so link markup in the copy would look clickable and do nothing.")]
	public string linkUrl;

	[Tooltip("Caption on the link button. Treated as a localization key like everything else when 'localized' is ticked. Falls back to the URL itself when left empty.")]
	public string linkLabel;

	[Header("Localization")]
	[Tooltip("When ticked, title/heading/body are treated as localization KEYS and resolved through LocalizationManager.Get. When unticked they are shown verbatim.")]
	public bool localized;

	public bool HasLink => false;

	public bool IsEmpty => false;

	public string GetTitle()
	{
		return null;
	}

	public string GetLinkLabel()
	{
		return null;
	}

	public string BuildBody()
	{
		return null;
	}

	private string Resolve(string value)
	{
		return null;
	}
}
