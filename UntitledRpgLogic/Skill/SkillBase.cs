namespace UntitledRpgLogic.Skill;

public abstract partial class SkillBase : INotifyValueChanged
{
    public const double SKILL_DEFAULT_SCALING = 1.0;
    public const int SKILL_DEFAULT_POINTS_FOR_LEVEL_1 = 1;

    /// <summary>
    ///     Represents the current "points" or experience towards the next level of the skill.
    /// </summary>
    private int _points;

    /// <summary>
    ///     Represents the current level of the skill, which is typically an integer value.
    /// </summary>
    private int _value;

    /// <summary>
    ///     The name of the skill
    /// </summary>
    public string Name { get; internal set; } = string.Empty;

    /// <summary>
    ///     The scaling factor for the skill, which is used to determine how the points required for each level increase.
    /// </summary>
    public double Scaling { get; internal set; } = SKILL_DEFAULT_SCALING;

    /// <summary>
    ///     "Points" are the amount of experience or effort required to reach the next level in the skill.
    /// </summary>
    public int PointsForLevel1 { get; internal set; } = SKILL_DEFAULT_POINTS_FOR_LEVEL_1;

    /// <summary>
    ///     How many total points are required for the current level of the skill.
    /// </summary>
    public int CurrentLevelPointsRequirement
    {
        get
        {
            if (Value <= 1) return PointsForLevel1;
            return (int)Math.Round(PointsForLevel1 * Math.Pow(Scaling, Value - 1));
        }
    }

    public int Points
    {
        get => _points;
        internal set
        {
            if (value == _points) return;

            var oldValue = _points;
            _points = value;
            var pointsChange = _points - oldValue;
            var progress = (double)pointsChange / PointsForLevel1;
            LogSkillPointsChanged(Name, pointsChange.ToString(), progress.ToString("P"));
        }
    }

    /// <summary>
    ///     The level of the skill, which is typically an integer value representing the skill's proficiency or mastery.
    /// </summary>
    public int Value
    {
        get => _value;
        internal set
        {
            if (value == _value) return;

            var oldValue = _value;
            _value = value;
            OnValueChanged(oldValue, _value);
        }
    }

    /// <inheritdoc />
    public event EventHandler<ValueChangedEventArgs>? ValueChanged;

    /// <inheritdoc />
    public void OnValueChanged(int oldValue, int newValue)
    {
        throw new NotImplementedException();
    }
}