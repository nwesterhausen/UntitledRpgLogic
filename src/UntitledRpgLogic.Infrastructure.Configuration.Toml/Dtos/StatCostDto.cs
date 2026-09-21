using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class StatCostDto
{
	public Ulid StatId { get; init; } = Ulid.Empty;
	public float Amount { get; init; }

	public StatCost ToModel() => new() { StatId = this.StatId, Amount = this.Amount };

	public static StatCostDto FromModel(StatCost model)
	{
		ArgumentNullException.ThrowIfNull(model);

		return new StatCostDto { StatId = model.StatId, Amount = model.Amount };
	}
}
