using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using RestClient.Core.Models;
using UnityEngine;

public class GameReportApiClient : MonoBehaviour
{
	public sealed class GameReportResult
	{
		public bool Success;

		public string Message;

		public string Code;
	}

	private sealed class ApiErrorResult
	{
		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("errors")]
		public List<string> Errors { get; set; }
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass12_0
	{
		public Response response;

		public bool done;

		internal void _003CSendReportRoutine_003Eb__0(Response r)
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CSendReportRoutine_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GameReportApiClient _003C_003E4__this;

		public Action<GameReportResult> callback;

		public string reporterCharacterName;

		public string reportedCharacterName;

		public ReportCategory category;

		public string details;

		private _003C_003Ec__DisplayClass12_0 _003C_003E8__1;

		private float _003Cdeadline_003E5__2;

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
		public _003CSendReportRoutine_003Ed__12(int _003C_003E1__state)
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

	[Header("Endpoint")]
	[Tooltip("Root of the public API, e.g. https://api.trinityrift.online — no trailing slash needed.")]
	[SerializeField]
	private string _apiBaseUrl;

	[Tooltip("Path of the report endpoint on that API.")]
	[SerializeField]
	private string _reportsEndpointPath;

	[Tooltip("Sent as the X-Game-Key header. Must match the API's GameReport:ApiKey for the build you are pointing at — the development key will NOT work against live.")]
	[SerializeField]
	private string _gameKey;

	[Tooltip("Give up on a report after this long. Long enough for a bad mobile connection, short enough that the window does not sit spinning.")]
	[SerializeField]
	private float _timeoutSeconds;

	public static GameReportApiClient Instance { get; private set; }

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void SendReport(string reporterCharacterName, string reportedCharacterName, ReportCategory category, string details, Action<GameReportResult> callback)
	{
	}

	[IteratorStateMachine(typeof(_003CSendReportRoutine_003Ed__12))]
	private IEnumerator SendReportRoutine(string reporterCharacterName, string reportedCharacterName, ReportCategory category, string details, Action<GameReportResult> callback)
	{
		return null;
	}

	private static GameReportResult ParseError(Response response)
	{
		return null;
	}

	private static GameReportResult Failure(string message, string code)
	{
		return null;
	}

	private string BuildUrl()
	{
		return null;
	}

	private static string CurrentServerKey()
	{
		return null;
	}
}
