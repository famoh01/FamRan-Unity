using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace UnityMcpBridge
{
	[InitializeOnLoad]
	public static class Bridge
	{
		private const int Port = 6451;
		private static TcpListener s_Listener;
		private static Thread s_AcceptThread;
		private static bool s_Running;
		private static readonly Queue<Action> s_MainThreadQueue = new Queue<Action>();
		private static readonly object s_QueueLock = new object();
		private static readonly List<string> s_LogBuffer = new List<string>();
		private static readonly object s_LogLock = new object();
		private const int MaxLogEntries = 500;

		static Bridge()
		{
			Application.logMessageReceivedThreaded += OnLogMessage;
			EditorApplication.update += PumpMainThreadQueue;
			// Release the port before every domain reload (play mode enter/exit, recompile)
			// so the re-run InitializeOnLoad can bind cleanly.
			AssemblyReloadEvents.beforeAssemblyReload += StopListener;
			EditorApplication.quitting += StopListener;
			StartListener();
			Debug.Log("[UnityMcpBridge] started on 127.0.0.1:" + Port);
		}

		private static void StopListener()
		{
			s_Running = false;
			try
			{
				s_Listener?.Stop();
			}
			catch
			{
			}
			s_Listener = null;
		}

		private static void OnLogMessage(string condition, string stackTrace, LogType type)
		{
			lock (s_LogLock)
			{
				s_LogBuffer.Add(type + "|" + condition.Replace('\n', ' ').Replace('\r', ' '));
				if (s_LogBuffer.Count > MaxLogEntries)
				{
					s_LogBuffer.RemoveAt(0);
				}
			}
		}

		private static void StartListener()
		{
			for (int attempt = 0; attempt < 20; attempt++)
			{
				try
				{
					s_Listener = new TcpListener(IPAddress.Loopback, Port);
					s_Listener.Start();
					s_Running = true;
					s_AcceptThread = new Thread(AcceptLoop) { IsBackground = true, Name = "UnityMcpBridge" };
					s_AcceptThread.Start();
					return;
				}
				catch (SocketException)
				{
					// Port briefly held (TIME_WAIT) after a domain reload; retry.
					Thread.Sleep(250);
				}
				catch (Exception ex)
				{
					Debug.LogError("[UnityMcpBridge] failed to start: " + ex.Message);
					return;
				}
			}
			Debug.LogError("[UnityMcpBridge] could not bind port " + Port + " after retries.");
		}

		private static void AcceptLoop()
		{
			while (s_Running)
			{
				TcpClient client;
				try
				{
					client = s_Listener.AcceptTcpClient();
				}
				catch
				{
					return;
				}
				Thread worker = new Thread(() => HandleClient(client)) { IsBackground = true };
				worker.Start();
			}
		}

		private static void HandleClient(TcpClient client)
		{
			using (client)
			using (NetworkStream stream = client.GetStream())
			using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
			using (StreamWriter writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true, NewLine = "\n" })
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					if (line.Trim().Length == 0)
					{
						continue;
					}
					string response = ProcessLine(line);
					writer.WriteLine(response);
				}
			}
		}

		private static string ProcessLine(string line)
		{
			object id = null;
			try
			{
				Dictionary<string, object> request = MiniJson.Parse(line) as Dictionary<string, object>;
				if (request == null)
				{
					return MakeResponse(null, false, null, "invalid JSON request");
				}
				id = request.TryGetValue("id", out object requestId) ? requestId : null;
				string command = request.TryGetValue("command", out object cmd) ? cmd as string : null;
				Dictionary<string, object> payload = request.TryGetValue("payload", out object p)
					? p as Dictionary<string, object>
					: new Dictionary<string, object>();
				if (payload == null)
				{
					payload = new Dictionary<string, object>();
				}

				object result;
				if (command == "execute_csharp")
				{
					// runs on the worker thread, hopping to the main thread per step so the
					// editor can pump the AssemblyBuilder compilation between checks
					result = ExecuteCSharp(GetString(payload, "code"));
				}
				else
				{
					result = ExecuteOnMainThread(command, payload);
				}
				return MakeResponse(id, true, result, null);
			}
			catch (TargetInvocationException tie)
			{
				Exception inner = tie.InnerException ?? tie;
				return MakeResponse(id, false, null, inner.GetType().Name + ": " + inner.Message);
			}
			catch (Exception ex)
			{
				return MakeResponse(id, false, null, ex.GetType().Name + ": " + ex.Message);
			}
		}

		private static object ExecuteOnMainThread(string command, Dictionary<string, object> payload)
		{
			return EnqueueMain<object>(() => Dispatch(command, payload ?? new Dictionary<string, object>()));
		}

		private static void PumpMainThreadQueue()
		{
			while (true)
			{
				Action action = null;
				lock (s_QueueLock)
				{
					if (s_MainThreadQueue.Count > 0)
					{
						action = s_MainThreadQueue.Dequeue();
					}
				}
				if (action == null)
				{
					return;
				}
				action();
			}
		}

		private static object Dispatch(string command, Dictionary<string, object> payload)
		{
			switch (command)
			{
				case "ping":
					return new Dictionary<string, object>
					{
						{ "unityVersion", Application.unityVersion },
						{ "productName", PlayerSettings.productName },
						{ "projectPath", Directory.GetParent(Application.dataPath).FullName },
						{ "isPlaying", EditorApplication.isPlaying },
						{ "isPaused", EditorApplication.isPaused },
						{ "isCompiling", EditorApplication.isCompiling }
					};
				case "get_scene":
					return SceneTree(GetInt(payload, "maxDepth", 3));
				case "get_cameras":
					return GetCameras();
				case "get_render_pipeline":
					return GetRenderPipeline();
				case "screenshot":
					return Screenshot(GetString(payload, "path", "C:/LDCheck/UnityMCP/shot.png"));
				case "probe_camera":
					return ProbeCamera(GetString(payload, "path", "C:/LDCheck/UnityMCP/probe.png"));
				case "probe_test_camera":
					return ProbeTestCamera(GetString(payload, "path", "C:/LDCheck/UnityMCP/probe_test.png"));
				case "probe_sanity":
					return ProbeSanity(GetString(payload, "path", "C:/LDCheck/UnityMCP/probe_sanity.png"));
				case "save_scene":
					return SaveScene();
				case "load_scene":
					return LoadSceneRuntime(GetString(payload, "name"));
				case "open_scene":
					return OpenScene(GetString(payload, "path"));
				case "find_objects":
					return FindObjects(payload);
				case "create_game_object":
					return CreateGameObject(payload);
				case "delete_game_object":
					DeleteGameObject(GetString(payload, "path"));
					return "deleted";
				case "set_active":
				{
					Transform t = FindByPath(GetString(payload, "path"));
					if (t == null)
					{
						throw new ArgumentException("not found: " + GetString(payload, "path"));
					}
					bool active = GetBool(payload, "active", true);
					t.gameObject.SetActive(active);
					return new Dictionary<string, object> { { "path", GetPath(t) }, { "active", active } };
				}
				case "find_fullscreen_graphics":
					return FindFullscreenGraphics();
				case "probe_urp_request":
					return ProbeUrpRequest(GetString(payload, "path", "C:/LDCheck/UnityMCP/probe_urp.png"));
				case "inspect_urp":
					return InspectUrp();
				case "check_urp_shaders":
					return CheckUrpShaders();
				case "check_global_settings":
					return CheckGlobalSettings();
				case "probe_render_events":
					return ProbeRenderEvents();
				case "check_render_support":
					return CheckRenderSupport();
				case "repair_renderer":
					return RepairRenderer();
				case "create_post_process_data":
					return CreatePostProcessData();
				case "set_urp_enabled":
					return SetUrpEnabled(GetBool(payload, "enabled", true));
				case "probe_scene_camera":
					return ProbeSceneCamera();
				case "focus_game_view":
					return FocusGameView();
				case "repaint_all":
				{
					UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
					EditorApplication.QueuePlayerLoopUpdate();
					return "repainted";
				}
				case "set_transform":
					return SetTransform(payload);
				case "add_component":
					return AddComponent(payload);
				case "set_component_value":
					return SetComponentValue(payload);
				case "invoke_component_method":
					return InvokeComponentMethod(payload);
				case "play":
					EditorApplication.isPlaying = true;
					return "playing";
				case "pause":
					EditorApplication.isPaused = true;
					return "paused";
				case "stop":
					EditorApplication.isPlaying = false;
					return "stopped";
				case "get_console_logs":
					return GetConsoleLogs(payload);
				case "list_assets":
					return ListAssets(payload);
				case "execute_csharp":
					return ExecuteCSharp(GetString(payload, "code"));
				default:
					throw new ArgumentException("unknown command: " + command);
			}
		}

		private static object OpenScene(string path)
		{
			if (EditorApplication.isPlaying)
			{
				throw new InvalidOperationException("cannot open scene while playing");
			}
			// Accept either an asset path or a bare scene name.
			string assetPath = path;
			if (!assetPath.EndsWith(".unity", StringComparison.OrdinalIgnoreCase))
			{
				assetPath = "Assets/" + path;
				if (!assetPath.EndsWith(".unity", StringComparison.OrdinalIgnoreCase))
				{
					assetPath += ".unity";
				}
			}
			if (!assetPath.StartsWith("Assets/", StringComparison.Ordinal))
			{
				assetPath = "Assets/" + assetPath;
			}
			UnityEditor.SceneManagement.EditorSceneManager.OpenScene(assetPath);
			return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		}

		private static object LoadSceneRuntime(string name)
		{
			if (!EditorApplication.isPlaying)
			{
				throw new InvalidOperationException("load_scene is for play mode; use open_scene in edit mode");
			}
			UnityEngine.SceneManagement.SceneManager.LoadScene(name);
			return new Dictionary<string, object> { { "loading", name } };
		}

		private static object SaveScene()
		{
			if (EditorApplication.isPlaying)
			{
				throw new InvalidOperationException("cannot save scene while playing");
			}
			UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
			bool ok = UnityEditor.SceneManagement.EditorSceneManager.SaveScene(scene);
			return new Dictionary<string, object> { { "scene", scene.name }, { "saved", ok } };
		}

		private static object ProbeSanity(string path)
		{
			// Sanity check of the probe itself: clear an RT to solid GREEN and read it back.
			// If this does NOT come back green, the capture pipeline is broken (not the game).
			int w = 64, h = 64;
			RenderTexture rt = new RenderTexture(w, h, 0, RenderTextureFormat.ARGB32);
			rt.Create();
			RenderTexture prev = RenderTexture.active;
			try
			{
				RenderTexture.active = rt;
				GL.Clear(true, true, Color.green);
				Texture2D tex = new Texture2D(w, h, TextureFormat.RGB24, false);
				tex.ReadPixels(new Rect(0, 0, w, h), 0, 0);
				tex.Apply();
				byte[] bytes = tex.EncodeToPNG();
				File.WriteAllBytes(path, bytes);
				Color32[] px = tex.GetPixels32();
				UnityEngine.Object.Destroy(tex);
				long r = 0, g = 0, b = 0;
				foreach (Color32 c in px) { r += c.r; g += c.g; b += c.b; }
				int n = px.Length;
				return new Dictionary<string, object>
				{
					{ "avgR", r / n }, { "avgG", g / n }, { "avgB", b / n },
					{ "probeWorks", (g / n) > 200 && (r / n) < 60 },
					{ "expect", "green: avgG~255" },
					{ "path", path }
				};
			}
			finally
			{
				RenderTexture.active = prev;
				rt.Release();
				UnityEngine.Object.Destroy(rt);
			}
		}

		private static object ProbeTestCamera(string path)
		{
			// Spawn a fresh camera with a pure-red solid clear and render it. This isolates
			// "does URP render at all" from any scene/camera misconfiguration.
			GameObject go = new GameObject("__McpProbeCam");
			Camera cam = go.AddComponent<Camera>();
			cam.clearFlags = CameraClearFlags.SolidColor;
			cam.backgroundColor = Color.red;
			cam.cullingMask = 0; // render nothing but the clear color
			cam.fieldOfView = 60;
			cam.nearClipPlane = 0.3f;
			cam.farClipPlane = 100;
			RenderTexture rt = new RenderTexture(160, 90, 24, RenderTextureFormat.ARGB32);
			try
			{
				cam.targetTexture = rt;
				cam.Render();
				RenderTexture.active = rt;
				Texture2D tex = new Texture2D(160, 90, TextureFormat.RGB24, false);
				tex.ReadPixels(new Rect(0, 0, 160, 90), 0, 0);
				tex.Apply();
				byte[] bytes = tex.EncodeToPNG();
				File.WriteAllBytes(path, bytes);
				Color32[] px = tex.GetPixels32();
				UnityEngine.Object.Destroy(tex);
				long r = 0, g = 0, b = 0;
				HashSet<int> distinct = new HashSet<int>();
				foreach (Color32 c in px)
				{
					r += c.r; g += c.g; b += c.b;
					distinct.Add((c.r << 16) | (c.g << 8) | c.b);
				}
				int n = px.Length;
				return new Dictionary<string, object>
				{
					{ "avgR", r / n }, { "avgG", g / n }, { "avgB", b / n },
					{ "distinctColors", distinct.Count },
					{ "expectRed", "avgR should be ~255, avgG/B ~0" },
					{ "urpRenders", (r / n) > 50 && (g / n) < 60 && (b / n) < 60 },
					{ "path", path }
				};
			}
			finally
			{
				RenderTexture.active = null;
				cam.targetTexture = null;
				rt.Release();
				UnityEngine.Object.Destroy(rt);
				UnityEngine.Object.Destroy(go);
			}
		}

		private static object ProbeCamera(string path)
		{
			// Render the active camera into a RenderTexture explicitly. This is ground truth:
			// it does not depend on the Game view tab being focused/visible.
			Camera cam = Camera.main;
			if (cam == null)
			{
				cam = UnityEngine.Object.FindFirstObjectByType<Camera>();
			}
			if (cam == null)
			{
				return new Dictionary<string, object> { { "error", "no camera in scene" } };
			}

			int w = 320;
			int h = 180;
			RenderTexture rt = new RenderTexture(w, h, 24, RenderTextureFormat.ARGB32);
			RenderTexture prevTarget = cam.targetTexture;
			RenderTexture prevActive = RenderTexture.active;
			try
			{
				cam.targetTexture = rt;
				cam.Render();
				RenderTexture.active = rt;
				Texture2D tex = new Texture2D(w, h, TextureFormat.RGB24, false);
				tex.ReadPixels(new Rect(0, 0, w, h), 0, 0);
				tex.Apply();
				byte[] bytes = tex.EncodeToPNG();
				File.WriteAllBytes(path, bytes);

				// sample average + distinct colors
				Color32[] px = tex.GetPixels32();
				UnityEngine.Object.Destroy(tex);
				long r = 0, g = 0, b = 0;
				int nonblack = 0;
				HashSet<int> distinct = new HashSet<int>();
				foreach (Color32 c in px)
				{
					r += c.r; g += c.g; b += c.b;
					if (c.r + c.g + c.b > 30) nonblack++;
					distinct.Add((c.r << 16) | (c.g << 8) | c.b);
				}
				int n = px.Length;
				return new Dictionary<string, object>
				{
					{ "camera", cam.name },
					{ "clearFlags", cam.clearFlags.ToString() },
					{ "avgR", r / n }, { "avgG", g / n }, { "avgB", b / n },
					{ "nonblackPct", Math.Round(100.0 * nonblack / n, 2) },
					{ "distinctColors", distinct.Count },
					{ "path", path }
				};
			}
			finally
			{
				cam.targetTexture = prevTarget;
				RenderTexture.active = prevActive;
				rt.Release();
				UnityEngine.Object.Destroy(rt);
			}
		}

		private static object Screenshot(string path)
		{
			// ScreenCapture is reliable from a queued main-thread call; ReadPixels is not.
			Texture2D tex = UnityEngine.ScreenCapture.CaptureScreenshotAsTexture();
			if (tex == null)
			{
				return new Dictionary<string, object> { { "error", "CaptureScreenshotAsTexture returned null" } };
			}
			byte[] bytes = tex.EncodeToPNG();
			File.WriteAllBytes(path, bytes);
			int w = tex.width;
			int h = tex.height;
			UnityEngine.Object.Destroy(tex);
			return new Dictionary<string, object> { { "path", path }, { "width", w }, { "height", h }, { "bytes", bytes.Length } };
		}

		private static object GetRenderPipeline()
		{
			UnityEngine.Rendering.RenderPipelineAsset current = UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline;
			UnityEngine.Rendering.RenderPipelineAsset def = UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline;
			UnityEngine.Rendering.RenderPipelineAsset quality = UnityEngine.QualitySettings.renderPipeline;
			return new Dictionary<string, object>
			{
				{ "currentRenderPipeline", current != null ? current.GetType().FullName : "NULL" },
				{ "defaultRenderPipeline", def != null ? def.GetType().FullName : "NULL" },
				{ "qualityRenderPipeline", quality != null ? quality.GetType().FullName : "NULL" },
				{ "qualityLevel", UnityEngine.QualitySettings.GetQualityLevel() },
				{ "colorSpace", UnityEditor.PlayerSettings.colorSpace.ToString() },
				{ "isPlaying", EditorApplication.isPlaying }
			};
		}

		private static object GetCameras()
		{
			List<object> list = new List<object>();
			foreach (Camera cam in UnityEngine.Object.FindObjectsByType<Camera>(FindObjectsInactive.Include, FindObjectsSortMode.None))
			{
				list.Add(new Dictionary<string, object>
				{
					{ "name", cam.gameObject.name },
					{ "enabled", cam.enabled },
					{ "activeInHierarchy", cam.gameObject.activeInHierarchy },
					{ "clearFlags", cam.clearFlags.ToString() },
					{ "backgroundColor", cam.backgroundColor.ToString() },
					{ "cullingMask", cam.cullingMask.ToString() },
					{ "position", Vec(cam.transform.position) },
					{ "targetDisplay", cam.targetDisplay },
					{ "scene", cam.gameObject.scene.name },
					{ "targetTexture", cam.targetTexture != null }
				});
			}
			int audioListeners = UnityEngine.Object.FindObjectsByType<AudioListener>(FindObjectsInactive.Include, FindObjectsSortMode.None).Length;
			return new Dictionary<string, object> { { "cameras", list }, { "cameraCount", list.Count }, { "audioListeners", audioListeners } };
		}

		private static Dictionary<string, object> SceneTree(int maxDepth)
		{
			Dictionary<string, object> root = new Dictionary<string, object>
			{
				{ "scene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name },
				{ "roots", NodeArray(UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects(), 0, maxDepth) }
			};
			return root;
		}

		private static List<object> NodeArray(GameObject[] objects, int depth, int maxDepth)
		{
			List<object> list = new List<object>();
			foreach (GameObject go in objects)
			{
				list.Add(Node(go, depth, maxDepth));
			}
			return list;
		}

		private static Dictionary<string, object> Node(GameObject go, int depth, int maxDepth)
		{
			List<string> components = new List<string>();
			foreach (Component c in go.GetComponents<Component>())
			{
				components.Add(c != null ? c.GetType().Name : "MissingScript");
			}
			Dictionary<string, object> node = new Dictionary<string, object>
			{
				{ "name", go.name },
				{ "path", GetPath(go.transform) },
				{ "active", go.activeSelf },
				{ "components", components }
			};
			if (depth < maxDepth)
			{
				List<object> children = new List<object>();
				for (int i = 0; i < go.transform.childCount; i++)
				{
					children.Add(Node(go.transform.GetChild(i).gameObject, depth + 1, maxDepth));
				}
				node["children"] = children;
			}
			return node;
		}

		private static string GetPath(Transform t)
		{
			string path = t.name;
			while (t.parent != null)
			{
				t = t.parent;
				path = t.name + "/" + path;
			}
			return path;
		}

		private static object ProbeUrpRequest(string path)
		{
			return new Dictionary<string, object> { { "error", "disabled: SingleCameraRequest not accessible from editor assembly" } };
		}

		private static object FocusGameView()
		{
			// Find the GameView editor window via reflection and focus it, so an external
			// screen capture samples the Game view (not the Scene view).
			System.Type gameViewType = null;
			foreach (System.Type t in typeof(EditorWindow).Assembly.GetTypes())
			{
				if (t.Name == "GameView")
				{
					gameViewType = t;
					break;
				}
			}
			if (gameViewType == null)
			{
				return new Dictionary<string, object> { { "error", "GameView type not found" } };
			}
			EditorWindow[] wins = Resources.FindObjectsOfTypeAll<EditorWindow>();
			foreach (EditorWindow w in wins)
			{
				if (w.GetType() == gameViewType)
				{
					w.Focus();
					w.Repaint();
					// send the window title so caller can locate it
					return new Dictionary<string, object>
					{
						{ "focused", true },
						{ "title", w.titleContent.text },
						{ "position", w.position.ToString() },
						{ "hasFocus", w.hasFocus }
					};
				}
			}
			return new Dictionary<string, object> { { "error", "GameView window not open" } };
		}

		private static object ProbeSceneCamera()
		{
			Camera cam = Camera.main;
			if (cam == null)
			{
				cam = UnityEngine.Object.FindFirstObjectByType<Camera>();
			}
			if (cam == null)
			{
				return new Dictionary<string, object> { { "error", "no camera" } };
			}

			var result = new Dictionary<string, object>
			{
				{ "camera", cam.name },
				{ "pos", Vec(cam.transform.position) },
				{ "forward", Vec(cam.transform.forward) },
				{ "clearFlags", cam.clearFlags.ToString() },
				{ "bg", cam.backgroundColor.ToString() },
				{ "cullingMask", cam.cullingMask.ToString() },
				{ "enabled", cam.enabled },
				{ "nearFar", cam.nearClipPlane + " / " + cam.farClipPlane }
			};

			// raycast from camera center
			Ray ray = new Ray(cam.transform.position, cam.transform.forward);
			if (Physics.Raycast(ray, out RaycastHit hit, cam.farClipPlane))
			{
				result["centerHit"] = hit.collider.gameObject.name + " @ " + hit.distance.ToString("F2");
				Renderer r = hit.collider.GetComponent<Renderer>();
				if (r != null)
				{
					result["hitRenderer"] = r.GetType().Name;
					result["hitMaterial"] = r.sharedMaterial != null ? r.sharedMaterial.name : "null";
					result["hitBoundsSize"] = r.bounds.size.ToString();
				}
			}
			else
			{
				result["centerHit"] = "NOTHING (clear color should show)";
			}

			// is camera inside any collider/mesh bounds?
			List<object> containing = new List<object>();
			foreach (Renderer r in UnityEngine.Object.FindObjectsByType<Renderer>(FindObjectsInactive.Include, FindObjectsSortMode.None))
			{
				if (r.bounds.Contains(cam.transform.position))
				{
					containing.Add(new Dictionary<string, object>
					{
						{ "name", r.gameObject.name },
						{ "boundsSize", r.bounds.size.ToString() },
						{ "material", r.sharedMaterial != null ? r.sharedMaterial.name : "null" },
						{ "materialShader", r.sharedMaterial != null && r.sharedMaterial.shader != null ? r.sharedMaterial.shader.name : "null" }
					});
				}
			}
			result["cameraInsideRenderers"] = containing;
			return result;
		}

		private static UnityEngine.Rendering.RenderPipelineAsset s_SavedUrp;

		private static object CreatePostProcessData()
		{
			// The renderer's postProcessData is null because AssetRipper never exported the
			// PostProcessData asset. Create one via URP's own type and assign its shaders
			// via Shader.Find, then wire it into the renderer and re-validate.
			var result = new Dictionary<string, object>();
			System.Type ppdType = System.Type.GetType("UnityEngine.Rendering.Universal.PostProcessData, Unity.RenderPipelines.Universal.Runtime");
			if (ppdType == null)
			{
				result["error"] = "PostProcessData type not found";
				return result;
			}

			string path = "Assets/MonoBehaviour/PostProcessData.asset";
			ScriptableObject ppd = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
			bool created = false;
			if (ppd == null)
			{
				ppd = ScriptableObject.CreateInstance(ppdType);
				AssetDatabase.CreateAsset(ppd, path);
				created = true;
			}
			result["created"] = created;
			result["ppdType"] = ppd.GetType().FullName;

			// assign shaders by field name using Shader.Find of known URP shader names
			var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
			var shaderResourcesField = ppdType.GetField("shaders", flags);
			object shaderResources = shaderResourcesField != null ? shaderResourcesField.GetValue(ppd) : null;
			if (shaderResources != null)
			{
				var srf = shaderResources.GetType();
				var map = new Dictionary<string, string>
				{
					{ "stopNanPS", "Hidden/Post Processing/Stop NaN" },
					{ "subpixelMorphologicalAntialiasingPS", "Hidden/Post Processing/SubpixelMorphologicalAntialiasing" },
					{ "gaussianDepthOfFieldPS", "Hidden/Post Processing/Gaussian Depth Of Field" },
					{ "bokehDepthOfFieldPS", "Hidden/Post Processing/Bokeh Depth Of Field" },
					{ "cameraMotionBlurPS", "Hidden/Post Processing/Camera Motion Blur" },
					{ "paniniProjectionPS", "Hidden/Post Processing/Panini Projection" },
					{ "lutBuilderLdrPS", "Hidden/Post Processing/LutBuilderLDR" },
					{ "lutBuilderHdrPS", "Hidden/Post Processing/LutBuilderHDR" },
					{ "bloomPS", "Hidden/Post Processing/Bloom" },
					{ "temporalAntialiasingPS", "Hidden/Post Processing/Temporal Antialiasing" },
					{ "LensFlareDataDrivenPS", "Hidden/Post Processing/LensFlareDataDriven" },
					{ "LensFlareScreenSpacePS", "Hidden/Post Processing/LensFlareScreenSpace" },
					{ "scalingSetupPS", "Hidden/Post Processing/Scaling Setup" },
					{ "easuPS", "Hidden/Post Processing/EASU" },
					{ "uberPostPS", "Hidden/Post Processing/UberPost" },
					{ "finalPostPassPS", "Hidden/Post Processing/FinalPost" }
				};
				int assigned = 0, missing = 0;
				foreach (var kv in map)
				{
					var f = srf.GetField(kv.Key, flags);
					if (f == null) continue;
					Shader s = Shader.Find(kv.Value);
					if (s != null) { f.SetValue(shaderResources, s); assigned++; }
					else { missing++; }
				}
				result["shadersAssigned"] = assigned;
				result["shadersMissing"] = missing;
				shaderResourcesField.SetValue(ppd, shaderResources);
			}
			else
			{
				result["shaderResources"] = "null (field missing or not initialized)";
			}

			EditorUtility.SetDirty(ppd);
			AssetDatabase.SaveAssets();

			// wire into renderer
			string rendererPath = "Assets/MonoBehaviour/New Universal Render Pipeline Asset_Renderer.asset";
			ScriptableObject rd = AssetDatabase.LoadAssetAtPath<ScriptableObject>(rendererPath);
			if (rd != null)
			{
				var ppdField = rd.GetType().GetField("postProcessData", flags);
				if (ppdField != null)
				{
					ppdField.SetValue(rd, ppd);
					EditorUtility.SetDirty(rd);
					AssetDatabase.SaveAssets();
					result["rendererPostProcessData"] = "assigned";
				}
			}

			// re-validate
			var asset = UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline;
			var vmethod = asset.GetType().GetMethod("ValidateRendererDataList", flags);
			if (vmethod != null)
			{
				try
				{
					object res = vmethod.Invoke(asset, new object[] { true });
					result["ValidateRendererDataList"] = res != null ? res.ToString() : "void";
				}
				catch (Exception ex)
				{
					result["validateErr"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
				}
			}
			return result;
		}

		private static object RepairRenderer()
		{
			// Load the URP renderer data asset and call URP's own ReloadAllNullProperties,
			// which repopulates null shader references via Shader.Find. Then re-validate.
			var result = new Dictionary<string, object>();
			string rendererPath = "Assets/MonoBehaviour/New Universal Render Pipeline Asset_Renderer.asset";
			ScriptableObject rd = AssetDatabase.LoadAssetAtPath<ScriptableObject>(rendererPath);
			if (rd == null)
			{
				result["error"] = "renderer asset not found at " + rendererPath;
				return result;
			}
			result["rendererType"] = rd.GetType().FullName;

			var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
			var reload = rd.GetType().GetMethod("ReloadAllNullProperties", flags);
			result["hasReloadAllNullProperties"] = reload != null;
			if (reload != null)
			{
				try
				{
					reload.Invoke(rd, null);
					result["reloadCalled"] = true;
				}
				catch (Exception ex)
				{
					result["reloadErr"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
				}
			}

			// check postProcessData now
			var ppd = rd.GetType().GetField("postProcessData", flags);
			if (ppd != null)
			{
				object v = ppd.GetValue(rd);
				result["postProcessData"] = v == null ? "NULL" : v.GetType().Name;
			}

			EditorUtility.SetDirty(rd);
			AssetDatabase.SaveAssets();

			// re-run pipeline validation
			var asset = UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline;
			var vmethod = asset.GetType().GetMethod("ValidateRendererDataList", flags);
			if (vmethod != null)
			{
				try
				{
					object res = vmethod.Invoke(asset, new object[] { true });
					result["ValidateRendererDataList_after"] = res != null ? res.ToString() : "void";
				}
				catch (Exception ex)
				{
					result["validateErr"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
				}
			}
			return result;
		}

		private static object CheckRenderSupport()
		{
			var r = new Dictionary<string, object>();
			r["supportsRenderPipelines"] = UnityEngine.Rendering.GraphicsSettings.isScriptableRenderPipelineEnabled;
			r["defaultRenderPipeline"] = UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline != null
				? UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline.name : "NULL";
			r["currentRenderPipeline"] = UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline != null
				? UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline.name : "NULL";
			r["qualityLevel"] = UnityEngine.QualitySettings.GetQualityLevel();
			r["qualityRenderPipeline"] = UnityEngine.QualitySettings.renderPipeline != null
				? UnityEngine.QualitySettings.renderPipeline.name : "NULL";
			r["activeColorSpace"] = UnityEditor.PlayerSettings.colorSpace.ToString();
			r["graphicsDeviceType"] = SystemInfo.graphicsDeviceType.ToString();
			r["graphicsDeviceVersion"] = SystemInfo.graphicsDeviceVersion;
			r["graphicsMemoryMB"] = SystemInfo.graphicsMemorySize;
			r["supportsComputeShaders"] = SystemInfo.supportsComputeShaders;
			r["maxTextureSize"] = SystemInfo.maxTextureSize;
			r["uvStartsAtTop"] = SystemInfo.graphicsUVStartsAtTop;

			// is the URP asset itself "valid"?
			var asset = UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline;
			if (asset != null)
			{
				try
				{
					var m = asset.GetType().GetMethod("ValidateRendererDataList", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
					if (m != null)
					{
						object res = m.Invoke(asset, new object[] { true });
						r["ValidateRendererDataList"] = res != null ? res.ToString() : "void";
					}
					var m2 = asset.GetType().GetMethod("ValidateRendererData", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
					if (m2 != null)
					{
						object res2 = m2.Invoke(asset, new object[] { true });
						r["ValidateRendererData"] = res2 != null ? res2.ToString() : "void";
					}
				}
				catch (Exception ex)
				{
					r["validateErr"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
				}
			}
			return r;
		}

		private static int s_BeginCount;
		private static int s_EndCount;
		private static string s_LastError;
		private static bool s_Hooked;

		private static object ProbeRenderEvents()
		{
			// Hook SRP render events + catch exceptions to see whether URP's camera
			// render actually runs, and what it throws.
			if (!s_Hooked)
			{
				UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering += (ctx, cam) =>
				{
					s_BeginCount++;
				};
				UnityEngine.Rendering.RenderPipelineManager.endCameraRendering += (ctx, cam) =>
				{
					s_EndCount++;
				};
				Application.logMessageReceived += (condition, stackTrace, type) =>
				{
					if (type == LogType.Error || type == LogType.Exception)
					{
						s_LastError = condition + " | " + (stackTrace != null && stackTrace.Length > 200 ? stackTrace.Substring(0, 200) : "");
					}
				};
				s_Hooked = true;
			}
			int b = s_BeginCount, e = s_EndCount;
			string err = s_LastError;
			s_BeginCount = 0;
			s_EndCount = 0;
			s_LastError = null;
			return new Dictionary<string, object>
			{
				{ "beginCameraRendering", b },
				{ "endCameraRendering", e },
				{ "lastError", err == null ? "none" : err },
				{ "isPlaying", EditorApplication.isPlaying }
			};
		}

		private static object CheckGlobalSettings()
		{
			// Read the URP global settings via the public API and report whether the
			// runtime shader resources (coreBlitPS etc.) resolved.
			var result = new Dictionary<string, object>();
			try
			{
				System.Type gsType = System.Type.GetType("UnityEngine.Rendering.Universal.UniversalRenderPipelineGlobalSettings, Unity.RenderPipelines.Universal.Runtime");
				result["globalSettingsType"] = gsType != null ? gsType.FullName : "type not found";

				// GraphicsSettings.GetSettingsForRenderPipeline<TRenderPipeline>()
				System.Type gfx = typeof(UnityEngine.Rendering.GraphicsSettings);
				var methods = gfx.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
				foreach (var m in methods)
				{
					if (m.Name == "GetSettingsForRenderPipeline" && m.IsGenericMethodDefinition)
					{
						try
						{
							var mi = m.MakeGenericMethod(gsType);
							object gs = mi.Invoke(null, null);
							result["globalSettingsInstance"] = gs == null ? "NULL" : gs.GetType().Name;
							if (gs != null)
							{
								// dump the settings list
								var flags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;
								var settingsField = gsType.GetField("m_Settings", flags);
								result["m_Settings"] = settingsField == null ? "field missing" : (settingsField.GetValue(gs) == null ? "NULL" : "present");
							}
							break;
						}
						catch (Exception ex)
						{
							result["invokeErr"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
						}
					}
				}
			}
			catch (Exception ex)
			{
				result["error"] = ex.Message;
			}
			return result;
		}

		private static object CheckUrpShaders()
		{
			// Ask Unity directly whether each URP-internal shader name resolves, and whether
			// the Global Settings asset is registered. This is ground truth for the black screen.
			string[] names = new string[]
			{
				"Hidden/Universal Render Pipeline/Blit",
				"Hidden/Universal/CoreBlit",
				"Hidden/Universal/CoreBlitColorAndDepth",
				"Hidden/Universal Render Pipeline/CopyDepth",
				"Hidden/Universal Render Pipeline/FallbackError",
				"Hidden/Universal Render Pipeline/MaterialError",
				"Hidden/Universal Render Pipeline/Sampling",
				"Universal Render Pipeline/Lit"
			};
			var found = new Dictionary<string, object>();
			foreach (string n in names)
			{
				Shader s = Shader.Find(n);
				found[n] = s == null ? "NULL" : (s.isSupported ? "OK" : "FOUND_BUT_UNSUPPORTED");
			}

			// Global settings registration
			object globalSettings = null;
			try
			{
				var method = typeof(UnityEngine.Rendering.GraphicsSettings).GetMethod(
					"GetRenderPipelineSettings", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static,
					null, new Type[] { }, null);
				// fallback: read the internal map via reflection
				var field = typeof(UnityEngine.Rendering.GraphicsSettings).GetField(
					"s_RenderPipelineGlobalSettingsMap", BindingFlags.NonPublic | BindingFlags.Static);
				if (field != null)
				{
					globalSettings = field.GetValue(null);
				}
			}
			catch (Exception ex)
			{
				globalSettings = "ERR: " + ex.Message;
			}

			return new Dictionary<string, object>
			{
				{ "shaders", found },
				{ "globalSettingsMapType", globalSettings != null ? globalSettings.GetType().Name : "null" },
				{ "defaultPipeline", UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline != null ? UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline.GetType().Name : "null" }
			};
		}

		private static object SetUrpEnabled(bool enabled)
		{
			// Temporarily swap the URP asset out (to Built-in) to isolate whether URP is
			// the black-screen cause. Saves the previous value so it can be restored.
			if (!enabled)
			{
				if (s_SavedUrp == null)
				{
					s_SavedUrp = UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline;
				}
				UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline = null;
				UnityEngine.QualitySettings.renderPipeline = null;
				return new Dictionary<string, object>
				{
					{ "urpEnabled", false },
					{ "saved", s_SavedUrp != null ? s_SavedUrp.GetType().Name : "null" },
					{ "current", UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline == null ? "NULL (builtin)" : "set" }
				};
			}
			if (s_SavedUrp != null)
			{
				UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline = s_SavedUrp;
				UnityEngine.QualitySettings.renderPipeline = s_SavedUrp;
			}
			return new Dictionary<string, object>
			{
				{ "urpEnabled", true },
				{ "current", UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline != null ? "URP restored" : "still null" }
			};
		}

		private static object InspectUrp()
		{
			var asset = UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline;
			var dict = new Dictionary<string, object>();
			dict["assetType"] = asset != null ? asset.GetType().FullName : "NULL";
			if (asset != null)
			{
				// reflect into m_RendererDataList / m_RendererData
				var flags = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
				var listField = asset.GetType().GetField("m_RendererDataList", flags);
				var dataField = asset.GetType().GetField("m_RendererData", flags);
				if (listField != null)
				{
					var arr = listField.GetValue(asset) as Array;
					int n = arr != null ? arr.Length : -1;
					dict["rendererDataListLen"] = n;
					if (arr != null)
					{
						var types = new List<string>();
						for (int i = 0; i < arr.Length; i++)
						{
							object item = arr.GetValue(i);
							types.Add(item == null ? "NULL" : item.GetType().Name);
						}
						dict["rendererDataTypes"] = types;
					}
				}
				if (dataField != null)
				{
					object d = dataField.GetValue(asset);
					dict["rendererData"] = d == null ? "NULL" : d.GetType().Name;
				}
			}
			// quality-level override
			var q = UnityEngine.QualitySettings.renderPipeline;
			dict["qualityPipeline"] = q == null ? "NULL" : q.GetType().Name;
			dict["qualityLevel"] = UnityEngine.QualitySettings.GetQualityLevel();
			return dict;
		}

		private static object FindFullscreenGraphics()
		{
			// Find UI Graphics (Image/RawImage) that cover the whole screen with high alpha.
			// These are the candidates for "something is covering the camera view".
			List<object> found = new List<object>();
			foreach (UnityEngine.UI.Graphic g in UnityEngine.Object.FindObjectsByType<UnityEngine.UI.Graphic>(FindObjectsInactive.Include, FindObjectsSortMode.None))
			{
				RectTransform rt = g.rectTransform;
				if (rt == null)
				{
					continue;
				}
				// covers screen?
				Vector3[] corners = new Vector3[4];
				rt.GetWorldCorners(corners);
				float minx = float.MaxValue, maxx = float.MinValue, miny = float.MaxValue, maxy = float.MinValue;
				for (int i = 0; i < 4; i++)
				{
					minx = Math.Min(minx, corners[i].x); maxx = Math.Max(maxx, corners[i].x);
					miny = Math.Min(miny, corners[i].y); maxy = Math.Max(maxy, corners[i].y);
				}
				float w = maxx - minx, h = maxy - miny;
				if (w < Screen.width * 0.9f || h < Screen.height * 0.9f)
				{
					continue;
				}
				// effective alpha incl. CanvasGroup ancestors
				float alpha = g.color.a;
				float groupAlpha = 1f;
				Transform p = g.transform;
				while (p != null)
				{
					CanvasGroup cg = p.GetComponent<CanvasGroup>();
					if (cg != null)
					{
						groupAlpha *= cg.alpha;
					}
					p = p.parent;
				}
				Canvas canvas = g.GetComponentInParent<Canvas>();
				int sorting = canvas != null ? canvas.sortingOrder : -999;
				bool raycastable = g.raycastTarget;
				found.Add(new Dictionary<string, object>
				{
					{ "path", GetPath(g.transform) },
					{ "type", g.GetType().Name },
					{ "color", g.color.ToString() },
					{ "effectiveAlpha", Math.Round(alpha * groupAlpha, 3) },
					{ "canvasGroupAlpha", Math.Round(groupAlpha, 3) },
					{ "sizePx", Math.Round(w) + "x" + Math.Round(h) },
					{ "screenPx", Screen.width + "x" + Screen.height },
					{ "sortingOrder", sorting },
					{ "raycastTarget", raycastable }
				});
			}
			return new Dictionary<string, object>
			{
				{ "count", found.Count },
				{ "candidates", found },
				{ "isPlaying", EditorApplication.isPlaying }
			};
		}

		private static Transform FindByPath(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}
			foreach (GameObject root in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
			{
				Transform found = FindRecursive(root.transform, path);
				if (found != null)
				{
					return found;
				}
			}
			return null;
		}

		private static Transform FindRecursive(Transform current, string path)
		{
			if (GetPath(current) == path)
			{
				return current;
			}
			for (int i = 0; i < current.childCount; i++)
			{
				Transform result = FindRecursive(current.GetChild(i), path);
				if (result != null)
				{
					return result;
				}
			}
			return null;
		}

		private static object FindObjects(Dictionary<string, object> payload)
		{
			string nameContains = GetString(payload, "nameContains");
			string typeName = GetString(payload, "typeName");
			bool includeInactive = GetBool(payload, "includeInactive", true);
			List<object> results = new List<object>();
			foreach (GameObject go in UnityEngine.Object.FindObjectsByType<GameObject>(
				includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude,
				FindObjectsSortMode.None))
			{
				if (!string.IsNullOrEmpty(nameContains) && go.name.IndexOf(nameContains, StringComparison.OrdinalIgnoreCase) < 0)
				{
					continue;
				}
				if (!string.IsNullOrEmpty(typeName))
				{
					Component c = go.GetComponent(typeName);
					if (c == null)
					{
						continue;
					}
				}
				results.Add(new Dictionary<string, object>
				{
					{ "name", go.name },
					{ "path", GetPath(go.transform) },
					{ "components", ComponentNames(go) }
				});
			}
			return new Dictionary<string, object> { { "count", results.Count }, { "objects", results } };
		}

		private static List<string> ComponentNames(GameObject go)
		{
			List<string> names = new List<string>();
			foreach (Component c in go.GetComponents<Component>())
			{
				names.Add(c != null ? c.GetType().Name : "MissingScript");
			}
			return names;
		}

		private static object CreateGameObject(Dictionary<string, object> payload)
		{
			string name = GetString(payload, "name", "New GameObject");
			GameObject go = new GameObject(name);

			string parentPath = GetString(payload, "parentPath");
			if (!string.IsNullOrEmpty(parentPath))
			{
				Transform parent = FindByPath(parentPath);
				if (parent == null)
				{
					throw new ArgumentException("parent not found: " + parentPath);
				}
				go.transform.SetParent(parent, false);
			}

			List<object> components = payload.TryGetValue("components", out object compObj) ? compObj as List<object> : null;
			if (components != null)
			{
				foreach (object comp in components)
				{
					string typeName = comp as string;
					if (string.IsNullOrEmpty(typeName))
					{
						continue;
					}
					Type type = ResolveType(typeName);
					if (type == null)
					{
						throw new ArgumentException("component type not found: " + typeName);
					}
					go.AddComponent(type);
				}
			}

			go.transform.position = GetVector3(payload, "position", Vector3.zero);
			go.transform.eulerAngles = GetVector3(payload, "rotationEuler", Vector3.zero);
			go.transform.localScale = GetVector3(payload, "scale", Vector3.one);

			Undo.RegisterCreatedObjectUndo(go, "MCP Create " + name);
			return new Dictionary<string, object> { { "path", GetPath(go.transform) }, { "name", go.name } };
		}

		private static void DeleteGameObject(string path)
		{
			Transform t = FindByPath(path);
			if (t == null)
			{
				throw new ArgumentException("not found: " + path);
			}
			Undo.DestroyObjectImmediate(t.gameObject);
		}

		private static object SetTransform(Dictionary<string, object> payload)
		{
			Transform t = RequireTransform(payload);
			if (payload.ContainsKey("position"))
			{
				t.position = GetVector3(payload, "position", t.position);
			}
			if (payload.ContainsKey("rotationEuler"))
			{
				t.eulerAngles = GetVector3(payload, "rotationEuler", t.eulerAngles);
			}
			if (payload.ContainsKey("scale"))
			{
				t.localScale = GetVector3(payload, "scale", t.localScale);
			}
			return new Dictionary<string, object>
			{
				{ "path", GetPath(t) },
				{ "position", Vec(t.position) },
				{ "rotationEuler", Vec(t.eulerAngles) },
				{ "scale", Vec(t.localScale) }
			};
		}

		private static object AddComponent(Dictionary<string, object> payload)
		{
			Transform t = RequireTransform(payload);
			string typeName = GetString(payload, "componentType");
			Type type = ResolveType(typeName);
			if (type == null)
			{
				throw new ArgumentException("component type not found: " + typeName);
			}
			Component c = Undo.AddComponent(t.gameObject, type);
			return c.GetType().Name;
		}

		private static object InvokeComponentMethod(Dictionary<string, object> payload)
		{
			string typeName = GetString(payload, "componentType");
			string methodName = GetString(payload, "method");
			string path = GetString(payload, "path");
			List<object> rawArgs = payload.TryGetValue("args", out object a) ? a as List<object> : null;

			UnityEngine.Object target = null;
			if (!string.IsNullOrEmpty(path))
			{
				Transform t = FindByPath(path);
				if (t != null)
				{
					target = t.gameObject.GetComponent(typeName);
				}
			}
			if (target == null)
			{
				// search whole scene for first component of this type
				foreach (UnityEngine.Object o in UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None))
				{
					if (o != null && o.GetType().FullName == typeName)
					{
						target = o;
						break;
					}
				}
			}
			if (target == null)
			{
				throw new ArgumentException("component not found: " + typeName + (string.IsNullOrEmpty(path) ? "" : " at " + path));
			}

			MonoBehaviour mb = (MonoBehaviour)target;
			MethodInfo method = mb.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new ArgumentException("method not found: " + methodName);
			}

			// bind args to parameter types
			ParameterInfo[] pars = method.GetParameters();
			object[] callArgs = new object[pars.Length];
			for (int i = 0; i < pars.Length; i++)
			{
				if (rawArgs != null && i < rawArgs.Count)
				{
					callArgs[i] = ConvertValue(rawArgs[i], pars[i].ParameterType);
				}
				else if (pars[i].HasDefaultValue)
				{
					callArgs[i] = pars[i].DefaultValue;
				}
				else
				{
					callArgs[i] = pars[i].ParameterType.IsValueType ? Activator.CreateInstance(pars[i].ParameterType) : null;
				}
			}

			object result = method.Invoke(mb, callArgs);
			return new Dictionary<string, object>
			{
				{ "target", mb.name },
				{ "method", methodName },
				{ "result", result != null ? result.ToString() : "null" }
			};
		}

		private static object SetComponentValue(Dictionary<string, object> payload)
		{
			Transform t = RequireTransform(payload);
			string typeName = GetString(payload, "componentType");
			string member = GetString(payload, "member");
			if (!payload.TryGetValue("value", out object value))
			{
				throw new ArgumentException("value is required");
			}

			Component c = t.gameObject.GetComponent(typeName);
			if (c == null)
			{
				throw new ArgumentException("component not found: " + typeName);
			}

			const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
			FieldInfo field = c.GetType().GetField(member, flags);
			if (field != null)
			{
				Undo.RecordObject(c, "MCP Set Field");
				field.SetValue(c, ConvertValue(value, field.FieldType));
				EditorUtility.SetDirty(c);
				return "field set";
			}
			PropertyInfo property = c.GetType().GetProperty(member, flags);
			if (property != null && property.CanWrite)
			{
				Undo.RecordObject(c, "MCP Set Property");
				property.SetValue(c, ConvertValue(value, property.PropertyType));
				EditorUtility.SetDirty(c);
				return "property set";
			}
			throw new ArgumentException("member not found: " + member);
		}

		private static object ConvertValue(object value, Type targetType)
		{
			if (value == null)
			{
				return null;
			}
			if (targetType.IsInstanceOfType(value))
			{
				return value;
			}
			if (targetType == typeof(Vector3) && value is Dictionary<string, object> vec)
			{
				return new Vector3(
					Convert.ToSingle(vec.TryGetValue("x", out object x) ? x : 0f),
					Convert.ToSingle(vec.TryGetValue("y", out object y) ? y : 0f),
					Convert.ToSingle(vec.TryGetValue("z", out object z) ? z : 0f));
			}
			if (targetType == typeof(Color) && value is Dictionary<string, object> col)
			{
				return new Color(
					Convert.ToSingle(col.TryGetValue("x", out object r) ? r : (col.TryGetValue("r", out object rr) ? rr : 0f)),
					Convert.ToSingle(col.TryGetValue("y", out object g) ? g : (col.TryGetValue("g", out object gg) ? gg : 0f)),
					Convert.ToSingle(col.TryGetValue("z", out object b) ? b : (col.TryGetValue("b", out object bb) ? bb : 0f)),
					Convert.ToSingle(col.TryGetValue("w", out object a) ? a : (col.TryGetValue("a", out object aa) ? aa : 1f)));
			}
			if (targetType.IsEnum)
			{
				if (value is string enumText)
				{
					try
					{
						return Enum.Parse(targetType, enumText, true);
					}
					catch
					{
						// fall through to numeric parse
					}
				}
				return Enum.ToObject(targetType, Convert.ToInt32(value));
			}
			if (targetType == typeof(bool) && value is string boolText)
			{
				return boolText == "true" || boolText == "True" || boolText == "1";
			}
			return Convert.ChangeType(value, targetType);
		}

		private static Transform RequireTransform(Dictionary<string, object> payload)
		{
			string path = GetString(payload, "path");
			Transform t = FindByPath(path);
			if (t == null)
			{
				throw new ArgumentException("not found: " + path);
			}
			return t;
		}

		private static Type ResolveType(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				return null;
			}
			Type type = Type.GetType(typeName);
			if (type != null)
			{
				return type;
			}
			foreach (System.Reflection.Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				type = assembly.GetType(typeName);
				if (type != null)
				{
					return type;
				}
				try
				{
					foreach (Type candidate in assembly.GetTypes())
					{
						if (candidate.Name == typeName)
						{
							return candidate;
						}
					}
				}
				catch (ReflectionTypeLoadException)
				{
				}
			}
			return null;
		}

		private static object GetConsoleLogs(Dictionary<string, object> payload)
		{
			int count = GetInt(payload, "count", 50);
			string minLevel = GetString(payload, "level");
			List<string> snapshot;
			lock (s_LogLock)
			{
				snapshot = new List<string>(s_LogBuffer);
			}
			if (!string.IsNullOrEmpty(minLevel))
			{
				List<string> filtered = new List<string>();
				foreach (string entry in snapshot)
				{
					if (entry.StartsWith(minLevel + "|", StringComparison.OrdinalIgnoreCase))
					{
						filtered.Add(entry);
					}
				}
				snapshot = filtered;
			}
			if (snapshot.Count > count)
			{
				snapshot = snapshot.GetRange(snapshot.Count - count, count);
			}
			return new Dictionary<string, object> { { "logs", snapshot } };
		}

		private static object ListAssets(Dictionary<string, object> payload)
		{
			string filter = GetString(payload, "filter", "t:Object");
			int limit = GetInt(payload, "limit", 100);
			string[] guids = AssetDatabase.FindAssets(filter);
			List<object> results = new List<object>();
			foreach (string guid in guids)
			{
				if (results.Count >= limit)
				{
					break;
				}
				string path = AssetDatabase.GUIDToAssetPath(guid);
				results.Add(path);
			}
			return new Dictionary<string, object>
			{
				{ "total", guids.Length },
				{ "returned", results.Count },
				{ "assets", results }
			};
		}

		private static List<string> ReadCompilerErrorsFromEditorLog()
		{
			List<string> errors = new List<string>();
			try
			{
				string logPath = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
					"Unity", "Editor", "Editor.log");
				if (!File.Exists(logPath))
				{
					return errors;
				}
				string[] lines = File.ReadAllLines(logPath);
				for (int i = lines.Length - 1; i >= 0 && errors.Count < 6; i--)
				{
					if (lines[i].Contains("error CS") && lines[i].Contains("McpEval"))
					{
						errors.Add(lines[i].Trim());
					}
				}
			}
			catch
			{
			}
			return errors;
		}

		private static string[] CollectUnityModuleReferences()
		{
			// The AssemblyBuilder defaults reference only the UnityEditor.dll/UnityEngine.dll
			// facades. Compiler needs the concrete module DLLs so forwarded types resolve.
			// Skip the bare facades (UnityEngine.dll / UnityEditor.dll); they are already defaults.
			List<string> refs = new List<string>();
			string unityEngineDir = Path.Combine(EditorApplication.applicationContentsPath, "Managed", "UnityEngine");
			if (Directory.Exists(unityEngineDir))
			{
				foreach (string dll in Directory.GetFiles(unityEngineDir, "*.dll", SearchOption.TopDirectoryOnly))
				{
					string name = Path.GetFileName(dll);
					if (name.StartsWith("UnityEngine.", StringComparison.Ordinal) || name.StartsWith("UnityEditor.", StringComparison.Ordinal))
					{
						refs.Add(dll);
					}
				}
			}
			return refs.ToArray();
		}

		private static T EnqueueMain<T>(Func<T> fn)
		{
			T result = default;
			Exception error = null;
			using (ManualResetEvent done = new ManualResetEvent(false))
			{
				lock (s_QueueLock)
				{
					s_MainThreadQueue.Enqueue(() =>
					{
						try
						{
							result = fn();
						}
						catch (Exception ex)
						{
							error = ex is TargetInvocationException tie && tie.InnerException != null ? tie.InnerException : ex;
						}
						finally
						{
							done.Set();
						}
					});
				}
				if (!done.WaitOne(TimeSpan.FromSeconds(120)))
				{
					throw new TimeoutException("command timed out on Unity main thread");
				}
			}
			if (error != null)
			{
				throw error;
			}
			return result;
		}

		private static object ExecuteCSharp(string code)
		{
			const string scriptPath = "Assets/Editor/__McpEval/McpEval.cs";
			const string dllPath = "Library/__McpEval.dll";
			string absoluteDll = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Library", "__McpEval.dll");
			string absoluteScript = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Assets", "Editor", "__McpEval", "McpEval.cs");

			try
			{
				AssemblyBuilder builder = EnqueueMain(() =>
				{
					Directory.CreateDirectory(Path.GetDirectoryName(absoluteScript)!);
					if (File.Exists(absoluteDll))
					{
						File.Delete(absoluteDll);
					}
					string source =
						"using System;\n" +
						"using System.Collections.Generic;\n" +
						"using System.Linq;\n" +
						"using UnityEngine;\n" +
						"using UnityEngine.SceneManagement;\n" +
						"using UnityEditor;\n" +
						"using UnityEditor.SceneManagement;\n" +
						"public static class McpEval\n" +
						"{\n" +
						"	public static object Run()\n" +
						"	{\n" +
						"		" + code.Replace("\n", "\n		") + "\n" +
						"	}\n" +
						"}\n";
					File.WriteAllText(absoluteScript, source);
					AssetDatabase.ImportAsset(scriptPath, ImportAssetOptions.ForceUpdate);

					AssemblyBuilder b = new AssemblyBuilder(dllPath, new[] { scriptPath });
					b.flags = AssemblyBuilderFlags.EditorAssembly;
					b.additionalReferences = CollectUnityModuleReferences();
					if (!b.Build())
					{
						throw new InvalidOperationException("AssemblyBuilder failed to start");
					}
					return b;
				});

				double deadline = DateTime.UtcNow.AddSeconds(60).Ticks;
				while (!EnqueueMain(() => builder.status == AssemblyBuilderStatus.Finished))
				{
					if (DateTime.UtcNow.Ticks > deadline)
					{
						throw new TimeoutException("eval compilation timed out");
					}
					Thread.Sleep(50);
				}

				return EnqueueMain<object>(() =>
				{
					if (!File.Exists(absoluteDll))
					{
						List<string> recentErrors = new List<string>();
						lock (s_LogLock)
						{
							for (int i = s_LogBuffer.Count - 1; i >= 0 && recentErrors.Count < 8; i--)
							{
								string entry = s_LogBuffer[i];
								if (entry.Contains("error CS") || entry.Contains("McpEval"))
								{
									recentErrors.Add(entry);
								}
							}
						}
						if (recentErrors.Count == 0)
						{
							recentErrors.AddRange(ReadCompilerErrorsFromEditorLog());
						}
						throw new InvalidOperationException(
							"eval compile failed: " + string.Join(" ;; ", recentErrors));
					}
					System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(File.ReadAllBytes(absoluteDll));
					Type type = assembly.GetType("McpEval");
					MethodInfo method = type.GetMethod("Run", BindingFlags.Public | BindingFlags.Static);
					object result = method.Invoke(null, null);
					return new Dictionary<string, object>
					{
						{ "result", result != null ? result.ToString() : "null" },
						{ "type", result != null ? result.GetType().FullName : "null" }
					};
				});
			}
			finally
			{
				// Always remove the temp script so a failed eval never leaves a broken asset.
				EnqueueMain<bool>(() =>
				{
					try
					{
						if (File.Exists(absoluteScript))
						{
							File.Delete(absoluteScript);
						}
						string meta = absoluteScript + ".meta";
						if (File.Exists(meta))
						{
							File.Delete(meta);
						}
						if (File.Exists(absoluteDll))
						{
							File.Delete(absoluteDll);
						}
						AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
					}
					catch
					{
					}
					return true;
				});
			}
		}

		private static Dictionary<string, object> Vec(Vector3 v)
		{
			return new Dictionary<string, object> { { "x", v.x }, { "y", v.y }, { "z", v.z } };
		}

		private static string GetString(Dictionary<string, object> payload, string key, string fallback = null)
		{
			return payload.TryGetValue(key, out object value) && value != null ? value.ToString() : fallback;
		}

		private static int GetInt(Dictionary<string, object> payload, string key, int fallback)
		{
			return payload.TryGetValue(key, out object value) && value != null ? Convert.ToInt32(value) : fallback;
		}

		private static bool GetBool(Dictionary<string, object> payload, string key, bool fallback)
		{
			return payload.TryGetValue(key, out object value) && value != null
				? Convert.ToBoolean(value)
				: fallback;
		}

		private static Vector3 GetVector3(Dictionary<string, object> payload, string key, Vector3 fallback)
		{
			if (!payload.TryGetValue(key, out object value) || !(value is Dictionary<string, object> vec))
			{
				return fallback;
			}
			return new Vector3(
				Convert.ToSingle(vec.TryGetValue("x", out object x) ? x : 0f),
				Convert.ToSingle(vec.TryGetValue("y", out object y) ? y : 0f),
				Convert.ToSingle(vec.TryGetValue("z", out object z) ? z : 0f));
		}

		private static string MakeResponse(object id, bool ok, object result, string error)
		{
			StringBuilder sb = new StringBuilder(256);
			sb.Append("{\"id\":").Append(MiniJson.Serialize(id)).Append(',');
			sb.Append("\"ok\":").Append(ok ? "true" : "false").Append(',');
			sb.Append("\"result\":").Append(MiniJson.Serialize(result)).Append(',');
			sb.Append("\"error\":").Append(MiniJson.Serialize(error)).Append('}');
			return sb.ToString();
		}
	}

	internal static class MiniJson
	{
		public static object Parse(string json)
		{
			int index = 0;
			object value = ParseValue(json, ref index);
			return value;
		}

		public static string Serialize(object value)
		{
			StringBuilder sb = new StringBuilder();
			Write(sb, value);
			return sb.ToString();
		}

		private static object ParseValue(string json, ref int index)
		{
			SkipWhitespace(json, ref index);
			if (index >= json.Length)
			{
				throw new FormatException("unexpected end of JSON");
			}
			char c = json[index];
			switch (c)
			{
				case '{': return ParseObject(json, ref index);
				case '[': return ParseArray(json, ref index);
				case '"': return ParseString(json, ref index);
				case 't':
					Expect(json, ref index, "true");
					return true;
				case 'f':
					Expect(json, ref index, "false");
					return false;
				case 'n':
					Expect(json, ref index, "null");
					return null;
				default: return ParseNumber(json, ref index);
			}
		}

		private static Dictionary<string, object> ParseObject(string json, ref int index)
		{
			Dictionary<string, object> dict = new Dictionary<string, object>();
			index++;
			SkipWhitespace(json, ref index);
			if (json[index] == '}')
			{
				index++;
				return dict;
			}
			while (true)
			{
				SkipWhitespace(json, ref index);
				string key = ParseString(json, ref index);
				SkipWhitespace(json, ref index);
				if (json[index] != ':')
				{
					throw new FormatException("expected ':' at " + index);
				}
				index++;
				dict[key] = ParseValue(json, ref index);
				SkipWhitespace(json, ref index);
				if (json[index] == ',')
				{
					index++;
					continue;
				}
				if (json[index] == '}')
				{
					index++;
					return dict;
				}
				throw new FormatException("expected ',' or '}' at " + index);
			}
		}

		private static List<object> ParseArray(string json, ref int index)
		{
			List<object> list = new List<object>();
			index++;
			SkipWhitespace(json, ref index);
			if (json[index] == ']')
			{
				index++;
				return list;
			}
			while (true)
			{
				list.Add(ParseValue(json, ref index));
				SkipWhitespace(json, ref index);
				if (json[index] == ',')
				{
					index++;
					continue;
				}
				if (json[index] == ']')
				{
					index++;
					return list;
				}
				throw new FormatException("expected ',' or ']' at " + index);
			}
		}

		private static string ParseString(string json, ref int index)
		{
			if (json[index] != '"')
			{
				throw new FormatException("expected '\"' at " + index);
			}
			index++;
			StringBuilder sb = new StringBuilder();
			while (true)
			{
				char c = json[index++];
				if (c == '"')
				{
					return sb.ToString();
				}
				if (c == '\\')
				{
					char escape = json[index++];
					switch (escape)
					{
						case '"': sb.Append('"'); break;
						case '\\': sb.Append('\\'); break;
						case '/': sb.Append('/'); break;
						case 'b': sb.Append('\b'); break;
						case 'f': sb.Append('\f'); break;
						case 'n': sb.Append('\n'); break;
						case 'r': sb.Append('\r'); break;
						case 't': sb.Append('\t'); break;
						case 'u':
							string hex = json.Substring(index, 4);
							index += 4;
							sb.Append((char)Convert.ToInt32(hex, 16));
							break;
						default:
							throw new FormatException("bad escape \\" + escape);
					}
				}
				else
				{
					sb.Append(c);
				}
			}
		}

		private static object ParseNumber(string json, ref int index)
		{
			int start = index;
			while (index < json.Length && (char.IsDigit(json[index]) || json[index] == '-' || json[index] == '+' || json[index] == '.' || json[index] == 'e' || json[index] == 'E'))
			{
				index++;
			}
			string text = json.Substring(start, index - start);
			if (text.Contains('.') || text.Contains('e') || text.Contains('E'))
			{
				return double.Parse(text, System.Globalization.CultureInfo.InvariantCulture);
			}
			return long.Parse(text, System.Globalization.CultureInfo.InvariantCulture);
		}

		private static void SkipWhitespace(string json, ref int index)
		{
			while (index < json.Length && char.IsWhiteSpace(json[index]))
			{
				index++;
			}
		}

		private static void Expect(string json, ref int index, string literal)
		{
			if (index + literal.Length > json.Length || json.Substring(index, literal.Length) != literal)
			{
				throw new FormatException("expected '" + literal + "' at " + index);
			}
			index += literal.Length;
		}

		private static void Write(StringBuilder sb, object value)
		{
			if (value == null)
			{
				sb.Append("null");
			}
			else if (value is bool b)
			{
				sb.Append(b ? "true" : "false");
			}
			else if (value is string s)
			{
				WriteString(sb, s);
			}
			else if (value is IDictionary<string, object> dict)
			{
				sb.Append('{');
				bool first = true;
				foreach (KeyValuePair<string, object> pair in dict)
				{
					if (!first)
					{
						sb.Append(',');
					}
					first = false;
					WriteString(sb, pair.Key);
					sb.Append(':');
					Write(sb, pair.Value);
				}
				sb.Append('}');
			}
			else if (value is IEnumerable enumerable && !(value is string))
			{
				sb.Append('[');
				bool first = true;
				foreach (object item in enumerable)
				{
					if (!first)
					{
						sb.Append(',');
					}
					first = false;
					Write(sb, item);
				}
				sb.Append(']');
			}
			else if (value is float f)
			{
				sb.Append(f.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
			}
			else if (value is double d)
			{
				sb.Append(d.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
			}
			else
			{
				sb.Append(Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture));
			}
		}

		private static void WriteString(StringBuilder sb, string s)
		{
			sb.Append('"');
			foreach (char c in s)
			{
				switch (c)
				{
					case '"': sb.Append("\\\""); break;
					case '\\': sb.Append("\\\\"); break;
					case '\n': sb.Append("\\n"); break;
					case '\r': sb.Append("\\r"); break;
					case '\t': sb.Append("\\t"); break;
					default:
						if (c < ' ')
						{
							sb.Append("\\u").Append(((int)c).ToString("x4"));
						}
						else
						{
							sb.Append(c);
						}
						break;
				}
			}
			sb.Append('"');
		}
	}
}








