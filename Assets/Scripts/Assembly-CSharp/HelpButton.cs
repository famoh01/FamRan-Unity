using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HelpButton : MonoBehaviour
{
	[Header("Content")]
	[Tooltip("Shared HelpTopic asset. Takes priority over the inline topic below.")]
	public HelpTopic topic;

	[Tooltip("One-off content, used only when no HelpTopic asset is assigned above.")]
	public HelpTopicData inlineTopic;

	[Header("Target")]
	[Tooltip("Leave empty to use HelpPanelView.Instance (the normal case). Set this only if the scene deliberately has more than one help panel.")]
	public HelpPanelView panelOverride;

	[Header("Diagnostics")]
	[Tooltip("Logs when this button is clicked and which topic it resolved. Leave on until the panel is confirmed working — a silent Console on click means the tap never reached the Button at all (raycast blocked, not interactable, or no EventSystem).")]
	public bool logDiagnostics;

	private Button _button;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void Open()
	{
	}
}
