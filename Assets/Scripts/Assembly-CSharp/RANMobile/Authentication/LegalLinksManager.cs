using UnityEngine;
using UnityEngine.UI;

namespace RANMobile.Authentication
{
	public class LegalLinksManager : MonoBehaviour
	{
		[Header("Website")]
		[Tooltip("Root of the website, e.g. https://trinityriftonline.com — no trailing slash needed. Use https:// for the live site; iOS blocks plain http.")]
		[SerializeField]
		private string _websiteBaseUrl;

		[Header("Paths")]
		[Tooltip("Terms of Service. The site's legal panel opens on Terms by default.")]
		[SerializeField]
		private string _termsOfServicePath;

		[Tooltip("Privacy Policy. The legal panel switches to Privacy on this path.")]
		[SerializeField]
		private string _privacyPolicyPath;

		[Header("Buttons (optional)")]
		[Tooltip("Assign these to have their OnClick wired at runtime. Leave empty if you prefer to hook the Open* methods in the inspector instead.")]
		[SerializeField]
		private Button _termsOfServiceButton;

		[SerializeField]
		private Button _privacyPolicyButton;

		[Header("Safety")]
		[Tooltip("Ignore repeat taps for this long. Stops a double-tap from opening two sheets while the app is still presenting the first one.")]
		[SerializeField]
		private float _retapCooldown;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		public void OpenTermsOfService()
		{
		}

		public void OpenPrivacyPolicy()
		{
		}

		public void OpenUrl(string pathOrUrl)
		{
		}

		private void Open(string pathOrUrl, string label)
		{
		}
	}
}
