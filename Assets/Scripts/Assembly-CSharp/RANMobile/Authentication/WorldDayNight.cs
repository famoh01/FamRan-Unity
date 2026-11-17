using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// P5 fidelity: the original runs a day/night cycle on a GAME clock (Emulator at
	/// game-time 01:06 showed the campus at full night; the calendar label is the ripped
	/// "DateAndTime" TMP). 1 game hour = 60 real seconds (full day = 24 min).
	///
	/// Applies: sun intensity/color, trilight ambient, fog color and skybox exposure
	/// (built-in skybox shaders all expose _Exposure; the sky material is cloned so the
	/// asset is never mutated). Starts at the real local hour so it feels live;
	/// DebugSetHour is a test hook.
	/// </summary>
	public class WorldDayNight : MonoBehaviour
	{
		public static float GameHour = 9f;

		public static int GameDay = 9;

		private static WorldDayNight _instance;

		private Light _sun;

		private Material _sky;

		private bool _hasExposure;

		private Color _dayAmbSky;

		private Color _dayAmbEq;

		private Color _dayAmbGnd;

		private Color _dayFog;

		private Color _daySunColor;

		private float _dayAmbInt;

		private float _daySunInt;

		private float _dayExposure = 1f;

		private float _lastHour = -1f;

		private float _nextLabelAt;

		private TextMesh[] _dateLabels;

		public static WorldDayNight Ensure()
		{
			if (_instance != null)
			{
				return _instance;
			}
			GameObject go = new GameObject("WorldDayNight");
			_instance = go.AddComponent<WorldDayNight>();
			return _instance;
		}

		private void Awake()
		{
			GameHour = System.DateTime.Now.Hour + System.DateTime.Now.Minute / 60f;
			// sun: the ripped scenes carry exactly one directional light
			_sun = Object.FindObjectOfType<Light>();
			_dayAmbSky = RenderSettings.ambientSkyColor;
			_dayAmbEq = RenderSettings.ambientEquatorColor;
			_dayAmbGnd = RenderSettings.ambientGroundColor;
			_dayAmbInt = RenderSettings.ambientIntensity;
			_dayFog = RenderSettings.fogColor;
			if (_sun != null)
			{
				_daySunColor = _sun.color;
				_daySunInt = _sun.intensity;
			}
			if (RenderSettings.skybox != null)
			{
				// clone — runtime edits must never dirty the sky asset
				_sky = new Material(RenderSettings.skybox);
				RenderSettings.skybox = _sky;
				_hasExposure = _sky.HasProperty("_Exposure");
				if (_hasExposure)
				{
					_dayExposure = _sky.GetFloat("_Exposure");
				}
			}
			GatherMapMaterials();
			Debug.Log("[DayNight] up, start hour=" + GameHour.ToString("0.0") +
				" sun=" + (_sun != null ? _sun.name : "NULL") + " exposure=" + _hasExposure +
				" tintMats=" + _dayMatColor.Count);
		}

		// The ripped map is BAKED (day lightmaps ignore sun/ambient drops at runtime),
		// so night must tint the materials themselves. Idempotent design: each shared
		// material is cloned exactly ONCE (NeverSave), the clone becomes the renderer's
		// material, and day colors are keyed by the CLONE so day/night lerps always
		// restore the true baseline (no cascading re-captures).
		private readonly Dictionary<Material, Color> _dayMatColor = new Dictionary<Material, Color>();
		private readonly Dictionary<Material, Material> _matClones = new Dictionary<Material, Material>();
		private readonly HashSet<Material> _ourClones = new HashSet<Material>();

		private static readonly Color NightMul = new Color(0.42f, 0.46f, 0.64f, 1f);

		private void GatherMapMaterials()
		{
			Renderer[] renderers = Object.FindObjectsOfType<Renderer>(true);
			for (int i = 0; i < renderers.Length; i++)
			{
				Renderer r = renderers[i];
				if (!(r is MeshRenderer) && !(r is SkinnedMeshRenderer))
				{
					continue;
				}
				Material[] shared = r.sharedMaterials;
				if (shared == null)
				{
					continue;
				}
				Material[] patched = null;
				for (int s = 0; s < shared.Length; s++)
				{
					Material m = shared[s];
					if (m == null || _ourClones.Contains(m) || !m.HasProperty("_Color"))
					{
						continue;
					}
					Material clone;
					if (!_matClones.TryGetValue(m, out clone))
					{
						clone = new Material(m);
						clone.hideFlags = HideFlags.DontSave;
						_matClones[m] = clone;
						_ourClones.Add(clone);
						_dayMatColor[clone] = m.GetColor("_Color");
					}
					if (patched == null)
					{
						patched = (Material[])shared.Clone();
					}
					patched[s] = clone;
				}
				if (patched != null)
				{
					r.sharedMaterials = patched;
				}
			}
		}

		private void ApplyNightTint(float k)
		{
			foreach (KeyValuePair<Material, Color> kv in _dayMatColor)
			{
				Material clone = kv.Key;
				if (clone == null)
				{
					continue;
				}
				Color day = kv.Value;
				clone.SetColor("_Color", Color.Lerp(day, new Color(
					day.r * NightMul.r, day.g * NightMul.g,
					day.b * NightMul.b, day.a), k));
			}
		}

		/// <summary>0 = full day … 1 = full night, with dusk/dawn ramps.</summary>
		public static float NightFactor(float hour)
		{
			if (hour >= 9f && hour < 17f)
			{
				return 0f;
			}
			if (hour >= 17f && hour < 19.5f)
			{
				return (hour - 17f) / 2.5f;
			}
			if (hour >= 19.5f || hour < 5.5f)
			{
				return 1f;
			}
			return 1f - (hour - 5.5f) / 3.5f; // dawn 5:30 → 9:00
		}

		private void Update()
		{
			GameHour += Time.deltaTime / 60f;
			if (GameHour >= 24f)
			{
				GameHour -= 24f;
				GameDay = (GameDay % 30) + 1;
			}
			if (Mathf.Abs(GameHour - _lastHour) >= 0.05f) // every ~3 s
			{
				_lastHour = GameHour;
				Apply(NightFactor(GameHour));
			}
			if (Time.unscaledTime >= _nextLabelAt)
			{
				_nextLabelAt = Time.unscaledTime + 0.3f;
				UpdateDateLabels();
			}
		}

		private void Apply(float k)
		{
			// incremental + idempotent: catches renderers spawned at any hour
			GatherMapMaterials();
			if (_sun != null)
			{
				// moonlight: dim but present — the original night stays readable
				_sun.intensity = Mathf.Lerp(_daySunInt, 0.30f, k);
				_sun.color = Color.Lerp(_daySunColor, new Color(0.55f, 0.62f, 0.85f), k);
			}
			RenderSettings.ambientIntensity = Mathf.Lerp(_dayAmbInt, 0.85f, k);
			RenderSettings.ambientSkyColor = Color.Lerp(_dayAmbSky, new Color(0.10f, 0.13f, 0.24f), k);
			RenderSettings.ambientEquatorColor = Color.Lerp(_dayAmbEq, new Color(0.08f, 0.09f, 0.17f), k);
			RenderSettings.ambientGroundColor = Color.Lerp(_dayAmbGnd, new Color(0.04f, 0.04f, 0.08f), k);
			RenderSettings.fogColor = Color.Lerp(_dayFog, new Color(0.07f, 0.08f, 0.16f), k);
			if (_sky != null && _hasExposure)
			{
				_sky.SetFloat("_Exposure", Mathf.Lerp(_dayExposure, 0.18f, k));
			}
			// baked lightmaps don't react to sun/ambient — tint the materials instead
			ApplyNightTint(k);
		}

		private void UpdateDateLabels()
		{
			if (_dateLabels == null)
			{
				string[] paths =
				{
					"In-Game UI/_SkillButtons/_LABELS (Bottom Left)/MapDetails/Date&Time",
					"In-Game UI/_SkillButtons/_LABELS (Bottom Left)/DateAndTime",
				};
				var list = new System.Collections.Generic.List<TextMesh>();
				for (int i = 0; i < paths.Length; i++)
				{
					GameObject go = GameObject.Find(paths[i]);
					if (go != null)
					{
						TextMesh tm = go.GetComponent<TextMesh>();
						if (tm == null)
						{
							tm = go.GetComponentInChildren<TextMesh>(true);
						}
						// ripped HUD uses TMP, not legacy TextMesh
						if (tm == null)
						{
							var tmp = go.GetComponent<TMPro.TextMeshProUGUI>();
							if (tmp == null)
							{
								tmp = go.GetComponentInChildren<TMPro.TextMeshProUGUI>(true);
							}
							if (tmp != null)
							{
								_dateLabels = new TextMesh[0];
								_dateLabelTmp = new TMPro.TextMeshProUGUI[] { tmp };
								break;
							}
						}
						if (tm != null)
						{
							list.Add(tm);
						}
					}
				}
				if (_dateLabels == null)
				{
					_dateLabels = list.ToArray();
				}
			}
			int h = (int)GameHour;
			int m = (int)((GameHour - h) * 60f);
			string text = string.Format("09/{0:00} {1:00}:{2:00}", GameDay, h, m);
			if (_dateLabelTmp != null)
			{
				for (int i = 0; i < _dateLabelTmp.Length; i++)
				{
					if (_dateLabelTmp[i] != null)
					{
						_dateLabelTmp[i].text = text;
					}
				}
			}
			if (_dateLabels != null)
			{
				for (int i = 0; i < _dateLabels.Length; i++)
				{
					if (_dateLabels[i] != null)
					{
						_dateLabels[i].text = text;
					}
				}
			}
			RelocateToClock();
		}

		private bool _relocated;

		/// <summary>The rip's Date&Time labels sit in a bottom-left group that renders
		/// off-view; the original shows the game calendar in the top-right stack under
		/// the real clock — move ours there once.</summary>
		private void RelocateToClock()
		{
			if (_relocated)
			{
				return;
			}
			if (_dateLabelTmp == null || _dateLabelTmp.Length == 0 || _dateLabelTmp[0] == null)
			{
				return;
			}
			var all = Object.FindObjectsOfType<TextMeshProUGUI>(true);
			for (int i = 0; i < all.Length; i++)
			{
				string t = all[i].text;
				if (string.IsNullOrEmpty(t) || all[i] == _dateLabelTmp[0])
				{
					continue;
				}
				if (Regex.IsMatch(t, @"^\d{1,2}/\d{1,2}\s+\d{1,2}:\d{2}$"))
				{
					RectTransform clockRt = (RectTransform)all[i].transform;
					RectTransform dateRt = (RectTransform)_dateLabelTmp[0].transform;
					dateRt.SetParent(clockRt.parent, false);
					dateRt.anchorMin = clockRt.anchorMin;
					dateRt.anchorMax = clockRt.anchorMax;
					dateRt.pivot = clockRt.pivot;
					dateRt.sizeDelta = clockRt.sizeDelta;
					dateRt.anchoredPosition = clockRt.anchoredPosition - new Vector2(0f, clockRt.rect.height + 4f);
					dateRt.localScale = clockRt.localScale;
					_relocated = true;
					Debug.Log("[DayNight] game date relocated under clock '" + t + "'");
					return;
				}
			}
		}

		private TMPro.TextMeshProUGUI[] _dateLabelTmp;

		/// <summary>Test hook: jump the game clock (e.g. 1 = deep night).</summary>
		public void DebugSetHour(float hour)
		{
			GameHour = hour;
			_lastHour = -1f;
			Apply(NightFactor(GameHour));
		}

		private void OnDestroy()
		{
			if (_instance == this)
			{
				_instance = null;
			}
		}
	}
}
