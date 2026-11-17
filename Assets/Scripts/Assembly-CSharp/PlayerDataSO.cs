using RAN.Mobile.Enumerators;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Scriptable Objects/Default Configuration/New Player Data")]
public class PlayerDataSO : ScriptableObject
{
	[SerializeField]
	private string m_username;

	[SerializeField]
	private string m_password;

	[SerializeField]
	private string m_accessToken;

	[SerializeField]
	private string m_refreshToken;

	[SerializeField]
	private UserRoleEnum m_userRole;

	public string username => null;

	public string password => null;

	public string accessToken => null;

	public string refreshToken => null;

	public UserRoleEnum userRole => UserRoleEnum.Player;

	public string getAccessToken()
	{
		return null;
	}

	public string getPassword()
	{
		return null;
	}

	public UserRoleEnum GetUserRole()
	{
		return UserRoleEnum.Player;
	}

	public void SetUsername(string _username)
	{
	}

	public void SetPassword(string _password)
	{
	}

	public void SetAccessToken(string _accessToken)
	{
	}

	public void SetRefreshToken(string _refreshToken)
	{
	}

	public void SetUserRole(UserRoleEnum _userRole)
	{
	}
}
