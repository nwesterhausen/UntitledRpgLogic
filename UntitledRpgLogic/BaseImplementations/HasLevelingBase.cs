using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.BaseImplementations;

/// <summary>
///     Base class for objects that have leveling properties.
/// </summary>
public abstract class HasLevelingBase : IHasLeveling
{
    /// <summary>
    ///     Default maximum level for leveling objects.
    /// </summary>
    public const int DefaultMaxLevel = 1024;

    /// <summary>
    ///     Default level scaling factor for leveling objects.
    /// </summary>
    public const float DefaultLevelScalingA = 1.0f;

    /// <summary>
    ///     Default number of points required to reach the first level from level 0.
    /// </summary>
    public const int DefaultPointsForFirstLevel = 1;

    /// <summary>
    ///     An additional amount that is applied to the level scaling formula.
    /// </summary>
    public const float DefaultLevelScalingB = 0.05f;

    /// <summary>
    ///     Default tertiary scaling factor for leveling objects, used in polynomial scaling.
    /// </summary>
    public const int DefaultLevelScalingC = 2;

    /// <summary>
    ///     How many experience points this object has accumulated.
    /// </summary>
    private int _expPoints;

    /// <summary>
    ///     Initializes a new instance of the <see cref="HasLevelingBase" /> class with the specified leveling options.
    /// </summary>
    /// <param name="options"></param>
    protected HasLevelingBase(LevelingOptions options)
    {
        MaxLevel = options.MaxLevel ?? DefaultMaxLevel;
        PointsForFirstLevel = options.PointsForFirstLevel ?? DefaultPointsForFirstLevel;
        // Set the scaling factors to their default values.
        LevelScalingA = options.LevelScalingA ?? DefaultLevelScalingA;
        ScalingFactorB = options.LevelScalingB ?? DefaultLevelScalingB;
        ScalingFactorC = options.LevelScalingC ?? DefaultLevelScalingC;
        ScalingCurve = options.ScalingCurve ?? ScalingCurveType.None;

        // Ensure that the initial experience points are set to a valid value.
        ExpPoints = 0;

        // Additional initialization logic can be added here if needed.
    }

    /// <summary>
    ///     How many experience points this object has accumulated.
    /// </summary>
    private int ExpPoints
    {
        get => _expPoints;
        set
        {
            var oldValue = _expPoints;
            var oldLevel = Level;
            var newValue = value >= 0 ? value : 0;
            if (oldValue == newValue) return;

            _expPoints = newValue;
            ValueChanged?.Invoke(oldValue, newValue);

            Level = CalculateLevel();
            if (oldLevel != Level) LevelChanged?.Invoke(oldLevel, Level);
        }
    }

    /// <inheritdoc />
    public int Value => ExpPoints;

    /// <inheritdoc />
    public void AddPoint()
    {
        ExpPoints++;
    }

    /// <inheritdoc />
    public void RemovePoint()
    {
        ExpPoints--;
    }

    /// <inheritdoc />
    public void AddPoints(int points)
    {
        ExpPoints += points;
    }

    /// <inheritdoc />
    public void RemovePoints(int points)
    {
        ExpPoints -= points;
    }

    /// <inheritdoc />
    public void SetPoints(int points)
    {
#if DEBUG
        if (points < 0)
            throw new ArgumentOutOfRangeException(nameof(points), "Points cannot be negative.");
#endif
        ExpPoints = points;
    }

    /// <inheritdoc />
    public event Action<int, int>? ValueChanged;

    /// <inheritdoc />
    public int Level { get; private set; }

    /// <inheritdoc />
    public int MaxLevel { get; set; }

    /// <inheritdoc />
    public int ExperienceToNextLevel => CalculateExperienceToNextLevel();

    /// <inheritdoc />
    public float LevelScalingA { get; set; }

    /// <inheritdoc />
    public int PointsForFirstLevel { get; set; }

    /// <inheritdoc />
    public float ProgressToNextLevel
    {
        get
        {
            if (ExperienceToNextLevel == 0)
                return 1.0f; // If no experience is needed for the next level, progress is complete.
            return Math.Clamp((float)ExpPoints / ExperienceToNextLevel, 0.0f, 1.0f);
        }
    }

    /// <inheritdoc />
    public event Action<int, int>? LevelChanged;

    /// <inheritdoc />
    public float ScalingFactorB { get; set; }

    /// <inheritdoc />
    public int ScalingFactorC { get; set; }

    /// <inheritdoc />
    public ScalingCurveType ScalingCurve { get; set; }

    /// <summary>
    ///     Calculates the level based on the current experience points and scaling factor.
    /// </summary>
    /// <returns></returns>
    private int CalculateLevel()
    {
        // This method should calculate the level based on ExpPoints and LevelScaling.
        // The actual implementation will depend on the specific leveling logic you want to use.
        // For now, we return a placeholder value.
        return (int)(ExpPoints / LevelScalingA);
    }

    /// <summary>
    ///     Calculates the experience required to reach the next level based on the current level and scaling factor.
    /// </summary>
    /// <returns></returns>
    private int CalculateExperienceToNextLevel()
    {
        if (Level == 0)
            return PointsForFirstLevel;

        return ScalingCurve switch
        {
            ScalingCurveType.Linear =>
                (int)(PointsForFirstLevel * Math.Pow(LevelScalingA, Level) * (1 + ScalingFactorB)),
            ScalingCurveType.Parabolic =>
                (int)(PointsForFirstLevel * Math.Pow(Level + 1, ScalingFactorC) * (1 + ScalingFactorB) * LevelScalingA),
            ScalingCurveType.Logarithmic =>
                (int)(PointsForFirstLevel * Math.Log(Level + ScalingFactorC) * (1 + ScalingFactorB) * LevelScalingA),
            _ => // None
                (int)(PointsForFirstLevel * Math.Pow(LevelScalingA, Level))
        };
    }
}