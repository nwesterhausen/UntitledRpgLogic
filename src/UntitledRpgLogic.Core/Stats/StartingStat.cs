namespace UntitledRpgLogic.Core.Stats;

public record StartingStat
{
	public Ulid StatId { get; init; }
	public int StartingMaxValue { get; init; }
	public bool StartsFull { get; init; } = true;
}
