using System;

[Serializable]
public class AzureOCRLine
{
	public string boundingBox;

	public AzureOCRWord[] words;
}
