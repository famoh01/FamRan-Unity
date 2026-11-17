using System;
using System.Collections.Generic;
using UnityEngine;

namespace RANMobile.Authentication
{
	/// <summary>
	/// Remote-player state fed by the TCP session (main thread via Pump) and
	/// consumed by WorldNetBehaviour in the world scene: spawns avatars for
	/// joiners, lerps them toward server positions, removes leavers, and
	/// reports the local player's transform back to the server at 10 Hz.
	/// </summary>
	public static class WorldNet
	{
		public class RemoteState
		{
			public WorldTcpClient.RemotePlayerInfo info;

			public Vector3 targetPos;

			public float targetDir;

			public bool run;

			public bool posDirty;

			public GameObject avatar;

			public bool hasLocoParams;

			public Transform nameplate;
		}

		public static readonly Dictionary<string, RemoteState> Remotes = new Dictionary<string, RemoteState>();

		/// <summary>
		/// Live TCP client for the current session. Set by CharacterLoginManager
		/// before the world scene loads (the manager itself is destroyed by the
		/// scene switch, so WorldNetBehaviour must not look it up afterwards).
		/// </summary>
		public static WorldTcpClient Client;

		/// <summary>Fired from WorldNetBehaviour on the main thread for each chat line.</summary>
		public static event Action<string, string> ChatLine;

		public static void FireChatLine(string name, string text)
		{
			Action<string, string> handler = ChatLine;
			if (handler != null)
			{
				handler(name, text);
			}
		}

		private static readonly List<string> _pendingSpawn = new List<string>();

		private static readonly List<string> _pendingRemove = new List<string>();

		public struct AttackEvent { public string id; public int combo; public float dir; }

		public struct HpEvent { public string id; public int hp; public int maxHp; }

		public struct ChatEvent { public string id; public string name; public string text; }

		private static readonly List<AttackEvent> _pendingAttack = new List<AttackEvent>();

		private static readonly List<HpEvent> _pendingHp = new List<HpEvent>();

		private static readonly List<ChatEvent> _pendingChat = new List<ChatEvent>();

		public static void Clear()
		{
			Remotes.Clear();
			_pendingSpawn.Clear();
			_pendingRemove.Clear();
			_pendingAttack.Clear();
			_pendingHp.Clear();
			_pendingChat.Clear();
			Client = null;
		}

		public static void QueueEnter(WorldTcpClient.RemotePlayerInfo info)
		{
			if (string.IsNullOrEmpty(info.id))
			{
				return;
			}
			if (Client != null && info.id == Client.SelfId)
			{
				WorldRuntime.Level = info.level;
				// Entering the world with a server-authoritative level: refresh the real
				// exp requirement and re-derive stats (class constants × level × gear).
				WorldRuntime.MaxExp = WorldRuntime.ExpForLevel(WorldRuntime.Level);
				WorldRuntime.RecomputeDerived(true);
			}
			// A LEAVE for this id may still be pending (session takeover sends
			// LEAVE+ENTER back-to-back); cancel it and reuse the state.
			_pendingRemove.Remove(info.id);
			RemoteState state;
			if (Remotes.TryGetValue(info.id, out state) && state.avatar != null)
			{
				state.targetPos = new Vector3(info.x, info.y, info.z);
				state.targetDir = info.dir;
				state.posDirty = true;
				return;
			}
			state = new RemoteState();
			state.info = info;
			state.targetPos = new Vector3(info.x, info.y, info.z);
			state.targetDir = info.dir;
			Remotes[info.id] = state;
			_pendingSpawn.Add(info.id);
			Debug.Log("[WorldNet] remote enter: " + info.name + " (" + info.id + ") at " + state.targetPos);
		}

		public static void QueueMove(string id, float x, float y, float z, float dir, bool run)
		{
			RemoteState state;
			if (!Remotes.TryGetValue(id, out state))
			{
				return;
			}
			state.targetPos = new Vector3(x, y, z);
			state.targetDir = dir;
			state.run = run;
			state.posDirty = true;
		}

		public static void QueueLeave(string id)
		{
			if (!Remotes.ContainsKey(id))
			{
				return;
			}
			_pendingRemove.Add(id);
			Debug.Log("[WorldNet] remote leave: " + id);
		}

		public static void QueueAttack(string id, int combo, float dir)
		{
			AttackEvent ev;
			ev.id = id;
			ev.combo = combo;
			ev.dir = dir;
			_pendingAttack.Add(ev);
		}

		public static void QueueHp(string id, int hp, int maxHp)
		{
			HpEvent ev;
			ev.id = id;
			ev.hp = hp;
			ev.maxHp = maxHp;
			_pendingHp.Add(ev);
		}

		public static void QueueChat(string id, string name, string text)
		{
			ChatEvent ev;
			ev.id = id;
			ev.name = name;
			ev.text = text;
			_pendingChat.Add(ev);
		}

		internal static void ConsumePending(List<string> spawnOut, List<string> removeOut, List<AttackEvent> attackOut, List<HpEvent> hpOut, List<ChatEvent> chatOut)
		{
			spawnOut.AddRange(_pendingSpawn);
			_pendingSpawn.Clear();
			removeOut.AddRange(_pendingRemove);
			_pendingRemove.Clear();
			attackOut.AddRange(_pendingAttack);
			_pendingAttack.Clear();
			hpOut.AddRange(_pendingHp);
			_pendingHp.Clear();
			chatOut.AddRange(_pendingChat);
			_pendingChat.Clear();
		}
	}

	/// <summary>
	/// Scene-side driver for remote players. Created by WorldEntry next to the
	/// local player; also throttles local move reports to the server.
	/// </summary>
	public class WorldNetBehaviour : MonoBehaviour
	{
		private const float SendInterval = 0.1f;

		private static readonly int IsStayParam = Animator.StringToHash("isStay");

		private static readonly int IsWalkParam = Animator.StringToHash("isWalk");

		private static readonly int IsRunParam = Animator.StringToHash("isRun");

		private static readonly int IsAttack1Param = Animator.StringToHash("isAttack1");

		private static readonly int IsAttack2Param = Animator.StringToHash("isAttack2");

		private static readonly int IsAttack3Param = Animator.StringToHash("isAttack3");

		private const float AttackAnimSeconds = 0.5f;

		private readonly List<string> _spawnBatch = new List<string>();

		private readonly List<string> _removeBatch = new List<string>();

		private readonly List<WorldNet.AttackEvent> _attackBatch = new List<WorldNet.AttackEvent>();

		private readonly List<WorldNet.HpEvent> _hpBatch = new List<WorldNet.HpEvent>();

		private readonly List<WorldNet.ChatEvent> _chatBatch = new List<WorldNet.ChatEvent>();

		private readonly Dictionary<string, float> _attackUntil = new Dictionary<string, float>();

		private Transform _localPlayer;

		private WorldTcpClient _client;

		private float _lastSendTime;

		private Vector3 _lastSentPos;

		private float _lastSentDir = float.MinValue;

		public void Init(Transform localPlayer)
		{
			_localPlayer = localPlayer;
			_client = WorldNet.Client;
		}

		private void Update()
		{
			if (_client != null)
			{
				_client.Pump();
			}
			SendLocalMoves();
			WorldNet.ConsumePending(_spawnBatch, _removeBatch, _attackBatch, _hpBatch, _chatBatch);
			for (int i = 0; i < _removeBatch.Count; i++)
			{
				RemoveAvatar(_removeBatch[i]);
			}
			_removeBatch.Clear();
			for (int i = 0; i < _spawnBatch.Count; i++)
			{
				SpawnAvatar(_spawnBatch[i]);
			}
			_spawnBatch.Clear();
			for (int i = 0; i < _attackBatch.Count; i++)
			{
				WorldNet.AttackEvent ev = _attackBatch[i];
				WorldNet.RemoteState state;
				if (WorldNet.Remotes.TryGetValue(ev.id, out state) && state.avatar != null)
				{
					_attackUntil[ev.id] = Time.unscaledTime + AttackAnimSeconds;
					Animator anim = state.avatar.GetComponent<Animator>();
					if (anim != null)
					{
						int param = (ev.combo == 2) ? IsAttack2Param : ((ev.combo == 3) ? IsAttack3Param : IsAttack1Param);
						anim.SetBool(param, true);
					}
					Debug.Log("[WorldNet] remote attack: " + ev.id + " combo " + ev.combo);
				}
			}
			_attackBatch.Clear();
			for (int i = 0; i < _hpBatch.Count; i++)
			{
				WorldNet.HpEvent ev = _hpBatch[i];
				Debug.Log("[WorldNet] hp update: " + ev.id + " " + ev.hp + "/" + ev.maxHp);
			}
			_hpBatch.Clear();
			for (int i = 0; i < _chatBatch.Count; i++)
			{
				WorldNet.ChatEvent ev = _chatBatch[i];
				Debug.Log("[WorldNet] chat <" + ev.name + ">: " + ev.text);
				WorldNet.FireChatLine(ev.name, ev.text);
			}
			_chatBatch.Clear();
			foreach (WorldNet.RemoteState state in WorldNet.Remotes.Values)
			{
				StepAvatar(state);
			}
		}

		private void SendLocalMoves()
		{
			if (_client == null || _localPlayer == null)
			{
				return;
			}
			if (Time.unscaledTime - _lastSendTime < SendInterval)
			{
				return;
			}
			Vector3 pos = _localPlayer.position;
			float dir = _localPlayer.eulerAngles.y;
			bool moved = (pos - _lastSentPos).sqrMagnitude > 0.01f || Mathf.Abs(Mathf.DeltaAngle(dir, _lastSentDir)) > 0.5f;
			if (!moved && _lastSentDir != float.MinValue)
			{
				return;
			}
			_lastSendTime = Time.unscaledTime;
			_lastSentPos = pos;
			_lastSentDir = dir;
			WorldPlayerMover mover = (_localPlayer != null) ? _localPlayer.GetComponent<WorldPlayerMover>() : null;
			bool running = (mover != null) && mover.IsRunning;
			_client.SendMove(pos.x, pos.y, pos.z, dir, running);
		}

		private static Vector3 SnapToGround(Vector3 pos)
		{
			if (Mathf.Abs(pos.y) > 0.01f)
			{
				return pos;
			}
			RaycastHit hit;
			if (Physics.Raycast(pos + Vector3.up * 50f, Vector3.down, out hit, 200f))
			{
				return new Vector3(pos.x, hit.point.y, pos.z);
			}
			return pos;
		}

		private void SpawnAvatar(string id)
		{
			WorldNet.RemoteState state;
			if (!WorldNet.Remotes.TryGetValue(id, out state) || state.avatar != null)
			{
				return;
			}
			bool female = state.info.gender == 1;
			GameObject prefab = Resources.Load<GameObject>("prefabs/characterselection/" + (female ? "w_playerS" : "m_playerS"));
			if (prefab == null)
			{
				Debug.LogWarning("[WorldNet] player prefab missing for " + state.info.name);
				return;
			}
			GameObject go = UnityEngine.Object.Instantiate(prefab, SnapToGround(state.targetPos), Quaternion.Euler(0f, state.targetDir, 0f));
			go.name = "Remote_" + state.info.name;
			var agent = go.GetComponent<UnityEngine.AI.NavMeshAgent>();
			if (agent != null)
			{
				agent.enabled = false;
			}
			PreviewGearBinder.Apply(go, state.info.school, female, state.info.cls);
			if (go.GetComponent<AnimationConfiguration>() == null)
			{
				go.AddComponent<AnimationConfiguration>();
			}
			Animator anim = go.GetComponent<Animator>();
			if (anim != null)
			{
				string path = "Animator/" + ClassFolder(state.info.cls);
				RuntimeAnimatorController ctrl = Resources.Load<RuntimeAnimatorController>(path);
				if (ctrl != null)
				{
					anim.runtimeAnimatorController = ctrl;
				}
				state.hasLocoParams = false;
				foreach (AnimatorControllerParameter p in anim.parameters)
				{
					if (p.nameHash == IsWalkParam)
					{
						state.hasLocoParams = true;
						break;
					}
				}
				anim.SetBool(IsStayParam, true);
			}
			TextMesh label = go.AddComponent<TextMesh>();
			label.text = state.info.name;
			label.fontSize = 48;
			label.characterSize = 0.12f;
			label.anchor = TextAnchor.LowerCenter;
			label.alignment = TextAlignment.Center;
			label.color = new Color(1f, 1f, 0.85f);
			label.transform.localPosition = new Vector3(0f, 16.5f, 0f);
			state.nameplate = label.transform;
			state.avatar = go;
			state.posDirty = false;
		}

		private void RemoveAvatar(string id)
		{
			WorldNet.RemoteState state;
			if (!WorldNet.Remotes.TryGetValue(id, out state))
			{
				return;
			}
			if (state.avatar != null)
			{
				UnityEngine.Object.Destroy(state.avatar);
			}
			WorldNet.Remotes.Remove(id);
		}

		private void StepAvatar(WorldNet.RemoteState state)
		{
			GameObject go = state.avatar;
			if (go == null)
			{
				return;
			}
			bool moving = (go.transform.position - state.targetPos).sqrMagnitude > 0.05f || !Mathf.Approximately(Mathf.DeltaAngle(go.transform.eulerAngles.y, state.targetDir), 0f);
			Vector3 groundedTarget = SnapToGround(state.targetPos);
			go.transform.position = Vector3.Lerp(go.transform.position, groundedTarget, 12f * Time.deltaTime);
			Quaternion target = Quaternion.Euler(0f, state.targetDir, 0f);
			go.transform.rotation = Quaternion.Slerp(go.transform.rotation, target, 12f * Time.deltaTime);
			// Billboard the nameplate toward the camera (TextMesh renders on +Z; the
			// avatar's own rotation would show it sideways/backwards).
			if (state.nameplate != null && WorldRuntime.WorldCamera != null)
			{
				state.nameplate.rotation = Quaternion.LookRotation(WorldRuntime.WorldCamera.transform.position - state.nameplate.position);
			}
			Animator anim = go.GetComponent<Animator>();
			if (anim != null && state.hasLocoParams)
			{
				anim.SetBool(IsStayParam, !moving);
				anim.SetBool(IsWalkParam, moving && !state.run);
				anim.SetBool(IsRunParam, moving && state.run);
			}
			float until;
			if (anim != null && _attackUntil.TryGetValue(state.info.id, out until))
			{
				if (Time.unscaledTime >= until)
				{
					_attackUntil.Remove(state.info.id);
					anim.SetBool(IsAttack1Param, false);
					anim.SetBool(IsAttack2Param, false);
					anim.SetBool(IsAttack3Param, false);
				}
			}
			state.posDirty = false;
		}

		private static string ClassFolder(int cls)
		{
			switch (cls)
			{
				case 1:
					return "Brawler";
				case 2:
					return "Swordsman";
				case 4:
					return "Archer";
				case 8:
					return "Shaman";
				default:
					return "Brawler";
			}
		}
	}
}
