using System;
using UnityEngine;

namespace RAN.Mobile.GameModels
{
	[Serializable]
	public struct PartyMemberInfo
	{
		[SerializeField]
		private bool _isLeader;

		[SerializeField]
		private string _name;

		[SerializeField]
		private int _playerId;

		[SerializeField]
		private ushort _hp;

		[SerializeField]
		private ushort _maxHP;

		[SerializeField]
		private float _currentPositionX;

		[SerializeField]
		private float _currentPositionZ;

		[SerializeField]
		private int _mapId;

		[SerializeField]
		private string _mapName;

		public bool IsLeader
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public string Name
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public int PlayerId
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public ushort HP
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public ushort MaxHP
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public float CurrentPositionX
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float CurrentPositionZ
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public int MapId
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public string MapName
		{
			get
			{
				return null;
			}
			set
			{
			}
		}
	}
}
