using System;

public class Body
{
	public int id { get; set; }

	public string username { get; set; }

	public string normalizedUsername { get; set; }

	public string password { get; set; }

	public string oldPassword { get; set; }

	public string pinCode { get; set; }

	public string oldPinCode { get; set; }

	public int userRole { get; set; }

	public int loginState { get; set; }

	public DateTime createdDate { get; set; }

	public string refreshToken { get; set; }

	public DateTime refreshTokenExpiryTime { get; set; }

	public DateTime lastLoginDate { get; set; }

	public object serverGroupNum { get; set; }

	public object serverNum { get; set; }

	public bool isBlocked { get; set; }

	public DateTime blockedDate { get; set; }

	public int characterRemain { get; set; }

	public DateTime premiumDate { get; set; }

	public DateTime chatBlockDate { get; set; }

	public int premiumPoints { get; set; }

	public int gameTime { get; set; }

	public int gamePoints { get; set; }

	public object registeredReferralCode { get; set; }

	public object referralCode { get; set; }

	public int referralPoints { get; set; }

	public bool emailAddressConfirmed { get; set; }

	public string emailAddress { get; set; }

	public string normalizedEmailAddress { get; set; }

	public object firstname { get; set; }

	public object lastname { get; set; }

	public object phoneNumber { get; set; }

	public int userAge { get; set; }

	public object secretAnswer { get; set; }

	public bool hasRegisteredRefferalCode { get; set; }

	public bool hasRefferalCode { get; set; }
}
