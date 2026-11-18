using System;
using System.Collections;
using Models.Network.Shared.Response;
using RestClient.Core.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Talks to the KlassX game server over HTTP. The base URL is a serialized
	/// field so it can be pointed at localhost during development.
	/// </summary>
	public class Authentication : MonoBehaviour
	{
		[Header("Server")]
		[Tooltip("Root of the auth API, e.g. http://127.0.0.1:8085 - no trailing slash.")]
		public string apiBaseUrl = "http://127.0.0.1:8085";

		private LoginManager loginManager;

		private CharacterCreateManager characterCreationManager;

		private CharacterDeleteManager characterDeleteManager;

		public string serverSelected;

		/// <summary>Session token returned by the server after a successful login.
		/// Static so it survives the scene change into CharacterSelection.</summary>
		public static string SessionToken { get; private set; }

		public static int AccountId { get; private set; }

		/// <summary>Base URL captured from whichever Authentication instance woke up first.</summary>
		public static string BaseUrl { get; private set; }

		[Header("Events")]
		public UnityEvent OnLoginSuccess = new UnityEvent();

		public UnityEvent<string> OnLoginFailed = new UnityEvent<string>();

		public UnityEvent OnCreateCharacterSuccess = new UnityEvent();

		public UnityEvent<string> OnCreateCharacterFailed = new UnityEvent<string>();

		public UnityEvent OnDeleteCharacterSuccess = new UnityEvent();

		public UnityEvent<string> OnDeleteCharacterFailed = new UnityEvent<string>();

		private void Awake()
		{
			apiBaseUrl = FamHawaiiEndpoint.ApiBaseUrl(apiBaseUrl);
			loginManager = GetComponent<LoginManager>();
			characterCreationManager = GetComponent<CharacterCreateManager>();
			characterDeleteManager = GetComponent<CharacterDeleteManager>();
			if (!string.IsNullOrEmpty(apiBaseUrl))
			{
				BaseUrl = apiBaseUrl.TrimEnd('/');
			}
		}

		private void Update()
		{
		}

		/// <summary>POST /api/auth/login and continue on success.</summary>
		public void LoginUser(string username, string password)
		{
			string body = JsonUtility.ToJson(new LoginRequest { username = username, password = password });
			StartCoroutine(PostJson("/api/auth/login", body, (ok, json) =>
			{
				if (!ok)
				{
					LoginFailed(new Response { Error = json });
					return;
				}
				// LoginResponse.SessionToken is a property, so JsonUtility cannot fill it.
				// Parse the token out of the raw response instead.
				string token = ExtractString(json, "token");
				SessionToken = token;
				LoginResponse resp = new LoginResponse();
				if (!string.IsNullOrEmpty(token))
				{
					try { resp.SessionToken = token; } catch { }
				}
				LoginSuccess(username, password, resp);
			}));
		}

		/// <summary>Pull a top-level string value out of a flat JSON object.</summary>
		private static string ExtractString(string json, string key)
		{
			if (string.IsNullOrEmpty(json)) return null;
			int i = json.IndexOf("\"" + key + "\"", StringComparison.Ordinal);
			if (i < 0) return null;
			int colon = json.IndexOf(':', i);
			if (colon < 0) return null;
			int quote = json.IndexOf('"', colon + 1);
			if (quote < 0) return null;
			int end = json.IndexOf('"', quote + 1);
			if (end < 0) return null;
			return json.Substring(quote + 1, end - quote - 1);
		}

		/// <summary>POST /api/characters then refresh the selection screen.</summary>
		public void CreateCharacterUser(string charname, int charclass, int charschool, int charhairtype, int charhaircolortype, int charfacetype)
		{
			// "class" is a reserved C# word, so build the JSON by hand rather than
			// relying on JsonUtility field names.
			string body = "{\"name\":\"" + EscapeJson(charname) + "\"," +
				"\"class\":" + charclass + "," +
				"\"school\":" + charschool + "," +
				"\"hairType\":" + charhairtype + "," +
				"\"hairColorType\":" + charhaircolortype + "," +
				"\"faceType\":" + charfacetype + "}";
			StartCoroutine(PostJson("/api/characters", body, (ok, json) =>
			{
				if (ok) CreateCharacterSuccess();
				else CreateCharacterFailed(json);
			}));
		}

		/// <summary>Minimal JSON string escaping for the fields we send.</summary>
		private static string EscapeJson(string s)
		{
			if (string.IsNullOrEmpty(s)) return "";
			return s.Replace("\\", "\\\\").Replace("\"", "\\\"");
		}

		/// <summary>DELETE /api/characters/{id} then refresh.</summary>
		public void DeleteCharacterUser(string charID)
		{
			StartCoroutine(DeleteJson("/api/characters/" + charID, (ok, json) =>
			{
				if (ok) DeleteCharacterSuccess();
				else DeleteCharacterFailed(json);
			}));
		}

		private IEnumerator PostJson(string path, string jsonBody, Action<bool, string> done)
		{
			using (UnityWebRequest req = new UnityWebRequest(apiBaseUrl + path, "POST"))
			{
				byte[] raw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
				req.uploadHandler = new UploadHandlerRaw(raw);
				req.downloadHandler = new DownloadHandlerBuffer();
				req.SetRequestHeader("Content-Type", "application/json");
				if (!string.IsNullOrEmpty(SessionToken))
				{
					req.SetRequestHeader("Authorization", "Bearer " + SessionToken);
				}
				yield return req.SendWebRequest();
				done(req.result == UnityWebRequest.Result.Success, req.downloadHandler.text);
			}
		}

		private IEnumerator DeleteJson(string path, Action<bool, string> done)
		{
			using (UnityWebRequest req = new UnityWebRequest(apiBaseUrl + path, "DELETE"))
			{
				req.downloadHandler = new DownloadHandlerBuffer();
				if (!string.IsNullOrEmpty(SessionToken))
				{
					req.SetRequestHeader("Authorization", "Bearer " + SessionToken);
				}
				yield return req.SendWebRequest();
				done(req.result == UnityWebRequest.Result.Success, req.downloadHandler.text);
			}
		}

		public void LoginSuccess(string username, string password, LoginResponse response)
		{
			if (string.IsNullOrEmpty(serverSelected))
			{
				serverSelected = "Local";
			}
			OnLoginSuccess?.Invoke();
		}

		public void LoginFailed(Response res)
		{
			OnLoginFailed?.Invoke(res?.Error ?? "Login failed.");
		}

		public void CreateCharacterSuccess()
		{
			OnCreateCharacterSuccess?.Invoke();
			RefreshCharacterSelect(returnFromCreation: true);
		}

		private void OnCharacterCreateReceived(CharacterCreateResponse response)
		{
			CreateCharacterSuccess();
		}

		private static void RefreshCharacterSelect(bool returnFromCreation)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelection");
		}

		public void CreateCharacterFailed(string errorMessage)
		{
			OnCreateCharacterFailed?.Invoke(errorMessage);
		}

		public void DeleteCharacterSuccess()
		{
			OnDeleteCharacterSuccess?.Invoke();
			RefreshCharacterSelect(returnFromCreation: false);
		}

		private void OnCharacterDeleteReceived(CharacterDeleteResponse response)
		{
			DeleteCharacterSuccess();
		}

		public void DeleteCharacterFailed(string errorMessage)
		{
			OnDeleteCharacterFailed?.Invoke(errorMessage);
		}

		[Serializable]
		private class LoginRequest
		{
			public string username;
			public string password;
		}
	}
}
