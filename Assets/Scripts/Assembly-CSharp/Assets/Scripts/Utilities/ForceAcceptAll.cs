using UnityEngine.Networking;

namespace Assets.Scripts.Utilities
{
	public class ForceAcceptAll : CertificateHandler
	{
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return false;
		}
	}
}
