using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
	private enum RendererType
	{
		none = 0,
		uiImage = 1,
		renderer = 2,
		sprite = 3
	}

	[CompilerGenerated]
	private sealed class _003CDownloader_003Ed__39 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ImageLoader _003C_003E4__this;

		private UnityWebRequest _003Cwww_003E5__2;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CDownloader_003Ed__39(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CImageTextureLoader_003Ed__42 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ImageLoader _003C_003E4__this;

		public Texture2D texture;

		private Color _003Ccolor_003E5__2;

		private Material _003Cmat_003E5__3;

		private float _003CmaxAlpha_003E5__4;

		private Image _003Cimage_003E5__5;

		private SpriteRenderer _003CspriteRenderer_003E5__6;

		private float _003Ctime_003E5__7;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CImageTextureLoader_003Ed__42(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	private static bool ENABLE_GLOBAL_LOGS;

	private bool enableLog;

	private float fadeTime;

	private bool cached;

	private RendererType rendererType;

	private GameObject targetObj;

	private string url;

	private Texture2D loadingPlaceholder;

	private Texture2D errorPlaceholder;

	private Texture2D loadedTexture;

	private UnityAction onStartAction;

	private UnityAction onDownloadedAction;

	private UnityAction OnLoadedAction;

	private UnityAction onEndAction;

	private UnityAction<int> onDownloadProgressChange;

	private UnityAction<string> onErrorAction;

	private static Dictionary<string, ImageLoader> underProcessImageLoaderes;

	private string uniqueHash;

	private int progress;

	public bool success;

	private static string filePath;

	public static ImageLoader get()
	{
		return null;
	}

	public ImageLoader load(string url)
	{
		return null;
	}

	public ImageLoader setFadeTime(float fadeTime)
	{
		return null;
	}

	public ImageLoader into(Image image)
	{
		return null;
	}

	public ImageLoader into(Renderer renderer)
	{
		return null;
	}

	public ImageLoader into(SpriteRenderer spriteRenderer)
	{
		return null;
	}

	public ImageLoader withStartAction(UnityAction action)
	{
		return null;
	}

	public ImageLoader withDownloadedAction(UnityAction action)
	{
		return null;
	}

	public ImageLoader withDownloadProgressChangedAction(UnityAction<int> action)
	{
		return null;
	}

	public ImageLoader withLoadedAction(UnityAction action)
	{
		return null;
	}

	public ImageLoader withErrorAction(UnityAction<string> action)
	{
		return null;
	}

	public ImageLoader withEndAction(UnityAction action)
	{
		return null;
	}

	public ImageLoader setEnableLog(bool enableLog)
	{
		return null;
	}

	public ImageLoader setLoadingPlaceholder(Texture2D loadingPlaceholder)
	{
		return null;
	}

	public ImageLoader setErrorPlaceholder(Texture2D errorPlaceholder)
	{
		return null;
	}

	public ImageLoader setCached(bool cached)
	{
		return null;
	}

	public void start()
	{
	}

	[IteratorStateMachine(typeof(_003CDownloader_003Ed__39))]
	private IEnumerator Downloader()
	{
		return null;
	}

	private void loadSpriteToImage()
	{
	}

	private void SetLoadingImage()
	{
	}

	[IteratorStateMachine(typeof(_003CImageTextureLoader_003Ed__42))]
	private IEnumerator ImageTextureLoader(Texture2D texture = null)
	{
		return null;
	}

	public static string CreateMD5(string input)
	{
		return null;
	}

	private void error(string message)
	{
	}

	private void finish()
	{
	}

	private void destroyer()
	{
	}

	public static void ClearCache(string url)
	{
	}

	public static void ClearAllCachedFiles()
	{
	}
}
