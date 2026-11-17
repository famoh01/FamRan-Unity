using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// In-game account registration. The login panel's "Register Here" button
/// already calls <see cref="OpenRegistration"/> through a persistent scene
/// call, so no rewiring is needed. New accounts are POSTed to the KlassX
/// server's /api/auth/register endpoint and stored in its SQLite database.
/// </summary>
public class RegistrationManager : MonoBehaviour
{
	[Header("Panels")]
	[Tooltip("The login panel child (LoginAccount/Login). Hidden while the register panel is open.")]
	public GameObject loginPanel;

	[Tooltip("The register panel child (LoginAccount/Register).")]
	public GameObject registerPanel;

	[Header("Fields")]
	public TMP_InputField usernameField;

	public TMP_InputField emailField;

	public TMP_InputField passwordField;

	public TMP_InputField confirmPasswordField;

	public TMP_InputField pinField;

	public TMP_InputField confirmPinField;

	[Header("Controls")]
	public Button registerButton;

	public Button backButton;

	public TextMeshProUGUI statusText;

	[Header("Server")]
	[Tooltip("Root of the auth API, e.g. http://127.0.0.1:8085 - no trailing slash.")]
	public string apiBaseUrl = "http://127.0.0.1:8085";

	private static readonly Color ErrorColor = new Color(0.95f, 0.35f, 0.3f);

	private static readonly Color SuccessColor = new Color(0.35f, 0.85f, 0.4f);

	private bool _submitting;

	private Coroutine _returnCo;

	private void Awake()
	{
		AdoptApiBaseUrl();
	}

	private void OnEnable()
	{
		// Domain reloads wipe runtime AddListener calls — re-attach here so the
		// panel keeps working after a script recompile during play.
		if (registerButton != null)
		{
			registerButton.onClick.RemoveListener(Submit);
			registerButton.onClick.AddListener(Submit);
		}
		if (backButton != null)
		{
			backButton.onClick.RemoveListener(BackToLogin);
			backButton.onClick.AddListener(BackToLogin);
		}
	}

	private void AdoptApiBaseUrl()
	{
		var auth = FindObjectOfType<RANMobile.Authentication.Authentication>();
		if (auth != null && !string.IsNullOrEmpty(auth.apiBaseUrl))
		{
			apiBaseUrl = auth.apiBaseUrl.TrimEnd('/');
		}
	}

	/// <summary>Swap the login panel for the register panel.</summary>
	public void OpenRegistration()
	{
		Debug.Log("[RegistrationManager] OpenRegistration", this);
		if (loginPanel != null)
		{
			loginPanel.SetActive(false);
		}
		if (registerPanel != null)
		{
			registerPanel.SetActive(true);
		}
		if (_returnCo != null)
		{
			StopCoroutine(_returnCo);
			_returnCo = null;
		}
		SetStatus(string.Empty, Color.white);
		if (usernameField != null)
		{
			usernameField.text = string.Empty;
			usernameField.ActivateInputField();
		}
		if (emailField != null) emailField.text = string.Empty;
		if (passwordField != null) passwordField.text = string.Empty;
		if (confirmPasswordField != null) confirmPasswordField.text = string.Empty;
		if (pinField != null) pinField.text = string.Empty;
		if (confirmPinField != null) confirmPinField.text = string.Empty;
	}

	/// <summary>Swap the register panel back to the login panel.</summary>
	public void BackToLogin()
	{
		if (registerPanel != null)
		{
			registerPanel.SetActive(false);
		}
		if (loginPanel != null)
		{
			loginPanel.SetActive(true);
		}
		if (_returnCo != null)
		{
			StopCoroutine(_returnCo);
			_returnCo = null;
		}
	}

	public void Submit()
	{
		if (_submitting)
		{
			return;
		}
		string user = usernameField != null ? usernameField.text.Trim() : string.Empty;
		string email = emailField != null ? emailField.text.Trim() : string.Empty;
		string pw = passwordField != null ? passwordField.text : string.Empty;
		string pw2 = confirmPasswordField != null ? confirmPasswordField.text : string.Empty;
		string pin = pinField != null ? pinField.text.Trim() : string.Empty;
		string pin2 = confirmPinField != null ? confirmPinField.text.Trim() : string.Empty;

		if (user.Length < 3 || user.Length > 20)
		{
			SetStatus("Username must be 3-20 letters, digits or underscores.", ErrorColor);
			return;
		}
		if (pw.Length < 8)
		{
			SetStatus("Password must be at least 8 characters.", ErrorColor);
			return;
		}
		if (pw != pw2)
		{
			SetStatus("Passwords do not match.", ErrorColor);
			return;
		}
		if (!IsDigits(pin) || pin.Length < 4 || pin.Length > 8)
		{
			SetStatus("PIN must be 4-8 digits.", ErrorColor);
			return;
		}
		if (pin != pin2)
		{
			SetStatus("PINs do not match.", ErrorColor);
			return;
		}
		if (email.Length > 0 && email.IndexOf('@') < 0)
		{
			SetStatus("Invalid email address.", ErrorColor);
			return;
		}

		AdoptApiBaseUrl();
		_submitting = true;
		if (registerButton != null)
		{
			registerButton.interactable = false;
		}
		SetStatus("Creating account...", Color.white);
		string body = "{\"username\":\"" + EscapeJson(user) +
			"\",\"password\":\"" + EscapeJson(pw) +
			"\",\"confirmPassword\":\"" + EscapeJson(pw2) +
			"\",\"pincode\":\"" + EscapeJson(pin) +
			"\",\"confirmPincode\":\"" + EscapeJson(pin2) +
			"\",\"emailAddress\":\"" + EscapeJson(email) + "\"}";
		StartCoroutine(PostRegister(user, body));
	}

	private IEnumerator PostRegister(string user, string body)
	{
		using (UnityWebRequest req = new UnityWebRequest(apiBaseUrl + "/api/auth/register", "POST"))
		{
			byte[] raw = Encoding.UTF8.GetBytes(body);
			req.uploadHandler = new UploadHandlerRaw(raw);
			req.downloadHandler = new DownloadHandlerBuffer();
			req.SetRequestHeader("Content-Type", "application/json");
			yield return req.SendWebRequest();

			_submitting = false;
			if (registerButton != null)
			{
				registerButton.interactable = true;
			}

			string text = req.downloadHandler.text;
			if (req.result == UnityWebRequest.Result.Success && req.responseCode == 201)
			{
				SetStatus("Account created! You can now log in as " + user + ".", SuccessColor);
				FillLoginUsername(user);
				_returnCo = StartCoroutine(ReturnToLoginAfter(1.5f));
			}
			else
			{
				SetStatus(ExtractError(text) ?? ("Registration failed (" + req.responseCode + ")."), ErrorColor);
			}
		}
	}

	private IEnumerator ReturnToLoginAfter(float seconds)
	{
		yield return new WaitForSecondsRealtime(seconds);
		_returnCo = null;
		BackToLogin();
	}

	private void FillLoginUsername(string user)
	{
		if (loginPanel == null)
		{
			return;
		}
		var fields = loginPanel.GetComponentsInChildren<TMP_InputField>(true);
		if (fields.Length > 0 && fields[0] != null)
		{
			fields[0].text = user;
		}
	}

	private void SetStatus(string msg, Color color)
	{
		if (statusText != null)
		{
			statusText.text = msg ?? string.Empty;
			statusText.color = color;
		}
	}

	private static bool IsDigits(string s)
	{
		if (string.IsNullOrEmpty(s))
		{
			return false;
		}
		for (int i = 0; i < s.Length; i++)
		{
			if (s[i] < '0' || s[i] > '9')
			{
				return false;
			}
		}
		return true;
	}

	private static string EscapeJson(string s)
	{
		if (string.IsNullOrEmpty(s))
		{
			return string.Empty;
		}
		return s.Replace("\\", "\\\\").Replace("\"", "\\\"");
	}

	/// <summary>Pull the server's error string out of a flat JSON payload.</summary>
	private static string ExtractError(string json)
	{
		if (string.IsNullOrEmpty(json))
		{
			return null;
		}
		int i = json.IndexOf("\"error\"", StringComparison.Ordinal);
		if (i < 0)
		{
			return null;
		}
		int colon = json.IndexOf(':', i);
		if (colon < 0)
		{
			return null;
		}
		int quote = json.IndexOf('"', colon + 1);
		if (quote < 0)
		{
			return null;
		}
		int end = json.IndexOf('"', quote + 1);
		if (end < 0)
		{
			return null;
		}
		string error = json.Substring(quote + 1, end - quote - 1);
		if (error == "validation")
		{
			// Join the per-field messages: {"field":"x","message":"y"},...
			var parts = new System.Collections.Generic.List<string>();
			int m = 0;
			while (true)
			{
				int mi = json.IndexOf("\"message\"", m, StringComparison.Ordinal);
				if (mi < 0)
				{
					break;
				}
				int mq = json.IndexOf('"', json.IndexOf(':', mi) + 1);
				int me = mq >= 0 ? json.IndexOf('"', mq + 1) : -1;
				if (mq < 0 || me < 0)
				{
					break;
				}
				parts.Add(json.Substring(mq + 1, me - mq - 1));
				m = me + 1;
			}
			return parts.Count > 0 ? string.Join("; ", parts) : "Validation failed.";
		}
		return error;
	}
}
