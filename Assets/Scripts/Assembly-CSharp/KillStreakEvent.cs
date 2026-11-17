public readonly struct KillStreakEvent
{
	public readonly KillStreakCategory Category;

	public readonly int Count;

	public readonly KillStreakTier Tier;

	public readonly string Label;

	public readonly ushort GaeaId;

	public readonly KillStreakSource Source;

	public readonly bool WorthBroadcasting;

	public KillStreakEvent(KillStreakCategory category, int count, KillStreakTier tier, ushort gaeaId, KillStreakSource source, bool worthBroadcasting)
	{
		Category = KillStreakCategory.FirstBlood;
		Count = 0;
		Tier = null;
		Label = null;
		GaeaId = 0;
		Source = KillStreakSource.ClientPredicted;
		WorthBroadcasting = false;
	}
}
