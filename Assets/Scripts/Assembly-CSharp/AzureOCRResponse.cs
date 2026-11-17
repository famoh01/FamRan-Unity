using System;

[Serializable]
public class AzureOCRResponse
{
	public string language;

	public decimal textAngle;

	public string orientation;

	public AzureOCRRegion[] regions;
}
