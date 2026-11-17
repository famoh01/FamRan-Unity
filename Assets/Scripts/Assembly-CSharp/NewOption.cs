using System;
using Core.Enumerators;

[Serializable]
public class NewOption
{
	public float rate;

	public RandomOptionEnum randomType;

	public int quantity;

	public NewOptionSection[] newOptionSection;
}
