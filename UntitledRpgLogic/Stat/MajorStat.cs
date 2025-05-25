namespace UntitledRpgLogic.Stat;

/// <summary>
///   A major stat is one that can be directly modified by the player.
/// </summary>
public abstract class MajorStat(string name, int maxValue) : UntitledRpgLogic.Stat.StatBase(StatVariation.Major, name, maxValue) {

}
