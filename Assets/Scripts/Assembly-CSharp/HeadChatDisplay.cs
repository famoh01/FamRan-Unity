using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Core.Enumerators;
using UnityEngine;
using UnityEngine.UI;

public class HeadChatDisplay : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CHideBubble_003Ed__44 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Unit selectedUnit;

		public HeadChatDisplay _003C_003E4__this;

		public int clientId;

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
		public _003CHideBubble_003Ed__44(int _003C_003E1__state)
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

	private const string MidmanIgnPrefix = "[MIDMAN]";

	[Header("Midman Chat Badge")]
	[Tooltip("Icon shown before chat messages when the server-provided sender IGN starts with [MIDMAN].")]
	[SerializeField]
	private Sprite midmanChatIcon;

	[SerializeField]
	[Min(1f)]
	private float midmanChatIconHeight;

	[SerializeField]
	[Min(0f)]
	private float midmanChatIconSpacing;

	public static HeadChatDisplay instance;

	private Coroutine _hideBubbleCoroutine;

	private Dictionary<int, Coroutine> _hideBubbleCoroutines;

	private string whisperChatName;

	private const int MaxMessagesPerContainer = 100;

	private readonly Dictionary<Transform, Queue<GameObject>> _historyByContainer;

	[Header("Chat Spam Limit")]
	[SerializeField]
	private int maxMessagesPerWindow;

	[SerializeField]
	private float chatWindowSeconds;

	[SerializeField]
	private float chatBanSeconds;

	private float[] _chatSendTimes;

	private int _chatSendIndex;

	private float _chatMutedUntil;

	private float _lastChatWarnTime;

	private Transform _chatContainerRoot;

	private Image _basicLeftChatBackground;

	private void Awake()
	{
	}

	private bool CheckChatRateLimit()
	{
		return false;
	}

	private void WarnThrottled(string text)
	{
	}

	public string GetWhisperChatName()
	{
		return null;
	}

	public void SetWhisperChatName(string name)
	{
	}

	public void SetWhisperChatName(GameObject gameObject)
	{
	}

	public bool IsValidWhisperChat(string message)
	{
		return false;
	}

	private static bool TryParseValidWhisper(string input, out string targetName, out string whisperBody)
	{
		targetName = null;
		whisperBody = null;
		return false;
	}

	public bool AddChat(Unit player, string strMessage, ChatTypeEnum type)
	{
		return false;
	}

	public void SetChat(string message, ChatTypeEnum type, string targetName = null)
	{
	}

	public void ChatDisplay(int clientId, string message, string messageType, Color color, ChatTypeEnum chatTypeEnum = ChatTypeEnum.General, string senderName = null)
	{
	}

	public void DisplayChatMessage(string message, ChatTypeEnum chatTypeEnum, Color color, string senderName = null)
	{
	}

	private void InstantiateAndTrack(GameObject template, Transform parent, bool showMidmanIcon)
	{
	}

	private static bool IsMidmanIgn(string senderName)
	{
		return false;
	}

	private void AddMidmanIcon(GameObject chatEntry)
	{
	}

	private static string StripEmoji(string s)
	{
		return null;
	}

	public static bool ContainsEmoji(string s)
	{
		return false;
	}

	private static bool IsEmojiCodepoint(int cp)
	{
		return false;
	}

	private Transform GetChatContainerRoot()
	{
		return null;
	}

	public void ClearAllChatHistory()
	{
	}

	private void DisplayChatToHead(int clientId, string message)
	{
	}

	public static string FormatHeadChatLine(string characterName, string message)
	{
		return null;
	}

	public Image BasicLeftChatBackground()
	{
		return null;
	}

	public Transform BasicChatLeftBar()
	{
		return null;
	}

	private bool TryShowLegacyBubble(Unit selectedUnit, string message)
	{
		return false;
	}

	[IteratorStateMachine(typeof(_003CHideBubble_003Ed__44))]
	private IEnumerator HideBubble(Unit selectedUnit, int clientId)
	{
		return null;
	}
}
