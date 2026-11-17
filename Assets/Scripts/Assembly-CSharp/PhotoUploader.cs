using UnityEngine;
using UnityEngine.UI;

public class PhotoUploader : MonoBehaviour
{
	public Image targetImage;

	public long maxFileSizeInBytes;

	private const float UploadDebounceSeconds = 5f;

	private float _nextUploadAt;

	private Sprite _previewSprite;

	private Texture2D _previewSourceTex;

	private readonly string[] validExtensions;

	public void OnUploadPhotoButtonClicked()
	{
	}

	private bool ValidateCanUploadBadge()
	{
		return false;
	}

	private static void ShowFeedback(string text)
	{
	}

	private bool IsValidImageFile(string path)
	{
		return false;
	}

	private bool IsFileSizeValid(string path)
	{
		return false;
	}

	private void ShowFileTooLargeWarning()
	{
	}

	private void LoadImageFromPath(string path)
	{
	}

	private void OnDestroy()
	{
	}

	private static byte[] EncodeBadgePayload(Texture2D source)
	{
		return null;
	}
}
