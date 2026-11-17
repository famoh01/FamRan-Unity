using System;

[Serializable]
public class Attribute
{
	[NonSerialized]
	public PlayerStats parent;

	public Attributes type;

	public ModifiableInt value;

	public void SetParent(PlayerStats _parent)
	{
	}

	public void AttributeModified()
	{
	}
}
