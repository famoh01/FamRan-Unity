namespace TrinityRift.Debugging
{
	public static class PerfMetrics
	{
		private static long _bytesSent;

		private static long _bytesReceived;

		private static long _packetsSent;

		private static long _packetsReceived;

		public static long BytesSent => 0L;

		public static long BytesReceived => 0L;

		public static long PacketsSent => 0L;

		public static long PacketsReceived => 0L;

		public static void RecordSend(int byteCount)
		{
		}

		public static void RecordReceive(int byteCount, int packetCount = 1)
		{
		}
	}
}
