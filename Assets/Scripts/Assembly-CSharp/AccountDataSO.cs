using UnityEngine;

[CreateAssetMenu(fileName = "New Account Data", menuName = "Scriptable Objects/Default Configuration/New Account Data")]
public class AccountDataSO : ScriptableObject
{
	[SerializeField]
	private string _id;

	[SerializeField]
	private string email;

	[SerializeField]
	private string game_id;

	[SerializeField]
	private string accountName;

	[SerializeField]
	private string gender;

	[SerializeField]
	private bool is_email_verified;

	[SerializeField]
	private bool is_approved;

	[SerializeField]
	private bool is_admin;

	[SerializeField]
	private int status;

	[SerializeField]
	private int group_id;

	[SerializeField]
	private string active_macaddress;

	[SerializeField]
	private string active_token;

	[SerializeField]
	private int game_status;

	[SerializeField]
	private bool isCoach;

	public string getId()
	{
		return null;
	}

	public string getEmail()
	{
		return null;
	}

	public string getGameID()
	{
		return null;
	}

	public string getName()
	{
		return null;
	}

	public string getGender()
	{
		return null;
	}

	public bool getEmailVerif()
	{
		return false;
	}

	public bool getAccountApproval()
	{
		return false;
	}

	public bool getAdminCredential()
	{
		return false;
	}

	public int getStatus()
	{
		return 0;
	}

	public int getGroupID()
	{
		return 0;
	}

	public string getActiveMacAddress()
	{
		return null;
	}

	public string getActiveToken()
	{
		return null;
	}

	public int getGameStatus()
	{
		return 0;
	}

	public bool getCoachCredential()
	{
		return false;
	}

	public void SetAccountDataSO(string id, string email, string game_id, string name, string gender, bool is_email_verified, bool is_approved, bool is_admin, int status, int group_id, string active_macaddress, string active_token, int game_status, bool isCoach)
	{
	}

	public void UpdateToken(string token)
	{
	}
}
