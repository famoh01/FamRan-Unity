using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiSceneHelper : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CAddSceneRoutine_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public bool _firstLoad;

		public MultiSceneHelper _003C_003E4__this;

		public bool _isLoader;

		public string _scene;

		private LoaderScene _003Ctarget_003E5__2;

		private AsyncOperation _003C_sceneAsync_003E5__3;

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
		public _003CAddSceneRoutine_003Ed__26(int _003C_003E1__state)
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
	private sealed class _003CAddSceneRoutineNotAdditive_003Ed__27 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public bool _firstLoad;

		public MultiSceneHelper _003C_003E4__this;

		public bool _isLoader;

		public int _scene;

		private LoaderScene _003Ctarget_003E5__2;

		private AsyncOperation _003C_sceneAsync_003E5__3;

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
		public _003CAddSceneRoutineNotAdditive_003Ed__27(int _003C_003E1__state)
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
	private sealed class _003CAddSceneRoutineNotAdditiveAsset_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public bool _firstLoad;

		public MultiSceneHelper _003C_003E4__this;

		public bool _isLoader;

		public string _scene;

		private LoaderScene _003Ctarget_003E5__2;

		private AsyncOperation _003C_sceneAsync_003E5__3;

		private PlayerHandler _003Cph_003E5__4;

		private float _003C_spawnWait_003E5__5;

		private float _003C_drainHold_003E5__6;

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
		public _003CAddSceneRoutineNotAdditiveAsset_003Ed__28(int _003C_003E1__state)
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
	private sealed class _003CMultipleSceneLoading_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MultiSceneHelper _003C_003E4__this;

		public string[] _unload;

		public string[] _add;

		public Action _callback;

		private List<AsyncOperation> _003C_asynList_003E5__2;

		private int _003Ci_003E5__3;

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
		public _003CMultipleSceneLoading_003Ed__29(int _003C_003E1__state)
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
	private sealed class _003CPhotonLoadLevelRoutine_003Ed__25 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MultiSceneHelper _003C_003E4__this;

		public float _progres;

		public Action _act;

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
		public _003CPhotonLoadLevelRoutine_003Ed__25(int _003C_003E1__state)
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

	private const float SpawnDrainHoldCap = 1f;

	[SerializeField]
	private LoaderScene _loader;

	[SerializeField]
	private LoaderScene _firstLoader;

	[SerializeField]
	private Camera _entryCamera;

	private Scene _loaderScene;

	public static MultiSceneHelper Instance { get; private set; }

	public LoaderScene Loader => null;

	public LoaderScene FirstLoader => null;

	public static bool IsLoaderVisible => false;

	public void HideCamera()
	{
	}

	public void ShowCamera()
	{
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void SingleSceneLoad(string _name, LOADTYPE _loadType, bool _isLoader, bool _firstLoad = false)
	{
	}

	public void NormalSingleSceneLoad(int _name, LOADTYPE _loadType, bool _isLoader, bool _firstLoad = false)
	{
	}

	public void NormalSingleSceneLoadAsset(string _name, LOADTYPE _loadType, bool _isLoader, bool _firstLoad = false)
	{
	}

	public void LoadMultipleScene(string[] _add, string[] _unload, [CanBeNull] Action _callback)
	{
	}

	public void PhotonLoadLevelScene(float _progres, [CanBeNull] Action _act)
	{
	}

	public void ReinitializeAllScene()
	{
	}

	[IteratorStateMachine(typeof(_003CPhotonLoadLevelRoutine_003Ed__25))]
	private IEnumerator PhotonLoadLevelRoutine(float _progres, [CanBeNull] Action _act)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CAddSceneRoutine_003Ed__26))]
	private IEnumerator AddSceneRoutine(string _scene, bool _isLoader, bool _firstLoad = false)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CAddSceneRoutineNotAdditive_003Ed__27))]
	private IEnumerator AddSceneRoutineNotAdditive(int _scene, bool _isLoader, bool _firstLoad = false)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CAddSceneRoutineNotAdditiveAsset_003Ed__28))]
	private IEnumerator AddSceneRoutineNotAdditiveAsset(string _scene, bool _isLoader, bool _firstLoad = false)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CMultipleSceneLoading_003Ed__29))]
	private IEnumerator MultipleSceneLoading([CanBeNull] string[] _add, [CanBeNull] string[] _unload, [CanBeNull] Action _callback)
	{
		return null;
	}
}
