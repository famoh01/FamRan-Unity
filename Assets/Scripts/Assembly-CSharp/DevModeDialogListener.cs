using System;
using UnityEngine;

public class DevModeDialogListener : AndroidJavaProxy
{
	private readonly Action _onDismissed;

	public DevModeDialogListener(Action onDismissed)
		: base((string)null)
	{
	}

	public void onClick(AndroidJavaObject dialog, int which)
	{
	}
}
