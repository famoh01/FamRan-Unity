using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Builds the in-game registration panel by duplicating the login panel and
/// reconfiguring it (Tools > KlassX > Build Register Panel). Idempotent — an
/// existing Register panel is rebuilt from scratch each run.
/// </summary>
public static class RegisterPanelBuilder
{
	[MenuItem("Tools/KlassX/Build Register Panel")]
	public static void Build()
	{
		var log = new System.Text.StringBuilder();
		Transform account = FindChildOf("LoginAccount", "UIServerSelection");
		Transform login = FindChildOf("Login", "LoginAccount");
		if (account == null || login == null)
		{
			Debug.LogError("[RegisterPanelBuilder] LoginAccount/Login not found.");
			return;
		}

		// Idempotency: drop a previous attempt.
		var old = account.Find("Register");
		if (old != null)
		{
			Object.DestroyImmediate(old.gameObject);
			log.AppendLine("removed previous Register panel");
		}

		var reg = Object.Instantiate(login.gameObject, account);
		reg.name = "Register";
		reg.transform.SetSiblingIndex(login.GetSiblingIndex() + 1);
		reg.SetActive(false);

		var container = FindDeepIn(reg.transform, "Container");
		if (container == null)
		{
			Debug.LogError("[RegisterPanelBuilder] Container not found in duplicate.");
			return;
		}

		// Ripped eye-icon buttons carry template-less Dropdown components —
		// clicking them logs "The dropdown template is not assigned". Strip them.
		foreach (var d in reg.GetComponentsInChildren<UnityEngine.UI.Dropdown>(true))
		{
			if (d.template == null)
			{
				Object.DestroyImmediate(d);
			}
		}

		// Remove login-only rows: Remember Password toggle, register footer, legal links.
		Kill(container, "Toggle");
		Kill(container, "Register");
		Kill(container, "Legal");

		var title = container.Find("Title")?.GetComponent<TextMeshProUGUI>();
		var username = container.Find("Username")?.GetComponent<TMP_InputField>();
		var password = container.Find("Password")?.GetComponent<TMP_InputField>();
		var buttons = container.Find("Buttons");
		if (title == null || username == null || password == null || buttons == null)
		{
			Debug.LogError("[RegisterPanelBuilder] Unexpected login panel structure.");
			return;
		}

		title.text = "CREATE YOUR ACCOUNT";

		// Duplicate the simple username field into email + both PIN fields,
		// and the password field into the confirm-password field.
		var email = DupField(username, container, "Email");
		var pin = DupField(username, container, "PinCode");
		var confirmPin = DupField(username, container, "ConfirmPinCode");
		var confirmPw = DupField(password, container, "ConfirmPassword");

		SetPlaceholder(username, "Username");
		SetPlaceholder(email, "Email Address (optional)");
		SetPlaceholder(pin, "PIN Code (4-8 digits)");
		SetPlaceholder(confirmPin, "Confirm PIN Code");
		SetPlaceholder(password, "Password");
		SetPlaceholder(confirmPw, "Confirm Password");

		// The ripped password fields reference "LiberationSans SDF", which is
		// blank in this export — nothing renders. Copy the working Tahoma font
		// from the username field onto every field (register AND login panels).
		var goodFont = username.textComponent != null ? username.textComponent.font : null;
		if (goodFont != null)
		{
			foreach (var f in new[] { username, email, password, confirmPw, pin, confirmPin })
			{
				ApplyFont(f, goodFont);
			}
			// Original login panel's password field has the same broken font.
			var loginContainer = login.Find("Bg")?.Find("Container");
			if (loginContainer != null)
			{
				var loginPw = loginContainer.Find("Password")?.GetComponent<TMP_InputField>();
				ApplyFont(loginPw, goodFont);
			}
		}

		// Back + Login -> Back + Register. Duplicating copies the original
		// persistent onClick calls (Login / BackToServerSelect) — wipe them or
		// clicking Register would also fire a login and load CharacterSelection.
		var back = buttons.Find("Back");
		var submit = buttons.Find("LoginBtn");
		if (submit != null)
		{
			submit.name = "RegisterBtn";
			var btn = submit.GetComponent<Button>();
			if (btn != null)
			{
				btn.onClick = new Button.ButtonClickedEvent();
			}
			var t = submit.GetComponentInChildren<TextMeshProUGUI>(true);
			if (t != null)
			{
				t.text = "Register";
			}
		}
		if (back != null)
		{
			var btn = back.GetComponent<Button>();
			if (btn != null)
			{
				btn.onClick = new Button.ButtonClickedEvent();
			}
		}

		// Status line under the buttons (copy of the title, smaller).
		var status = Object.Instantiate(title.gameObject, container);
		status.name = "Status";
		status.SetActive(true);
		var statusText = status.GetComponent<TextMeshProUGUI>();
		statusText.text = string.Empty;
		statusText.fontSize = Mathf.Max(16f, title.fontSize * 0.75f);
		statusText.color = Color.white;

		// Child order for the vertical layout group.
		Transform[] order =
		{
			container.Find("Title"),
			container.Find("Username"),
			container.Find("Email"),
			container.Find("Password"),
			container.Find("ConfirmPassword"),
			container.Find("PinCode"),
			container.Find("ConfirmPinCode"),
			buttons,
			status.transform,
		};
		for (int i = 0; i < order.Length; i++)
		{
			if (order[i] != null)
			{
				order[i].SetSiblingIndex(i);
			}
		}

		// Layout: the screen is 882px tall in the game view — the panel has to
		// stay below the logo and above the bottom edge, so it is a little
		// shorter than the login panel with tighter, fixed-size rows.
		var vlg = container.GetComponent<VerticalLayoutGroup>();
		vlg.spacing = 8f;
		vlg.childForceExpandHeight = false;

		foreach (var f in new[] { username, email, password, confirmPw, pin, confirmPin })
		{
			var rt = (RectTransform)f.transform;
			var size = rt.sizeDelta;
			size.y = 70f;
			rt.sizeDelta = size;
		}
		var statusRt = (RectTransform)status.transform;
		var statusSize = statusRt.sizeDelta;
		statusSize.y = 22f;
		statusRt.sizeDelta = statusSize;

		var panelRt = (RectTransform)reg.transform;
		var panelSize = panelRt.sizeDelta;
		panelSize.y = 705f;
		panelRt.sizeDelta = panelSize;
		var pos = panelRt.anchoredPosition;
		pos.y = -84f;
		panelRt.anchoredPosition = pos;

		// Wire the controller that the login panel's "Register Here" button calls.
		var manager = Object.FindObjectsOfType<RegistrationManager>(true)
			.FirstOrDefault(m => m.gameObject.scene == reg.scene);
		if (manager == null)
		{
			Debug.LogError("[RegisterPanelBuilder] RegistrationManager not found in scene.");
			return;
		}
		manager.loginPanel = login.gameObject;
		manager.registerPanel = reg;
		manager.usernameField = username;
		manager.emailField = email;
		manager.passwordField = password;
		manager.confirmPasswordField = confirmPw;
		manager.pinField = pin;
		manager.confirmPinField = confirmPin;
		manager.registerButton = submit != null ? submit.GetComponent<Button>() : null;
		manager.backButton = back != null ? back.GetComponent<Button>() : null;
		manager.statusText = statusText;
		EditorUtility.SetDirty(manager);

		EditorSceneManager.MarkSceneDirty(reg.scene);
		EditorSceneManager.SaveOpenScenes();
		log.AppendLine("Register panel built and wired; scene saved.");
		Debug.Log("[RegisterPanelBuilder] " + log);
	}

	private static TMP_InputField DupField(TMP_InputField source, Transform parent, string name)
	{
		var go = Object.Instantiate(source.gameObject, parent);
		go.name = name;
		return go.GetComponent<TMP_InputField>();
	}

	private static void ApplyFont(TMP_InputField field, TMP_FontAsset font)
	{
		if (field == null)
		{
			return;
		}
		if (field.textComponent != null && field.textComponent.font != font)
		{
			field.textComponent.font = font;
		}
		var ph = field.transform.Find("Text Area/Placeholder")?.GetComponent<TextMeshProUGUI>();
		if (ph != null && ph.font != font)
		{
			ph.font = font;
		}
	}

	private static void SetPlaceholder(TMP_InputField field, string text)
	{
		if (field == null)
		{
			return;
		}
		var ph = field.transform.Find("Text Area/Placeholder")?.GetComponent<TextMeshProUGUI>();
		if (ph != null)
		{
			ph.text = text;
			// The password fields' placeholder rects are 0-sized in the ripped
			// prefab (so the hint never rendered) — give them a real height.
			var rt = ph.rectTransform;
			if (rt.sizeDelta.y <= 0f)
			{
				rt.sizeDelta = new Vector2(rt.sizeDelta.x, 15f);
			}
		}
	}

	private static void Kill(Transform parent, string childName)
	{
		var t = parent.Find(childName);
		if (t != null)
		{
			Object.DestroyImmediate(t.gameObject);
		}
	}

	private static Transform FindChildOf(string name, string parentName)
	{
		return Object.FindObjectsOfType<Transform>(true)
			.FirstOrDefault(t => t.name == name && t.parent != null && t.parent.name == parentName);
	}

	private static Transform FindDeepIn(Transform root, string name)
	{
		if (root.name == name)
		{
			return root;
		}
		for (int i = 0; i < root.childCount; i++)
		{
			var hit = FindDeepIn(root.GetChild(i), name);
			if (hit != null)
			{
				return hit;
			}
		}
		return null;
	}
}
