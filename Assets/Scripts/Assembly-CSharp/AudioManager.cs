using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Central sound router over the ripped "Sounds" root (mixer groups + ENV/SFX/BGM sources).
/// The original decompile had every body gutted; this restores them so SFX/BGM actually play.
/// The whole "Sounds" root is made persistent so audio survives the Init -> login -> world loads.
/// </summary>
public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public AudioMixerGroup BGM;
	public AudioMixerGroup SFX;
	public AudioMixerGroup ENV;

	public AudioSource SFXSource;
	public AudioSource BGMSource;
	public AudioSource ENVSource;

	public Sound[] music;
	public Sound[] sounds;

	private Dictionary<string, AudioClip> _sfxLookup;
	private Dictionary<string, float> _sfxVolume;
	private Dictionary<string, AudioClip> _bgmLookup;
	private string _currentBgm;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		// Persist the whole Sounds root (mixer groups + the ENV/SFX/BGM sources are siblings
		// of this node) so audio keeps working after scene changes.
		Transform rootT = transform.root;
		Object.DontDestroyOnLoad(rootT != null ? rootT.gameObject : gameObject);
		BuildLookups();
		// Route the sources through their mixer groups if the refs were left unassigned.
		AssignGroup(SFXSource, SFX);
		AssignGroup(BGMSource, BGM);
		AssignGroup(ENVSource, ENV);
		UnityEngine.Debug.Log("[Audio] ready sfx=" + (_sfxLookup != null ? _sfxLookup.Count : 0) +
			" bgm=" + (_bgmLookup != null ? _bgmLookup.Count : 0) +
			" sfxSrc=" + (SFXSource != null));
	}

	private static void AssignGroup(AudioSource src, AudioMixerGroup group)
	{
		if (src != null && group != null && src.outputAudioMixerGroup == null)
		{
			src.outputAudioMixerGroup = group;
		}
	}

	private void BuildLookups()
	{
		_sfxLookup = new Dictionary<string, AudioClip>(System.StringComparer.OrdinalIgnoreCase);
		_sfxVolume = new Dictionary<string, float>(System.StringComparer.OrdinalIgnoreCase);
		_bgmLookup = new Dictionary<string, AudioClip>(System.StringComparer.OrdinalIgnoreCase);

		if (sounds != null)
		{
			for (int i = 0; i < sounds.Length; i++)
			{
				Sound s = sounds[i];
				if (s == null || s.clip == null || string.IsNullOrEmpty(s.name))
				{
					continue;
				}
				if (!_sfxLookup.ContainsKey(s.name))
				{
					_sfxLookup[s.name] = s.clip;
					_sfxVolume[s.name] = s.volume;
				}
			}
		}
		if (music != null)
		{
			for (int i = 0; i < music.Length; i++)
			{
				Sound s = music[i];
				if (s == null || s.clip == null || string.IsNullOrEmpty(s.name))
				{
					continue;
				}
				if (!_bgmLookup.ContainsKey(s.name))
				{
					_bgmLookup[s.name] = s.clip;
				}
			}
		}
	}

	public static void PlaySfx(string sfxName)
	{
		if (instance == null)
		{
			return;
		}
		PlaySfx(sfxName, instance.SFXSource);
	}

	public static void PlaySfx(string sfxName, AudioSource source)
	{
		AudioClip clip;
		if (!TryGetSfxClip(sfxName, out clip) || source == null || clip == null)
		{
			return;
		}
		float vol = 1f;
		if (instance != null && instance._sfxVolume != null)
		{
			float v;
			if (instance._sfxVolume.TryGetValue(sfxName, out v))
			{
				vol = v;
			}
		}
		source.PlayOneShot(clip, vol);
	}

	public static bool TryGetSfxClip(string sfxName, out AudioClip clip)
	{
		clip = null;
		if (instance == null || instance._sfxLookup == null || string.IsNullOrEmpty(sfxName))
		{
			return false;
		}
		return instance._sfxLookup.TryGetValue(sfxName, out clip);
	}

	public static void PlayBgm(string bgmName)
	{
		if (instance == null || instance.BGMSource == null)
		{
			return;
		}
		AudioClip clip;
		if (instance._bgmLookup == null || !instance._bgmLookup.TryGetValue(bgmName, out clip) || clip == null)
		{
			return;
		}
		if (instance._currentBgm == bgmName && instance.BGMSource.isPlaying)
		{
			return; // already looping this track
		}
		instance._currentBgm = bgmName;
		instance.BGMSource.clip = clip;
		instance.BGMSource.loop = true;
		instance.BGMSource.Play();
	}

	public static void RestartBgm()
	{
		if (instance == null || instance.BGMSource == null || instance.BGMSource.clip == null)
		{
			return;
		}
		instance.BGMSource.Play();
	}

	public static void ResetMapAudio()
	{
		// Map-scoped one-shots (weather/ambience) live on the ENV source; stop them on map change.
		if (instance != null && instance.ENVSource != null)
		{
			instance.ENVSource.Stop();
		}
	}

	public static void SetFallbackListener(bool enabled)
	{
		// The original swaps a listener when the audio listener is disabled (cutscenes/pause).
		AudioListener listener = Object.FindFirstObjectByType<AudioListener>();
		if (listener != null)
		{
			listener.enabled = enabled;
		}
	}

	private IEnumerator SwitchBgm(string bgmName)
	{
		if (instance == null || instance.BGMSource == null)
		{
			yield break;
		}
		// Short fade-out then fade-in on the new track.
		float t = 0f;
		float startVol = instance.BGMSource.volume;
		while (t < 0.6f)
		{
			t += Time.unscaledDeltaTime;
			instance.BGMSource.volume = Mathf.Lerp(startVol, 0f, t / 0.6f);
			yield return null;
		}
		PlayBgm(bgmName);
		if (instance.BGMSource.clip != null)
		{
			instance.BGMSource.volume = 1f;
		}
	}
}
