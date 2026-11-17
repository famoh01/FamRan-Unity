using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts
{
	public class GameActor
	{
		private const float RAN_TO_WORLD = 0.1125f;

		public float BodyRadiusGame;

		public float BodyRadiusWorld;

		public float MaxSpeed;

		public bool FlatXZ;

		public float StopDistance;

		public float CornerTolerance;

		public float LookAhead;

		public float MaxTurnSpeed;

		private readonly CornerPath _path;

		private int _cornerIndex;

		private bool _pathActive;

		private bool _hasNavCorrection;

		private float _corrX;

		private float _corrY;

		private float _corrZ;

		private bool _hasDirectTarget;

		private Vector3 _directTargetLocal;

		private Vector3 _lastPosLocal;

		private float _stuckTimer;

		public float StuckCheckInterval;

		public float StuckMinMoveDistance;

		public bool EnableStuckDetection;

		[CompilerGenerated]
		private Action m_Arrived;

		[CompilerGenerated]
		private Action<int> m_CornerPassed;

		[CompilerGenerated]
		private Action m_Stuck;

		private float _fCorrectionTimer;

		public Vector3 PositionLocal { get; private set; }

		public Vector3 VelocityLocal { get; private set; }

		public Vector3 FacingLocal { get; private set; }

		public bool HasPath => false;

		public int CornerIndex => 0;

		public int CornerCount => 0;

		public bool IsBusy => false;

		public event Action Arrived
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public event Action<int> CornerPassed
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public event Action Stuck
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public GameActor(int initialPathCapacity = 64)
		{
		}

		public void Reset()
		{
		}

		public void SetBodyRadiusGame(float ranRadius)
		{
		}

		public void SetPositionLocal(Vector3 pos)
		{
		}

		public void SetCornerIndex(int index)
		{
		}

		public Vector3 GetTargetPositionLocal()
		{
			return default;
		}

		public Vector3 GetCurrentMoveTarget()
		{
			return default;
		}

		public void SetPathLocal(Vector3[] localCorners, int count, bool includeCurrentPosAsFirstCorner = true)
		{
		}

		public void ClearPath()
		{
		}

		public void MoveToLocal(Vector3 targetLocal)
		{
		}

		public void Stop()
		{
		}

		public void SetMaxSpeed(float speed)
		{
		}

		public void SyncPositionY(float y)
		{
		}

		public void ApplyNavCorrection(Vector3 correctedLocal)
		{
		}

		public bool Update(long tick, float dt)
		{
			return false;
		}

		protected virtual float GetCurrentSpeed()
		{
			return 0f;
		}

		protected virtual void OnMoved(float dt)
		{
		}

		protected virtual void OnIdleTick(float dt)
		{
		}

		protected virtual void OnArrived()
		{
		}

		protected virtual void OnStop()
		{
		}

		private bool TryGetCurrentTargetLocal(out Vector3 targetLocal)
		{
			targetLocal = default;
			return false;
		}

		private void SkipNearCorners()
		{
		}

		private void AdvanceCornersIfNeeded()
		{
		}

		private bool IsAtTarget(Vector3 pos, Vector3 target)
		{
			return false;
		}

		private static bool IsAtTarget(Vector3 pos, Vector3 target, float tol)
		{
			return false;
		}

		private static float DistanceSqXZ(Vector3 a, Vector3 b)
		{
			return 0f;
		}

		private void UpdateFacingFromDelta(Vector3 delta)
		{
		}

		private void TickStuckDetection(float dt)
		{
		}

		private static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
		{
			return default;
		}
	}
}
