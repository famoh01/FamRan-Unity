using System.Text;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
	public sealed class LootObtainedFeedController : MonoBehaviour
	{
		private struct FeedEntry
		{
			public string Name;

			public long Quantity;

			public float ExpiresAt;
		}

		[Header("Required")]
		[SerializeField]
		private Canvas panelRoot;

		[SerializeField]
		private TMP_Text entriesLabel;

		[Header("Display")]
		[SerializeField]
		private string moneyDisplayName;

		[SerializeField]
		private string separator;

		[SerializeField]
		[Min(1f)]
		private int maxEntries;

		[Tooltip("Seconds each line remains visible. Use 0 to keep entries until ClearEntries is called.")]
		[SerializeField]
		[Min(0f)]
		private float entryLifetimeSeconds;

		[SerializeField]
		private bool hideWhenEmpty;

		private readonly StringBuilder _textBuilder;

		private FeedEntry[] _entries;

		private int _head;

		private int _count;

		private PlayerHandler _playerHandler;

		private void Awake()
		{
		}

		private void OnEnable()
		{
		}

		private void Start()
		{
		}

		private void OnDisable()
		{
		}

		private void Update()
		{
		}

		public void ShowItem(ItemObject item, int quantity)
		{
		}

		public void ShowMoney(long amount)
		{
		}

		public void ClearEntries()
		{
		}

		private void AddEntry(string displayName, long quantity)
		{
		}

		private void RefreshLabel()
		{
		}

		private void ResizeEntriesLabelToContent()
		{
		}

		private void SubscribeToPlayerHandler()
		{
		}

		private void UnsubscribeFromPlayerHandler()
		{
		}

		private void SetPanelVisible(bool visible)
		{
		}
	}
}
