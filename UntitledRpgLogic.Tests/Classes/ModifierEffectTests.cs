using UntitledRpgLogic.Classes;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.Tests.Classes;

[TestClass]
public class ModifierEffectTests
{
    [TestMethod]
    public void ApplyEffect_Additive_Positive_NoScaling_FlatAmount()
    {
        ModifierEffectOptions options = new() { FlatAmount = 10, IsPositive = true, IsAdditive = true };
        ModifierEffect effect = new(options);
        Assert.AreEqual(110, effect.ApplyEffect(100, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Additive_Positive_NoScaling_Percentage()
    {
        ModifierEffectOptions options = new() { Percentage = 0.1f, IsPositive = true, IsAdditive = true };
        ModifierEffect effect = new(options);
        Assert.AreEqual(110, effect.ApplyEffect(100, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Additive_Positive_NoScaling_PercentageOfMax()
    {
        ModifierEffectOptions options = new() { PercentageOfMax = 0.1f, IsPositive = true, IsAdditive = true };
        ModifierEffect effect = new(options);
        Assert.AreEqual(120, effect.ApplyEffect(100, 100, 200)); // 100 + (0.1 * 200)
    }

    [TestMethod]
    public void ApplyEffect_Additive_Negative_NoScaling_FlatAmount()
    {
        ModifierEffectOptions options = new() { FlatAmount = 10, IsPositive = false, IsAdditive = true };
        ModifierEffect effect = new(options);
        Assert.AreEqual(90, effect.ApplyEffect(100, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Additive_Negative_NoScaling_Percentage()
    {
        ModifierEffectOptions options = new() { Percentage = 0.1f, IsPositive = false, IsAdditive = true };
        ModifierEffect effect = new(options);
        Assert.AreEqual(90, effect.ApplyEffect(100, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Additive_Negative_NoScaling_PercentageOfMax()
    {
        ModifierEffectOptions options = new() { PercentageOfMax = 0.1f, IsPositive = false, IsAdditive = true };
        ModifierEffect effect = new(options);
        Assert.AreEqual(80, effect.ApplyEffect(100, 100, 200)); // 100 - (0.1 * 200)
    }

    [TestMethod]
    public void ApplyEffect_Additive_Positive_ScalesOnBase_FlatAmount()
    {
        // (FlatAmount + baseValue * ScalingFactor)
        ModifierEffectOptions options = new()
            { FlatAmount = 10, IsPositive = true, IsAdditive = true, ScalesOnBaseValue = true, ScalingFactor = 0.5f };
        ModifierEffect effect = new(options);
        // currentValue + FlatAmount + (baseValue * ScalingFactor) = 100 + 10 + (50 * 0.5) = 100 + 10 + 25 = 135. Incorrect calculation in original code.
        // currentValue + FlatAmount + round(baseValue * ScalingFactor) = 100 + 10 + round(50 * 0.5) = 100 + 10 + 25. Still 135.
        // The code is: returnValue += FlatAmount + (int)Math.Round(scaledBaseValue); where scaledBaseValue = baseValue * ScalingFactor
        // So: 100 + 10 + (int)Math.Round(50f * 1f) = 100 + 10 + 50 = 160. If baseValue = 50, scalingFactor = 1f
        // If baseValue = 100, scalingFactor = 0.5f: 100 + 10 + (int)Math.Round(100f * 0.5f) = 100 + 10 + 50 = 160
        Assert.AreEqual(160, effect.ApplyEffect(100, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Additive_Positive_ScalesOnBase_Percentage()
    {
        // currentValue + (Percentage * (baseValue * ScalingFactor))
        ModifierEffectOptions options = new()
            { Percentage = 0.1f, IsPositive = true, IsAdditive = true, ScalesOnBaseValue = true, ScalingFactor = 0.5f };
        ModifierEffect effect = new(options);
        // 100 + (0.1 * (100 * 0.5)) = 100 + (0.1 * 50) = 100 + 5 = 105
        Assert.AreEqual(105, effect.ApplyEffect(100, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Additive_Positive_ScalesOnBase_PercentageOfMax()
    {
        // currentValue + (PercentageOfMax * maxValue * (baseValue * ScalingFactor))
        ModifierEffectOptions options = new()
        {
            PercentageOfMax = 0.1f, IsPositive = true, IsAdditive = true, ScalesOnBaseValue = true, ScalingFactor = 0.5f
        };
        ModifierEffect effect = new(options);
        // 100 + (0.1 * 200 * (100 * 0.5)) = 100 + (0.1 * 200 * 50) = 100 + (20 * 50) = 100 + 1000 = 1100
        Assert.AreEqual(1100, effect.ApplyEffect(100, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Multiplicative_Positive_NoScaling_FlatAmount()
    {
        // currentValue + FlatAmount * currentValue
        ModifierEffectOptions
            options = new()
                { FlatAmount = 2, IsPositive = true, IsAdditive = false }; // Using 2 to make multiplication obvious
        ModifierEffect effect = new(options);
        // 100 + 2 * 100 = 300
        Assert.AreEqual(300, effect.ApplyEffect(50, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Multiplicative_Positive_NoScaling_Percentage()
    {
        // currentValue + (Percentage * currentValue) * currentValue
        ModifierEffectOptions options = new() { Percentage = 0.1f, IsPositive = true, IsAdditive = false };
        ModifierEffect effect = new(options);
        // 100 + (0.1 * 100) * 100 = 100 + 10 * 100 = 100 + 1000 = 1100
        Assert.AreEqual(1100, effect.ApplyEffect(50, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Multiplicative_Positive_NoScaling_PercentageOfMax()
    {
        // currentValue + (PercentageOfMax * maxValue) * currentValue
        ModifierEffectOptions options = new() { PercentageOfMax = 0.1f, IsPositive = true, IsAdditive = false };
        ModifierEffect effect = new(options);
        // 100 + (0.1 * 200) * 100 = 100 + 20 * 100 = 100 + 2000 = 2100
        Assert.AreEqual(2100, effect.ApplyEffect(50, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Multiplicative_Negative_NoScaling_FlatAmount()
    {
        // currentValue - FlatAmount * currentValue
        ModifierEffectOptions options = new()
        {
            FlatAmount = 1, IsPositive = false, IsAdditive = false
        }; // Using 1 to avoid negative results for this test
        ModifierEffect effect = new(options);
        // 100 - 1 * 100 = 0
        Assert.AreEqual(0, effect.ApplyEffect(50, 100, 200));
    }


    [TestMethod]
    public void ApplyEffect_Multiplicative_Positive_ScalesOnBase_FlatAmount()
    {
        // currentValue + (FlatAmount + round(baseValue*ScalingFactor)) * currentValue
        ModifierEffectOptions options = new()
            { FlatAmount = 1, IsPositive = true, IsAdditive = false, ScalesOnBaseValue = true, ScalingFactor = 0.5f };
        ModifierEffect effect = new(options);
        // 100 + (1 + round(50 * 0.5)) * 100 = 100 + (1 + 25) * 100 = 100 + 26 * 100 = 100 + 2600 = 2700
        Assert.AreEqual(2700, effect.ApplyEffect(50, 100, 200));
    }

    // Add more tests for ScalesOnBaseValue for Multiplicative effects (Positive and Negative)
    // Additive Negative ScalesOnBase
    [TestMethod]
    public void ApplyEffect_Additive_Negative_ScalesOnBase_FlatAmount()
    {
        ModifierEffectOptions options = new()
            { FlatAmount = 10, IsPositive = false, IsAdditive = true, ScalesOnBaseValue = true, ScalingFactor = 1f };
        ModifierEffect effect = new(options);
        // 100 - (10 + round(100*1)) = 100 - (10+100) = 100 - 110 = -10
        Assert.AreEqual(-10, effect.ApplyEffect(100, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Additive_Negative_ScalesOnBase_Percentage()
    {
        ModifierEffectOptions options = new()
            { Percentage = 0.1f, IsPositive = false, IsAdditive = true, ScalesOnBaseValue = true, ScalingFactor = 1f };
        ModifierEffect effect = new(options);
        // 100 - (0.1 * (100*1)) = 100 - (0.1*100) = 100 - 10 = 90
        Assert.AreEqual(90, effect.ApplyEffect(100, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Additive_Negative_ScalesOnBase_PercentageOfMax()
    {
        ModifierEffectOptions options = new()
        {
            PercentageOfMax = 0.1f, IsPositive = false, IsAdditive = true, ScalesOnBaseValue = true, ScalingFactor = 1f
        };
        ModifierEffect effect = new(options);
        // 100 - (0.1 * 200 * (100*1)) = 100 - (0.1*200*100) = 100 - (20*100) = 100 - 2000 = -1900
        Assert.AreEqual(-1900, effect.ApplyEffect(100, 100, 200));
    }

    // Multiplicative Negative NoScaling
    [TestMethod]
    public void ApplyEffect_Multiplicative_Negative_NoScaling_Percentage()
    {
        ModifierEffectOptions options = new() { Percentage = 0.1f, IsPositive = false, IsAdditive = false };
        ModifierEffect effect = new(options);
        // 100 - (0.1f * 100) * 100 = 100 - 10 * 100 = 100 - 1000 = -900
        Assert.AreEqual(-900, effect.ApplyEffect(100, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Multiplicative_Negative_NoScaling_PercentageOfMax()
    {
        ModifierEffectOptions options = new() { PercentageOfMax = 0.1f, IsPositive = false, IsAdditive = false };
        ModifierEffect effect = new(options);
        // 100 - (0.1f * 200) * 100 = 100 - 20 * 100 = 100 - 2000 = -1900
        Assert.AreEqual(-1900, effect.ApplyEffect(100, 100, 200));
    }

    // Multiplicative Positive ScalesOnBase
    [TestMethod]
    public void ApplyEffect_Multiplicative_Positive_ScalesOnBase_Percentage()
    {
        ModifierEffectOptions options = new()
        {
            Percentage = 0.1f, IsPositive = true, IsAdditive = false, ScalesOnBaseValue = true, ScalingFactor = 0.5f
        };
        ModifierEffect effect = new(options);
        // 100 + (0.1f * (100*0.5f)) * 100 = 100 + (0.1f*50f)*100 = 100 + 5 * 100 = 100 + 500 = 600
        Assert.AreEqual(600, effect.ApplyEffect(100, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Multiplicative_Positive_ScalesOnBase_PercentageOfMax()
    {
        ModifierEffectOptions options = new()
        {
            PercentageOfMax = 0.1f, IsPositive = true, IsAdditive = false, ScalesOnBaseValue = true,
            ScalingFactor = 0.5f
        };
        ModifierEffect effect = new(options);
        // 100 + (0.1f * 200 * (100*0.5f)) * 100 = 100 + (0.1f*200*50f)*100 = 100 + (20f*50f)*100 = 100 + 1000*100 = 100 + 100000 = 100100
        Assert.AreEqual(100100, effect.ApplyEffect(100, 100, 200));
    }

    // Multiplicative Negative ScalesOnBase
    [TestMethod]
    public void ApplyEffect_Multiplicative_Negative_ScalesOnBase_FlatAmount()
    {
        ModifierEffectOptions options = new()
            { FlatAmount = 1, IsPositive = false, IsAdditive = false, ScalesOnBaseValue = true, ScalingFactor = 0.5f };
        ModifierEffect effect = new(options);
        // 100 - (1 + round(100 * 0.5f)) * 100 = 100 - (1+50)*100 = 100 - 51*100 = 100 - 5100 = -5000
        Assert.AreEqual(-5000, effect.ApplyEffect(100, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Multiplicative_Negative_ScalesOnBase_Percentage()
    {
        ModifierEffectOptions options = new()
        {
            Percentage = 0.1f, IsPositive = false, IsAdditive = false, ScalesOnBaseValue = true, ScalingFactor = 0.5f
        };
        ModifierEffect effect = new(options);
        // 100 - (0.1f * (100*0.5f)) * 100 = 100 - (0.1f*50f)*100 = 100 - 5 * 100 = 100 - 500 = -400
        Assert.AreEqual(-400, effect.ApplyEffect(100, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Multiplicative_Negative_ScalesOnBase_PercentageOfMax()
    {
        ModifierEffectOptions options = new()
        {
            PercentageOfMax = 0.1f, IsPositive = false, IsAdditive = false, ScalesOnBaseValue = true,
            ScalingFactor = 0.5f
        };
        ModifierEffect effect = new(options);
        // 100 - (0.1f * 200 * (100*0.5f)) * 100 = 100 - (0.1f*200*50f)*100 = 100 - (20f*50f)*100 = 100 - 1000*100 = 100 - 100000 = -99900
        Assert.AreEqual(-99900, effect.ApplyEffect(100, 100, 200));
    }

    [TestMethod]
    public void ApplyEffect_Defaults_Correctly()
    {
        ModifierEffectOptions options = new();
        ModifierEffect effect = new(options);
        // Default is Additive, Positive, No Scaling, FlatAmount = 0, Percentage = 0, PercentageOfMax = 0
        Assert.AreEqual(100, effect.ApplyEffect(100, 100, 200)); // Should result in no change
    }

    [TestMethod]
    public void ApplyEffect_NoApplicationType_ReturnsCurrentValue()
    {
        // Test with IsPositive = true, IsAdditive = true, but no actual application type (Flat, Percentage, PercentageOfMax)
        ModifierEffectOptions
            options = new()
                { IsPositive = true, IsAdditive = true }; // FlatAmount, Percentage, PercentageOfMax are 0 by default
        ModifierEffect effect = new(options);
        Assert.AreEqual(100, effect.ApplyEffect(50, 100, 200),
            "Additive Positive, no application type, should not change value.");

        options = new ModifierEffectOptions { IsPositive = false, IsAdditive = true };
        effect = new ModifierEffect(options);
        Assert.AreEqual(100, effect.ApplyEffect(50, 100, 200),
            "Additive Negative, no application type, should not change value.");

        options = new ModifierEffectOptions { IsPositive = true, IsAdditive = false };
        effect = new ModifierEffect(options);
        Assert.AreEqual(100, effect.ApplyEffect(50, 100, 200),
            "Multiplicative Positive, no application type, should not change value.");

        options = new ModifierEffectOptions { IsPositive = false, IsAdditive = false };
        effect = new ModifierEffect(options);
        Assert.AreEqual(100, effect.ApplyEffect(50, 100, 200),
            "Multiplicative Negative, no application type, should not change value.");
    }
}
