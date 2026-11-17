using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TrinityRift.UI.ServerSelect
{
	[DisallowMultipleComponent]
	public class ServerRowView : MonoBehaviour
	{
		[SerializeField]
		private Button button;

		[SerializeField]
		private TextMeshProUGUI nameLabel;

		[Tooltip("Optional — player count on this server. Leave empty to not show one.")]
		[SerializeField]
		private TextMeshProUGUI populationLabel;

		[Tooltip("Optional — arrow/glow shown on the selected row.")]
		[SerializeField]
		private GameObject selectedIndicator;

		[Tooltip("Optional — row background swapped between the two sprites below.")]
		[SerializeField]
		private Image background;

		[Header("Selected / normal look")]
		[SerializeField]
		private Color selectedTextColor = new Color(1f, 0.85f, 0.3f);

		[SerializeField]
		private Color normalTextColor = Color.white;

		[SerializeField]
		private Sprite selectedBackground;

		[SerializeField]
		private Sprite normalBackground;

		private ServerSelectUI _owner;

		private int _index;

		private int _population;

		private bool _selected;

		public int Index => _index;

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
				_owner.OnServerRowClicked(_index);
			}
		}

		public void Bind(int index, string displayName, int population)
		{
			_index = index;
			_population = population;
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
				nameLabel.text = displayName;
			}
			if (populationLabel != null)
			{
				populationLabel.text = population.ToString();
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
			if (background != null)
			{
				Sprite s = selected ? selectedBackground : normalBackground;
				if (s != null)
				{
					background.sprite = s;
				}
			}
		}

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}
	}
}
