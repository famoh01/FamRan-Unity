using System;
using Core.Enumerators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public sealed class NearbyLootRowView : MonoBehaviour
	{
		[Header("Required")]
		[SerializeField]
		private Button selectButton;

		[SerializeField]
		private TMP_Text nameLabel;

		[Header("Optional")]
		[SerializeField]
		private Image iconImage;

		[SerializeField]
		private TMP_Text quantityLabel;

		[SerializeField]
		private TMP_Text distanceLabel;

		private Action<CrowType, ushort> _onSelected;

		private CrowType _crowType;

		private ushort _globalId;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		public void Bind(NearbyLootEntry entry, Action<CrowType, ushort> onSelected, string moneyDisplayName, Sprite moneyIcon)
		{
		}

		public void Release()
		{
		}

		private void HandleSelected()
		{
		}
	}
}
