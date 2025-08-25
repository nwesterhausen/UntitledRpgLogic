using UntitledRpgLogic.Core.Interfaces.Effects;
using UntitledRpgLogic.Core.Options;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Effects that each stack of a modification can have on a stat.
/// </summary>
public class ModifierEffect : IModifierEffect
{
	/// <summary>
	///     Creates a new modifier effect with the specified options.
	/// </summary>
	/// <param name="options"></param>
	public ModifierEffect(ModifierEffectOptions options)
	{
		ArgumentNullException.ThrowIfNull(options, nameof(options));

		this.FlatAmount = options.FlatAmount ?? 0;
		this.Percentage = options.Percentage ?? 0f;
		this.PercentageOfMax = options.PercentageOfMax ?? 0f;
		this.IsPositive = options.IsPositive ?? true;
		this.IsAdditive = options.IsAdditive ?? true;
		this.ScalesOnBaseValue = options.ScalesOnBaseValue ?? false;
		this.ScalingFactor = options.ScalingFactor ?? 1f;
		this.Priority = options.Priority ?? 0;
	}

	/// <inheritdoc />
	public int FlatAmount { get; init; }

	/// <inheritdoc />
	public float Percentage { get; init; }

	/// <inheritdoc />
	public float PercentageOfMax { get; init; }

	/// <inheritdoc />
	public bool IsPositive { get; init; }

	/// <inheritdoc />
	public bool IsAdditive { get; init; }

	/// <inheritdoc />
	public bool ScalesOnBaseValue { get; init; }

	/// <inheritdoc />
	public int Priority { get; init; }

	/// <inheritdoc />
	public float ScalingFactor { get; init; }

	/// <inheritdoc />
	public bool AppliesFlatAmount => this.FlatAmount > 0;

	/// <inheritdoc />
	public bool AppliesPercentage => this.Percentage > 0;

	/// <inheritdoc />
	public bool AppliesPercentageOfMax => this.PercentageOfMax > 0;

	/// <inheritdoc />
	public int ApplyEffect(int baseValue, int currentValue, int maxValue)
	{
		if (this.IsAdditive)
		{
			return this.ApplyAdditiveEffect(baseValue, currentValue, maxValue);
		}

		return this.ApplyMultiplicativeEffect(baseValue, currentValue, maxValue);
	}

	/// <summary>
	///     Applies this modifier effect as an additive effect to a stat.
	/// </summary>
	/// <param name="baseValue"></param>
	/// <param name="currentValue"></param>
	/// <param name="maxValue"></param>
	/// <returns></returns>
	private int ApplyAdditiveEffect(int baseValue, int currentValue, int maxValue)
	{
		var returnValue = currentValue;
		if (this.ScalesOnBaseValue)
		{
			var scaledBaseValue = baseValue * this.ScalingFactor;
			if (this.IsPositive)
			{
				if (this.AppliesFlatAmount)
				{
					returnValue += this.FlatAmount + (int)Math.Round(scaledBaseValue);
				}

				if (this.AppliesPercentage)
				{
					returnValue += (int)(this.Percentage * scaledBaseValue);
				}

				if (this.AppliesPercentageOfMax)
				{
					returnValue += (int)(this.PercentageOfMax * maxValue * scaledBaseValue);
				}

				return returnValue;
			}

			if (this.AppliesFlatAmount)
			{
				returnValue -= this.FlatAmount + (int)Math.Round(scaledBaseValue);
			}

			if (this.AppliesPercentage)
			{
				returnValue -= (int)(this.Percentage * scaledBaseValue);
			}

			if (this.AppliesPercentageOfMax)
			{
				returnValue -= (int)(this.PercentageOfMax * maxValue * scaledBaseValue);
			}

			return returnValue;
		}

		if (this.IsPositive)
		{
			if (this.AppliesFlatAmount)
			{
				returnValue += this.FlatAmount;
			}

			if (this.AppliesPercentage)
			{
				returnValue += (int)(this.Percentage * currentValue);
			}

			if (this.AppliesPercentageOfMax)
			{
				returnValue += (int)(this.PercentageOfMax * maxValue);
			}

			return returnValue;
		}

		if (this.AppliesFlatAmount)
		{
			returnValue -= this.FlatAmount;
		}

		if (this.AppliesPercentage)
		{
			returnValue -= (int)(this.Percentage * currentValue);
		}

		if (this.AppliesPercentageOfMax)
		{
			returnValue -= (int)(this.PercentageOfMax * maxValue);
		}

		return returnValue;
	}

	/// <summary>
	///     Applies this modifier effect as a multiplicative effect to a stat.
	/// </summary>
	/// <param name="baseValue"></param>
	/// <param name="currentValue"></param>
	/// <param name="maxValue"></param>
	/// <returns></returns>
	private int ApplyMultiplicativeEffect(int baseValue, int currentValue, int maxValue)
	{
		var returnValue = currentValue;
		if (this.IsPositive)
		{
			if (this.ScalesOnBaseValue)
			{
				var scaledBaseValue = baseValue * this.ScalingFactor;
				if (this.AppliesFlatAmount)
				{
					returnValue += (this.FlatAmount + (int)Math.Round(scaledBaseValue)) * currentValue;
				}

				if (this.AppliesPercentage)
				{
					returnValue += (int)(this.Percentage * scaledBaseValue) * currentValue;
				}

				if (this.AppliesPercentageOfMax)
				{
					returnValue += (int)(this.PercentageOfMax * maxValue * scaledBaseValue) * currentValue;
				}

				return returnValue;
			}

			if (this.AppliesFlatAmount)
			{
				returnValue += this.FlatAmount * currentValue;
			}

			if (this.AppliesPercentage)
			{
				returnValue += (int)(this.Percentage * currentValue) * currentValue;
			}

			if (this.AppliesPercentageOfMax)
			{
				returnValue += (int)(this.PercentageOfMax * maxValue) * currentValue;
			}

			return returnValue;
		}

		if (this.ScalesOnBaseValue)
		{
			var scaledBaseValue = baseValue * this.ScalingFactor;
			if (this.AppliesFlatAmount)
			{
				returnValue -= (this.FlatAmount + (int)Math.Round(scaledBaseValue)) * currentValue;
			}

			if (this.AppliesPercentage)
			{
				returnValue -= (int)(this.Percentage * scaledBaseValue) * currentValue;
			}

			if (this.AppliesPercentageOfMax)
			{
				returnValue -= (int)(this.PercentageOfMax * maxValue * scaledBaseValue) * currentValue;
			}

			return returnValue;
		}

		if (this.AppliesFlatAmount)
		{
			returnValue -= this.FlatAmount * currentValue;
		}

		if (this.AppliesPercentage)
		{
			returnValue -= (int)(this.Percentage * currentValue) * currentValue;
		}

		if (this.AppliesPercentageOfMax)
		{
			returnValue -= (int)(this.PercentageOfMax * maxValue) * currentValue;
		}

		return returnValue;
	}
}
