using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SocialLinkButton : MonoBehaviour
{
	[Header("Link")]
	[Tooltip("Directory asset holding the URLs. Leave empty to use the shared one from Resources/SocialLinks (SocialLinkDirectory.Default).")]
	public SocialLinkDirectory directory;

	[Tooltip("Which entry in the directory this button opens, e.g. 'discord'. Ignored when urlOverride is set.")]
	public string linkId;

	[Tooltip("A literal URL for this button, used instead of the directory when filled in.")]
	public string urlOverride;

	[Header("Presentation")]
	[Tooltip("Optional caption filled from the directory entry's Display Name. Leave empty for icon-only buttons, or fill the text in by hand and leave this unassigned.")]
	public TextMeshProUGUI label;

	[Tooltip("Hide the button when its link is missing, disabled or blank. Untick to leave it visible but non-interactable instead.")]
	public bool hideWhenUnavailable;

	[Header("Safety")]
	[Tooltip("Only open http:// and https:// addresses. Tick the box below to also allow app schemes like discord:// or fb://, which is the only reason to loosen this.")]
	public bool allowNonHttpScheme;

	[Tooltip("Ignore repeat taps for this long. Stops a double-tap from opening two browser tabs while the app is still being backgrounded.")]
	public float retapCooldown;

	private Button _button;

	private string _resolvedUrl;

	private float _lastOpenTime;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	private void Start()
	{
	}

	public void Refresh()
	{
	}

	public void Open()
	{
	}

	private bool IsSchemeAllowed(string url)
	{
		return false;
	}
}
