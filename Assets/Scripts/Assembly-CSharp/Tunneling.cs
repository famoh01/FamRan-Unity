using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class Tunneling : MonoBehaviour
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CConnectTunnelAsync_003Ed__2 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public string serverIp;

		public int port;

		private TcpClient _003Cclient_003E5__2;

		private TaskAwaiter _003C_003Eu__1;

		private NetworkStream _003Cstream_003E5__3;

		private byte[] _003Cbuffer_003E5__4;

		private TaskAwaiter<int> _003C_003Eu__2;

		private void MoveNext()
		{
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	public void Start()
	{
	}

	public void Connect()
	{
	}

	[AsyncStateMachine(typeof(_003CConnectTunnelAsync_003Ed__2))]
	private void ConnectTunnelAsync(string serverIp, int port)
	{
	}
}
