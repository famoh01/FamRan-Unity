using System;
using Models.Network.Shared.Enums;
using Models.Network.Shared.Interface;
using UnityEngine;

namespace TrinityRift.Network
{
	public class S_ZoneIdentifyResponse : IMessage
	{
		public byte Status;

		public ushort GaeaId;

		public int MapId;

		public uint GateId;

		public Vector3 Position;

		public byte[] BroadcastKey;

		public bool IsSuccess => false;

		public int MapMainId => 0;

		public EventTypesEnum Type => EventTypesEnum.None;

		public static IMessage ReadFrom(ReadOnlySpan<byte> data)
		{
			return null;
		}

		public void Reset()
		{
		}
	}
}
