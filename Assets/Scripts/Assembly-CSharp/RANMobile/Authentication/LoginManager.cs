using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RANMobile.Authentication
{
	public class LoginManager : MonoBehaviour
	{
		[SerializeField]
		private TMP_InputField _gameIdField;

		[SerializeField]
		private TMP_InputField _passwordIdField;

		public TextMeshProUGUI _responseMsgTxt;

		[SerializeField]
		private Authentication _auth;

		[SerializeField]
		private Button _button;

		[Header("Remember Password")]
		[Tooltip("Checkbox in the login panel. When ticked, the last successful password is restored on the next launch. Hook its On Value Changed to OnRememberPasswordChanged.")]
		[SerializeField]
		private Toggle _rememberPasswordToggle;

		public bool isLoggingIn;

		private bool _advanced;

		private const string LastUsernameKey = "LastLoginUsername";

		private const string RememberPasswordKey = "RememberPassword";

		private const string LastPasswordKey = "LastLoginPassword";

		private const string PasswordObfuscationKey = "TrinityRift";

		public void Update()
		{
			// Only react while the login panel is actually on screen — Enter on
			// the main menu or the register panel must not trigger a login.
			if (_gameIdField == null || !_gameIdField.gameObject.activeInHierarchy)
			{
				return;
			}
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				EnterToInput();
			}
			else if (Input.GetKeyDown(KeyCode.Tab))
			{
				TabToInput();
			}
		}

		private void EnterToInput()
		{
			Login();
		}

		private void TabToInput()
		{
			if (_gameIdField != null && _gameIdField.isFocused)
			{
				_gameIdField.DeactivateInputField();
				if (_passwordIdField != null)
				{
					_passwordIdField.ActivateInputField();
				}
			}
			else if (_passwordIdField != null && _passwordIdField.isFocused)
			{
				_passwordIdField.DeactivateInputField();
				if (_gameIdField != null)
				{
					_gameIdField.ActivateInputField();
				}
			}
		}

		private IEnumerator Start()
		{
			RestoreRememberedPassword();
			if (_auth != null)
			{
				// Advance only AFTER the server confirms the login — otherwise the
				// scene change kills the request coroutine and the session token
				// is never captured (CharacterSelection then has nothing to fetch with).
				_auth.OnLoginSuccess.RemoveListener(LoginSucess);
				_auth.OnLoginSuccess.AddListener(LoginSucess);
				_auth.OnLoginFailed.RemoveListener(OnAuthLoginFailed);
				_auth.OnLoginFailed.AddListener(OnAuthLoginFailed);
			}
			yield return null;
			FocusPasswordField();
		}

		private void OnAuthLoginFailed(string error)
		{
			isLoggingIn = false;
			if (_button != null)
			{
				_button.interactable = true;
			}
			FetchResponseStatus(string.IsNullOrEmpty(error) ? "Login failed." : error);
		}

		private void RestoreRememberedPassword()
		{
			bool remember = PlayerPrefs.GetInt(RememberPasswordKey, 0) == 1;
			if (_rememberPasswordToggle != null)
			{
				_rememberPasswordToggle.isOn = remember;
			}
			if (!remember)
			{
				return;
			}
			if (_gameIdField != null)
			{
				_gameIdField.text = PlayerPrefs.GetString(LastUsernameKey, string.Empty);
			}
			if (_passwordIdField != null)
			{
				_passwordIdField.text = Unobfuscate(PlayerPrefs.GetString(LastPasswordKey, string.Empty));
			}
		}

		private void SaveRememberedPassword()
		{
			bool remember = _rememberPasswordToggle == null || _rememberPasswordToggle.isOn;
			PlayerPrefs.SetInt(RememberPasswordKey, remember ? 1 : 0);
			if (!remember)
			{
				PlayerPrefs.DeleteKey(LastUsernameKey);
				PlayerPrefs.DeleteKey(LastPasswordKey);
				return;
			}
			PlayerPrefs.SetString(LastUsernameKey, _gameIdField != null ? _gameIdField.text : string.Empty);
			PlayerPrefs.SetString(LastPasswordKey, Obfuscate(_passwordIdField != null ? _passwordIdField.text : string.Empty));
			PlayerPrefs.Save();
		}

		public void OnRememberPasswordChanged(bool remember)
		{
			PlayerPrefs.SetInt(RememberPasswordKey, remember ? 1 : 0);
			PlayerPrefs.Save();
		}

		private static string Obfuscate(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			byte[] key = Encoding.UTF8.GetBytes(PasswordObfuscationKey);
			byte[] data = Encoding.UTF8.GetBytes(value);
			byte[] result = new byte[data.Length];
			for (int i = 0; i < data.Length; i++)
			{
				result[i] = (byte)(data[i] ^ key[i % key.Length]);
			}
			return Convert.ToBase64String(result);
		}

		private static string Unobfuscate(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			try
			{
				byte[] key = Encoding.UTF8.GetBytes(PasswordObfuscationKey);
				byte[] data = Convert.FromBase64String(value);
				byte[] result = new byte[data.Length];
				for (int i = 0; i < data.Length; i++)
				{
					result[i] = (byte)(data[i] ^ key[i % key.Length]);
				}
				return Encoding.UTF8.GetString(result);
			}
			catch
			{
				return string.Empty;
			}
		}

		private void FocusPasswordField()
		{
			if (_gameIdField != null && string.IsNullOrEmpty(_gameIdField.text))
			{
				_gameIdField.ActivateInputField();
			}
			else if (_passwordIdField != null)
			{
				_passwordIdField.ActivateInputField();
			}
		}

		public void AutoLogin()
		{
			Login();
		}

		public void Login()
		{
			if (isLoggingIn)
			{
				return;
			}
			string id = _gameIdField != null ? _gameIdField.text.Trim() : string.Empty;
			string pw = _passwordIdField != null ? _passwordIdField.text : string.Empty;
			if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
			{
				FetchResponseStatus("Please enter your ID and password.");
				return;
			}
			isLoggingIn = true;
			if (_button != null)
			{
				_button.interactable = false;
			}
			SaveRememberedPassword();
			FetchResponseStatus("Logging in...");
			if (_auth != null)
			{
				_auth.LoginUser(id, pw);
			}
			// On success Authentication raises OnLoginSuccess -> LoginSucess()
			// (wired in Start), which loads CharacterSelection with the token set.
		}

		public void OpenURL(string url)
		{
			if (!string.IsNullOrEmpty(url))
			{
				Application.OpenURL(url);
			}
		}

		public void FetchResponseStatus(string res)
		{
			if (_responseMsgTxt != null)
			{
				_responseMsgTxt.text = res ?? string.Empty;
			}
		}

		private IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
		{
			yield return new WaitForSeconds(seconds);
			if (obj != null)
			{
				obj.SetActive(false);
			}
		}

		public void LoginSucess()
		{
			if (_advanced)
			{
				return;
			}
			_advanced = true;
			isLoggingIn = false;
			SceneManager.LoadScene("CharacterSelection");
		}

		public void OpenSettings()
		{
		}

		public void OnSoundMute()
		{
		}
	}
}
