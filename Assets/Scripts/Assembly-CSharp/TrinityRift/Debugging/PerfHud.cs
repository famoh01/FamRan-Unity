using System.Text;
using TMPro;
using UnityEngine;

namespace TrinityRift.Debugging
{
	public sealed class PerfHud : MonoBehaviour
	{
		private const float SampleIntervalSeconds = 0.5f;

		private const int FpsWindowSize = 120;

		private const float SpikeFrameTimeMs = 33f;

		private readonly float[] _frameTimesMs;

		private readonly float[] _sortedFrameTimesMs;

		private int _frameTimeIdx;

		private int _frameTimeCount;

		private float _fpsAvg;

		private float _fpsLow1pct;

		private float _frameTimeAvgMs;

		private float _frameTimeWorstMs;

		private long _monoHeapBytes;

		private long _totalReservedBytes;

		private long _allocPerSecond;

		private long _lastMonoHeapBytes;

		private float _lastSampleTime;

		private long _lastBytesSent;

		private long _lastBytesReceived;

		private long _lastPacketsSent;

		private long _lastPacketsReceived;

		private float _bytesSentPerSec;

		private float _bytesReceivedPerSec;

		private float _packetsSentPerSec;

		private float _packetsReceivedPerSec;

		private Canvas _canvas;

		private TextMeshProUGUI _text;

		private readonly StringBuilder _sb;

		private int _cachedSceneHandle;

		private string _cachedSceneName;

		private float _sampleTimer;

		public static PerfHud Instance { get; private set; }

		public bool IsVisible => false;

		private void Awake()
		{
		}

		public bool Toggle()
		{
			return false;
		}

		private void Update()
		{
		}

		private void RefreshSample()
		{
		}

		private void RenderText()
		{
		}

		private void BuildUi()
		{
		}
	}
}
