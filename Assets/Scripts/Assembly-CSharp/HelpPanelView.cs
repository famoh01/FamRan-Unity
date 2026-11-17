using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanelView : MonoBehaviour
{
	[Header("Wiring")]
	[Tooltip("Root object toggled on/off. Should contain the backdrop and the panel itself.")]
	public GameObject root;

	[Tooltip("Full-screen transparent Button behind the panel; tapping it closes the popup. Optional.")]
	public Button backdropButton;

	public Button closeButton;

	[Header("Content")]
	public TMP_Text titleLabel;

	public TMP_Text bodyLabel;

	[Tooltip("Optional icon Image. Hidden automatically when the topic has no icon.")]
	public Image iconImage;

	[Header("Link")]
	[Tooltip("Optional button that opens the topic's Link Url in the device browser. Hidden automatically for topics with no link, so it can live permanently on the shared panel.")]
	public Button linkButton;

	[Tooltip("Caption on the link button. Optional — leave empty to keep whatever the prefab says.")]
	public TMP_Text linkButtonLabel;

	[Tooltip("Optional. Reset to the top whenever a topic is shown, so a long topic never opens mid-scroll from a previous one.")]
	public ScrollRect bodyScroll;

	[Header("Canvas")]
	[Tooltip("Canvas switched on with the panel. Auto-resolved from THIS GameObject when left empty. Needed because this project hides popup UI by disabling the Canvas COMPONENT — which leaves the GameObject active, so activating Root alone renders nothing.")]
	public Canvas canvas;

	private bool _ownsCanvas;

	public static HelpPanelView Instance { get; private set; }

	public bool IsOpen => false;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void Show(HelpTopic topic)
	{
	}

	public void Show(HelpTopicData topic)
	{
	}

	public void Close()
	{
	}

	private Canvas FindDisabledParentCanvas()
	{
		return null;
	}
}
