using RAN.Mobile.Enumerators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TrinityRift.UI.ServerSelect
{
	[DisallowMultipleComponent]
	public class ChannelRowView : MonoBehaviour
	{
		[SerializeField]
		private Button button;

		[SerializeField]
		private TextMeshProUGUI nameLabel;

		[Tooltip("Optional — 'Smooth' / 'Busy' / 'Full', tinted by status.")]
		[SerializeField]
		private TextMeshProUGUI statusLabel;

		[Tooltip("Optional — icon/badge shown only on PK channels. The [PK] suffix on the name is separate.")]
		[SerializeField]
		private GameObject pkBadge;

		[Tooltip("Optional — arrow/glow shown on the selected row.")]
		[SerializeField]
		private GameObject selectedIndicator;

		[Tooltip("Optional — status bar/fill whose fillAmount tracks how full the channel is.")]
		[SerializeField]
		private Image populationBar;

		[Header("Selected / normal look")]
		[SerializeField]
		private Color selectedTextColor = new Color(1f, 0.85f, 0.3f);

		[SerializeField]
		private Color normalTextColor = Color.white;

		private static readonly Color SmoothColor = new Color(0.35f, 0.85f, 0.4f);

		private static readonly Color BusyColor = new Color(0.95f, 0.75f, 0.2f);

		private static readonly Color FullColor = new Color(0.95f, 0.3f, 0.3f);

		private static readonly Color UnknownColor = new Color(0.7f, 0.7f, 0.7f);

		private ServerSelectUI _owner;

		private int _index;

		private bool _selected;

		public int Index => _index;

		private static Color StatusColor(ChannelStatusEnum status)
		{
			switch (status)
			{
			case ChannelStatusEnum.Smooth:
				return SmoothColor;
			case ChannelStatusEnum.Busy:
				return BusyColor;
			case ChannelStatusEnum.Full:
				return FullColor;
			default:
				return UnknownColor;
			}
		}

		public void Initialize(ServerSelectUI owner)
		{
			_owner = owner;
			if (button == null)
			{
				button = GetComponentInChildren<Button>(true);
			}
			if (nameLabel == null)
			{
				nameLabel = GetComponentInChildren<TextMeshProUGUI>(true);
			}
			if (button != null)
			{
				button.onClick.RemoveListener(HandleClick);
				button.onClick.AddListener(HandleClick);
			}
		}

		private void HandleClick()
		{
			if (_owner != null)
			{
				_owner.OnChannelRowClicked(_index);
			}
		}

		public void Bind(int index, ClientServerChannel data)
		{
			// Use the list position, not data.channelIndex — OnChannelRowClicked
			// validates against the _channels array bounds, and the server's
			// channelIndex is not guaranteed to match that positional index.
			_index = index;
			if (button == null)
			{
				button = GetComponentInChildren<Button>(true);
			}
			if (nameLabel == null)
			{
				nameLabel = GetComponentInChildren<TextMeshProUGUI>(true);
			}
			if (button != null)
			{
				button.onClick.RemoveListener(HandleClick);
				button.onClick.AddListener(HandleClick);
			}
			if (nameLabel != null)
			{
				nameLabel.text = ServerSelectLabels.Channel(_index, data != null && data.pk);
			}
			if (statusLabel != null && data != null)
			{
				statusLabel.text = data.channelStatus.ToString();
				statusLabel.color = StatusColor(data.channelStatus);
			}
			if (pkBadge != null && data != null)
			{
				pkBadge.SetActive(data.pk);
			}
			if (populationBar != null && data != null)
			{
				populationBar.fillAmount = data.maxChannelUser > 0
					? Mathf.Clamp01((float)data.playersInChannel / data.maxChannelUser)
					: 0f;
			}
			SetSelected(_selected, force: true);
		}

		public void SetSelected(bool selected, bool force = false)
		{
			if (_selected == selected && !force)
			{
				return;
			}
			_selected = selected;
			if (selectedIndicator != null)
			{
				selectedIndicator.SetActive(selected);
			}
			if (nameLabel != null)
			{
				nameLabel.color = selected ? selectedTextColor : normalTextColor;
			}
		}

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}
	}
}
