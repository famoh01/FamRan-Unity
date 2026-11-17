using System;
using UnityEngine;

internal sealed class TyrannyHudCanvas
{
	private readonly string label;

	private readonly Func<Canvas> resolver;

	private Canvas canvas;

	private bool warnedMissing;

	public bool IsOpen => false;

	public TyrannyHudCanvas(string label, Func<Canvas> resolver)
	{
	}

	public bool Set(bool visible, string logPrefix, UnityEngine.Object context)
	{
		return false;
	}
}
