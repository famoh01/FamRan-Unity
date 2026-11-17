using System;

[Serializable]
public class AzureOCRRegion
{
	public string boundingBox;

	public AzureOCRLine[] lines;
}
