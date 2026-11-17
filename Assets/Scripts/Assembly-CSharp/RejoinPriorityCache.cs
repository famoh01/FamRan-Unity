public static class RejoinPriorityCache
{
	private const string KeyChannel = "rejoinPriority_channelIndex";

	private const string KeyStampUtcTicks = "rejoinPriority_stampUtcTicks";

	public const double WindowSeconds = 140.0;

	public static void Stamp(int channelIndex)
	{
	}

	public static void Persist()
	{
	}

	public static bool IsEligible(int channelIndex)
	{
		return false;
	}
}
