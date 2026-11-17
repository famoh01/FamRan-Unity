using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using Models.Network.Shared.Crypto;
using Models.Network.Shared.Interface;

namespace TrinityRift.Network
{
	public class GameTcpConnection
	{
		[CompilerGenerated]
		private sealed class _003CConnectAsync_003Ed__24 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public GameTcpConnection _003C_003E4__this;

			public Action<bool> callback;

			public string host;

			public int port;

			private IAsyncResult _003CasyncResult_003E5__2;

			private float _003Ctimeout_003E5__3;

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
			public _003CConnectAsync_003Ed__24(int _003C_003E1__state)
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

		private const int HeaderSize = 2;

		private const int OpcodeSize = 2;

		private const int ReceiveBufferSize = 65536;

		private static readonly byte[] PlayerHandshake;

		private const uint KeyMix = 1511506921u;

		private static readonly byte[] _kp0;

		private static readonly byte[] _kp1;

		private TcpClient _tcpClient;

		private NetworkStream _stream;

		private Thread _receiveThread;

		private bool _running;

		private SessionCrypto _crypto;

		private BroadcastOpener _broadcastOpener;

		private readonly ThreadManager.DispatchGate _gate;

		private readonly byte[] _receiveBuffer;

		private int _bufferStart;

		private int _bufferEnd;

		private readonly byte[] _decodeScratch;

		public Action<ushort, IMessage> OnPacketReceived;

		public Action OnDisconnected;

		public static bool PingProbeEnabled;

		public Action<ushort, IMessage> OnRecvThreadPacket;

		public ConnectionState State { get; private set; }

		[IteratorStateMachine(typeof(_003CConnectAsync_003Ed__24))]
		public IEnumerator ConnectAsync(string host, int port, Action<bool> callback)
		{
			return null;
		}

		public void Send(IOutgoingPacket packet)
		{
		}

		public void Disconnect()
		{
		}

		private static byte[] ReadExact(NetworkStream stream, int size)
		{
			return null;
		}

		private static byte[] AssemblePayloadKey()
		{
			return null;
		}

		private void CompactBuffer()
		{
		}

		private void ReceiveLoop()
		{
		}

		private void ProcessReceivedData()
		{
		}

		private bool DispatchBundle(ReadOnlySpan<byte> inner)
		{
			return false;
		}

		private void DispatchMessage(ushort opcode, ReadOnlySpan<byte> data)
		{
		}

		private void SetBroadcastKey(byte[] key)
		{
		}
	}
}
