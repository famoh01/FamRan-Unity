using System.Collections.Generic;
using Models.Library.Shared.Game;
using UnityEngine;
using UnityEngine.UI;

public class SelectedPlayerBuffDisplay : MonoBehaviour
{
	[Header("Buff UI References")]
	[Tooltip("The parent transform where buff icons will be instantiated.")]
	public Transform buffIconsContainer;

	[Tooltip("The prefab for the buff icon. Must contain Icon Image at index 1 and Fill Image at index 2.")]
	public GameObject buffIconPrefab;

	private Dictionary<Vector2, GameObject> _activeBuffIcons;

	private Dictionary<Vector2, Image> _activeBuffFills;

	private Dictionary<Vector2, SkillObject> _activeBuffSkills;

	private List<Vector2> _buffRemoveBuffer;

	private readonly HashSet<Vector2> _currentBuffsThisFrame;

	private readonly Stack<GameObject> _buffIconPool;

	private GameObject _lastTarget;

	private PlayerControllerClient _cachedTargetPlayer;

	private EnemyAiController _cachedTargetMob;

	private const int BuffIconImageIndex = 1;

	private const int BuffFillImageIndex = 2;

	private void Update()
	{
	}

	private void SyncBuffs(SkillFact[] skillFacts)
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
