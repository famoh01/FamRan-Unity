using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetail : MonoBehaviour
{
	[Header("UI Components")]
	public Image _schoolImage;

	public Image _playerClassImage;

	public TextMeshProUGUI _playerIGNText;

	public TextMeshProUGUI _playerLevelText;

	[Header("Class Icon References")]
	public Sprite brawlerIcon;

	public Sprite swordsmanIcon;

	public Sprite archerIcon;

	public Sprite shamanIcon;

	[Header("School Icon References")]
	public Sprite sacredGateIcon;

	public Sprite mysticPeakIcon;

	public Sprite phoenixIcon;

	private Sprite _defaultClassSprite;

	private Sprite _defaultSchoolSprite;

	private Color _defaultLevelColor;

	private void Awake()
	{
	}

	public void SetPlayerDetail(FriendDisplayData data)
	{
	}
}
