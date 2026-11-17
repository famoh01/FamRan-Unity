using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace XftWeapon
{
	public class XWeaponTrail : MonoBehaviour
	{
		public class Element
		{
			public Vector3 PointStart;

			public Vector3 PointEnd;

			public Vector3 Pos => default;

			public Element(Vector3 start, Vector3 end)
			{
			}

			public Element()
			{
			}
		}

		public class ElementPool
		{
			private readonly Stack<Element> _stack;

			public int CountAll { get; private set; }

			public int CountActive => 0;

			public int CountInactive => 0;

			public ElementPool(int preCount)
			{
			}

			public Element Get()
			{
				return null;
			}

			public void Release(Element element)
			{
			}
		}

		public static string Version;

		public bool UseWith2D;

		public bool UseWithSRP;

		public string SortingLayerName;

		public int SortingOrder;

		public Transform PointStart;

		public Transform PointEnd;

		public int MaxFrame;

		public int Granularity;

		public float Fps;

		public Color MyColor;

		public Material MyMaterial;

		protected float mTrailWidth;

		protected Element mHeadElem;

		protected List<Element> mSnapshotList;

		protected ElementPool mElemPool;

		protected Spline mSpline;

		protected float mFadeT;

		protected bool mIsFading;

		protected float mFadeTime;

		protected float mElapsedTime;

		protected float mFadeElapsedime;

		protected GameObject mMeshObj;

		protected VertexPool mVertexPool;

		protected VertexPool.VertexSegment mVertexSegment;

		protected bool mInited;

		protected bool mActivated;

		protected Camera _myCamera;

		public float UpdateInterval => 0f;

		public Vector3 CurHeadPos => default;

		public float TrailWidth => 0f;

		public Camera MyCamera => null;

		protected Camera FindMyCamera()
		{
			return null;
		}

		public void Init()
		{
		}

		public void Activate()
		{
		}

		public void Deactivate()
		{
		}

		public void StopSmoothly(float fadeTime)
		{
		}

		private void Start()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void OnBeginCameraRendering(ScriptableRenderContext context, Camera cam)
		{
		}

		private void OnEndCameraRendering(ScriptableRenderContext context, Camera cam)
		{
		}

		private void LateUpdate()
		{
		}

		public void MyPreRender(Camera cam)
		{
		}

		public void MyPostRender(Camera cam)
		{
		}

		private void OnDestroy()
		{
		}

		private void OnDrawGizmos()
		{
		}

		private void InitSpline()
		{
		}

		private void RefreshSpline()
		{
		}

		private void UpdateVertex()
		{
		}

		private void UpdateIndices()
		{
		}

		private void UpdateHeadElem()
		{
		}

		private void UpdateFade()
		{
		}

		private void RecordCurElem()
		{
		}

		private void InitOriginalElements()
		{
		}

		private void InitMeshObj()
		{
		}
	}
}
