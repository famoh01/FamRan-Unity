using System.Collections.Generic;
using Models.Library.Shared.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Party
{
	public class PartyLeaderSlot : MonoBehaviour
	{
		[Header("Buffs")]
		public Transform _buffIconsContainer;

		public GameObject _buffIconPrefab;

		private int _leaderPlayerId;

		private Canvas _panelCanvas;

		private Dictionary<Vector2, GameObject> _activeBuffIcons;

		private Dictionary<Vector2, Image> _activeBuffFills;

		private Dictionary<Vector2, SkillObject> _activeBuffSkills;

		private List<Vector2> _buffRemoveBuffer;

		private readonly Stack<GameObject> _buffIconPool;

		private readonly HashSet<Vector2> _currentBuffsThisFrame;

		private TargetInfoEx _cachedTargetInfo;

		private ushort _cachedTargetInfoPlayerId;

		private bool _hasCachedTargetInfo;

		private const int BuffIconImageIndex = 1;

		private const int BuffFillImageIndex = 2;

		public void InitializeLeaderBuffs(int leaderPlayerId)
		{
		}

		private TargetInfoEx GetCachedTargetInfo(ushort playerId)
		{
			return default;
		}

		private void Update()
		{
		}

		private void SyncBuffsCentralized(SkillFact[] skillFacts)
		{
		}

		private void ClearAllBuffs()
		{
		}

		private GameObject RentBuffIcon()
		{
			return null;
		}

		private void ReturnBuffIcon(GameObject icon)
		{
		}

		private void OnDestroy()
		{
		}
	}
}
