using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMinimap : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CSlideContainerRoutine_003Ed__50 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UIMinimap _003C_003E4__this;

		public float duration;

		public float targetY;

		private float _003CstartY_003E5__2;

		private float _003Celapsed_003E5__3;

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
		public _003CSlideContainerRoutine_003Ed__50(int _003C_003E1__state)
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
	private sealed class _003CWaitUntilArrived_003Ed__63 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UIMinimap _003C_003E4__this;

		private PlayerController _003Cpc_003E5__2;

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
		public _003CWaitUntilArrived_003Ed__63(int _003C_003E1__state)
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

	public static UIMinimap Instance;

	public Canvas parentGameMap;

	[Space]
	[Header("MiniMap Indicators")]
	public Image miniMapImage;

	public RectTransform playerInMiniMap;

	public GameObject destMarkInMiniMap;

	[Header("Big Map Indicators")]
	public Image bigMapImage;

	public GameObject destMarkInBigMap;

	public RectTransform playerInBigMap;

	[Space]
	public RectTransform mapBound;

	public Vector2 multiply;

	public Vector2 division;

	public Vector2 addPos;

	public float pMap;

	public Vector2 pMapRotation;

	private GameObject player;

	private Transform camPivot;

	private MinimapSettingObject setting;

	private bool isDragging;

	private Vector2 dragStartPosition;

	public Button toggleFocusButton;

	private bool isFocusOnPlayerEnabled;

	[Header("MiniMap MiniZoom")]
	private int pressCount;

	private float[] scales;

	[Header("MiniMap Toggle")]
	public Canvas miniMapCanvas;

	public Button toggleMinimapButton;

	private bool isMinimapEnabled;

	public const string HideMinimapByDefaultPrefKey = "HideMinimapDefault";

	private bool loginDefaultApplied;

	[Tooltip("Drag your SlidingUI Layout Group RectTransform here")]
	public RectTransform slidingUIContainer;

	[Tooltip("How far (in units) the container slides up when the minimap is hidden")]
	public float containerSlideOffset;

	private float slidingUIBaseY;

	public Sprite[] partyIcon;

	private float mapOpenTime;

	private string _activeSceneName;

	private PlayerController _cachedPlayerController;

	private RectTransform _destMarkBigRect;

	private RectTransform _miniMapViewport;

	private RectTransform _bigMapViewport;

	private readonly Vector3[] _viewportWorldCorners;

	private Coroutine slideCoroutine;

	private const float DeepProbeUp = 2000f;

	private const float DeepSampleRadius = 5000f;

	private const float DeepSampleMaxXZ = 80f;

	public static bool HideMinimapByDefault => false;

	public void OpenGameMap()
	{
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
	}

	private static string GetHierarchyPath(Transform t)
	{
		return null;
	}

	private void ToggleMinimapCanvas()
	{
	}

	private void AnimateSlidingContainer(float targetY)
	{
	}

	[IteratorStateMachine(typeof(_003CSlideContainerRoutine_003Ed__50))]
	private IEnumerator SlideContainerRoutine(float targetY, float duration)
	{
		return null;
	}

	public void ApplyLoginDefaultVisibility()
	{
	}

	private void SnapSlidingContainer()
	{
	}

	public void SyncMinimapState()
	{
	}

	public MinimapSettingObject GetMinimap()
	{
		return null;
	}

	public void InitializeMinimap()
	{
	}

	private void Update()
	{
	}

	private void HandleMapClick(Vector2 clickPosition)
	{
	}

	public bool MoveToWorldPosition(Vector3 worldPosition)
	{
		return false;
	}

	private bool TravelTo(Vector3 worldPosition, bool bigMarkerAtScreenPoint, Vector2 screenPoint)
	{
		return false;
	}

	[IteratorStateMachine(typeof(_003CWaitUntilArrived_003Ed__63))]
	private IEnumerator WaitUntilArrived(Vector3 destination)
	{
		return null;
	}

	public Vector3 GetScaledPosition(Vector3 pos)
	{
		return default;
	}

	private void LateUpdate()
	{
	}

	public void ToggleMinimapScale()
	{
	}

	private void SetMinimapScale(float scale)
	{
	}

	private static RectTransform FindClippingViewport(RectTransform mapImage)
	{
		return null;
	}

	private void FocusOnPlayer(RectTransform mapImage, RectTransform viewport, Vector3 pos)
	{
	}

	private static float ClampMapAxis(float desiredPosition, float viewportMin, float viewportMax, float scaledMapEdgeA, float scaledMapEdgeB)
	{
		return 0f;
	}

	public void ToggleFocusOnPlayer()
	{
	}
}
