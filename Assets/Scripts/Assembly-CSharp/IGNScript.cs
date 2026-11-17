using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IGNScript : MonoBehaviour
{
	public static IGNScript Instance;

	public TextMeshProUGUI _charName;

	public GameObject[] _charSchool;

	public GameObject _charPartyLeader;

	public GameObject charGameMaster;

	public Image _nameBackground;

	public Sprite _bgCertifierSprite;

	[SerializeField]
	private Sprite _bgNormalSprite;

	private RectTransform _ignBackground;

	public GameObject _guildLogo;

	public GameObject _guildText;

	public bool _haveGuild;

	private bool _haveParty;

	private byte[] _previousBadge;

	private uint _previousClubId;

	private uint _previousMarkVer;

	private string _previousGuildText;

	private Image _guildLogoImg;

	private Sprite _defaultGuildLogoSprite;

	private Color _defaultGuildTextColor;

	public Sprite[] partyLeaderIcon;

	private Unit _unit;

	private TextMeshProUGUI _guildTMP;

	private Image _leaderImage;

	private int _lastLeaderColorTier;

	private static readonly Color s_partyColor;

	private static readonly Color s_colorOrange;

	private static readonly Color s_pkDelinquent;

	private static readonly Color s_pkBully;

	private static readonly Color s_pkMurderer;

	private static readonly Color s_pkMax;

	internal int _driverIndex;

	private int _lastGen;

	private float _backstopAt;

	private NameplateContentDriver _driver;

	private const uint GmUserLevel = 10u;

	private RectTransform _plateLayoutRoot;

	private bool _plateLayoutDirty;

	private string _appliedName;

	private int _plateLayoutRetries;

	private const int MaxPlateLayoutRetries = 30;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void DriverTick(int generation, float now)
	{
	}

	private void MarkPlateLayoutDirty()
	{
	}

	public void RebuildPlateLayout()
	{
	}

	private void FlushPlateLayout()
	{
	}

	private void Start()
	{
	}

	private Unit EnsureUnit()
	{
		return null;
	}

	public void Refresh()
	{
	}

	public void CharacterSchool()
	{
	}

	public void CharacterName()
	{
	}

	public void CharacterHaveParty()
	{
	}

	private void CharacterCertifier()
	{
	}

	public void CharacterHaveGuild()
	{
	}

	private static Sprite BuildClubBadgeSprite(byte[] rgba)
	{
		return null;
	}

	public void SetPartyLeaderIcon(Unit chaUnit, bool isInParty)
	{
	}

	public void SetPartyLeaderIconColor(int partyType)
	{
	}

	public void GuildIGNEnabler(bool flag)
	{
	}

	private static bool IsEventPvPTarget(Unit unit)
	{
		return false;
	}

	private static bool IsHostileToMe(uint charId)
	{
		return false;
	}

	private static bool IsClubWarPvPTarget(Unit unit)
	{
		return false;
	}

	private static bool IsTyrannyPvPTarget(Unit unit)
	{
		return false;
	}

	public static bool IsNameColorRed(Unit unit)
	{
		return false;
	}

	private static Color GetPKColor(int nBright)
	{
		return default;
	}
}
