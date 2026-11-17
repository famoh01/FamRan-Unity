using System;
using System.Collections.Concurrent;
using Models.Network.Shared.Interface;
using UnityEngine;

public class ThreadManager : MonoBehaviour
{
	public sealed class DispatchGate
	{
		public bool Open;
	}

	private struct PacketJob
	{
		public ushort Opcode;

		public IMessage Msg;

		public Action<ushort, IMessage> Handler;

		public DispatchGate Gate;
	}

	private static readonly ConcurrentQueue<Action> _queue;

	private static readonly ConcurrentQueue<PacketJob> _packets;

	private const int MaxPacketsPerFrame = 512;

	private const int MaxQueueDepth = 8192;

	public static long OverflowDrops;

	private static long _lastLoggedOverflow;

	private static int _overflowLogFrameGate;

	public static ThreadManager instance;

	private void Awake()
	{
	}

	public static void ExecuteOnMainThread(Action action)
	{
	}

	public static void EnqueuePacket(ushort opcode, IMessage msg, Action<ushort, IMessage> handler, DispatchGate gate)
	{
	}

	public static void UpdateMain()
	{
	}
}
