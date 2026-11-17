using System;

[Serializable]
public class QuestProgress
{
	public bool useProgress;

	public FailType failType;

	public ProgressStart progressStart;

	public ProgressEnd progressEnd;

	public ProgressFail progressFail;
}
